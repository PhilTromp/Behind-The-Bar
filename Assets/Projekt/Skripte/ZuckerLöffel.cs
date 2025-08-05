using UnityEngine;


// Dieses Skript verwaltet die Interaktion eines Teelöffels mit Schüsseln für braunen und weißen Zucker. 
// Wenn der Löffel mit einer Schüssel kollidiert, wird der entsprechende Zucker (weiß oder braun) auf dem Löffel gespawnt. 
// Der Zucker kann später auf einen Behälter oder Shaker abgegeben werden.

public class ZuckerLöffel : MonoBehaviour
{
    [Header("Einstellungen")]
    public GameObject whiteSugarPrefab;  // Prefab für weißen Zucker
    public GameObject brownSugarPrefab;  // Prefab für braunen Zucker
    public Transform sugarSpawnPoint;    // Wo der Zucker auf dem Löffel erscheint
    
    private GameObject currentSugar;     // Aktuell gespawnter Zucker
    private Rigidbody sugarRb;
    private string currentSugarType = ""; // Speichert, welche Zuckerart auf dem Löffel ist

    private void Start()
    {
        RemoveSugar(); // Startet ohne Zucker
    }

    private void OnTriggerEnter(Collider other)
    {
        // Zucker hinzufügen, je nachdem welche Schüssel getroffen wird
        if (other.CompareTag("Schüssel Weißen Zucker"))
        {
            AddSugar(whiteSugarPrefab, "WhiteSugar");
        }
        else if (other.CompareTag("Schüssel Brauner Zucker"))
        {
            AddSugar(brownSugarPrefab, "BrownSugar");
        }

        // Wenn der Löffel in den Behälter kommt -> Zucker loslassen
        if (other.CompareTag("Shaker") || other.CompareTag("Longdrink Glas") || other.CompareTag("Coupette Glas") || other.CompareTag("Hurricnae Glas") || other.CompareTag("Cocktail Glas") || other.CompareTag("Tumbler Glas"))
        {
            DropSugar();
        }
    }

    void AddSugar(GameObject sugarPrefab, string sugarType)
    {
        // Nur Zucker spawnen, wenn kein anderer Zucker vorhanden ist oder es eine andere Zuckerart ist
        if (currentSugar == null || currentSugarType != sugarType)
        {
            RemoveSugar(); // Falls bereits anderer Zucker vorhanden ist, erst entfernen

            currentSugar = Instantiate(sugarPrefab, sugarSpawnPoint.position, sugarSpawnPoint.rotation, sugarSpawnPoint);
            currentSugarType = sugarType;

            // Falls das Zucker-Objekt keinen Rigidbody hat, fügen wir eins hinzu
            sugarRb = currentSugar.GetComponent<Rigidbody>();
            if (sugarRb == null)
            {
                sugarRb = currentSugar.AddComponent<Rigidbody>();
            }

            sugarRb.isKinematic = true; // Zucker bleibt am Löffel "kleben"
        }
    }

    void DropSugar()
    {
        if (currentSugar != null)
        {
            sugarRb.isKinematic = false; // Zucker wird losgelassen
            currentSugar.transform.parent = null; // Zucker wird vom Löffel gelöst
            currentSugar = null; // Vergisst das aktuelle Zucker-Objekt
            currentSugarType = ""; // Setzt den Zuckerstatus zurück
        }
    }

    void RemoveSugar()
    {
        if (currentSugar != null)
        {
            Destroy(currentSugar);
            currentSugar = null;
            currentSugarType = "";
        }
    }
}
