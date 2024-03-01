namespace INFR2100U.Continent;

using INFR2100U.Graph;
using INFR2100U.Country;
using INFR2100U.Player;

public class Continent
{
    public string continentName { get; private set; }
    public int controlValue { get; private set; }
    public Graph<Country> countries = new Graph<Country>();
    public bool CheckIfSoleOwned(PlayerColour player)
    {
        foreach (Country country in countries)
        {
            if (country.owner != player) return false;
        }
        return true;
    }
    public PlayerColour GetFirstOwner() { return countries.FirstNode.owner; }

    public Continent(string name, Graph<Country> subGraph)
    {
        continentName = name;
        countries = subGraph;

        switch (name)
        {
            case "N. America":
                {
                    controlValue = 5;
                    break;
                }
            case "S. America":
                {
                    controlValue = 2;
                    break;
                }
            case "Africa":
                {
                    controlValue = 3;
                    break;
                }
            case "Asia":
                {
                    controlValue = 7;
                    break;
                }
            case "Europe":
                {
                    controlValue = 5;
                    break;
                }
            case "Australia":
                {
                    controlValue = 2;
                    break;
                }
        }

    }
}

