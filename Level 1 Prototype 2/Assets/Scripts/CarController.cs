using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    [Header("Car settings")]
    public float accelerationFactor = 20.0f;
    public float turnFactor = 3.5f;

    public float driftFactor = .7f;
    public float maxSpeed = 20;

    public Text text;


    //local variables
    float accelerationInput = 0;
    float steeringInput = 0;
    float rotationAngle = -90;

    float velocityVsUp = 0;

    Rigidbody2D carRigidboy2D;
    LevelWin levelWin;


    bool[] checkpoints = { false, false, false, false};
    public static int lap = 1;

    void Awake()
    {
        levelWin = GetComponent<LevelWin>();
        carRigidboy2D = GetComponentInParent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        ApplyEngineForce();

        KillOrthogonalVelocity();

        ApplySteering();

        UIChange();
    }

    void ApplyEngineForce(){
        velocityVsUp = Vector2.Dot(transform.up, carRigidboy2D.velocity);

        if (velocityVsUp > maxSpeed && accelerationInput > 0)
            return;
        if (velocityVsUp < -maxSpeed && accelerationInput > 0)
            return;
        if (carRigidboy2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationFactor > 0)
            return;

        if (accelerationInput == 0)
            carRigidboy2D.drag = Mathf.Lerp(carRigidboy2D.drag, 3.0f, Time.fixedDeltaTime * 3);
        else carRigidboy2D.drag = 0;

        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;

        carRigidboy2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering(){
        float min = (carRigidboy2D.velocity.magnitude/8);

        min = Mathf.Clamp01(min);

        rotationAngle -= steeringInput * turnFactor * min;

        carRigidboy2D.MoveRotation(rotationAngle);
    }

    void KillOrthogonalVelocity(){
        Vector2 forwardVel = transform.up * Vector2.Dot(carRigidboy2D.velocity, transform.up);
        Vector2 rightVel = transform.right * Vector2.Dot(carRigidboy2D.velocity, transform.right);

        carRigidboy2D.velocity = forwardVel + rightVel * driftFactor;
    }

    float GetLateralVelocity(){
        return Vector2.Dot(transform.right, carRigidboy2D.velocity);
    }
    public void SetInputVector(Vector2 inputVector){
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }
    public bool IsTireScreeching(out float lateralVelocity, out bool isBreaking){
        lateralVelocity = GetLateralVelocity();
        isBreaking = false;

        if(accelerationInput < 0 && velocityVsUp > 0)
        {
            isBreaking = true;
            return true;
        }

        if(Mathf.Abs(GetLateralVelocity()) > 4.0f)
        {
            return true;
        }

        return false;

    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (lap == 3 && checkpoints[3] && gameObject.name == "Car")
            LevelWin.LevelWinScreen();
        if (checkpoints[0] && checkpoints[1] && checkpoints[2] && checkpoints[3])
        {
            lap++;
            checkpoints[0] = false;
            checkpoints[1] = false;
            checkpoints[2] = false;
            checkpoints[3] = false;
        }
            
        if (collision.gameObject.name == "CheckpointOne")
            checkpoints[0] = true;
        else if (collision.gameObject.name == "CheckpointTwo")
            checkpoints[1] = true;
        else if (collision.gameObject.name == "CheckpointThree")
            checkpoints[2] = true;
        else if (collision.gameObject.name == "Startline")
            checkpoints[3] = true;
    }

    private void UIChange()
    {
        if (text != null && text.gameObject.name == "Text")
        {
            text.text = "Lap: " + lap;
        }
        else
        {

        }
    }
}
