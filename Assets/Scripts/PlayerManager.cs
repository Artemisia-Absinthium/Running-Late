using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public GameObject player;
    public float jumpVelocity;
    public float gravity;
    public float invincibilityTime;

    private bool canJump;
    private bool falling;
    internal bool isInvincible;
    private bool isDead;
    private float minHeight;
    private float currentVelocity;
    private float blikingDeltaTime;
    private float deltaTime;
    // Use this for initialization
    void Start()
    {
        minHeight = player.transform.position.y;
        canJump = true;
        falling = false;
        isInvincible = false;
        isDead = false;
        blikingDeltaTime = 0.0f;
        deltaTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            deltaTime += Time.deltaTime;

            if(deltaTime > 1.0f)
            {
                foreach (Transform child in player.transform)
                {
                    if (child.tag == "Player")
                    {
                        child.GetComponent<SpriteRenderer>().enabled = false;
                    }
                }
            }
        }
        else
        {
            //
            // Check input
            if (canJump && Input.GetButton("Jump"))
            {
                canJump = false;
                currentVelocity = jumpVelocity;
                GetComponent<GameManager>().PlayJumpSound();
            }
            else if (!falling && Input.GetButtonUp("Jump"))
            {
                currentVelocity = 0;
                falling = true;
            }

            if (isInvincible)
            {
                //
                // Invincible animation
                deltaTime += Time.deltaTime;
                blikingDeltaTime += Time.deltaTime;

                if (blikingDeltaTime > 0.15f)
                {
                    blikingDeltaTime = 0.0f;
                    foreach (Transform child in player.transform)
                    {
                        if (child.tag == "Player")
                        {
                            if(child.GetComponent<SpriteRenderer>().enabled)
                            {
                                child.GetComponent<SpriteRenderer>().enabled = false;
                            }
                            else
                            {
                                child.GetComponent<SpriteRenderer>().enabled = true;
                            }
                        }
                    }
                }
                if (deltaTime > invincibilityTime)
                {
                    deltaTime = 0.0f;
                    blikingDeltaTime = 0.0f;
                    foreach (Transform child in player.transform)
                    {
                        if (child.tag == "Player")
                        {
                            child.GetComponent<SpriteRenderer>().enabled = true;
                        }
                    }
                    isInvincible = false;
                }
            }
        }

        //
        // Check jump end
        if (!canJump)
        {
            foreach (Transform child in player.transform)
            {
                if (child.tag == "Player")
                {
                    child.GetComponent<Animator>().Play("Jumping");
                }
            }

            /*currentVelocity -= gravity * Time.deltaTime;
            if (!falling && currentVelocity <= 0)
            {
                falling = true;
            }

            Vector3 pos = player.transform.position;
            pos.y += currentVelocity;
            player.transform.position = pos;
            if (pos.y < minHeight)
            {
                pos.y = minHeight;
                player.transform.position = pos;
                canJump = true;
                falling = false;
            }*/
        }
        else if(!isDead)
        {
            foreach (Transform child in player.transform)
            {
                if (child.tag == "Player")
                {
                    child.GetComponent<Animator>().Play("Running");
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (!canJump)
        {
            currentVelocity -= gravity * Time.deltaTime;
            if (!falling && currentVelocity <= 0)
            {
                falling = true;
            }

            Vector3 pos = player.transform.position;
            pos.y += currentVelocity;
            player.transform.position = pos;
            if (pos.y < minHeight)
            {
                pos.y = minHeight;
                player.transform.position = pos;
                canJump = true;
                falling = false;
            }
        }
    }
    internal void SnailJump()
    {
        canJump = false;
        currentVelocity = jumpVelocity/1.2f;
    }

    public void Die()
    {
        isDead = true;
        foreach (Transform child in player.transform)
        {
            if (child.tag == "Player")
            {
                child.GetComponent<Animator>().Play("Dying");
            }
        }

        canJump = true;
        falling = false;
        isInvincible = false;
        blikingDeltaTime = 0.0f;
    }

    public void Restart()
    {
        foreach (Transform child in player.transform)
        {
            if (child.tag == "Player")
            {
                child.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        Vector3 pos = player.transform.position;
        pos.y = minHeight;
        player.transform.position = pos;
        deltaTime = 0.0f;

        canJump = true;
        falling = false;
        isInvincible = false;
        isDead = false;
        blikingDeltaTime = 0.0f;
        deltaTime = 0.0f;
    }
    public void GetTouch()
    {
        isInvincible = true;
        GetComponent<GameManager>().PlayHurtSound();
    }
}
