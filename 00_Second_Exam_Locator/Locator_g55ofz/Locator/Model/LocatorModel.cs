using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Locator.Model
{
    public class LocatorModel
    {
        private int size;
        private int[,] board;
        private bool radar;
        private int points;
        private int enemy;

        public event EventHandler<FieldRefreshEventArgs> refreshField;
        public event EventHandler<EventArgs> refreshBoard;
        public event EventHandler<int> OnGameOver;
        public event EventHandler<String> StatusStrip;


        public LocatorModel(int startingSize = 9)
        {
            newGame(startingSize);
        }
        public int getSize()
        {
            return size;
        }

        public bool isInGame()
        {
            return true;
        }

        public int[,] getBoard()
        {
            return board;
        }

        public void newGame(int paramsize)
        {
            size = paramsize;
            board = new int[size, size];
            radar = false;
            points = 0;
            enemy = size;
            DOStatusStrip("Bomb mode");

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    board[i, j] = 0;
                }
            }

            Random rand = new Random();
            List<Position> prev = new List<Position>(size);

            for (int i = 0; i < size; i++)
            {

                int x = rand.Next(size);
                int y = rand.Next(size);
                Position p = new Position(x, y);
                while(prev.Contains(p))
                {
                    x = rand.Next(size);
                    y = rand.Next(size);
                    p = new Position(x, y);
                }
                prev.Add(p);

                board[x, y] = 1;
            }
        }

        private bool CheckGameOver()
        {
            if (enemy == 0) {
                DOgameOver(points);
                newGame(size);
                DORefreshBoard();
            }
            return enemy == 0;
        }

        public void SwitchMode()
        {
            radar = !radar;
            if (radar)
                DOStatusStrip("Radar mode");
            else
            {
                DOStatusStrip("Bomb mode");
            }
        }

        public void Step(Position p)
        {
            if (!radar)
            {
                points++;
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        int x = p.X + i;
                        int y = p.Y + j;

                        if (0 <= x && 0 <= y && x < size && y < size)
                        {
                            if (board[x, y] % 2 == 1)
                            {
                                board[x, y] = 2;
                                enemy--;
                                //DORefreshField(new Position(p.X + i, p.Y + j));
                                CheckGameOver();

                            }

                        }
                       
                    }
                }
            }

            else
            {
                points++;
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (board[i, j] > 2)
                            board[i, j] -= 2;
                    }
                }
                DORefreshBoard();

                for (int i = -2; i <= 2; i++)
                {
                    for (int j = -2; j <= 2; j++)
                    {
                        int x = p.X + i;
                        int y = p.Y + j;

                        if (0 <= x && 0 <= y && x < size && y < size)
                        {
                            if (board[x, y] != 0)
                            {
                                board[x, y] += 2;
                                DORefreshField(new Position(p.X + i, p.Y + j));
                                CheckGameOver();

                            }

                        }

                    }
                }
            }
        }

        private void DORefreshField(Position p)
        { 
            refreshField?.Invoke(this, new FieldRefreshEventArgs(p));
        }

        private void DOgameOver(int i)
        {
            OnGameOver?.Invoke(this, i);
        }

        private void DORefreshBoard()
        {
            refreshBoard?.Invoke(this, new EventArgs());
        }

        private void DOStatusStrip(String s)
        {
            StatusStrip?.Invoke(this, s);
        }
    }
}
