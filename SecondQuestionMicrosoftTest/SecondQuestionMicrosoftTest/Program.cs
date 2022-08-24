public class Program
{
    public static void Main()
    {
        Console.WriteLine(!Solution(new string[4] { "X.....", "..v..X", ".>..X..", "A......" }) ? "Correto" : "Errado");
        Console.WriteLine(Solution(new string[3] { "...Xv", "AX..^", ".XX.." }) ? "Correto" : "Errado");
        Console.WriteLine(!Solution(new string[2] { "...", ">.A" }) ? "Correto" : "Errado");
        Console.WriteLine(!Solution(new string[2] { "A.v", "..." }) ? "Correto" : "Errado");
    }

    internal static bool Solution(string[] B)
    {
        int x = -1, y = -1;
        FindAssassin(B, ref x, ref y);
        var buffer = new List<Tuple<int, int>>();
        return IsPossible(B, x, y, ref buffer);
    }

    internal static bool IsPossible(string[] B, int x, int y, ref List<Tuple<int, int>> alreadyPass)
    {
        if (IsNotAuthorized(ref x, ref y, ref B, ref alreadyPass))
            return false;
        else if (IsBottomRight(ref x, ref y, ref B))
            return true;
        else
        {
            alreadyPass.Add(new Tuple<int, int>(x, y));
            return IsPossible(B, x, y + 1, ref alreadyPass) || IsPossible(B, x, y - 1, ref alreadyPass)
                || IsPossible(B, x + 1, y, ref alreadyPass) || IsPossible(B, x - 1, y, ref alreadyPass);
        }
    }

    internal static bool IsBottomRight(ref int x, ref int y, ref string[] B) => x == B.Length - 1 && y == B.First().Length - 1;

    internal static bool IsNotAuthorized(ref int x, ref int y, ref string[] B, ref List<Tuple<int, int>> alreadyPass) => x == -1 || y == -1 || x >= B.Length
                    || y >= B.First().Length || alreadyPass.Contains(new Tuple<int, int>(x, y)) || HaveGuardsLooking(B, x, y) || B[x][y] == 'X';

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