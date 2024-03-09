namespace INFR2100U.Player;

using INFR2100U.Card;
using INFR2100U.Country;
using INFR2100U.Continent;

public class Player
{
    #region PUBLIC_DATA
    public PlayerColour playerColour { get; private set; } = PlayerColour.None;
    public int turnOrder { get; private set; }
    public List<Country> controlledTerritory = new List<Country>();
    public List<Card> cards = new List<Card>();
    public bool isElimiated = false;
    #endregion


    #region PRIVATE_DATA
    private int setTraded = 0;
    private bool cardTradeBonus = false;
    private bool successfulConquer = false;
    private int armyUsed = 0;
    private int reienforcmentOwned = 0;
    private GamePhase currentPhase;
    #endregion


    public Player(int turn, PlayerColour colour, int startingArmies)
    {
        // Default Setting on load
        setTraded = 0;
        cardTradeBonus = false;
        currentPhase = GamePhase.End;
        reienforcmentOwned = 0;

        // Clear list
        controlledTerritory.Clear();
        cards.Clear();

        // Set player values
        turnOrder = turn;
        playerColour = colour;
        reienforcmentOwned = startingArmies;
    }


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

        // Exit if the set is less than 3 cards
        if (set.Count < 3)
        {
            return 0;
        }

        // Remove the cards from the list
        cards.Remove(set[0]);
        cards.Remove(set[1]);
        cards.Remove(set[2]);

        setTraded++;

        // Checks if any card has a country on it that the player owns for bonus armies on trade.
        foreach (Country controlledCountry in controlledTerritory)
        {
            if ((set[0].country == controlledCountry || set[1].country == controlledCountry || set[2].country == controlledCountry) && cardTradeBonus == false)
            {
                cardTradeBonus = true;
            }
        }

        return SetBonus();
    }

    public void PlayerTurn()
    {
        if (armyUsed == 0)
        {
            int population = (int)Math.Ceiling((double)reienforcmentOwned / controlledTerritory.Count);

            foreach(Country owned in controlledTerritory)
            {
                owned.population = population;
                armyUsed += population;
                reienforcmentOwned -= (reienforcmentOwned - population) >= 0 ? population : reienforcmentOwned;
            }
        }

        Reinforcement();
        Attack();
        Fortification();
        EndTurn();
    }


    #region GAME_PHASE
    /// <summary>
    /// Reienforcement Phase. Also known as the start of the turn.
    /// </summary>
    private void Reinforcement()
    {
        currentPhase = GamePhase.Reinforcement;

        int gainedArmy = Convert.ToInt32(MathF.Floor(controlledTerritory.Count / 3));

        // Get bonus for owning a continent
        foreach (Continent continent in Map.map)
        {
            if (continent.CheckIfSoleOwned(this))
            {
                gainedArmy += continent.controlValue;
            }
        }

        // Auto trade a set when the player has 5 cards.
        if (cards.Count == 5)
        {
            gainedArmy += TradeSet();
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

    /// <summary>
    /// Attack Phase
    /// </summary>
    private void Attack()
    {
    ATTACK_AGAIN:

        #region Player_Attack?
        
        string attackAgain = "";

        while (attackAgain.ToUpper() != "Y" && attackAgain.ToUpper() != "N")
        {
            Console.WriteLine("Do you want to attack? (Y/N): ");
            attackAgain = Console.ReadLine();
        }

        if (attackAgain.ToUpper() == "N")
        {
            return;
        }

        #endregion

        currentPhase = GamePhase.Attack;

        #region Choose_Attack_Point
        Console.WriteLine("Choose territory you want to attack from.");
        Country attackingTerritory = null;

        string owned = "";
        List<Country> validAttackers = new List<Country>();
        foreach(Country ownedCountry in controlledTerritory)
        {
            if (ownedCountry!= controlledTerritory.First())
            {
                owned += ", ";
            }

            if(ownedCountry.population > 1)
            {
                owned += $"{ownedCountry.countryName}";
                validAttackers.Add(ownedCountry);
            }
        }
        Console.WriteLine(owned);


        string attackingName = Console.ReadLine();
        foreach(Country attackOrigin in validAttackers)
        {
            if (attackOrigin.countryName == attackingName)
            {
                attackingTerritory = attackOrigin;
                break;
            }
        }
        #endregion

        #region Choose_Enemy_Territory
        Console.WriteLine("Choose a territory to attack: ");

        List<Country> enemyCountryList = new List<Country>();
        string enemyCountries = "";
        foreach (Continent continent in Map.map)
        {
            if (continent.CheckIfSoleOwned(this) == false)
            {
                foreach (Country enemy in continent.countries.GetNeighbors(attackingTerritory))
                {
                    if (enemy.owner.playerColour != playerColour)
                    {
                        if (enemy != continent.countries.GetNeighbors(attackingTerritory).First())
                        {
                            enemyCountries += ", ";
                        }

                        enemyCountries += $"{enemy.countryName}";
                        enemyCountryList.Add(enemy);
                    }
                }
            }
        }
        Console.WriteLine($"Adjacent Countries to {attackingTerritory.countryName}: {enemyCountries} \n");


        string enemyTerritory = Console.ReadLine();
        Country defendingTerritory = null;
        foreach(Country enemyCountry in enemyCountryList)
        {
            if (enemyCountry.countryName == enemyTerritory)
            {
                defendingTerritory = enemyCountry;
            }
        }
        #endregion

        #region Attack
        int diceCount = 0;

        while (diceCount < 1 || diceCount > 3 && diceCount < attackingTerritory.population)
        {
            Console.WriteLine("Choose the number of dice to roll (1-3). You must have 1 more army than dice rolled: ");
            diceCount = Console.Read();
        }

        if (GetHighestRoll(diceCount) > defendingTerritory.owner.Defend(defendingTerritory))
        {
            defendingTerritory.population--;
        }
        else
        {
            attackingTerritory.population--;
        }

        if (defendingTerritory.population <= 0)
        {
            defendingTerritory.owner.LostTerritory(defendingTerritory);

            defendingTerritory.owner = this;
            controlledTerritory.Add(defendingTerritory);

            defendingTerritory.population = diceCount;

            attackingTerritory.population -= diceCount;

            successfulConquer = true;
        }

        #endregion

        goto ATTACK_AGAIN;
    }

    /// <summary>
    /// Rolls for defending your territory.
    /// </summary>
    /// <param name="defending">The country that is defending.</param>
    /// <returns>The highest roll needed for the attack phase.</returns>
    public int Defend(Country defending)
    {
        int defendRoll = 0;

        while (defendRoll <  0 || defendRoll > 2 && defending.population >= defendRoll)
        {
            Console.WriteLine("Choose the number of dice to roll (1-2). Number of dice cannot exceed the number of armies you have: ");
            defendRoll = Console.Read();
        }

        return GetHighestRoll(defendRoll);
    }

    /// <summary>
    /// Fortification Phase
    /// </summary>
    private void Fortification()
    {
        currentPhase = GamePhase.Fortification;
    }

    /// <summary>
    /// End turn
    /// </summary>
    private void EndTurn()
    {
        currentPhase = GamePhase.End;

        if (successfulConquer)
            DrawCard();

        successfulConquer = false;
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


    #region HELPERS
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


    private int GetHighestRoll(int diceCount)
    {
        int highest = 0;

        List<int> rolls = new List<int>();
        for (int i = 0; i < diceCount; i++)
        {
            rolls.Add(new Random().Next(1, 7)); // Max is exclusive
        }

        highest = rolls.First();
        foreach(int roll in rolls)
        {
            highest = highest < roll ? highest : roll;
        }

        return highest;
    }

    public void LostTerritory(Country lost)
    {
        controlledTerritory.Remove(lost);

        if (controlledTerritory.Count == 0)
        {
            isElimiated = true;
        }
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

public enum GamePhase 
{
    Reinforcement = 0,
    Attack = 1,
    Fortification = 2,
    End = 3
}
