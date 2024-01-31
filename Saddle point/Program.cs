using System.Diagnostics;

Random rand = new Random();

Console.WriteLine("Enter dimentions of MxN matrix");
bool condition = false;

int M = -1;
do
{
    Console.Write("M: ");
    try
    {
        M = Convert.ToInt32(Console.ReadLine());

    }
    catch (Exception)
    {
        //Intentionally left blank   
    }
}while(M >= 0 && M < int.MaxValue);



int N = -1;
do
{
    Console.Write("N: ");
    try
    {
        N = Convert.ToInt32(Console.ReadLine());

    }
    catch (Exception)
    {
        //Intentionally left blank
    }
} while (N >= 0 && N < int.MaxValue);

Console.Write(M + " " + N);