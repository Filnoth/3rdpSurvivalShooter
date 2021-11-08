using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothDamp : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float _speed = 10f;

    void LateUpdate()
    {
        Vector3 targetPos = _target.transform.position;
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * _speed);
        transform.rotation = Quaternion.Euler(_target.transform.rotation.eulerAngles);
    }
}
