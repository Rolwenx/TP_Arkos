using UnityEngine;

public class AudioAnchor : MonoBehaviour
{
    public Transform target; // usually the player or main camera

    void Update()
    {
        if (target != null)
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
    }
}
