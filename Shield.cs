using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float speed;
    private Quaternion myRotation;
    public int times;
    public GameObject scoreSystem;
    // Update is called once per frame
    void Update()
    {
        transform.rotation = myRotation;
        transform.Rotate(0,0,speed * Time.deltaTime);
        myRotation = transform.rotation;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "enemy")
        {
            times--;
            if(times == 0)
            {
                scoreSystem.GetComponent<ScoreSystem>().UnShield();
            }
        }
    }
}
