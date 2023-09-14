using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyp : MonoBehaviour
{
    public float speed;
    public float damage;
    private float tm;
    public SpriteRenderer sprt;
    public ParticleSystem ptcl;
    public GameObject mcamera;
    private float cd;
    public int type;
    // Start is called before the first frame update
    void Start()
    {
        cd = 20;
        tm = Time.time;
        speed = Random.Range(0,1.0f);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(mcamera.transform.position.x - transform.position.x >= 15)
            Destroy(this.gameObject);
        transform.Translate(new Vector3(0,4 * (float)System.Math.Sin((Time.time - tm)*System.Math.PI) * speed * Time.deltaTime,0));
        if(cd>0 && cd < 10)
        {
            cd -= Time.deltaTime;
        }
        else if(cd<=0)
        {
            
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && cd >=10)
        {
            other.gameObject.GetComponent<Monkey1>().Hurt(damage);
            cd = 3f;
            sprt.enabled = !sprt.enabled;
            ptcl.Play();
        }
        else if(other.gameObject.tag == "other" && cd >=10)
        {
            cd = 3f;
            sprt.enabled = !sprt.enabled;
            ptcl.Play();
        }
    }
}
