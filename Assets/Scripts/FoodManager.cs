using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    Color color;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255));
        spriteRenderer.color = color;
    }
}
