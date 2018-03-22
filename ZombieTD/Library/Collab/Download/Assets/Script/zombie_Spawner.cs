using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie_Spawner : MonoBehaviour {
    private GameObject Zombie_Prefab;
    public GameObject[] Zombie_Spawnpoints;
    protected Vector3 currentPosition;
    public float spawn_Height = 0.01f;
    // Use this for initialization
    void Start()
    {
        currentPosition = transform.position;
        currentPosition.y = currentPosition.y + spawn_Height;
        Instantiate(Zombie_Prefab, currentPosition, transform.rotation);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
