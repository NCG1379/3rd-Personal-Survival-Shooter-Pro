using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Chase,
        Attack
    }


    private CharacterController _enemyController;
    [Header("Enemy Settings")]
    [SerializeField]
    private float _speed = 2.0f;

    Vector3 _direction;
    Vector3 _velocity;
    private float _gravity = -20.0f;

    Transform _playerController;

    [SerializeField]
    private EnemyState _currentState;

    private Health _health;
    private float _attackDelay = 1.5f;
    private float _nextAttack = -1;

    private void Start()
    {
        _enemyController = GetComponent<CharacterController>();

        if (_enemyController == null)
            Debug.LogError("_enemyController is NULL");

        _playerController = GameObject.FindGameObjectWithTag("Player").transform;
        _health = _playerController.GetComponent<Health>();

        if (_playerController == null || _health == null)
            Debug.LogError("Player Components are NULL");

        _currentState = EnemyState.Chase;
    }

    private void Update()
    {
        switch (_currentState)
        {
            case EnemyState.Chase:
                Movement();
                break;
            case EnemyState.Attack:
                Attack();
                break;
        }
    }

    public void Movement()
    {
        if (_enemyController.isGrounded)
        {
            _direction = _playerController.position - transform.position;
            _direction.y = 0;
            _direction.Normalize();
            _velocity = _direction * _speed;
        }

        transform.rotation = Quaternion.LookRotation(_direction);

        _velocity.y += _gravity;
        _enemyController.Move(_velocity * Time.deltaTime);
    }

    void Attack()
    {
        if (Time.time > _nextAttack)
        {
            if (_health != null)
            {
                _health.Damage(10);
                _nextAttack = Time.time + _attackDelay;
            }
        }
    }

    public void CurrentState(int state )
    {
        switch (state)
        {
            case 0:
                _currentState = EnemyState.Idle;
                break;
            case 1:
                _currentState = EnemyState.Chase;
                break;
            case 2:
                _currentState = EnemyState.Attack;
                break;
            default:
                break;
        }
        
    }

}
