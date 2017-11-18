using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour {

    private GameObject gameMG;

    // Use this for initialization
    void Start()
    {
        gameMG = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            if (!gameMG.GetComponent<PlayerManager>().isInvincible)
            {
                gameMG.GetComponent<GameManager>().GetTouch();
            }
        }
    }
}
