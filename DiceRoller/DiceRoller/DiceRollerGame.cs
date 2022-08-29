using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Threading;

namespace DiceRoller

{
    internal class DiceRollerGame
    {
        private int _intWon = 0;
        private int _intLost = 0;
        private int _intGames = 0;
        private Random random = new Random();

        

        public DiceRollerGame()
        {

        }
        
        public int Won { get { return _intWon; } set { _intWon = value; } }
        public int Lose { get { return _intLost; } set { _intLost = value; } }
        public int Game { get { return _intGames; } set { _intGames = value; } }
    }
}
