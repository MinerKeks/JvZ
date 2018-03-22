using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    private static Transform _player;

    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public static Transform player
    {
        get { return _player; }
    }
}