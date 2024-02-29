namespace INFR2100U.Continent
{
    using INFR2100U.Country;
    using INFR2100U.Graph;


    public class Continent
	{
		public int pointBonus { get; private set; }
		public Graph<Country> countries = new Graph<Country>();
		public bool CheckIfSoleOwned()
		{
			foreach (Country country in countries)
			{
				if (country.owner != countries.FirstNode.owner) return false;
			}
			return true;
		}
		public PlayerColour GetFirstOwner() { return countries.FirstNode.owner; }
	}
}
