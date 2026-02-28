using UnityEngine;

public class DisableEdgeColliderOnCollision : MonoBehaviour
{
    private EdgeCollider2D edgeCollider;

    private void Start()
    {
        // Get the EdgeCollider2D component attached to the game object
        edgeCollider = GetComponent<EdgeCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision occurred with the object you want to trigger the removal of the EdgeCollider2D
        if (collision.gameObject.CompareTag("Collision"))
        {
            // Disable the EdgeCollider2D component
            edgeCollider.enabled = false;

            // Alternatively, you can remove the EdgeCollider2D component entirely if you don't need it later
            // Destroy(edgeCollider);
        }
    }
}
