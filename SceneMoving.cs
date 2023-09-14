using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMoving : MonoBehaviour
{
    public float speed;
    private float speedr;
    public GameObject player;
    public Rigidbody2D playerRb;
    public float speedOffsetCoefficient;


    void Start()
    {
        speedr = speed;
    }
    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x > transform.position.x)
        {
            float spd = speed + speedOffsetCoefficient * (player.transform.position.x - transform.position.x);
            transform.Translate(Vector3.right * spd * Time.deltaTime, Space.World);
            Debug.Log(spd);
        }
        else
            
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
    }

    public void RefreshSpeed()
    {
        speed = speedr;
    }
}
