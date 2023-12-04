using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PatrolController : MonoBehaviour
{
    
    [SerializeField]
    float Speed ;

    [SerializeField]
    bool isFacingRight = true;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    LayerMask groundMask;

    Rigidbody2D _rb;

   

    void Awake()
    {
        

        _rb = GetComponent<Rigidbody2D>();
        

    }
   
    void FixedUpdate()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.30f);
        if (!raycastHit2D) 
        {
            FlipX();
        }
        _rb.velocity = new Vector2(Speed *Time.fixedDeltaTime, _rb.velocity.y);
        

    }

    private void FlipX()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f,180.0f,0.0f);
        Speed *= -1;
    }

   
    bool IsGrounded()
    {
        return

             Physics2D.OverlapCapsule(
                   groundCheck.position, new Vector2(0.8f, -0.8f),
                        CapsuleDirection2D.Horizontal, 0.0f, groundMask);
    }
}
