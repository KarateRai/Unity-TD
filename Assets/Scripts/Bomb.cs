using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb: MonoBehaviour
{
    [Header("Unity Stuff")]
    public GameObject impactEffect;
    [Header("Unit Stats")]
    public float explosionRadius;
    public float speed = 10;
    public float damage;
    

    //Movement stuff
    private int randomInt;
    private Transform target;
    private Transform[] path;
    private int wavepointIndex;

    private void Start()
    {
        randomInt = Random.Range(1, WayPoints.pathsOnMap+1);
        UsePath(this.randomInt);
        target = path[path.Length -1];
        wavepointIndex = path.Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Explode();
        }
    }

    private void UsePath(int pathNR)
    {
        switch (pathNR)
        {
            case 1:
                path = WayPoints.path1;
                break;
            case 2:
                path = WayPoints.path2;
                break;
            case 3:
                path = WayPoints.path3;
                break;
            case 4:
                path = WayPoints.path4;
                break;
            case 5:
                path = WayPoints.path5;
                break;
            case 6:
                path = WayPoints.path6;
                break;
        }
        target = path[0];
    }

    public void Update()
    {
        if (this != null)
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);


            if (Vector3.Distance(transform.position, target.position) <= 0.4f)
            {
                GetNextWaypoint();
            }
        }
    }
    void Explode()
    {
        Collider[] hitObject = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in hitObject)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
            else if (collider.tag == "Bomb")
            {
                Die(collider.transform);
            }
        }
        Die();
    }

    public void Die()
    {
        GameObject effect = (GameObject)Instantiate(impactEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);

        Destroy(gameObject);
    }

    public void Die(Transform bomb)
    {
        Bomb b = bomb.GetComponent<Bomb>();

        if (bomb != null)
        {
            b.Die();
        }
    }

    private void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();
        if (e != null)
        {
            e.TakeDamage(damage);
        }
    }

    void GetNextWaypoint()
    {

        if (wavepointIndex <= 0)
        {
            PathEnded();
            return;
        }
        else
        {
            wavepointIndex--;
            target = path[wavepointIndex];
        }
    }

    void PathEnded()
    {
        GameObject effect = (GameObject)Instantiate(impactEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
        Destroy(gameObject);
    }


}
