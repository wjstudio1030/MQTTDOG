using UnityEngine;

public class ObjectActivation : MonoBehaviour
{
    public GameObject objectToActivate;

    private void Start()
    {
        // Activate the GameObject at the start of the game.
        objectToActivate.SetActive(true);
    }
}
