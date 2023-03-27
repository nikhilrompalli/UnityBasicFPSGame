using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent enemy;
    public Transform player;
    int hitRate = 3;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        enemy.destination = player.position;
    }

    public void TakeDamage(int hitValue)
    {
        hitRate -= hitValue;
        Debug.Log("Bullet Hit By Enemy - " + hitRate);
        if (hitRate <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}

