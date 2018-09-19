using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {

    public int number = 0;

    private bool enabled = true;

	// Use this for initialization
	void Start () {
        GameStateManager.getInstance().registerGate(this);
	}

    void disableChildren()
    {
        if (!enabled)
            return;

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }

        enabled = false;
    }

    void enableChildren()
    {
        if (enabled)
            return;

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }

        enabled = true;
    }

    // Update is called once per frame
    void Update () {
        if (GameStateManager.getInstance().getActiveGate() != number)
            disableChildren();
        else
            enableChildren();
    }
}
