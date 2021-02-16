//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayCon2 : MonoBehaviour
//{
//    Vector3 up_pos;
//    Vector3 move_pos;
//    Vector3 move;
//    void Start()
//    {
//        up_pos = new Vector3(transform.position.x, 0.2f, transform.position.z);
//        StartCoroutine("charamove");
//    }
//    void Update()
//    {
//        if (Input.GetKey(KeyCode.H))
//            move_pos = Vector3.forward;
//        if (Input.GetKey(KeyCode.N))
//            move_pos = Vector3.back;
//        if (Input.GetKey(KeyCode.B))
//            move_pos = Vector3.left;
//        if (Input.GetKey(KeyCode.M))
//            move_pos = Vector3.right;
//        if (Input.GetKey(KeyCode.Tab))
//        {
//            move_pos = Vector3.zero;
//            Debug.Log("출력");
//        }
//    }

//    private void FixedUpdate()
//    {
//        //charamove();
//    }
//    IEnumerator charamove()
//    {
//        while (true)
//        {
//            transform.position += move;
//            yield return new WaitForSeconds(0.5f);
//        }
//    }
//}
