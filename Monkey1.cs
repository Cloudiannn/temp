using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Monkey1 : MonoBehaviour
{
    private Vector3 screenPosition;
    private Vector3 mousePositionOnScreen;
    private Vector3 mousePositionInWorld;
    public GameObject mcamera;
    public GameObject hookhead;
    public Rigidbody2D rb;
    public float cd;
    public Image cdBar;
    private float cdr;
    public Text cdText;
    private float lastX;
    private bool copied;
    private GameObject currentHook;
    public float hp;
    public Image hpBar;
    public Text hpText;
    private float hpR;
    public bool flashing;
    private float speed;
    public TrailRenderer trail;
    void Awake()
    {
        cdr = cd;
        hpR = hp;
        cd = 0;
        hpText.text = hp.ToString("f0");
        flashing = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(cd > 0)
        {
            cd -= Time.deltaTime;
            cdBar.fillAmount = 1 - cd/cdr;
            cdText.text = cd.ToString("f1");
        }
        else
        {
            if(!copied && !currentHook)
                CreateHook();
            if(Input.GetMouseButtonDown(0) && Time.timeScale != 0 && !flashing)
                LaunchHook();
            else
                lastX = transform.position.x;
        }
        if(flashing && Time.timeScale != 0)
        {
            transform.Translate(new Vector3(speed * Time.unscaledDeltaTime, 0, 0));
        }
    }

    public void Hurt(float damage)
    {
        hp -= damage;
        hpBar.fillAmount -= damage / hpR;
        hpText.text = hp.ToString("n");
    }

    public void Flash(float speeed)
    {
        this.gameObject.tag = "other";
        flashing = true;
        transform.localEulerAngles = new Vector3(0,0,0);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        speed = speeed;
        mcamera.GetComponent<SceneMoving>().speed = speeed;
        rb.gravityScale = 0;
        rb.velocity = new Vector3(0,0,0);
        trail.enabled = !trail.enabled;
        if(!copied && currentHook)
        {
            Destroy(currentHook);
        }
    }
    public void StopFlash()
    {
        this.gameObject.tag = "Player";
        flashing = false;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.velocity = new Vector3(3,1,0);
        mcamera.GetComponent<SceneMoving>().RefreshSpeed();
        rb.gravityScale = 0.5f;
        trail.enabled = !trail.enabled;
    }

    private void CreateHook()
    {
        currentHook = Instantiate(hookhead, transform.position, Quaternion.identity);
        currentHook.SetActive(false);
        copied = true;
        cdText.text = "Ready";
    }

    private void LaunchHook()
    {
                cd = cdr;
                screenPosition = Camera.main.WorldToScreenPoint(transform.position);
                mousePositionOnScreen = Input.mousePosition;
                mousePositionOnScreen.z = screenPosition.z;
                mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
                currentHook.SetActive(true);
                currentHook.transform.position = transform.position;
                Vector2 direction = mousePositionInWorld - currentHook.transform.position;
                currentHook.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90);
                copied = false;
    }
}
