using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour {

    public int KeyColor;
    public GameObject coinPrefab;

    private GameObject gameMG;

    // Use this for initialization
    void Start () {
        gameMG = GameObject.Find("GameManager");

        //
        // Check key
        if(KeyColor != gameMG.GetComponent<GameManager>().keyStatue+1 
            || GameObject.Find("GameManager").GetComponent<ScoreManager>().currentScore < 300 * KeyColor)
        {
            if (KeyColor != 4)
            {
                //
                // Change to a coin
                GameObject obj = (GameObject)Instantiate(coinPrefab,
                    this.transform.position,
                    Quaternion.identity);
                obj.transform.parent = this.transform.parent;

                if (KeyColor == 2)
                {
                    obj.GetComponent<CoinScript>().ForceGood();
                }
            }

            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "PlayerFeet" || coll.tag == "Player")
        {
            if (KeyColor == gameMG.GetComponent<GameManager>().keyStatue + 1)
            {
                gameMG.GetComponent<GameManager>().AddKey(KeyColor);
            }
            gameMG.GetComponent<GameManager>().PlayKeySound();
  
            Destroy(this.gameObject);
        }
    }
}
