using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    private MeshRenderer renderer = null;
    private Vector2 offset = Vector2.zero;

    [SerializeField]
    private float speed = 0.1f;

    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();    
    }

    private void Update()
    {
        offset.y += speed * Time.deltaTime;
        renderer.material.SetTextureOffset("_MainTex", offset);
    }
}
