using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyCam : MonoBehaviour {
    public GameObject player;
    Camera myCam;
    Vector3 velocity;
    private bool bounds;
    [SerializeField]
    private float mapX, mapY;
    private float smoothTimeY = .2f;
    private float smoothTimeX = .2f;
    private float minX, maxX, minY, maxY;

    // Use this for initialization
    void Start () {
        Collider2D collider = GameObject.Find("Bounds").GetComponent<BoxCollider2D>();
        myCam = GetComponent<Camera>();
        mapX = collider.bounds.size.x;
        mapY = collider.bounds.size.y;
        float vertExtent = myCam.orthographicSize;
        float horzExtent = vertExtent * Screen.width / Screen.height;
        minX = horzExtent;
        maxX = mapX - horzExtent;
        minY = vertExtent ;
        maxY = mapY - vertExtent;
        player = GameObject.Find("Jeff");

    }
	
	// Update is called once per frame
	void Update () {
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);
        transform.position = new Vector3(posX, posY, transform.position.z);
        var v3 = transform.position;
        v3.x = Mathf.Clamp(v3.x, minX, maxX);
        v3.y = Mathf.Clamp(v3.y, minY, maxY);
        transform.position = v3;
    }
}
