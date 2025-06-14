using UnityEngine;
using UnityEngine.AI;

public class RandomWalker : MonoBehaviour
{
    public Transform[] points;
    private NavMeshAgent agent;
    private int index;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.avoidancePriority = 70;
        index = Random.Range(0, points.Length);
        agent.SetDestination(points[index].position);
    }

    void Update()
    {
        if (agent != null && agent.isOnNavMesh)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                index = Random.Range(0, points.Length);
                agent.SetDestination(points[index].position);
            }
        }
    }

}
