/**
 * Filename:            PlayerBehaviour.cs
 * Student Name:        Jash Shah
 * Student ID:          101274212
 * Date last modified:  26/09/2021
 * Program Description: Game Over Menu controller
 * Revision History:
 *  - 26/09/2021 - Add a game over menu
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
    }

    public void OnMainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnReplayButton()
    {
        SceneManager.LoadScene("GameplayScene");
    }

}
