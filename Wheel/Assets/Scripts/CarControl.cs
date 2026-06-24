using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class CarControl : MonoBehaviour
{
    [Header("Car Properties")]
    public float motorTorque = 200.0f;
    public float brakeTorque = 2000f;
    public float maxSpeed = 20f;
    public float steeringRange = 30f;
    public float steeringRangeAtMaxSpeed = 10f;
    public float centreOfGravityOffset = -1f;

    private WheelControl[] wheels;
    private Rigidbody rigidBody;

    private CarInputActions carControls;

    private bool isAPressed;
    private bool isSPressed;
    private bool isKPressed;
    private bool isLPressed;
    private bool isGearPressed;

    void Awake()
    {
        carControls = new CarInputActions();
    }
    void OnEnable()
    {
        carControls.Enable();
    }

    void OnDisable()
    {
        carControls.Disable();
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        // Adjust center of mass to improve stability and prevent rolling
        Vector3 centerOfMass = rigidBody.centerOfMass;
        centerOfMass.y += centreOfGravityOffset;
        rigidBody.centerOfMass = centerOfMass;

        wheels = GetComponentsInChildren<WheelControl>();
        foreach(WheelControl wheel in wheels){
            wheel.WheelCollider.brakeTorque = 0f;
        }
    }

    void Update()
    {
        isAPressed = carControls.Car.FL.IsPressed();
        isSPressed = carControls.Car.FR.IsPressed();
        isKPressed = carControls.Car.BL.IsPressed();
        isLPressed = carControls.Car.BR.IsPressed();
        isGearPressed = carControls.Car.Gear.IsPressed();
    }

    void FixedUpdate()
    {
        Vector2 inputVector = carControls.Car.Movement.ReadValue<Vector2>();
        float gear = carControls.Car.Switch.ReadValue<float>();

        float vInput = inputVector.y;
        float hInput = inputVector.x;

        // Calculate current speed along the car's forward axis
        float forwardSpeed = Vector3.Dot(transform.forward, rigidBody.linearVelocity);
        float speedFactor = Mathf.InverseLerp(0, maxSpeed, Mathf.Abs(forwardSpeed)); //Normalize

        // Reduce motor torque and steering at high speeds for better handling
        float currentMotorTorque = Mathf.Lerp(motorTorque, -motorTorque, speedFactor);
        float currentSteerRange = Mathf.Lerp(steeringRange, steeringRangeAtMaxSpeed, speedFactor);

        // Determine if the player is accelerating or trying to reverse
        //bool isAccelerating = Mathf.Sign(vInput) == Mathf.Sign(forwardSpeed);

        foreach (var wheel in wheels)
        {
            //if (wheel.steerable)
            //{
            //    wheel.WheelCollider.steerAngle = hInput * currentSteerRange;
            //}
            //wheel.WheelCollider.motorTorque = vInput * currentMotorTorque;
            // Release brakes when accelerating
            wheel.WheelCollider.brakeTorque = 0f;
        }

        //Hardcoded stuff
        //print(hInput);
        //if (gear > 0.0f)
        //{
        //    if (vInput > 0)
        //    {
        //        wheels[0].WheelCollider.motorTorque = Mathf.Lerp(wheels[0].WheelCollider.motorTorque, -motorTorque, 0.2f);
        //        wheels[0].WheelCollider.steerAngle = Mathf.Lerp(wheels[0].WheelCollider.steerAngle, -5.0f, 0.2f);
        //    }
        //    if (vInput < 0)
        //    {
        //        wheels[1].WheelCollider.motorTorque = Mathf.Lerp(wheels[1].WheelCollider.motorTorque, -motorTorque, 0.2f);
        //        wheels[1].WheelCollider.steerAngle = Mathf.Lerp(wheels[1].WheelCollider.steerAngle, 5.0f, 0.2f);
        //    }
        //    if (hInput < 0)
        //    {
        //        wheels[2].WheelCollider.motorTorque = Mathf.Lerp(wheels[2].WheelCollider.motorTorque, -motorTorque, 0.2f);
        //        wheels[2].WheelCollider.steerAngle = Mathf.Lerp(wheels[2].WheelCollider.steerAngle, 5.0f, 0.2f);
        //    }
        //    if (hInput > 0)
        //    {
        //        wheels[3].WheelCollider.motorTorque = Mathf.Lerp(wheels[3].WheelCollider.motorTorque, -motorTorque, 0.2f);
        //        wheels[3].WheelCollider.steerAngle = Mathf.Lerp(wheels[3].WheelCollider.steerAngle, -5.0f, 0.2f);
        //    }
        //}
        //else
        //{
        //    if (vInput > 0)
        //    {
        //        wheels[0].WheelCollider.motorTorque = Mathf.Lerp(wheels[0].WheelCollider.motorTorque, motorTorque, 0.2f);
        //        wheels[0].WheelCollider.steerAngle = Mathf.Lerp(wheels[0].WheelCollider.steerAngle, 5.0f, 0.2f);
        //    }
        //    if (vInput < 0)
        //    {
        //        wheels[1].WheelCollider.motorTorque = Mathf.Lerp(wheels[1].WheelCollider.motorTorque, motorTorque, 0.2f);
        //        wheels[1].WheelCollider.steerAngle = Mathf.Lerp(wheels[1].WheelCollider.steerAngle, -5.0f, 0.2f);
        //    }
        //    if (hInput < 0)
        //    {
        //        wheels[2].WheelCollider.motorTorque = Mathf.Lerp(wheels[2].WheelCollider.motorTorque, motorTorque, 0.2f);
        //        wheels[2].WheelCollider.steerAngle = Mathf.Lerp(wheels[2].WheelCollider.steerAngle, -5.0f, 0.2f);
        //    }
        //    if (hInput > 0)
        //    {
        //        wheels[3].WheelCollider.motorTorque = Mathf.Lerp(wheels[3].WheelCollider.motorTorque, motorTorque, 0.2f);
        //        wheels[3].WheelCollider.steerAngle = Mathf.Lerp(wheels[3].WheelCollider.steerAngle, 5.0f, 0.2f);
        //    }
        //    //wheels[2].WheelCollider.motorTorque = Mathf.Lerp(wheels[0].WheelCollider.motorTorque, currentMotorTorque, 2.0f);
        //    //wheels[3].WheelCollider.motorTorque = Mathf.Lerp(wheels[0].WheelCollider.motorTorque, currentMotorTorque, 2.0f);
        //}
        print("front left: " + wheels[0].WheelCollider.motorTorque);
        print("front right: " + wheels[1].WheelCollider.motorTorque);
        print("back left: " + wheels[2].WheelCollider.motorTorque);
        print("back right: " + wheels[3].WheelCollider.motorTorque);
        //if(vInput > 1)
        //{
        //    wheels[0].WheelCollider.brakeTorque = 0f;
        //    wheels[0].WheelCollider.motorTorque = Mathf.Lerp(wheels[0].WheelCollider.motorTorque, currentMotorTorque, 0.2f);
        //}

        for (int i = 0; i < wheels.Length; i++)
        {
            // 1. Determine which key belongs to which wheel index
            bool isWheelKeyPressed = false;
            if (i == 0) isWheelKeyPressed = isAPressed; // Front Left
            if (i == 1) isWheelKeyPressed = isSPressed; // Front Right
            if (i == 2) isWheelKeyPressed = isKPressed; // Back Left
            if (i == 3) isWheelKeyPressed = isLPressed; // Back Right

            // 2. Apply the independent torque and steer logic for this specific wheel
            if (isWheelKeyPressed && isGearPressed)
            {
                wheels[i].WheelCollider.motorTorque = Mathf.Lerp(wheels[i].WheelCollider.motorTorque, -motorTorque, 0.2f);
                wheels[i].WheelCollider.steerAngle = Mathf.Lerp(wheels[i].WheelCollider.steerAngle, -5.0f, 0.2f);
            }
            else if (isWheelKeyPressed)
            {
                wheels[i].WheelCollider.motorTorque = Mathf.Lerp(wheels[i].WheelCollider.motorTorque, motorTorque, 0.2f);
                wheels[i].WheelCollider.steerAngle = Mathf.Lerp(wheels[i].WheelCollider.steerAngle, 5.0f, 0.2f);
            }
            else
            {
                // Smoothly decelerate torque back to zero when this wheel's key is released
                wheels[i].WheelCollider.motorTorque = Mathf.MoveTowards(wheels[i].WheelCollider.motorTorque, 0f, brakeTorque * Time.fixedDeltaTime);
                
                // Smoothly straighten the steering angle back to center
                wheels[i].WheelCollider.steerAngle = Mathf.MoveTowards(wheels[i].WheelCollider.steerAngle, 0f, 15f * Time.fixedDeltaTime);
            }
        }
    }
}
