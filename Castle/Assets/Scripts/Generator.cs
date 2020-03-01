using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class Generator : MonoBehaviour
{
    public int size;
    public Tilemap back;
    public Tilemap solid;

    public class Map
    {
        private char[,] background;
        private char[,] solidTiles;
        private int size;
        private Vector2Int absoluteCoordinates;
        private Tilemap backgroundTileMap;
        private Tilemap solidTilemap;

        public Vector3Int GetNewTileCoordinates(int x, int y)
        {
            if (x > -1 && x < size && y > -1 && y < size)
            {
                switch (background[x, y])
                {
                    case 's':
                        return new Vector3Int(-9, 1, 0);
                        break;
                    case 'p':
                        return new Vector3Int(-8, 0, 0);
                        break;
                    case 'f':
                        return new Vector3Int(-9, 2, 0);
                        break;
                    case 'i':
                        return new Vector3Int(-8, -3, 0);
                        break;
                    default:
                        return new Vector3Int(-7, 2, 0);
                        break;
                }
            }
            else return new Vector3Int(-7, 2, 0);
        }

        public Vector3Int GetNewSolidTileCoordinates(int x, int y)
        {
            if (x > -1 && x < size && y > -1 && y < size)
            {
                switch (solidTiles[x, y])
                {
                    case 'r':
                        return new Vector3Int(-7, 1, 0);
                        break;
                    case 'p':
                        return new Vector3Int(-8, 0, 0);
                        break;
                    case 'f':
                        return new Vector3Int(-9, 2, 0);
                        break;
                    case 'i':
                        return new Vector3Int(-8, -3, 0);
                        break;
                    default:
                        return new Vector3Int(-7, 2, 0);
                        break;
                }
            }
            else return new Vector3Int(-7, 2, 0);
        }

        public void DrawMapByCenter(Vector2Int center)
        {
            absoluteCoordinates = center;
            for (int i = -5; i < 6; i++)
            {
                for (int j = -3; j < 3; j++)
                {
                    TileBase tile = backgroundTileMap.GetTile(GetNewTileCoordinates(absoluteCoordinates.x + i, absoluteCoordinates.y + j));
                    backgroundTileMap.SetTile(new Vector3Int(i, j, 0), tile);
                }
            }

            for (int i = -5; i < 6; i++)
            {
                for (int j = -3; j < 3; j++)
                {
                    TileBase tile = solidTilemap.GetTile(GetNewSolidTileCoordinates(absoluteCoordinates.x + i, absoluteCoordinates.y + j));
                    solidTilemap.SetTile(new Vector3Int(i, j, 1), tile);
                }
            }
        }

        public void Fill()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    double dist = Math.Sqrt((size / 2 - i) * (size / 2 - i) + (size / 2 - j) * (size / 2 - j));
                    dist /= (size / 10);
                    if (i > size - 10 || i < 10 || j > size - 10 || j < 10) dist = 10;
                    switch ((int) dist)
                    {
                        case 4:
                            background[i, j] = 's'; //sand -9 1 0
                            break;
                        case 3:
                            background[i, j] = 'p'; //plains -8 0 0
                            break;
                        case 2:
                            background[i, j] = 'f'; //forest -9 2 0
                            break;
                        case 1:
                            background[i, j] = 'p'; //
                            break;
                        case 0:
                            background[i, j] = 'i'; //ice -8 3 0
                            break;
                        default:
                            background[i, j] = 'o'; //ocean -7 2 0
                            break;
                    }
                }
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int x = Random.Range(0, 4);
                    if (x == 0)
                    {
                        solidTiles[i, j] = 'r';
                    }
                }
            }

            absoluteCoordinates = new Vector2Int(size / 2, size / 2);
        }

        public Vector2Int GetCoords()
        {
            return absoluteCoordinates;
        }

        public Map(int s, Tilemap b, Tilemap solid)
        {
            background = new char[s, s];
            solidTiles = new char[s, s];
            backgroundTileMap = b;
            solidTilemap = solid;
            size = s;
        }
    }

    public Map gameMap;

    // Start is called before the first frame update
    void Start()
    {
        gameMap = new Map(size, back, solid);
        gameMap.Fill();
        gameMap.DrawMapByCenter(gameMap.GetCoords());
    }

    // Update is called once per frame
    void Update()
    {
    }
}