/**
 * Filename:            MainMenuController.cs
 * Student Name:        Jash Shah
 * Student ID:          101274212
 * Date last modified:  25/09/2021
 * Program Description: Controls Main Menu functions
 * Revision History:
 *  - 25/09/2021 - Add Initial Main Menu functions
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void OnStartButton()
    {
        SceneManager.LoadScene("GameplayScene");
    }

    public void OnInstructionsButton()
    {
        // TODO: Activate instructions
    }

    public void OnExitButton()
    {
        Application.Quit();
    }
}
