using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour {

    public ROVModel boat;
    public bool active = false;
    public bool stopKeysActive = true;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.F10))
        {
            active = true;
            Debug.Log("Keyboard Active");
        }
        if (Input.GetKey(KeyCode.F11))
        {
            active = false;
            Debug.Log("Keyboard Inactive");
        }

        if (!stopKeysActive)
            return;

        if (Input.GetKey(KeyCode.P)) // stop move
            boat.stopMove();
        if (Input.GetKey(KeyCode.L)) // stop rotate
            boat.stopRotation();
        if (Input.GetKey(KeyCode.O)) // stop rotate
            boat.resetAngles();

        if (!active)
            return;
        Vector3 vectorForKeys = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W)) // go forward
            vectorForKeys.z += 1;
        if (Input.GetKey(KeyCode.S)) // go backward
            vectorForKeys.z -= 1;

        if (Input.GetKey(KeyCode.A)) // go left
            vectorForKeys.x -= 1;
        if (Input.GetKey(KeyCode.D)) // go right
            vectorForKeys.x += 1;

        if (Input.GetKey(KeyCode.R)) // go up
            vectorForKeys.y += 1;
        if (Input.GetKey(KeyCode.F)) // go down
            vectorForKeys.y -= 1;

        boat.setEngineForce(vectorForKeys);

        Vector3 engineTorqueVector = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.E)) // rotate left
            engineTorqueVector.y += 1;
        if (Input.GetKey(KeyCode.Q)) // rotate right
            engineTorqueVector.y -= 1;

        boat.setEngineTorque(engineTorqueVector);

       
    }
}
