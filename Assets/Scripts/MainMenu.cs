using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public Button[] levelButtons;
    public Button creditsButton;
    public Button menuButton;
    public Button exitButton; // Reference for the Exit button

    public GameObject LevelMenu;
    public GameObject CreditsMenu;

    void Start()
    {
        // Set up level buttons to load the corresponding levels
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int index = i + 1; // fix: capture new variable
            levelButtons[i].onClick.AddListener(() => LoadLevel(index));
        }

        // Add the listener for the credits button
        creditsButton.onClick.AddListener(Credits);
        menuButton.onClick.AddListener(MenuReturn);

        // Add the listener for the exit button
        exitButton.onClick.AddListener(ExitGame);


        LevelMenu = GameObject.Find("Level Select Panel");
        CreditsMenu = GameObject.Find("Credits Panel");
        CreditsMenu.SetActive(false);
    }

    void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene("Level" + levelIndex);
    }

    void Credits()
    {
        LevelMenu.SetActive(false);
        CreditsMenu.SetActive(true);
    }

    void MenuReturn()
    {
        LevelMenu.SetActive(true);
        CreditsMenu.SetActive(false);
    }

    void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}