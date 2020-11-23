using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {

    private float rotSpeed = 45f;
    private GameObject gameMG;

    private bool isBad;
    private bool forceGood;

    // Use this for initialization
    void Start () {
        gameMG = GameObject.Find("GameManager");
        if(!forceGood && Random.value > 0.5)
        {
            isBad = true;
            this.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    // Update is called once per frame
    void Update () {
        this.transform.Rotate(Vector3.up, Time.deltaTime * rotSpeed);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "PlayerFeet" || coll.tag == "Player")
        {
            if (isBad)
            {
                gameMG.GetComponent<ScoreManager>().AddScore(-50);
                gameMG.GetComponent<GameManager>().PlayHurtSound();
            } 
            else
            {
                gameMG.GetComponent<ScoreManager>().AddScore(50);
                gameMG.GetComponent<GameManager>().PlayCoinSound();
            }

            Destroy(this.gameObject);
        }
    }

    public void ForceGood()
    {
        forceGood = true;
    }
}
