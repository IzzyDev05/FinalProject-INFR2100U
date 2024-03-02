using INFR2100U.Graphics;

class Program
{
    public static void Main(string[] args)
    {
        using Game risk = new Game(1200, 614, "Risk - INFR2100U");
        risk.Run();
    }
}