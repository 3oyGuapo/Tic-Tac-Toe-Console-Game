using System;

namespace Tic_Tac_Toe_Console_Version
{
    class Program
    {
        static void Main(string[] args)
        {
            //Main logic of the game: a while loop to iterate the game process. Few if statment checks the validty of user moves and show the move on the board.

            //Define board size: 3x3
            char[,] board = new char[3,3];
            //Define few variables for storing data
            int currentPlayer = 0;
            int turns = 0;
            int scoreO = 0;
            int scoreX = 0;

            //Initialize board
            InitializeBoard(board);

            //Some insructions
            Console.WriteLine("Welcome to Tic-Tac-Toe gmae.");
            Console.WriteLine("Choose your game mode:");
            Console.WriteLine("Type 1 for player vs player.");
            Console.WriteLine("Type 2 for player vs computer.");
            string modeInput = Console.ReadLine();

            //Check mode of the game by user input
            bool vsComputer = (modeInput == "2");

            //Main logic for the game
            while (true)
            {
                //Show and update the board
                UpdateBoard(board, scoreO, scoreX);
                
                //Condition check for current player and assign symbol of current player
                char playerSymbol = currentPlayer == 0 ? 'O' : 'X';

                //If vsComputer is true which player choose to fight computer and is computer's turn
                if (vsComputer && currentPlayer == 1)
                {
                    //Call the method for computer take move
                    ComputerPlayer(board);
                    //Update current player to player and start new loop
                    currentPlayer = currentPlayer == 0 ? 1 : 0;
                    continue;
                }

                //Ask player to make their move
                else 
                {
                    Console.WriteLine($"Player {playerSymbol} make your move by input between 1-9:");
                }
                
                //Take the player's input and validate it
                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))//Check the player inputs a valid number
                {
                    
                    Console.WriteLine($"Invalid input, Please try again:");
                    Console.Clear();
                    UpdateBoard(board, scoreO, scoreX);
                    continue;
                    //Console.WriteLine($"Player {playerSymbol} make your move by input between 1-9:");
                }

                //Check validty of number is between the board size
                if (choice < 1 || choice > 9)
                {
                    Console.WriteLine("Invalid number! Please enter number between 1 - 9.");
                    continue;
                }

                //Convert to 2d position of the board for comparing with the player symbol
                int row = (choice - 1) / 3;
                int col = (choice - 1) % 3;

                //Check current field available or not
                if (board[row, col] == 'X' || board[row, col] == 'O')
                {
                    Console.WriteLine("Field already taken! Please choose another field.");
                    continue;
                }

                //If pass all the above validty check, then add current player's symbol to the selected field and update turns
                board[row, col] = playerSymbol;
                turns++;
                //Boolean variable deciding game over
                bool gameOver = false;

                //Check board condition for any winning lines
                if (CheckCondition(board))
                {
                    //Update board with and adding scores to the player that wins
                    UpdateBoard(board, scoreO, scoreX);

                    //Update the score for the matching player
                    if (playerSymbol == 'O')
                    {
                        scoreO++;
                    }

                    else 
                    {
                        scoreX++;
                    }
                    
                    Console.WriteLine($"Player {playerSymbol} wins! Congradulation!");
                    //Update bool variable tells game over
                    gameOver = true;
                }

                //Check draw condition
                else if (turns == 9)
                {
                    UpdateBoard(board, scoreO, scoreX);
                    Console.WriteLine("Draw! Game Over!");

                    gameOver = true;
                }

                //Check game over condition
                if (gameOver)
                {
                    //Ask player to continue playing or not
                    if (ContinuePlayOrNot())
                    {
                        //If continue playing, then initialize the board to start again and reset turns
                        InitializeBoard(board);
                        turns = 0;
                        continue;
                    }

                    //Quit the loop and game over
                    else
                    {
                        break;
                    }
                }

                //Game continues loop and swap player
                UpdateBoard(board, scoreO, scoreX);
                currentPlayer = currentPlayer == 0 ? 1 : 0;
            }
        }

        //Method that initialize the board by assigning values to each field
        static void InitializeBoard(char[,] board)
        {
            int counter = 1;
            //Nested loop that iterate every field in the board
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++) 
                {
                    //Assigning converted char value to current field and update counter to avoid same value added
                    board[row, col] = (char)(counter + '0');//'0' adding with int will use its code number in ASCII to add and then convert back to char to get the corrent value
                    counter++;
                }
            }
        }

        //Method that show and update the status of board
        static void UpdateBoard(char[,] board, int scoreO, int scoreX) 
        {
            //Clear at the beginning to avoid multiple board on the screen
            Console.Clear();
            Console.WriteLine("Tic Tac Toe game");
            Console.WriteLine($"Player O's score:{scoreO}      Player X's score:{scoreX}");
            Console.WriteLine("====================");
            Console.WriteLine($"     |     |     ");
            Console.WriteLine($"  {board[0, 0]}  |  {board[0, 1]}  |  {board[0, 2]}  ");
            Console.WriteLine($"_____|_____|_____");
            Console.WriteLine($"     |     |     ");
            Console.WriteLine($"  {board[1, 0]}  |  {board[1, 1]}  |  {board[1, 2]}  ");
            Console.WriteLine($"_____|_____|_____");
            Console.WriteLine($"     |     |     ");
            Console.WriteLine($"  {board[2, 0]}  |  {board[2, 1]}  |  {board[2, 2]}  ");
            Console.WriteLine($"     |     |     ");
            Console.WriteLine("====================");
        }

        //Method that calls the other method to check the winning combo
        static bool CheckCondition(char[,] board)
        {
            return CheckRowColCondition(board) || CheckDiagonalCondition(board);
        }

        //Method checks the winning combo for rows and cols
        static bool CheckRowColCondition(char[,] board)
        {
            //A single loop that checks each rows and cols are lined or not
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                { 
                    return true; 
                }
                else if (board[0, i] == board[1, i] && board[1, i] == board[2, i])
                {
                    return true;
                }
            }
            return false;
        }

        //Method checks the diagonla winning combo
        static bool CheckDiagonalCondition(char[,] board)
        {
            if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            {
                return true;
            }

            else if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
            {
                return true;
            }
            return false;
        }


        //Ask player to continue playing the game or quit
        static bool ContinuePlayOrNot()
        {
            Console.WriteLine("Do you want continue playing? Y/N");
            string continueInput = Console.ReadLine().ToUpper();//Take player input and convert to upper than store in variable

            //If the player input is not empty and eaxctly one y/Y then continue the game
            if (!string.IsNullOrEmpty(continueInput) && continueInput.Trim() == "Y")
            {
                return true;
            }

            return false;
        }

        //Method that control the computer player to make movements
        static void ComputerPlayer(char[,] board)
        {
            //A lsit stores the int of all the current available fields on the board
            List<int> availableFields = new List<int>();

            //Nested loop to check and add to the list
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j ++)
                {
                    if (board[i,j] != 'O' && board[i,j] != 'X')//Check current field available or not
                    {
                        availableFields.Add(board[i, j] - '0'); //Transfer char's ASCII code for each number to int by deleting the ASCII code of char 0
                        //availableFields.Add(i * 3 + j + 1);  This is another way which gives the number that player sees.
                    }
                }
            }

            //Random object that generate random numbers
            Random random = new Random();
            int randIndex = random.Next(0, availableFields.Count());//Get a number from 0 to the number of availableFields
            int move = availableFields[randIndex];//Access the element of randIndex element of availableFields generate from previous line and store as computer's move

            //Convert to 2d field and replace with the symbol of computer player
            int row = (move - 1) / 3;
            int col = (move - 1) % 3;
            board[row, col] = 'X';

            Console.WriteLine($"Computer has choose position {move}.");
            System.Threading.Thread.Sleep(2000);//Stop for 2 seconds to allow time for player to see before end computer's turn
        }

    }
}