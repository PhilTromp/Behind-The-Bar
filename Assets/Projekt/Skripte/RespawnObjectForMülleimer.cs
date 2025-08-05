using UnityEngine;


// Dieses Skript sorgt dafür, dass Objekte, die nicht in den Mülleimer geworfen werden können,  
// automatisch an ihren ursprünglichen Startpunkt zurückkehren, wenn sie im Mülleimer landen.  
  

public class RespawnObjectForMülleimer : MonoBehaviour
{
    private Vector3 initialPosition; 
    private Quaternion initialRotation; 


    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }


    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Mülleimer") ) 
        {
            ResetPosition(); 
        }
    }


    void ResetPosition()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
}