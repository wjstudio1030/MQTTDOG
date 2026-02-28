using UnityEngine;

public class SquareController : MonoBehaviour
{
    public GameObject brokenSquarePrefab; // Assign a prefab of the broken square in the Inspector

    private bool isBroken = false;

    // This method is automatically called when a collision occurs with another collider
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision involves the objects you want to interact with
        // You can use tags, layers, or specific GameObject names to filter the collision if needed
        if (!isBroken && collision.gameObject.CompareTag("Obstacle"))
        {
            // Call a method to handle the "splitting" of the square
            SplitSquare();
        }
    }

    private void SplitSquare()
    {
        // Perform the actions to split the square
        // For example, you could instantiate the broken square prefab at the current position
        // and then destroy the original square
        Instantiate(brokenSquarePrefab, transform.position, transform.rotation);
        Destroy(gameObject);

        isBroken = true; // Set the flag to prevent further splitting
    }
}
