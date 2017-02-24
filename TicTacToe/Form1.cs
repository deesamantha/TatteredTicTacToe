using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        //Create a new eventhandler for establishing the winner
        public class GameWon : EventArgs
        {
            public Player Winner { get; set; }
        }

        //Create enumerable for holding the player choices
        public enum Player
        {
            E,
            X,
            O
        }

        //Start off with X as current player
        Player currentPlayer = Player.X;

        //Create 3x3 game board to hold players
        Player[,] gameBoard = new Player[3, 3];

        //Hold mount of times x and o have won
        int xWins = 0;
        int oWins = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            //Event Handlers for picturebox click
            pictureBox0.Click += User_Click;
            pictureBox1.Click += User_Click;
            pictureBox2.Click += User_Click;
            pictureBox3.Click += User_Click;
            pictureBox4.Click += User_Click;
            pictureBox5.Click += User_Click;
            pictureBox6.Click += User_Click;
            pictureBox7.Click += User_Click;
            pictureBox8.Click += User_Click;

        }

        private void User_Click(object sender, EventArgs e)
        {
            //picturebox that clicked is sender
            var clicked = (PictureBox)sender;
            //Name of picturebox clicked
            var name = clicked.Name;
            //Get the number from the end of the picturebox name (0-9)
            int count = Convert.ToInt32(name.Substring(name.Length - 1));
            //Get modulus
            int remainder = count % 3;
            //Get quotiend
            int quotient = count / 3;
            //Set clicked gameboard piece to current player
            gameBoard[remainder, quotient] = currentPlayer;
            //Set clicked image to resource image
            clicked.Image = (currentPlayer == Player.X) ? Properties.Resources.x : Properties.Resources.o;
            //Check for winner of game
            CheckForWinner();
            //change player turn
            currentPlayer = (currentPlayer == Player.X) ? Player.O : Player.X;
            //disable click on already clicked picturebox
            clicked.Enabled = false;
            //redraw
            Invalidate();
        }

        public void CheckForWinner()
        {
            //create empty player
            Player winner = new Player();
           
            //for 0-3 as y
            for (int y = 0; y < gameBoard.GetLength(1); y++)
            {
                //if three vertical are equal and not player E
                if (!(gameBoard[0, y].Equals(gameBoard[1, y].Equals(gameBoard[2, y]))).Equals(Player.E))
                {
                    //winner
                    winner = gameBoard[0, y];
                }
            }
            //0-3 as x
            for (int x = 0; x < gameBoard.GetLength(0); x++)
            {
            //if three horizontal are equal and not player E
                if (!(gameBoard[x, 0].Equals(gameBoard[x, 1].Equals(gameBoard[x, 2]))).Equals(Player.E))
                {
                    //set winner
                    winner = gameBoard[x, 0];
                }
            }
            //if horizontal 1 is equal
            if (gameBoard[0, 0].Equals(gameBoard[1, 1]) && gameBoard[1, 1].Equals(gameBoard[2, 2]))
            {
                winner = gameBoard[0, 0];
            }
            //if horizontal 2 is equal
            if (gameBoard[0, 2].Equals(gameBoard[1, 1]) && gameBoard[1, 1].Equals(gameBoard[2, 0]))
            {
                winner = gameBoard[0, 2];
            }

            if (winner == Player.X)
            {
                //add one to player x wins
                xWins++;
            }

            if (winner == Player.O)
            {
                //add one to player 0 wins
                oWins++;
            }
            if (winner != Player.E) {
                GameOver(this, new GameWon { Winner=winner});
            }
            
        }

        public void GameOver(object sender, GameWon p) {
            MessageBox.Show(p.Winner.ToString() + " WINS!");
            label1.Text = "X: " + xWins + " | O: " + oWins;
        }
    }
}
