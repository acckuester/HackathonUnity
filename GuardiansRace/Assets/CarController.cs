using UnityEngine;

public class CarController : MonoBehaviour
{

    [SerializeField] float acceleration = 4;
    [SerializeField] float maxSpeed = 100;
    [SerializeField] float turnFactor = 2;
    [SerializeField] float driftFactor = .95f;

    Rigidbody2D carRigidbody2D;
    protected float accelerationFactorInput;
    protected float steeringFactorInput;
    float rotationAngle;

    void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        ApplyEngineForce();
        ApplySteering();
    }

    // Gets called at the start of the collision 
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Entered collision with " + collision.gameObject.name);
    }

    // Gets called during the collision
    void OnCollisionStay(Collision collision)
    {
        Debug.Log("Colliding with " + collision.gameObject.name);
    }

    // Gets called when the object exits the collision
    void OnCollisionExit(Collision collision)
    {
        Debug.Log("Exited collision with " + collision.gameObject.name);
    }

    void ApplyEngineForce()
    {

        //Caculate how much "forward" we are going in terms of the direction of our velocity

        float velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

        //Limit so we cannot go faster than the max speed in the "forward" direction 

        if (velocityVsUp > maxSpeed && accelerationFactorInput == 0)
            return;

        //Create a force for the engine
        Vector2 engineForceVector = transform.up * acceleration * accelerationFactorInput;

        //Apply force and pushes the car forward
        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);

    }

    void ApplySteering()
    {

        //Update the rotation angle based on input
        rotationAngle -= turnFactor * steeringFactorInput;

        //Apply steering by rotating the car object
        carRigidbody2D.MoveRotation(rotationAngle);
    }
}