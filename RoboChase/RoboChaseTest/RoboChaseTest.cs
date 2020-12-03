using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoboChase.Model;
using RoboChase.Persistance;
using System.Diagnostics;
using System;
using Moq;
using System.Threading.Tasks;

namespace RoboChase.Test
{
    [TestClass]
    public class RoboChaseTest
    {

        private RoboChaseModel _model; 
        private RoboChaseInfo _mockedTable;
        private Mock<IRoboChaseData> _mock;

        [TestInitialize]
        public void Initialize()
        {
            _mockedTable = new RoboChaseInfo(5, new Position(1,2), 0);
            _mock = new Mock<IRoboChaseData>();
            _mock.Setup(mock => mock.LoadAsync(It.IsAny<String>()))
                        .Returns(() => Task.FromResult(_mockedTable));

            _model = new RoboChaseModel(_mock.Object);
        }

        

        [TestMethod]
        public void ConstructorCheck()
        {
            int size = 7;
            Position robotpos = new Position(1, 1);
            ulong time = 0;
            RobotDirection robotdir = RobotDirection.UP;
            FieldType fieldOnBot = FieldType.NO_WALL;
            int timeleftcrazy = 8;

            RoboChaseInfo gameInfo = new RoboChaseInfo(size, robotpos, time, robotdir, fieldOnBot, timeleftcrazy);
            RoboChaseModel gameModel = new RoboChaseModel();
            gameModel.newGame(7, gameInfo);
            Assert.AreEqual(gameModel.getRobotPos(), gameInfo.robot);
            Assert.AreEqual(gameModel.getSize(), 7);
            Assert.AreEqual(gameInfo.size, 7);
          
            RoboChaseInfo gameInfo11 = new RoboChaseInfo(11, robotpos, time, robotdir, fieldOnBot, timeleftcrazy);
            gameModel.newGame(11, gameInfo11);
            Assert.AreEqual(gameModel.getRobotPos(), gameInfo11.robot);
            Assert.AreEqual(gameModel.getSize(), 11);
            Assert.AreEqual(gameInfo11.size, 11);

            for (int i = 0; i < 10; i++)
            {
                int prev = gameModel.getTime();
                gameModel.AdvanceTime(this, new System.EventArgs());
                if (gameModel.isInGame()) Assert.IsTrue(prev < gameModel.getTime());

            }

        }

        [TestMethod]
        public void MoveRobot() {
            int size = 7;
            Position robotpos = new Position(1, 1);
            ulong time = 0;
            RobotDirection robotdir = RobotDirection.UP;
            FieldType fieldOnBot = FieldType.NO_WALL;
            int timeleftcrazy = 8;

            RoboChaseInfo gameInfo = new RoboChaseInfo(size, robotpos, time, robotdir, fieldOnBot, timeleftcrazy);
            RoboChaseModel gameModel = new RoboChaseModel();
            gameModel.newGame(7, gameInfo);
            Assert.AreEqual(gameModel.getSize(), 7);
            Assert.AreEqual(gameInfo.size, 7);

            Assert.AreEqual(gameModel.getRobotPos(), gameInfo.robot);
            gameModel.AdvanceTime(this, new System.EventArgs());
            gameModel.AdvanceTime(this, new System.EventArgs());
            Assert.AreEqual(gameModel.getRobotPos(), gameInfo.robot);
            Console.WriteLine(gameModel.getRobotPos().X + ", " + gameModel.getRobotPos().Y);
            Assert.AreEqual(gameModel.getRobotPos(), new Position(1,0));

            gameInfo.robotDir = RobotDirection.DOWN;
            gameModel.AdvanceTime(this, new System.EventArgs());
            Assert.AreEqual(gameModel.getRobotPos(), gameInfo.robot);
            Assert.AreEqual(gameModel.getRobotPos(), new Position(1, 1));

            gameInfo.robotDir = RobotDirection.RIGHT;
            gameModel.AdvanceTime(this, new System.EventArgs());
            Assert.AreEqual(gameModel.getRobotPos(), gameInfo.robot);
            Assert.AreEqual(gameModel.getRobotPos(), new Position(2, 1));

            gameInfo.robotDir = RobotDirection.LEFT;
            gameModel.AdvanceTime(this, new System.EventArgs());
            Assert.AreEqual(gameModel.getRobotPos(), gameInfo.robot);
            Assert.AreEqual(gameModel.getRobotPos(), new Position(1, 1));

        }

        [TestMethod]
        public void PlaceWalls() {
            int size = 7;
            Position robotpos = new Position(1, 1);
            ulong time = 0;
            RobotDirection robotdir = RobotDirection.UP;
            FieldType fieldOnBot = FieldType.NO_WALL;
            int timeleftcrazy = 8;

            RoboChaseInfo gameInfo = new RoboChaseInfo(size, robotpos, time, robotdir, fieldOnBot, timeleftcrazy);
            RoboChaseModel gameModel = new RoboChaseModel();
            gameModel.newGame(7, gameInfo);
            Assert.AreEqual(gameModel.getSize(), 7);
            Assert.AreEqual(gameInfo.size, 7);

            //Place walls
            Assert.AreEqual(gameModel.getBoard()[6, 6], FieldType.NO_WALL);
            gameModel.invertWall(new Position(6, 6));
            Assert.AreEqual(gameModel.getBoard()[6, 6], FieldType.WALL);

            Assert.AreEqual(gameModel.getBoard()[3, 1], FieldType.NO_WALL);
            gameModel.invertWall(new Position(3, 1));
            Assert.AreEqual(gameModel.getBoard()[3, 1], FieldType.WALL);

            Assert.AreEqual(gameModel.getBoard()[2, 5], FieldType.NO_WALL);
            gameModel.invertWall(new Position(2, 5));
            Assert.AreEqual(gameModel.getBoard()[2, 5], FieldType.WALL);

            //Try to pick up those walls
            Assert.AreEqual(gameModel.getBoard()[6, 6], FieldType.WALL);
            gameModel.invertWall(new Position(6, 6));
            Assert.AreEqual(gameModel.getBoard()[6, 6], FieldType.WALL);

            Assert.AreEqual(gameModel.getBoard()[3, 1], FieldType.WALL);
            gameModel.invertWall(new Position(3, 1));
            Assert.AreEqual(gameModel.getBoard()[3, 1], FieldType.WALL);

            Assert.AreEqual(gameModel.getBoard()[2, 5], FieldType.WALL);
            gameModel.invertWall(new Position(2, 5));
            Assert.AreEqual(gameModel.getBoard()[2, 5], FieldType.WALL);

            //Try place walls on robot and magnet
            Console.WriteLine(gameModel.getBoard()[gameInfo.robot.X, gameInfo.robot.Y].ToString());
            Assert.IsTrue(gameModel.getBoard()[gameInfo.robot.X, gameInfo.robot.Y] == FieldType.ROBOT);
            gameModel.invertWall(new Position(gameInfo.robot.X, gameInfo.robot.Y));
            Assert.IsTrue(gameModel.getBoard()[gameInfo.robot.X, gameInfo.robot.Y] == FieldType.ROBOT);
            Assert.IsFalse(gameModel.getBoard()[gameInfo.robot.X, gameInfo.robot.Y] == FieldType.WALL);

            Assert.IsTrue(gameModel.getBoard()[gameModel.getMagnetPos().X, gameModel.getMagnetPos().Y] == FieldType.MAGNET);
            gameModel.invertWall(new Position(6, 6));
            Assert.IsTrue(gameModel.getBoard()[gameModel.getMagnetPos().X, gameModel.getMagnetPos().Y] == FieldType.MAGNET);
            Assert.IsFalse(gameModel.getBoard()[gameModel.getMagnetPos().X, gameModel.getMagnetPos().Y] == FieldType.WALL);

        }

        [TestMethod]
        public void HitPlacedWall() { //TODO: CSINÁLD MEG A MOCK-OLÁST
            int size = 7;
            Position robotpos = new Position(1, 1);
            ulong time = 0;
            RobotDirection robotdir = RobotDirection.RIGHT;
            FieldType fieldOnBot = FieldType.NO_WALL;
            int timeleftcrazy = 8;

            RoboChaseInfo gameInfo = new RoboChaseInfo(size, robotpos, time, robotdir, fieldOnBot, timeleftcrazy);
            RoboChaseModel gameModel = new RoboChaseModel();
            gameModel.newGame(7, gameInfo);
            Assert.AreEqual(gameModel.getSize(), 7);
            Assert.AreEqual(gameInfo.size, 7);

            RobotDirection prevDir;
            gameModel.AdvanceTime(this, new EventArgs());

            //Place walls and hit it from RIGHT
            prevDir = gameInfo.robotDir;
            Assert.AreEqual(gameModel.getBoard()[2, 1], FieldType.NO_WALL);
            gameModel.invertWall(new Position(2, 1));
            Assert.AreEqual(gameModel.getBoard()[2, 1], FieldType.WALL);
            gameModel.AdvanceTime(this, new System.EventArgs());
            Assert.AreEqual(gameModel.getBoard()[2, 1], FieldType.CANNOT_WALL);

            Assert.AreNotEqual(prevDir, gameInfo.robotDir);


            //Place walls and hit it from LEFT;
            gameInfo.robotDir = RobotDirection.LEFT;
            prevDir = gameInfo.robotDir;

            gameInfo.robot = new Position(2, 2);
            Assert.AreEqual(gameModel.getBoard()[1, 2], FieldType.NO_WALL);
            gameModel.invertWall(new Position(1, 2));
            Assert.AreEqual(gameModel.getBoard()[1, 2], FieldType.WALL);
            gameModel.AdvanceTime(this, new System.EventArgs());
            Assert.AreEqual(gameModel.getBoard()[1, 2], FieldType.CANNOT_WALL);

            Assert.AreNotEqual(prevDir, gameInfo.robotDir);


            //Place walls and hit it from UP;
            gameInfo.robotDir = RobotDirection.UP;
            prevDir = gameInfo.robotDir;

            gameInfo.robot = new Position(5, 3);
            Assert.AreEqual(gameModel.getBoard()[5, 2], FieldType.NO_WALL);
            gameModel.invertWall(new Position(5, 2));
            Assert.AreEqual(gameModel.getBoard()[5, 2], FieldType.WALL);
            gameModel.AdvanceTime(this, new System.EventArgs());
            Assert.AreEqual(gameModel.getBoard()[5, 2], FieldType.CANNOT_WALL);

            Assert.AreNotEqual(prevDir, gameInfo.robotDir);


            //Place walls and hit it from DOWN;
            gameInfo.robotDir = RobotDirection.DOWN;
            prevDir = gameInfo.robotDir;

            gameInfo.robot = new Position(4, 3);
            Assert.AreEqual(gameModel.getBoard()[4, 4], FieldType.NO_WALL);
            gameModel.invertWall(new Position(4, 4));
            Assert.AreEqual(gameModel.getBoard()[4, 4], FieldType.WALL);
            gameModel.AdvanceTime(this, new System.EventArgs());
            Assert.AreEqual(gameModel.getBoard()[1, 2], FieldType.CANNOT_WALL);

            Assert.AreNotEqual(prevDir, gameInfo.robotDir);

        }

        [TestMethod]
        public void HitEdge() {
            int size = 7;
            Position robotpos = new Position(1, 1);
            ulong time = 0;
            RobotDirection robotdir = RobotDirection.LEFT;
            FieldType fieldOnBot = FieldType.NO_WALL;
            int timeleftcrazy = 8;

            RoboChaseInfo gameInfo = new RoboChaseInfo(size, robotpos, time, robotdir, fieldOnBot, timeleftcrazy);
            RoboChaseModel gameModel = new RoboChaseModel();
            gameModel.newGame(7, gameInfo);
            Assert.AreEqual(gameModel.getSize(), 7);
            Assert.AreEqual(gameInfo.size, 7);

            RobotDirection prevDir;
            gameModel.AdvanceTime(this, new EventArgs());

            //LEFT
            gameInfo.robotDir = RobotDirection.LEFT;
            gameInfo.robot = new Position(1, 1);

            prevDir = gameInfo.robotDir;
            gameModel.AdvanceTime(this, new System.EventArgs());
            gameModel.AdvanceTime(this, new System.EventArgs());
            Assert.AreNotEqual(prevDir, gameInfo.robotDir);

            //RIGHT
            gameInfo.robotDir = RobotDirection.RIGHT;
            gameInfo.robot = new Position(5, 1);

            prevDir = gameInfo.robotDir;
            gameModel.AdvanceTime(this, new System.EventArgs());
            gameModel.AdvanceTime(this, new System.EventArgs());
            Assert.AreNotEqual(prevDir, gameInfo.robotDir);

            //UP
            gameInfo.robotDir = RobotDirection.UP;
            gameInfo.robot = new Position(5, 1);

            prevDir = gameInfo.robotDir;
            gameModel.AdvanceTime(this, new System.EventArgs());
            gameModel.AdvanceTime(this, new System.EventArgs());
            Assert.AreNotEqual(prevDir, gameInfo.robotDir);

            //DOWN
            gameInfo.robotDir = RobotDirection.DOWN;
            gameInfo.robot = new Position(5, 5);

            prevDir = gameInfo.robotDir;
            gameModel.AdvanceTime(this, new System.EventArgs());
            gameModel.AdvanceTime(this, new System.EventArgs());
            Assert.AreNotEqual(prevDir, gameInfo.robotDir);


        }

        [TestMethod]
        public void WalkOnCannotWall() {
            int size = 7;
            Position robotpos = new Position(1, 1);
            ulong time = 0;
            RobotDirection robotdir = RobotDirection.UP;
            FieldType fieldOnBot = FieldType.NO_WALL;
            int timeleftcrazy = 8;

            RoboChaseInfo gameInfo = new RoboChaseInfo(size, robotpos, time, robotdir, fieldOnBot, timeleftcrazy);
            RoboChaseModel gameModel = new RoboChaseModel();
            gameModel.newGame(7, gameInfo);
            Assert.AreEqual(gameModel.getSize(), 7);
            Assert.AreEqual(gameInfo.size, 7);
            gameModel.AdvanceTime(this, new System.EventArgs());


            //Place walls
            Assert.AreEqual(gameModel.getBoard()[2, 4], FieldType.NO_WALL);
            gameModel.invertWall(new Position(2, 4));
            Assert.AreEqual(gameModel.getBoard()[2, 4], FieldType.WALL);
            gameInfo.robot = new Position(2, 5);
            gameModel.AdvanceTime(this, new System.EventArgs());
            Assert.AreEqual(gameModel.getBoard()[2, 4], FieldType.CANNOT_WALL);

        }

        [TestMethod]
        public void RobotGotMagnet() {
            int size = 7;
            Position robotpos = new Position(1, 1);
            ulong time = 0;
            RobotDirection robotdir = RobotDirection.UP;
            FieldType fieldOnBot = FieldType.NO_WALL;
            int timeleftcrazy = 8;

            RoboChaseInfo gameInfo = new RoboChaseInfo(size, robotpos, time, robotdir, fieldOnBot, timeleftcrazy);
            RoboChaseModel gameModel = new RoboChaseModel();
            gameModel.newGame(7, gameInfo);
            Assert.AreEqual(gameModel.getSize(), 7);
            Assert.AreEqual(gameInfo.size, 7);
            gameModel.AdvanceTime(this, new System.EventArgs());

            Assert.IsTrue(gameModel.isInGame());
            gameInfo.robot = new Position(size/2, (size/2)+1);
            gameModel.AdvanceTime(this, new System.EventArgs());
            Assert.IsFalse(gameModel.isInGame());

            //11
            size = 11;
            gameInfo = new RoboChaseInfo(size, robotpos, time, robotdir, fieldOnBot, timeleftcrazy);
            gameModel = new RoboChaseModel();
            gameModel.newGame(size, gameInfo);
            Assert.AreEqual(gameModel.getSize(), 11);
            Assert.AreEqual(gameInfo.size, 11);
            gameModel.AdvanceTime(this, new System.EventArgs());

            Assert.IsTrue(gameModel.isInGame());
            gameInfo.robot = new Position(size / 2, (size / 2) + 1);
            gameModel.AdvanceTime(this, new System.EventArgs());
            Assert.IsFalse(gameModel.isInGame());

            //15
            size = 15;
            gameInfo = new RoboChaseInfo(size, robotpos, time, robotdir, fieldOnBot, timeleftcrazy);
            gameModel = new RoboChaseModel();
            gameModel.newGame(size, gameInfo);
            Assert.AreEqual(gameModel.getSize(), 15);
            Assert.AreEqual(gameInfo.size, 15);
            gameModel.AdvanceTime(this, new System.EventArgs());

            Assert.IsTrue(gameModel.isInGame());
            gameInfo.robot = new Position(size / 2, (size / 2) + 1);
            gameModel.AdvanceTime(this, new System.EventArgs());
            Assert.IsFalse(gameModel.isInGame());
        }

        [TestMethod]
        public void RobotRandomChange() {
            int size = 7;
            Position robotpos = new Position(1, 1);
            ulong time = 0;
            RobotDirection robotdir = RobotDirection.UP;
            FieldType fieldOnBot = FieldType.NO_WALL;
            int timeleftcrazy = 2;

            RoboChaseInfo gameInfo = new RoboChaseInfo(size, robotpos, time, robotdir, fieldOnBot, timeleftcrazy);
            RoboChaseModel gameModel = new RoboChaseModel();
            gameModel.newGame(7, gameInfo);
            Assert.AreEqual(gameModel.getSize(), 7);
            Assert.AreEqual(gameInfo.size, 7);
            gameModel.AdvanceTime(this, new System.EventArgs());

            Assert.IsTrue(gameModel.isInGame());
            RobotDirection prev = gameInfo.robotDir;
            Assert.AreEqual(gameInfo.robotDir, prev);

            gameModel.AdvanceTime(this, new System.EventArgs());
            gameModel.AdvanceTime(this, new System.EventArgs());

            Assert.AreNotEqual(gameInfo.robotDir, prev);



        }



    }
}
