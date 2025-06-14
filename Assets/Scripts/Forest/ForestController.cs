using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using StarterAssets;

public class ForestController : MonoBehaviour
{
    public GameObject door;
    public GameObject door2;
    public AudioSource clickSound;
    public AudioSource ouchSound;
    public GameObject toxicMessage;
    public TextMeshProUGUI messageText;

    private ParticleSystem[] allToxicParticles;

    public GameObject introPanel;
    public GameObject tooLatePanel;
    public TextMeshProUGUI timerText;
    public GameObject fogToActivate;
    public float timeLimit = 60f;


    private bool challengeStarted = false; 
    public bool forestChallengeFinished = false;

    private bool timerStarted = false;
    private bool toxicDisabled = false;
    private float timer;

    public GameObject player; 
    private ThirdPersonController movementScript; 

    private int toxicHits = 0;
    private int maxToxicHits = 3;



    public void StartForestChallenge()
    {

        movementScript = player.GetComponent<ThirdPersonController>();
        if (movementScript != null)
            movementScript.enabled = false;

        challengeStarted = true;

        introPanel.SetActive(true);
        timerText.gameObject.SetActive(false);

        if (fogToActivate != null)
            fogToActivate.SetActive(false);

        GameObject[] toxicZones = GameObject.FindGameObjectsWithTag("ToxicZone");
        var tempList = new System.Collections.Generic.List<ParticleSystem>();

        foreach (GameObject zone in toxicZones)
        {
            tempList.AddRange(zone.GetComponentsInChildren<ParticleSystem>());
        }

        allToxicParticles = tempList.ToArray();
    }

    private void Update()
    {
        if (!challengeStarted) return;

            if (!timerStarted && Input.GetKeyDown(KeyCode.Space))
            {
                introPanel.SetActive(false);
                Time.timeScale = 1f;
                timerText.gameObject.SetActive(true);
                timer = timeLimit;
                timerStarted = true;

                if (movementScript != null)
                    movementScript.enabled = true;
        
            }

        if (timerStarted && !toxicDisabled)
        {
            timer -= Time.deltaTime;
            timerText.text = "Temps restant : " + Mathf.Ceil(timer) + "s";

            if (timer <= 0)
            {
                TriggerFailure();
            }
        }
    }

    public void ShowToxicWarning()
    {
        if (toxicDisabled) return;
        toxicHits++;
        if (toxicMessage != null)
            toxicMessage.SetActive(true);

        if (ouchSound != null)
            ouchSound.Play();

        Invoke(nameof(HideMessage), 2.5f);

        if (toxicHits >= maxToxicHits)
        {
            TriggerFailure(); 
        }
    }


    void HideMessage()
    {
        if (messageText != null)
        {
            toxicMessage.SetActive(false);
        }
    }

    public void DisableToxic()
    {
        toxicDisabled = true;
        timerStarted = false;
        challengeStarted = false;
        timerText.gameObject.SetActive(false);
        foreach (ParticleSystem ps in allToxicParticles)
        {
            ps.Stop();
        }

        if (clickSound != null)
            clickSound.Play();

        StartCoroutine(DelayedDoorOpen());
    }

    private IEnumerator DelayedDoorOpen()
    {
        if (messageText != null)
        {
            messageText.gameObject.SetActive(true);
            messageText.text = "La prochaine zone est disponible";
        }

        yield return new WaitForSeconds(3f);

        door.SetActive(false);
        door2.SetActive(false);
        timerText.gameObject.SetActive(false);

        messageText.gameObject.SetActive(false);
    }
    
    void TriggerFailure()
    {
        if (fogToActivate != null) fogToActivate.SetActive(true);
        tooLatePanel.SetActive(true);
        Invoke("Restart", 5f);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
