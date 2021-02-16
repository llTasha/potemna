using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickManager : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform outLine;
    public RectTransform inLine;
    public GameObject player;
    public Vector2 pos;
    float size;
    void Start()
    {
        outLine = GetComponent<RectTransform>();
        size = outLine.sizeDelta.x;
        inLine = transform.GetChild(0).GetComponent<RectTransform>();
        //inLine.gameObject.SetActive(false);
        player = Find.Find_obj("Player");
    }
    void Update()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    player.transform.position += new Vector3(pos.x, 0, pos.y).normalized * 0.1f;
        //}
        player.transform.position += new Vector3(pos.x, 0 , pos.y).normalized * Time.deltaTime * 12f;
    }
    public void OnDrag(PointerEventData eventData)
    {
        bool is_bool = RectTransformUtility.ScreenPointToLocalPointInRectangle(outLine,
                                                                               eventData.position,
                                                                               eventData.pressEventCamera,
                                                                               out pos);
        Debug.Log("OnDrag" + eventData.position);
        pos = Vector2.ClampMagnitude(pos, size / 2.8f);
        inLine.transform.localPosition = pos;
        SetMove = move.normalized;
       // Debug.Log(pos.normalized);
        //outLine.transform.position = new Vector2(outLine.position.x, outLine.position.y) + pos;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //inLine.gameObject.SetActive(true);
        Debug.Log("OnPointerDown" + eventData.position);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        inLine.position = outLine.position;
        //inLine.gameObject.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pos = Vector3.zero;
        //Debug.Log("OnPointerEnter" + eventData.position);
        //inLine.transform.localScale = Vector3.zero;
        inLine.transform.position = outLine.transform.position;
        //SetMove = Vector3.zero;
    }
    Vector3 move;
    public Vector3 GetMove
    {
        get { return move; }
    }
    Vector3 SetMove
    {
        set { move = value ; }
    }
}
