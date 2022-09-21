using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class StickmanMover : MonoBehaviour
{
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void SetTarget(Transform target)
    {
        if (target == null)
            throw new NullReferenceException(nameof(target));

        _agent.SetDestination(target.position);
    }

    public void Continue()
    {
        _agent.isStopped = false;
    }

    public void Stop()
    {
        _agent.isStopped = true;
    }

    public void ResetTarget()
    {
        _agent.SetDestination(transform.position);
    }
}
