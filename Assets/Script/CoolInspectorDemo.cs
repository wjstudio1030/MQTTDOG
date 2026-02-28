using UnityEngine;

public class CoolInspectorDemo : MonoBehaviour
{
    public float speed = 5f;
    private Renderer rend;
    private bool isAnimating = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    public void ApplyMagic(Color c)
    {
        if (rend != null)
        {
            rend.material.color = c;
        }

        if (!isAnimating)
        {
            StartCoroutine(Spin());
        }
    }

    System.Collections.IEnumerator Spin()
    {
        isAnimating = true;
        float timer = 0f;
        while (timer < 2f)
        {
            transform.Rotate(Vector3.up, speed * 50 * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
        isAnimating = false;
    }
}
