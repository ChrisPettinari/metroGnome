using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchTracking;
using Xamarin.Forms;

namespace metroGnome
{
    public partial class MainPage : ContentPage
    {

        private double centerX = 0, centerY = 0;
        private double startingRotation = 0, startingDegrees = 0, lastDegrees = 0;

        private double unitsPerDegree = 0.1;
        private double min = 20, max = 500, current = 120;

        private double temp { get; set; }

        public MainPage()
        {
            InitializeComponent();
        }

        void PanGesture_TouchAction(object sender, TouchActionEventArgs args)
        {
            centerX = gridContainer.Width / 2;
            centerY = gridContainer.Height / 2;

            Point p;


            switch (args.Type)
            {
                case TouchActionType.Pressed:

                    p = new Point(args.Location.X - centerX, args.Location.Y - centerY);

                    startingDegrees = (Math.Atan2(p.Y, p.X) * 180.0 / Math.PI + 180 - 90);
                    lastDegrees = startingDegrees;
                    startingRotation = knob.Rotation;

                    //Console.WriteLine("X: {0} Y: {1}", lastX, lastY);
                    break;

                case TouchActionType.Moved:

                    p = new Point(args.Location.X - centerX, args.Location.Y - centerY);

                    double currentDegrees = (Math.Atan2(p.Y, p.X) * 180.0 / Math.PI + 180 - 90);
                    if (currentDegrees < 0) currentDegrees = 360 - Math.Abs(currentDegrees);

                    knob.Rotation = startingRotation + (currentDegrees - startingDegrees);  //startingRotation + degrees;

                    //double diff =  180 - Math.Abs(Math.Abs(currentDegrees - lastDegrees) - 180);

                    double diff = currentDegrees - lastDegrees;
                    if (diff < -180)
                        diff = currentDegrees - (lastDegrees - 360);
                    else if (diff > 180)
                        diff = lastDegrees - (currentDegrees - 360);

                    temp = diff;

                    // if (lastDegrees > currentDegrees) diff = 360 - currentDegrees - lastDegrees;

                    Console.WriteLine("Current: {0}, currentDegrees: {1}, diff: {2}", (int)current, currentDegrees, diff);


                    diff = diff * unitsPerDegree;

                    lastDegrees = currentDegrees;

                    current += diff;
                    current = Math.Max(Math.Min(current, max), min);

                    bpm.Text = (int)current + "";
                    //

                    break;
                case TouchActionType.Released:
                    Console.WriteLine(temp);

                    break;
            }
        }
    }
}
