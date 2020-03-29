using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DiController
{
    private Tilemap _solid;
    private Hero _heroScript;
    private Generator _generator;

    public DiController()
    {
        _solid = GameObject.Find("UpperLayer").GetComponent<Tilemap>();
        _heroScript = GameObject.Find("Hero").GetComponent<Hero>();
        _generator = GameObject.Find("MapGen").GetComponent<Generator>();
    }

    public T Get<T>()
    {
        if (typeof(T) == typeof(Hero))
        {
            return (T) Convert.ChangeType(_heroScript, typeof(T));
        }

        if (typeof(T) == typeof(Tilemap))
        {
            return (T) Convert.ChangeType(_solid, typeof(T));
        }
        
        if (typeof(T) == typeof(Generator))
        {
            return (T) Convert.ChangeType(_generator, typeof(T));
        }

        return default(T);
    }
}