﻿/**
 * Filename:            PlayerBehaviour.cs
 * Student Name:        Jash Shah
 * Student ID:          101274212
 * Date last modified:  26/09/2021
 * Program Description: Controls the Player Character
 * Revision History:
 *  - 26/09/2021 - Add a basic gameplay scene with player character
 */

using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UI;

[System.Serializable]
public enum PlayerAnimationType
{
    IDLE,
    RUN,
    JUMP,
    CROUCH
}

public enum RampDirection
{
    NONE,
    UP,
    DOWN
}

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Controls")]
    public float horizontalForce;
    public float verticalForce;

    [Header("Platform Detection")]
    public bool isGrounded;
    public bool isJumping;
    public bool isCrouching;
    public Transform lookAheadPoint;
    public Transform lookInFrontPoint;
    public LayerMask collisionGroundLayer;
    public LayerMask collisionWallLayer;
    public RampDirection rampDirection;
    public bool onRamp;
    public Text victoryText;
    public Transform respawnPoint;
    public Transform finishPoint;

    private Rigidbody2D m_rigidBody2D;
    private SpriteRenderer m_spriteRenderer;
    private Animator m_animator;
    private RaycastHit2D groundHit;

    private float m_clicked_position = 0f;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody2D = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
        m_clicked_position = gameObject.transform.position.x;
        victoryText.enabled = false;
        Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m_clicked_position = worldPosition.x;
        }

        //text.text = "Velocity: " + Mathf.Round(m_rigidBody2D.velocity.magnitude);
        if (Mathf.Sqrt(Mathf.Pow(gameObject.transform.position.x - finishPoint.transform.position.x, 2) + 
            Mathf.Pow(gameObject.transform.position.y - finishPoint.transform.position.y, 2)) < 1)
        {
            victoryText.enabled = true;
            Time.timeScale = 0;
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _LookInFront();
        _LookAhead();
        _Move();
    }

    private void _LookInFront()
    {
        if (!isGrounded)
        {
            rampDirection = RampDirection.NONE;
            return;
        }

        var wallHit = Physics2D.Linecast(transform.position, lookInFrontPoint.position, collisionWallLayer);
        if (wallHit && isOnSlope())
        {
            rampDirection = RampDirection.UP;
        }
        else if (!wallHit && isOnSlope())
        {
            rampDirection = RampDirection.DOWN;
        }

        Debug.DrawLine(transform.position, lookInFrontPoint.position, Color.red);
    }

    private void _LookAhead()
    {
        var groundHitLeft = Physics2D.Linecast(transform.position, lookAheadPoint.position + Vector3.left * 0.2f, collisionGroundLayer);
        var groundHitRight = Physics2D.Linecast(transform.position, lookAheadPoint.position + Vector3.right * 0.2f, collisionGroundLayer);
        groundHit = Physics2D.Linecast(transform.position, lookAheadPoint.position, collisionGroundLayer);

        isGrounded = (groundHit || groundHitLeft || groundHitRight) ? true : false;

        Debug.DrawLine(transform.position, lookAheadPoint.position, Color.green);
    }

    private bool isOnSlope()
    {
        if (!isGrounded)
        {
            onRamp = false;
            return false;
        }

        if (groundHit.normal != Vector2.up)
        {
            onRamp = true;
            return true;
        }

        onRamp = false;
        return false;
    }

    void _Move()
    {
        if (isGrounded)
        {
            if (!isJumping && !isCrouching)
            {
                if (Input.GetAxis("Horizontal") > 0)
                {
                    // move right
                    m_rigidBody2D.AddForce(Vector2.right * horizontalForce * Time.deltaTime);
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    if (onRamp && rampDirection == RampDirection.UP)
                    {
                        m_rigidBody2D.AddForce(Vector2.up * horizontalForce * 0.5f * Time.deltaTime);
                    }
                    else if (onRamp && rampDirection == RampDirection.DOWN)
                    {
                        m_rigidBody2D.AddForce(Vector2.down * horizontalForce * 0.5f * Time.deltaTime);
                    }


                    //m_animator.SetInteger("AnimState", (int)PlayerAnimationType.RUN);
                    m_animator.ResetTrigger("idleTrigger");
                    m_animator.ResetTrigger("jumpTrigger");
                    m_animator.SetTrigger("walkTrigger");
                }
                else if (Input.GetAxis("Horizontal") < 0)
                {
                    // move left
                    m_rigidBody2D.AddForce(Vector2.left * horizontalForce * Time.deltaTime);
                    transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                    if (onRamp && rampDirection == RampDirection.UP)
                    {
                        m_rigidBody2D.AddForce(Vector2.up * horizontalForce * 0.5f * Time.deltaTime);
                    }
                    else if (onRamp && rampDirection == RampDirection.DOWN)
                    {
                        m_rigidBody2D.AddForce(Vector2.down * horizontalForce * 0.5f * Time.deltaTime);
                    }

                    //m_animator.SetInteger("AnimState", (int)PlayerAnimationType.RUN);
                    m_animator.ResetTrigger("idleTrigger");
                    m_animator.ResetTrigger("jumpTrigger");
                    m_animator.SetTrigger("walkTrigger");
                }
                else
                {
                    //m_animator.SetInteger("AnimState", (int)PlayerAnimationType.IDLE);
                    m_animator.ResetTrigger("walkTrigger");
                    m_animator.ResetTrigger("jumpTrigger");
                    m_animator.SetTrigger("idleTrigger");
                    m_rigidBody2D.velocity = new Vector2(0.0f, 0.0f);
                }
            }

            if ((Input.GetAxis("Vertical") > 0) && (!isJumping))
            {
                // jump
                m_rigidBody2D.AddForce(Vector2.up * verticalForce);
                //m_animator.SetInteger("AnimState", (int) PlayerAnimationType.JUMP);
                m_animator.ResetTrigger("idleTrigger");
                m_animator.ResetTrigger("walkTrigger");
                m_animator.SetTrigger("jumpTrigger");
                isJumping = true;
            }
            else
            {
                isJumping = false;
            }

            if ((Input.GetAxis("Vertical") < 0) && (!isCrouching))
            {
                //m_animator.SetInteger("AnimState", (int)PlayerAnimationType.CROUCH);
                isCrouching = true;
            }
            else
            {
                isCrouching = false;
            }
        }

        if (gameObject.transform.position.x < 11)
        {
            gameObject.transform.position = new Vector3(11f, gameObject.transform.position.y, gameObject.transform.position.z);
        }

        if (gameObject.transform.position.x > 46)
        {
            gameObject.transform.position = new Vector3(46f, gameObject.transform.position.y, gameObject.transform.position.z);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fire")
        {
            gameObject.transform.position = respawnPoint.transform.position;
            isJumping = true;
            m_rigidBody2D.velocity = Vector2.zero;
        }
    }
}