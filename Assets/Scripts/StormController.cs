using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
public class StormController : MonoBehaviour
{
    public ParticleSystem stormSnow;
    public ParticleSystem stormFog;
    public ParticleSystem normalSnow;
    public Light globalLight;
    public GameObject door;
    public GameObject door2;
    public AudioSource clickSound;
    public TextMeshProUGUI messageText;
    public AudioSource blizzardSound;

    public void CalmStorm()
    {
        stormSnow.Stop();
        stormFog.Stop();
        normalSnow.Play();
        globalLight.intensity = 1;

        if (clickSound != null)
        {
            clickSound.Play();
        }
        if (blizzardSound != null)
            blizzardSound.Stop(); // 🔇 Stop blizzard sound

        StartCoroutine(DelayedDoorOpen());
    }

    private IEnumerator DelayedDoorOpen()
    {
        if (messageText != null)
        {
            messageText.gameObject.SetActive(true);
            messageText.text = "Proceed to next zone";
        }

        yield return new WaitForSeconds(3f); // Wait 3 seconds

        // Open the doors
        door.SetActive(false);
        door2.SetActive(false);

        messageText.gameObject.SetActive(false);
    }
}
