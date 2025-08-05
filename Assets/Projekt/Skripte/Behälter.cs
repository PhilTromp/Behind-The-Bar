using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


// Dieses Skript verwaltet einen Behälter (z. B. ein Glas) und speichert sowohl Flüssigkeiten als auch Dekorationen,
// die hinzugefügt werden. Es kontrolliert auch, ob bestimmte Zutaten, wie Zucker oder Eiswürfel, im Behälter sind.
// Zusätzlich wird der Status von Booleans für verschiedene Zutaten gesetzt und die Füllmenge wird angepasst.

public class Behälter : MonoBehaviour
{
    private Dictionary<string, int> tagCounters; // Dictionary für verschiedene Tags und ihre Zähler
    
    // Ein Tropfen = 5ml;
    public int füllMengeMax = 350; // Maximale Füllmenge des Behälters (in ml)
    public int füllMenge = 0; // Aktuelle Füllmenge des Behälters (in ml)

    // Füllstand Visualisierung
    public GameObject flüssigkeitInhalt; // Das GameObject für die Füllstandsanzeige
    public float startFill;
    public float currentFill; 
    public float maxFill; 


    private bool nurEinmal = false;

    // Liste der Tags, die nur Booleans setzen
    private readonly HashSet<string> booleanTags = new HashSet<string>
    {
        "Brauner Zucker", "Weißer Zucker", "Salz", "Pfeffer", "Eiswürfel", 
        "Crushed Ice", "Tabasco", "Worcestersauce", "Zitrone", "Limetten", "Minzblatt", "Orange", 
        "Cocktailkirschen", "Annanas", "Strohhalm", "Sellerie", "Barlöffel", "Stößel", "Mixer",

    };

    // Liste der Tags, die zur Füllmenge beitragen
    private readonly HashSet<string> füllMengeTags = new HashSet<string>
    {
        "Vodka", "Korn", "Rum", "Whisky", "Rye Whisky", 
        "Tequila", "Gin", "Campari", "Roter Wermut", 
        "Pfirsich Likör", "Cachaca", "Cointreau", "Energydrink", 
        "Cola", "Zitronensaft", "Limettensaft", "Orangensaft", 
        "Cranberry Direktsaft", "Soda", "Tomatensaft", "Zuckersirup"
    };

    // Booleans für spezifische Tags
    public bool istBraunerZucker = false;
    public bool istBraunerZucker2 = false;
    public bool istWeißerZucker = false;
    public bool istWeißerZucker2 = false;
    public bool istSalz = false;
    public bool istPfeffer = false;
    public bool istEiswürfel = false;
    public bool istCrushedIce = false;
    public bool istTabasco = false;
    public bool istWorcestersauce = false;
    public bool istBarlöffel = false;
    public bool istStößel = false;
    public bool istMixer = false;
    public bool istZitrone = false;
    public bool istLimetten = false;
    public bool istMinzblatt = false;
    public bool istOrange = false;
    public bool istCocktailkirschen = false;
    public bool istAnnanas = false;
    public bool istStrohhalm = false;
    public bool istSellerie = false;

    // Prefab für Eiswürfel und Crushed Ice
    public GameObject eisPrefab; // Prefab für Eiswürfel
    public GameObject crushedIcePrefab; // Prefab für Crushed Ice

    // Öffentlicher Transform-Wert für die Spawn-Position
    public Transform spawnPoint;
    // Prefabs für Dekorationen
    public GameObject zitronePrefab;
    public GameObject limettePrefab;
    public GameObject minzblattPrefab;
    public GameObject orangePrefab;
    public GameObject cocktailkirschenPrefab;
    public GameObject annanasPrefab;
    public GameObject strohhalmPrefab;
    public GameObject selleriePrefab;

    // Spawnpunkte für Dekorationen
    public Transform scheibenSpawnPoint;
    public Transform limettenSpawnPoint; // Für Caipirinham, da es hier nicht als deko sondern als zutat dienen soll = es wird mti dem Stößel verarbeitet darum muss der spawnpoint drinne sein = nur für Tumbler Glas 
    public Transform minzblattSpawnPoint;
    public Transform strohhalmSpawnPoint;
    public Transform cocktailkirschenSpawnPoint;
    public Transform sellerieSpawnPoint;

    private HashSet<GameObject> pendingObjects = new HashSet<GameObject>();
    private Dictionary<GameObject, Coroutine> activeCoroutines = new Dictionary<GameObject, Coroutine>();

    void Start()
    {
        tagCounters = new Dictionary<string, int>();
    }

    void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;

        if (!string.IsNullOrEmpty(tag))
        {


            // Für booleanTags: Warten, bevor der Boolean gesetzt wird
            if (tag == "Barlöffel" || tag == "Stößel")
            {
                if (!activeCoroutines.ContainsKey(other.gameObject))
                {
                    Coroutine coroutine = StartCoroutine(WaitBeforeSetBoolean(tag, other.gameObject));
                    activeCoroutines[other.gameObject] = coroutine;
                }
            }
            // Für füllMengeTags: Erhöhe die Füllmenge
            else if (füllMengeTags.Contains(tag))
            {
                if (füllMenge + 5 <= füllMengeMax)
                {
                    if (!tagCounters.ContainsKey(tag))
                    {
                        tagCounters[tag] = 0;
                    }

                    tagCounters[tag]++;
                    füllMenge += 5; // Ein Tropfen = 5ml
                    UpdateFill(); // Füllstand anpassen
                    Debug.Log($"{tag}: {tagCounters[tag] * 5} ml hinzugefügt.");
                }
                Destroy(other.gameObject);
            }
             // Für Eiswürfel oder Crushed Ice: Spawn das entsprechende Prefab
            else if (tag == "Eiswürfel" || tag == "Crushed Ice")
            {
                SpawnPrefab(tag == "Eiswürfel" ? eisPrefab : crushedIcePrefab, spawnPoint);
                Destroy(other.gameObject);

                 // Setzt Booleans für Eiswürfel und Crushed Ice
                if (tag == "Eiswürfel")
                {
                    SetBooleanForTag("Eiswürfel");
                    Debug.Log("Eiswürfel sind im Behälter!");
                    Debug.Log($"Boolean für {tag} gesetzt.");
                }
                else if (tag == "Crushed Ice")
                {
                    SetBooleanForTag("Crushed Ice");
                    Debug.Log("Crushed Ice sind im Behälter!");
                    Debug.Log($"Boolean für {tag} gesetzt.");
                }
            }
            // Für Dekorationen
            else if (tag == "Zitrone")
            {
                SpawnPrefab(zitronePrefab, scheibenSpawnPoint);
                SetBooleanForTag("Zitrone");
                Debug.Log($"Boolean für {tag} gesetzt.");
                Destroy(other.gameObject);
            }
            else if (tag == "Limetten")
            {
                SpawnPrefab(limettePrefab, limettenSpawnPoint); //nur wichtig für Caipirinha (kein scheibenSpawnpoint sondern für diesen drinkt limettenSpawnpoint)
                SetBooleanForTag("Limetten");
                Debug.Log($"Boolean für {tag} gesetzt.");
                Destroy(other.gameObject);
            }
            else if (tag == "Minzblatt")
            {
                SpawnPrefab(minzblattPrefab, minzblattSpawnPoint);
                SetBooleanForTag("Minzblatt");
                Debug.Log($"Boolean für {tag} gesetzt.");
                Destroy(other.gameObject);
            }
            else if (tag == "Orange")
            {
                SpawnPrefab(orangePrefab, scheibenSpawnPoint);
                SetBooleanForTag("Orange");
                Debug.Log($"Boolean für {tag} gesetzt.");
                Destroy(other.gameObject);
            }
            else if (tag == "Cocktailkirschen")
            {
                SpawnPrefab(cocktailkirschenPrefab, cocktailkirschenSpawnPoint);
                SetBooleanForTag("Cocktailkirschen");
                Debug.Log($"Boolean für {tag} gesetzt.");
                Destroy(other.gameObject);
            }
            else if (tag == "Annanas")
            {
                SpawnPrefab(annanasPrefab, scheibenSpawnPoint);
                SetBooleanForTag("Annanas");
                Debug.Log($"Boolean für {tag} gesetzt.");
                Destroy(other.gameObject);
            }
            else if (tag == "Strohhalm")
            {
                SpawnPrefab(strohhalmPrefab, strohhalmSpawnPoint);
                SetBooleanForTag("Strohhalm");
                Debug.Log($"Boolean für {tag} gesetzt.");
                Destroy(other.gameObject);
            }
            else if (tag == "Sellerie")
            {
                SpawnPrefab(selleriePrefab, sellerieSpawnPoint);
                SetBooleanForTag("Sellerie");
                Debug.Log($"Boolean für {tag} gesetzt.");
                Destroy(other.gameObject);
            }
            else if (booleanTags.Contains(tag))
            {
                SetBooleanForTag(tag);
                Destroy(other.gameObject);
                Debug.Log($"Boolean für {tag} gesetzt.");
            }
            else
            {
                Debug.LogWarning($"Tag '{tag}' ist nicht erlaubt.");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        string tag = other.tag;

        if (tag == "Barlöffel" || tag == "Stößel")
        {
            if (activeCoroutines.ContainsKey(other.gameObject))
            {
                StopCoroutine(activeCoroutines[other.gameObject]);
                activeCoroutines.Remove(other.gameObject);
                Debug.Log($"Abbruch: {tag} hat das Trigger-Feld verlassen.");
            }
        }
    }

    IEnumerator WaitBeforeSetBoolean(string tag, GameObject obj)
    {
        Debug.Log($"Wartezeit gestartet für {tag}...");
        float elapsedTime = 0f;

        while (elapsedTime < 5f)
        {
            if (!obj)
            {
                yield break; 
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SetBooleanForTag(tag);
        Debug.Log($"Boolean für {tag} nach 5 Sekunden gesetzt.");
        activeCoroutines.Remove(obj);
    }

    void SpawnPrefab(GameObject prefab, Transform spawnPoint)
    {
        if (prefab == null || spawnPoint == null)
        {
            Debug.LogError("Prefab oder Spawnpunkt ist null!");
            return;
        }
        GameObject spawnedObject = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation, spawnPoint);
        Debug.Log($"Prefab {prefab.name} wurde an {spawnPoint.position} gespawnt.");
    }
    
    void Update()
    {
        if (füllMenge >= füllMengeMax && !nurEinmal)
        {
            Debug.Log("Das Glas ist voll");
            nurEinmal = true;
        }

        // Aktualisiert Füllstand im Material (wird durch UpdateFill() aufgerufen)
        if (flüssigkeitInhalt != null)
        {
            flüssigkeitInhalt.GetComponent<Renderer>().material.SetFloat("_Fill", currentFill);
        }
    }

    // Methode, um den Füllstand basierend auf der aktuellen Füllmenge zu aktualisieren
    public void UpdateFill()
    {
        // Berechne den neuen Füllstand als lineare Transformation startFill und füllmengeMax
        currentFill = Mathf.Lerp(startFill, maxFill, (float)füllMenge / füllMengeMax);
    }
    

    

    // Setzt die entsprechenden Booleans basierend auf dem Tag
    void SetBooleanForTag(string tag)
    {
        switch (tag)
        {
            case "Brauner Zucker":
                //istBraunerZucker = true;
                if (istBraunerZucker)
                    istBraunerZucker2 = true;
                else
                    istBraunerZucker = true;
                break;
            case "Weißer Zucker":
                //istWeißerZucker = true;
                if (istWeißerZucker)
                    istWeißerZucker2 = true;
                else
                    istWeißerZucker = true;
                break;
            case "Salz":
                istSalz = true;
                break;
            case "Pfeffer":
                istPfeffer = true;
                break;
            case "Eiswürfel":
                istEiswürfel = true;
                break;
            case "Crushed Ice":
                istCrushedIce = true;
                break;
            case "Tabasco":
                istTabasco = true;
                break;
            case "Worcestersauce":
                istWorcestersauce = true;
                break;
            case "Barlöffel":
                istBarlöffel = true;
                break;
            case "Stößel":
                istStößel = true;
                break;
            case "Zitrone":
                istZitrone = true;
                break;
            case "Limetten":
                istLimetten = true;
                break;
            case "Minzblatt":
                istMinzblatt = true;
                break;
            case "Orange":
                istOrange = true;
                break;
            case "Cocktailkirschen":
                istCocktailkirschen = true;
                break;
            case "Annanas":
                istAnnanas = true;
                break;
            case "Strohhalm":
                istStrohhalm = true;
                break;
            case "Sellerie":
                istSellerie = true;
                break;
        }

        

    }

    public Dictionary<string, int> GetTagCounters()
    {
        return tagCounters;
    }



public void SetzeBooleans(bool eiswürfel, bool crushedIce)
{
    istEiswürfel = eiswürfel;
    istCrushedIce = crushedIce;
    Debug.Log($"Booleans gesetzt: Eiswürfel = {istEiswürfel}, Crushed Ice = {istCrushedIce}");
}



// Methode, um Flüssigkeit zu empfangen vom Jigger
    public void ErhalteInhalt(string tag, int menge)
{
    if (füllMenge + menge <= füllMengeMax)
    {
        if (!tagCounters.ContainsKey(tag))
        {
            tagCounters[tag] = 0;
        }
        tagCounters[tag] += menge;
        füllMenge += menge * 5;  // Hier musste ich mit 5 multiplizieren damit beim Übertragen vom Shaker zu Behälter die grafische Darstellung von der Flüssigkeit richtig funktioniert
        UpdateFill();
        Debug.Log($"{tag}: {menge * 5} ml in den Behälter hinzugefügt.");
    }
    else
    {
        Debug.LogWarning("Behälter ist voll! Flüssigkeit kann nicht hinzugefügt werden.");
    }
}




public void SetIstMixer(bool mixerStatus)
{
    istMixer = mixerStatus;
    Debug.Log($"Mixer-Status im Behälter gesetzt: {istMixer}");
}
public void SetIstBraunerZucker(bool value) { istBraunerZucker = value; }
public void SetIstWeißerZucker(bool value) { istWeißerZucker = value; }
public void SetIstSalz(bool value) { istSalz = value; }
public void SetIstPfeffer(bool value) { istPfeffer = value; }
public void SetIstTabasco(bool value) { istTabasco = value; }
public void SetIstWorcestersauce(bool value) { istWorcestersauce = value; }

}



