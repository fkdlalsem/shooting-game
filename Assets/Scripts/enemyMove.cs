using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMove : MonoBehaviour
{
    [SerializeField]
    protected float speed = 0.5f;

    [SerializeField]
    private int rewardScore = 100;
    [SerializeField]
    private int hp = 2;

    protected GameManager gameManager = null;
    private SpriteRenderer spriteRenderer = null;
    private Animator animator = null;

    protected bool isDead = false;

    protected virtual void Start(){
        gameManager = FindObjectOfType<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        animator.Play("Idle");
    }

    protected virtual void Update(){
        if(isDead)
        {
            return;
        }
        transform.Translate(Vector2.down * speed);
        if(transform.localPosition.y < gameManager.minimumPosition.y - 2f)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.tag == "Bullet")
        {
            hp--;
            Destroy(collision.gameObject);

            if(hp <= 0)
            {
                isDead = true;
                gameManager.score +=rewardScore;
                gameManager.UpdateScore();
                animator.Play("Explosion");
                Destroy(gameObject, 0.5f);
            }
            else
            {
                StartCoroutine(DamageEffect());
            }
        }
    }

    private IEnumerator DamageEffect(){
        spriteRenderer.material.SetColor("_Color", new Color(1f, 1f, 1f, 0f));
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material.SetColor("_Color", new Color(0f, 0f, 0f, 0f));
    }
}
