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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using static DiceRoller.DiceRollerGame;
using static System.Net.Mime.MediaTypeNames;

namespace DiceRoller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DiceRollerGame game = new DiceRollerGame();
        /// <summary>
        /// 
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            string header = String.Format("{0,5} {1,15} {2, 15} {3, 15}", "Face","Frequency","Percent","Guessed");
            string stats1 = String.Format("\n{0,5} {1,17} {2, 21} {3, 17}", "1", "0", "00.00", "0");
            string stats2 = String.Format("\n{0,5} {1,17} {2, 21} {3, 17}", "2", "0", "00.00", "0");
            string stats3 = String.Format("\n{0,5} {1,17} {2, 21} {3, 17}", "3", "0", "00.00", "0");
            string stats4 = String.Format("\n{0,5} {1,17} {2, 21} {3, 17}", "4", "0", "00.00", "0");
            string stats5 = String.Format("\n{0,5} {1,17} {2, 21} {3, 17}", "5", "0", "00.00", "0");
            string stats6 = String.Format("\n{0,5} {1,17} {2, 21} {3, 17}", "6", "0", "00.00", "0");
            string stats = header + stats1 + stats2 + stats3 + stats4 + stats5 + stats6;
            txtStats.Text = stats;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRoll_Click(object sender, RoutedEventArgs e)
        {
            const string strLost = "Number of Times Lost : ";
            const string strWon = "Number of Times Won : ";
            const string strGame = "Number of Times Played : ";
            int lost = int.Parse(lblLost.Text.Split(" ")[^1]);
            int won = int.Parse(lblWon.Text.Split(" ")[^1]);
            int games = int.Parse(lblPlayed.Text.Split(" ")[^1]);

            Random rand = new Random();
            int roll = rand.Next(1,7);
            rollDie(roll);

            int guess = int.Parse(txtInput.Text);

            games++;
            lost += roll == guess ? 0 : 1;
            won += roll == guess ? 1 : 0;

            lblLost.Text = $"{strLost}{lost}";
            lblWon.Text = $"{strWon}{won}";
            lblPlayed.Text = $"{strGame}{games}";

            return;
        }

        private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            string strInput = txtInput.Text;
            try
            {
                int input = Int16.Parse(strInput);
                if(!(input > 0 && input < 7))
                {
                    btnRoll.IsEnabled = false;
                }
                else
                {
                    btnRoll.IsEnabled = true;
                }
            }
            catch (System.FormatException)
            {
                    btnRoll.IsEnabled = false;
            }
            return;
        }
        private void rollDie(int roll)
        {
            
            Random rand = new Random();

            int rolls = rand.Next(1, 10);
            for (int i = 0; i < rolls; i++)
            {
                int imgroll = rand.Next(1, 7);
                BitmapImage tempbit = new BitmapImage();
                tempbit.BeginInit();
                tempbit.CacheOption = BitmapCacheOption.OnLoad;
                tempbit.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                tempbit.UriSource = new Uri(@$"die{imgroll}.gif", UriKind.Relative);
                tempbit.EndInit();
                imgOutput.Source = tempbit;
                imgroll = rand.Next(20, 50);
                Thread.Sleep(imgroll);
            }

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            bitmap.UriSource = new Uri(@$"die{roll}.gif", UriKind.Relative);
            bitmap.EndInit();
            imgOutput.Source = bitmap;
            return;
        }
    }
}
