using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookHead : MonoBehaviour
{
    public float speedh;
    private CapsuleCollider2D hookCollider;
    private bool triggered;
    private GameObject father;
    public float burstForce;
    private Rigidbody2D fatherRb;
    private Rigidbody2D hookRb;
    private float speedOffset;
    public ParticleSystem ptcl;
    void Start()
    {
        father = GameObject.Find("Monkey1");
        hookRb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!triggered)
            transform.Translate(Vector3.up * speedh * Time.deltaTime);
        if(Input.GetMouseButtonUp(0) && Time.timeScale != 0)
            Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "ceiling")
        {
            triggered = true;
            ptcl.Play();
            gameObject.AddComponent<HingeJoint2D>();
            fatherRb = father.GetComponent<Rigidbody2D>();
            gameObject.GetComponent<HingeJoint2D>().connectedBody = fatherRb;
            fatherRb.AddForce(transform.right * burstForce);
        }
    }

}
