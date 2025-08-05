using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// Die Shaker-Klasse repräsentiert einen Shaker, der Flüssigkeiten aufnehmen, schütteln und an einen Behälter (wie ein Glas) abgeben kann.
// Der Shaker überwacht, ob der Deckel auf dem Behälter sitzt und ob er geschüttelt wird. Wenn er geschüttelt wird, 
// können die Flüssigkeiten im Shaker miteinander vermischt werden. Die Flüssigkeit kann auch von Flaschen oder Jiggern aufgenommen 
// und in den Shaker überführt werden. Einmal geschüttelt, können die Flüssigkeiten an einen Behälter übergeben werden,
// wobei auch spezielle Zutaten wie Zucker oder Gewürze berücksichtigt werden.
public class Shaker : MonoBehaviour
{
    private Dictionary<string, int> tagCounters; // Speichert die Tags und deren Mengen
    public int füllMengeMax = 400; // Maximale Füllmenge des Shakers (in ml)
    public int füllMenge = 0; // Aktuelle Füllmenge des Shakers (in ml)

    // Füllstand Visualisierung
    public GameObject flüssigkeitObject; // Flüssigkeitsvisualisierung
    public float startFill;
    public float currentFill; 
    public float maxFill; 

    // Booleans wie im Behälter-Skript
    public bool istBraunerZucker = false;
    public bool istWeißerZucker = false;
    public bool istSalz = false;
    public bool istPfeffer = false;
    public bool istTabasco = false;
    public bool istWorcestersauce = false;
    public bool istBarlöffel = false;

    public bool istMixer = false;

    // Tags, die Flüssigkeiten und Objekte hinzuzufügen
    private readonly HashSet<string> booleanTags = new HashSet<string>
    {
        "Brauner Zucker", "Weißer Zucker", "Salz", "Pfeffer", "Tabasco", "Worcestersauce", "Mixer"
    };

    private readonly HashSet<string> füllMengeTags = new HashSet<string>
    {
        "Vodka", "Korn", "Rum", "Whiskey", "Rye Whiskey", 
        "Tequila", "Gin", "Campari", "roter Wermut", 
        "Pfirsich Likör", "Cachaca", "Cointreau", "Energydrink", 
        "Cola", "Zitronensaft", "Limettensaft", "Orangensaft", 
        "Cranberry Direktsaft", "Soda", "Tomatensaft", "Zuckersirup"
    };

    // Zielbehälter, mit dem der Shaker in Kontakt tritt
    private GameObject zielBehälter;
    private Transform parentTransform; // Referenz zum Mutterobjekt
    private Vector3 lastParentPosition; // Letzte bekannte Position des Mutterobjekts
    private Vector3 lastVelocity = Vector3.zero; // Letzte Geschwindigkeit des Mutterobjekts

    private float shakeSensitivity = 0.01f; // Empfindlichkeit für Schüttelerkennung
    private float shakeTimer = 0f; // Timer für Schüttelzeit
    private float requiredShakeTime = 10f; // Benötigte Schüttelzeit in Sekunden

    // Referenz zum DeckelSocketInteraction-Skript
    public DeckelSocketInteraction deckelSocketInteraction;

    // Sound
    private AudioSource shakeSound;
    public AudioClip shakingClip; 
    private AudioSource umfüllenSound;
    public AudioClip umfüllenClip; 
    // Flag zur Verhinderung, dass der Umfüllen-Sound mehrfach abgespielt wird
    private bool umfüllenSoundAbgespielt = false;

    void Start()
    {
        tagCounters = new Dictionary<string, int>();
        parentTransform = transform.parent; 
        if (parentTransform != null)
        {
            lastParentPosition = parentTransform.position; 
        }
        else
        {
            Debug.LogError("Mutterobjekt ist nicht zugewiesen. Das Skript benötigt ein übergeordnetes Objekt.");
        }
        // AudioSource hinzufügen für Schütteln
        shakeSound = gameObject.AddComponent<AudioSource>();
        shakeSound.clip = shakingClip;
        shakeSound.loop = true; // Loop aktivieren
        shakeSound.playOnAwake = false;

        // AudioSource für Umfüllen-Sound hinzufügen
        umfüllenSound = gameObject.AddComponent<AudioSource>();
        umfüllenSound.clip = umfüllenClip;
        umfüllenSound.playOnAwake = false;
    }

    void Update()
    {
        if (parentTransform != null)
        {
            // Nur schütteln, wenn der Deckel auf dem Behälter ist
            if (deckelSocketInteraction != null && deckelSocketInteraction.deckelAufBehälter)
            {
                if (füllMenge > 0 && IsShaking() && istMixer == false)
                {
                    shakeTimer += Time.deltaTime;


                    // Sound starten
                     if (!shakeSound.isPlaying)
                    {
                        shakeSound.Play(); 
                    }

                    if (shakeTimer >= requiredShakeTime)
                    {
                        istMixer = true;
                        Debug.Log("Shaker wurde erfolgreich geschüttelt. Mixer aktiviert!");
                    }
                }
                else
                {
                    shakeTimer = 0f; // Timer zurücksetzen, wenn keine Bewegung erkannt wird

                    // Sound stoppen 
                    if (shakeSound.isPlaying)
                    {
                        shakeSound.Stop(); 
                    }


                }
            }
        }
        // Rücksetzen des Umfüllen-Sounds, wenn die Füllmenge auf 0 sinkt
        if (füllMenge == 0 && umfüllenSoundAbgespielt)
        {
            umfüllenSoundAbgespielt = false;
        }
    }

    private bool IsShaking()
    {
        // Berechnung der aktuellen Bewegung basierend auf Position und Geschwindigkeit
        Vector3 currentPosition = parentTransform.position;
        Vector3 velocity = (currentPosition - lastParentPosition) / Time.deltaTime;

        // Berechnung der Beschleunigung
        Vector3 acceleration = (velocity - lastVelocity) / Time.deltaTime;
        lastVelocity = velocity; // Aktuelle Geschwindigkeit speichern

        // Aktualisiere die letzte Position
        lastParentPosition = currentPosition;

        // Überprüfe, ob die Beschleunigung einen bestimmten Schwellenwert überschreitet
        return acceleration.magnitude > shakeSensitivity;
    }

    void OnTriggerEnter(Collider other)
    {
        // Wenn der Shaker mit einem "Glas" in Kontakt tritt und der Deckel nicht auf dem Behälter ist
        if (deckelSocketInteraction != null && !deckelSocketInteraction.deckelAufBehälter)
        {
            if (other.CompareTag("Glas") || other.CompareTag("Tumbler Glas") || other.CompareTag("Longdrink Glas") ||
                other.CompareTag("Hurricane Glas") || other.CompareTag("Coupette Glas") || other.CompareTag("Cocktail Glas"))
            {
                zielBehälter = other.gameObject; // Zielbehälter setzen
                Behälter behälterScript = zielBehälter.GetComponent<Behälter>();
                if (behälterScript != null && füllMenge > 0)
                {
                    // Umfüllen-Sound abspielen
                    if (!umfüllenSoundAbgespielt)
                    {
                        umfüllenSound.Play();
                        umfüllenSoundAbgespielt = true; // Verhindere weiteres Abspielen des Sounds
                    }

                    ÜbertrageInhaltAnBehälter(behälterScript); // Inhalt an den Behälter übertragen
                }
            }

            // Wenn das Objekt ein Tag aus der füllMengeTags-Liste hat
            if (füllMengeTags.Contains(other.tag))
            {
                if (füllMenge + 5 <= füllMengeMax)
                {
                    if (!tagCounters.ContainsKey(other.tag))
                    {
                        tagCounters[other.tag] = 0;
                    }

                    tagCounters[other.tag]++;
                    füllMenge += 5; // Ein Tropfen = 5ml
                    UpdateFill(); // Füllstand aktualisieren
                    Debug.Log($"{other.tag}: {tagCounters[other.tag] * 5} ml hinzugefügt.");
                    Destroy(other.gameObject); // Zerstöre das Objekt nach dem Hinzufügen
                }
            }
            // Booleans setzen
            else if (booleanTags.Contains(other.tag))
            {
                SetBooleanForTag(other.tag);
                Destroy(other.gameObject); // Zerstöre das Objekt nach dem Setzen des Booleans
            }
        }
    }

    // Methode, um Flüssigkeit zu empfangen vom Jigger
    public void ErhalteInhalt(string inhaltTag, int menge)
    {
        
        // Überprüfe, ob das Tag für Flüssigkeit existiert
        if (füllMengeTags.Contains(inhaltTag))
        {
            // Falls das Tag bereits vorhanden ist, erhöhe die Menge
            if (tagCounters.ContainsKey(inhaltTag))
            {
                tagCounters[inhaltTag] += menge;
            }
            else
            {
                // Falls das Tag noch nicht existiert, füge es hinzu
                tagCounters[inhaltTag] = menge;
            }

            // Aktualisiere die Füllmenge des Shakers
            füllMenge += menge * 5; // 1 Tropfenprefab = 5ml Flüssigkeit 
            if (füllMenge > füllMengeMax)
            {
                füllMenge = füllMengeMax; // Stelle sicher, dass die Füllmenge nicht überläuft
            }
            UpdateFill(); // Füllstand aktualisieren

            Debug.Log($"{inhaltTag}: {menge} ml erhalten. Aktuelle Füllmenge: {füllMenge * 5} ml.");
        }
        else
        {
            Debug.LogError("Ungültiges Tag für Flüssigkeit empfangen: " + inhaltTag);
        }
    }

    public void UpdateFill()
    {
        currentFill = Mathf.Lerp(startFill, maxFill, (float)füllMenge / füllMengeMax);

        if (flüssigkeitObject != null)
        {
            flüssigkeitObject.GetComponent<Renderer>().material.SetFloat("_Fill", currentFill);
        }
    }

    void SetBooleanForTag(string tag)
    {
        switch (tag)
        {
            case "Brauner Zucker":
                istBraunerZucker = true;
                break;
            case "Weißer Zucker":
                istWeißerZucker = true;
                break;
            case "Salz":
                istSalz = true;
                break;
            case "Pfeffer":
                istPfeffer = true;
                break;
            case "Tabasco":
                istTabasco = true;
                break;
            case "Worcestersauce":
                istWorcestersauce = true;
                break;
            case "Mixer":
                istMixer = true;
                break;
        }
    }

    public Dictionary<string, int> GetTagCounters()
    {
        return tagCounters;
    }

    // Methode, um den Inhalt an den Behälter zu übertragen
    void ÜbertrageInhaltAnBehälter(Behälter behälterScript)
    {
        int übertrageneMenge = füllMenge;
        int behälterMaxFüllMenge = behälterScript.füllMengeMax;

        // Wenn die Menge im Shaker den Behälter überschreiten würde, passen wir das Verhältnis an
        if (übertrageneMenge > behälterMaxFüllMenge)
        {
            float übertragungsVerhältnis = (float)behälterMaxFüllMenge / übertrageneMenge;

            // Reduziere die Menge jeder Flüssigkeit basierend auf dem Verhältnis
            foreach (var tag in tagCounters.Keys.ToList())
            {
                tagCounters[tag] = Mathf.RoundToInt(tagCounters[tag] * übertragungsVerhältnis);
            }

            // Setze die Füllmenge des Shakers auf die maximale Menge des Behälters
            übertrageneMenge = behälterMaxFüllMenge;
        }

        // Übertrage die Flüssigkeits-Tags in den Behälter
        foreach (var tag in tagCounters)
        {
            behälterScript.ErhalteInhalt(tag.Key, tag.Value);
        }

        // Übertrage den Mixer-Status
        behälterScript.SetIstMixer(istMixer);
        // Übertrage der restlichen Booleans
        behälterScript.SetIstBraunerZucker(istBraunerZucker);
        behälterScript.SetIstWeißerZucker(istWeißerZucker);
        behälterScript.SetIstSalz(istSalz);
        behälterScript.SetIstPfeffer(istPfeffer);
        behälterScript.SetIstTabasco(istTabasco);
        behälterScript.SetIstWorcestersauce(istWorcestersauce);

        Debug.Log("Inhalt vom Shaker an den Behälter übertragen.");

        // Shaker-Inhalt zurücksetzen
        tagCounters.Clear();
        istMixer = false;
        istBraunerZucker = false;
        istWeißerZucker = false;
        istSalz = false;
        istPfeffer = false;
        istTabasco = false;
        istWorcestersauce = false;
        füllMenge = 0;
        UpdateFill();
    }
}
