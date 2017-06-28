//using UnityEngine;
//using System.Collections;

//public class FormationController : MonoBehaviour {
//	public GameObject enemyPrefab;
//	public float width = 10;
//	public float height = 5;
//	public float speed = 5.0f;
//	public float padding = 1;
//	public float spawnDelaySeconds = 1f;

//	private int direction = 1;
//	private float boundaryRightEdge, boundaryLeftEdge;

//	// Use this for initialization
//	void Start () {
//		Camera camera = Camera.main;
//		float distance = transform.position.z - camera.transform.position.z;
//		boundaryLeftEdge = camera.ViewportToWorldPoint(new Vector3(0,0,distance)).x + padding;
//		boundaryRightEdge = camera.ViewportToWorldPoint(new Vector3(1,1,distance)).x - padding;
//		SpawnUntilFull();
//	}

//	void OnDrawGizmos(){
//		float xmin,xmax,ymin,ymax;
//		xmin = transform.position.x - 0.5f * width;
//		xmax = transform.position.x + 0.5f * width;
//		ymin = transform.position.y - 0.5f * height;
//		ymax = transform.position.y + 0.5f * height;
//		Gizmos.DrawLine(new Vector3(xmin,ymin,0), new Vector3(xmin,ymax));
//		Gizmos.DrawLine(new Vector3(xmin,ymax,0), new Vector3(xmax,ymax));
//		Gizmos.DrawLine(new Vector3(xmax,ymax,0), new Vector3(xmax,ymin));
//		Gizmos.DrawLine(new Vector3(xmax,ymin,0), new Vector3(xmin,ymin));
//	}

//	// Update is called once per frame
//	void Update () {
//		float formationRightEdge = transform.position.x + 0.5f * width;
//		float formationLeftEdge = transform.position.x - 0.5f * width;
//		if (formationRightEdge > boundaryRightEdge){
//			direction = -1;
//		}
//		if (formationLeftEdge < boundaryLeftEdge){
//			direction = 1;
//		}
//		transform.position += new Vector3(direction * speed * Time.deltaTime,0,0);

//		if(AllMembersAreDead()){
//			Debug.Log("My formation is empty :(");
//			SpawnUntilFull();
//		}
//	}

//	void SpawnUntilFull(){
//		Transform freePos = NextFreePosition();
//		GameObject enemy = Instantiate(enemyPrefab, freePos.position, Quaternion.identity) as GameObject;
//		enemy.transform.parent = freePos;
//		if(FreePositionExists()){
//			Invoke("SpawnUntilFull", spawnDelaySeconds);
//		}
//	}

//	bool FreePositionExists(){
//		foreach(Transform position in transform){
//			if(position.childCount <= 0){
//				return true;
//			}
//		}
//		return false;
//	}

//	Transform NextFreePosition(){
//		foreach(Transform position in transform){
//			if(position.childCount <= 0){
//				return position;
//			}
//		}
//		return null;
//	}

//	bool AllMembersAreDead(){
//		foreach(Transform position in transform){
//			if(position.childCount > 0){
//				return false;
//			}
//		}
//		return true;
//	}
//}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationController : MonoBehaviour
{

    public GameObject enemyPrefab;
    public float width = 10f;
    public float height = 5f;
    public float speed = 5.0f;
    public float padding = 1;
    private bool movingRight;
    private float xmin, xmax;
    private float boundaryRightEdge, boundaryLeftEdge;
    public float spawnDelay = 0.5f;

    // Use this for initialization
    void Start()
    {
        Camera camera = Camera.main;
        float distance = this.transform.position.z - camera.transform.position.z;
        boundaryLeftEdge = camera.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + padding;
        boundaryRightEdge = camera.ViewportToWorldPoint(new Vector3(1, 0, distance)).x - padding;
        xmin = boundaryLeftEdge;
        xmax = boundaryRightEdge;
        SpawnEnemies();
    }


    void SpawnEnemies()
    {
        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
    }

    void SpawnUntilFull()
    {
        Transform freePosition = NextFreePosition();
        if (freePosition)
        {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition;
        }
        if (NextFreePosition())
        {
            Invoke("SpawnUntilFull", spawnDelay);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }
    // Update is called once per frame
    void Update()
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

        if (AllMembersDead())
        {
            Debug.Log("Empty Formation");
            SpawnUntilFull();
        }
    }

    private Transform NextFreePosition()
    {
        foreach (Transform childPositionGameObject in this.transform)
        {
            if (childPositionGameObject.childCount == 0)
            {
                return childPositionGameObject;
            }
        }
        return null;
    }

    private bool AllMembersDead()
    {
        foreach (Transform childPositionGameObjection in this.transform)
        {
            if (childPositionGameObjection.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }
}

