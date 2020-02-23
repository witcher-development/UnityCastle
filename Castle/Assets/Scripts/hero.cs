using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class hero : MonoBehaviour
{
    //public GameObject castle;

    public GameObject pref;
    
    public float speed;
    public Transform transform;
    private Vector2 targetPos;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            targetPos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //castle.GetComponent<Transform>().position = targetPos;
        }
        if((Vector2)transform.position != targetPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
    }
}
