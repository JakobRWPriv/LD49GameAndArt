using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyOne;
    public GameObject enemyTwo;
    public GameObject enemyOneWhite;
    public GameObject enemyTwoWhite;
    public GameObject enemyOneYellow;
    public GameObject enemyTwoYellow;
    public GameObject enemyOneBlack;
    public GameObject enemyTwoBlack;
    public float waitTime;
    float timeToReach;
    public bool enemyTwoSpawner;
    public ScoreHandler scoreHandler;
    public int orderInLayerToAdd = 0;

    void Start() {
        timeToReach = Time.time + waitTime;
    }

    // Update is called once per frame
    void Update() {
        if (waitTime > 1f) {
            waitTime -= Time.deltaTime * 0.005f;
        }
        if (Time.time > timeToReach) {
            timeToReach = Time.time + waitTime;
            SpawnEnemy();
        }
    }

    void SpawnEnemy() {
        orderInLayerToAdd += 15;

        int random = Random.Range(0, 2);
        if (random == 0) {
            if (scoreHandler.score < 40) {
                GameObject enemy = Instantiate(enemyOne, transform.position, Quaternion.identity);
                enemy.transform.position = new Vector3(enemy.transform.position.x, Random.Range(6f, 23f), 0);
                enemy.GetComponent<Enemy>().ChangeOrderInLayer(orderInLayerToAdd);
            } else if (scoreHandler.score < 80) {
                int random3 = Random.Range(0, 5);
                if (random3 == 0) {
                    GameObject enemy = Instantiate(enemyOneWhite, transform.position, Quaternion.identity);
                    enemy.transform.position = new Vector3(enemy.transform.position.x, Random.Range(6f, 23f), 0);
                    enemy.GetComponent<Enemy>().ChangeOrderInLayer(orderInLayerToAdd);
                } else {
                    GameObject enemy = Instantiate(enemyOne, transform.position, Quaternion.identity);
                    enemy.transform.position = new Vector3(enemy.transform.position.x, Random.Range(6f, 23f), 0);
                    enemy.GetComponent<Enemy>().ChangeOrderInLayer(orderInLayerToAdd);
                }
            } else if (scoreHandler.score < 120) {
                int random3 = Random.Range(0, 3);
                if (random3 == 0) {
                    GameObject enemy = Instantiate(enemyOneWhite, transform.position, Quaternion.identity);
                    enemy.transform.position = new Vector3(enemy.transform.position.x, Random.Range(6f, 23f), 0);
                enemy.GetComponent<Enemy>().ChangeOrderInLayer(orderInLayerToAdd);
                } else {
                    GameObject enemy = Instantiate(enemyOne, transform.position, Quaternion.identity);
                    enemy.transform.position = new Vector3(enemy.transform.position.x, Random.Range(6f, 23f), 0);
                enemy.GetComponent<Enemy>().ChangeOrderInLayer(orderInLayerToAdd);
                }
            } else if (scoreHandler.score < 160) {
                int random3 = Random.Range(0, 5);
                if (random3 == 0) {
                    GameObject enemy = Instantiate(enemyOneYellow, transform.position, Quaternion.identity);
                    enemy.transform.position = new Vector3(enemy.transform.position.x, Random.Range(6f, 23f), 0);
                enemy.GetComponent<Enemy>().ChangeOrderInLayer(orderInLayerToAdd);
                } else {
                    GameObject enemy = Instantiate(enemyOneWhite, transform.position, Quaternion.identity);
                    enemy.transform.position = new Vector3(enemy.transform.position.x, Random.Range(6f, 23f), 0);
                enemy.GetComponent<Enemy>().ChangeOrderInLayer(orderInLayerToAdd);
                }
            } else if (scoreHandler.score < 200) {
                int random3 = Random.Range(0, 3);
                if (random3 == 0) {
                    GameObject enemy = Instantiate(enemyOneYellow, transform.position, Quaternion.identity);
                    enemy.transform.position = new Vector3(enemy.transform.position.x, Random.Range(6f, 23f), 0);
                enemy.GetComponent<Enemy>().ChangeOrderInLayer(orderInLayerToAdd);
                } else {
                    GameObject enemy = Instantiate(enemyOneWhite, transform.position, Quaternion.identity);
                    enemy.transform.position = new Vector3(enemy.transform.position.x, Random.Range(6f, 23f), 0);
                enemy.GetComponent<Enemy>().ChangeOrderInLayer(orderInLayerToAdd);
                }
            } else if (scoreHandler.score < 240) {
                int random3 = Random.Range(0, 5);
                if (random3 == 0) {
                    GameObject enemy = Instantiate(enemyOneBlack, transform.position, Quaternion.identity);
                    enemy.transform.position = new Vector3(enemy.transform.position.x, Random.Range(6f, 23f), 0);
                enemy.GetComponent<Enemy>().ChangeOrderInLayer(orderInLayerToAdd);
                } else {
                    GameObject enemy = Instantiate(enemyOneYellow, transform.position, Quaternion.identity);
                    enemy.transform.position = new Vector3(enemy.transform.position.x, Random.Range(6f, 23f), 0);
                enemy.GetComponent<Enemy>().ChangeOrderInLayer(orderInLayerToAdd);
                }
            } else if (scoreHandler.score < 280) {
                int random3 = Random.Range(0, 3);
                if (random3 == 0) {
                    GameObject enemy = Instantiate(enemyOneBlack, transform.position, Quaternion.identity);
                    enemy.transform.position = new Vector3(enemy.transform.position.x, Random.Range(6f, 23f), 0);
                enemy.GetComponent<Enemy>().ChangeOrderInLayer(orderInLayerToAdd);
                } else {
                    GameObject enemy = Instantiate(enemyOneYellow, transform.position, Quaternion.identity);
                    enemy.transform.position = new Vector3(enemy.transform.position.x, Random.Range(6f, 23f), 0);
                enemy.GetComponent<Enemy>().ChangeOrderInLayer(orderInLayerToAdd);
                }
            } else {
                GameObject enemy = Instantiate(enemyOneBlack, transform.position, Quaternion.identity);
                enemy.transform.position = new Vector3(enemy.transform.position.x, Random.Range(6f, 23f), 0);
                enemy.GetComponent<Enemy>().ChangeOrderInLayer(orderInLayerToAdd);
            }
        }

        if (enemyTwoSpawner && random == 1) {
            int random2 = 0;
            
            if (scoreHandler.score < 60) {
                random2 = Random.Range(0, 9);
            } else if (scoreHandler.score < 120) {
                random2 = Random.Range(0, 7);
            } else if (scoreHandler.score < 180) {
                random2 = Random.Range(0, 5);
            } else if (scoreHandler.score < 240) {
                random2 = Random.Range(0, 4);
            }

            if (random2 == 0 && scoreHandler.amountOfEnemyTwos < 2) {
                GameObject enemyTwoObj = enemyTwo;
                if (scoreHandler.score < 40) {
                    enemyTwoObj = enemyTwo;
                } else if (scoreHandler.score < 80) {
                    int random4 = Random.Range(0, 5);
                    if (random4 == 1) {
                        enemyTwoObj = enemyTwoWhite;
                    }
                } else if (scoreHandler.score < 120) {
                    int random4 = Random.Range(0, 3);
                    if (random4 == 1) {
                        enemyTwoObj = enemyTwoWhite;
                    }
                } else if (scoreHandler.score < 160) {
                    enemyTwoObj = enemyTwoWhite;
                } else if (scoreHandler.score < 200) {
                    enemyTwoObj = enemyTwoWhite;

                    int random4 = Random.Range(0, 5);
                    if (random4 == 1) {
                        enemyTwoObj = enemyTwoYellow;
                    }
                } else if (scoreHandler.score < 240) {
                    enemyTwoObj = enemyTwoWhite;

                    int random4 = Random.Range(0, 3);
                    if (random4 == 1) {
                        enemyTwoObj = enemyTwoYellow;
                    }
                } else if (scoreHandler.score < 280) {
                    enemyTwoObj = enemyTwoYellow;

                    int random4 = Random.Range(0, 5);
                    if (random4 == 1) {
                        enemyTwoObj = enemyTwoBlack;
                    }
                } else {
                    enemyTwoObj = enemyTwoYellow;

                    int random4 = Random.Range(0, 3);
                    if (random4 == 1) {
                        enemyTwoObj = enemyTwoBlack;
                    }
                }


                int random5 = Random.Range(0, 2);

                if (random5 == 0) {
                    scoreHandler.amountOfEnemyTwos++;
                    GameObject enemy = Instantiate(enemyTwoObj, transform.position, Quaternion.identity);
                    enemy.transform.position = new Vector3(Random.Range(12f, 24f), Random.Range(14f, 19f), 0);
                    enemy.SetActive(true);
                    enemy.GetComponent<Enemy>().ChangeOrderInLayer(orderInLayerToAdd);
                } else {
                    scoreHandler.amountOfEnemyTwos++;
                    GameObject enemy = Instantiate(enemyTwoObj, transform.position, Quaternion.identity);
                    enemy.transform.position = new Vector3(Random.Range(-24f, -12f), Random.Range(14f, 19f), 0);
                    enemy.SetActive(true);
                    enemy.GetComponent<Enemy>().ChangeOrderInLayer(orderInLayerToAdd);
                }
            }
        }
    }
}
