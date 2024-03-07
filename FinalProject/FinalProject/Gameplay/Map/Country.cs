namespace INFR2100U.Country;

using System.Numerics;
using INFR2100U.Player;

public class Country
{
    public string name { get; private set; }
    public PlayerColour owner;
    public int population;
    public int position;

    public Country(string newName, int newPos)
    {
        name = newName;
        owner = PlayerColour.None;
        population = 0;
        position = newPos;
    }

    public Country(string newName, int newPos, PlayerColour player = PlayerColour.None, int army = 0) 
    {
        name = newName;
        owner = player;
        population = army;
        position = newPos;
    }

    public int Population { get { return population; } set { population = value; } }
}