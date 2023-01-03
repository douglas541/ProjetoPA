using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 10;
    private Animator animator;
    private PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.H))
        {
            Attack();
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");

    }

    public void EnemyHit(int enemyDamage)
    {
        health = health + enemyDamage;
        var debugMessage = health <= 0 ? "You lost" : $"{health}";
        Debug.Log(debugMessage);
    }
}
