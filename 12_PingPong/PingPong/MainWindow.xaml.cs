using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PingPong
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Thickness ballStartPosition;
        Thickness ballCurrentPosition;
        Thickness ballNextPosition;
        Thickness padStartPosition;
        Thickness padCurrentPostition;

        ThicknessAnimation ballAnimation;
        ThicknessAnimation padAnimation;

        DateTime startTime;

        bool isStarted;
        float speedFactor;




        public MainWindow()
        {
            InitializeComponent();

            padStartPosition = padCurrentPostition = rectanglePad.Margin;
            ballStartPosition = ellipseBall.Margin;
            ballNextPosition = new Thickness();
            isStarted = false;

            this.KeyDown += Navigate;
            ellipseBall.LayoutUpdated += BallLayoutUpdated;
        }

        private void BallLayoutUpdated(object sender, EventArgs e)
        {
            if (isStarted)
            {
                //utőnek balról
                //ütőnek jobbról

                //tetőnek
                if (ballCurrentPosition.Top == 0)
                {
                    ballNextPosition.Top = this.Height;
                }

                //ablak baloldalának
                if (ballCurrentPosition.Left == 0)
                {
                    ballNextPosition.Left = this.Width;
                }
            }

        }

        private void Exit(object sender, EventArgs e)
        {
            this.Close();
        }



        private void NewGame(object sender, RoutedEventArgs e)
        {
            StartGame();
        }

        private void StartGame()
        {
            Random rand = new Random();
            ballStartPosition = ellipseBall.Margin;
            ballCurrentPosition = ellipseBall.Margin;
            padStartPosition = rectanglePad.Margin;
            padCurrentPostition = rectanglePad.Margin;

            int dir = (rand.Next(0, 2) == 0) ? (int)this.Height : 0;

            ballCurrentPosition = new Thickness(rand.NextDouble() * this.Width, dir, 0, 0);

            startTime = DateTime.Now;

            isStarted = true;
            speedFactor = 1;

            AnimateBall();
        }

        private void StopGame() 
        {
            isStarted = false;
        }

        private void AnimateBall()
        {
            ballAnimation = new ThicknessAnimation(ballCurrentPosition, ballNextPosition, new Duration(TimeSpan.FromMilliseconds(5)));
            float SpeedRatio = speedFactor / BallTravelDistance();

            ellipseBall.BeginAnimation(Ellipse.MarginProperty, ballAnimation, HandoffBehavior.SnapshotAndReplace);
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void Navigate(object sender, KeyEventArgs e)
        {
            padCurrentPostition = rectanglePad.Margin;
            if (e.Key == Key.Left)
            {
                if (padCurrentPostition.Left > 0)
                {
                    AnimatePad(-100);
                }
            }

            else if (e.Key == Key.Right)
            {
                if (padCurrentPostition.Left < (this.Width - rectanglePad.Width))
                {
                    AnimatePad(100);
                }
            }
        }

        private float BallTravelDistance()
        {
            float F = MathF.Pow((float)(ballNextPosition.Left - ballCurrentPosition.Left), 2);
            float S = MathF.Pow((float)(ballNextPosition.Top - ballCurrentPosition.Top), 2);
            return MathF.Sqrt(F + S);
            
        }

     

        private void AnimatePad(int v)
        {
            padAnimation = new ThicknessAnimation(rectanglePad.Margin, new Thickness(rectanglePad.Margin.Left + v, rectanglePad.Margin.Top, rectanglePad.Margin.Right, rectanglePad.Margin.Bottom), new Duration(new TimeSpan(100)));
            rectanglePad.BeginAnimation(Rectangle.MarginProperty, padAnimation, HandoffBehavior.SnapshotAndReplace);

        }
    }
}

