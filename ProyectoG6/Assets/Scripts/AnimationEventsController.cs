using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventsController : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnAttack()
    {
        Character2DController.Instance.Attack();
       
    }
}
