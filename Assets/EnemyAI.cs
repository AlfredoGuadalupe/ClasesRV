using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform Player;
    private NavMeshAgent agent;
    public float distance = 10;
    private Vector3 _posicionInicial;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _posicionInicial = agent.gameObject.transform.position;
    }

    void Update()
    {
        agent.SetDestination(Player.position);
        float temp = agent.remainingDistance;
        if (temp != Mathf.Infinity && temp != 0)
        {
            distance = temp;
        }
    }

    public void Run()
    {
        agent.Warp(_posicionInicial);
        agent.isStopped = false;
        distance = 10;
    }

    public void Stop()
    {
        if (agent != null)
        {
            agent.isStopped = true;
        }
    }
}
