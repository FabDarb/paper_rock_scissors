using paper_rock_scissors.Class.Db;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Threading;

namespace paper_rock_scissors.Class.Game
{
    public class Game
    {
        public DispatcherTimer Timer { get; set; } = new DispatcherTimer();
        public int Count { get; set; } = 3;
        public bool CanTakePicture { get; set; } = false;
        public int NumberOfImageTake { get; set; } = 0;
        public string PlayerChoose { get; set; } = string.Empty;
        public bool FirstSet { get; set; } = true;
        private MainWindow Main { get; set; }
        public string IAChoose { get; set; } = string.Empty;
        public int Round { get; set; } = 0;
        public string? UserName { get; set; }
        public int Score { get; set; } = 0;
        public int TestMode { get; set; } = 3;
        public Game(MainWindow main)
        {
            Main = main;
            Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            
            Main.TimerCounter.Refresh(Count.ToString());
            if (Count == 1)
            {
                Timer.Stop();
                Choose();
                CanTakePicture = true;
                if(TestMode <= 0)
                {
                    ++Round;
                }
                

            }
            if (Round == 20)
            {
                Timer.Stop();
                Decision.ResetSign();
                UserRepository.StoreUser(UserName!, Score + 1);
            }
            --Count;
        }
        public void Reset()
        {
            Count = 3;
            CanTakePicture = false;
            NumberOfImageTake = 0;
            PlayerChoose = "";
        }

        public void Choose()
        {
            IAChoose = Decision.ChooseSign(PlayerChoose);
        }

        public string Controle()
        {
            string response = string.Empty;
            Decision.AddResultToSigns(PlayerChoose);
            if (IAChoose == PlayerChoose)
            {
                Main.resultStr.Foreground = System.Windows.Media.Brushes.Orange;
                Main.startBtn.Background = System.Windows.Media.Brushes.Orange;
                return "draw";
                
            }
            response = ControlePlayerWin(PlayerChoose, IAChoose, "player");
            if (response == "")
            {
                response = ControlePlayerWin(IAChoose, PlayerChoose, "IA");
                Main.resultStr.Foreground = System.Windows.Media.Brushes.Red;
                Main.startBtn.Background = System.Windows.Media.Brushes.Red;
            }
            else
            {
                ++Score;
                Main.resultStr.Foreground = System.Windows.Media.Brushes.Green;
                Main.startBtn.Background = System.Windows.Media.Brushes.Green;
            }

            return response;
        }

        private string ControlePlayerWin(string firstString, string secondString, string player)
        {
            if(firstString == "paper" && secondString == "rock" || firstString == "rock" && secondString == "scissors" || firstString == "scissors" && secondString == "paper")
            {
                return player + " : win";
            }
            return "";
        }
    }
}
