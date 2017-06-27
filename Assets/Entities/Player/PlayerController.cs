using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject Projectile;
    private float speed = 15f;
    public float padding = 1f;
    float xmin;
    float xmax;
    public float projectileSpeed;
    public float firingRate = 0.2f;

    // Use this for initialization
    void Start ()
    {
        // distance between camera and object (player), which is we want to clamp
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0, distance)); //Viewport to world point (take worldpoint and return worldpoint)
        // continue: 0 and 1 is x and y coor relative to the size of the screen --> 00: left bottom, 0.5 middle, 11: right top
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1,0, distance));
        xmin = leftmost.x + padding;
        xmax = rightmost.x - padding;
    }

    void Fire()
    {
        GameObject beam = (GameObject)Instantiate(Projectile, this.transform.position, Quaternion.identity);
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.000001f, firingRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position += new Vector3(-speed * Time.deltaTime, 0, 0); //independent of frame rate
            
            /*----OR----------- Alternatively, you can also do */
            //this.transform.position += Vector3.left * speed * Time.deltaTime;

            
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);

            /*----OR----------- Alternatively, you can also do */
            //this.transform.position += Vector3.right * speed * Time.deltaTime;
        }

        // restrict the player to the gamespace
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX, this.transform.position.y, this.transform.position.z);
    }
}
