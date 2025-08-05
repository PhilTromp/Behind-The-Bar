using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// Dieses Skript ist dafür verantwortlich, eine Checkliste auf einem Canvas anzuzeigen, wenn ein Getränk auf dem Testdetecter abgelegt wird.
// Die Checkliste zeigt an, welche Zutaten und Gegenstände korrekt vorbereitet sind und was fehlt.
// Außerdem werden Münzen als Bewertungssystem verwendet: 1 Münze für schlecht, 2 Münzen für mittel und 3 Münzen für sehr gut.
public class DrinkDisplayUI : MonoBehaviour
{
    public GameObject drinkCanvas; 
    public Transform checklistContainer; 
    public GameObject checklistItemPrefab; 

    private Coroutine hideCanvasCoroutine;

    public GameObject coinPrefab; 
    public Transform[] coinSpawnPositions; 
    private List<GameObject> spawnedCoins = new List<GameObject>(); 




    public void ShowDrinkChecklist(DrinkRecipeModell recipe, Dictionary<string, int> tagCounters, Behälter objScript, Collider other)
    {
        if (drinkCanvas == null)
        {
            Debug.LogError("Drink Canvas is not assigned!");
            return;
        }

        if (!drinkCanvas.activeSelf)
        {
            drinkCanvas.SetActive(true);
        }

        if (checklistContainer == null)
        {
            Debug.LogError("Checklist Container is not assigned!");
            return;
        }

        if (checklistItemPrefab == null)
        {
            Debug.LogError("Checklist Item Prefab is not assigned!");
            return;
        }

        foreach (Transform child in checklistContainer)
        {
            Destroy(child.gameObject);
        }

        // Hier wird die Checkliste erstellt mit den jeweiligen Zutaten von der DrinkRecipes Klasse 
        AddChecklistItems(recipe.ingredients, tagCounters);
        AddChecklistItems(recipe.specialItems, objScript);
        AddChecklistItems(recipe.tools, objScript);
        AddChecklistItems(recipe.deko, objScript);
        AddGlassChecklistItem(recipe.glasBehälter, other);

        // Canvas wird nach 20 Sekunden wieder deaktiviert 
        if (hideCanvasCoroutine != null)
        {
            StopCoroutine(hideCanvasCoroutine);
        }
        hideCanvasCoroutine = StartCoroutine(HideCanvasAfterDelay(20f));
    }

    private void AddChecklistItems(Dictionary<string, int> requiredItems, Dictionary<string, int> actualItems)
    {
        foreach (var item in requiredItems)
        {
            string itemName = item.Key;
            int requiredAmount = item.Value;
            int actualAmount = actualItems.TryGetValue(itemName, out int count) ? count * 5 : 0;

            bool isPresent = actualItems.ContainsKey(itemName);
            bool isCorrectAmount = actualAmount == requiredAmount;

            string itemText = $"{itemName}: {requiredAmount} ml";
            if (!isPresent)
            {
                itemText += " (Fehlende Flüssigkeit)";
            }
            else if (!isCorrectAmount)
            {
                itemText += actualAmount < requiredAmount ? $" (Zu wenig: {actualAmount} ml)" : $" (Zu viel: {actualAmount} ml)";
            }

            AddChecklistItem(itemText, isPresent && isCorrectAmount);
        }
    }

    private void AddChecklistItems(List<string> requiredItems, Behälter objScript)
    {
        Dictionary<string, int> itemCounts = new Dictionary<string, int>();
        Dictionary<string, int> presentItemCounts = new Dictionary<string, int>();

        foreach (var item in requiredItems)
        {
            if (itemCounts.ContainsKey(item))
                itemCounts[item]++;
            else
                itemCounts[item] = 1;
        }

        foreach (var item in itemCounts.Keys)
        {
            if (item == "Brauner Zucker")
            {
                int count = 0;
                if (objScript.istBraunerZucker) count++;
                if (objScript.istBraunerZucker2) count++;

                presentItemCounts[item] = count;
            }
            else if (item == "Weißer Zucker")
            {
                int count = 0;
                if (objScript.istWeißerZucker) count++;
                if (objScript.istWeißerZucker2) count++;

                presentItemCounts[item] = count;
            }
            else if (CheckItemPresence(item, objScript))
            {
                presentItemCounts[item] = 1;
            }
        }

        foreach (var item in itemCounts)
        {
            string itemName = item.Key;
            int requiredAmount = item.Value;
            int presentAmount = presentItemCounts.ContainsKey(itemName) ? presentItemCounts[itemName] : 0;

            bool isChecked = (requiredAmount == presentAmount);

            string itemText = requiredAmount == 2 ? $"{itemName} (x2)" : itemName;
            AddChecklistItem(itemText, isChecked);
        }
    }

    private void AddGlassChecklistItem(string requiredGlass, Collider other)
    {
        bool isCorrectGlass = other.CompareTag(requiredGlass);
        AddChecklistItem(requiredGlass, isCorrectGlass);
    }

    private void AddChecklistItem(string itemName, bool isChecked)
    {
        GameObject checklistItem = Instantiate(checklistItemPrefab, checklistContainer);
        TMP_Text itemText = checklistItem.GetComponentInChildren<TMP_Text>();
        if (itemText != null)
        {
            itemText.text = itemName;
        }

        Toggle itemToggle = checklistItem.GetComponent<Toggle>();
        if (itemToggle != null)
        {
            itemToggle.isOn = isChecked;
            Debug.Log($"{itemName}: Toggle isOn = {itemToggle.isOn}");
        }
        else
        {
            Debug.LogError("Toggle component is missing on the prefab!");
        }

        StartCoroutine(FixChecklistPosition());
    }

    private IEnumerator FixChecklistPosition()
    {
        yield return new WaitForEndOfFrame();

        Vector3 fixedPosition = checklistContainer.position;

        LayoutRebuilder.ForceRebuildLayoutImmediate(checklistContainer.GetComponent<RectTransform>());

        checklistContainer.position = fixedPosition;
    }

    private bool CheckItemPresence(string item, Behälter objScript)
    {
        return item switch
        {
            "Brauner Zucker" => objScript.istBraunerZucker || objScript.istBraunerZucker2,
            "Weißer Zucker" => objScript.istWeißerZucker|| objScript.istWeißerZucker2,
            "Salz" => objScript.istSalz,
            "Pfeffer" => objScript.istPfeffer,
            "Eiswürfel" => objScript.istEiswürfel,
            "Crushed Ice" => objScript.istCrushedIce,
            "Tabasco" => objScript.istTabasco,
            "Worcestersauce" => objScript.istWorcestersauce,
            "Barlöffel" => objScript.istBarlöffel,
            "Stößel" => objScript.istStößel,
            "Shaker" => objScript.istMixer,
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

    public void HideDrinkFacts()
    {
        if (drinkCanvas != null)
        {
            drinkCanvas.SetActive(false);
            ResetCoins(); // Münzen löschen


            if (hideCanvasCoroutine != null)
            {
                StopCoroutine(hideCanvasCoroutine);
            }
        }
    }

    private IEnumerator HideCanvasAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        HideDrinkFacts();
    }




public void SpawnCoins(string rating)
{
    // Löscht vorherige Münzen
    ResetCoins();

    int coinCount = 0;

    switch (rating)
    {
        case "Sehr gut":
            coinCount = 3;
            break;
        case "Mittel":
            coinCount = 2;
            break;
        case "Schlecht":
            coinCount = 1;
            break;
    }

    // Spawnt Coins an den verschiedenen Positionen
    for (int i = 0; i < coinCount; i++)
    {
        // Erstellt eine neue Münze
        GameObject newCoin = Instantiate(coinPrefab);

        // Überprüft, ob der Index innerhalb der Länge der Spawn-Positionen liegt
        if (i < coinSpawnPositions.Length)
        {
            newCoin.transform.position = coinSpawnPositions[i].position;
            newCoin.transform.SetParent(coinSpawnPositions[i]);
            spawnedCoins.Add(newCoin);
        }
    }
}

public void ResetCoins()
{
    // Zerstört alle aktuell generierten Münzen und lösche die Liste
    foreach (GameObject coin in spawnedCoins)
    {
        Destroy(coin);
    }
    spawnedCoins.Clear();
}


}
