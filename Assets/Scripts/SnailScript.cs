using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailScript : MonoBehaviour {

    public float snailSpeed;

    private GameObject gameMG;
    private bool isDead;

    // Use this for initialization
    void Start () {
        gameMG = GameObject.Find("GameManager");
        isDead = false;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 position = transform.position;
        position.x -= Time.deltaTime * snailSpeed;
        transform.position = position;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(!isDead)
        {
            if (coll.tag == "PlayerFeet")
            {
                if (!gameMG.GetComponent<PlayerManager>().isInvincible)
                {
                    //
                    // change animation
                    GetComponent<Animator>().Play("SnailDead");
                    gameMG.GetComponent<PlayerManager>().SnailJump();
                    gameMG.GetComponent<ScoreManager>().AddScore(10);
                    gameMG.GetComponent<GameManager>().PlaySnailSound();

                    isDead = true;
                    //Destroy(this.gameObject);
                }
            }
            else if (coll.tag == "Player")
            {
                if (!gameMG.GetComponent<PlayerManager>().isInvincible)
                {
                    gameMG.GetComponent<GameManager>().GetTouch();
                }
            }
        }
    }
}
