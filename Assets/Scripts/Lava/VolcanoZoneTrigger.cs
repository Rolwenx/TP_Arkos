using UnityEngine;
using StarterAssets;

public class VolcanoZoneTrigger : MonoBehaviour
{
    public GameObject volcanoIntroPanel;
    private ThirdPersonController movementScript; 

    private bool hasTriggered = false;
    private bool waitingForInput = false;
    public GameObject player; 

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            movementScript = player.GetComponent<ThirdPersonController>();
            if (movementScript != null)
                movementScript.enabled = false;


            hasTriggered = true;
            volcanoIntroPanel.SetActive(true);
            Time.timeScale = 0f;
            waitingForInput = true;
        }
    }

    private void Update()
    {
        if (waitingForInput && Input.GetKeyDown(KeyCode.Space))
        {
            volcanoIntroPanel.SetActive(false);
            Time.timeScale = 1f;
            waitingForInput = false;
            
            if (movementScript != null)
                    movementScript.enabled = true;
        }
    }
}
