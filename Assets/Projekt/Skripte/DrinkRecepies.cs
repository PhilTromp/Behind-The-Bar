using System.Collections.Generic;

// Diese Klasse enthält eine Liste aller Getränke-Rezepte mit ihren jeweiligen Zutaten,  
// besonderen Zutaten, benötigten Werkzeugen, Dekorationen und dem passenden Glas.  
// Die Rezepte werden in einem Dictionary gespeichert, wobei der Name des Getränks als Schlüssel dient.  
public class DrinkRecipes
{
    public static Dictionary<string, DrinkRecipeModell> recipes = new Dictionary<string, DrinkRecipeModell>();

    static DrinkRecipes()
    {
        recipes.Add("Vodka-E", new DrinkRecipeModell(
            new Dictionary<string, int> { { "Vodka", 40 }, { "Energydrink", 150 } }, 
            new List<string> { "Eiswürfel" }, 
            new List<string>(), 
            new List<string>(), 
            "Longdrink Glas"
        ));

        recipes.Add("Cola Korn", new DrinkRecipeModell(
            new Dictionary<string, int> { { "Korn", 40 }, { "Cola", 160 } },
            new List<string> { "Eiswürfel" },
            new List<string>(), 
            new List<string>(),  
            "Longdrink Glas"
        ));

        recipes.Add("Cola Rum", new DrinkRecipeModell(
            new Dictionary<string, int> { { "Rum", 40 }, { "Cola", 160 } },
            new List<string> { "Eiswürfel" },
            new List<string>(),
            new List<string> { "Zitrone" }, 
            "Longdrink Glas"
        ));

        recipes.Add("Jacky Cola", new DrinkRecipeModell(
            new Dictionary<string, int> { { "Whisky", 40 }, { "Cola", 120 }, { "Zitronensaft", 10 } },
            new List<string> { "Eiswürfel" },
            new List<string>(), 
            new List<string> { "Zitrone" }, 
            "Longdrink Glas"
        ));

        recipes.Add("Negroni", new DrinkRecipeModell(
            new Dictionary<string, int> { { "Gin", 30 }, { "Campari", 30 }, { "Roter Wermut", 30 } },
            new List<string> { "Eiswürfel" },
            new List<string> { "Barlöffel" }, 
            new List<string> { "Orange" }, 
            "Tumbler Glas"
        ));

        recipes.Add("Wodka Sour", new DrinkRecipeModell(
            new Dictionary<string, int> { { "Vodka", 50 }, { "Zitronensaft", 30 }, { "Zuckersirup", 20 } },
            new List<string> { "Eiswürfel" },
            new List<string> { "Shaker", }, 
            new List<string>(),  
            "Tumbler Glas"
        ));

        recipes.Add("Manhattan", new DrinkRecipeModell(
            new Dictionary<string, int> { { "Rye Whisky", 50 }, { "Roter Wermut", 20 } },
            new List<string> { "Eiswürfel" },
            new List<string> { "Barlöffel" }, 
            new List<string> { "Cocktailkirsche" }, 
            "Cocktail Glas"
        ));
        // Einmal Zucker (falsch)
        /*
        recipes.Add("Daiquiri", new DrinkRecipeModell(
            new Dictionary<string, int> { { "Rum", 50 }, { "Limettensaft", 25 }},
            new List<string> {"Weißer Zucker"},
            new List<string> { "Shaker" }, // Werkzeuge: Shaker, Teesieb, Teelöffel
            new List<string> { "Limetten" }, // Deko: Limettenscheibe
            "Coupette Glas"
        ));
        */
        recipes.Add("Daiquiri", new DrinkRecipeModell(
            new Dictionary<string, int> { { "Rum", 50 }, { "Limettensaft", 25 }},
            new List<string> {"Weißer Zucker", "Weißer Zucker"},
            new List<string> { "Shaker" }, 
            new List<string> { "Limetten" }, 
            "Coupette Glas"
        ));
        // Einmal Zucker (falsch)
        /*
        recipes.Add("Mojito", new DrinkRecipeModell(
            new Dictionary<string, int> { { "Rum", 50 }, { "Soda", 60 }, { "Limettensaft", 25 } },
            new List<string> {"Crushed Ice", "Brauner Zucker" },
            new List<string> { "Stößel", "Barlöffel" }, // Werkzeuge: Stößel, Barlöffel
            new List<string> { "Minzblatt", "Limetten" }, // Deko: Minzblatt, Limettenscheibe // Minzblatt nicht wirklich deko aber soll man ja sehen also halb deko
            "Longdrink Glas"
        ));
        */
        recipes.Add("Mojito", new DrinkRecipeModell(
            new Dictionary<string, int> { { "Rum", 50 }, { "Soda", 60 }, { "Limettensaft", 25 } },
            new List<string> { "Crushed Ice", "Brauner Zucker", "Brauner Zucker" }, 
            new List<string> { "Stößel", "Barlöffel" }, 
            new List<string> { "Minzblatt", "Limetten" }, 
            "Longdrink Glas"
        ));

        recipes.Add("Caipirinha", new DrinkRecipeModell(
            new Dictionary<string, int> { { "Cachaca", 60 } },
            new List<string> { "Brauner Zucker", "Crushed Ice" }, 
            new List<string> { "Stößel" }, 
            new List<string> {"Strohhalm", "Limetten"}, 
            "Tumbler Glas"
        ));

        recipes.Add("Sex on the Beach", new DrinkRecipeModell(
            new Dictionary<string, int> { { "Vodka", 40 }, { "Pfirsich Likör", 20 }, { "Cranberry Direktsaft", 40 }, { "Zitronensaft", 10 }, { "Orangensaft", 250 } },
            new List<string> { "Eiswürfel" },
            new List<string> { "Shaker" }, 
            new List<string> { "Ananas", "Strohhalm", "Cocktailkirsche" }, 
            "Hurricane Glas"
        ));

        recipes.Add("Bloody Mary", new DrinkRecipeModell(
            new Dictionary<string, int> { { "Vodka", 20 }, { "Tomatensaft", 60 }, { "Zitronensaft", 20 } },
            new List<string> { "Tabasco", "Worcestersauce", "Salz", "Pfeffer" },
            new List<string> { "Barlöffel" }, 
            new List<string> { "Limetten", "Sellerie" }, 
            "Longdrink Glas"
        ));

        recipes.Add("Long Island Iced Tea", new DrinkRecipeModell(
            new Dictionary<string, int> { { "Vodka", 15 }, { "Rum", 15 }, { "Tequila", 15 }, { "Gin", 15 }, { "Cointreau", 15 }, { "Limettensaft", 20 }, { "Cola", 140 } },
            new List<string> { "Crushed Ice" },
            new List<string> { "Barlöffel" }, 
            new List<string> {"Strohhalm"}, 
            "Longdrink Glas"
        ));
    }
}
