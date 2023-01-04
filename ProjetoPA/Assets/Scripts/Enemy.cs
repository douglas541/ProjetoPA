using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private CapsuleCollider2D playerCollider;
    [SerializeField] private Rigidbody2D enemyRigidBody;
    [SerializeField] private GameObject playerGameObject;
    List<RaycastHit2D> results = new List<RaycastHit2D>();
    ContactFilter2D contactFilter2D = new ContactFilter2D();
    bool enemyHit = false;
    private int enemyDamage = -1;
    private int enemyHealth = 25;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        EnemyHit();
    }

    private void EnemyHit()
    {
        Vector2 enemySize = new Vector2() { x = 0.5f, y = 0.1f };
        int hitCount = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0f, Vector2.down, contactFilter2D, results, 1f);
        bool wasHit = results.Any(result => result.rigidbody == enemyRigidBody);

        if (wasHit && !enemyHit)
        {
            enemyHit = true;
            playerGameObject.SendMessage("EnemyHit", enemyDamage);
        }
        else if (!wasHit)
        {
            enemyHit = false;
        }
    }

    public void SetEnemyHealth(int attackDamage)
    {
        enemyHealth -= attackDamage;
    }

    public int GetEnemyHealth()
    {
        return enemyHealth;
    }
}
