using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D playerRigidBody;
    public float movementSpeed = 5;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMovement();
    }

    void HorizontalMovement () {
        if(Input.GetKey(KeyCode.RightArrow)) {
            transform.position += Vector3.right * movementSpeed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.LeftArrow)) {
            transform.position += Vector3.left * movementSpeed * Time.deltaTime;
        }
    }
}