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
    static void isValid(int[,] board, int pos1, int pos2)
    {
        if ((pos1 > 0 && pos1 < 9) && (pos2 > 0 && pos2 < 9))
        {
            board[8 - pos1, pos2 - 1] = 1;
        }

    }

    static bool validPlaceToMove(int[,] board, int pos1, int pos2)
    {
        if (((pos1 > 0 && pos1 < 9) && (pos2 > 0 && pos2 < 9)) && board[8 - pos1, pos2 - 1] == 0)
        {
            return true;
        }

        return false;
    }

    static void printMatrix(int[,] board)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {

                if (board[i, j] == 0)
                    Console.ForegroundColor = ConsoleColor.Green;
                else if (board[i, j] == 3 || board[i,j] == 9)
                    Console.ForegroundColor = ConsoleColor.Red;
                else if (board[i, j] == 1)
                    Console.ForegroundColor = ConsoleColor.Yellow;


                Console.Write("{0,2} ", board[i, j]);

                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }

    static void boardRun(int[,] board, int posx, int posy, int move = 1)
    {
        printMatrix(board);
        int[,] refboard = { { 2, 3, 4, 4, 4, 4, 3, 2 },
                            { 3, 4, 6, 6, 6, 6, 4, 3 },
                            { 4, 6, 8, 8, 8, 8, 6, 4 },
                            { 4, 6, 8, 8, 8, 8, 6, 4 },
                            { 4, 6, 8, 8, 8, 8, 6, 4 },
                            { 4, 6, 8, 8, 8, 8, 6, 4 },
                            { 3, 4, 6, 6, 6, 6, 4, 3 },
                            { 2, 3, 4, 4, 4, 4, 3, 2 }  };

        Random rand = new Random();
        bool check = true, validplace = true;
        int leastnumber;
        int[] possibleplaces = new int[8];
        while (check)
        {
            Console.WriteLine("Press any key to continue");

            possibleplaces = new int[8];
            leastnumber = 9;
            if (validPlaceToMove(board, posx - 1, posy - 2))
            {
                if (refboard[8 - (posx - 1), (posy - 2) - 1] != 0)
                {
                    leastnumber = refboard[8 - (posx - 1), (posy - 2) - 1];
                    possibleplaces[0] = 1;
                }
            }
            if (validPlaceToMove(board, posx - 2, posy - 1))
            {
                if (leastnumber > refboard[8 - (posx - 2), (posy - 1) - 1]
                               && refboard[8 - (posx - 2), (posy - 1) - 1] != 0)
                {
                    leastnumber = refboard[8 - (posx - 2), (posy - 1) - 1];
                    possibleplaces = new int[8];
                    possibleplaces[1] = 1;
                }
                else if (leastnumber == refboard[8 - (posx - 2), (posy - 1) - 1])
                {
                    possibleplaces[1] = 1;
                }
            }
            if (validPlaceToMove(board, posx - 2, posy + 1))
            {
                if (leastnumber > refboard[8 - (posx - 2), (posy + 1) - 1]
                               && refboard[8 - (posx - 2), (posy + 1) - 1] != 0)
                {
                    leastnumber = refboard[8 - (posx - 2), (posy + 1) - 1];
                    possibleplaces = new int[8];
                    possibleplaces[2] = 1;
                }
                else if (leastnumber == refboard[8 - (posx - 2), (posy + 1) - 1])
                {
                    possibleplaces[2] = 1;
                }
            }
            if (validPlaceToMove(board, posx - 1, posy + 2))
            {
                if (leastnumber > refboard[8 - (posx - 1), (posy + 2) - 1]
                               && refboard[8 - (posx - 1), (posy + 2) - 1] != 0)
                {
                    leastnumber = refboard[8 - (posx - 1), (posy + 2) - 1];
                    possibleplaces = new int[8];
                    possibleplaces[3] = 1;
                }
                else if (leastnumber == refboard[8 - (posx - 1), (posy + 2) - 1])
                {
                    possibleplaces[3] = 1;
                }
            }
            if (validPlaceToMove(board, posx + 1, posy + 2))
            {
                if (leastnumber > refboard[8 - (posx + 1), (posy + 2) - 1]
                               && refboard[8 - (posx + 1), (posy + 2) - 1] != 0)
                {
                    leastnumber = refboard[8 - (posx + 1), (posy + 2) - 1];
                    possibleplaces = new int[8];
                    possibleplaces[4] = 1;
                }
                else if (leastnumber == refboard[8 - (posx + 1), (posy + 2) - 1])
                {
                    possibleplaces[4] = 1;
                }
            }
            if (validPlaceToMove(board, posx + 2, posy + 1))
            {
                if (leastnumber > refboard[8 - (posx + 2), (posy + 1) - 1]
                               && refboard[8 - (posx + 2), (posy + 1) - 1] != 0)
                {
                    leastnumber = refboard[8 - (posx + 2), (posy + 1) - 1];
                    possibleplaces = new int[8];
                    possibleplaces[5] = 1;
                }
                else if (leastnumber == refboard[8 - (posx + 2), (posy + 1) - 1])
                {
                    possibleplaces[5] = 1;
                }
            }
            if (validPlaceToMove(board, posx + 2, posy - 1))
            {
                if (leastnumber > refboard[8 - (posx + 2), (posy - 1) - 1]
                               && refboard[8 - (posx + 2), (posy - 1) - 1] != 0)
                {
                    leastnumber = refboard[8 - (posx + 2), (posy - 1) - 1];
                    possibleplaces = new int[8];
                    possibleplaces[6] = 1;
                }
                else if (leastnumber == refboard[8 - (posx + 2), (posy - 1) - 1])
                {
                    possibleplaces[6] = 1;
                }
            }
            if (validPlaceToMove(board, posx + 1, posy - 2))
            {
                if (leastnumber > refboard[8 - (posx + 1), (posy - 2) - 1]
                               && refboard[8 - (posx + 1), (posy - 2) - 1] != 0)
                {
                    leastnumber = refboard[8 - (posx + 1), (posy - 2) - 1];
                    possibleplaces = new int[8];
                    possibleplaces[7] = 1;
                }
                else if (leastnumber == refboard[8 - (posx + 1), (posy - 2) - 1])
                {
                    possibleplaces[7] = 1;
                }
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
                        refboard[8 - posx, posy - 1] = 0;
                        break;
                    case 1:
                        posx -= 2;
                        posy -= 1;
                        move++;
                        board[8 - posx, posy - 1] = move;
                        refboard[8 - posx, posy - 1] = 0;
                        break;
                    case 2:
                        posx -= 2;
                        posy += 1;
                        move++;
                        board[8 - posx, posy - 1] = move;
                        refboard[8 - posx, posy - 1] = 0;
                        break;
                    case 3:
                        posx -= 1;
                        posy += 2;
                        move++;
                        board[8 - posx, posy - 1] = move;
                        refboard[8 - posx, posy - 1] = 0;
                        break;
                    case 4:
                        posx += 1;
                        posy += 2;
                        move++;
                        board[8 - posx, posy - 1] = move;
                        refboard[8 - posx, posy - 1] = 0;
                        break;
                    case 5:
                        posx += 2;
                        posy += 1;
                        move++;
                        board[8 - posx, posy - 1] = move;
                        refboard[8 - posx, posy - 1] = 0;
                        break;
                    case 6:
                        posx += 2;
                        posy -= 1;
                        move++;
                        board[8 - posx, posy - 1] = move;
                        refboard[8 - posx, posy - 1] = 0;
                        break;
                    case 7:
                        posx += 1;
                        posy -= 2;
                        move++;
                        board[8 - posx, posy - 1] = move;
                        refboard[8 - posx, posy - 1] = 0;
                        break;
                }

                validplace = false;

                Console.ReadKey();
                Console.WriteLine();

                printMatrix(board);
            }

        }
        Console.WriteLine("No valid places left");
        Console.ReadLine();
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

        board = new int[8, 8];
        board[8 - posx, posy - 1] = 1;
        boardRun(board, posx, posy);

    }


    static int queenMoves(int[,] board, int posx, int posy)
    {
        int tempx = posx;
        int tempy = posy;
        int callCount = 0;

        while (posx > 0)
        {
            posx--;
            if (validPlaceToMove(board, posx, tempy))
            {
                board[8 - posx, tempy - 1] = 1;
                callCount++;
            }
        }

        posx = tempx;

        while (posx < 9)
        {
            posx++;
            if (validPlaceToMove(board, posx, tempy))
            {
                board[8 - posx, tempy - 1] = 1;
                callCount++;
            }
        }

        while (posy > 0)
        {
            posy--;
            if (validPlaceToMove(board, tempx, posy))
            {
                board[8 - tempx, posy - 1] = 1;
                callCount++;
            }
        }

        posy = tempy;

        while (posy < 9)
        {
            posy++;
            if (validPlaceToMove(board, tempx, posy))
            {
                board[8 - tempx, posy - 1] = 1;
                callCount++;
            }
        }

        posx = tempx;
        posy = tempy;


        while (posx > 0 && posy > 0)
        {
            posx--;
            posy--;
            if (validPlaceToMove(board, posx, posy))
            {
                board[8 - posx, posy - 1] = 1;
                callCount++;
            }
        }

        posx = tempx;
        posy = tempy;


        while (posx > 0 && posy < 9)
        {
            posx--;
            posy++;
            if (validPlaceToMove(board, posx, posy))
            {
                board[8 - posx, posy - 1] = 1;
                callCount++;
            }
        }

        posx = tempx;
        posy = tempy;


        while (posx < 9 && posy > 0)
        {
            posx++;
            posy--;
            if (validPlaceToMove(board, posx, posy))
            {
                board[8 - posx, posy - 1] = 1;
                callCount++;
            }
        }

        posx = tempx;
        posy = tempy;


        while (posx < 9 && posy < 9)
        {
            posx++;
            posy++;
            if (validPlaceToMove(board, posx, posy))
            {
                board[8 - posx, posy - 1] = 1;
                callCount++;
            }
        }

        return callCount;
    }

    static void queen(int posx, int posy)
    {
        int[,] board = new int[8, 8];

        // queen position marked with 9
        board[8 - posx, posy - 1] = 9;

        queenMoves(board, posx, posy);

        printMatrix(board);

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
                        break;
                    }

                }
            }

            if (validplace)
            {
                Console.Write("\nPress any key to place a new queen");
                Console.ReadKey();
                nextQueen(board);

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
        int[,] tempBoard, visualBoard = (int[,])board.Clone();
        int squaresOccupied, lastsquaresOccupied = 64, k = 0;
        var tuple = new (int x, int y, int count)[8];


        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (board[i, j] == 0)
                {
                    tempBoard = (int[,])board.Clone();
                    tempBoard[7 - i, j] = 9;
                    squaresOccupied = 0;
                    int x = i, y = j;
                    squaresOccupied = queenMoves(tempBoard, ++x, ++y);
                    visualBoard[i, j] = squaresOccupied;

                    if (squaresOccupied < lastsquaresOccupied)
                    {
                        lastsquaresOccupied = squaresOccupied;
                        k = 0;
                        tuple = new (int x, int y, int count)[8];
                        tuple[k] = (i, j, lastsquaresOccupied);

                    }
                    else if (squaresOccupied == lastsquaresOccupied)
                    {
                        ++k;
                        tuple[k] = (i, j, lastsquaresOccupied);

                    }
                }
            }
        }

        Console.WriteLine("\nSquares being occupied by queen if placed in that square\n");
        printMatrix(visualBoard);

        Random rand = new Random();

        while (tuple[k].count <= 0)
        {
            k = rand.Next(0, 8);
        }



        //Final placement of queen after checks
        board[tuple[k].x, tuple[k].y] = 9;

        queenMoves(board, 8 - tuple[k].x, tuple[k].y + 1);

        Console.WriteLine("\n");
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

            }
            catch (Exception)
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