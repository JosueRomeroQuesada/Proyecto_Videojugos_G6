using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    float health;

    [SerializeField]
    float maxHealth = 100.0f;

    [SerializeField]
    float loseControlTimeout = 1f;

    [SerializeField]
    Vector2 reboundVelocity;

    HealthBarController _healthBarController;
    Character2DController _characterController;
  
    Rigidbody2D _rb;

    void Start()
    {
        health = maxHealth;

        _healthBarController = GetComponent<HealthBarController>();
        _healthBarController.Initialize(health);

        _characterController = Character2DController.Instance;
        

        
        _rb = GetComponent<Rigidbody2D>();
    }

    
    IEnumerator LoseControl() 
    {
        _characterController._canMove = false;
        yield return new WaitForSeconds(loseControlTimeout);
        _characterController._canMove = true;

    }

    void Rebound(Vector2 contactPoint) 
    {
        _rb.velocity =new Vector2
            (-reboundVelocity.x * contactPoint.x, reboundVelocity.y);
    }
    public void TakeDamage(float damage, Vector2 contactPoint)
    {
        health -= damage;
        if (health <= 0.0f)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            return;
        }
        _characterController.animator.SetTrigger("hit");
        _healthBarController.OnDamage.Invoke(damage);

        Rebound(contactPoint);
        StartCoroutine(LoseControl());
    }
    public void Heal(float value) 
    {
        health += Mathf.Abs(value);
        _healthBarController.Onheal.Invoke(value);

    }
}
