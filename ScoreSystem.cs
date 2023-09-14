using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    private float score;
    public GameObject player;
    public Text txt;
    private float[] cd; 
    private float[] cdr; 
    private float downCountingCd;
    private int i;
    public int scorePerSec;
    private int scorePerSecR;
    public RectTransform[] buffPanel;
    public string[] buffName;
    public Image[] buffBar;
    public Text[] buffText;
    public GameObject pausePanel;
    public Text supText;
    public Text pauseText;
    private bool downCounting;
    private bool flashing;
    public GameObject shield;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        i = 0;
        cd = new float[10] {0,0,0,0,0,0,0,0,0,0};
        cdr = new float[10] {10,5,3,1.5f,6,0,0,0,0,0};
        scorePerSecR = scorePerSec;
        pauseText.text = "Paused";
        pausePanel.SetActive(true);
        downCountingCd = 4;
        downCounting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(downCountingCd > 0 && downCountingCd <= 3)
        {
            downCountingCd -= Time.unscaledDeltaTime;
            if(downCountingCd <= 2 && pauseText.text == "3")
                pauseText.text = "2";
            else if(downCountingCd <= 1 && pauseText.text == "2")
                pauseText.text = "1";
        }
        if(downCountingCd <=0 && downCounting)
        {
                if(cd[1] < 0 || cd[1] > cdr[1])
                    Time.timeScale = 1;
                else
                    Time.timeScale = 0.5f;
                pausePanel.SetActive(false);
                downCounting = false;
        }
        if((downCountingCd <=0 || downCountingCd > 3) && Input.GetButtonDown("Jump"))
            if(Time.timeScale == 0)
            {
                downCountingCd = 3;
                pauseText.text = "3";
                downCounting = true;
            }
            else
            {
                Time.timeScale = 0;
                pauseText.text = "Paused";
                pausePanel.SetActive(true);
            }
        if(Time.timeScale == 0.5f && cd[1] < 0)
        {
            Time.timeScale = 1;
        }
        else if(Time.timeScale == 1 && cd[1] >= 0)
        {
            Time.timeScale = 0.5f; 
        }
        if(scorePerSec != scorePerSecR && cd[0] < 0)
        {
            scorePerSec /= 2;
            txt.color = Color.black;
        }
        else if(scorePerSec == scorePerSecR && cd[0] >= 0)
        {
            scorePerSec *= 2; 
            txt.color = Color.red;
        }
        if(cd[3] < 0 && flashing)
        {
            player.GetComponent<Monkey1>().StopFlash(); 
            flashing = false;
        }
        else
        {
            score += Time.deltaTime*9*scorePerSec;
        }
        if(cd[4] < 0 && shield)
        {
            shield.SetActive(false);
            player.tag = "Player";
        }
        score += Time.deltaTime * scorePerSec;
        txt.text = "score：" + score.ToString("f0");
        int j = 0;
        for(i = 0;i<5;i++)
            if(cd[i]>=0)
            {
                cd[i] -= Time.deltaTime;
                if(i != 2)
                {
                    if(!buffPanel[i].gameObject.activeSelf)
                    {
                        buffPanel[i].gameObject.SetActive(true);
                    }
                    buffPanel[i].anchoredPosition = new Vector2(buffPanel[i].anchoredPosition.x, 287 - j * 130);
                    buffBar[i].fillAmount = cd[i] / cdr[i];
                    buffText[i].text = buffName[i] + "\n" + cd[i].ToString("f1");
                    j++;
                }
            }
            else 
            {
                if(i != 2)
                {   
                    if(buffPanel[i].gameObject.activeSelf)
                        buffPanel[i].gameObject.SetActive(false);
                }
                else
                    supText.text = "";
            }
                
                
    }
    public void Double()
    {
        cd[0] = cdr[0];
    }
    public void BulletTime()
    {
        cd[1] = cdr[1];
    }
    public void SuperScore()
    {
        int s = Random.Range(200,2000);
        score += s;
        supText.text = "+" + s.ToString("f0");
        cd[2] = cdr[2];
    }
    public void Flash()
    {
        cd[3] = cdr[3];
        player.GetComponent<Monkey1>().Flash(15);
        flashing = true;
    }
    public void Shield()
    {
        cd[4] = cdr[4];
        shield.SetActive(true);
        shield.GetComponent<Shield>().times = 1;
        player.tag = "other";
    }
    public void UnShield()
    {
        cd[4] = 0;
        score += 500;
    }
}
