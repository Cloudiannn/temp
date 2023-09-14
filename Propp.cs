using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propp : MonoBehaviour
{
    private GameObject scoreSystem;
    public SpriteRenderer sprt;
    public ParticleSystem ptcl;
    public GameObject mcamera;
    public Collider2D cldr;
    public float speed;
    public int type;
    private float cd;
    private float tm;
    
    // Start is called before the first frame update
    void Start()
    {
        cd = 20;
        scoreSystem = GameObject.FindWithTag("score");
        tm = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale != 0 && cd >=10)
            transform.Translate(new Vector3(0,4 * (float)System.Math.Sin((Time.time - tm)*System.Math.PI) * speed * Time.deltaTime,0));
        if(mcamera.transform.position.x - transform.position.x >= 15)
            Destroy(this.gameObject);
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
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "other" && cd >=10)
        {
            switch(type)
            {
                case 0:
                {
                    scoreSystem.GetComponent<ScoreSystem>().Double();
                    break;
                }
                case 1:
                {
                    scoreSystem.GetComponent<ScoreSystem>().BulletTime();
                    break;
                }
                case 2:
                {
                    scoreSystem.GetComponent<ScoreSystem>().SuperScore();
                    break;
                }
                case 3:
                {
                    scoreSystem.GetComponent<ScoreSystem>().Flash();
                    break;
                }
                case 4:
                {
                    scoreSystem.GetComponent<ScoreSystem>().Shield();
                    break;
                }
            }
            cd = 3f;
            cldr.enabled = !cldr.enabled;
            sprt.enabled = !sprt.enabled;
            ptcl.Play();
        }
    }
}
