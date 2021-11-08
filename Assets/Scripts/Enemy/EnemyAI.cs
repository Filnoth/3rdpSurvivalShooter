using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Chase,
        Attack
    }

    private CharacterController _controller;
    private Transform _player;
    private float _speed = 3.5f;
    private Vector3 _velocity;
    private int _grav = 20;
    [SerializeField]
    private EnemyState _currentState = EnemyState.Chase;

    private Health _playerHealth;
    [SerializeField]
    private float _attackDelay = 1.5f;
    private float _nextAttack = -1;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerHealth = _player.GetComponent<Health>();

        if (_player == null || _playerHealth == null)
        {
            Debug.LogError("Player components are NULL");
        }

        if (_controller == null)
        {
            Debug.Log("Enemy controller is NULL");
        }
    }

    private void Update()
    {
        
        switch (_currentState)
        {
            case EnemyState.Attack:
                Attack();
                break;

            case EnemyState.Chase:
                CalculateMovement();
                break;
        }

    }
    
    void CalculateMovement()
    {
        //float distance = Vector3.Distance(transform.localPosition, _player.transform.localPosition);
        if (_controller.isGrounded == true)
        {
            Vector3 direction = _player.position - transform.position;
            direction.Normalize();
            direction.y = 0;
            transform.localRotation = Quaternion.LookRotation(direction);
            _velocity = direction * _speed;

        }

        _velocity.y -= _grav * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }

    void Attack()
    {
        if (Time.time > _nextAttack)
        {
            if (_playerHealth != null)
            {
                _playerHealth.Damage(25);
            }
            _nextAttack = Time.time + _attackDelay;

        }
    }


    public void AttackState()
    {
            _currentState = EnemyState.Attack;
    }

    public void ChaseState()
    {
        _currentState = EnemyState.Chase;
    }
}
