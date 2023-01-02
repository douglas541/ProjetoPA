using System.Collections.Generic;
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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        EnemyHit();
    }

    bool EnemyHit()
    {
        bool initialEnemyHit = enemyHit;
        enemyHit = false;
        int hit = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0.0f, Vector2.down, contactFilter2D, results, 1.0f);

        results.ForEach(result =>
        {
            if (result.rigidbody == enemyRigidBody)
            {
                enemyHit = true;
            }
        });

        if (initialEnemyHit != enemyHit && enemyHit == true)
        {
            playerGameObject.SendMessage("EnemyHit", enemyDamage);
        }

        return enemyHit;
    }
}
