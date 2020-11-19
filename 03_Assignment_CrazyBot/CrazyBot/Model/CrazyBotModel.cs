using System;
using System.Timers;

using CrazyBot.Persistance;

namespace CrazyBot.Model
{
    public class CrazyBotModel
    {
        private CrazyBotInfo gameInfo;
        private int magnetPos;
        private System.Timers.Timer timer;
        private ICrazyBotDataModel persistance;
        private bool gameIsOver;

        public event EventHandler<EventArgs> refreshBoard;
        public event EventHandler<FieldRefreshEventArgs> refreshField;
        public event EventHandler<EventArgs> refreshTime;
        public event EventHandler<EventArgs> OnGameOver;
        public event EventHandler<EventArgs> displayPaused;

        public CrazyBotModel()
        {
            gameInfo = null;
            magnetPos = -1;
            timer = new System.Timers.Timer(1000);
            gameIsOver = false;

            persistance = new CrazyBotFileDataAccess();
            timer.Elapsed += new ElapsedEventHandler(AdvanceTime);
        }


        #region Getters & Setters
        public Position getRobotPos()
        {
            return gameInfo.robot;
        }
        public Position getMagnetPos()
        {
            return new Position(gameInfo.size / 2, gameInfo.size / 2);
        }
        public int getSize()
        {
            return gameInfo.size;
        }
        public bool isInGame()
        {
            return gameInfo != null && !gameIsOver;
        }
        public int getTime()
        {
            return (int)gameInfo.time;
        }
        public FieldType[,] getBoard()
        {
            return gameInfo.board;
        }
        public FieldType getFieldTypeOnRobot()
        {
            return gameInfo.fieldTypeOnRobot;
        }

        #endregion

        #region Robot Handlers :: direction, step, move, destructwall, invertwall

        public void invertWall(Position p)
        {
            if (getRobotPos().Equals(p)) return;
            if (getMagnetPos().Equals(p)) return;
            if (getBoard()[p.X, p.Y] == FieldType.NO_WALL)
            {
                getBoard()[p.X, p.Y] = FieldType.WALL;
                DORefreshField(p);
            }
        }
        private void moveRobot(object obj, EventArgs e)
        {
            if (isInGame())
            {
                //Robot pick-up
                Position prevPostionOfRobot = new Position(gameInfo.robot.X, gameInfo.robot.Y);
                FieldType robotPrev = gameInfo.fieldTypeOnRobot;
                getBoard()[prevPostionOfRobot.X, prevPostionOfRobot.Y] = robotPrev;


                //Robot put-down
                oneStepRobot();
                DORefreshField(prevPostionOfRobot);
                if (getBoard()[gameInfo.robot.X, gameInfo.robot.Y] == FieldType.MAGNET)
                {
                    gameOver();
                }
                else
                {
                    gameInfo.fieldTypeOnRobot = getBoard()[gameInfo.robot.X, gameInfo.robot.Y];
                    //getBoard()[gameInfo.robot.X, gameInfo.robot.Y] = FieldType.ROBOT;
                    DORefreshField(gameInfo.robot);
                }

            }
        }
        private RobotDirection randomiseDirection(RobotDirection except)
        {

            Random rand = new Random();
            RobotDirection r = (RobotDirection)rand.Next(4);
            while (r == except)
            {
                r = (RobotDirection)rand.Next(4);
            }
            return r;
        }
        private Position oneStepRobot()
        {
            RobotDirection rdir = gameInfo.robotDir;

            Position roboAlgo(int x, int y)
            {
                //Check wheter it would get off from the table
                if (gameInfo.robot.Y + y >= 0 && gameInfo.robot.Y + y < gameInfo.size &&
                    gameInfo.robot.X + x >= 0 && gameInfo.robot.X + x < gameInfo.size)
                {
                    if (getBoard()[gameInfo.robot.X + x, gameInfo.robot.Y + y] == FieldType.WALL)
                    {
                        destructWall(x, y);

                        return oneStepRobot();
                    }
                    else
                    {
                        gameInfo.robot.Y += y;
                        gameInfo.robot.X += x;
                    }
                }
                else
                {
                    gameInfo.robotDir = randomiseDirection(gameInfo.robotDir);
                    return oneStepRobot();

                }
                return new Position(gameInfo.robot.X + x, gameInfo.robot.Y + y);
            }

            if (rdir == RobotDirection.UP)
            {
                return roboAlgo(0, -1);
            }
            else if (rdir == RobotDirection.DOWN)
            {
                return roboAlgo(0, 1);
            }
            else if (rdir == RobotDirection.LEFT)
            {
                return roboAlgo(-1, 0);
            }
            else if (rdir == RobotDirection.RIGHT)
            {
                return roboAlgo(1, 0);
            }
            return new Position(0, 0);


        }
        private void destructWall(int i, int j)
        {
            getBoard()[gameInfo.robot.X + i, gameInfo.robot.Y + j] = FieldType.CANNOT_WALL;
            DORefreshField(new Position(gameInfo.robot.X + i, gameInfo.robot.Y + j));
            gameInfo.robotDir = randomiseDirection(gameInfo.robotDir);

        }


        #endregion

        #region Game Management :: play, pause, gameOver, newGame, advanceTime

        public void pause()
        {
            timer.Stop();  
        }

        public void play()
        {
            timer.Start();
        }

        private void gameOver()
        {
            gameIsOver = true;
            timer.Stop();
            DOgameOver();
        }

        public void newGame(int size, CrazyBotInfo gameInfoTOImport = null)
        {
            var rand = new Random();

            magnetPos = size / 2;



            if (gameInfoTOImport == null)
            {
                gameIsOver = false;
                int x = rand.Next(size);
                while (x == magnetPos) x = rand.Next(size);
                int y = rand.Next(size);
                while (y == magnetPos) y = rand.Next(size);
                gameInfo = new CrazyBotInfo(size, new Position(x, y), 0, RobotDirection.UP, FieldType.NO_WALL, rand.Next(16));
            }
            else
            {
                gameInfo = gameInfoTOImport;
            }

            DORefreshBoard();

            //gameInfo.time = 0;
            timer.Start();


        }

        public void AdvanceTime(object obj, EventArgs e)
        {
            //Advancing time
            gameInfo.time++;
            DORefreshTime();

            //Moveing robot
            if (gameInfo.time > 1)
                moveRobot(this, new EventArgs());

            //Make robot even crazier
            gameInfo.timeLeftUntilCrazy--;
            if (gameInfo.timeLeftUntilCrazy == 0)
            {
                gameInfo.robotDir = randomiseDirection(gameInfo.robotDir);
                gameInfo.timeLeftUntilCrazy = (new Random()).Next(16);

            }
        }

        #endregion

        #region Save & Load

        public void saveGame(string path)
        {

            if (gameInfo == null)
                throw new InvalidOperationException("No data access is provided.");

             persistance.Save(path, gameInfo);
        }

        public void loadFromFile(string path)
        {
            CrazyBotInfo info = persistance.Load(path);
            newGame(info.size, info);
            pause();
        }
        #endregion

        #region Event SEND-ers aka. DO
        private void DORefreshBoard()
        {
            refreshBoard?.Invoke(this, new EventArgs());
        }
       
        private void DORefreshField(Position p)
        {
            refreshField?.Invoke(this, new FieldRefreshEventArgs(p));
        }

        private void DORefreshTime()
        {
            refreshTime?.Invoke(this, new EventArgs());
        }

        private void DOgameOver()
        {
            OnGameOver?.Invoke(this, new EventArgs());
        }

        

        #endregion

    }
}
