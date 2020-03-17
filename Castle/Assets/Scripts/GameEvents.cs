using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Pathfinder.Path;

public class GameEvents : MonoBehaviour
{

    public hero h;
    public GridLayout gridLayout;
    public Generator map;
    private Vector2 targetPos;
    private List<Vector3Int> path;
    private Pathfinder.Path p;
    private float period = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        path = new List<Vector3Int>();
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
                //Debug.Log(1);
                p = new Pathfinder.Path();
                path = p.GetPath(new Vector3Int(map.gameMap.GetCoords().x, map.gameMap.GetCoords().y, 0),
                    new Vector3Int(currPosition.x, currPosition.y, 0));
                Debug.Log(path.Count);
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        OnClickEvent();
        if (period > 0.1f)
        {
            //Debug.Log(3);
            if (path.Count > 0)
            {
                //Debug.Log("Count: "+path.Count);
                map.gameMap.DrawMapByCenter(new Vector2Int(path[0].x, path[0].y));
                path.RemoveAt(0);
            }
            period = 0.0f;
        }
        period+=0.005f;
    }
}
