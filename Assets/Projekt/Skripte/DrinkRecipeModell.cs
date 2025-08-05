using System.Collections.Generic;


// Diese Klasse dient dazu, individuelle Getränke-Rezepte zu erstellen.  
// Sie speichert die Zutaten, besonderen Zutaten, benötigten Werkzeuge, Dekorationen  
// und das passende Glas für jedes Getränk.  
public class DrinkRecipeModell
{
    public Dictionary<string, int> ingredients;  // Zutaten und ihre Mengen in cl
    public List<string> specialItems;            // Besondere Zutaten
    public List<string> tools;                  // Werkzeuge, die verwendet werden
    public List<string> deko;                 // Deko, die verwendet wurden
    public string glasBehälter;           // Glas, welches verwendet wurde 

    // Konstruktor
    public DrinkRecipeModell(Dictionary<string, int> ingredients, List<string> specialItems, List<string> tools, List<string> deko, string glasBehälter)
    {
        this.ingredients = ingredients;
        this.specialItems = specialItems;
        this.tools = tools;
        this.deko = deko;
        this.glasBehälter = glasBehälter;
    }
}
