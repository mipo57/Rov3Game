using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObjectController : MonoBehaviour {

    public GameObject target;
    public List<GameObject> floatingObjects;

    public List<RandomPosition> generators;

    public float minDistance = 1;
    public float maxDistance = 4;

    public bool active = true;
    public float period = 1.0f; // period in seconds
    public bool timer = true;

    float timeElapsed = 0.0f;
    int frames = 0;
    
    // Use this for initialization
    void Start () {
		foreach(GameObject floatingObject in floatingObjects)
        {
            RandomPosition randomPosition = floatingObject.AddComponent<RandomPosition>();

            randomPosition.target = target;
            randomPosition.lookatRandomnes = new Vector3(1,1,1);
            randomPosition.minDistance = minDistance;
            randomPosition.maxDistance = maxDistance;
            randomPosition.agent = floatingObject;
        }
	}

    void updatePositions()
    {
        foreach (GameObject floatingObject in floatingObjects)
        {
            RandomPosition randomPosition = floatingObject.GetComponent<RandomPosition>();
            randomPosition.newPositionAndRotation();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (!active)
            return;

        if (timer)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed > period)
            {
                updatePositions();
                timeElapsed = 0;
            }
        }
        else
        {
            if (frames % (int)period == 0)
            {
                updatePositions();
                frames = 0;
            }
            frames++;
        }
    }
}
