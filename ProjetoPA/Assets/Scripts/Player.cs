using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health = 10;
    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Attack()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {

        }
    }
}
