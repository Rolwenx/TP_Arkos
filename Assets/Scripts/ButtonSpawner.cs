using UnityEngine;

public class ButtonSpawner : MonoBehaviour
{
    public GameObject stormButtonPrefab;
    public StormController stormController;
    public Transform[] spawnPoints;
    public Transform player;
    public GameObject interactPrompt;

    void Start()
    {
        SpawnAtRandomPoint();
    }

    void SpawnAtRandomPoint()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points assigned!");
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform chosenPoint = spawnPoints[randomIndex];

        GameObject newButton = Instantiate(stormButtonPrefab, chosenPoint.position, Quaternion.identity);
        ClickableButton clickScript = newButton.AddComponent<ClickableButton>();
        clickScript.stormController = stormController;
        clickScript.player = player;
        clickScript.interactPrompt = interactPrompt;
    }
}
