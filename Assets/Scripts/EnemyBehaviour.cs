using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    public GameObject projectile;
    public float projectileSpeed = 10f;
    public float health = 150;
    public float shotsPerSEconds = 0.5f;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider);
        Projectile missile = collider.gameObject.GetComponent<Projectile>();
        if(missile)
        {
            health -= missile.GetDamage();
            missile.Hit();
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        float probability = Time.deltaTime * shotsPerSEconds;
        if (Random.value < probability)
        {
            Fire();
        }
    }

    void Fire()
    {
        Vector3 startPosition = transform.position + new Vector3(0, -1, 0);
        GameObject beam = Instantiate(projectile, startPosition, Quaternion.identity);
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -projectileSpeed, 0);
    }
}
