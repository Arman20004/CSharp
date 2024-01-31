
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO.Pipes;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Xml;

class main
{
    static void isValid(int[,] board ,int pos1, int pos2)
    {
        if((pos1 > 0 && pos1 < 9) && (pos2 > 0 && pos2 < 9))
        {
            board[8 - pos1, pos2 - 1] = 1;
        }

    }

    static bool canJumpHere(int[,] board, int pos1, int pos2)
    {
        if (((pos1 > 0 && pos1 < 9) && (pos2 > 0 && pos2 < 9)) && board[8 - pos1, pos2 - 1] == 0)
        {
            return true;
        }

        return false;
    }

    static void printMatrix(int [,] board) {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {

                if (board[i, j] == 3 || board[i,j] == 9)
                    Console.ForegroundColor = ConsoleColor.Red;
                else if (board[i,j] == 1)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else if (board[i,j] == 0)
                    Console.ForegroundColor = ConsoleColor.Green;


                Console.Write(" " + board[i, j]);

                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }

    static void boardRun(int[,] board ,int posx, int posy, int move)

    {
        printMatrix(board);

        Random rand = new Random();
        bool check = true;
        bool validplace = true;
        while (check)
        {
            Console.WriteLine("Press any key to continue");
            int[] possibleplaces = new int[8];

            if (canJumpHere(board, posx - 1, posy - 2))
            {
                possibleplaces[0] = 1;
            }
            if (canJumpHere(board, posx - 2, posy - 1))
            {
                possibleplaces[1] = 1;
            }
            if (canJumpHere(board, posx - 2, posy + 1))
            {
                possibleplaces[2] = 1;
            }
            if (canJumpHere(board, posx - 1, posy + 2))
            {
                possibleplaces[3] = 1;
            }
            if (canJumpHere(board, posx + 1, posy + 2))
            {
                possibleplaces[4] = 1;
            }
            if (canJumpHere(board, posx + 2, posy + 1))
            {
                possibleplaces[5] = 1;
            }
            if (canJumpHere(board, posx + 2, posy - 1))
            {
                possibleplaces[6] = 1;
            }
            if (canJumpHere(board, posx + 1, posy - 2))
            {
                possibleplaces[7] = 1;
            }

            for (int i = 0; i < 8; i++)
            {
                if (possibleplaces[i] == 1)
                {
                    validplace = true;
                    break;
                }
            }

            //////////////////////////////////////////////////////// exit point
            if (!validplace)
            {
                check = false;
            }
            else
            {
                int x = rand.Next(0, 8);

                while (possibleplaces[x] != 1)
                {
                    x = rand.Next(0, 8);
                }

                switch (x)
                {
                    case 0:
                        posx -= 1;
                        posy -= 2;
                        move++;
                        board[8 - posx, posy - 1] = move;
                        break;
                    case 1:
                        posx -= 2;
                        posy -= 1;
                        move++;
                        board[8 - posx, posy - 1] = move;
                        break;
                    case 2:
                        posx -= 2;
                        posy += 1;
                        move++;
                        board[8 - posx, posy - 1] = move;
                        break;
                    case 3:
                        posx -= 1;
                        posy += 2;
                        move++;
                        board[8 - posx, posy - 1] = move;
                        break;
                    case 4:
                        posx += 1;
                        posy += 2;
                        move++;
                        board[8 - posx, posy - 1] = move;
                        break;
                    case 5:
                        posx += 2;
                        posy += 1;
                        move++;
                        board[8 - posx, posy - 1] = move;
                        break;
                    case 6:
                        posx += 2;
                        posy -= 1;
                        move++;
                        board[8 - posx, posy - 1] = move;
                        break;
                    case 7:
                        posx += 1;
                        posy -= 2;
                        move++;
                        board[8 - posx, posy - 1] = move;
                        break;
                }

                validplace= false;

                Console.ReadKey();
                Console.WriteLine();

                printMatrix(board);
            }
            
        }
        Console.WriteLine("No valid places left");
    }

    static void horse(int posx, int posy)
    {

        int[,] board = new int[8, 8];

        // horse position marked with 3
        board[8 - posx, posy - 1] = 3;

        // valid moves marked with 1
        isValid(board, posx - 1, posy - 2);
        isValid(board, posx - 2, posy - 1);
        isValid(board, posx - 2, posy + 1);
        isValid(board, posx - 1, posy + 2);
        isValid(board, posx + 1, posy + 2);
        isValid(board, posx + 2, posy + 1);
        isValid(board, posx + 2, posy - 1);
        isValid(board, posx + 1, posy - 2);

        printMatrix(board);

        string answ = "";
        Console.Write("Enter y to start playing random horse moves: ");
        answ = Console.ReadLine();

        if (answ.ToLower() == "y")
        {
            board = new int[8, 8];
            board[8 - posx, posy - 1] = 1;
            int moveno = 1;
            boardRun(board, posx, posy, moveno);
            
        }
        else
        {
            Console.WriteLine("Exiting program");
            return;
        }

    }


    static void queenMoves(int[,] board, int posx, int posy)
    {
        int tempx = posx;
        int tempy = posy;

        while (posx > 0)
        {
            posx--;
            isValid(board, posx, tempy);
        }

        posx = tempx;

        while (posx < 9)
        {
            posx++;
            isValid(board, posx, tempy);
        }

        while (posy > 0)
        {
            posy--;
            isValid(board, tempx, posy);
        }

        posy = tempy;

        while (posy < 9)
        {
            posy++;
            isValid(board, tempx, posy);
        }

        posx = tempx;
        posy = tempy;


        while (posx > 0 && posy > 0)
        {
            posx--;
            posy--;
            isValid(board, posx, posy);
        }

        posx = tempx;
        posy = tempy;


        while (posx > 0 && posy < 9)
        {
            posx--;
            posy++;
            isValid(board, posx, posy);
        }

        posx = tempx;
        posy = tempy;


        while (posx < 9 && posy > 0)
        {
            posx++;
            posy--;
            isValid(board, posx, posy);
        }

        posx = tempx;
        posy = tempy;


        while (posx < 9 && posy < 9)
        {
            posx++;
            posy++;
            isValid(board, posx, posy);
        }
    }

    static void queen(int posx, int posy) 
    {
        int[,] board = new int[8, 8];
 
        // queen position marked with 9
        board[8 - posx, posy - 1] = 9;

        queenMoves(board, posx, posy);

        printMatrix(board);

        string answ;
        bool check = true;
        bool validplace = false;

        while (check)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] == 0)
                    {
                        validplace = true;
                    }

                }
            }

            if (validplace)
            {
                Console.Write("Enter y to place a new queen: ");
                answ = Console.ReadLine();

                if (answ.ToLower() == "y")
                    nextQueen(board);
                else
                {
                    Console.WriteLine("Exiting program");
                    check = false;
                }

                validplace = false;
            }
            else
            {
                Console.WriteLine("No valid places left");
                check = false;
            }

            
        }

        
    }

    static void nextQueen(int[,] board)
    {

        Random rand = new Random();

        int x = rand.Next(0, 8);
        int y = rand.Next(0, 8);

        while (board[7 - x,y] != 0)
        {
            x = rand.Next(0, 8);
            y = rand.Next(0, 8);
        }

        board[7 - x,y] = 9;

        queenMoves(board, x + 1, y + 1);
        
        Console.Clear();
        Console.WriteLine("Board with changes ");
        printMatrix(board);

    }
 
    static int getX(string inp)
    {
        int posx = 0;
        do
        {
            Console.Write("Enter X in range from 1 to 8: ");

            try
            {
                posx = Convert.ToInt32(Console.ReadLine());

            }catch (Exception)
            {
                //Intentionally left blank   
            }


        } while (!(posx > 0 && posx < 9));

        return posx;
    }

    static int getY(string inp)
    {
        int posy = 0;
        do
        {
            Console.Write("Enter Y in range from 1 to 8: ");
            try
            {
                posy = Convert.ToInt32(Console.ReadLine());

            }
            catch (Exception)
            {
             //Intentionally left blank   
            }

        } while (!(posy > 0 && posy < 9));

        return posy;
    }

    static void Main()
    {

        Console.WriteLine("Enter q for queen or h for horse");
        string inp = "";
        do
        {
            inp = Console.ReadLine();
            switch (inp.ToLower())
            {
                case "q":
                    Console.WriteLine("Enter position of queen");
                    queen(getX(inp), getY(inp));
                    break;
                case "h":
                    Console.WriteLine("Enter position of horse");
                    horse(getX(inp), getY(inp));
                    break;
                default:
                    Console.WriteLine("Try again\n");
                    break;
            }

        } while ((inp.ToLower() != "q") && (inp.ToLower() != "h"));

    }

}
