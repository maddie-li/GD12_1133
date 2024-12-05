using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] public Item item;

    private float rotationSpeed = 150f;

    private SpriteRenderer spriteRenderer;

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            Sprite sprite = Sprite.Create((Texture2D)item.image, new Rect(0, 0, item.image.width, item.image.height), new Vector2(0.5f, 0.5f));
            spriteRenderer.sprite = sprite;
        }
    }

    void Update()
    {
        // Rotation effect (rotation around the Y axis)
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
