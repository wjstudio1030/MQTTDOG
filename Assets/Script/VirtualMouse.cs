using UnityEngine;

public class VirtualMouse : MonoBehaviour
{
    // The position where the virtual mouse should stop
    public Vector3 stopPosition = new Vector3(740.3f, 166.4f, 0.0f);

    // Speed at which the virtual mouse moves
    public float moveSpeed = 5f;

    void Update()
    {
        // Get the current position of the virtual mouse
        Vector3 currentPosition = transform.position;

        // Get the target direction to move towards the stop position
        Vector3 targetDirection = stopPosition - currentPosition;

        // Calculate the distance to the target position
        float distanceToTarget = targetDirection.magnitude;

        // If the distance is greater than the move speed, move towards the target
        if (distanceToTarget > moveSpeed * Time.deltaTime)
        {
            Vector3 moveDirection = targetDirection.normalized * moveSpeed;
            transform.Translate(moveDirection * Time.deltaTime);
        }
        else
        {
            // If the distance is smaller than the move speed, set the position to the stop position
            transform.position = stopPosition;
        }
    }
}
