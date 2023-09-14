using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IniSystem : MonoBehaviour
{
    public GameObject mcamera;

    private int propType;
    private int enemyType;

    public GameObject prop;
    private GameObject currentProp;

    public float propBaseCd;
    private float propCd;
    private float propCdOffset;
    public float[] propCdOffsetRange_PN;

    public GameObject enemy;
    private GameObject currentEnemy;

    public float enemyBaseCd;
    private float enemyCd;
    private float enemyCdOffset;
    public float[] enemyCdOffsetRange_PN;

    public float[] damage;
    // Start is called before the first frame update
    void Start()
    {
        propCd = 0;
        enemyCd = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(propCd > 0)
        {
            propCd -= Time.deltaTime;
        }
        if(enemyCd > 0)
        {
            enemyCd -= Time.deltaTime;
        }

        if(propCd <= 0)
        {
            propType = 4;//Random.Range(0,4);
            currentProp = Instantiate(prop, mcamera.transform.position + new Vector3(15,Random.Range(-0.8f,0), 10), Quaternion.identity);
            currentProp.GetComponent<Propp>().speed = Random.Range(0,1.0f);
            currentProp.GetComponent<Propp>().mcamera = mcamera;
            currentProp.GetComponent<Propp>().type = propType;
            
            propCdOffset = Random.Range(propCdOffsetRange_PN[0],propCdOffsetRange_PN[1]);
            propCd = propBaseCd + propCdOffset;
        }
        if(enemyCd <= 0)
        {
            enemyType = 0;//Random.Range(0,2);
            currentEnemy = Instantiate(enemy, mcamera.transform.position + new Vector3(12,Random.Range(-0.8f,0), 10), Quaternion.identity);
            currentEnemy.GetComponent<Enemyp>().speed = Random.Range(0,1.0f);
            currentEnemy.GetComponent<Enemyp>().mcamera = mcamera;
            currentEnemy.GetComponent<Enemyp>().type = enemyType;
            currentEnemy.GetComponent<Enemyp>().damage = damage[enemyType];
            
            enemyCdOffset = Random.Range(enemyCdOffsetRange_PN[0],enemyCdOffsetRange_PN[1]);
            enemyCd = enemyBaseCd + enemyCdOffset;
        }
        if(currentProp && currentEnemy)
            if(currentProp.transform.position.x - currentEnemy.transform.position.x >= -1 && currentProp.transform.position.x - currentEnemy.transform.position.x <= 0)
                currentEnemy.transform.position += new Vector3(1,0,0);
            else if(currentProp.transform.position.x - currentEnemy.transform.position.x <= 1 && currentProp.transform.position.x - currentEnemy.transform.position.x > 0)
                currentProp.transform.position += new Vector3(1,0,0);
    }
}
