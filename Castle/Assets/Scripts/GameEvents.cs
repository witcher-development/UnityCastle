using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{

    public Hero h;
    public GridLayout gridLayout;
    public Generator map;
    private Vector2 targetPos;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnClickEvent()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2Int currPosition = map.gameMap.GetCoords();
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellIndex = gridLayout.WorldToCell(targetPos);
            currPosition.x += cellIndex.x;
            currPosition.y += cellIndex.y;
            if (currPosition != map.gameMap.GetCoords())
            {
                map.gameMap.DrawMapByCenter(currPosition);
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        OnClickEvent();
    }
}
