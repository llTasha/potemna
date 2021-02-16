using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ShopManager : MonoBehaviour
{
    SoundManager sound;

    GameObject player;  // 플레이어
    GameObject shop_Onbutton;
    GameObject ShopMenu;
    GameObject shopselecter;
    GameObject Items;
    GameObject ShopInfoText;

    RaycastHit hit;
    bool shop_button;

    // 상점 오픈
    int shopselecter_move_count;
    public bool shop_open;
    public int index;


    public void Start()
    {
        sound           = GameObject.FindObjectOfType<SoundManager>();
        player          = Find.Find_obj("Player");
        shop_Onbutton   = Find.Find_obj("Shop").transform.GetChild(0).gameObject;    // 상점을 여는버튼
        ShopMenu        = Find.Find_obj("Shop").transform.GetChild(1).gameObject;    // 상점
        shopselecter    = ShopMenu.transform.GetChild(2).gameObject;                 // 상점에서 고르는거
        Items           = ShopMenu.transform.GetChild(3).gameObject;                 // 상점 아이템
        ShopInfoText    = ShopMenu.transform.GetChild(4).GetChild(0).gameObject;     // 상점 안에 설명 해주는 텍스트

        //shop_Onbutton.SetActive(false);
        //ShopMenu.SetActive(false);
        // 상점 버튼 처음에는 안보임
        shop_open = false;                  // 상점 들어오기 까지는 안보임

        

        StartCoroutine("shop_find");
        StartCoroutine("TextMoveEffect");
    }
    void Update()
    {
        if (shop_open == true)
            shopselecter_init();
    }


    IEnumerator shop_find()         // 상점을 찾음
    {
        while (true)
        {
            // 밑바닥에 레이저쏴서 상점인걸 발견할때
            if (Physics.Raycast(player.transform.position, new Vector3(0, -1, 0), out hit, 1.3f))
            {
                //Debug.DrawRay(hit.point, hit.normal, Color.blue, 5f);
                if (hit.collider.name == "Shop_pre")
                {
                    shop_Onbutton.SetActive(true);
                    shop_openUp_keycode();
                    //Debug.Log("상점에 들어왔다.");
                }
                else
                {
                    shop_Onbutton.SetActive(false);
                    //Debug.Log("상점에 나갔다");
                }
            }
            yield return null;
        }
    }

    // 상점 버튼 누르면 실행함
    public void shop_openUp()
    {
        shop_open = true;
        ShopMenu.SetActive(true);
        sound.sound_effect(0);
        Debug.Log("상점열음");
      //  return true;
    }
    void shop_openUp_keycode()
    {
        if (shop_open == false)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                shop_open = true;
                ShopMenu.SetActive(true);
                sound.sound_effect(0);
            }
        }
    }



    // 상점 끄는 버튼
    public void shop_close()
    {
        shop_open = false;
        ShopMenu.SetActive(false);
        sound.sound_effect(0);
    }
    public void shopselecter_init()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (index == 2)
            {
                index = 0;
                shopselecter.transform.position = Items.transform.GetChild(index).position;
                shopinfo(index);
            }
            else
            {
                index++;
                shopselecter.transform.position = Items.transform.GetChild(index).position;
                shopinfo(index);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (index == 0)
            {
                index = 2;
                shopselecter.transform.position = Items.transform.GetChild(index).position;
                shopinfo(index);
            }
            else
            {
                index--;
                shopselecter.transform.position = Items.transform.GetChild(index).position;
                shopinfo(index);
            }
        }
    }

    // 상점안에서 설명 표시 해주는것
    IEnumerator TextMoveEffect()
    {
        string name = "     상점안에서는 시간이 멈춥니다. 구매를 원하시면 F 를 누르거나 터치해주세요.                   ";
        char[] cs = name.ToCharArray();
        string test_string = "";
        int index = 0;
        while (true)
        {
            if (shop_open == true)
            {
                if (index % cs.Length == 0)
                {
                    index = 0;
                }
                test_string += cs[index].ToString();
                ShopInfoText.transform.GetComponent<Text>().text = test_string;

                index++;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    void shopinfo(int num)
    {
        if(num == 0)
        {
            Items.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = "스태미너 충전";
            Items.transform.GetChild(3).GetChild(1).GetComponent<Text>().text = "가격 : 20 Line";
        }
        if (num == 1)
        {
            Items.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = "시간 30초 증가";
            Items.transform.GetChild(3).GetChild(1).GetComponent<Text>().text = "가격 : 30 Line";
        }
    }
}
