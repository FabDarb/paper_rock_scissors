using paper_rock_scissors.Class.Db;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace paper_rock_scissors
{
    /// <summary>
    /// Interaction logic for LeaderBoard.xaml
    /// </summary>
    public partial class LeaderBoard : Window
    {
        public LeaderBoard()
        {
            InitializeComponent();
            Init();
        }
        private async void Init()
        {
            List<User> users = await UserRepository.GetUsers10BestUsers();
            LeaderBoardGrid.ItemsSource = users.Select((user, index) => new UserGrid(user.Name, user.Score, index + 1));
        }

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            Init();
        }
    }
    class UserGrid
    {
        public UserGrid(string name, int score, int index)
        {
            Name = name;
            Score = score;
            Index = index;
        }
        public int Index { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
    }
}
