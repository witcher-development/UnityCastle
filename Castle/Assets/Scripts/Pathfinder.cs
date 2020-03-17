using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using UnityEngine;
using UnityEngine.Tilemaps;
using Debug = UnityEngine.Debug;

public class Pathfinder : MonoBehaviour
{
    public class Point
    {
        public Vector3Int position;
        public Point parent;

        public Point(Vector3Int pos, Point par)
        {
            position = pos;
            parent = par;
        }
    }

    public class Path
    {
        private List<Point> path;
        private int xStartOffset = 0;
        private int yStartOffset = 0;
        private Vector3Int endPoint;
        private List<Vector3Int> backtrackPath;
        private bool[,] map;
        private char[,] solidMap;


        private void Backtrack(int i)
        {
            Point p = path[i];
            do
            {
                backtrackPath.Insert(0, new Vector3Int(p.position.x + xStartOffset, p.position.y + yStartOffset, 0));
                //Debug.Log(p.position);
                p = p.parent;
            } while (p != null);
        }

        private bool CheckMap()
        {
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (!map[i, j]) return false;
                }
            }

            return true;
        }

        private void FindPath()
        {
            int i;
            for (i = 0; i < path.Count; i++)
            {
                if (!map[path[i].position.x + 6, path[i].position.y + 3])
                {
                    if (path[i].position == endPoint)
                    {
                        Backtrack(i);
                        return;
                    }

                    RepopulatePoints(path[i]);
                    if (!CheckMap()) FindPath();
                    return;
                }
            }
        }

        private void RepopulatePoints(Point p)
        {
            map[p.position.x + 6, p.position.y + 3] = true;
            Debug.Log(p.position);
            Debug.Log("Map position: " + (p.position.x + 6) + ";" + (p.position.y + 3));
            if (p.position.x + 6 > 0)
            {
                if (solidMap[p.position.x - 1 + xStartOffset, p.position.y + yStartOffset] == 'n')
                {
                    if (!map[p.position.x + 5, p.position.y + 3])
                        path.Add(new Point(new Vector3Int(p.position.x - 1, p.position.y, 0), p));
                }
                else map[p.position.x + 5, p.position.y + 3] = true;
            }

            if (p.position.x + 6 < 12)
            {
                if (solidMap[p.position.x + 1 + xStartOffset, p.position.y + yStartOffset] == 'n')
                {
                    if (!map[p.position.x + 7, p.position.y + 3])
                        path.Add(new Point(new Vector3Int(p.position.x + 1, p.position.y, 0), p));
                }
                else map[p.position.x + 7, p.position.y + 3] = true;
            }

            if (p.position.y + 3 > 0)
            {
                if (solidMap[p.position.x  + xStartOffset, p.position.y + yStartOffset - 1] == 'n')
                {
                    if (!map[p.position.x + 6, p.position.y + 2])
                        path.Add(new Point(new Vector3Int(p.position.x, p.position.y - 1, 0), p));
                }
                else map[p.position.x + 6, p.position.y + 2] = true;
            }

            if (p.position.y + 3 < 7)
            {
                if (solidMap[p.position.x  + xStartOffset, p.position.y + yStartOffset + 1] == 'n')
                {
                    if (!map[p.position.x + 6, p.position.y + 4])
                        path.Add(new Point(new Vector3Int(p.position.x, p.position.y + 1, 0), p));
                }
                else map[p.position.x + 6, p.position.y + 4] = true;
            }
        }

        public Path()
        {
        }

        public List<Vector3Int> GetPath(Vector3Int start, Vector3Int end)
        {
            path = new List<Point>();
            backtrackPath = new List<Vector3Int>();
            xStartOffset = start.x;
            yStartOffset = start.y;
            start = new Vector3Int(0, 0, 0);
            end = new Vector3Int(end.x - xStartOffset, end.y - yStartOffset, 0);
            path.Clear();
            backtrackPath.Clear();
            endPoint = end;
            path.Add(new Point(start, null));
            int s = GameObject.Find("mapGen").GetComponent<Generator>().gameMap.GetSize();
            solidMap = new char[s,s];
            solidMap = GameObject.Find("mapGen").GetComponent<Generator>().gameMap.GetSolidMap();
            map = new bool[13, 8];
            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    map[i, j] = false;
                }
            }

            FindPath();
            return backtrackPath;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}