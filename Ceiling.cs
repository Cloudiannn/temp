using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ceiling : MonoBehaviour
{
    public GameObject mainCamera;
    // Update is called once per frame
    void Update()
    {
        if(transform.position.x - mainCamera.transform.position.x <= -13.5)
            transform.Translate(Vector3.right * 27);
    }
}
