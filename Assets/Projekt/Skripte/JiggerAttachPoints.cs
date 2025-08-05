using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


// Dieses Skript ermöglicht es, ein Objekt an zwei verschiedenen Punkten zu greifen (für den Jigger).
// Wenn der Benutzer das Objekt unten greift, wird die größere Seite nach oben zeigen,
// und wenn er es oben greift, wird das Objekt um 180 Grad gedreht, sodass die kleine Seite oben ist.
// Leider funktioniert die Drehung nicht wie erwartet!!!

public class JiggerAttachPoints : MonoBehaviour
{
    public Transform attachTop;
    public Transform attachBottom;
    private XRGrabInteractable grabInteractable;
    private bool isRotated = false; 

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.attachTransform = attachBottom;
        grabInteractable.useDynamicAttach = true; 

        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease); 
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        Vector3 interactorPosition = args.interactorObject.transform.position;

        if (args.interactorObject is XRRayInteractor rayInteractor)
        {
            RaycastHit hit;
            if (rayInteractor.TryGetCurrent3DRaycastHit(out hit))
            {
                interactorPosition = hit.point;
            }
        }

        // Distanzen berechnet 
        float distanceToTop = Vector3.Distance(interactorPosition, attachTop.position);
        float distanceToBottom = Vector3.Distance(interactorPosition, attachBottom.position);

        // Attach-Punkt wird basierend auf der Nähe gesetzt
        grabInteractable.attachTransform = (distanceToTop < distanceToBottom) ? attachTop : attachBottom;

        // Wenn der Attach-Punkt oben ist, wird das Objekt in der Hand um 180 Grad auf der X-Achse gedreht 
        if (grabInteractable.attachTransform == attachTop && !isRotated)
        {
            // Drehung um 180 Grad auf der X-Achse in der Hand (relative Rotation)
            args.interactorObject.transform.Rotate(180f, 0f, 0f, Space.Self);

  
            isRotated = true;
        }
        // Debug-Ausgabe zur Überprüfung
        Debug.Log($"Attach-Punkt gesetzt auf: {(grabInteractable.attachTransform == attachTop ? "Oben" : "Unten")}");
    }

    // Wenn das Objekt losgelassen wird, Rotation zurücksetzen
    private void OnRelease(SelectExitEventArgs args)
    {
        isRotated = false;
    }
}
