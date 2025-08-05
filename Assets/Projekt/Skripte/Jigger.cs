using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Die Jigger-Klasse repräsentiert ein Behälter-Objekt, das Flüssigkeiten aufnehmen und abgeben kann.
// Der Jigger hat eine bestimmte maximale Füllmenge und kann mit verschiedenen Flüssigkeiten befüllt werden,
// wie zum Beispiel Vodka, Rum, Zitronensaft, Cola und viele weitere.
// Wenn der Jigger eine Flüssigkeit aufnimmt, wird sie im Jigger gespeichert und kann bei Bedarf an einen anderen Behälter 
// (wie ein Glas oder Shaker) abgegeben werden. Bei der Übergabe wird ein Sound abgespielt und die Füllmenge wird 
// zurückgesetzt. Der Füllstand des Jiggers wird auch visuell angezeigt und dynamisch aktualisiert.

public class Jigger : MonoBehaviour
{
    public int füllMengeMax = 40;  // Maximale Füllmenge in ml
    public int füllMenge = 0;  // Aktuelle Füllmenge
    public string aktuelleFlüssigkeit = "";  // Aktuelle Flüssigkeit, die der Jogger hält (leer zu Beginn)

    public GameObject flüssigkeitObject;  // Das Objekt zur Visualisierung der Flüssigkeit
    public float startFill;
    public float currentFill; 
    public float maxFill; 

    // Referenz zum DeckelSocketInteraction-Skript. Damit kein e Flüssigkeit an den Shaker übergeben werden kann, falls der Deckel noch drauf ist.
    public DeckelSocketInteraction deckelSocketInteraction;


    // AudioSource für das Abspielen des Sounds
    public AudioSource audioSource; 
    public AudioClip soundClip;  
    // Flag zur Verhinderung, dass der Sound mehrfach abgespielt wird, wenn die Füllmenge > 0 ist
    private bool soundAbgespielt = false;



    // Liste der erlaubten Flüssigkeiten
    private readonly HashSet<string> erlaubteFlüssigkeiten = new HashSet<string>
    {
        "Vodka", "Korn", "Rum", "Whiskey", "Rye Whiskey", 
        "Tequila", "Gin", "Campari", "roter Wermut", 
        "Pfirsichlikör", "Cachaca", "Cointreau", "Energydrink", 
        "Cola", "Zitronensaft", "Limettensaft", "Orangensaft", 
        "Cranberry Direktsaft", "Soda", "Tomatensaft", "Zuckersirup"
    };

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }  
    }

    void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;

        // Flüssigkeit aufnehmen
        if (erlaubteFlüssigkeiten.Contains(tag) && füllMenge < füllMengeMax)
        {
            if (aktuelleFlüssigkeit == "" || aktuelleFlüssigkeit == tag)
            {
                füllMenge += 5;  // Ein Tropfen = 5 ml
                aktuelleFlüssigkeit = tag;  // Setzt die Flüssigkeit, die der Jigger hält
                UpdateFill();
                Debug.Log($"{tag} wurde aufgenommen. Aktuelle Füllmenge: {füllMenge} ml");
                Destroy(other.gameObject);
            }
            else
            {
                Debug.LogWarning("Der Jogger hält bereits eine andere Flüssigkeit.");
            }
        }
        // Flüssigkeit abgeben an einen Behälter oder Shaker
        else if (other.CompareTag("Glas")|| other.CompareTag("Tumbler Glas")|| other.CompareTag("Longdrink Glas") || other.CompareTag("Hurricane Glas") || other.CompareTag("Coupette Glas")|| other.CompareTag("Cocktail Glas"))
        {
            Behälter behälter = other.GetComponent<Behälter>();  
            


            if (behälter != null && füllMenge > 0)
            {



                // Vor dem Zurücksetzen der Füllmenge den Sound abspielen, wenn der Sound noch nicht abgespielt wurde
                if (!soundAbgespielt && audioSource != null && soundClip != null)
                {
                    audioSource.clip = soundClip;
                    audioSource.Play();
                    soundAbgespielt = true;  // Verhindere, dass der Sound wiederholt abgespielt wird
                }


                // Flüssigkeit an den Behälter übergeben
                behälter.ErhalteInhalt(aktuelleFlüssigkeit, füllMenge / 5);  // Flüssigkeit übergeben (5 ml pro Tropfen)
                Debug.Log($"Flüssigkeit an {other.tag} übergeben. Füllmenge: {füllMenge} ml");
                füllMenge = 0;  // Jigger zurücksetzen
                aktuelleFlüssigkeit = "";  // Jigger hat keine Flüssigkeit mehr
                UpdateFill();
            }
            
        }
        else if (other.CompareTag("Shaker"))
        {
            Shaker shaker = other.GetComponent<Shaker>();  // Holen des Shaker-Skripts
            if (deckelSocketInteraction != null && !deckelSocketInteraction.deckelAufBehälter) {
                
            


            if (shaker != null && füllMenge > 0)
            {



                // Vor dem Zurücksetzen der Füllmenge den Sound abspielen, wenn der Sound noch nicht abgespielt wurde
                if (!soundAbgespielt && audioSource != null && soundClip != null)
                {
                    audioSource.clip = soundClip;
                    audioSource.Play();
                    soundAbgespielt = true;  // Verhindere, dass der Sound wiederholt abgespielt wird
                }

                // Flüssigkeit an den Shaker übergeben
                shaker.ErhalteInhalt(aktuelleFlüssigkeit, füllMenge / 5);  // Flüssigkeit übergeben (5 ml pro Tropfen)
                Debug.Log($"Flüssigkeit an {other.tag} übergeben. Füllmenge: {füllMenge} ml");
                füllMenge = 0;  // Jigger zurücksetzen
                aktuelleFlüssigkeit = "";  // Jigger hat keine Flüssigkeit mehr
                UpdateFill();
            }
            
        }
        }
    }

    void Update()
    {
        if (flüssigkeitObject != null)
        {
            // Aktualisiere den Füllstand im Visualisierungsobjekt
            flüssigkeitObject.GetComponent<Renderer>().material.SetFloat("_Fill", currentFill);
        }

         // Wenn die Füllmenge zurück auf mehr als 0 geht, den Sound zurücksetzen und erneut abspielen können
        if (füllMenge > 0 && soundAbgespielt)
        {
            soundAbgespielt = false;  // Der Sound kann wieder abgespielt werden, wenn die Füllmenge größer als 0 ist
        }
    }

    // Methode, um den Füllstand zu aktualisieren
    public void UpdateFill()
    {
        // Berechne den neuen Füllstand
        currentFill = Mathf.Lerp(startFill, maxFill, (float)füllMenge / füllMengeMax);
    }
}
