using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickColorController : MonoBehaviour
{
    public Gradient gradient;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer.color = gradient.Evaluate(Random.Range(0f, 1f));
    }
}
