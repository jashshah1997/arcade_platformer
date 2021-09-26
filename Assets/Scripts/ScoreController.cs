/**
 * Filename:            PlayerBehaviour.cs
 * Student Name:        Jash Shah
 * Student ID:          101274212
 * Date last modified:  26/09/2021
 * Program Description: Updates game score
 * Revision History:
 *  - 26/09/2021 - Add a score controller
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public TextMesh scoreText;
    private PlayerBehaviour player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        scoreText.text = "Score: 0";
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + player.score;
    }
}
