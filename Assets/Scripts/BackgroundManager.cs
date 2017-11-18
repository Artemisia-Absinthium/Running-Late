using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour {

    public GameObject background1;
    public GameObject background2;
    public float backgorundSpeed;

    private float backgroundSize;
    internal bool isPlaying;

    // Use this for initialization
    void Start () {
        backgroundSize = background1.GetComponent<Renderer>().bounds.size.x;
        isPlaying = false;
    }

    // Update is called once per frame
    void Update () {
        if (isPlaying)
        {
            moveBackground(background1);
            moveBackground(background2);
        }
    }

    private void moveBackground(GameObject backObject)
    {
        //
        // Update the position
        Vector3 position = backObject.transform.position;
        position.x -= Time.deltaTime * backgorundSpeed;
        backObject.transform.position = position;

        //
        // Check the position
        if (position.x < -backgroundSize)
        {
            position.x += 2 * backgroundSize -1; //-1 is here for perfect clamping
            backObject.transform.position = position;
        }
    }
}
