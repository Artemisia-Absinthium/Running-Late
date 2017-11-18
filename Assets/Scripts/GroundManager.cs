using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour {

    public GameObject ground1;
    public GameObject ground2;
    public GameObject spawnPosition;
    public float groundSpeed;

    private float groundSize;
    internal bool isPlaying;

    // Use this for initialization
    void Start()
    {
        //
        // Compute the prefab size
        Bounds groundBounds = new Bounds();
        foreach (Transform child in ground1.transform)
        {
            groundBounds.Encapsulate(child.GetComponent<Renderer>().bounds);
        }
        groundSize = groundBounds.size.x;

        //
        // Init pos
        Vector3 position = ground1.transform.position;
        position.x += groundSize - 1;
        ground2.transform.position = position;

        isPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            moveGround(ground1);
            moveGround(ground2);
        }
    }

    private void moveGround(GameObject backObject)
    {
        //
        // Update the position
        Vector3 position = backObject.transform.position;
        position.x -= Time.deltaTime * groundSpeed;
        backObject.transform.position = position;

        //
        // Check the position
        if (position.x < -groundSize)
        {
            position.x += 2 * groundSize - 2; //-2 is here for perfect clamping
            backObject.transform.position = position;
        }
    }
}
