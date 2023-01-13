using UnityEngine;

public class Player : MonoBehaviour
{
    public Enemy enemy;
    public PlayerMovement playerMovement;
    public int health = 10;
    private int playerAttackDamage = 5;
    private float attackCooldown;
    private float timerOfNextAttack = 0.4f;
    private Animator animator;
    [SerializeField] Collider2D attackHitbox;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        attackHitbox = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.H))
        {
            Attack();
        }
        else if(Time.time > attackCooldown)
        {
            playerMovement.SetPlayerCanMove(true);
        }
        if (enemy.GetEnemyHealth() == 0)
        {
            enemy.gameObject.SetActive(false);
        }

    }

    void Attack()
    {
        if (Time.time > attackCooldown)
        {
            playerMovement.SetPlayerCanMove(false);
            attackCooldown = timerOfNextAttack + Time.time;
            animator.SetTrigger("Attack");
            OnTriggerEnter2D(attackHitbox);
        }
    }

    public void EnemyHit(int enemyDamage)
    {
        health = health + enemyDamage;
        var debugMessage = health <= 0 ? "You lost" : $"{health}";
        Debug.Log(debugMessage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var layerMask = collision.gameObject.layer;
        if (layerMask == 10)
        {
            enemy.SetEnemyHealth(playerAttackDamage);
            Debug.Log(enemy.GetEnemyHealth());
        }
    }
}
