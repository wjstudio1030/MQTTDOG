using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillReload : MonoBehaviour
{
    [SerializeField] Projetile projetile;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Skill"))
        {
            projetile.raineffect.SetActive(false); // Deactivate the game object
            projetile.raineffect.transform.position = new Vector3(2.22f, 14.3f, 0f);
            projetile.cometupeffect.SetActive(false);
            projetile.cometupeffect.transform.position = new Vector3(2.22f, 14.3f, 0f);
            projetile.cometdowneffect.SetActive(false);
            projetile.cometdowneffect.transform.position = new Vector3(0f, 0f, 0f);
            projetile.stoneeffect.SetActive(false);
            projetile.stoneeffect.transform.position = new Vector3(2f, 9.09f, 0f);
            //projetile.Candogvoice = true;
            //projetile.CanBirdvoice = true;
            //projetile.CanCatvoice = true;

            Debug.Log("touch");
        }
    }
}
