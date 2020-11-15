using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyBot.Persistance
{
    enum FieldType { NO_WALL = 0, WALL = 1, CANNOT_WALL = 2, ROBOT = 3, MAGNET = 4 }
    enum RobotDirection { UP = 0, DOWN = 1, LEFT = 2, RIGHT = 3 }
    class CrazyBotInfo
    {
        public int size { get; private set; }
        public FieldType[,] board { get; set; }
        public Position robot { get; set; }
        public RobotDirection robotDir { get; set; }
        public FieldType fieldTypeOnRobot { get; set; }
        public ulong time { get; set;  }
        public int timeLeftUntilCrazy { get; set; }




        public CrazyBotInfo(int _size, Position _robot, ulong _time, RobotDirection _rd = RobotDirection.UP, FieldType fieldOnBot = FieldType.NO_WALL, int timeleft = -1)
        {
            size = _size;
            robot = _robot;
            robotDir = _rd;
            time = _time;
            board = new FieldType[size, size];
            fieldTypeOnRobot = fieldOnBot;


            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    board[i, j] = FieldType.NO_WALL;
                }
            }

            board[robot.X, robot.Y] = FieldType.ROBOT;
            board[size / 2, size / 2] = FieldType.MAGNET;

            if (timeleft < 0)
            {
                Random rand = new Random();
                timeLeftUntilCrazy = rand.Next(16);

            }
            else timeLeftUntilCrazy = timeleft;
        }

    }
}
