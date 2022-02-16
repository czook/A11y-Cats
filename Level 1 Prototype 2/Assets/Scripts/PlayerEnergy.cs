using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    public static float energy = 10.0f;

    private float time = 0f;

    public GameObject player;

    public GameObject panel;


    // Start is called before the first frame update
    void Start(){
    }

    // Update is called once per frame
    void Update()
    {
        if(!PlayerMovement.inSunnySpot){
            time = Time.deltaTime;
            //Debug.Log(time);
            if(energy>0.0f){
                energy -= time;
                Debug.Log(energy);
                panel.transform.localScale = new Vector3((energy/10)*1,1,1);
            }
        }
    }
}
