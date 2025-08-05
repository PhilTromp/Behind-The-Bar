using UnityEngine;


// Dieses Skript sorgt dafür, dass ein Canvas (UI-Element) immer der Kamera folgt und sich in deren Blickrichtung ausrichtet.
// Erstellt für die Flaschen mit der HoverTextController Klasse, für die Deko-Schüssel Objekte, Mülleimer und Spülbecken 
public class HUDFollowCamera : MonoBehaviour
{
    public Canvas displayCanvas; // Das Canvas, das dem Spieler folgt
    public Camera mainCamera;    // Die Hauptkamera (kann automatisch zugewiesen werden)

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Die Kamera automatisch zuweisen, falls sie nicht zugewiesen wurde
        }

        if (displayCanvas != null)
        {
            // Stelle sicher, dass der Canvas im World Space ist
            displayCanvas.renderMode = RenderMode.WorldSpace;
        }
    }

    void Update()
    {
        if (displayCanvas != null && mainCamera != null)
        {
            // Richten des Canvas, damit es immer der Kamera zugewandt ist
            Vector3 direction = displayCanvas.transform.position - mainCamera.transform.position;
            displayCanvas.transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
