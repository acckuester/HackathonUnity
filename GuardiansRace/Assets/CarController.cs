using UnityEngine;

public class CarController : MonoBehaviour
{

    [SerializeField] float acceleration = 4;
    [SerializeField] float maxSpeed;
    [SerializeField] float turnFactor = 2;
    [SerializeField] float driftFactor = .95f;

    Rigidbody2D carRigidbody2D;
    float accelerationInput;
    float steeringInput;
    float rotationAngle;

    void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        accelerationInput = Input.GetAxis("Vertical");
        steeringInput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        ApplyEngineForce();
        KillLateralVelocity();
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

        if (accelerationInput == 0)
        {

            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 2, Time.fixedDeltaTime * 3);
        }
        else
        {

            carRigidbody2D.drag = 0.1f;
        }

        //Caculate how much "forward" we are going in terms of the direction of our velocity

        float velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

        //Limit so we cannot go faster than the max speed in the "forward" direction 

        if (velocityVsUp > maxSpeed && accelerationInput == 0)
            return;

        //Create a force for the engine
        Vector2 engineForceVector = transform.up * acceleration * accelerationInput;

        //Apply force and pushes the car forward
        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);

    }

    void ApplySteering()
    {

        //Update the rotation angle based on input
        rotationAngle -= steeringInput * turnFactor;

        //Apply steering by rotating the car object
        carRigidbody2D.MoveRotation(rotationAngle);
    }

    void KillLateralVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 lateralVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);
        carRigidbody2D.velocity = forwardVelocity * lateralVelocity * driftFactor;
    }
}