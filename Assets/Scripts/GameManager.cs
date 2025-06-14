using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject startPanel;
    public AudioSource forestAudio;

    public Transform player;
    public float checkInterval = 0.5f;

    private bool panelDismissed = false;
    private float checkTimer = 0f;
    private string currentTerrainTag = "";

    void Awake()
    {
        startPanel.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    void Update()
    {
        if (!panelDismissed && Input.GetKeyDown(KeyCode.Space))
        {
            HidePanel();
        }

        if (panelDismissed)
        {
            checkTimer += Time.deltaTime;
            if (checkTimer >= checkInterval)
            {
                CheckPlayerTerrain();
                checkTimer = 0f;
            }
        }
    }

    void HidePanel()
    {
        if (startPanel != null)
        {
            startPanel.SetActive(false);
            panelDismissed = true;
            Time.timeScale = 1f;
        }
    }

    void CheckPlayerTerrain()
{
    RaycastHit hit;
    if (Physics.Raycast(player.position + Vector3.up, Vector3.down, out hit, 10f))
    {
        string hitTag = hit.collider.tag;

        if (hitTag == "ForestTerrain")
        {
            if (currentTerrainTag != "ForestTerrain")
            {
                currentTerrainTag = "ForestTerrain";
                if (forestAudio != null && !forestAudio.isPlaying)
                    forestAudio.Play();
            }
        }
        else if (hitTag == "SnowTerrain")
        {
            if (currentTerrainTag != "SnowTerrain")
            {
                currentTerrainTag = "SnowTerrain";
                if (forestAudio != null && forestAudio.isPlaying)
                    forestAudio.Stop();
            }
        }
    }
}

}
