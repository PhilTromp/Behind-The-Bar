using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// Dieses Skript ermöglicht es dem Nutzer, Objekte wie Zitronenscheiben aus kinematischen Schüsseln zu entnehmen.  
// Beim Greifen wird ein neues Objekt (Prefab) an der Position der Hand gespawnt.  
// Das neu erzeugte Objekt wird direkt in die Hand des Nutzers übergeben.  
// Gleichzeitig wird die Interaktion mit dem ursprünglichen Objekt abgebrochen, sodass es nur als "Ausgabepunkt" dient.  

public class ObjektAnvisieren : MonoBehaviour
{
    public GameObject prefabToSpawn; 
    private XRGrabInteractable grabInteractable;

    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // Sicherstellen, dass ein Prefab existiert
        if (prefabToSpawn == null) return;

        // Die Hand (Interactor) bekommen, die das Objekt greifen wollte
        IXRSelectInteractor interactor = args.interactorObject;
        if (interactor == null) return;

        // Prefab an der Position der Hand spawnen
        GameObject spawnedObject = Instantiate(prefabToSpawn, interactor.transform.position, interactor.transform.rotation);

        // Das gespawnte Objekt als "grabbares" Objekt setzen
        IXRSelectInteractable spawnedInteractable = spawnedObject.GetComponent<IXRSelectInteractable>();
        if (spawnedInteractable != null)
        {
            // Das gespawnte Objekt sofort in die Hand geben (mit neuer API)
            grabInteractable.interactionManager.SelectEnter(interactor, spawnedInteractable);
        }

        // Das Greifen des ursprünglichen Cubes sofort abbrechen (mit neuer API)
        grabInteractable.interactionManager.CancelInteractableSelection(grabInteractable as IXRSelectInteractable);
    }
}

