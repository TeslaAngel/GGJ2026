using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management
using UnityEditor; // This namespace is required for Editor-only functionality

public class MainMenu : MonoBehaviour
{
    // Call this method to load the main menu scene
    public void LoadMainMenu()
    {
        // Use the name of your main menu scene
        SceneManager.LoadScene("MainMenu");
    }

    // Call this method to load the instructions scene
    public void LoadInstructions()
    {
        // Use the name of your main menu scene
        SceneManager.LoadScene("InstructionsPage");
    }


    // Call this method to load the main game scene
    public void StartGame()
    {
        // Use the name of your game scene
        SceneManager.LoadScene("GameScene");
    }

    // Call this method to quit the application (only works in a build)
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit"); // Log for testing in the editor
        EditorApplication.isPlaying = false;
    }
}
