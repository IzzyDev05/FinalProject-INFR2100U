namespace INFR2100U.Player;

using INFR2100U.Card;
using INFR2100U.Country;
using INFR2100U.Continent;

public class Player
{
    #region PUBLIC_DATA
    public PlayerColour colour;
    public int turnOrder { get; private set; }
    public List<Country> controlledTerritory = new List<Country>();
    public List<Card> cards = new List<Card>();
    public int armyUsed = 0;
    public int reienforcmentOwned = 0;
    #endregion

    #region PRIVATE_DATA
    private int setTraded = 0;
    private bool cardTradeBonus = false;
    private Phase currentPhase;
    #endregion

    /// <summary>
    /// Adds the country to the list of controlled countries.
    /// </summary>
    /// <param name="country">The country the player gained.</param>
    public void GetTerritory(Country country)
    {
        if (!controlledTerritory.Contains(country))
        {
            controlledTerritory.Add(country);
        }
    }

    /// <summary>
    /// Trades a set and gain bonus armies.
    /// </summary>
    public int TradeSet()
    {
        List<Card> set = FindSet();

        // Remove the cards from the list
        cards.Remove(set[0]);
        cards.Remove(set[1]);
        cards.Remove(set[2]);

        setTraded++;

        // Checks if any card has a country on it that the player owns for bonus armies on trade.
        foreach (Country country in controlledTerritory)
        {
            if ((set[0].country == country || set[1].country == country || set[2].country == country) && cardTradeBonus == false)
            {
                cardTradeBonus = true;
            }
        }

        return SetBonus();
    }

    #region GAME_PHASE
    /// <summary>
    /// Gain army to use.
    /// </summary>
    /// <param name="bonus">Bonus army gained.</param>
    public void GainArmies(bool tradeSet)
    {
        int gainedArmy = Convert.ToInt32(MathF.Floor(controlledTerritory.Count / 3f));

        // Get a bonus for trading in a set
        if (tradeSet || cards.Count >= 5)
        {
            gainedArmy += TradeSet();
        }

        // Get bonus for owning a continent
        foreach (Continent continent in Map.everyContinent)
        {
            if (continent.CheckIfSoleOwned(colour))
            {
                gainedArmy += continent.controlValue;
            }
        }

        // Give a minimum of 3 armies
        if (gainedArmy < 3)
        {
            reienforcmentOwned += 3;
        }
        else
        {
            reienforcmentOwned += gainedArmy;
        }
    }

    public void Reinforcement()
    {

    }

    public void Attack()
    {

    }

    public void Fortification()
    {

    }

    /// <summary>
    /// Draw and add card to the list of cards. If there is 5 or more cards, trade a set.
    /// </summary>
    public void DrawCard()
    {
        Card newCard = new Card();

        cards.Add(newCard);

        if (cards.Count >= 5)
        {
            reienforcmentOwned += TradeSet();
        }
    }
    #endregion

    #region PRIVATE_FUNCTIONS
    /// <summary>
    /// Grants bonus armies depending on the number of sets given.
    /// </summary>
    /// <returns>The bonus army gained from giving a set.</returns>
    private int SetBonus()
    {
        int bonusArmy = 0;
        if (cardTradeBonus)
        {
            bonusArmy = 2;
            cardTradeBonus = false;
        }

        return 5 + bonusArmy + (5 * (setTraded - 1)) < 3 ? 3 : 5 + bonusArmy + (5 * (setTraded - 1));
    }

    /// <summary>
    /// Finds a set of matching or different cards.
    /// </summary>
    /// <returns>A list of cards that forms a set, either all the same or all different.</returns>
    private List<Card> FindSet()
    {
        List<Card> set = new List<Card>();

        // Checks for match sets
        Predicate<Card> match = card => card.symbol.Equals(cards[0].symbol);
        set = cards.FindAll(match);

        // Set not formed
        if (set.Count < 3)
        {
            List<Card> exempt = set; // Hold the duplicate

            set.Clear(); // Clears the return variable

            set.Add(exempt[0]); // Adds the 1 duplicate to the set

            foreach (Card card in cards)
            {
                foreach (Card ignore in exempt)
                {
                    if (!card.Equals(ignore))
                    {
                        set.Add(card); // Add card to set
                    }
                }

                // Exit when set is formed
                if (set.Count == 3)
                {
                    break;
                }
            }
        }

        return set;
    }
    #endregion
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

public enum Phase 
{
    Reinforcement = 0,
    Attack = 1,
    Fortification = 2,
    End = 3
}
