using UnityEngine;
using TMPro;
using StarterAssets;

public class PNJBehaviour : MonoBehaviour
{

    public Camera mainCamera;
    public Camera pnjCamera;
    public Camera buttonCamera;

    public Transform player;
    public float interactionDistance = 2.5f;
    public GameObject interactPrompt;
    public GameObject dialoguePanel;

    public TextMeshProUGUI pnjText;
    public ThirdPersonController playerMovement;
    public Animator pnjAnimator;

    public Transform pointingTarget;


    private string[] dialogueLines = new string[]
    {
        "Hello...",
        "Didn't expect to see anyone out here in this snow.",
        "But you're not here by choice, are you?",
        "If you want to get out...",
        "And stop this horrible storm...",
        "Look over there.",
        "See that button?",
        "That might be your way out.",
        "Good luck."
    };

    private int currentLine = 0;
    private bool isTalking = false;
    void Update()
    {

        if (player == null || interactPrompt == null)
            return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (!isTalking)
        {
            // Show prompt when close
            if (distance <= interactionDistance)
            {
                interactPrompt.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    playerMovement.enabled = false;
                    mainCamera.gameObject.SetActive(false);
                    pnjCamera.gameObject.SetActive(true);
                    interactPrompt.SetActive(false);
                    isTalking = true;
                    currentLine = 0;
                    dialoguePanel.SetActive(true);

                    pnjText.text = dialogueLines[currentLine];
                }
            }
            else
            {
                interactPrompt.SetActive(false);
            }
        }
        else
        {
            // If talking: press Space to go to next dialogue
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentLine++;

                if (currentLine < dialogueLines.Length)
                {
                    pnjText.text = dialogueLines[currentLine];
                    if (pnjText.text == "Look over there.")
                    {
                        buttonCamera.gameObject.SetActive(true);
                        pnjCamera.gameObject.SetActive(false);

                        // look at the button
                        GameObject button = GameObject.FindWithTag("Button");
                        if (button != null)
                            buttonCamera.transform.LookAt(button.transform);
                    }
                    else
                    {
                        pnjCamera.gameObject.SetActive(true);
                    }

                    if (pnjText.text == "Look over there.")
                    {
                        pointingTarget.position = GameObject.FindWithTag("Button").transform.position;
                    }
                }
                else
                {
                    // End of dialogue
                    isTalking = false;
                    playerMovement.enabled = true;
                    dialoguePanel.gameObject.SetActive(false);
                    interactPrompt.SetActive(true);
                    mainCamera.gameObject.SetActive(true);
                    pnjCamera.gameObject.SetActive(false);
                    buttonCamera.gameObject.SetActive(false);
                }
            }
        }
    }

}