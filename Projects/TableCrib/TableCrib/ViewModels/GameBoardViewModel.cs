using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Popups;
using System.Threading.Tasks;
using TableCrib.Models;
using Windows.UI.Xaml.Shapes;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections.ObjectModel;

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace TableCrib.ViewModels
{
    public class GameBoardViewModel : ViewModelBase
    {
        private Canvas gameBoardCanvas;


        private PointCollection playerOnePoints;
        private PointCollection playerTwoPoints;


        public PointCollection PlayerOnePoints { get { return playerOnePoints; } }
        public PointCollection PlayerTwoPoints { get { return playerTwoPoints; } }
        public Canvas GameBoardCanvas 
        {
            get
            {
                return gameBoardCanvas;
            }
            set 
            {
                Set(() => GameBoardCanvas, ref gameBoardCanvas, value);
            }
        }
        

        public Storyboard PlayerOnePegMovementStoryboard { get; set; }
        public Storyboard PlayerTwoPegMovementStoryboard { get; set; }

        

        public PegAnimationModel Player1PegXAnimation { get; set; }
        public PegAnimationModel Player1PegYAnimation { get; set; }
        public PegAnimationModel Player2PegXAnimation { get; set; }
        public PegAnimationModel Player2PegYAnimation { get; set; }

        private Ellipse player1PegEllipse;

        public Ellipse Player1PegEllipse 
        {
            get
            {
                return player1PegEllipse;
            }
            set 
            {
                Set(() => Player1PegEllipse, ref player1PegEllipse, value);
            }
        }

        private Ellipse player2PegEllipse;
        public Ellipse Player2PegEllipse 
        { 
            get
            {
                return player2PegEllipse;
            }
            set 
            {
                Set(() => Player2PegEllipse, ref player2PegEllipse, value);
            }
        } 


        

        public GameBoardViewModel()
        {
Rect bounds = Window.Current.Bounds;
double height = bounds.Height * .75;
double width = bounds.Width * .33;

            
#if WINDOWS_PHONE_APP
         height = (double)bounds.Height*(.75);
          width = bounds.Width;
#endif

            double x1 = width / 3;

            double x2 = ((width * 2) / 3);
            double x3 = ((width / 2));
            

            double y1 = height / 9;
            double y2 = ((height * 8) / 9);


            GameBoardCanvas = new Canvas();            

            Windows.UI.Xaml.Shapes.Path redpath = GetGeometry(width, x1, x2, x3, y1, y2, new SolidColorBrush(Windows.UI.Colors.Red));
            Windows.UI.Xaml.Shapes.Path bluepath = GetGeometry(width + 20, x1 - 15, x2 + 15, x3 - 15, y1, y2, new SolidColorBrush(Windows.UI.Colors.Blue));


            GameBoardCanvas.Children.Add(redpath);
            GameBoardCanvas.Children.Add(bluepath);

            //get points

            PointCollection redpoints = GetPoints((GeometryGroup)redpath.Data);

            PointCollection bluepoints = GetPoints((GeometryGroup)bluepath.Data);            

            foreach (Point p in redpoints)
            {
                Ellipse redellipse = new Ellipse();
                redellipse.Height = 10;
                redellipse.Width = 10;
                redellipse.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                redellipse.Fill = new SolidColorBrush(Windows.UI.Colors.Red);
           //     redellipse.Name = "Red" + redpoints.IndexOf(p).ToString();
                Canvas.SetTop(redellipse, p.Y );
                Canvas.SetLeft(redellipse, p.X );
             
                GameBoardCanvas.Children.Add(redellipse);
            }

            playerOnePoints = redpoints;

            foreach (Point p in bluepoints)
            {
                Ellipse blueellipse = new Ellipse();
                blueellipse.Height = 10;
                blueellipse.Width = 10;
                blueellipse.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                blueellipse.Fill = new SolidColorBrush(Windows.UI.Colors.Blue);
             //   blueellipse.Name = "Blue" + bluepoints.IndexOf(p).ToString();
                
                Canvas.SetTop(blueellipse, p.Y );
                Canvas.SetLeft(blueellipse, p.X );
            
                GameBoardCanvas.Children.Add(blueellipse);
            }

            playerTwoPoints = bluepoints;

            RowDefinition row = new RowDefinition();
            row.Height = new GridLength(1, GridUnitType.Star);

            

            SetupPegs();

            

            SetupSBStuff();

            

        }

        public void SetupPegs()
        {
            Player1PegEllipse = new Ellipse();
            Player1PegEllipse.Height = 10;
            Player1PegEllipse.Width = 10;
            Player1PegEllipse.Fill = new SolidColorBrush(Windows.UI.Colors.Aquamarine);
            Player1PegEllipse.Stroke = new SolidColorBrush(Windows.UI.Colors.DarkOrange);

            Player2PegEllipse = new Ellipse();
            Player2PegEllipse.Height = 10;
            Player2PegEllipse.Width = 10;
            Player2PegEllipse.Fill = new SolidColorBrush(Windows.UI.Colors.Aquamarine);
            Player2PegEllipse.Stroke = new SolidColorBrush(Windows.UI.Colors.DarkOrange);
            
            SetPegsToZero();
            
            GameBoardCanvas.Children.Add(Player1PegEllipse);
            GameBoardCanvas.Children.Add(Player2PegEllipse);
            

        }

       public void SetupSBStuff()
       {
           

           TimeSpan PegDuration = (TimeSpan.FromSeconds(2));
           

           Player1PegXAnimation = new PegAnimationModel() { Animation = new DoubleAnimation() { To = 0, From = 0, Duration = PegDuration, EasingFunction = new CubicEase() {EasingMode = EasingMode.EaseInOut } } };
           Player1PegYAnimation = new PegAnimationModel() { Animation = new DoubleAnimation() { To = 0, From = 0, Duration = PegDuration, EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseInOut } } };
           

           Player2PegXAnimation = new PegAnimationModel(){ Animation = new DoubleAnimation(){ To = 0, From = 0, Duration = PegDuration, EasingFunction = new CubicEase() {EasingMode = EasingMode.EaseInOut }}};
           Player2PegYAnimation = new PegAnimationModel() { Animation = new DoubleAnimation() { To = 0, From = 0, Duration = PegDuration, EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseInOut } } };
                  

           PlayerOnePegMovementStoryboard = new Storyboard();

           PlayerTwoPegMovementStoryboard = new Storyboard();

           Storyboard.SetTarget(Player1PegXAnimation.Animation, Player1PegEllipse);
           Storyboard.SetTargetProperty(Player1PegXAnimation.Animation, "(Canvas.Left)");

           Storyboard.SetTarget(Player1PegYAnimation.Animation, Player1PegEllipse);
           Storyboard.SetTargetProperty(Player1PegYAnimation.Animation, "(Canvas.Top)");

           Storyboard.SetTarget(Player2PegXAnimation.Animation, Player2PegEllipse);
           Storyboard.SetTargetProperty(Player2PegXAnimation.Animation, "(Canvas.Left)");

           Storyboard.SetTarget(Player2PegYAnimation.Animation, Player2PegEllipse);
           Storyboard.SetTargetProperty(Player2PegYAnimation.Animation, "(Canvas.Top)");

           PlayerOnePegMovementStoryboard.Children.Add(Player1PegXAnimation.Animation);
           PlayerOnePegMovementStoryboard.Children.Add(Player1PegYAnimation.Animation);

           PlayerTwoPegMovementStoryboard.Children.Add(Player2PegXAnimation.Animation);
           PlayerTwoPegMovementStoryboard.Children.Add(Player2PegYAnimation.Animation);

           

           






        }

       
        public void SetPegsToZero()
        {
            Canvas.SetLeft(Player1PegEllipse, PlayerOnePoints[0].X);
            Canvas.SetTop(Player1PegEllipse, PlayerOnePoints[0].Y);

            Canvas.SetLeft(Player2PegEllipse, PlayerTwoPoints[0].X);
            Canvas.SetTop(Player2PegEllipse, PlayerTwoPoints[0].Y);
        }


        private static PointCollection GetPoints(GeometryGroup geo)
        {
            PointCollection points = new PointCollection();
            foreach (Geometry geom in geo.Children)
            {

                if (geom.GetType() == typeof(LineGeometry))
                {
                    LineGeometry line = geom as LineGeometry;
                    Point start = line.StartPoint;
                    Point end = line.EndPoint;

                    double length = Math.Abs(end.Y - start.Y);

                    double fraction = length / 30;

                    for (int x = 0; x < 30; x++)
                    {
                        if (geom == geo.Children[0])
                        {
                            points.Add(new Point(start.X - 5,  start.Y - (x) * fraction));
                        }
                        else if( geom == geo.Children[4])
                        {
                            points.Add(new Point(start.X - 5, -5 + start.Y - (x) * fraction));
                        }
                        else //number 2 here
                        {
                        points.Add(new Point(start.X - 5, -10 + start.Y + (x) * fraction));
                        }

                    }

                }

                if (geom.GetType() == typeof(PathGeometry))
                {
                    PathGeometry pathgeo = geom as PathGeometry;

                    PathFigure figure = pathgeo.Figures[0];

                    ArcSegment path = (ArcSegment)figure.Segments[0];

                    Point start = figure.StartPoint;
                    Point end = path.Point;


                    double x6 = Math.Abs(end.X + start.X) / 2;
                    double radius = Math.Abs(x6 - start.X);
                    for (int c = 16; c > 1; c--)
                    {

                        if (geom == geo.Children[1])
                        {
                            points.Add(new Point(x6 + (-5) + radius * Math.Cos((11 * c * Math.PI) / 180), 5 + start.Y - radius * Math.Sin((11 * c * Math.PI) / 180)));
                        }
                        else // number 3 here
                        {
                            points.Add(new Point(x6 + (-5) - radius * Math.Cos((11 * (c) * Math.PI) / 180), -10 + start.Y + radius * Math.Sin((11 * (c) * Math.PI) / 180)));

                        }

                    }
                }

            }
            
            return points;
        }


        public static Windows.UI.Xaml.Shapes.Path GetGeometry(double width, double widthPoint1, double widthPoint2, double widthPoint3, double heightPoint1, double heightPoint2, SolidColorBrush color)
        {
            Windows.UI.Xaml.Shapes.Path myPath = new Windows.UI.Xaml.Shapes.Path();
            myPath.Stroke = color;
            myPath.StrokeThickness = 15;
           

            LineGeometry myLineGeometry = new LineGeometry();
            myLineGeometry.StartPoint = new Point(widthPoint1, heightPoint2);
            myLineGeometry.EndPoint = new Point(widthPoint1, heightPoint1);


            ArcSegment arc2 = new ArcSegment();
            arc2.Point = new Point(widthPoint2, heightPoint1);
            arc2.RotationAngle = 180;
            arc2.SweepDirection = SweepDirection.Clockwise;
            arc2.Size = new Size((double)(width / 6), heightPoint1);



            LineGeometry line3 = new LineGeometry();
            line3.StartPoint = new Point(widthPoint2, heightPoint1);
            line3.EndPoint = new Point(widthPoint2, heightPoint2);



            ArcSegment arc4 = new ArcSegment();
            arc4.Point = new Point(widthPoint3, heightPoint2);
            arc4.RotationAngle = 180;
            

            arc4.SweepDirection = SweepDirection.Clockwise;
            arc4.Size = new Size(width / 12, heightPoint1 / 2);

            LineGeometry line5 = new LineGeometry();
            line5.StartPoint = new Point(widthPoint3, heightPoint2);
            line5.EndPoint = new Point(widthPoint3, heightPoint1);

            

            PathFigure pathfigureforarc2 = new PathFigure();

            pathfigureforarc2.StartPoint = new Point(widthPoint1, heightPoint1);
            pathfigureforarc2.Segments.Add(arc2);
            pathfigureforarc2.IsFilled = false;

            PathFigure pathfigureforarc4 = new PathFigure();
            pathfigureforarc4.StartPoint = new Point(widthPoint2, heightPoint2);
            pathfigureforarc4.Segments.Add(arc4);
            pathfigureforarc4.IsFilled = false;

            PathGeometry pathgeoforarc2 = new PathGeometry();

            pathgeoforarc2.Figures.Add(pathfigureforarc2);


            PathGeometry pathgeoforarc4 = new PathGeometry();
            pathgeoforarc4.Figures.Add(pathfigureforarc4);


            GeometryGroup myGeometryGroup = new GeometryGroup();
            myGeometryGroup.Children.Add(myLineGeometry);
            myGeometryGroup.Children.Add(pathgeoforarc2);
            myGeometryGroup.Children.Add(line3);
            myGeometryGroup.Children.Add(pathgeoforarc4);
            myGeometryGroup.Children.Add(line5);            

            myPath.Data = myGeometryGroup;
            return myPath;
        }



        

        
    
    }
}
