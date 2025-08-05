using UnityEngine;
using UnityEngine.UI;

// Dieses Skript ermöglicht es dem Spieler im Spielmodus "Anfänger", ein Getränk auszuwählen und zu bestätigen.
// Die Auswahl wird gespeichert und visuell mit einem farbigen Button und einem Schild dargestellt.
// Der Testdetector nutzt diese Auswahl, um das Getränk korrekt zu überprüfen.

public class TabletNavigation : MonoBehaviour
{
    public GameObject mainScreen; // Hauptscreen
    public GameObject[] pages; // Array für Seiten
    public Button[] pageButtons; // Array für Navigations-Buttons
    public Button[] backButtons; // Array für Zurück-Buttons
    public Button[] selectButtons; // Array für Select-Buttons (einem Getränk zuweisen)
    public string[] drinkNames; // Namen der Getränke (im Inspektor zuweisen)
    public GameObject shieldPrefab; 
    public string selectedDrink = ""; // Der aktuell gewählte Drink
    private Button lastSelectedButton = null;
    private GameObject lastShield = null; 
    private Button lastSelectedPageButton = null; // Der zuletzt ausgewählte Page Button

    void Start()
    {
        // Setzt den Hauptscreen als aktiven Screen beim Start
        ShowMainScreen();

        // Setzt Click-Listener für alle  Page-Buttons
        for (int i = 0; i < pageButtons.Length; i++)
        {
            int index = i; 
            pageButtons[i].onClick.AddListener(() => ShowPage(index));
        }

        // Click-Listener Zurück-Buttons
        for (int i = 0; i < backButtons.Length; i++)
        {
            backButtons[i].onClick.AddListener(ShowMainScreen);
        }

        // Click-Listener Select-Buttons
        for (int i = 0; i < selectButtons.Length; i++)
        {
            int index = i; 
            selectButtons[i].onClick.AddListener(() => SelectDrink(index));
        }
    }

    // Zeigt den Hauptscreen und verstecke alle anderen Seiten
    public void ShowMainScreen()
    {
        mainScreen.SetActive(true);
        foreach (GameObject page in pages)
        {
            page.SetActive(false);
        }
    }

    // Zeigt eine bestimmte Seite basierend auf dem Index
    public void ShowPage(int index)
    {
        mainScreen.SetActive(false); // Versteckt den Hauptscreen
        pages[index].SetActive(true); // Zeigt die entsprechende Seite
    }

    // Wählt ein Getränk und speichere es in der String-Variable
    public void SelectDrink(int index)
    {
        if (index >= 0 && index < drinkNames.Length)
        {

            selectedDrink = drinkNames[index];
            Debug.Log("Selected Drink: " + selectedDrink);


            if (lastShield != null)
            {
                Destroy(lastShield); 
            }
            lastShield = Instantiate(shieldPrefab, selectButtons[index].transform);
            lastShield.transform.localPosition = new Vector3(0, -9, 0);


            if (lastSelectedButton != null)
            {
                lastSelectedButton.GetComponent<Image>().color = Color.white; 
            }

            lastSelectedButton = selectButtons[index];
            lastSelectedButton.GetComponent<Image>().color = new Color32(255, 153, 153, 255); 


            if (lastSelectedPageButton != null)
            {
                lastSelectedPageButton.GetComponent<Image>().color = Color.white; 
            }


            lastSelectedPageButton = pageButtons[index];
            lastSelectedPageButton.GetComponent<Image>().color = new Color32(255, 153, 153, 255); 
        }
    }
}
