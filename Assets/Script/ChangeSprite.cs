using UnityEngine;
using System.Collections;

public class ChangeSprite : MonoBehaviour
{
    public Sprite dogSprite; // Assign the "Cat" sprite in the Inspector
    public Sprite catSprite;
    public Sprite birdSprite;
    public SpriteRenderer spriteRendererDog;
    public SpriteRenderer spriteRendererCat;
    public SpriteRenderer spriteRendererbird;
    public ParticleSystem ShowEffect;
    public SpriteRenderer sprite;
    

    public bool NextCat;
    public bool NextBird;

    public bool isLocked = false;

    private void Start()
    {
        spriteRendererDog = GetComponent<SpriteRenderer>();
        spriteRendererCat = GetComponent<SpriteRenderer>();
        spriteRendererbird = GetComponent<SpriteRenderer>(); 
        NextCat = false;
        NextBird = false;
    }

    public void Update()
    {   
        sprite = GetComponent<SpriteRenderer>();

        if (isLocked) return;

        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangeToSpriteDog();
            ShowEffect.Play();
            NextCat = true;
            sprite.color = new Color(255f, 255f, 255f, 255f);
        }
        if (Input.GetKeyDown(KeyCode.O) && NextCat)
        {
            ChangeToSpriteCat();
            ShowEffect.Play();
            NextBird = true;
            sprite.color = new Color(255f, 255f, 255f, 255f);

        }
        if (Input.GetKeyDown(KeyCode.I) && NextBird)
        {
            ChangeToSpriteBird();
            ShowEffect.Play();
            sprite.color = new Color(255f, 255f, 255f, 255f);
        }

    }

    

    public void ChangeToSpriteDog()
    {
        // Check if the "Cat" sprite is assigned, then change the sprite
        if (dogSprite != null)
        {
            spriteRendererDog.sprite = dogSprite;
        }
       
    }
    public void ChangeToSpriteCat()
    {
        // Check if the "Cat" sprite is assigned, then change the sprite
        if (catSprite != null)
        {
            spriteRendererCat.sprite = catSprite;
        }

    }
    public void ChangeToSpriteBird()
    {
        // Check if the "Cat" sprite is assigned, then change the sprite
        if (birdSprite != null)
        {
            spriteRendererbird.sprite = birdSprite;
        }

    }

}
