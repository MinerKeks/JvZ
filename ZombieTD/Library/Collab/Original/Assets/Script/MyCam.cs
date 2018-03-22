using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyCam : MonoBehaviour {
    public Transform Target;
    public GameObject player;
    Camera myCam;
    private Vector2 velocity;
    private float rightBound, leftBound, topBound, bottomBound;
    Collider2D collider;
    public bool bounds;
    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;
    public float smoothTimeY, smoothTimeX;

    // Use this for initialization
    void Start () {
        myCam = GetComponent<Camera>();
        player = GameObject.Find("Jeff");
        float height = 2f * myCam.orthographicSize;
        float width = height * myCam.aspect;
        collider = GameObject.Find("LevelBounce").GetComponent<BoxCollider2D>();
        Vector3 PosZ = new Vector3(width, height/2, 0);

        
        
        

    }
	
	// Update is called once per frame
	void Update () {
        myCam.orthographicSize = (Screen.height / 100f) / 1f;
        float height = 2f * myCam.orthographicSize;
        float width = height * myCam.aspect;
        minCameraPos = new Vector3(collider.bounds.min.x + (width/1.1f), height/2, -70);
        maxCameraPos = new Vector3(collider.bounds.max.x - (width/1.1f), collider.bounds.max.y, -70);
        
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);
        transform.position = new Vector3(posX, posY, transform.position.z);
        //transform.position = Vector3.Lerp(transform.position, Target.position, 0.1f) + new Vector3(0, 0.20f, -10);
        if (bounds)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
                Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
                Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z)) + new Vector3(0, 0.20f, -10);

            
        }
    }
}
