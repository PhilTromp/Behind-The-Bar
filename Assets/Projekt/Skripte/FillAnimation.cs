using UnityEngine;


// Diese Klasse steuert die Animation des Flüssigkeitseinfüllens,  
// indem sie Tropfen generiert, wenn ein Objekt mit diesem Script über 150 Grad gekippt wird.  

public class FillAnimation : MonoBehaviour
{
    public GameObject tropfenPrefab; // Tropfenprefab Objekte die gespawnt werden
    public GameObject[] tropfenPrefabs; // unterschiedliche Formen von Tropfen in einer Liste 
    public Transform parentObject; // Das Skript ist an einem Kindobjekt befestigt (Position der Flaschenöffnung) und die Flasche ist das übergeordnete Objekt.
    public float tiltThreshold = 150f; // Winkel, ab wann Flüssigkeit rausgekippt wird 
    public float spawnRate = 10f;
    public float tropfenSpeed = 5f;
    public float tropfenSize = 0.005f;
    public string flüssigkeitTag; // Die Tropfen erhalten einen bestimmten Tag, der im Inspektor gesetzt wird, damit die Behälter-Klasse weiß, welche Art von Flüssigkeit sie speichern soll
    public Vector3 spawnPoint = new Vector3(0, 0, 0); // Punkt, wo die Tropfen spawnen sollen 
    private float spawnTimer = 0f;
    private bool isSpawning = false; // Ob Tropfen gerade gespawnt werden


    // Zusätzlich wird ein Tropfensound abgespielt, solange Flüssigkeit austritt.  
    public AudioSource audioSource; 
    public AudioClip tropfenSound;  

    

    void Start() {
        tropfenSize = 0.005f;
 

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }  
    }

    void Update()
    {
        float tiltAngle = GetTiltAngle();

        if (IsTilted())
        {
            spawnTimer += Time.deltaTime;
            while (spawnTimer >= 1f / spawnRate)
            {
                SpawnTropfen();
                spawnTimer -= 1f / spawnRate;
            }

            // Überprüfen, ob Tropfen gespawnt werden und Sound abspielen
            if (!isSpawning)
            {
                StartTropfenSound(); // Startet den Tropfensound im Loop, solange die FLasche über 150 Grad gekippt ist / Wenn Tropfen Spawnen
                isSpawning = true;
            }
        }
        else if (isSpawning)
        {
            // Stoppt den Sound, wenn keine Tropfen mehr gespawnt werden
            StopTropfenSound(); // Stoppt den Tropfensound, unter 150 Grad / Wenn Tropfen nicht mehr spawnen 
            isSpawning = false;
        }
    }

    void SpawnTropfen()
    {
        // Wähle ein zufälliges Prefab aus dem Array
        if (tropfenPrefabs != null && tropfenPrefabs.Length > 0)
        {
            int randomIndex = Random.Range(0, tropfenPrefabs.Length);
            tropfenPrefab = tropfenPrefabs[randomIndex];
        }

        // Spawnen der Tropfen / Flüssigkeit 
        Vector3 fixedSpawnPosition = transform.TransformPoint(spawnPoint);
        GameObject tropfen = Instantiate(tropfenPrefab, fixedSpawnPosition, Quaternion.identity);
        tropfen.transform.localScale = Vector3.one * tropfenSize;

        tropfen.tag = flüssigkeitTag;

        Rigidbody rb = tropfen.AddComponent<Rigidbody>();
        Vector3 upwardDirection = transform.up;
        rb.velocity = upwardDirection * tropfenSpeed;

        Destroy(tropfen, 1f); // Tropfen, die nicht im Behälter landen, sollen gelöscht werden nach 1 Sekunde 
    }



    // Überprüft, ob die Flasche in einem bestimmten Winkel geneigt ist, um Tropfen zu erzeugen.
    bool IsTilted()
    {
        float tiltAngle = GetTiltAngle();
        return tiltAngle > tiltThreshold;
    }

    float GetTiltAngle()
    {
        float angle = Mathf.Abs(parentObject.localRotation.eulerAngles.z);
        if (angle > 180f)
        {
            angle -= 360f;
        }
        return Mathf.Abs(angle);
    }



    
    //Tropfensound im Loop
    void StartTropfenSound()
    {
        if (audioSource != null && tropfenSound != null)
        {
            audioSource.clip = tropfenSound;
            audioSource.loop = true;
            audioSource.Play();
            Debug.Log("Tropfensound gestartet und läuft im Loop.");
        }
    }

    // Stoppt den Tropfensound
    void StopTropfenSound()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
            Debug.Log("Tropfensound gestoppt.");
        }
    }
}