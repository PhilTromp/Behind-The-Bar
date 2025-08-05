using UnityEngine;
using UnityEngine.UI;
using TMPro; 


// Dieses Skript wurde für den Testing-Bereich genutzt, um dem Nutzer die Auswahl eines Getränks zu ermöglichen.  
// Der Testdetector konnte so erkennen, welches Getränk getestet werden sollte.  
// Es ist nicht mehr aktiv im Spiel, war aber hilfreich für die Testphase.  


public class SelectDrink : MonoBehaviour
{
    public Button[] buttons; 
    public string selectedButtonText = ""; // Der String, der den Namen des ausgewählten Buttons speichert
    private Button activeButton; // Der aktuell aktive Button

    void Start()
    {
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => OnButtonClicked(button));
        }
    }

    void OnButtonClicked(Button clickedButton)
    {
        // Vorherigen Button zurücksetzen, falls vorhanden
        if (activeButton != null)
        {
            ResetButtonColor(activeButton);
        }


        TMP_Text buttonText = clickedButton.GetComponentInChildren<TMP_Text>();
        if (buttonText != null)
        {
            // Text des Buttons speichern
            selectedButtonText = buttonText.text;
        }

        // Den aktuellen Button markieren und speichern
        clickedButton.GetComponent<Image>().color = Color.red;
        activeButton = clickedButton;


        Debug.Log("Selected Button Text: " + selectedButtonText);
    }

    void ResetButtonColor(Button button)
    {
        // Farbe des Buttons zurücksetzen (kann nach Bedarf angepasst werden)
        button.GetComponent<Image>().color = Color.white;
    }
}
