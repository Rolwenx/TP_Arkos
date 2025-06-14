using UnityEngine;

public class ForestClickable : MonoBehaviour
{
   public ForestController forestController;
    public Transform player;
    public float interactionDistance = 2.5f;
    public GameObject interactPrompt;

    private bool isInRange = false;

    void Update()
    {
        if (player == null || interactPrompt == null)
            return;

        float distance = Vector3.Distance(transform.position, player.position);
        isInRange = distance <= interactionDistance;

        interactPrompt.SetActive(isInRange);

        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            forestController.DisableToxic();
            interactPrompt.SetActive(false);
            Destroy(gameObject);
        }
    }
}