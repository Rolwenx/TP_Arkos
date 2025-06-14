using UnityEngine;
using TMPro;
using StarterAssets;

public class PNJBehaviour : MonoBehaviour
{

    public Camera mainCamera;
    public Camera pnjCamera;
    public Camera buttonCamera;

    public Transform player;
    public GameObject player_obj;
    public float interactionDistance = 2.5f;
    public GameObject interactPrompt;
    public GameObject dialoguePanel;

    public TextMeshProUGUI pnjText;
    public ThirdPersonController playerMovement;
    public Animator pnjAnimator;

    public Transform pointingTarget;


    private string[] dialogueLines = new string[]
    {
        "Bonjour...",
        "Je ne m'attendais pas à voir quelqu’un ici, dans toute cette neige.",
        "Mais tu n’es pas ici de ton plein gré, n’est-ce pas ?",
        "Si tu veux sortir d’ici...",
        "Et arrêter cette horrible tempête...",
        "Regarde par là-bas.",
        "Tu vois ce bouton ?",
        "C’est peut-être ta porte de sortie.",
        "Bonne chance."
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
                    player_obj.SetActive(false);
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
                    if (pnjText.text == "Regarde par là-bas.")
                    {
                        buttonCamera.gameObject.SetActive(true);
                        pnjCamera.gameObject.SetActive(false);
                        
                        // look at the button
                        GameObject button = GameObject.FindWithTag("Button");
                        pointingTarget.position = button.transform.position;
                        if (button != null)
                            buttonCamera.transform.LookAt(button.transform);
                    }
                    else
                    {
                        pnjCamera.gameObject.SetActive(true);
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
                    player_obj.SetActive(true);
                }
            }
        }
    }

}