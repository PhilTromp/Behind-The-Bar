using UnityEngine;


// Dieses Skript sorgt dafür, dass Objekte wie Flaschen oder Werkzeuge  
// automatisch an ihren ursprünglichen Startpunkt zurückkehren, wenn sie auf den Boden fallen.  
// Das hilft dem Nutzer, verlorene Gegenstände schnell wiederzufinden  
// und macht das Spiel komfortabler.  

public class RespawnObject : MonoBehaviour
{
    private Vector3 initialPosition;   // Speichert die ursprüngliche Position
    private Quaternion initialRotation; // Speichert die ursprüngliche Rotation
    private Rigidbody rb;              // Referenz zum Rigidbody

    void Start()
    {
        // Speichert die Ausgangsposition und Rotation des Objekts
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Prüft, ob das Objekt den Boden berührt
        if (collision.gameObject.CompareTag("Floor")) 
        {
            ResetPosition(); // Setzt das Objekt zurück
        }
    }

    // Mehtode zum Zurücksetzen der Objekte 
    void ResetPosition()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        // Die Geschwindigkeit muss vom Rigidbody ebenfalls zurückgesetzt werden 
        // Stellt sicher, dass das Objekt nicht weiterfällt
        if (rb != null)
        {
            rb.velocity = Vector3.zero;         
            rb.angularVelocity = Vector3.zero;  
        }
    }
}
