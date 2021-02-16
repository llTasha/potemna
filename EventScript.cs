using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventScript : MonoBehaviour
{
    PlayerCon player;
    ShopManager shop;
    SoundManager sound;
    Person_move person;

    GameObject Timer;
    GameObject Line;
    GameObject Stamina;
    GameObject Score;
    GameObject Fever;
    GameObject person_find;

    public float stamina_max = 1.00f;
    public float stamina_value = 0f;
    int Line_Count = 0;
    float Times_sec = 60f;
    float color_max = 1;
    bool timer_switch;
    int  score_count;
    int line_count;
    int fevergagemax;
    int total_combo = 1;
    int shop_price = 10;
    int count_plus = 0;


    public float Staminer;
    

    void Start()
    {
        sound = GameObject.FindObjectOfType<SoundManager>();
        shop = GameObject.FindObjectOfType<ShopManager>();
        player = GameObject.FindObjectOfType<PlayerCon>();
        Score = GameObject.Find("Score");
        Fever = GameObject.Find("GameView");

        Timer = Find.Find_obj(Global.timer);
        Line = Find.Find_obj(Global.linecount);

        fevergagemax = 50;

        Staminer = 1;
        timer_switch = true;
    }
    
    void Update()
    {
        // 상점이 꺼져잇을때 끄고 켜져잇을때 켜고
        //testbool = !transform.gameObject.activeSelf;

        ScoreSystem();


        // 상점이 열려 있을땐 타이머 정지
        if (shop.shop_open == false)
        {
            stamina_gauge();
            Timer_System();
        }
        else
        {
            // 상점에서 스태미너 충전 구매
            if (shop.index == 0)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    //돈이 있을떄
                    //20 > 40
                    if (player.person_tag.Count >= shop_price * (3 + count_plus))
                    {
                        Debug.Log("스테미너가");
                        stamina_max = 1;
                        Stamina.transform.GetChild(0).GetComponent<Text>().text = "Stamina : " + (stamina_max * 100f).ToString("N0") + "%";
                        Staminer = stamina_max;
                        Stamina.GetComponent<Image>().fillAmount = Staminer;
                        Stamina.GetComponent<Image>().color = new Color(1, color_max = 1, 0);
                        sound.sound_effect(1);
                        Debug.Log("충전되었습니다.");
                        //for(int i = 0; i > shop_price * (3 + count_plus); i++)
                        //{
                        //    player.person_tag.RemoveAt(player.person_tag.Count - i);
                        //}
                        count_plus++;
                    }
                    //돈이 없을때
                    else
                    {
                        Debug.Log("돈없음");
                    }
                }
            }
            // 상점에서 시간 충전 구매
            if (shop.index == 1)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Debug.Log("시간추가");
                    Times_sec += 30f;
                    Timer.transform.GetChild(0).GetComponent<Text>().text = Times_sec.ToString("N2");
                    sound.sound_effect(1);
                    Debug.Log("되었습니다.");
                }
            }
        }
        Line_Count_System();
    }
    void Timer_System()
    {
        if (timer_switch)
        {
            if (Times_sec <= 0.01f)
            {
                Timer.transform.GetChild(0).GetComponent<Text>().text = "Time Out";
            }
            else
            {
                
                Timer.transform.GetChild(0).GetComponent<Text>().text = Times_sec.ToString("N2");
                Times_sec -= Time.deltaTime;
            }
        }
    }

    void Line_Count_System()
    {
        Line_Count = player.person_tag.Count - 1;
        Line.transform.GetChild(0).GetComponent<Text>().text = "Line : " + Line_Count.ToString();
    }
    public void stamina_gauge()
    {

        Stamina = Find.Find_obj(Global.stamina_count);              // 스테미너 찾기
        stamina_max = Stamina.GetComponent<Image>().fillAmount;     // 스테미너 값
        Stamina.transform.GetChild(0).GetComponent<Text>().text = "Stamina : " + (stamina_max * 100f).ToString("N0") + "%";   // 스테미너 텍스트 출력
        if (timer_switch)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (Staminer >= 0)
                    Staminer -= 0.001f;
                if (color_max >= 0)
                    Stamina.GetComponent<Image>().color = new Color(1, color_max -= 0.001f, 0);

            }
            else
            {
                if (Staminer <= 1)
                    Staminer += 0.0002f;
                if (color_max <= 1)
                    Stamina.GetComponent<Image>().color = new Color(1, color_max += 0.0002f, 0);
            }
        }
        Stamina.GetComponent<Image>().fillAmount = Staminer;
    }



    int countman = 0;
    int countman2 = 0;
    bool endpoint;
    bool is_bool;
    void ScoreSystem()
    {
        line_count = (player.person_tag.Count - 1);
       
        countman = line_count * 100;
        FeverSystem();


        //Debug.Log("total_combo" + total_combo);

        if (total_combo == 1)
        {
            countman = line_count * 100;
        }
        else //if (total_combo > 1)
        {
            if (is_bool)
            {
                countman2 += ((line_count / 10) * (total_combo * 100));
                is_bool = false;
                //score_count = countman + countman2;
            }
        }
        score_count = countman + countman2;



        // 배수 표기
        if (total_combo >= 2)
            Score.transform.GetChild(1).GetComponent<Text>().text = "   X   " + total_combo.ToString();

        // 점수판 표기
        Score.transform.GetChild(0).GetComponent<Text>().text = score_count.ToString("0,000") + "   Pt       ";
    }


    int rePlayMax;
    int count = 1;
    void FeverSystem()
    {
        rePlayMax = (player.person_tag.Count - 1) / 50;
        // 게이지 찾을때 실행
        if (rePlayMax == count)
        {
            Fever.transform.GetChild(5).GetChild(1).GetComponent<Image>().fillAmount = 1f;
            // 피버중일때 실행
            if (Fever.transform.GetChild(5).GetChild(1).GetComponent<Image>().fillAmount != 0)
            {
                is_bool = true;
                total_combo++;
                sound.sound_effect(3);
            }   
            // 피버 아닐때 실행
            else
            {
                total_combo = 1;
            }
            count++;
        }
        else
        {
            if (count > 1)
            {
                Fever.transform.GetChild(5).GetChild(1).GetComponent<Image>().fillAmount -= 0.00025f;//0.00125f;
                if(Fever.transform.GetChild(5).GetChild(1).GetComponent<Image>().fillAmount == 0)
                    total_combo = 1;
            }
               
        }
        Fever.transform.GetChild(6).GetChild(1).GetComponent<Image>().fillAmount =(float) ((player.person_tag.Count)%50)/fevergagemax;
    }
}