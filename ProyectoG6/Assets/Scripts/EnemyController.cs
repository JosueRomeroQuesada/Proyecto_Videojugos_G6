using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    float damage = 10.0f;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player")) 
        {
            Vector2 contactPoint = other.GetContact(0).normal;

            if (contactPoint.y < -0.9)
            {
                Character2DController.Instance.Rebound();

                Destroy(gameObject);
            }
            else
            {
                HealthController controller = other.collider.GetComponent<HealthController>();
                controller.TakeDamage(damage, other.GetContact(0).normal);


            }
        }
    
        
    }
}

