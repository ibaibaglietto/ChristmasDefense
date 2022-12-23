using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterPhysicsComponent : PhysicsComponent
{

    public override void FixedUpdate(Skeleton gameObject)
    {
        //We change the looking right boolean depending on the velocity of the monster
        if (gameObject.GetComponent<NavMeshAgent>().desiredVelocity.x < 0 && gameObject.lookingRight == 1) gameObject.lookingRight = -1;
        else if (gameObject.GetComponent<NavMeshAgent>().desiredVelocity.x > 0 && gameObject.lookingRight == -1) gameObject.lookingRight = 1;
        //When the monster dies it will fall to the ground
        if (gameObject.dead)
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
        //When the monster is hitted it stops. When the animation finishes it starts moving again.
        if (gameObject.takingDamage && !gameObject.dead && !gameObject.GetComponent<NavMeshAgent>().isStopped) gameObject.GetComponent<NavMeshAgent>().isStopped = true; 
        else if(!gameObject.takingDamage && !gameObject.dead && gameObject.GetComponent<NavMeshAgent>().isStopped) gameObject.GetComponent<NavMeshAgent>().isStopped = false;
    }

}
