using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AggroController : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    float speed;

    [SerializeField]
    float awakeDistance;

    [SerializeField]
    float stopDistance;

    [SerializeField]
    bool isFacingRight;

    float _distance;

    bool _isChasing;

    Vector2 _position;

    Animator _animator;

    void Start()
    {
        _position = transform.position;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _distance= Vector2.Distance(transform.position, player.position);
        if (_distance < awakeDistance && !_isChasing)
        {
            _isChasing = true;
            _animator.SetFloat("speed", 1.0f);
        }
        else if (_distance >= stopDistance && _isChasing) 
        {
            _isChasing = false;
            _animator.SetFloat("speed", 0.0f);
            _animator.SetTrigger("shoot");

        }

        Vector2 lookAt = Vector2.zero;

        if (_isChasing) 
        {
            Vector2 position = new Vector2(player.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position,position, speed * Time.deltaTime);
        }

        if (lookAt.x > transform.position.x)
        {
            if (!isFacingRight)
            {
                isFacingRight = true;
                transform.Rotate(0.0f, 180.0f, 0.0f);
            }
        }
        else 
        {
            if (!isFacingRight) 
            {
                isFacingRight = false;
                transform.Rotate(0.0f, 180.0f,0.0f);
            }
        }
        
    }


}
