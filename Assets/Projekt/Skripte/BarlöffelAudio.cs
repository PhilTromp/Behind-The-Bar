using UnityEngine;



// Diese Klasse spielt einen Sound ab, wenn der Barlöffel mit einem Objekt kollidiert, das das "Behälter"-Skript besitzt,  
// und stoppt den Sound, sobald das Objekt den Bereich verlässt.  

public class BarlöffelAudio : MonoBehaviour
{
    public AudioSource audioSource; 
    public AudioClip barlöffelClip; 


    void Start()
    {
        // fügt automatische eine aduiosource hinzu an das Objekt 
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }  
    }

    // Mehtode: Wenn ein Objekt mit dem Behälter Skript in den Trigger Bereich kommt 
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.GetComponent<Behälter>() != null)
        {
            if (audioSource != null && barlöffelClip != null)
            {
                if (!audioSource.isPlaying) 
                {
                    audioSource.clip = barlöffelClip;
                    audioSource.loop = true; 
                    audioSource.Play();
                }
            }
        }
    }

    // Mehtode: Wenn ein Objekt mit dem Behälter Skript in den Trigger Bereich verlässt 
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Behälter>() != null)
        {
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.loop = false; 
                audioSource.Stop();
            }
        }
    }
}
