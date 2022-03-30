using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelWin : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject inGame;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            winScreen.GetComponent<Canvas>().enabled = true;
            inGame.GetComponent<Canvas>().enabled = false;
            PlayerMovement.move = false;
        }
    }

    public void NextLevelButton()
    {
        //gets current level name ex: "Level 5" and the 7th char 
        //(6th index) in the string is the level number
        char temp = SceneManager.GetActiveScene().name[6];
        int next = 1 + (temp - '0');
        SceneManager.LoadScene("Level " + next);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayerMovement.move = true;
        PlayerEnergy.energy = 10;
    }
}
