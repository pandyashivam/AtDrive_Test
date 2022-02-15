using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using System.IO;

namespace AtDrive_Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent(); 
        }

        private string Mainfilename;
        private void MainBrowseButton_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            // Launch OpenFileDialog by calling ShowDialog method
            openFileDlg.DefaultExt = ".png";
            openFileDlg.Filter = "Image documents (.png)|*.png";
            bool? result = openFileDlg.ShowDialog();
            // Get the selected file name and display in a TextBox.
            // Load content of file in a TextBlock
            if (result == true)
            {
                Mainfilename = openFileDlg.FileName;
                MainFileNameTextBox.Text = Mainfilename.Split('\\').Last();
            }
        }

        private string subfilename;
        public void SubBrowseButton_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            // Launch OpenFileDialog by calling ShowDialog method
            openFileDlg.DefaultExt = ".png";
            openFileDlg.Filter = "Image documents (.png)|*.png";
            bool? result = openFileDlg.ShowDialog();
            // Get the selected file name and display in a TextBox.
            // Load content of file in a TextBlock with only filename
            if (result == true)
            {
                subfilename = openFileDlg.FileName;
                SubFileNameTextBox.Text = subfilename.Split('\\').Last(); 
            }
        }

        public void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            if(subfilename != null && Mainfilename != null)
            {
                //create a imagebrush to assign image 
                ImageBrush Mainbrush = new ImageBrush();
                ImageBrush Innerbrush = new ImageBrush();
                Mainbrush.ImageSource = new BitmapImage(new Uri(@Mainfilename, UriKind.Relative));
               //assign image to canvas
                MainCanvas.Background = Mainbrush;
                //assign inner image to rectangle
                Innerbrush.ImageSource = new BitmapImage(new Uri(@subfilename, UriKind.Relative));
                ballRectangle.Fill = Innerbrush;
            }
            else
            {
                MessageBox.Show("Both file should be added");
            }
            // ballRectangle.TranslatePoint(new Point(50.00,50.00),new );
            //var Trs = new TranslateTransform();
            //var Anim = new DoubleAnimation(0,-200,TimeSpan.FromSeconds(2));
            //Trs.BeginAnimation(TranslateTransform.XProperty, Anim);
            SpeedYTextBox.TextChanged += StartButton_Click;
        }

        
        private string Speed;
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            int XValues;
            int YValues;
           
           //take a distance from textbox and ball to move on x axis 
           if(SpeedXTextBox.Text.Length != 0 )
            {
                if (Int32.Parse(SpeedXTextBox.Text) <= 535)
                {
                    XValues = Int32.Parse(SpeedXTextBox.Text);
                    TranslateTransform translateTransform1 = new TranslateTransform(XValues, 0);
                    ballRectangle.RenderTransform = translateTransform1;
                }

                else
                {
                    MessageBox.Show("You can not add value more than 535");
                }
            }
            //take a Hight from textbox and ball to move on y axis 
            if (SpeedYTextBox.Text.Length != 0)
            {
                if (Int32.Parse(SpeedYTextBox.Text) <= 290)
                {
                    YValues = Int32.Parse(SpeedYTextBox.Text);
                    //DoubleAnimation anim = new DoubleAnimation();
                    TopDown.From = 290 - YValues;
                    DownTop.To = 290 - YValues;
                }
                else
                {
                    MessageBox.Show("You can not add value more than 290");
                }
            }
            if (SpeedComboBox.Text.Length != 0)
            {  
                //manage speed of movement with dropdown
                switch (Speed)
                    {
                        case "Slow":
                            Duration dur1 = new Duration(TimeSpan.FromSeconds(5));
                            TopDown.Duration = dur1;
                            DownTop.Duration = dur1;
                            DownTop.BeginTime = new TimeSpan(0,0,5);
                            break;

                        case "Medium":
                            Duration dur2 = new Duration(TimeSpan.FromSeconds(1));
                            TopDown.Duration = dur2;
                            DownTop.Duration = dur2;
                            DownTop.BeginTime = new TimeSpan(0, 0, 1);
                            break;

                    case "Fast":
                        Duration dur3 = new Duration(new TimeSpan(0,0,0,0,200));
                        TopDown.Duration = dur3;
                        DownTop.Duration = dur3;
                        DownTop.BeginTime = new TimeSpan(0, 0, 0,0,200);
                        break;


                }

             }
                

            }

        private void SpeedComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //assign dropdown value for speed control
            ComboBoxItem cbi = (ComboBoxItem)SpeedComboBox.SelectedItem;
            Speed = cbi.Content.ToString();
           
        }

     
        //button to save gif
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //move.FillBehavior = stop;
           // move.Begin(MainCanvas, true);
            //storyboard.Pause();
            //var clock = storyboard.CreateClock();
            //clock.Controller.Pause();
            var secs = Enumerable.Range(0, 100).Select(t => (((double)t) /10));

            //MainCanvas.Measure(new Size(590, 360));


            //MainCanvas.Arrange(new Rect(MainCanvas.DesiredSize));
            
            foreach (var sec in secs)
            {
                //clock.Controller.SeekAlignedToLastTick(TimeSpan.FromSeconds(sec), TimeSeekOrigin.BeginTime);
                // move.SeekAlignedToLastTick(MainCanvas.AnimPlots.canvs, TimeSpan, System.Windows.Media.Animation.TimeSeekOrigin.BeginTime);
                
               // move.SeekAlignedToLastTick(TimeSpan.FromMilliseconds(sec), TimeSeekOrigin.BeginTime);
                //MainCanvas.InvalidateVisual();
                //MainCanvas.UpdateLayout();
                
                //create a Image of each frame
                var filename = System.IO.Path.Combine("C:\\Users\\91777\\OneDrive\\Desktop\\atDrive\\Output", string.Format("image{0}.png", sec));
                var rtb = new RenderTargetBitmap((int)MainCanvas.ActualWidth, (int)MainCanvas.ActualHeight, 96, 96, PixelFormats.Pbgra32);
                rtb.Render(MainCanvas);

                var png = new PngBitmapEncoder();
                png.Frames.Add(BitmapFrame.Create(rtb));

                //save each png to create a gif
                using (var stream = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite))
                {
                    png.Save(stream);
                }
            }
        }
    }

   
   

       
    }

