using UnityEngine;
using TMPro;


// Dieses Skript steuert das Ein- und Ausblenden eines Text-Popups (Canvas),
// das angezeigt wird, wenn der Benutzer mit dem Raycast über die Flaschen fährt.
public class HoverTextController : MonoBehaviour
{
    [SerializeField] private GameObject textPopup; 
    
    public void ShowHoverText()
    {
        if(textPopup != null)
            textPopup.SetActive(true);
    }

    public void HideHoverText()
    {
        if(textPopup != null)
            textPopup.SetActive(false);
    }
}