using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Dieses Skript zeigt den aktuellen Füllstand eines Jiggers in einem HUD an (genauso wie im Skript "FüllstandHUD"),
// während Flüssigkeit in den Jigger eingefüllt wird und setzt die Anzeige nach kurzer Zeit zurück.
// Es zeigt nur die Menge der aktuell hinzugefügten Flüssigkeit, nicht die Gesamtmenge.
public class FüllstandJigger : MonoBehaviour
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

    private Jigger jigger;

    void Start()
    {
        jigger = GetComponent<Jigger>();
        if (jigger != null)
        {
            füllmengeMaxHUD = jigger.füllMengeMax;
        }
    }

    void Update()
    {
        if (jigger != null && jigger.füllMenge == 0)
        {
            ResetFüllmengeHUD();
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
                countText.text = "Jigger ist voll";
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
