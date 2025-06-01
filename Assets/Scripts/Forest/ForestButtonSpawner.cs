using UnityEngine;

public class ForestButtonSpawner : MonoBehaviour
{
    public GameObject ButtonPrefab;
    public ForestController forestController;
    public Transform[] spawnPoints;
    public Transform player;
    public GameObject interactPrompt;

    public void SpawnAtRandomPoint()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points assigned!");
            return;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform chosenPoint = spawnPoints[randomIndex];

        GameObject newButton = Instantiate(ButtonPrefab, chosenPoint.position, Quaternion.identity);
        ForestClickable clickScript = newButton.AddComponent<ForestClickable>();
        clickScript.forestController = forestController;
        clickScript.player = player;
        clickScript.interactPrompt = interactPrompt;
    }
}
