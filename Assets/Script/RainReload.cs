using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainReload : MonoBehaviour
{
    public GameObject gameobject;
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("reload"))
        {
            gameobject.SetActive(false); // Deactivate the game object
            Debug.Log("touch");
        }
    }

}
