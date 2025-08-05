using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Dieses Skript zeigt den aktuellen Füllstand eines Shakers in einem HUD an (genauso wie im Skript "FüllstandHUD"),
// während Flüssigkeit in den Shaker eingefüllt wird und setzt die Anzeige nach kurzer Zeit zurück.
// Es zeigt nur die Menge der aktuell hinzugefügten Flüssigkeit, nicht die Gesamtmenge.
// Die HUD kann nur angezeigt werden, wenn kein Deckel auf dem Shaker drauf ist 
public class FüllstandShaker : MonoBehaviour
{
    public Canvas displayCanvas;
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

    private Shaker shaker;

    void Start()
    {
        shaker = GetComponent<Shaker>();
        if (shaker != null)
        {
            füllmengeMaxHUD = shaker.füllMengeMax;
            füllmengeHUD = shaker.füllMenge; 
        }
    }

    void Update()
    {
        if (shaker != null)
        {
            füllmengeHUD = shaker.füllMenge;
            if (füllmengeHUD == 0)
            {
                ResetFüllmengeHUD(); 
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
                    countText.text = currentFüllmenge.ToString() + " ml"; 
                }
                isFull = false;
            }
        }
    }

    void ResetCurrentFüllmenge()
    {
        currentFüllmenge = 0;
        countText.text = ""; 
        isCanvasActive = false; 
    }

    void HideCanvas()
    {
        displayCanvas.gameObject.SetActive(false);
        isCanvasActive = false;
        isFull = false;
    }

    void ResetFüllmengeHUD()
    {
        füllmengeHUD = 0f;
        currentFüllmenge = 0f;
        countText.text = ""; 
        displayCanvas.gameObject.SetActive(false); 
        isCanvasActive = false;
    }
}
