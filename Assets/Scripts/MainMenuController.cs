/**
 * Filename:            MainMenuController.cs
 * Student Name:        Jash Shah
 * Student ID:          101274212
 * Date last modified:  26/09/2021
 * Program Description: Controls Main Menu functions
 * Revision History:
 *  - 25/09/2021 - Add Initial Main Menu functions
 *  - 26/09/2021 - Add instruction screen functionality
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    GameObject m_main_menu;
    GameObject m_instruction_screen;

    void Start()
    {
        m_main_menu = GameObject.FindGameObjectWithTag("MainMenu");
        m_instruction_screen = GameObject.FindGameObjectWithTag("InstructionScreen");
        
        m_main_menu.SetActive(true);
        m_instruction_screen.SetActive(false);
    }

    public void OnStartButton()
    {
        SceneManager.LoadScene("GameplayScene");
    }

    public void OnBackButton()
    {
        toggle_main_and_instruction_menu();
    }

    public void OnInstructionsButton()
    {
        toggle_main_and_instruction_menu();
    }

    public void OnExitButton()
    {
        Application.Quit();
    }

    void toggle_main_and_instruction_menu()
    {
        var is_active = m_main_menu.activeSelf;

        m_main_menu.SetActive(!is_active);
        m_instruction_screen.SetActive(is_active);
    }
}
