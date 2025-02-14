using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfEllipsen
{
    public partial class MainWindow : Window
    {
        private const int MaxEllipses = 25;
        private const int MinSize = 20;
        private const int MaxSize = 100;
        private DispatcherTimer timer;
        private int ellipsCount = 0;
        private Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();

            // DispatcherTimer instellen
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(50);
            timer.Tick += Timer_Tick;
        }

        private void btnTekenen_Click(object sender, RoutedEventArgs e)
        {
            // Start het tekenen van ellipsen
            ellipsCount = 0; // reset het aantal ellipsen
            canvas1.Children.Clear(); // leeg canvas voor nieuwe set ellipsen
            timer.Start(); // start timer
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (ellipsCount >= MaxEllipses)
            {
                timer.Stop(); // stop timer als er 25 ellipsen zijn getekend
                return;
            }

            // Willekeurige breedte, hoogte, positie en kleur instellen
            double width = random.Next(MinSize, MaxSize);
            double height = random.Next(MinSize, MaxSize);
            double xPos = random.Next(0, (int)(canvas1.ActualWidth - width));
            double yPos = random.Next(0, (int)(canvas1.ActualHeight - height));
            Color randomColor = Color.FromRgb((byte)random.Next(0, 256), (byte)random.Next(0, 256), (byte)random.Next(0, 256));

            // Maak een nieuwe ellips
            Ellipse newEllipse = new Ellipse()
            {
                Width = width,
                Height = height,
                Fill = new SolidColorBrush(randomColor)
            };

            // Plaats de ellips op de juiste positie
            newEllipse.SetValue(Canvas.LeftProperty, xPos);
            newEllipse.SetValue(Canvas.TopProperty, yPos);

            // Voeg de ellips toe aan het canvas
            canvas1.Children.Add(newEllipse);

            ellipsCount++; // verhoog het aantal getekende ellipsen
        }
    }
}
