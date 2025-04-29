using UnityEngine;

public class LoopBackground : MonoBehaviour
{
    public float speed = 2f;
    private float spriteWidth;

    void Start()
    {
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x <= Camera.main.transform.position.x - spriteWidth)
        {
            transform.position += Vector3.right * (spriteWidth * 2f - 0.05f);
        }
    }



}
