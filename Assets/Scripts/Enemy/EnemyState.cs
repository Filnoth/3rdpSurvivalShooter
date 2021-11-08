using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    private EnemyAI _enemy;

    private void Start()
    {
        _enemy = GetComponentInParent<EnemyAI>();

        if (_enemy == null)
        {
            Debug.LogError("EnemyAI is NULL");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _enemy.AttackState();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _enemy.ChaseState();
        }
    }
}
