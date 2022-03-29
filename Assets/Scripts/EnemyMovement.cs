using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private Transform[] path;
    private int wavepointIndex = 0;

    private Enemy enemy;

    public Transform body;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        UsePath(enemy.randomInt);

    }

    private void Update()
    {
        if (this != null)
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);


            if (Vector3.Distance(transform.position, target.position) <= 0.4f)
            {
                GetNextWaypoint();
            }

            enemy.speed = enemy.startSpeed;
            LookRotation(dir);
        }
       
    }

    private void LookRotation(Vector3 dir)
    {
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(body.rotation, lookRotation, 1).eulerAngles;
        body.rotation = Quaternion.Euler(0f, rotation.y, 0f);
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

    void GetNextWaypoint()
    {

        if (wavepointIndex >= path.Length - 1)
        {
            PathEnded();
            return;
        }
        else
        {
            wavepointIndex++;
            target = path[wavepointIndex];
        }
    }

    void PathEnded()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
