namespace INFR2100U.Country;

using System.Numerics;
using INFR2100U.Player;

public class Country
{
    public string countryName { get; private set; }
    public Player owner;
    public int population;
    public int position;

    public Country(string newName, int newPos)
    {
        countryName = newName;
        population = 0;
        position = newPos;
    }

    public Country(string newName, int newPos, Player player, int army = 0) 
    {
        countryName = newName;
        owner = player;
        population = army;
        position = newPos;
    }

    public int Population { get { return population; } set { population = value; } }
}