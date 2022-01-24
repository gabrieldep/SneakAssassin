public class Program
{
    public static void Main()
    {
        string[] A = new string[4] { "X.....", "..v..X", ".>..X..", "A......" };
        string[] B = new string[3] { "...Xv", "AX..^", ".XX.." };
        string[] C = new string[2] { "...", ">.A" };
        string[] D = new string[2] { "A.v", "..." };

        Console.WriteLine(!Solution(A) ? "Correto" : "Errado");
        Console.WriteLine(Solution(B) ? "Correto" : "Errado");
        Console.WriteLine(!Solution(C) ? "Correto" : "Errado");
        Console.WriteLine(!Solution(D) ? "Correto" : "Errado");
    }
    internal static bool Solution(string[] B)
    {
        int x = -1, y = -1;
        FindAssassin(B, ref x, ref y);
        return IsPossible(B, x, y, new List<Tuple<int, int>>());
    }

    internal static bool IsPossible(string[] B, int x, int y, List<Tuple<int, int>> alreadyPass)
    {
        int stringSize = B.First().Length;
        if (x == -1 || y == -1 || x >= B.Length || y >= B.First().Length || 
                alreadyPass.Contains(new Tuple<int, int>(x, y)) || HaveGuardsLooking(B, x, y) || B[x][y] == 'X')
            return false;
        else if (x == B.Length - 1 && y == stringSize - 1)
        {
            return true;
        }
        else
        {
            alreadyPass.Add(new Tuple<int, int>(x, y));
            bool one = y != stringSize - 1 && IsPossible(B, x, y + 1, alreadyPass);
            if (one)
                return true;
            bool two = y != stringSize - 1 && IsPossible(B, x, y - 1, alreadyPass);
            if (two)
                return true;
            bool three = x != B.Length - 1 && IsPossible(B, x + 1, y, alreadyPass);
            if (three)
                return true;
            bool four = x != B.Length - 1 && IsPossible(B, x - 1, y, alreadyPass);
            if (four)
                return true;
            return false;
        }
    }

    internal static bool HaveGuardsLooking(string[] B, int x, int y)
    {
        for (int i = y; i < B.First().Length; i++)
        {
            if (B[x][i] == '<')
                return true;
            else if (B[x][i] != '.' && B[x][i] != 'A')
                break;
        }
        for (int i = y; i >= 0; i--)
        {
            if (B[x][i] == '>')
                return true;
            else if (B[x][i] != '.' && B[x][i] != 'A')
                break;
        }
        for (int i = x; i < B.Length; i++)
        {
            if (B[i][y] == '^')
                return true;
            else if (B[i][y] != '.' && B[x][i] != 'A')
                break;
        }
        for (int i = x; i >= 0; i--)
        {
            if (B[i][y] == 'v')
                return true;
            else if (B[i][y] != '.' && B[x][i] != 'A')
                break;
        }
        return false;
    }

    internal static void FindAssassin(string[] B, ref int x, ref int y)
    {
        for (int i = 0; i < B.Length; i++)
        {
            for (int j = 0; j < B.First().Length; j++)
            {
                if (B[i][j] == 'A')
                {
                    x = i;
                    y = j;
                    return;
                }
            }
        }
    }
}