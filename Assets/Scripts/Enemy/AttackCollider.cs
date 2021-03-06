using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    private EnemyAI _enemyAI;

    private int Attack = 2;
    private int Chase = 1;
    private int Idle = 0;

    private void Start()
    {
        _enemyAI = GameObject.Find("Enemy").GetComponent<EnemyAI>();

        if (_enemyAI == null)
            Debug.LogError("_enemyAI is NULL");
    }
    private void OnTriggerEnter(Collider other)
    {
        //Begin attacking
        if (other.CompareTag("Player"))
        {
            _enemyAI.CurrentState(Attack);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _enemyAI.CurrentState(Chase);
        }
        //Resume movement
    }
}
