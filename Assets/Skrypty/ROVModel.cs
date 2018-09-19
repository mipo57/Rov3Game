using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROVModel : MonoBehaviour {

    private new Rigidbody rigidbody;
    private const float waterSurfacePosition = 11.45f;

    private Vector3 Force = new Vector3(0, 0, 0);
    private Vector3 Torque = new Vector3(0, 0, 0);

    [Header("Forces Converters")]
    public Vector3 forceRatios = new Vector3(1, 1, 1);
    public Vector3 torqueRatios = new Vector3(1, 1, 1);
    [Range(0.0f, 1.0f)]
    public float maxAngularVelocity = 0.1f;
    [Range(0.0f, 50.0f)]
    public float maxLineVelocity = 20.0f;
    [Space(10)]

    [Header("Random forces options")]
    public bool areRandomForcesActive = true; // to decide wheter to create random forces and torques
    [Range(0f, 100f)]
    public float randomForceRange = 1f; // range for random forces
    [Range(0f, 0.2f)]
    public float randomTorqueRange = 0.01f; // range for random torques
    [Range(1, 100)]
    public int PIDcycleDelay = 10; // number of loops to PID response for random force !!!! may not work corectly with Update, but shoud with FixedUpdate

    [Header("Pseudo PID options")]
    public bool isPIDactive = true; // for pid, which fix rotation
    public float maxInclination = 0.16f; // if boat exceed this angle value - it will reset angles

    private Queue<Vector3> queueOfForceVectors = new Queue<Vector3>(); // queue of random forces - use to call "PID" "response"
    private Queue<Vector3> queueOfTorgueVectors = new Queue<Vector3>(); // queue of random torques

    private bool isDelay = false; // information to PID that it shoud start to work
    private Quaternion startEulerAngles;
    private void Start()
    {
        Debug.Log("Starting");
        rigidbody = GetComponent<Rigidbody>();
        startEulerAngles =  transform.rotation;
        float startAngleYaw = transform.rotation[1];
    }

    void FixedUpdate()
    {
        int counter = 0; // for comparemation with PIDcycleDelay

        rigidbody.AddRelativeForce(Force, ForceMode.Impulse);
        rigidbody.AddTorque(Torque, ForceMode.Impulse);


        queueOfForceVectors.Enqueue(randomForce()); // generate random force and add to queue of this forces
        queueOfTorgueVectors.Enqueue(randomTorque()); // generate random torque and add to queue of this torques

        if (!isDelay)
        {
            if (counter == PIDcycleDelay)
                isDelay = true;
            else
                counter++;
        }
        else
        {
            rigidbody.AddForce(queueOfForceVectors.Dequeue() * (-1f));
            rigidbody.AddTorque(queueOfTorgueVectors.Dequeue() * (-1f));
        }
        if(isPIDactive)
            pseudoPid();
        //Debug.Log(rigidbody.angularVelocity);

        if (waterSurfacePosition - transform.position[1] <= 0) {
            Vector3 new_pos = transform.position;
            new_pos.y = waterSurfacePosition;

            transform.position = new_pos;
        }
    }

    public void setEngineForce(Vector3 force)
    {
        force.x = Mathf.Clamp(force.x, -1, 1);
        force.y = Mathf.Clamp(force.y, -1, 1);
        force.z = Mathf.Clamp(force.z, -1, 1);

        Force = Vector3.Scale(force, forceRatios) * maxLineVelocity;
    }

    public void setEngineTorque(Vector3 torque)
    {
        torque.x = Mathf.Clamp(torque.x, -1, 1);
        torque.y = Mathf.Clamp(torque.y, -1, 1);
        torque.z = Mathf.Clamp(torque.z, -1, 1);

        Torque = Vector3.Scale(torque, torqueRatios) * maxAngularVelocity;
    }

    // set velocity using Vector
    public void setVelocity(Vector3 velocityVector)
    {
        Force = Vector3.Scale(velocityVector, forceRatios);
    }
        
    //set angular velocity using vector
    public void setAngularVelocity(Vector3 angularVelocityVector)
    {
        Torque = Vector3.Scale(angularVelocityVector, forceRatios) * maxAngularVelocity;
    }

    // change only one coordinate
    public void setAngularVelocityYaxis(float angularVelocity)
    {
        //Debug.Log(angularVelocity);
        Torque.y = angularVelocity * torqueRatios[1] * maxAngularVelocity ;
    }

    // instantly stop rotation
    public void stopRotation()
    {
        rigidbody.angularVelocity = Vector3.zero;
    }

    // instatnty stop moving
    public void stopMove()
    {
        rigidbody.velocity = Vector3.zero;
    }

    public void resetAngles()
    {
        //rigidbody.rotation.eulerAngles.Set(0,0,0);
        Quaternion currAngles = transform.rotation;
        currAngles[0] = startEulerAngles[0];
        currAngles[2] = startEulerAngles[2];
        Debug.Log(transform.rotation);
        transform.rotation = currAngles;
    }

    public float getDepth()
    {
        float depth = (waterSurfacePosition - transform.position[1]) * 100;
        if (depth < 0)
            depth = 0;
        return depth;
    }

    public void pseudoPid()
    {
        Quaternion angles = transform.rotation;
        if (angles[0] > maxInclination || angles[0] < -maxInclination)
            resetAngles();
        if (angles[2] > maxInclination || angles[2] < -maxInclination)
            resetAngles();
    }

    public Vector3 getLinearVelocity()
    {
        return rigidbody.velocity;
    }

    public Vector3 getAngularVelocity()
    {
        return rigidbody.angularVelocity;
    }

    public Vector3 getAngularPosition()
    {
        return rigidbody.rotation.eulerAngles; 
    }

    private Vector3 randomForce()
    {
        Vector3 forceVector = new Vector3(Random.Range(-randomForceRange, randomForceRange),
            Random.Range(-randomForceRange, randomForceRange), Random.Range(-randomForceRange, randomForceRange));
        rigidbody.AddForce(forceVector);
        return forceVector;
    }

    private Vector3 randomTorque()
    {
        Vector3 vectorOFTorque = new Vector3(Random.Range(-randomTorqueRange, randomTorqueRange),
            Random.Range(-randomTorqueRange, randomTorqueRange), Random.Range(-randomTorqueRange, randomTorqueRange));
        rigidbody.AddTorque(vectorOFTorque);
        return vectorOFTorque;
    }
}
