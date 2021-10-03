using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTwo : Enemy
{
    public GameObject enemyOne;
    public ScoreHandler scoreHandler;
    public GameObject createParticles;

    void Start(){
        StartCoroutine(TeleportCo());
        StartCoroutine(WaitCo());
    }
    
    IEnumerator TeleportCo() {
        yield return new WaitForSeconds(3f);
        transform.position = new Vector3(Random.Range(-24f, 24f), Random.Range(14f, 19f), 0);
        if (transform.position.x > -12 && transform.position.x < 12) {
            if (transform.position.x <= 0) {
                transform.position = new Vector2(transform.position.x - 12, transform.position.y);
            } else {
                transform.position = new Vector2(transform.position.x + 12, transform.position.y);
            }
        }
        StartCoroutine(TeleportCo());
    }

    IEnumerator WaitCo() {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(ShootCo());
    }

    IEnumerator ShootCo() {
        yield return new WaitForSeconds(3f);
        int random = Random.Range(0, 1);
        if (random == 0) {
            Enemy enemy = Instantiate(enemyOne, transform.position, Quaternion.identity).GetComponent<Enemy>();
            enemy.ChangeOrderInLayer(300);
            Instantiate(createParticles, transform.position, Quaternion.identity);
        }
        StartCoroutine(ShootCo());
    }

    void OnDestroy() {
        scoreHandler.amountOfEnemyTwos--;
    }
}
