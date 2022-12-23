using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    //An int to know the damage the attack will deal
    [SerializeField] private int damage;
    //When a monster enters the trigger the damage will be dealt
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Monster")
        {
            collision.GetComponent<Skeleton>().StartDamage(damage);
        }
    }
}
