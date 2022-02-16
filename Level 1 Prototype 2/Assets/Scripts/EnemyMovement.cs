using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int movementSpeed;
    public Transform[] moveSpots;
    private int currentSpot;

    // Start is called before the first frame update
    void Start()
    {
        currentSpot = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[currentSpot + 1].position, movementSpeed * Time.deltaTime);
    }

}
