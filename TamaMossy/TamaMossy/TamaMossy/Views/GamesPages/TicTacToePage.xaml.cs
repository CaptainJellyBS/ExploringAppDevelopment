using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TamaMossy.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TamaMossy.Views.GamesPages
{ 
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TicTacToePage : ContentPage
    {
        int[,] grid;
        int currentPlayer;
        Random r = new Random();

        string mossImage;
        public string MossImage { get { return mossImage; } set { if (mossImage != value) { mossImage = value; OnPropertyChanged("MossImage"); } } }

        public TicTacToePage()
        {
            BindingContext = this;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            MossImage = SpriteCalculator.CalculateAnimationPath();
            InitializeNewGrid();
        }

        void InitializeNewGrid()
        {
            grid = new int[3, 3];
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    grid[x, y] = 0;
                }
            }

            //Determine which player starts
            if(r.Next(0,2) == 0)
            {
                currentPlayer = 1;
            }
            else
            {
                currentPlayer = 2;
                OpponentTurn();
            }
        }

        void OnTilePressed(object sender, EventArgs e)
        {
            if(currentPlayer != 1) { return; } //If it's not our turn, do nothing

            bool successfulPlay = false;
            if(sender == LT) { successfulPlay = PlayTile(0, 0, 1); }
            if(sender == CT) { successfulPlay = PlayTile(1, 0, 1); }
            if(sender == RT) { successfulPlay = PlayTile(2, 0, 1); }

            if (sender == LC) { successfulPlay = PlayTile(0, 1, 1); }
            if (sender == CC) { successfulPlay = PlayTile(1, 1, 1); }
            if (sender == RC) { successfulPlay = PlayTile(2, 1, 1); }

            if (sender == LB) { successfulPlay = PlayTile(0, 2, 1); }
            if (sender == CB) { successfulPlay = PlayTile(1, 2, 1); }
            if (sender == RB) { successfulPlay = PlayTile(2, 2, 1); }

            if(successfulPlay)
            {
                currentPlayer = 2;
                int winner = CheckWin();
                if(winner >= 0) { EndGame(winner); }
                else { OpponentTurn(); }
            }

        }

        //The Creature is a professional tic-tac-toe player, and just picks a random empty square. 
        void OpponentTurn()
        {
            if(currentPlayer != 2) { throw new ArgumentException("Ran the Opponent Turn function when it was not the opponent's turn..."); }

            int i = r.Next(0, 9);

            //Try to play a tile based on i. If it doesn't work, increase i by 1 and modulo by 9. 
            //Since we always check for a winner before we call this, the loop eventually terminates
            while (!PlayTile(i%3,i/3,2)) 
            {
                i++;
                i %= 9;
            }

            currentPlayer = 1;
            int winner = CheckWin();
            if (winner >= 0) { EndGame(winner); }
        }

        bool PlayTile(int x, int y, int player)
        {
            if(grid[x,y] != 0) { return false; } //Don't play anything if a tile is already filled

            grid[x, y] = player;
            ButtonFromPos(x, y).Text = TileSymbol(player);
            return true;
        }

        //Returns 1 when player wins. Returns 2 when creature wins. Returns 0 when Draw. Returns -1 when game isn't over. Go magic numbers!
        int CheckWin()
        {
            bool encounteredZero = false; //Bool that keeps track on whether we encountered a zero. If we don't and there's no winner, it's a draw.

            //Check horizontals
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                int check = grid[x, 0];
                int winner = check;

                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    encounteredZero = grid[x,y] == 0 || encounteredZero;
                    if(grid[x,y] != check) { winner = 0; }
                }

                if (winner > 0) { return winner; }
            }

            //Check verticals
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                int check = grid[0, y];
                int winner = check;
                
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    encounteredZero = grid[x, y] == 0 || encounteredZero;
                    if (grid[x, y] != check) { winner = 0; }
                }

                if (winner > 0) { return winner; }
            }

            //Check diagonal. Will crash if, FOR SOME REASON, the tic-tac-toe grid is not square.
            {
                int check = grid[0, 0];
                int winner = check;

                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    encounteredZero = grid[i, i] == 0 || encounteredZero;
                    if (grid[i, i] != check) { winner = 0; }
                }

                if (winner > 0) { return winner; }
            }
           
            //Check diagonal. Will crash if, FOR SOME REASON, the tic-tac-toe grid is not square.
            {
                int l = grid.GetLength(0) - 1;
                
                int check = grid[0, l];
                int winner = check;

                for (int x = 0, y = l; x < grid.GetLength(0); x++, y--)
                {
                    encounteredZero = check == 0 || encounteredZero;
                    if (grid[x, y] != check) { winner = 0; }

                }

                if (winner > 0) { return winner; }
            }

            //Nobody has won, determine if draw or if game isn't over yet
            if (encounteredZero) { return -1; }
            return 0;
        }

        void EndGame(int winner)
        {
            Console.WriteLine("Game Ended!");
            Console.WriteLine("Player " + winner + " won!");
            currentPlayer = -1; //Make sure neither player can play, just in case
           
            //Reset the timer to getting bored, since we just played a game with the creature
            AlarmManager am = AlarmManager.LoadAlarms();
            am.ResetBoredTimer();
            App.CurState.CurrentBoredState = BoredState.Satisfied;
        }

        string TileSymbol(int i)
        {
            switch(i)
            {
                case 0: return " ";
                case 1: return "X";
                case 2: return "O";
                default: throw new ArgumentException("Tic Tac Toe has only two players");
            }
        }

        Button ButtonFromPos(int x, int y)
        {
            switch(y)
            {
                case 0: switch(x)
                    {
                        case 0: return LT;
                        case 1: return CT;
                        case 2: return RT;
                        default: throw new ArgumentException("Wtf happened to the tic-tac-toe grid");
                    }
                case 1:
                    switch (x)
                    {
                        case 0: return LC;
                        case 1: return CC;
                        case 2: return RC;
                        default: throw new ArgumentException("Wtf happened to the tic-tac-toe grid");
                    }
                case 2:
                    switch (x)
                    {
                        case 0: return LB;
                        case 1: return CB;
                        case 2: return RB;
                        default: throw new ArgumentException("Wtf happened to the tic-tac-toe grid");
                    }
                default: throw new ArgumentException("Wtf happened to the tic-tac-toe grid");
            }
        }
    }
}