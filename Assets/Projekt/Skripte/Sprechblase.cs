using System.Collections;
using UnityEngine;
using TMPro;


// Dieses Skript steuert eine Sprechblase über dem NPC im Spielmodus "Experte".  
// Die Sprechblase zeigt das gewünschte Getränk des NPCs an.  
// Nach einer bestimmten Zeit und wenn man das vorherige Getränk abgeschlossen hat, wird die Sprechblase ausgeblendet und ein neues Getränk zufällig gewählt.  

public class Sprechblase : MonoBehaviour
{
    public TextMeshProUGUI textElement;
    private Canvas canvas;

    private string[] drinks = { "Vodka-E", "Cola Korn", "Cola Rum", "Jacky Cola", "Negroni", "Wodka Sour", "Manhattan", "Daiquiri", "Mojito", "Caipirinha", "Sex on the Beach", "Bloody Mary", "Long Island Iced Tea" };


    public string currentDrink;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
        if (textElement == null)
        {
            Debug.LogError("TextMeshPro Element wurde nicht zugewiesen!");
            return;
        }

        // Ersten zufälligen Drink setzen
        SetRandomDrink();
    }

    private void SetRandomDrink()
    {
        currentDrink = drinks[Random.Range(0, drinks.Length)];
        textElement.text = $"Ich haette gerne einen {currentDrink}";
    }

    public void HideAndResetBubble()
    {
        StartCoroutine(DeactivateAndReactivate());
    }

    private IEnumerator DeactivateAndReactivate()
    {
        canvas.enabled = false; 
        yield return new WaitForSeconds(25); 
        SetRandomDrink(); 
        canvas.enabled = true; 
    }
}
