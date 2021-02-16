using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCon : MonoBehaviour
{
    //public ShopManager shopmanager = new ShopManager();
    SoundManager sound;
    public List<Transform> person_tag;
    public Collider[] person_col;
    public RaycastHit hit;
    public ShopManager shop;
    Camera cam;
    Vector3 a;
    Vector3 b;

    float h;
    float v;

    float follow_spd = 40f;
    float chara_spd  = 3f;
    float stamina_data;

    public Vector3 chara_pos;



    void Start()
    {
        a = Vector3.zero;
        b = Vector3.zero;
        cam = Camera.main;
        person_tag.Add(transform);  // 시작하면 리스트에 플레이어 넣음

        StartCoroutine("Follow_Player");
        StartCoroutine("Find_anythink");
        transform.position = new Vector3(0, 0.2f, 0);

        sound = GameObject.FindObjectOfType<SoundManager>();
        shop = GameObject.Find("Shop_pre").GetComponent<ShopManager>();
        //shop = FindObjectOfType<ShopManager>();
        test = true;
    }

    void Update()
    {
        //Debug.Log(h);
        //Debug.Log(v);
        cam.transform.position = new Vector3(transform.position.x, cam.transform.position.y, transform.position.z);
    }



    void FixedUpdate()
    {
        if (shop.shop_open == false)
        {
            charamove();           // 캐릭터 뱡향키로 이동
        }
    }

    bool test;

    Vector3 movePos;
    void charamove()        // 플레이어가 움직임
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");


        if (test)
        {
            //if (shopmanager.shop_open == false)
            movePos = new Vector3(h, 0, v);
            //else
            //    movePos = Vector3.zero;
        }
        //Debug.Log(h);
        //Debug.Log(test);
        //Debug.Log(movePos);

        transform.position = Vector3.MoveTowards(transform.position, transform.position + movePos, Time.deltaTime * chara_spd);
        //transform.rotation = Quaternion.LookRotation(movePos);


        if (Input.GetKey(KeyCode.Space) && stamina_data >= 0.01f)
            chara_spd = 7f;
        else
            chara_spd = 4f;// 2.5f;
    
        //testvoid22();


        if (h != 0 || v != 0)
        {
            follow_spd = 1000f;
            transform.rotation = Quaternion.LookRotation(movePos);
            //transform.LookAt(new Vector3(h, 0, v));           
            //transform.Translate(Vector3.forward * Time.deltaTime * chara_spd); 
        }
        else
            follow_spd = 1f;
    

        //Debug.Log(movePos.normalized);



        // 스크린 끄면 오류뜸 불러올게 없어서
        stamina_data = GameObject.Find(Global.stamina_count).GetComponent<Image>().fillAmount;
        // 스테미너 데이터 불러옴

    }

    void testvoid22()
    {
        Vector3 left = Engles(-90f);
        Vector3 forward = Engles(0f);
        Vector3 right = Engles(90f);
        Vector3 back = Engles(180f);

        Debug.DrawRay(transform.position, left, Color.red);
        Debug.DrawRay(transform.position, forward, Color.red);
        Debug.DrawRay(transform.position, right, Color.red);
        Debug.DrawRay(transform.position, back, Color.red);

        if (Physics.Raycast(transform.position, forward, out hit, 0.3f) &&
            Physics.Raycast(transform.position, left, out hit, 0.3f) ||
            Physics.Raycast(transform.position, forward, out hit, 0.3f) &&
            Physics.Raycast(transform.position, right, out hit, 0.3f) ||
            Physics.Raycast(transform.position, forward, out hit, 0.2f))
        {
            if (Physics.Raycast(transform.position, forward, out hit, 0.3f) &&
            Physics.Raycast(transform.position, left, out hit, 0.3f))
            {
                movePos = new Vector3(h, 0, 0);
            }
            if (Physics.Raycast(transform.position, forward, out hit, 0.3f) &&
            Physics.Raycast(transform.position, right, out hit, 0.3f))
            {
                movePos = new Vector3(0, 0, v);
            }
            else
            {
                Debug.Log("야 됫다 ");
                chara_spd = 0f;
            }
        }
        else
            Debug.Log("");



        //if(Physics.Raycast(transform.position, forward, out hit, 1.3f))
        //{
        //    if(hit.transform.gameObject.layer == 10)
        //    {
        //        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        //        {
        //            if (!Physics.Raycast(transform.position, left, out hit, 1.3f))
        //            {
        //                return;
        //            }
        //            else
        //                movePos = new Vector3(h, 0, 0);
        //        }
        //        else
        //            movePos = new Vector3(h, 0, v);
        //        Debug.Log("아 못하겟다!" + hit.transform.gameObject.layer);
        //    }
        //    Debug.DrawRay(hit.point, hit.normal, Color.red, 5f);
        //}


            //if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
            //{
            //    if (Physics.Raycast(transform.position, forward, out hit, 1.3f) &&
            //        Physics.Raycast(transform.position, left, out hit, 1.3f) &&
            //      !(Physics.Raycast(transform.position, right, out hit, 1.3f)))
            //    {
            //        test = false;
            //        //Debug.Log("회전 회오리 감자");
            //        movePos = new Vector3(h, 0, 0);
            //        //transform.rotation = Quaternion.LookRotation(true_right);
            //    }
            //    else
            //    {
            //        //Debug.Log("111111");
            //    }
            //}
            //else
            //    test = true;
    }
    Vector3 Engles(float engle)
    {
        engle += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(engle*Mathf.Deg2Rad),0,Mathf.Cos(engle * Mathf.Deg2Rad));
    }



    IEnumerator Follow_Player() // 사람들이 플레이어를 따라옴
    {
       // yield return new WaitForSeconds(0.5f);
        while (true)
        {
            for (int i = 1; i < person_tag.Count; i++)
            {
                Vector3 dir = person_tag[i].transform.position - person_tag[i - 1].transform.position;
                person_tag[i].transform.position = Vector3.Lerp(person_tag[i].transform.position,
                                                                person_tag[i - 1].transform.position + dir * 0.3f,
                                                                Time.deltaTime * follow_spd);

            }
            //Physics.IgnoreLayerCollision(LayerMask.NameToLayer("9"), LayerMask.NameToLayer("10"), true);
            // 실행 이 찾는거보다 빨라서 러프가 바로 이동함 수정해야함!!!
            yield return null;
        }
    }

    IEnumerator Find_anythink()
    {
        while (true)
        {
            person_col = Physics.OverlapSphere(transform.position, 1f, 1 << 9);
            for (int i = 0; i < person_col.Length; i++)
            {

                //Debug.Log(person_col[i].name);
                if (person_tag[i].name != person_col[i].name)
                {
                    person_tag.Add(person_col[i].transform);
                    sound.sound_effect(2);
                    person_col[i].gameObject.layer = 0;
                }
                //Debug.Log(person_col[i] + "펄슨콜라이더");
            }
            // 예시 .. 이걸로 하면될거같아 
            // Collider [] 주변오브젝트  = Physics.OverlapSphere(A.transform.position, 원하는 거리);
            yield return null;
        }
    }

    void jisun_jeonghoon()
    {
        Vector3 movePos = transform.position + (a + b).normalized;
        transform.position = Vector3.MoveTowards(transform.position, movePos, Time.deltaTime* 5f);
        transform.LookAt(movePos);

        if (Input.GetKey(KeyCode.W))
        {
            if(Physics.Raycast(transform.position, transform.forward, out hit, 0.5f))
            {
                b = Vector3.zero;
            }
            else
            {
                b = Vector3.forward;
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            b = Vector3.back;
        }
        else
        {
            b = Vector3.zero;
        }


        if (Input.GetKey(KeyCode.D))
        {
            a = Vector3.right;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            a = Vector3.left;
        }
        else
        {
            a = Vector3.zero;
        }
    }
}