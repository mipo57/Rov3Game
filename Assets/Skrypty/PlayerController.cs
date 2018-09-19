using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isKeyboardActive = true;
    private new Rigidbody rigidbody;
    private const float waterSurfacePosition = 11.45f;

    private Vector3 Sila = new Vector3(0, 0, 0);
    private Vector3 Moment = new Vector3(0, 0, 0);

    [Header("Forces Converters")]
    public Vector3 przelicznikSily = new Vector3(1, 1, 1);
    public Vector3 przelicznikMomentu = new Vector3(1, 1, 1);
    [Range(0.0f, 1.0f)]
    public float maxAngularVelocity = 0.1f;
    [Range(0.0f, 50.0f)]
    public float maxLineVelocity = 20.0f;
    [Space(10)]

    [Header("Random mess options")]
    public bool isRandomMessActive = true; // to decite if create random forces and torques
    [Range(0f, 100f)]
    public float randomForceRange = 1f; // range for mess forces
    [Range(0f, 0.2f)]
    public float randomTorqueRange = 0.01f; // range for mess torques
    [Range(1, 100)]
    public int PIDcycleDelay = 10; // number of loops to PID response for random force !!!! may not work corectly with Update, but shoud with FixedUpdate

    private Queue<Vector3> queueOfForceVectors = new Queue<Vector3>(); // queue of random forces - use to call "PID" "response"
    private Queue<Vector3> queueOfTorgueVectors = new Queue<Vector3>(); // queue of random torques

    private bool isDelay = false; // information to PID that it shoud start to work

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        int counter = 0; // for comparemation with PIDcycleDelay

        if (isKeyboardActive)
            sterowanieKlawiszami();


        if (waterSurfacePosition - transform.position[1] <= 0 && Sila.y > 0f) // stop going up when on surface
            Sila.y = 0;

        rigidbody.AddRelativeForce(Sila, ForceMode.Impulse);
        rigidbody.AddTorque(Moment, ForceMode.Impulse);


        queueOfForceVectors.Enqueue(ForceMess()); // generate random force and add to queue of this forces
        queueOfTorgueVectors.Enqueue(TorqueMess()); // generate random torque and add to queue of this torques

        if (!isDelay)
            if (counter == PIDcycleDelay)
                isDelay = true;
            else
                counter++;
        else
        {
            rigidbody.AddForce(queueOfForceVectors.Dequeue() * (-1f));
            rigidbody.AddTorque(queueOfTorgueVectors.Dequeue() * (-1f));
        }

        //Debug.Log(rigidbody.angularVelocity);
    }

    // set velocity using Vector
    public void setVelocity(Vector3 velocityVector)
    { 
        Sila = Vector3.Scale(velocityVector, przelicznikSily);
    }

    //set angular velocity using vector
    public void setAngularVelocity(Vector3 angularVelocityVector) 
    {
        Moment = Vector3.Scale(angularVelocityVector, przelicznikSily);
    }

    // to change only one cordinte
    public void setAngularVelocityYaxis(float angularVelocity) 
    {
        Moment.y = angularVelocity * przelicznikMomentu[1];
    }

    // instant stop rotate
    public void stopRotation() 
    {
        rigidbody.angularVelocity = Vector3.zero;
    }

    // instatnt stop move
    public void stopMove() 
    {
        rigidbody.velocity = Vector3.zero;
    }

    public float getDepth()
    {
        return waterSurfacePosition - transform.position[1];
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
        return rigidbody.rotation.eulerAngles; // return in eulerAngles
    }

    private void sterowanieKlawiszami()
    {
        Debug.Log("Player");
        Vector3 vectorForKeys = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W)) // go forward
            vectorForKeys.x = maxLineVelocity;
        else if (Input.GetKey(KeyCode.S)) // go backward
            vectorForKeys.x = -maxLineVelocity;
        else
            vectorForKeys.x = 0.0f;

        if (Input.GetKey(KeyCode.A)) // go left
            vectorForKeys.z = maxLineVelocity;
        else if (Input.GetKey(KeyCode.D)) // go right
            vectorForKeys.z = -maxLineVelocity;
        else
            vectorForKeys.y = 0f;

        if (Input.GetKey(KeyCode.R)) // go up
            vectorForKeys.y = maxLineVelocity;
        else if (Input.GetKey(KeyCode.F)) // go down
            vectorForKeys.y = -maxLineVelocity;
        else
            vectorForKeys.y = 0f;

        setVelocity(vectorForKeys);

        if (Input.GetKey(KeyCode.E)) // rotate left
        {
            setAngularVelocityYaxis(maxAngularVelocity);
        }
        else if (Input.GetKey(KeyCode.Q)) // rotate right
        {
            setAngularVelocityYaxis(-maxAngularVelocity);
        }
        else
            setAngularVelocityYaxis(0f);

        if (Input.GetKey(KeyCode.P)) // stop move
        {
            stopMove();
        }
        if (Input.GetKey(KeyCode.L)) // stop rotate
        {
            stopRotation();
        }

    }

    private Vector3 ForceMess()
    {
        Vector3 forceVector = new Vector3(Random.Range(-randomForceRange, randomForceRange),
            Random.Range(-randomForceRange, randomForceRange), Random.Range(-randomForceRange, randomForceRange));
        rigidbody.AddForce(forceVector);
        return forceVector;
    }

    private Vector3 TorqueMess()
    {
        Vector3 vectorOFTorque = new Vector3(Random.Range(-randomTorqueRange, randomTorqueRange),
            Random.Range(-randomTorqueRange, randomTorqueRange), Random.Range(-randomTorqueRange, randomTorqueRange));
        rigidbody.AddTorque(vectorOFTorque);
        return vectorOFTorque;
    }
}
