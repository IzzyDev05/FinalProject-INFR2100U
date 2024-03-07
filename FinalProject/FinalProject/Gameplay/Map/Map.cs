using INFR2100U.Continent;
using INFR2100U.Graph;
using INFR2100U.Country;


public static class Map
{
	public static Graph<Continent> map = new Graph<Continent>();

    #region Subgraph_Setup
    private static Graph<Country> SetupNorthAmerica()
    {
        Graph<Country> NorthAmerica = new Graph<Country>();

        Country Alaska = new Country("Alaska", 1);
        Country Alberta = new Country("Alberta", 2);
        Country CentralAmerica = new Country("Central America", 3);
        Country EasternUnitedStates = new Country("Eastern United States", 4);
        Country Greenland = new Country("Greenland", 5);
        Country NorthwestTerritory = new Country("Northwest Territory", 6);
        Country Ontario = new Country("Ontario", 7);
        Country Quebec = new Country("Quebec", 8);
        Country WesternUnitedStates = new Country("Western United States", 9);
        
        

        NorthAmerica.Add(Alaska, new List<Country> { Alberta, NorthwestTerritory });
        NorthAmerica.Add(Alberta, new List<Country> { Alaska, NorthwestTerritory, Ontario, WesternUnitedStates });
        NorthAmerica.Add(CentralAmerica, new List<Country> { EasternUnitedStates, WesternUnitedStates });
        NorthAmerica.Add(EasternUnitedStates, new List<Country> { CentralAmerica, Ontario, Quebec, WesternUnitedStates });
        NorthAmerica.Add(Greenland, new List<Country> { NorthwestTerritory, Ontario, Quebec });
        NorthAmerica.Add(NorthwestTerritory, new List<Country> { Alaska, Alberta, Greenland, Ontario });
        NorthAmerica.Add(Ontario, new List<Country> { Alberta, EasternUnitedStates, Greenland, NorthwestTerritory, Quebec, WesternUnitedStates });
        NorthAmerica.Add(Quebec, new List<Country> { EasternUnitedStates, Greenland, Ontario });
        NorthAmerica.Add(WesternUnitedStates, new List<Country> { Alberta, CentralAmerica, EasternUnitedStates, Ontario });

        return NorthAmerica;
    }

    private static Graph<Country> SetupSouthAmerica()
    {
        Graph<Country> SouthAmerica = new Graph<Country>();

        Country Argentina = new Country("Argentina", 1);
        Country Brazil = new Country("Brazil", 2);
        Country Venezuela = new Country("Venezuela", 3);
        Country Peru = new Country("Peru", 4);

        SouthAmerica.Add(Argentina, new List<Country> { Brazil, Peru });
        SouthAmerica.Add(Brazil, new List<Country> { Argentina, Venezuela, Peru });
        SouthAmerica.Add(Venezuela, new List<Country> { Brazil, Peru });
        SouthAmerica.Add(Peru, new List<Country> { Argentina, Brazil, Venezuela });

        return SouthAmerica;
    }

    private static Graph<Country> SetupEurope()
    {
        Graph<Country> Europe = new Graph<Country>();

        Country GreatBritain = new Country("Great Britain", 1);
        Country Iceland = new Country("Iceland", 2);
        Country NorthernEurope = new Country("Northern Europe", 3);
        Country Scandinavia = new Country("Scandinavia", 4);
        Country SouthernEurope = new Country("Southern Europe", 5);
        Country Ukraine = new Country("Ukraine", 6);
        Country WesternEurope = new Country("Western Europe", 7);

        Europe.Add(GreatBritain, new List<Country> { Iceland, NorthernEurope, Scandinavia, WesternEurope });
        Europe.Add(Iceland, new List<Country> { GreatBritain, Scandinavia });
        Europe.Add(NorthernEurope, new List<Country> { GreatBritain, Scandinavia, SouthernEurope, Ukraine, WesternEurope });
        Europe.Add(Scandinavia, new List<Country> { GreatBritain, Iceland, NorthernEurope, Ukraine });
        Europe.Add(SouthernEurope, new List<Country> { NorthernEurope, Ukraine, WesternEurope });
        Europe.Add(Ukraine, new List<Country> { NorthernEurope, Scandinavia, SouthernEurope });
        Europe.Add(WesternEurope, new List<Country> { GreatBritain, NorthernEurope, SouthernEurope });

        return Europe;
    }

    private static Graph<Country> SetupAfrica()
    {
        Graph<Country> Africa = new Graph<Country>();

        Country Congo = new Country("Congo", 1);
        Country EastAfrica = new Country("East Africa", 2);
        Country Egypt = new Country("Egypt", 3);
        Country Madagascar = new Country("Madagascar", 4);
        Country NorthAfrica = new Country("North Africa", 5);
        Country SouthAfrica = new Country("South Africa", 6);

        Africa.Add(Congo, new List<Country> { EastAfrica, NorthAfrica, SouthAfrica });
        Africa.Add(EastAfrica, new List<Country> { Congo, Egypt, Madagascar, SouthAfrica });
        Africa.Add(Egypt, new List<Country> { EastAfrica, NorthAfrica });
        Africa.Add(Madagascar, new List<Country> { EastAfrica, SouthAfrica });
        Africa.Add(NorthAfrica, new List<Country> { Congo, EastAfrica, Egypt });
        Africa.Add(SouthAfrica, new List<Country> { Congo, EastAfrica, Madagascar });

        return Africa;
    }

    private static Graph<Country> SetupAsia()
    {
        Graph<Country> Asia = new Graph<Country>();

        Country Afghanistan = new Country("Afghanistan", 1);
        Country China = new Country("China", 2);
        Country India = new Country("India", 3);
        Country Irkutsk = new Country("Irkutsk", 4);
        Country Japan = new Country("Japan", 5);
        Country Kamchatka = new Country("Kamchatka", 6);
        Country MiddleEast = new Country("Middle East", 7);
        Country Mongolia = new Country("Mongolia", 8);
        Country Siam = new Country("Siam", 9);
        Country Siberia = new Country("Siberia", 10);
        Country Ural = new Country("Ural", 11);
        Country Yakutsk = new Country("Yakutsk", 12);

        Asia.Add(Afghanistan, new List<Country> { China, India, MiddleEast, Ural });
        Asia.Add(China, new List<Country> { Afghanistan, India,Mongolia, Siam, Siberia, Ural });
        Asia.Add(India, new List<Country> { Afghanistan, China, MiddleEast, Siam });
        Asia.Add(Irkutsk, new List<Country> { Kamchatka, Mongolia, Siberia, Yakutsk });
        Asia.Add(Japan, new List<Country> { Kamchatka, Mongolia });
        Asia.Add(Kamchatka, new List<Country> { Irkutsk, Japan, Mongolia, Yakutsk });
        Asia.Add(MiddleEast, new List<Country> { Afghanistan, India });
        Asia.Add(Mongolia, new List<Country> { China, Irkutsk, Japan, Kamchatka, Siberia });
        Asia.Add(Siam, new List<Country> { China, India });
        Asia.Add(Siberia, new List<Country> { China, Irkutsk, Mongolia, Ural, Yakutsk });
        Asia.Add(Ural, new List<Country> { Afghanistan, China, Siberia });
        Asia.Add(Yakutsk, new List<Country> { Irkutsk, Kamchatka, Siberia });

        return Asia;
    }

    private static Graph<Country> SetupAustralia()
    {
        Graph<Country> Australia = new Graph<Country>();

        Country EasternAustralia = new Country("Eastern Australia", 1);
        Country NewGuinea = new Country("New Guinea", 2);
        Country Indoneisa = new Country("Indoneisa", 3);
        Country WesternAustralia = new Country("Western Australia", 4);

        Australia.Add(EasternAustralia, new List<Country> { NewGuinea, WesternAustralia });
        Australia.Add(NewGuinea, new List<Country> { EasternAustralia, Indoneisa, WesternAustralia });
        Australia.Add(Indoneisa, new List<Country> { NewGuinea, WesternAustralia });
        Australia.Add(WesternAustralia, new List<Country> { EasternAustralia, NewGuinea, Indoneisa });

        return Australia;
    }
    #endregion

    public static void SetCountryGraph()
	{
        // North America
        SetupNorthAmerica();

        // South America
        SetupSouthAmerica();

        // Europe
        SetupEurope();

        // Africa
        SetupAfrica();

        // Asia
        SetupAsia();

        // Australia
        SetupAustralia();
	}

	public static void MakeMapGraph() 
	{
        // Create Continents
        Continent NorthAmerica = new Continent("North America", SetupNorthAmerica());
        Continent SouthAmerica = new Continent("South America", SetupSouthAmerica());
        Continent Europe = new Continent("Europe", SetupEurope());
        Continent Africa = new Continent("Africa", SetupAfrica());
        Continent Asia = new Continent("Asia", SetupAsia());
        Continent Austrialia = new Continent("Australia", SetupAustralia());

        // Link Continents by Country adjacency
        #region NA_Link
        NorthAmerica.countries[new Country("Greenland", 5)].Add(Europe.countries.FindNode(new Country("Iceland", 2))); // Link to Europe
        NorthAmerica.countries[new Country("Central America", 3)].Add(SouthAmerica.countries.FindNode(new Country("Venezuela", 3))); // Link to South America
        NorthAmerica.countries[new Country("Alaska", 1)].Add(Asia.countries.FindNode(new Country("Kamchatka", 6))); // Link to Asia
        #endregion

        #region SA_Link    
        SouthAmerica.countries[new Country("Brazil", 2)].Add(Africa.countries.FindNode(new Country("North Africa", 5))); // Link to Africa
        SouthAmerica.countries[new Country("Venezuela", 3)].Add(NorthAmerica.countries.FindNode(new Country("Central America", 3))); // Link to North America
        #endregion

        #region EU_Link
        Europe.countries[new Country("Southern Europe", 5)].Add(Asia.countries.FindNode(new Country("Middle East", 7))); // Link to Asia
        Europe.countries[new Country("Ukraine", 6)].Add(Asia.countries.FindNode(new Country("Afghanistan", 1))); // Link to Asia
        Europe.countries[new Country("Ukraine", 6)].Add(Asia.countries.FindNode(new Country("Middle East", 7))); // Link to Asia
        Europe.countries[new Country("Ukraine", 6)].Add(Asia.countries.FindNode(new Country("Ural", 11))); // Link to Asia

        Europe.countries[new Country("Iceland", 2)].Add(NorthAmerica.countries.FindNode(new Country("Greenland", 5))); // Link to North America

        Europe.countries[new Country("Southern Europe", 5)].Add(Africa.countries.FindNode(new Country("Egypt", 3))); // Link to Africa
        //Europe.countries[new Country("Southern Europe", 5)].Add(Africa.countries.FindNode(new Country("North Africa", 5))); // Link to Africa
        Europe.countries[new Country("Western Europe", 7)].Add(Africa.countries.FindNode(new Country("North Africa", 5))); // Link to Africa
        #endregion

        #region AF_Link
        Africa.countries[new Country("North Africa", 5)].Add(SouthAmerica.countries.FindNode(new Country("Brazil", 2))); //Link to South America

        Africa.countries[new Country("Egypt", 3)].Add(Europe.countries.FindNode(new Country("Southern Europe", 5))); //Link to Europe
        //Africa.countries[new Country("North Africa", 5)].Add(Europe.countries.FindNode(new Country("Southern Europe", 5))); //Link to Europe
        Africa.countries[new Country("North Africa", 5)].Add(Europe.countries.FindNode(new Country("Western Europe", 7))); //Link to Europe

        Africa.countries[new Country("Egypt", 3)].Add(Asia.countries.FindNode(new Country("Middle East", 7))); // Link to Asia
        Africa.countries[new Country("East Africa", 2)].Add(Asia.countries.FindNode(new Country("Middle East", 7))); // Link to Asia
        #endregion

        #region AS_Link
        Asia.countries[new Country("Afghanistan", 1)].Add(Europe.countries.FindNode(new Country("Ukraine", 6))); // Link to Europe
        Asia.countries[new Country("Ural", 11)].Add(Europe.countries.FindNode(new Country("Ukraine", 6))); // Link to Europe
        Asia.countries[new Country("Middle East", 7)].Add(Europe.countries.FindNode(new Country("Ukraine", 6))); // Link to Europe
        Asia.countries[new Country("Middle East", 7)].Add(Europe.countries.FindNode(new Country("Southern Europe", 5))); // Linke to Europe

        Asia.countries[new Country("Middle East", 7)].Add(Africa.countries.FindNode(new Country("Egypt", 3))); // Link to Africa
        Asia.countries[new Country("Middle East", 7)].Add(Africa.countries.FindNode(new Country("East Africa", 2))); // Link to Africa

        Asia.countries[new Country("Siam", 9)].Add(Austrialia.countries.FindNode(new Country("Indoneisa", 3))); // Link to Australia

        Asia.countries[new Country("Kamchatka", 6)].Add(NorthAmerica.countries.FindNode(new Country("Alaska", 1))); // Link to North America
        #endregion

        #region AU_Link
        Austrialia.countries[new Country("Indoneisa", 3)].Add(Asia.countries.FindNode(new Country("Siam", 9))); // Link to Asia
        #endregion

        // Link Continents to create map
        map.Add(NorthAmerica, new List<Continent> { SouthAmerica, Europe, Asia });
        map.Add(SouthAmerica, new List<Continent> { NorthAmerica, Africa });
        map.Add(Europe, new List<Continent> { NorthAmerica, Africa, Asia });
        map.Add(Africa, new List<Continent> { SouthAmerica, Europe, Asia });
        map.Add(Asia, new List<Continent> { NorthAmerica, Europe, Africa, Austrialia });
        map.Add(Austrialia, new List<Continent> { Asia });
	}
}