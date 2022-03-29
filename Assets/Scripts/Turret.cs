using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    private Enemy targetEnemy = null;

    [Header("General")]
    public float range = 10f;
    

    [Header("Use Bullets (default)")]
    public float fireRate = 1.5f;
    private float fireCountdown = 0;
    public GameObject bulletPrefab;
    private Animator anim;

    [Header("Use Lazer")]
    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public ParticleSystem hurr;
    public Light impactLight;
    public int startDamage = 35;
    public float multiplier;
    public float multipliedbytime;
    public float damageOverTime;
    public float slowPercent = 20f;

    [Header("Unity Setup Fields")]
    
    public string enemyTag = "Enemy";
    public float turnSpeed = 10f;
    public Transform head;

    public Transform firePoint;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        damageOverTime = startDamage;
        
    }
    
    private void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();

        }
        else
        {
            target = null;
            if (anim != null)
            {
                anim.Play("Idle");
            }
        }
    }

    private void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                damageOverTime = startDamage;
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }
            return;
        }

        LockOnTarget();

        if (useLaser)
        {
            Laser();
        }
        else
        {
            Gun();
        }



    }

    private void Gun()
    {
        
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;

            if (anim != null)
            {
                anim.Play("Fire");
            }

        }
        fireCountdown -= Time.deltaTime;
    }

    void Laser()
    {
        LaserGraphics();
        damageOverTime += (damageOverTime * multiplier * Time.deltaTime);
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowPercent/100);
        
        
    }

    private void LaserGraphics()
    {
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;

        impactEffect.transform.position = target.position + dir.normalized;

        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    private void LockOnTarget()
    {
        //Target Lock-On
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(head.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        head.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
