using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Hero : MonoBehaviour
{
    //public GameObject castle;

    public string Log()
    {
        return "it is a test string1";
    }



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            //castle.GetComponent<Transform>().position = targetPos;
        }
    }
}