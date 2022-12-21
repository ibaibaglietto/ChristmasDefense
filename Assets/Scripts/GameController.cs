using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private float lastMonster = 0.0f;
    private int monsterPos = 0;
    private int health = 20;
    private float startTime;
    [SerializeField] private GameObject[] monsters;
    [SerializeField] private TextMeshProUGUI healthText;
    void Start()
    {
        healthText.text = health.ToString();
        startTime = Time.fixedTime;
        lastMonster = Time.fixedTime - 6.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.fixedTime - startTime < 24.0f)
        {
            Debug.Log("1");
            if ((Time.fixedTime - lastMonster) > 6f)
            {
                monsterPos = Random.Range(1, 9);
                Instantiate(monsters[0], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity).GetComponent<Skeleton>().monsterPos = monsterPos;
                lastMonster = Time.fixedTime;
            }
        }
        else if (Time.fixedTime - startTime < 60.0f)
        {
            Debug.Log("2");
            if ((Time.fixedTime - lastMonster) > 4.5f)
            {
                monsterPos = Random.Range(1, 9);
                Instantiate(monsters[Random.Range(0, 2)], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity).GetComponent<Skeleton>().monsterPos = monsterPos;
                lastMonster = Time.fixedTime;
            }
        }
        else 
        {
            Debug.Log("3");
            if ((Time.fixedTime - lastMonster) > 3.5f)
            {
                monsterPos = Random.Range(1, 9);
                Instantiate(monsters[Random.Range(0, 3)], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity).GetComponent<Skeleton>().monsterPos = monsterPos;
                lastMonster = Time.fixedTime;
            }
        }        
    }

    public void DealDamage(int d)
    {
        health -= d;
        if (health < 0)
        {
            health = 0;
            Debug.Log("Has perdido");
        }
        healthText.text = health.ToString();
        
    }
}
