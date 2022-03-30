using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEnergy : MonoBehaviour
{
    public static float energy = 10.0f;

    private float time = 0f;

    public GameObject player;

    public GameObject panel;
    public static bool inSunnySpot = false;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!inSunnySpot)
        {
            time = Time.deltaTime;
            //Debug.Log(time);
            if (energy > 0.0f)
            {
                energy -= time;
                //Debug.Log(energy);
            }
            else
            {
                PlayerMovement.LostLevel("Ran out of Energy");

                //SceneManager.LoadScene("Level 1");
            }
        }
        else
        {
            energy = Mathf.Min(energy + (.5f * Time.deltaTime), 10.0f);
        }
        panel.transform.localScale = new Vector3((energy / 10) * 1, 1, 1);
    }
}
