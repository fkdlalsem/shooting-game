using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.5f;
    [SerializeField]
    private Transform bulletPosition = null;
    [SerializeField]
    private GameObject bulletPrefab = null;
    
    private Vector2 mousePosition = Vector2.zero;
    private Vector3 targetPosition = Vector3.zero;
    
    private GameManager gameManager = null;
    private Collider2D col = null;
    private SpriteRenderer spriteRenderer = null;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col.enabled = true;
        StartCoroutine(Fire());    
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) == true){
            mousePosition = Input.mousePosition;
            targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            targetPosition.x = Mathf.Clamp(targetPosition.x, gameManager.minimumPosition.x, gameManager.maximumPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, gameManager.minimumPosition.y, gameManager.maximumPosition.y);
            targetPosition.z = 0f;

            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, speed * Time.deltaTime);
        }
    }
    IEnumerator Fire() {
        while (true){
            GameObject.Instantiate(bulletPrefab, bulletPosition.position, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }
    }
    IEnumerator Revive(){
        col.enabled = false;

        int count = 0;
        while(count < 5){
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.25f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.25f);
            count++;
        }
        col.enabled = true;
    }
    
    private void OnTriggerEnter2D(Collider2D Collision){
        if(gameManager.life > 0){
            gameManager.life--;
            gameManager.UpdateLife();
            StartCoroutine(Revive());
        }
        else{
            SceneManager.LoadScene("GameOver");
        }
    }
}


//    [2차 수행평가 안내]
//1) 배경이 요구하는 종스크롤 방향으로 잘 움직이는가? (2중 모두)
//2) Player는 마우스 클릭 위치로 정확하게 이동하는가?
//3) 마우스 클릭 위치를 World 좌표계로 변환하여 이동했는가?
//4) Player 이동에 경계 영역을 지정해 주었는가?
//5) Player가 경계 영역을 벗어날 경우 이동에 제한을 주었는가?
//6) Player가 총알을 잘 발사하는가?
//7) 총알이 경계 영역을 벗어날 경우 사라지는가?
//8) 코드 가독성이 좋은가?
//9) 코드 복잡도(Big-O)가 낮도록 최적화가 되어있는가?
//    ░░░░░░░░░▄░░░░░░░░░░░░░░▄░░░░
//    ░░░░░░░░▌▒█░░░░░░░░░░░▄▀▒▌░░░
//    ░░░░░░░░▌▒▒█░░░░░░░░▄▀▒▒▒▐░░░
//    ░░░░░░░▐▄▀▒▒▀▀▀▀▄▄▄▀▒▒▒▒▒▐░░░
//    ░░░░░▄▄▀▒░▒▒▒▒▒▒▒▒▒█▒▒▄█▒▐░░░
//    ░░░▄▀▒▒▒░░░▒▒▒░░░▒▒▒▀██▀▒▌░░░ 
//    ░░▐▒▒▒▄▄▒▒▒▒░░░▒▒▒▒▒▒▒▀▄▒▒▌░░
//    ░░▌░░▌█▀▒▒▒▒▒▄▀█▄▒▒▒▒▒▒▒█▒▐░░
//    ░▐░░░▒▒▒▒▒▒▒▒▌██▀▒▒░░░▒▒▒▀▄▌░
//    ░▌░▒▄██▄▒▒▒▒▒▒▒▒▒░░░░░░▒▒▒▒▌░
//    ▀▒▀▐▄█▄█▌▄░▀▒▒░░░░░░░░░░▒▒▒▐░
//    ▐▒▒▐▀▐▀▒░▄▄▒▄▒▒▒▒▒▒░▒░▒░▒▒▒▒▌
//    ▐▒▒▒▀▀▄▄▒▒▒▄▒▒▒▒▒▒▒▒░▒░▒░▒▒▐░
//    ░▌▒▒▒▒▒▒▀▀▀▒▒▒▒▒▒░▒░▒░▒░▒▒▒▌░
//    ░▐▒▒▒▒▒▒▒▒▒▒▒▒▒▒░▒░▒░▒▒▄▒▒▐░░
//    ░░▀▄▒▒▒▒▒▒▒▒▒▒▒░▒░▒░▒▄▒▒▒▒▌░░
//    ░░░░▀▄▒▒▒▒▒▒▒▒▒▒▄▄▄▀▒▒▒▒▄▀░░░
//    ░░░░░░▀▄▄▄▄▄▄▀▀▀▒▒▒▒▒▄▄▀░░░░░
//    ░░░░░░░░░▒▒▒▒▒▒▒▒▒▒▀▀░░░░░░░░
