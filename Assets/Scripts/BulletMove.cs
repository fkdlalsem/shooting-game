using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.5f;

    private GameManager gameManager = null;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        transform.Translate(Vector2.up * speed);
        if(transform.localPosition.y > gameManager.maximumPosition.y + 2f)
        {
            Destroy(gameObject);
        }
    }
}
