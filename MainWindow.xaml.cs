using AForge.Video.DirectShow;
using paper_rock_scissors.Class.Db;
using paper_rock_scissors.Class.Game;
using paper_rock_scissors.Class.Python;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace paper_rock_scissors
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
   

    public partial class MainWindow : Window
    {
        int imageCounter = 0;
        string path = $"C:\\Users\\{Environment.UserName}\\.cache\\ia";
        Game game;
        
        public MainWindow()
        {
            InitializeComponent();
            game = new Game(this);
            InitDirectory();
            new LeaderBoard().Show();
            resultStr.Content = PythonProcess.Instance.StreamReader.ReadLine();
        }

        private void InitDirectory()
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private void Vcd_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            Dispatcher.Invoke(() =>
            {
                
                ImageSource? fram = ToBitMapImage((System.Drawing.Image)eventArgs.Frame.Clone());
                image.Source = fram;
                ++imageCounter;
                if (imageCounter >= 5 && game.CanTakePicture && game.NumberOfImageTake < 5)
                {
                    SaveImage(fram!);
                    ++game.NumberOfImageTake;
                    try
                    {
                        PythonProcess.Instance.StreamWriter.WriteLine(path + "\\image.png");
                        game.PlayerChoose = PythonProcess.Instance.StreamReader.ReadLine()!;

                        imageCounter = 0;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"{ex.Message}");
                    }
                    finally
                    {
                        File.Delete(path + "\\image.png");
                    }
                }
                else if (game.NumberOfImageTake >= 5)
                {
                    Dispatcher.Invoke(() =>
                    {
                        resultStr.Content = game.Controle();
                        SignView.Player = game.PlayerChoose;
                        SignView.IA = game.IAChoose;
                        LblScore.Content = $"Score : {game.Score}";
                    });
                    game.Reset();
                    if(game.Round < 20)
                    {
                        game.Timer.Start();
                    }
                    else
                    {
                        game.Round = 0;
                        game.Count = 3;
                        game.Score = 0;
                    }
                }
            });
        }

        private void SaveImage(ImageSource img)
        {
            using(var fileStream = new FileStream(path + "\\image.png", FileMode.Create))
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(img as BitmapSource));
                encoder.Save(fileStream);
            }
        }

        private BitmapSource? ToBitMapImage(System.Drawing.Image img)
        {
            BitmapSource? fram;
            Bitmap bitmap = (Bitmap)img;
            var hBitmap = bitmap.GetHbitmap();
            try
            {
                fram = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            catch (Exception ex)
            { 
                Debug.WriteLine(ex);
                fram = null;
            }
            return fram;
        }

        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            if (game.FirstSet)
            {
                FilterInfoCollection fic;
                VideoCaptureDevice vcd;

                fic = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                vcd = new VideoCaptureDevice(fic[2].MonikerString);
                vcd.NewFrame += Vcd_NewFrame;
                vcd.Start();
                game.FirstSet = false;
            }
            UserForm dialog = new UserForm();
            if (!dialog.ShowDialog()!.Value)
            {
                game.UserName = dialog.Name;
                game.Timer.Start();
            }
            
        }
    }
}