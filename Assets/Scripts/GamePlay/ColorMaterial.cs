using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMaterial : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer spriteRenderer;
    public Shader shader;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        spriteRenderer.material = new Material(shader);
    }


}
