using UnityEngine;

public class ClickableButton : MonoBehaviour
{
    public StormController stormController;
    public Transform player;
    public float interactionDistance = 2.5f;
    public GameObject interactPrompt;

    void Update()
    {
        if (player == null || interactPrompt == null)
            return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= interactionDistance)
        {
            interactPrompt.SetActive(true);

            if (Input.GetMouseButtonDown(0)) 
            {
                stormController.CalmStorm();
                interactPrompt.SetActive(false);
                Destroy(gameObject);
            }
        }
        else
        {
            interactPrompt.SetActive(false);
        }
    }
}
