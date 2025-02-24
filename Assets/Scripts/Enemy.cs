using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent _agent;

    public enum EnemyState
    {
        Patrolling,
        Chasing,
        Attacking
    }

    public EnemyState currentState;

    [SerializeField] private Transform[]_patrolPoints;
    private int _patrolIndex;
    private Transform _playerTransform;
    [SerializeField] private float _visionRange = 15;
    [SerializeField] private float _attackRange = 3;
    void Awake()
    {
        _agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _playerTransform = GameObject.FindWithTag("Player").transform;
    }
    
    void Start()
    {
        currentState = EnemyState.Patrolling;
        SetPatrolPoint();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            
            case EnemyState.Patrolling:
                Patrol();
            break;
            case EnemyState.Chasing:
                Chase();
            break;
            case EnemyState.Attacking:
                Attack();
            break;
        }
    }

    void Patrol()
    {
        if(InRange(_visionRange))
        {
            currentState = EnemyState.Chasing;
        }
        
        if(_agent.remainingDistance < 0.5f)
        {
            SetPatrolPoint();
        }
    }

    void SetPatrolPoint()
    {
        _agent.destination = _patrolPoints[Random.Range(0, _patrolPoints.Length)].position;
    }

    void Chase()
    {
        if(!InRange(_visionRange))
        {
            currentState = EnemyState.Chasing;
        }

        if(InRange(_attackRange))
        {
            currentState = EnemyState.Attacking;
        }

        _agent.destination = _playerTransform.position;
    }

    bool InRange(float range)
    {
        return Vector3.Distance(transform.position, _playerTransform.position) < range;
    }

    void Attack()
    {
        Debug.Log("atacando");
    }
}