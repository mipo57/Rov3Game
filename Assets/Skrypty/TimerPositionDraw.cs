using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerPositionDraw : MonoBehaviour {

    public RandomPosition positionDrawer;
    public float period = 1.0f; // period in seconds
    public bool timer = true;
    public bool active = true;

    float timeElapsed = 0.0f;
    int frames = 0;
	
	// Update is called once per frame
	void Update () {
        if (!active)
            return;
        if (timer)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed > period)
            {
                positionDrawer.newPositionAndRotation();
                timeElapsed = 0;
            }
        }
        else
        {
            if (frames % (int)period == 0)
            {
                positionDrawer.newPositionAndRotation();
                frames = 0;
            }
            frames++;
        }
    }
}
