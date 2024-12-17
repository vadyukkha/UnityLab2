using UnityEngine;
using UnityEngine.AI;

public class HidingEnemy : MonoBehaviour
{
    Transform player; 
    float health = 100f;
    float changeCoverRange = 5f;
    float attackDamage = 10f;
    float attackRange = 40f; 
    float shootingRange = 40f;  
    float attackRate = 2f;  
    [SerializeField] LayerMask coverMask;  


    private NavMeshAgent agent;
    private Transform currentCover;
    private float shootingCooldown;

    float recoilAmount = 0.1f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player =  GameObject.FindGameObjectWithTag("Player").transform;
        ChangeCover();
    }

    void Update()
    {

        Vector3 direction = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer < changeCoverRange)
        {
            ChangeCover();
        }

        if (distanceToPlayer <= attackRange)
        {
            shootingCooldown -= Time.deltaTime;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);

            if (distanceToPlayer <= shootingRange && shootingCooldown <= 0f)
            {
                Attack();
                shootingCooldown = attackRate;
            }
        }
    }

    void ChangeCover()
    {
        Transform closestCover = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject cover in GameObject.FindGameObjectsWithTag("Cover"))
        {
            float distance = Vector3.Distance(transform.position, cover.transform.position);
            if (distance < closestDistance && cover.transform != currentCover)
            {
                closestDistance = distance;
                closestCover = cover.transform;
            }
        }

        if (closestCover != null)
        {
            currentCover = closestCover;
            agent.SetDestination(currentCover.position);
        }
    }

    void Attack()
    {
        Vector3 recoil = new Vector3(Random.Range(-recoilAmount, +recoilAmount), Random.Range(-recoilAmount, +recoilAmount), Random.Range(-recoilAmount, +recoilAmount));
        Vector3 direction = (player.position - transform.position).normalized + recoil;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= shootingRange)
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
            Invoke("ClearLineRenderer", 0.1f); 
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

