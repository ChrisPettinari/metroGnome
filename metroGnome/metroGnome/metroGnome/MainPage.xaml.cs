using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchTracking;
using Xamarin.Forms;
using Plugin.SimpleAudioPlayer;
using Syncfusion.XForms.ProgressBar;

namespace metroGnome
{
    public partial class MainPage : ContentPage
    {   //wat
        public bool woo = false;



        private int startTicks = Environment.TickCount;

        private double centerX = 0, centerY = 0;
        private double startingRotation = 0, startingDegrees = 0, lastDegrees = 0;

        private double unitsPerDegree = 0.1;
        private double min = 20, max = 500, current = 120;

        private double temp { get; set; }

        private List<ISimpleAudioPlayer> players;

        public MainPage()
        {
            InitializeComponent();

            players = new List<ISimpleAudioPlayer>();

            /*for (int i = 0; i < 60; i++)
            {
                ISimpleAudioPlayer player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
                player.Load(@"Clave Sound.wav");
                player.Loop = false;

                players.Add(player);

            }*/

           
        }

        void Play()
        {
            Random rdm = new Random();

            for (int i = 0; i < players.Count; i++)
            {
                if (!players[i].IsPlaying)
                {
                    players[i].Play();

                    if (woo)
                    {
                        wooBtn.BackgroundColor = Color.FromRgb(rdm.Next(0, 255), rdm.Next(0, 255), rdm.Next(0, 255));
                        woodblockBtn.BackgroundColor = Color.FromRgb(rdm.Next(0, 255), rdm.Next(0, 255), rdm.Next(0, 255));
                        claveBtn.BackgroundColor = Color.FromRgb(rdm.Next(0, 255), rdm.Next(0, 255), rdm.Next(0, 255));
                        templeblockBtn.BackgroundColor = Color.FromRgb(rdm.Next(0, 255), rdm.Next(0, 255), rdm.Next(0, 255));
                        grid.BackgroundColor = Color.FromRgb(rdm.Next(0, 255), rdm.Next(0, 255), rdm.Next(0, 255));
                        wooBtn.BorderColor =  Color.FromRgb(rdm.Next(0, 255), rdm.Next(0, 255), rdm.Next(0, 255));
                        woodblockBtn.BorderColor = Color.FromRgb(rdm.Next(0, 255), rdm.Next(0, 255), rdm.Next(0, 255));
                        claveBtn.BorderColor = Color.FromRgb(rdm.Next(0, 255), rdm.Next(0, 255), rdm.Next(0, 255));
                        wooBtn.TextColor = Color.FromRgb(rdm.Next(0, 255), rdm.Next(0, 255), rdm.Next(0, 255));
                        templeblockBtn.BorderColor = Color.FromRgb(rdm.Next(0, 255), rdm.Next(0, 255), rdm.Next(0, 255));
                        woodblockBtn.TextColor = Color.FromRgb(rdm.Next(0, 255), rdm.Next(0, 255), rdm.Next(0, 255));
                        claveBtn.TextColor = Color.FromRgb(rdm.Next(0, 255), rdm.Next(0, 255), rdm.Next(0, 255));
                        templeblockBtn.TextColor = Color.FromRgb(rdm.Next(0, 255), rdm.Next(0, 255), rdm.Next(0, 255));
                        metronome.BackgroundColor = Color.FromRgb(rdm.Next(0, 255), rdm.Next(0, 255), rdm.Next(0, 255));
                        metronome.ProgressColor = Color.FromRgb(rdm.Next(0, 255), rdm.Next(0, 255), rdm.Next(0, 255));
                        gridContainer.BackgroundColor = Color.FromRgb(rdm.Next(0, 255), rdm.Next(0, 255), rdm.Next(0, 255));
                        poop.TextColor = Color.FromRgb(rdm.Next(0, 255), rdm.Next(0, 255), rdm.Next(0, 255));
                        bpm.TextColor = Color.FromRgb(rdm.Next(0, 255), rdm.Next(0, 255), rdm.Next(0, 255));

                    }
                    else
                    {
                        wooBtn.BackgroundColor = Color.LightGray;
                        woodblockBtn.BackgroundColor = Color.LightGray;
                        claveBtn.BackgroundColor = Color.LightGray;
                        templeblockBtn.BackgroundColor = Color.LightGray;
                        grid.BackgroundColor = Color.White;
                        wooBtn.BorderColor = Color.LightGray;
                        woodblockBtn.BorderColor = Color.LightGray;
                        claveBtn.BorderColor = Color.LightGray;
                        templeblockBtn.BorderColor = Color.LightGray;
                        wooBtn.TextColor = Color.Black;
                        templeblockBtn.BorderColor = Color.LightGray;
                        woodblockBtn.TextColor = Color.Black;
                        claveBtn.TextColor = Color.Black;
                        templeblockBtn.TextColor = Color.Black;
                        metronome.BackgroundColor = Color.LightGray;
                        metronome.ProgressColor = Color.Blue;
                        gridContainer.BackgroundColor = Color.White;
                        poop.TextColor = Color.Black;
                        bpm.TextColor = Color.Black;
                    }

                    break;

                   
                }
            }
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


                    break;
                case TouchActionType.Released:
                    Console.WriteLine(temp);

                    break;
            }
        }

        //int rate = (1 / (Convert.ToUInt32(bpm.Text) / 60) * 1000);
        protected override async void OnAppearing()
        {


            base.OnAppearing();
            bpm.Text = "120";

            while (true)
            {

                int elapsedTicks = Environment.TickCount - startTicks;

                int beats = (1000 * 60) / Convert.ToInt32(bpm.Text);  // Calculate ms between beats

                //int rate = (int)((1 / (Convert.ToDouble(bpm.Text) / 60)) * 1000);

                double progress = (elapsedTicks % beats) / (double)beats;

                progress *= 1.1;
                progress = Math.Min(1, Math.Max(0, progress));

                if (elapsedTicks / beats % 2 == 1)
                {
                    progress = 1 - progress;

                }

                if (progress == 1 || progress == 0)
                {
                    Random rdm = new Random();
                    Play();
                    
                }
                metronome.Progress = progress;
                await Task.Delay(1);

            }
        }

        private void Pressed(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            if (b.Text == "Temple Block")
            {
                players.Clear();

                for (int i = 0; i < 60; i++)
                {
                    

                    ISimpleAudioPlayer player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
                    player.Load(@"Temple Block Sound.wav");
                    player.Loop = false;

                    players.Add(player);
                    woo = false;
                }
            }
            else if (b.Text == "Wood Block")
            {
                players.Clear();
                for (int i = 0; i < 60; i++)
                {
                    

                    ISimpleAudioPlayer player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
                    player.Load(@"Wood Block Sound.wav");
                    player.Loop = false;

                    players.Add(player);
                    woo = false;
                }
            }
            else if (b.Text == "Clave")
            {
                players.Clear();
                for (int i = 0; i < 60; i++)
                {
                    

                    ISimpleAudioPlayer player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
                    player.Load(@"Clave Sound.wav");
                    player.Loop = false;

                    players.Add(player);
                    woo = false;
                }
            }
            else if (b.Text == "woo")
            {
                players.Clear();

                for (int i = 0; i < 60; i++)
                {
                    ISimpleAudioPlayer player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
                    player.Load(@"woo.wav");
                    player.Loop = false;

                    players.Add(player);

                }
                Random rdm = new Random();
                b.BackgroundColor = Color.FromRgb(rdm.Next(0, 255), rdm.Next(0, 255), rdm.Next(0, 255));
                woo = true;

            }
        }
    }
}