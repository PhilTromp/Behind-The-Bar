using UnityEngine;


// Dieses Skript sorgt dafür, dass ein Audio abgespielt wird, wenn das Objekt "Stößel" mit einem Objekt,
// das das "Behälter"-Skript hat, in Kontakt kommt. Der Sound läuft im Loop, solange sich der Stößel im Behälter befindet,
// und stoppt, sobald er entfernt wird.

public class StößelAudio : MonoBehaviour
{
    public AudioSource audioSource; 
    public AudioClip stößelClip; 


    void Start()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }  
    }
    private void OnTriggerEnter(Collider other)
    {
        // Prüft, ob das Objekt, das betreten wurde, das Behälter-Skript hat
        if (other.GetComponent<Behälter>() != null)
        {
            if (audioSource != null && stößelClip != null)
            {
                if (!audioSource.isPlaying) // Verhindert, dass der Sound mehrfach startet
                {
                    audioSource.clip = stößelClip;
                    audioSource.loop = true; // Setzt den Loop-Modus auf true
                    audioSource.Play();
                    Debug.Log("Stößel-Sound gestartet und läuft im Loop.");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Prüft, ob das Objekt, das verlassen wurde, das Behälter-Skript hat
        if (other.GetComponent<Behälter>() != null)
        {
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.loop = false; // Stoppt den Loop-Modus, wenn der Bereich verlassen wird
                audioSource.Stop();
                Debug.Log("Stößel-Sound gestoppt.");
            }
        }
    }
}
