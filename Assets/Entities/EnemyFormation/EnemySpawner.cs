using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public float width = 10f;
    public float height = 5f;
    public float speed = 5.0f;
    public float padding = 1;
    private bool movingRight;
    private float xmin, xmax;
    private float boundaryRightEdge, boundaryLeftEdge;
    // Use this for initialization
    void Start ()
    {
        Camera camera = Camera.main;
        float distance = this.transform.position.z - camera.transform.position.z;
        boundaryLeftEdge = camera.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + padding;
        boundaryRightEdge = camera.ViewportToWorldPoint(new Vector3(1, 0, distance)).x - padding;
        xmin = boundaryLeftEdge;
        xmax = boundaryRightEdge;
        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab,child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
        
	}

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }
    // Update is called once per frame
    void Update ()
    {
		if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        float rightEdgeOfFormation = transform.position.x + (0.5f * width);
        float leftEdgeOfFormation = transform.position.x - (0.5f * width);
        if (leftEdgeOfFormation < xmin)
        {
            movingRight = true;
        }
        else if (rightEdgeOfFormation > xmax)
        {
            movingRight = false;
        }
    }
}
