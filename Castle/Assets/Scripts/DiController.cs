using UnityEngine;
using UnityEngine.Tilemaps;

public class DiController : MonoBehaviour
{
    private Tilemap _solid;
    private Hero _heroScript;

    private void Start()
    {
        _solid = GameObject.Find("UpperLayer").GetComponent<Tilemap>();
        _heroScript = GameObject.Find("Hero").GetComponent<Hero>();
        Debug.Log(_heroScript.Log());
    }
}