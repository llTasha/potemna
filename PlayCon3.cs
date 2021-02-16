using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCon3 : MonoBehaviour
{
    float up, down, left, right;
    GameObject player_body;

    void Start()
    {
        // a 와 b 를 값을 넣어주고 계산은 밑에서 해준다.
        // a + b 한거 해주는거다.
        Debug.Log(test_int(1, 10));
        player_body = Find.Find_obj("Player");

        StartCoroutine("chara_pos");
    }
    void Update()
    {
        Debug.Log("Up :    " + up + "______" +
                  "Down :  " + down + "\n                    " +
                  "Left : " + left + "______" +
                  "Right :   " + right);
    }
    private void FixedUpdate()
    {
        player_body.transform.position += new Vector3(up, 0, right);
        player_body.transform.position -= new Vector3(down, 0, left);
    }

    IEnumerator chara_pos()
    {
        while (true)
        {
            float count = 0.01f;
            if (Input.GetKey(KeyCode.W) ||
                Input.GetKey(KeyCode.S) ||
                Input.GetKey(KeyCode.A) ||
                Input.GetKey(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.W))
                    up += count;
                if (Input.GetKey(KeyCode.S))
                    down += count;
                if (Input.GetKey(KeyCode.A))
                    left += count;
                if (Input.GetKey(KeyCode.D))
                    right += count;
            }
            yield return null;  
        }
    }


    int test_int(int a, int b)
    {
        int c = a + b;
        return c;
    }

}