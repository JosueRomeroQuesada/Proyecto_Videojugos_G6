using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField]
    GameObject[] targets;

    [SerializeField]
    Transform[] portals;

    CinemachineTargetGroup _targetGroup;
    CinemachineVirtualCamera _virtualCamera;
    Collider2D _collider ;

    void Start()
    {
        _targetGroup = GetComponent<CinemachineTargetGroup>();
        _virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        _collider = GetComponent<Collider2D>();



    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _virtualCamera.Follow = _targetGroup.transform;

            foreach (var target in targets)
            {
                _targetGroup.AddMember(target.transform, 1, 1);
            }

            foreach (var portal in portals)
            {
                portal.gameObject.SetActive(false);
            }

            
            GetComponent<Collider>().enabled = false;
        }
    }
}
