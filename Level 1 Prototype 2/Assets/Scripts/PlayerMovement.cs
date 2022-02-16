using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontalInput = 0;
    private float verticalInput = 0;
    public int movementSpeed;
    public static bool inSunnySpot = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        GetPlayerInput();
        MovePlayer();
    }

    private void GetPlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        Vector3 directionVector = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(directionVector.normalized * Time.deltaTime * movementSpeed);
    }

    void OnCollisionStay2D(Collision2D collision){
        if (collision.gameObject.tag == "SunnySpot")
        {
            inSunnySpot = true;
            PlayerEnergy.energy = Mathf.Min(PlayerEnergy.energy + (.5f * Time.deltaTime), 10.0f);
        } else {
            inSunnySpot = false;
        }
    }
}
