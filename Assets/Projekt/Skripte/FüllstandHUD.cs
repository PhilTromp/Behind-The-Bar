using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


// Dieses Skript zeigt den aktuellen Füllstand eines Behälters in einem HUD an,
// während Flüssigkeit in den Behälter eingefüllt wird und setzt die Anzeige nach kurzer Zeit zurück.
// Es zeigt nur die Menge der aktuell hinzugefügten Flüssigkeit, nicht die Gesamtmenge.

public class FüllstandHUD : MonoBehaviour
{
    public Canvas displayCanvas; // Canvas für die Anzeige der Flüssigkeitsmenge 
    public TextMeshProUGUI countText;
    public float füllmengeHUD = 0f;
    public float currentFüllmenge = 0f;
    private float füllmengeMaxHUD;
    
    private float resetTimer = 0f;
    private bool isCanvasActive = false;
    private bool isFull = false;

    private readonly HashSet<string> validTags = new HashSet<string>
    {
        "Vodka", "Korn", "Rum", "Whisky", "Rye Whisky",
        "Tequila", "Gin", "Campari", "Roter Wermut",
        "Pfirsich Likör", "Cachaca", "Cointreau", "Energydrink",
        "Cola", "Zitronensaft", "Limettensaft", "Orangensaft",
        "Cranberry Direktsaft", "Soda", "Tomatensaft", "Zuckersirup"
    };

    private Behälter behälter;

    void Start()
    {
        behälter = GetComponent<Behälter>();
        if (behälter != null)
        {
            füllmengeMaxHUD = behälter.füllMengeMax;
            füllmengeHUD = behälter.füllMenge; 
        }
    }

    void Update()
    {
        // die Füllmenge vom Behälter abfragen und aktualisieren
        if (behälter != null)
        {
            // Synchronisiere den Wert der Füllmenge im HUD mit dem Wert aus dem Behälter
            füllmengeHUD = behälter.füllMenge;
            //currentFüllmenge = behälter.füllMenge;

            // Überprüfen, ob die Füllmenge im Behälter 0 ist
            if (füllmengeHUD == 0)
            {
                ResetFüllmengeHUD(); // Reset, wenn die Füllmenge 0 ist
            }
        }

        if (isCanvasActive)
        {
            resetTimer -= Time.deltaTime;

            if (resetTimer <= 0f)
            {
                if (isFull)
                {
                    HideCanvas();
                }
                else
                {
                    ResetCurrentFüllmenge();
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (validTags.Contains(other.tag))
        {
            float value = 5;
            füllmengeHUD += value;
            currentFüllmenge += value;

            if (!isCanvasActive)
            {
                displayCanvas.gameObject.SetActive(true);
                isCanvasActive = true;
            }

            resetTimer = füllmengeHUD >= füllmengeMaxHUD ? 2f : 3f;

            UpdateDisplay();

            Destroy(other.gameObject);
        }
    }

    void UpdateDisplay()
    {
        if (countText != null)
        {
            if (füllmengeHUD >= füllmengeMaxHUD)
            {
                countText.text = "Glas ist voll";
                isFull = true;
            }
            else
            {
                if (currentFüllmenge == 0)
                {
                    countText.text = ""; 
                }
                else
                {
                    countText.text = currentFüllmenge.ToString() + " ml"; // lässt Flüssigkeitsmenge anzeigen 
                }
                isFull = false;
            }
        }
    }

    void ResetCurrentFüllmenge()
    {
        currentFüllmenge = 0;
        countText.text = ""; // Text bleibt leer, wenn currentFüllmenge 0 ist
        isCanvasActive = false; // Canvas ausblenden
    }


    // Solange nichts eingefüllt wird, soll die Hud wieder verschwinden
    void HideCanvas()
    {
        displayCanvas.gameObject.SetActive(false);
        isCanvasActive = false;
        isFull = false;
    }


    // Anzeige soll nach kurzer Zeit ebenfalls verschwinden 
    void ResetFüllmengeHUD()
    {
        füllmengeHUD = 0f;
        currentFüllmenge = 0f;
        countText.text = ""; // Text leer lassen, wenn die Füllmenge 0 ist
        displayCanvas.gameObject.SetActive(false); // Canvas ausblenden, wenn die Füllmenge 0 ist
        isCanvasActive = false;
    }
}
