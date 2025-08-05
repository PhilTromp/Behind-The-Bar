using System.Collections;
using TMPro; 
using UnityEngine;


// Dieses Skript sorgt dafür, dass Objekte, die in den Mülleimer geworfen werden, gelöscht werden – außer Werkzeuge wie Barlöffel, Stößel, Jigger, Shaker, Deckel und Flaschen.  
// Wenn ein löschbares Objekt in den Mülleimer geworfen wird, erscheint ein HUD, das bestätigt, dass es beseitigt wurde.  
// Wenn ein nicht löschbares Objekt hineingeworfen wird, erscheint ein HUD mit der Meldung, dass es Eigentum des Restaurants ist und nicht entsorgt werden kann.  
// Das HUD wird nach ein paar Sekunden automatisch wieder deaktiviert.  
// HUD verschwindet nach 5 Sekunden wieder. 

public class Mülleimer : MonoBehaviour
{
    public GameObject canvas;
    public TextMeshProUGUI textMeshPro;


    void Start()
    {
        canvas.SetActive(false); // Canvas zu Beginn soll deaktiviert sein 
    }


    void Update()
    {
        
    }
    
    void OnTriggerEnter(Collider müll)
    {
        // Wenn der Tag nicht den unlöschbaren gleicht, sollen die gelöscht werden 
        if (!(müll.CompareTag("Stößel") || müll.CompareTag("Barlöffel") || müll.CompareTag("Shaker") || müll.CompareTag("Jigger") || müll.CompareTag("Deckel") || müll.CompareTag("Flasche") || müll.CompareTag("Tee Löffel")))
        {
            Destroy(müll.gameObject);
            Debug.Log("Müll wurde beseitigt");
            textMeshPro.text = "Müll wurde beseitigt";

 
            canvas.SetActive(true); // HUD wird angezeigt
            StartCoroutine(DeaktiviereCanvasNachZeit(5f)); // Nach 5 Sekunden verschwidet sie wieder 
        }
        else
        {
            Debug.Log("Das darf nicht weggeschmissen werden. Das ist das Eigentum des Restaurants!!!");
            textMeshPro.text = "Eigentum des Restaurants, kann nicht weggeschmissen werden";

            canvas.SetActive(true); // HUD wird angezeigt
            StartCoroutine(DeaktiviereCanvasNachZeit(5f)); // Nach 5 Sekunden verschwidet sie wieder 
        }
    }

    // Coroutine, die das Canvas nach einer bestimmten Zeit deaktiviert
    IEnumerator DeaktiviereCanvasNachZeit(float zeit)
    {
        yield return new WaitForSeconds(zeit);
        canvas.SetActive(false);
    }
}
