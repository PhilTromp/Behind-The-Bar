using UnityEngine;


// Dieses Skript spawnt ebenfalls Tropfen aus einer Flasche wieder basierend auf deren Neigungswinkel.
// Es funktioniert ähnlich wie "FillAnimation", aber angepasst für Tabasco und Worcestersauce.
// Überprüft, ob die Flasche in einem bestimmten Winkel geneigt ist, um Tropfen zu erzeugen.
// Wenn der Winkel im definierten Bereich ist, wird nur ein Tropfen erzeugt.
// Der nächste Tropfen kann nur wieder aus der Flasche fallen, wenn die Flasche einmal wieder zurück geklippt wird unter 160 Grad 
// Die Tropfen erhalten den `flüssigkeitTag`, um die Art der Flüssigkeit festzulegen.
// Wenn die Flasche in eine Ausgangsposition zurückkehrt, wird der Tropfen-Spawn zurückgesetzt.

public class Fillspices : MonoBehaviour
{
    [Header("Einstellungen")]
    public GameObject tropfenPrefab;
    public Transform parentObject; // Parent-Objekt bleibt erhalten
    public float triggerMinAngle = 160f; // Positiver Mindestwinkel
    public float triggerMaxAngle = 200f; // Positiver Maximalwinkel
    public float negTriggerMinAngle = -180f; // Negativer Mindestwinkel
    public float negTriggerMaxAngle = -160f; // Negativer Maximalwinkel
    public float resetAngle = 150f; // Positiver Reset-Winkel
    public float negResetAngle = -150f; // Negativer Reset-Winkel
    public Vector3 spawnOffset = new Vector3(0, 0, 0);
    public float tropfenSpeed = 5f;
    public float tropfenSize = 0.2f;
    public string flüssigkeitTag;

    [Header("Status")]
    public bool canSpawn = true;
    public float debugAngle;

    private bool wasTriggered = false;
    private float previousAngle;





    void Update()
    {
        float currentAngle = GetNormalizedAngle();
        debugAngle = currentAngle;

        // Überprüfe, ob der Winkel im positiven oder negativen Bereich liegt
        if (canSpawn && (IsAngleInRange(currentAngle, triggerMinAngle, triggerMaxAngle) ||
                         IsAngleInRange(currentAngle, negTriggerMinAngle, negTriggerMaxAngle)))
        {
            SpawnTropfen();
            canSpawn = false;
            wasTriggered = true;
        }

        // Reset, wenn der Winkel unter dem positiven oder negativen Reset-Winkel fällt
        if (wasTriggered && (currentAngle < resetAngle && currentAngle > negResetAngle))
        {
            canSpawn = true;
            wasTriggered = false;
        }

        previousAngle = currentAngle;
    }

    void SpawnTropfen()
    {
        Vector3 spawnPosition = transform.TransformPoint(spawnOffset);
        GameObject tropfen = Instantiate(tropfenPrefab, spawnPosition, Quaternion.identity);
        
        tropfen.transform.localScale = Vector3.one * tropfenSize;
        tropfen.tag = flüssigkeitTag;

        Rigidbody rb = tropfen.GetComponent<Rigidbody>();
        if (rb == null) rb = tropfen.AddComponent<Rigidbody>();

        rb.velocity = -transform.up * tropfenSpeed; 
        Destroy(tropfen, 10f);
    }

    float GetNormalizedAngle()
    {
        // Normalisiert den Winkel auf den Bereich -180° bis 180°
        float angle = parentObject.localEulerAngles.z;
        return angle > 180f ? angle - 360f : angle;
    }

    bool IsAngleInRange(float angle, float min, float max)
    {
        // Überprüft, ob der Winkel innerhalb des Bereichs liegt
        return angle >= min && angle <= max;
    }
}
