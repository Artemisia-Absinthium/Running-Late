using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {

    private float rotSpeed = 1.5f;
    private GameObject gameMG;

    // Use this for initialization
    void Start () {
        gameMG = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update () {
        Quaternion rot = this.transform.rotation;
        rot.y += Time.deltaTime * rotSpeed;
        this.transform.rotation = rot;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "PlayerFeet" || coll.tag == "Player")
        {
            gameMG.GetComponent<ScoreManager>().AddScore(50);
            gameMG.GetComponent<GameManager>().PlayCoinSound();

            Destroy(this.gameObject);
        }
    }
}
