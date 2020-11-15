using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Windows.Forms;

using CrazyBot.Persistance;

namespace CrazyBot.Model
{
    class CrazyBotModel
    {
        private CrazyBotInfo gameInfo;
        private int magnetPos;
        private System.Timers.Timer timer;
        private int time;
        private CrazyBotFileDataAccess persistance;

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
            return gameInfo != null;
        }
        public int getTime()
        {
            return time;
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
            return new Position(0,0);

            
        }

        internal void pause()
        {
            timer.Stop();  
        }

        internal void play()
        {
            timer.Start();
        }

        

       

        private void destructWall(int i, int j)
        {
            getBoard()[gameInfo.robot.X+i, gameInfo.robot.Y+j] = FieldType.CANNOT_WALL;
            DORefreshField(new Position(gameInfo.robot.X + i, gameInfo.robot.Y + j));
            gameInfo.robotDir = randomiseDirection(gameInfo.robotDir);
            
        }

        public void newGame(int size)
        {
            var rand = new Random();

            magnetPos = size / 2;

            int x = rand.Next(size);
            while (x == magnetPos) x = rand.Next(size);
            int y = rand.Next(size);
            while (y == magnetPos) y = rand.Next(size);

            gameInfo = new CrazyBotInfo(size, new Position(x, y), 0, RobotDirection.UP, FieldType.NO_WALL, rand.Next(16));
            DORefreshBoard();

            time = 0;
            timer.Start();


        }
        private void onElapsed(object obj, EventArgs e)
        {
            if (isInGame())
            {
                gameInfo.time = gameInfo.time + 1;
            }

        }

        private void moveRobot(object obj, EventArgs e)
        {
            if (isInGame())
            {
                //Robot pick-up
                Position prevPostionOfRobot = gameInfo.robot;
                FieldType robotPrev = gameInfo.fieldTypeOnRobot;
                getBoard()[prevPostionOfRobot.X, prevPostionOfRobot.Y] = robotPrev;
                DORefreshField(prevPostionOfRobot);

                //Robot put-down
                oneStepRobot();
                if (getBoard()[gameInfo.robot.X, gameInfo.robot.Y] == FieldType.MAGNET)
                { 
                    gameOver();
                }
                else 
                {
                    gameInfo.fieldTypeOnRobot = getBoard()[gameInfo.robot.X, gameInfo.robot.Y];
                    getBoard()[gameInfo.robot.X, gameInfo.robot.Y] = FieldType.ROBOT;
                    DORefreshField(gameInfo.robot);
                }
                
            }
        }

        private void gameOver()
        {
            timer.Stop();
            DOgameOver();
        }

        internal void AdvanceTime(object obj, EventArgs e)
        {
            //Advancing time
            time++;
            DORefreshTime();

            //Moveing robot
            if (time > 1)
            moveRobot(this, new EventArgs());

            //Make robot even crazier
            gameInfo.timeLeftUntilCrazy--;
            if (gameInfo.timeLeftUntilCrazy == 0)
            {
                gameInfo.robotDir = randomiseDirection(gameInfo.robotDir);
                gameInfo.timeLeftUntilCrazy = (new Random()).Next(16); 

            }
        }

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


        internal async void saveGame(string path)
        {
            
            if (gameInfo == null)
                throw new InvalidOperationException("No data access is provided.");

            await persistance.SaveAsync(path, gameInfo);
        }

        internal void loadFromFile(string filePath)
        {

        }



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

        private void DOdisplayPaused()
        {
            displayPaused?.Invoke(this, new EventArgs());
        }

        #endregion

    }
}
