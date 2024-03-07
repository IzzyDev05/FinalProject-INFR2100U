namespace INFR2100U.Card;

using INFR2100U.Country;

public class Card
{
    public Country country;
    public Symbol symbol;

    /// <summary>
    /// Default Constructor used to create random cards
    /// </summary>
    public Card()
    {
        Random random = new Random();

        switch (random.Next(0, 3))
        {
            case 0:
                {
                    symbol = Symbol.Cavalry;
                    break;
                }
            case 1: 
                {
                    symbol = Symbol.Artillery;
                    break;
                }
            case 2: 
                {
                    symbol = Symbol.Infantry;
                    break;
                }
        }

        List<Country> countries = new List<Country>();

        country = countries[random.Next(0, countries.Count-1)];
    }
}

public enum Symbol
{
    Cavalry = 0,
    Artillery = 1,
    Infantry = 2
}
