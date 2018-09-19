using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPosition : MonoBehaviour {

    public GameObject agent = null;
    public GameObject target = null;
    public float minDistance = 2.0f;
    public float maxDistance = 4.0f;
    public bool fog = false;

    public Vector3 lookatRandomnes = new Vector3(0.05f, 0.05f, 0.05f); // Randomness, from 0 to 1


    public void newPositionAndRotation()
    {
        float fog_op = Random.Range(0.1f, 0.6f);
        Debug.Log(fog_op);
        if (fog)
        {
            RenderSettings.fogDensity = fog_op;
        }
        float phi = Random.Range(0, 2 * Mathf.PI);
        float theta = Random.Range(0, 2 * Mathf.PI);
        float r = Random.Range(minDistance, maxDistance);
        Vector3 newPosition = target.transform.position;
        Vector3 rotationVector = new Vector3(0, 0, 0);
        newPosition.x += r * Mathf.Cos(theta) * Mathf.Cos(phi);
        newPosition.y += Mathf.Min(r * Mathf.Cos(theta) * Mathf.Sin(phi), 11f);
        newPosition.z += r * Mathf.Sin(theta);
        rotationVector.x = Random.Range(-180, 180);
        rotationVector.y = Random.Range(-180, 180);
        rotationVector.z = Random.Range(-180, 180);
        rotationVector.Scale(lookatRandomnes);
        if (agent == null || target == null)
            return;

        agent.transform.position = newPosition;
        agent.transform.LookAt(target.transform.position);

        agent.transform.Rotate(rotationVector);
    }
}
