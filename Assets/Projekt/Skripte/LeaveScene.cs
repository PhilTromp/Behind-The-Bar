using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;


// Dieses Skript lädt die "Menu"-Szene, wenn der Benutzer den Menu-Button auf dem linken Controller drückt.
// Es überprüft den Zustand des Menu-Buttons und reagiert nur, wenn dieser Button gerade gedrückt wurde.
public class LeaveScene : MonoBehaviour
{
    private bool wasMenuButtonPressed = false; // Zustand des Menu-Buttons im vorherigen Frame


    private void Update()
    {
        // Linker Controller
        InputDevice leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        
        if (leftController.TryGetFeatureValue(CommonUsages.menuButton, out bool menuButtonPressed))
        {
            if (menuButtonPressed && !wasMenuButtonPressed)
            {
                Debug.Log("Menu-Button (drei Striche) gedr�ckt.");
                LoadMenuScene();
            }
            wasMenuButtonPressed = menuButtonPressed;
        }

       
    }

    // Lädt die Szene "Menu"
    private void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
}