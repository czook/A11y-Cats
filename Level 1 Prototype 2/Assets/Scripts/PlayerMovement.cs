using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private float horizontalInput = 0;
    private float verticalInput = 0;
    public int movementSpeed;
    public static bool inSunnySpot = false;
    public static bool move = true;
    public static GameObject lostScreen;
    public static GameObject inGame;
    // Start is called before the first frame update
    void Start()
    {
        lostScreen = GameObject.Find("LosingScreenCanvas");
        inGame = GameObject.Find("InGameUICanvas");
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
        if (move == true)
        {
            Vector3 directionVector = new Vector3(horizontalInput, verticalInput, 0);
            transform.Translate(directionVector.normalized * Time.deltaTime * movementSpeed);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            LostLevel("PlayerTrigger");
        }
    }

    public static void LostLevel(string reason)
    {
        lostScreen.GetComponent<Canvas>().enabled = true;
        inGame.GetComponent<Canvas>().enabled = false;
        Debug.Log(reason);
        move = false;
    }
}
