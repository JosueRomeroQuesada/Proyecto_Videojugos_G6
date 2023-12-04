using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]


public class Character2Dcontroller : MonoState<Character2Dcontroller>
{
    [Header("Move")]
    [SerializeField]
    float moveSpeed = 100.0f;

    [SerializeField]
    float maxSpeed = 7.0f;

    [SerializeField]
    float linearDrag = 4.0f;

    [SerializeField]
    bool isFacingRight = true;

    [Header("Jump")]
    [SerializeField]
    float jumpForce = 140.0f;

    
    [SerializeField]
    float fallMultiplier = 3.0f;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    LayerMask groundMask;

    [SerializeField]
    float jumpGraceTime = 0.20f;


    [Header("Attack")]
    [SerializeField]
    Transform attackPoint;

    [SerializeField]
    LayerMask enemyMask;

    [SerializeField]
    float reboundY=5.0f;

    [Header("Animation")]
    [SerializeField]
    public Animator animator;

    Rigidbody2D _rb;

    Vector2 _direction;

    bool _isMoving;
    bool _isJumping;
    bool _isJumpPressed;
    bool _isAttacking;

    float _gravityY;
    float _lastTimeJumpPressed;
    float _meleeDamage;

    [HideInInspector]
    public bool _canMove = true;
    protected override void Awake()
    {
        base.Awake();
    
        _rb = GetComponent<Rigidbody2D>();
        _gravityY = -Physics2D.gravity.y;
        
    }

    void Update()
    {
        HandleInputs();
        
    }

    void FixedUpdate()
    {
        HandleJump();
        HandleFlixX();
        HandleMove();
        
    }

    

    void HandleInputs() 
    {
        
        _direction= new Vector2 (Input.GetAxisRaw("Horizontal"),0.0f);
        _isMoving= _direction.x != 0.0f;

        _isJumpPressed = Input.GetButtonDown("Jump");
        if (_isJumpPressed) 
        {
            _lastTimeJumpPressed= Time.time;
        }
    }
    void HandleMove()
    {
        if (!_canMove)
        {
            return;
        }


        bool isMoving = animator.GetFloat("speed") > 0.01f;
        if (_isMoving != isMoving && !_isJumping)
        {
            animator.SetFloat("speed", Mathf.Abs(_direction.x));
        }

            
        

        Vector2 velocity = _direction * moveSpeed * Time.fixedDeltaTime;
        if (Mathf.Abs(velocity.x) > maxSpeed) 
        {
            
            velocity.x=Mathf.Sign(velocity.x) * maxSpeed ;
        }

        velocity.y = _rb.velocity.y;
        _rb.velocity = velocity;    

    }
    void HandleFlixX() 
    {
        if (!_isMoving) 
        {
            return;
        }
        bool facingRight = _direction.x > 0.0f;

        if (isFacingRight != facingRight)
        {
            isFacingRight = facingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);

            _rb.drag = linearDrag;
        }
        else 
        {
            _rb.drag = 0.0f;
        }
    }
    void HandleJump()
    {

        if (_lastTimeJumpPressed > 0.0f && Time.time - _lastTimeJumpPressed <= jumpGraceTime)
        {
            _isJumpPressed = true;
        }
        else
        {
            _lastTimeJumpPressed = 0.0f;
        }

        if (_isJumpPressed)
        {

            bool isGrounded = IsGrounded();
            if (isGrounded)
            {
                _rb.velocity += Vector2.up * jumpForce * Time.fixedDeltaTime;
            }

        }
        if (_rb.velocity.y < -0.01f)
        {
            _rb.velocity -= Vector2.up * _gravityY * fallMultiplier * Time.fixedDeltaTime;
        }


        _isJumping = !IsGrounded();

        bool isJumping = animator.GetBool("isJumping");
        if (_isJumping != isJumping)
        {
            animator.SetBool("isJumping", _isJumping);

        }
        bool isFalling = animator.GetBool("isFalling");
        bool isNegativeVelocityY = _rb.velocity.y > -0.01f;
        if (isNegativeVelocityY != isFalling) 
        {
            animator.SetBool("isFalling", isNegativeVelocityY);

        }

    }
    bool IsGrounded()
    {
        return

             Physics2D.OverlapCapsule(
                   groundCheck.position,new Vector2(1.0f, 0.01f),
                        CapsuleDirection2D.Horizontal,0.0f, groundMask);
    }

    public void Attack(float damage)
    {
        if (_isAttacking) 
        {
            return;
        }
        _isAttacking = true;
        _meleeDamage = damage;
        animator.SetBool("attack", true);
    }
    public void Attack() 
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, 0.2f, enemyMask);
        foreach (Collider2D collider in enemies) 
        {

        }
        animator.SetBool("attack", false);
    }

    public void Rebound()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, reboundY);
        
    }
}
