using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public Button[] levelButtons;
    public Button exitButton; // Reference for the Exit button

    void Start()
    {
        // Set up level buttons to load the corresponding levels
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int index = i + 1; // fix: capture new variable
            levelButtons[i].onClick.AddListener(() => LoadLevel(index));
        }

        // Add the listener for the exit button
        exitButton.onClick.AddListener(ExitGame);
    }

    void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene("Level" + levelIndex);
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
