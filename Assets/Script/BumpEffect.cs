using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpEffect : MonoBehaviour
{
    [SerializeField] AudioSource audiosource;
    private bool hasPlayed = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasPlayed && collision.gameObject.CompareTag("building"))
        {
            audiosource.Play();
            hasPlayed = true;
        }
    }
}
