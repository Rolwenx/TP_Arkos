using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class AgentController : MonoBehaviour
{
    public Transform cible;
     public GameObject victoryPanel;

    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.updateRotation = false;
        agent.avoidancePriority = 10;
        agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;

    }

    void Update()
    {
        if (agent != null && agent.isOnNavMesh)
        {
            agent.SetDestination(cible.position);

            float speed = agent.velocity.magnitude;
            float distance = Vector3.Distance(transform.position, cible.position);
            Debug.Log(distance);
            if (distance < 1.5f)
            {
                TriggerVictory();
            }
            if (animator != null)
            {
                animator.SetFloat("Speed", agent.velocity.magnitude);
            }

        }
    }
    
    void TriggerVictory()
    {
        Debug.Log("TriggerVictory called");

        if (victoryPanel != null && !victoryPanel.activeSelf)
        {
            victoryPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            agent.isStopped = true;
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
