using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private float lastMonster = 0.0f;
    private int monsterPos = 0;
    [SerializeField] private GameObject normalMonster;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.fixedTime - lastMonster) > 3.0f)
        {
            monsterPos = Random.Range(1, 8);
            Instantiate(normalMonster, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity).GetComponent<Skeleton>().monsterPos = monsterPos;
            lastMonster = Time.time;
            //if (monsterPos == 8) monsterPos = 0;
        }
    }
}
