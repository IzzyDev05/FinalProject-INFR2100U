namespace INFR2100U.Country;

using System.Numerics;
using INFR2100U.Player;

public class Country
{
    public string name { get; private set; }
    public PlayerColour owner;
    public int population;
    private Vector2 position;

    public Country(string newName, Vector2 newPos)
    {
        name = newName;
        owner = PlayerColour.None;
        population = 0;
        position = newPos;
    }

    public Vector2 GetPosition() { return position; }
}