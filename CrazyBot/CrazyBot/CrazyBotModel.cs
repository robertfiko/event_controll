using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace CrazyBot
{
   
    class CrazyBotModel
    {
        private Timer timer;
        private CrazyBotInfo gameInfo;
        private int magnetPos;

        public CrazyBotModel()
        {
            gameInfo = null;
            timer = new Timer(1000);
            timer.Elapsed += onElapsed;
            magnetPos = -1;
        }


        public FieldType[,] getBoard()
        {
            return gameInfo.board;
        }
        public void newGame(int size)
        {
            var rand = new Random();

            magnetPos = size / 2;

            int x = rand.Next(size);
            while (x == magnetPos) x = rand.Next(size);
            int y = rand.Next(size);
            while (y == magnetPos) y = rand.Next(size);

            gameInfo = new CrazyBotInfo(size, new Tuple<int, int>(x,y), 0);
            invokeRefreshBoard();

        }

        private void onElapsed(object obj, EventArgs e)
        {
            if (isInGame())
            {
                gameInfo.time = gameInfo.time + 1;
            }
        }


       

        public void invertWall(int x, int y)
        {
            if (getBoard()[x, y] == FieldType.NO_WALL)
            {
                getBoard()[x, y] = FieldType.WALL;
            }

            invokeRefreshField(x, y);
        }

        public event EventHandler<EventArgs> refreshBoard;
        public event EventHandler<FieldRefreshEventArgs> refreshField;


        private void invokeRefreshBoard()
        {
            this.refreshBoard?.Invoke(this, new EventArgs());
        }
        private void invokeRefreshField(int i, int j)
        {
            this.refreshField?.Invoke(this, new FieldRefreshEventArgs(i,j));
        }

        public int getSize()
        {
            return gameInfo.size;
        }
        public bool isInGame()
        {
            return gameInfo != null;
        }
        public void startTimer()
        {
            timer.Start();
        }

        public void stopTimer()
        {
            timer.Stop();
        }

        /*public void resetTimer()
        {
            timer.;
        }*/

        public int getTime()
        {
            return Convert.ToInt32(timer);
        }
    }
}
