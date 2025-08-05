using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


// Dieses Skript ermöglicht es, den Deckel eines Shakers auf den Shaker zu setzen und wieder abzunehmen.  
// Es nutzt einen XRSocketInteractor, um zu erkennen, ob der Deckel mit dem richtigen Tag ("Deckel")  
// in den Socket eingesetzt oder entfernt wird. Die Variable "deckelAufBehälter" speichert den aktuellen Status.  

public class DeckelSocketInteraction : MonoBehaviour
{
    public XRSocketInteractor socketInteractor;
    public string erlaubterTag = "Deckel"; 
    public bool deckelAufBehälter = false;

    private void Start()
    {
        if (socketInteractor != null)
        {
            // socketInteraction Event-Systeme
            socketInteractor.selectEntered.AddListener(OnDeckelSocketEntered);
            socketInteractor.selectExited.AddListener(OnDeckelSocketExited);
        }
    }

    private void OnDeckelSocketEntered(SelectEnterEventArgs args)
    {
        // Nur Objekte mit den erlaubten Tag dürfen sich an den Shaker ranhängen
        if (args.interactableObject.transform.CompareTag(erlaubterTag))
        {
            deckelAufBehälter = true;
        }
    }

    private void OnDeckelSocketExited(SelectExitEventArgs args)
    {
        
        if (args.interactableObject.transform.CompareTag(erlaubterTag))
        {
            deckelAufBehälter = false;
        }
    }

    private void OnDestroy()
    {
        if (socketInteractor != null)
        {
            // Entferne die Listener, um Speicherlecks zu vermeiden
            socketInteractor.selectEntered.RemoveListener(OnDeckelSocketEntered);
            socketInteractor.selectExited.RemoveListener(OnDeckelSocketExited);
        }
    }
}
