using UnityEngine;

public class ToxicZone : MonoBehaviour
{

    public Transform player;
    public ForestController forestController;
    public float dangerDistance = 3.5f;
    public float warningCooldown = 2f;

    private float lastWarningTime = -999f;

    void Update()
    {
        if (player == null || forestController == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= dangerDistance && Time.time - lastWarningTime >= warningCooldown)
        {
            lastWarningTime = Time.time;
            forestController.ShowToxicWarning();
        }
    }
}

