/**
 * Filename:            EnemyBehaviour.cs
 * Student Name:        Jash Shah
 * Student ID:          101274212
 * Date last modified:  15/10/2021
 * Program Description: Controls the Enemy Character
 * Revision History:
 *  - 15/10/2021 - Add EnemyBehavior script
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    public float walkSpeed = -50;
    public float walkDistance = 100;
    public Rigidbody2D rb;

    float start_x;

    // Start is called before the first frame update
    void Start()
    {
        start_x = gameObject.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(gameObject.transform.position.x - start_x) > walkDistance)
        {
            walkSpeed *= -1;
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }
}
