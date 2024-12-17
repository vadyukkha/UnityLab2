using UnityEngine;

public class RunningEnemy : MonoBehaviour
{
    Rigidbody rb;

    Transform player;

    float health = 100f;
    float movementSpeed = 3f;
    float attackDamage = 20f;
    float attackRange = 15f;
    float detectionRange = 15f;

    float stopRange = 4f;

    float attackRate = 2f;
    float lastAttackTime;

    float recoilAmount = 0.1f;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player =  GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        
        Vector3 direction = (player.position - transform.position).normalized;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange) 
        {   
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);


            if (distanceToPlayer > stopRange) 
            {
                rb.velocity = new Vector3(direction.x * movementSpeed, rb.velocity.y, direction.z * movementSpeed);
            }
            else
            {
                rb.velocity = new Vector3(0, 0, 0);
            }
        }
        else
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
            
        if (Time.time - lastAttackTime >= attackRate)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void Attack()
    {
        Vector3 recoil = new Vector3(Random.Range(-recoilAmount, +recoilAmount), Random.Range(-recoilAmount, +recoilAmount), Random.Range(-recoilAmount, +recoilAmount));
        Vector3 direction = (player.position - transform.position).normalized + recoil;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, direction, out hit, attackRange))
            {
                if (hit.collider.CompareTag("Player"))
                {   
                    
                    hit.collider.GetComponent<PlayerHealth>().takeDamage(attackDamage);
                }
            }

            LineRenderer lr = GetComponent<LineRenderer>();
            lr.positionCount = 2;
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, hit.point);
            lr.startWidth = 0.01f;
            lr.endWidth = 0.01f;
            Invoke("ClearLineRenderer",0.1f);   
        }
        
    }

    void ClearLineRenderer()
    {
        LineRenderer lr = GetComponent<LineRenderer>();
        lr.positionCount = 0;
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

