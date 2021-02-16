using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person_move : MonoBehaviour
{
    //List<GameObject> person_init_list;
    //public GameObject persons_capsule;

    GameObject road;
    GameObject persons;
    public GameObject persons_prefab;
    Vector3 road_pos;
    int random_num;

    void Start()
    {


        persons_prefab = Resources.Load<GameObject>("person_prefabs");

        road = Find.Find_obj("Road");
        persons = Find.Find_obj("Persons");
        StartCoroutine("person_respown");
        

    }
    void Update()
    {
        
        person_init();
    }
    void person_init()
    {
        //Instantiate(persons_capsule, new Vector3(0, 0, 0),Quaternion.identity).transform.parent = persons.transform;

       
    }

    IEnumerator person_respown()
    {
        while(true)
        {

            random_num = Random.Range(0, road.transform.childCount);
            // 위치 포지션값 찾기

            road_pos = road.transform.GetChild(random_num).position;


            GameObject person_parent = Instantiate(persons_prefab);
            person_parent.transform.position = new Vector3(road_pos.x, road_pos.y, road_pos.z);
            person_parent.transform.parent = persons.transform;
            //persons.transform.parent = persons_parent.transform;
            yield return new WaitForSeconds(0.01f);
        }
    }


    //void road_pos()
    //{
    //    // 0번 부터 176번 까지 랜덤 소환
    //    random_num = Random.Range(0, 176);
    //    road.transform.GetChild(random_num).transform.position = 
    //}
    // 우선 포지션, 일정 갯수 소환, 

}