using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class grave_Spawner : MonoBehaviour {
    public GameObject Grave_Prefab;
    [SerializeField]
    private float cooldownLeft = 0;
    [SerializeField]
    private int maxGraves;
    private float spawn_Height = 0.79f;
    GameObject[] graves;
    GameObject[] grave_Spawnpoints;
    // Use this for initialization
    void Start () {
        grave_Spawnpoints = GameObject.FindGameObjectsWithTag("Waypoint");
        maxGraves = (grave_Spawnpoints.Length / 2);
    }
	
	// Update is called once per frame
	void Update () {
        cooldownLeft -= Time.deltaTime;


     if (cooldownLeft <= 0)
        {
            graves = GameObject.FindGameObjectsWithTag("Grave");
            if (graves.Length < maxGraves )
            {
                cooldownLeft = 5f;
                GameObject randomPoint = grave_Spawnpoints[Random.Range(0, grave_Spawnpoints.Length)];
                Vector3 grave_Position = randomPoint.transform.position;
                grave_Position.Set(grave_Position.x, grave_Position.y + spawn_Height, grave_Position.z);
                Instantiate(Grave_Prefab, grave_Position, randomPoint.transform.rotation);
            }
        }
    }
}
