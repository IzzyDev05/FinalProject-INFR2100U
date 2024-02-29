using INFR2100U.Continent;
using INFR2100U.Graph;
using INFR2100U.Country;

public class Map
{
	public Graph<Country> everyCountry = new Graph<Country>();
	public Graph<Continent> everyContinent = new Graph<Continent>();
}

public enum PlayerColour
{
	None = 0,
	Red = 1,
	Green = 2,
	Blue = 3,
	Yellow = 4,
	Cyan = 5,
	Magenta = 6,
	Grey = 7,
}