using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    Transform destination;

    [SerializeField]
    float speed = 3.0f;

    Rigidbody2D _rb;

    void Start()
    {
        _rb = player.GetComponent<Rigidbody2D>();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Vector2.Distance(player.position, transform.position) > 0.3f) 
            {
                StartCoroutine(TeleportCoroutine());
            }
        }
        
    }

    IEnumerator TeleportCoroutine()
    {

        _rb.simulated = false;
        StartCoroutine(MoveToCoroutine());
        yield return new WaitForSeconds(0.5f);

        player.position =destination.position;
        yield return new WaitForSeconds(0.5f);
        _rb.simulated = true;

    }

    IEnumerator MoveToCoroutine()
    {
        float timer = 0.0f;
        while (timer < 0.5f) 
        {
            player.position = Vector2.MoveTowards(player.position, transform.position, speed* Time.deltaTime);
            yield return new WaitForEndOfFrame();

            timer += Time.deltaTime;
           
        }
    }
}
