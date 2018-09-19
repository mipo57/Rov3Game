using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "lodz")
        {
            Debug.Log("Brama zaliczona!");
            GameStateManager.getInstance().NextGate();
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
