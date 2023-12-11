using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    public CollectableTypes collectableTypes;

    [SerializeField]
    public bool isHealth = false;

    [SerializeField]
    public float value;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            InventoryController.Instance.Add(collectableTypes,value,isHealth);
            Destroy(gameObject);
        }

    
        
    }

}
