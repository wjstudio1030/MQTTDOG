using UnityEngine;

public class MousePositionDisplay : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Debug.Log("Mouse Position: " + mousePosition);
        }
    }
}