using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyBot
{
    enum FieldType { NO_WALL = 0, WALL = 1, CANNOT_WALL = 2, ROBOT = 3, MAGNET = 4 }
    class CrazyBotInfo
    {
        public int size { get; private set; }
        public FieldType[,] board { get; set; }
        public Tuple<int, int> robot { get; set; }
        public ulong time { get; set;  }

        public CrazyBotInfo(int _size, Tuple<int, int> _robot, ulong _time)
        {
            size = _size;
            robot = _robot;
            time = _time;
            board = new FieldType[size, size];

          
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    board[i, j] = FieldType.NO_WALL;
                }
            }
            board[_robot.Item1, _robot.Item2] = FieldType.ROBOT; 
            board[size / 2, size / 2] = FieldType.MAGNET; //Magnet
            
        }

    }
}
