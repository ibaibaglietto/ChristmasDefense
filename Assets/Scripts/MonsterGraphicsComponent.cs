using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGraphicsComponent : GraphicsComponent
{
    //The states of the state machine that we will follow
    enum State
    {
        STATE_FLY_RIGHT,
        STATE_FLY_LEFT,
        STATE_ATTACKING_RIGHT,
        STATE_ATTACKING_LEFT,
        STATE_HURTING_RIGHT,
        STATE_HURTING_LEFT,
        STATE_DEAD_LEFT,
        STATE_DEAD_RIGHT
    };
    //The starting state
    private State state = State.STATE_FLY_RIGHT;

    //We built a state machine to send the correct variables to the animator and to play the correct sounds when the state changes.
    public override void Update(Skeleton gameObject)
    {
        switch (state)
        {
            case State.STATE_FLY_RIGHT:
                if (gameObject.dead)
                {
                    gameObject.GetComponent<Animator>().SetBool("isDead", true);
                    state = State.STATE_DEAD_RIGHT;
                }
                else if (gameObject.takeDamage)
                {
                    gameObject.GetComponent<Animator>().SetBool("takeDamage", true);
                    gameObject.StartTakingDamage();
                    state = State.STATE_HURTING_RIGHT;
                }
                else if (gameObject.lookingRight == -1)
                {
                    gameObject.GetComponent<Animator>().SetBool("lookingRight", false);
                    state = State.STATE_FLY_LEFT;
                }
                else if (gameObject.attack)
                {
                    gameObject.GetComponent<Animator>().SetBool("isAttacking", true);
                    gameObject.StartAttacking();
                    state = State.STATE_ATTACKING_RIGHT;
                }
                break;
            case State.STATE_FLY_LEFT:
                if (gameObject.dead)
                {
                    gameObject.GetComponent<Animator>().SetBool("isDead", true);
                    state = State.STATE_DEAD_LEFT;
                }
                else if (gameObject.takeDamage)
                {
                    gameObject.GetComponent<Animator>().SetBool("takeDamage", true);
                    gameObject.StartTakingDamage();
                    state = State.STATE_HURTING_LEFT;
                }
                else if (gameObject.lookingRight == 1)
                {
                    gameObject.GetComponent<Animator>().SetBool("lookingRight", true);
                    state = State.STATE_FLY_RIGHT;
                }
                else if (gameObject.attack)
                {
                    gameObject.GetComponent<Animator>().SetBool("isAttacking", true);
                    gameObject.StartAttacking();
                    state = State.STATE_ATTACKING_LEFT;
                }
                break;
            case State.STATE_ATTACKING_RIGHT:
                if (gameObject.dead)
                {
                    gameObject.GetComponent<Animator>().SetBool("isDead", true);
                    state = State.STATE_DEAD_RIGHT;
                }
                else if (gameObject.takeDamage)
                {
                    gameObject.GetComponent<Animator>().SetBool("takeDamage", true);
                    gameObject.GetComponent<Animator>().SetBool("isAttacking", false);
                    gameObject.StartTakingDamage();
                    state = State.STATE_HURTING_RIGHT;
                }
                else if (!gameObject.attacking)
                {
                    gameObject.GetComponent<Animator>().SetBool("isAttacking", false);
                    state = State.STATE_FLY_RIGHT;
                }
                break;
            case State.STATE_ATTACKING_LEFT:
                if (gameObject.dead)
                {
                    gameObject.GetComponent<Animator>().SetBool("isDead", true);
                    state = State.STATE_DEAD_LEFT;
                }
                else if (gameObject.takeDamage)
                {
                    gameObject.GetComponent<Animator>().SetBool("takeDamage", true);
                    gameObject.GetComponent<Animator>().SetBool("isAttacking", false);
                    gameObject.StartTakingDamage();
                    state = State.STATE_HURTING_LEFT;
                }
                else if (!gameObject.attacking)
                {
                    gameObject.GetComponent<Animator>().SetBool("isAttacking", false);
                    state = State.STATE_FLY_LEFT;
                }
                break;
            case State.STATE_HURTING_RIGHT:
                if (gameObject.dead)
                {
                    gameObject.GetComponent<Animator>().SetBool("isDead", true);
                    gameObject.GetComponent<Animator>().SetBool("takeDamage", false);
                    state = State.STATE_DEAD_RIGHT;
                }
                else if (!gameObject.takingDamage)
                {
                    gameObject.GetComponent<Animator>().SetBool("takeDamage", false);
                    state = State.STATE_FLY_RIGHT;
                }
                break;
            case State.STATE_HURTING_LEFT:
                if (gameObject.dead)
                {
                    gameObject.GetComponent<Animator>().SetBool("isDead", true);
                    gameObject.GetComponent<Animator>().SetBool("takeDamage", false);
                    state = State.STATE_DEAD_LEFT;
                }
                else if (!gameObject.takingDamage)
                {
                    gameObject.GetComponent<Animator>().SetBool("takeDamage", false);
                    state = State.STATE_FLY_LEFT;
                }
                break;
        }
    }
}
