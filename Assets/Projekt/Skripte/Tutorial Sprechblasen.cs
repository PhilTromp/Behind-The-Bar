using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

// Dieses Skript steuert die Sprechblasen im Tutorial-Modus. 
// Es ermöglicht das Durchlaufen mehrerer Canvas-Elemente durch den "Weiter"-Button und 
// berücksichtigt spezielle Logik für bestimmte Schritte, z. B. das Warten auf eine Zustandsänderung in TestDetector.
// Sobald alle Tutorial-Schritte abgeschlossen sind, wird die nächste Szene geladen.


public class TutorialSprechblasen : MonoBehaviour
{
    [System.Serializable]
    public struct CanvasData
    {
        public Canvas canvas;      
        public Button weiterButton; 
    }

    public CanvasData[] canvasDataArray; 
    public string nextSceneName; 
    public TestDetecter testDetector; 

    private int currentCanvasIndex = 0;
    private bool isWaitingForChangeState = false; 

    void Start()
    {
        foreach (CanvasData canvasData in canvasDataArray)
        {
            canvasData.canvas.gameObject.SetActive(false);
        }

        if (canvasDataArray.Length > 0)
        {
            ActivateCanvas(currentCanvasIndex);
        }
    }


    // Überprüft den Zustand und wechselt das Canvas, wenn eine Zustandsänderung auftritt
    void Update()
    {
        if (currentCanvasIndex == 8 && testDetector != null)
        {
            if (testDetector.toggleBool)
            {
                canvasDataArray[currentCanvasIndex].canvas.gameObject.SetActive(false);
                currentCanvasIndex++;
                ActivateCanvas(currentCanvasIndex);
                isWaitingForChangeState = false;
            }
        }
    }

    // Aktiviert das Canvas und setzt den Button für die "Weiter"-Aktion
    void ActivateCanvas(int index)
    {
        canvasDataArray[index].canvas.gameObject.SetActive(true);
        if (canvasDataArray[index].weiterButton != null)
        {
            canvasDataArray[index].weiterButton.onClick.RemoveAllListeners(); 
            canvasDataArray[index].weiterButton.onClick.AddListener(OnWeiterButtonClicked);
        }
        if (index == 8)
        {
            isWaitingForChangeState = true;
        }
    }

    // Wird aufgerufen, wenn der "Weiter"-Button geklickt wird, um das nächste Canvas anzuzeigen oder die Szene zu wechseln
    void OnWeiterButtonClicked()
    {
        if (currentCanvasIndex != 8)
        {
            canvasDataArray[currentCanvasIndex].canvas.gameObject.SetActive(false);
            currentCanvasIndex++;

            if (currentCanvasIndex < canvasDataArray.Length)
            {
                ActivateCanvas(currentCanvasIndex);
            }
            else
            {
                if (!string.IsNullOrEmpty(nextSceneName))
                {
                    SceneManager.LoadScene("Menu");
                }
                else
                {
                    Debug.LogWarning("Kein Szenenname angegeben. Bitte f�ge den Namen der n�chsten Szene im Inspector hinzu.");
                }
            }
        }
    }
}
