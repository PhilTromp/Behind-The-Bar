using System.Collections;
using System.Collections.Generic;
using TMPro;  
using UnityEngine;


// Dieses Skript ermöglicht das automatische Entleeren von Behältern, Jiggern und Shakern, 
// wenn sie in den Bereich des Spülbeckens gelangen.  
// Es setzt alle Werte dieser Objekte zurück, entfernt hinzugefügte Inhalte und zeigt eine HUD-Nachricht an.  

public class Spülbecken : MonoBehaviour
{

    public DeckelSocketInteraction deckelSocketInteraction;
    public GameObject canvas;
    public TextMeshProUGUI textMeshPro;


    void OnTriggerEnter(Collider other)
    {
        // Prüft, ob das eintretende Objekt das Skript "Behälter" hat
        Behälter behälter = other.GetComponent<Behälter>();
        if (behälter != null)
        {
            ResetBehälter(behälter);
            Debug.Log("Behälter wurde im Spülbecken geleert und zurückgesetzt!");
            textMeshPro.text = "Behälter wurde im Spülbecken entleert";
            canvas.SetActive(true);
            StartCoroutine(DeaktiviereCanvasNachZeit(5f));
        }

        // Prüft, ob das eintretende Objekt das Skript "Shaker" hat
        Shaker shaker = other.GetComponent<Shaker>();
        if (shaker != null)
        {
            if (deckelSocketInteraction != null && !deckelSocketInteraction.deckelAufBehälter) {
                // Rufe die Methode auf, die alle Werte im Shaker zurücksetzt
                ResetShaker(shaker);
                Debug.Log("Shaker wurde im Spülbecken geleert und zurückgesetzt!");
                textMeshPro.text = "Shaker wurde im Spülbecken entleert";
                canvas.SetActive(true);
                StartCoroutine(DeaktiviereCanvasNachZeit(5f));
            }
        }

        // Prüft, ob das eintretende Objekt das Skript "Jigger" hat
        Jigger jigger = other.GetComponent<Jigger>();
        if (jigger != null)
        {
            // Rufe die Methode auf, die alle Werte im Jigger zurücksetzt
            ResetJigger(jigger);
            Debug.Log("Jigger wurde im Spülbecken geleert und zurückgesetzt!");
            textMeshPro.text = "Jigger wurde im Spülbecken entleert";
            canvas.SetActive(true);
            StartCoroutine(DeaktiviereCanvasNachZeit(5f));
        }
    }

    // Methode zum Zurücksetzen der Werte im Behälter
    private void ResetBehälter(Behälter behälter)
    {
        // Setzt alle Booleans auf false
        behälter.istBraunerZucker = false;
        behälter.istWeißerZucker = false;
        behälter.istSalz = false;
        behälter.istPfeffer = false;
        behälter.istEiswürfel = false;
        behälter.istCrushedIce = false;
        behälter.istTabasco = false;
        behälter.istWorcestersauce = false;
        behälter.istBarlöffel = false;
        behälter.istStößel = false;
        behälter.istMixer = false;
        behälter.istZitrone = false;
        behälter.istLimetten = false;
        behälter.istMinzblatt = false;
        behälter.istOrange = false;
        behälter.istCocktailkirschen = false;
        behälter.istAnnanas = false;
        behälter.istStrohhalm = false;
        behälter.istSellerie = false;

        behälter.füllMenge = 0; // Setzt die Füllmenge auf 0

        behälter.GetTagCounters().Clear();  // Setzt das Tag-Counter-Dictionary zurück

        
        behälter.UpdateFill(); // Aktualisiere den Füllstand Animation (Shader)

        Debug.Log("Alle Werte des Behälters wurden zurückgesetzt.");
        
        // Lösche alle Kindobjekte des Behälters (Für Deko usw.)
        foreach (Transform child in behälter.transform)
        {
            Destroy(child.gameObject); 
        }
    }

    // Methode zum Zurücksetzen der Werte im Shaker
    private void ResetShaker(Shaker shaker)
    {
        // Setze alle Booleans auf false
        shaker.istBraunerZucker = false;
        shaker.istWeißerZucker = false;
        shaker.istSalz = false;
        shaker.istPfeffer = false;
        shaker.istTabasco = false;
        shaker.istWorcestersauce = false;
        shaker.istMixer = false;

        shaker.füllMenge = 0;  // Setzt die Füllmenge des Shakers auf 0

        shaker.GetTagCounters().Clear(); // Setzt das Tag-Counter-Dictionary zurück

        shaker.UpdateFill(); // Aktualisiere den Füllstand Animation (Shader)

        Debug.Log("Alle Werte des Shakers wurden zurückgesetzt.");
        
        // Löscht alle Kindobjekte des Shakers
        foreach (Transform child in shaker.transform)
        {
            Destroy(child.gameObject); 
        }
    }

    private void ResetJigger(Jigger jigger)
    {
        // Setze die Füllmenge des Jiggers auf 0 & Flüssigkeitart entfernen
        jigger.füllMenge = 0;
        jigger.aktuelleFlüssigkeit = "";


        
        jigger.UpdateFill(); // Aktualisiere den Füllstand Animation (Shader)

        Debug.Log("Alle Werte des Jiggers wurden zurückgesetzt.");
        
        // Lösche alle Kindobjekte des Jiggers
        foreach (Transform child in jigger.transform)
        {
            Destroy(child.gameObject); // Löscht das Kindobjekt
        }
    }

    // Coroutine, die das Canvas (HUD) nach einer bestimmten Zeit deaktiviert
    IEnumerator DeaktiviereCanvasNachZeit(float zeit)
    {
        yield return new WaitForSeconds(zeit);
        canvas.SetActive(false);
    }
}