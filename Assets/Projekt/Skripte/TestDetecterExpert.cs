using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Diese Klasse ist verantwortlich für das Erkennen und Bewerten von Getränken,
// die von NPCs getrunken werden. Sie prüft, ob das richtige Getränk und Glas 
// vorhanden sind, und aktiviert eine Animation, die den Eindruck erweckt, dass der NPC trinkt.
// Zusätzlich wird ein Klon des Behälters erzeugt, um das Getränk visuell darzustellen und zu bewerten.
// Benutzt ebenfalls die ausgabe für die DrinkdisplayUi Klasse bzw. die Abbildung der Checkliste 
// für die Spielmodi: Tutorial und Anfänger

public class TestDetecterExpert : MonoBehaviour
{
    public string currentDrink;
    public Sprechblase sprechblase;
    public DrinkDisplayUI drinkDisplayUI; 

    [Header("Animator Einstellungen")]
    public Animator animator;  
    public string boolParameterName = "toggleBool";  
    public bool toggleBool; 
    private GameObject clonedObject; 
    

    [Header("Zielobjekt für das Child-Setzen")]
    public Transform targetParent; 

    [Header("Audio Einstellungen")]
    public AudioSource audioSource; 
    public AudioClip audioClip; 

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }  
    }

    void Update()
    {
        if (!string.IsNullOrEmpty(sprechblase?.currentDrink))
        {
            currentDrink = sprechblase.currentDrink;
        }

        if (animator != null)
        {
            animator.SetBool(boolParameterName, toggleBool);
        }
    }



void OnTriggerEnter(Collider other)
{
    if (clonedObject != null) 
    {
        Debug.Log("Ein geklontes Objekt existiert bereits, keine neue Kopie wird erstellt.");
        return;
    }

    Behälter objScript = other.gameObject.GetComponentInChildren<Behälter>();
    if (objScript != null)
    {
        toggleBool = true;

        if (animator != null)
        {
            animator.SetBool(boolParameterName, true);
        }


        // Sound wird nach 6 sekunden abgespielt
        StartCoroutine(PlaySoundAfterDelay(6f));


        StartCoroutine(ResetToggleBool());

        Transform parentObj = other.transform.parent;
        if (parentObj == null)
        {
            parentObj = other.transform;
        }

        if (targetParent != null)
        {
            // Klonen nur, wenn kein Objekt an der Targetposition existiert
            if (clonedObject == null)
            {
                clonedObject = Instantiate(parentObj.gameObject, targetParent);
                
                // Position fixieren auf 0,0,0 innerhalb des Parents
                clonedObject.transform.localPosition = Vector3.zero;
                clonedObject.transform.localRotation = Quaternion.identity;
                clonedObject.transform.localScale = new Vector3(80f, 80f, 80f);

                // Collider deaktivieren, um erneutes Triggern zu verhindern
                foreach (Collider col in clonedObject.GetComponentsInChildren<Collider>())
                {
                    col.enabled = false;
                }

                Debug.Log($"{other.name} wurde als Kopie erstellt und zu {targetParent.name} gesetzt.");
            }

            // Originalobjekt zerstören
            Destroy(parentObj.gameObject);
        }
        else
        {
            Debug.LogWarning("Kein Zielobjekt für das Setzen des Parents festgelegt!");
        }

        EvaluateDrink(objScript, other);

        
        //Experten-Modus
        Sprechblase sprechblase = FindObjectOfType<Sprechblase>(); 
        if (sprechblase != null) 
        {
            sprechblase.HideAndResetBubble();
        }

    }
    else
    {
        Debug.Log("Kein Behälter-Script im getriggerten Objekt gefunden.");
    }
}

private IEnumerator ResetToggleBool()
{
    yield return new WaitForSeconds(20);
    toggleBool = false;

    if (animator != null)
    {
        animator.SetBool(boolParameterName, false);
    }

    // Entferne das geklonte Objekt nach dem Timeout
    if (clonedObject != null)
    {
        Debug.Log($"Geklontes Objekt {clonedObject.name} wird entfernt.");
        Destroy(clonedObject);
        yield return new WaitForEndOfFrame(); // Warten, um doppelte Klone zu verhindern
        clonedObject = null; // Sicherstellen, dass es auf null gesetzt wird
    }
}

private IEnumerator PlaySoundAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (audioSource != null && audioClip != null)
        {
            audioSource.PlayOneShot(audioClip); // Spielt das Audio einmal ab
        }
        else
        {
            Debug.LogWarning("AudioSource oder AudioClip fehlt!");
        }
    }
    

    private void EvaluateDrink(Behälter objScript, Collider other)
    {
        // wird überpüft ob ein Getränk von der Sprechblase ausgewählt wurde 
        if (string.IsNullOrEmpty(currentDrink))
        {
            Debug.Log("Kein Getränk ausgewählt zur Evaluierung.");
            return;
        }

        if (!DrinkRecipes.recipes.TryGetValue(currentDrink, out DrinkRecipeModell recipe))
        {
            Debug.Log($"Das Rezept für {currentDrink} wurde nicht gefunden.");
            return;
        }

        // Holt die Tag-Zähler (Zutaten und deren Mengen) aus dem Behälter-Skript
        Dictionary<string, int> tagCounters = objScript.GetTagCounters();
        if (tagCounters == null || tagCounters.Count == 0)
        {
            Debug.Log("Der Behälter enthält keine Flüssigkeiten.");
            return;
        }

        // Wird überprüft ob das richtige Glas verwendet wurde 
        if (!IsCorrectGlass(other))
        {
            Debug.Log($"Falsches Glas für {currentDrink}! Erwartet: {GetExpectedGlass()}, gefunden: {other.tag}");
        }

        
        string facts = "Flüssigkeiten im Behälter:\n";
        foreach (var entry in tagCounters)
        {
            int amount = entry.Value * 5; 
            facts += $"- {entry.Key}: {amount} ml\n";
        }

        // Bewertet die Mengen der Zutaten
        string quantityRating = EvaluateQuantities(recipe, tagCounters);

        // Bewertet spezielle Zutaten, Werkzeuge und Dekorationen
        bool correctSpecials = EvaluateSpecialIngredients(recipe, objScript);
        bool correctTools = EvaluateTools(recipe, objScript);
        bool correctDecoration = EvaluateDecorations(recipe, objScript);

        // Debug Bewertungen Nachrichten 
        if (correctSpecials && correctTools && correctDecoration)
        {
            facts += $"Das Getränk {currentDrink} wurde korrekt zubereitet! Bewertung für Flüssigkeiten: {quantityRating}";
        }
        else
        {
            facts += $"Das Getränk {currentDrink} entspricht nicht dem Rezept. Flüssigkeiten: {quantityRating}";
        }

        // Bewertung auf der Checkliste anezigen lassen 
        if (drinkDisplayUI != null)
        {
            drinkDisplayUI.ShowDrinkChecklist(recipe, tagCounters, objScript, other);
        }
    }

    private string EvaluateQuantities(DrinkRecipeModell recipe, Dictionary<string, int> tagCounters)
    {
        string rating = "Sehr gut";
        foreach (var ingredient in recipe.ingredients)
        {
            if (tagCounters.TryGetValue(ingredient.Key, out int count))
            {
                int actualAmount = count * 5;
                int expectedAmount = ingredient.Value;

                int difference = Mathf.Abs(actualAmount - expectedAmount);

                // Evaluate the difference between actual and expected amounts
                if (difference == 0)
                {
                    Debug.Log($"Die Menge für {ingredient.Key} ist perfekt.");
                }
                else if (difference <= expectedAmount * 0.1f)
                {
                    Debug.Log($"Die Menge für {ingredient.Key} ist fast korrekt, aber etwas abweichend.");
                    rating = "Mittel";
                }
                else
                {
                    Debug.Log($"Die Menge für {ingredient.Key} weicht stark ab.");
                    rating = "Schlecht";
                }
            }
            else
            {
                Debug.Log($"Die Zutat {ingredient.Key} fehlt im Behälter.");
                rating = "Schlecht";
            }
        }
        if (drinkDisplayUI != null)
    {
        drinkDisplayUI.SpawnCoins(rating);
    }

    return rating;
    }

    private bool EvaluateSpecialIngredients(DrinkRecipeModell recipe, Behälter objScript)
{
    bool correctSpecials = true;

    foreach (var special in recipe.specialItems)
    {
        bool isPresent = CheckSpecialIngredient(special, objScript);

        // Prüfen, ob eine doppelte Zuckerart enthalten ist
        if (special == "Brauner Zucker" && objScript.istBraunerZucker2)
        {
            Debug.Log("Es sind zwei Braune Zucker im Behälter.");
        }
        else if (special == "Weißer Zucker" && objScript.istWeißerZucker2)
        {
            Debug.Log("Es sind zwei Weiße Zucker im Behälter.");
        }

        if (!isPresent)
        {
            Debug.Log($"Fehlende spezielle Zutat: {special}.");
            correctSpecials = false;
        }
    }

    return correctSpecials;
}


    private bool EvaluateTools(DrinkRecipeModell recipe, Behälter objScript)
    {
        bool correctTools = true;
        foreach (var tool in recipe.tools)
        {
            if (!CheckTool(tool, objScript))
            {
                Debug.Log($"Fehlendes Werkzeug: {tool}.");
                correctTools = false;
            }
        }
        return correctTools;
    }

    private bool EvaluateDecorations(DrinkRecipeModell recipe, Behälter objScript)
    {
        bool correctDecoration = true;
        foreach (var garnish in recipe.deko)
        {
            if (!CheckDecoration(garnish, objScript))
            {
                Debug.Log($"Fehlende Dekoration: {garnish}.");
                correctDecoration = false;
            }
        }
        return correctDecoration;
    }

    private bool CheckSpecialIngredient(string special, Behälter objScript)
    {
        return special switch
        {
            "Brauner Zucker" => objScript.istBraunerZucker,
            "Weißer Zucker" => objScript.istWeißerZucker,
            "Salz" => objScript.istSalz,
            "Pfeffer" => objScript.istPfeffer,
            "Eiswürfel" => objScript.istEiswürfel,
            "Crushed Ice" => objScript.istCrushedIce,
            "Tabasco" => objScript.istTabasco,
            "Worcestersauce" => objScript.istWorcestersauce,
            _ => false
        };
    }

    private bool CheckTool(string tool, Behälter objScript)
    {
        return tool switch
        {
            "Barlöffel" => objScript.istBarlöffel,
            "Stößel" => objScript.istStößel,
            "Shaker" => objScript.istMixer, // Annahme: Mixer repräsentiert Shaker
            _ => false
        };
    }

    private bool CheckDecoration(string garnish, Behälter objScript)
    {
        return garnish switch
        {
            "Zitrone" => objScript.istZitrone,
            "Limetten" => objScript.istLimetten,
            "Minzblatt" => objScript.istMinzblatt,
            "Orange" => objScript.istOrange,
            "Cocktailkirsche" => objScript.istCocktailkirschen,
            "Ananas" => objScript.istAnnanas,
            "Sellerie" => objScript.istSellerie,
            "Strohhalm" => objScript.istStrohhalm,
            _ => false
        };
    }

    private bool IsCorrectGlass(Collider other)
    {
        if (string.IsNullOrEmpty(currentDrink))
        {
            Debug.Log("Kein Getränk ausgewählt.");
            return false;
        }

        if (DrinkRecipes.recipes.TryGetValue(currentDrink, out DrinkRecipeModell recipe))
        {
            // Compare the tag of the collided object with the expected glass
            return other.CompareTag(recipe.glasBehälter);
        }

        Debug.Log($"Rezept für {currentDrink} wurde nicht gefunden.");
        return false;
    }

    private string GetExpectedGlass()
    {
        if (DrinkRecipes.recipes.TryGetValue(currentDrink, out DrinkRecipeModell recipe))
        {
            return recipe.glasBehälter;
        }
        return "Unbekannt";
    }
}



