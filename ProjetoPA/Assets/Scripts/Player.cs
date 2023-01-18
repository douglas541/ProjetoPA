using UnityEngine;

public class Player : MonoBehaviour
{
    public Enemy enemy;
    public PlayerMovement playerMovement;
    public int health = 10;
    private int _playerAttackDamage = 5;
    private float _gameTimeForNextAttack;
    private float _attackDelayInSeconds = 0.4f;
    private Animator _animator;
    [SerializeField] Collider2D attackHitbox;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        attackHitbox = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.H))
        {
            Attack();
        }
        if (AttackHasEnded())
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
        if (AttackHasEnded())
        {
            HandleAttack();  
        }
    }

    bool AttackHasEnded()
    {
        if(Time.time > _gameTimeForNextAttack)
        {
            return true;
        }

        return false;
    }

    void HandleAttack()
    {
        playerMovement.SetPlayerCanMove(false);
        _gameTimeForNextAttack = _attackDelayInSeconds + Time.time;
        _animator.SetTrigger("Attack");
        OnTriggerEnter2D(attackHitbox);
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
            enemy.SetEnemyHealth(_playerAttackDamage);
            Debug.Log(enemy.GetEnemyHealth());
        }
    }
}