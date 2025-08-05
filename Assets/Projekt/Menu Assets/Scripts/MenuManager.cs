using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    // Menu Seiten 
    public GameObject gameStartMenu;
    public GameObject szeneSelectMenu;
    public GameObject creditsMenu;
    public GameObject aboutMenu;

    // Buttons im Game Start Menu
    public Button startButton;
    public Button quitButton;

    // Buttons im Szene Select Menu
    public Button anfaengerButton;
    public Button expertenButton;
    public Button tutorialButton; 
    public Button aboutButton;
    public Button creditsButton;
    public Button backButton;

    // Buttons im About Menu
    public Button aboutBackButton;

    // Buttons im Credits Menu
    public Button creditsBackButton;

    private void Start()
    {
        // Beim Start wird das Game Start Menu aktiviert und die anderen deaktiviert
        gameStartMenu.SetActive(true);
        szeneSelectMenu.SetActive(false);
        creditsMenu.SetActive(false);
        aboutMenu.SetActive(false);

        // Button-Listener zuweisen
        AssignButtonListeners();
    }

    private void AssignButtonListeners()
    {
        // Game Start Menu
        startButton.onClick.AddListener(OnStartButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);

        // Szene Select Menu
        anfaengerButton.onClick.AddListener(OnAnfaengerButtonClicked);
        expertenButton.onClick.AddListener(OnExpertenButtonClicked);
        tutorialButton.onClick.AddListener(OnTutorialButtonClicked); // Listener f�r den Tutorial-Button
        aboutButton.onClick.AddListener(OnAboutButtonClicked);
        creditsButton.onClick.AddListener(OnCreditsButtonClicked);
        backButton.onClick.AddListener(OnBackButtonClicked);

        // About Menu
        aboutBackButton.onClick.AddListener(OnAboutBackButtonClicked);

        // Credits Menu
        creditsBackButton.onClick.AddListener(OnCreditsBackButtonClicked);
    }

    // Methode f�r den Beenden-Button
    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    // Methode f�r den Button, der zum Szene Select Menu f�hrt
    public void OnStartButtonClicked()
    {
        gameStartMenu.SetActive(false);
        szeneSelectMenu.SetActive(true);
    }

    // Methode f�r den Zur�ck-Button im Szene Select Menu
    public void OnBackButtonClicked()
    {
        szeneSelectMenu.SetActive(false);
        gameStartMenu.SetActive(true);
    }

    // Methode f�r den About-Button im Szene Select Menu
    public void OnAboutButtonClicked()
    {
        szeneSelectMenu.SetActive(false);
        aboutMenu.SetActive(true);
    }

    // Methode f�r den Zur�ck-Button im About Menu
    public void OnAboutBackButtonClicked()
    {
        aboutMenu.SetActive(false);
        szeneSelectMenu.SetActive(true);
    }

    // Methode f�r den Credits-Button im Szene Select Menu
    public void OnCreditsButtonClicked()
    {
        szeneSelectMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    // Methode f�r den Zur�ck-Button im Credits Menu
    public void OnCreditsBackButtonClicked()
    {
        creditsMenu.SetActive(false);
        szeneSelectMenu.SetActive(true);
    }

    // Methode f�r den Anf�nger-Button im Szene Select Menu
    public void OnAnfaengerButtonClicked()
    {
        SceneManager.LoadScene("Anfaenger"); // Ersetze "AnfaengerSzene" mit dem Namen deiner Szene
    }

    // Methode f�r den Experten-Button im Szene Select Menu
    public void OnExpertenButtonClicked()
    {
        SceneManager.LoadScene("Experte"); // Ersetze "ExpertenSzene" mit dem Namen deiner Szene
    }

    // Methode f�r den Tutorial-Button im Szene Select Menu
    public void OnTutorialButtonClicked()
    {
        SceneManager.LoadScene("Tutorial"); // Ersetze "TutorialSzene" mit dem Namen deiner Szene
    }
}