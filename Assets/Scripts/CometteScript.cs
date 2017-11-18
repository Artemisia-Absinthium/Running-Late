using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CometteScript : MonoBehaviour {

    private bool start;
    private float speed = 10.0f;
    private float delta;
    private float randStart;

    // Use this for initialization
    void Start () {
        start = true;
        randStart = Random.Range(1.0f, 2.5f);
    }
	
	// Update is called once per frame
	void Update () {
        if (start)
        {
            delta += Time.deltaTime;
            if(delta > randStart)
            {
                randStart = Random.Range(1.0f, 2.5f);
                Vector3 pos = this.transform.localPosition;
                pos.x = Random.Range(-1.5f, 5.0f);
                pos.y = 2.8f;
                this.transform.localPosition = pos;
                delta = 0.0f;
                start = false;
            }
        }
        else
        {
            Vector3 pos = this.transform.localPosition;
            pos.x -= Time.deltaTime * speed;
            pos.y -= Time.deltaTime * speed;
            this.transform.localPosition = pos;

            if (pos.y < -3.0f)
            {
                start = true;
            }
        }
	}
}
