using UnityEngine;

public class Fly : MonoBehaviour
{
    public float moveAmount = 0.1f; // Amount to move along Y-axis

    void Update()
    {
            Vector3 newPosition = transform.position + new Vector3(0f, moveAmount, 0f);
            transform.position = newPosition;
    }
}
