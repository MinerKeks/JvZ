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
        collider = GameObject.Find("LevelBounce").GetComponent<Collider2D>();
        Vector3 PosZ = new Vector3(width, height/2, 0);

        minCameraPos = new Vector3(collider.bounds.min.x + width, ((height + 0.8f) * 2f), -70);
        maxCameraPos = new Vector3(collider.bounds.max.x - width, 10000, -70);
        Debug.Log(width + ":" + height); 
        Debug.Log(maxCameraPos + ":"+ minCameraPos  ); 
        

    }
	
	// Update is called once per frame
	void Update () {
        float height = 2f * myCam.orthographicSize;
        float width = height * myCam.aspect;
        myCam.orthographicSize = (Screen.height / 100f) / 0.8f;
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);
        transform.position = new Vector3(posX, posY, transform.position.z);
        //transform.position = Vector3.Lerp(transform.position, Target.position, 0.1f) + new Vector3(0, 0.20f, -10);
        if (bounds)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
                Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
                Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));

            
        }
    }
}
