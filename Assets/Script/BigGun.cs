using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigGun : MonoBehaviour
{
    public AudioSource audiosource;
    [SerializeField] Projetile projetile;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && projetile.CanShoot)
        {
            audiosource.Play();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            audiosource.Pause();
        }

    }
    // Start is called before the first frame update
   
}
