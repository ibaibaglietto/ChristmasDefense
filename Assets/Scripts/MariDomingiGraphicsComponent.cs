using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MariDomingiGraphicsComponent : GraphicsComponent
{
    //The states of the state machine that we will follow
    enum State
    {
        STATE_IDLE_RIGHT,
        STATE_IDLE_LEFT,
        STATE_RUNNING_RIGHT,
        STATE_RUNNING_LEFT,
        STATE_JUMPING_RIGHT,
        STATE_JUMPING_LEFT,
        STATE_FALLING_RIGHT,
        STATE_FALLING_LEFT,
        STATE_DASHING_RIGHT,
        STATE_DASHING_LEFT,
        STATE_CROUCHING_RIGHT,
        STATE_CROUCHING_LEFT,
        STATE_SLIDEING_RIGHT,
        STATE_SLIDEING_LEFT,
        STATE_ATTACKING1_RIGHT,
        STATE_ATTACKING1_LEFT,
        STATE_ATTACKING2_RIGHT,
        STATE_ATTACKING2_LEFT,
        STATE_DASHATTACKING_RIGHT,
        STATE_DASHATTACKING_LEFT,
        STATE_HURTING_RIGHT,
        STATE_HURTING_LEFT,
        STATE_DEAD_LEFT,
        STATE_DEAD_RIGHT
    };
    //The starting state
    private State state = State.STATE_IDLE_RIGHT;

    //We built a state machine to send the correct variables to the animator and to play the correct sounds when the state changes.
    public override void Update(Skeleton gameObject)
    {
        switch (state)
        {
            case State.STATE_IDLE_RIGHT:
                if (!gameObject.waiting)
                {
                    if (gameObject.dead)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.deathClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.GetComponent<Animator>().SetBool("isDead", true);
                        state = State.STATE_DEAD_RIGHT;
                    }
                    else if (gameObject.takeDamage)
                    {
                        gameObject.GetComponent<Animator>().SetBool("takeDamage", true);
                        state = State.STATE_HURTING_RIGHT;
                    }
                    else if (gameObject.dashing)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.dashClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.GetComponent<Animator>().SetBool("isDashing", true);
                        state = State.STATE_DASHING_RIGHT;
                    }
                    else if (gameObject.crouch && !gameObject.onAir)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isCrouching", true);
                        gameObject.Crouching();
                        state = State.STATE_CROUCHING_RIGHT;
                    }
                    else if (gameObject.storedAttack)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isAttacking", true);
                        gameObject.GetComponent<AudioSource>().clip = gameObject.attackClip;
                        gameObject.GetComponent<AudioSource>().Play();
                        gameObject.StartAttacking();
                        state = State.STATE_ATTACKING1_RIGHT;
                    }
                    else if (gameObject.falling)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isFalling", true);
                        gameObject.GetComponent<Animator>().SetBool("isJumping", false);
                        state = State.STATE_FALLING_RIGHT;
                    }
                    else if (gameObject.jump)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.jumpClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.GetComponent<Animator>().SetBool("isJumping", true);
                        state = State.STATE_JUMPING_RIGHT;
                    }
                    else if (gameObject.speed < -0.1f)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isMoving", true);
                        gameObject.GetComponent<Animator>().SetBool("lookingRight", false);
                        gameObject.lookingRight = -1;
                        state = State.STATE_RUNNING_LEFT;
                    }
                    else if (gameObject.speed > 0.1f)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isMoving", true);
                        state = State.STATE_RUNNING_RIGHT;
                    }
                }                
                break;
            case State.STATE_IDLE_LEFT:
                if (!gameObject.waiting)
                {
                    if (gameObject.dead)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.deathClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.GetComponent<Animator>().SetBool("isDead", true);
                        state = State.STATE_DEAD_LEFT;
                    }
                    else if (gameObject.takeDamage)
                    {
                        gameObject.GetComponent<Animator>().SetBool("takeDamage", true);
                        state = State.STATE_HURTING_LEFT;
                    }
                    else if (gameObject.dashing)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.dashClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.GetComponent<Animator>().SetBool("isDashing", true);
                        state = State.STATE_DASHING_LEFT;
                    }
                    else if (gameObject.crouch && !gameObject.onAir)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isCrouching", true);
                        gameObject.Crouching();
                        state = State.STATE_CROUCHING_LEFT;
                    }
                    else if (gameObject.storedAttack)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isAttacking", true);
                        gameObject.GetComponent<AudioSource>().clip = gameObject.attackClip;
                        gameObject.GetComponent<AudioSource>().Play();
                        gameObject.StartAttacking();
                        state = State.STATE_ATTACKING1_LEFT;
                    }
                    else if (gameObject.falling)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isFalling", true);
                        gameObject.GetComponent<Animator>().SetBool("isJumping", false);
                        state = State.STATE_FALLING_LEFT;
                    }
                    else if (gameObject.jump)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.jumpClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.GetComponent<Animator>().SetBool("isJumping", true);
                        state = State.STATE_JUMPING_LEFT;
                    }
                    else if (gameObject.speed < -0.1f)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isMoving", true);
                        state = State.STATE_RUNNING_LEFT;
                    }
                    else if (gameObject.speed > 0.1f)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isMoving", true);
                        gameObject.GetComponent<Animator>().SetBool("lookingRight", true);
                        gameObject.lookingRight = 1;
                        state = State.STATE_RUNNING_RIGHT;
                    }
                }                    
                break;
            case State.STATE_RUNNING_RIGHT:
                if (!gameObject.waiting)
                {
                    if (gameObject.dead)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.deathClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.GetComponent<Animator>().SetBool("isDead", true);
                        state = State.STATE_DEAD_RIGHT;
                    }
                    else if (gameObject.takeDamage)
                    {
                        gameObject.GetComponent<Animator>().SetBool("takeDamage", true);
                        gameObject.GetComponent<Animator>().SetBool("isMoving", false);
                        gameObject.Crouching();
                        state = State.STATE_HURTING_RIGHT;
                    }
                    else if (gameObject.dashing)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.dashClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.GetComponent<Animator>().SetBool("isDashing", true);
                        state = State.STATE_DASHING_RIGHT;
                    }
                    else if (gameObject.crouch)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isCrouching", true);
                        gameObject.Crouching();
                        gameObject.GetComponent<Animator>().SetBool("isMoving", false);
                        state = State.STATE_CROUCHING_RIGHT;
                    }
                    else if (gameObject.storedAttack)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isAttacking", true);
                        gameObject.GetComponent<Animator>().SetBool("isMoving", false);
                        gameObject.GetComponent<AudioSource>().clip = gameObject.attackClip;
                        gameObject.GetComponent<AudioSource>().Play();
                        gameObject.StartAttacking();
                        state = State.STATE_ATTACKING1_RIGHT;
                    }
                    else if (gameObject.jump)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.jumpClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.GetComponent<Animator>().SetBool("isJumping", true);
                        state = State.STATE_JUMPING_RIGHT;
                    }
                    else if (gameObject.falling)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isFalling", true);
                        gameObject.GetComponent<Animator>().SetBool("isJumping", false);
                        state = State.STATE_FALLING_RIGHT;
                    }
                    else if (gameObject.speed < -0.1f)
                    {
                        gameObject.GetComponent<Animator>().SetBool("lookingRight", false);
                        gameObject.lookingRight = -1;
                        state = State.STATE_RUNNING_LEFT;
                    }
                    else if (gameObject.speed == 0.0f)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isMoving", false);
                        state = State.STATE_IDLE_RIGHT;
                    }
                }                    
                break;
            case State.STATE_RUNNING_LEFT:
                if (!gameObject.waiting)
                {
                    if (gameObject.dead)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.deathClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.GetComponent<Animator>().SetBool("isDead", true);
                        state = State.STATE_DEAD_LEFT;
                    }
                    else if (gameObject.takeDamage)
                    {
                        gameObject.GetComponent<Animator>().SetBool("takeDamage", true);
                        gameObject.GetComponent<Animator>().SetBool("isMoving", false);
                        state = State.STATE_HURTING_LEFT;
                    }
                    else if (gameObject.dashing)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.dashClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.GetComponent<Animator>().SetBool("isDashing", true);
                        state = State.STATE_DASHING_LEFT;
                    }
                    else if (gameObject.crouch)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isCrouching", true);
                        gameObject.Crouching();
                        gameObject.GetComponent<Animator>().SetBool("isMoving", false);
                        gameObject.Crouching();
                        state = State.STATE_CROUCHING_LEFT;
                    }
                    else if (gameObject.storedAttack)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isAttacking", true);
                        gameObject.GetComponent<Animator>().SetBool("isMoving", false);
                        gameObject.GetComponent<AudioSource>().clip = gameObject.attackClip;
                        gameObject.GetComponent<AudioSource>().Play();
                        gameObject.StartAttacking();
                        state = State.STATE_ATTACKING1_LEFT;
                    }
                    else if (gameObject.jump)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.jumpClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.GetComponent<Animator>().SetBool("isJumping", true);
                        state = State.STATE_JUMPING_LEFT;
                    }
                    else if (gameObject.falling)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isFalling", true);
                        gameObject.GetComponent<Animator>().SetBool("isJumping", false);
                        state = State.STATE_FALLING_LEFT;
                    }
                    else if (gameObject.speed > 0.1f)
                    {
                        gameObject.GetComponent<Animator>().SetBool("lookingRight", true);
                        gameObject.lookingRight = 1;
                        state = State.STATE_RUNNING_RIGHT;
                    }
                    else if (gameObject.speed == 0.0f)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isMoving", false);
                        state = State.STATE_IDLE_LEFT;
                    }
                }                    
                break;
            case State.STATE_CROUCHING_RIGHT:
                if (!gameObject.waiting)
                {
                    if (gameObject.dead)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.deathClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.GetComponent<Animator>().SetBool("isDead", true);
                        state = State.STATE_DEAD_RIGHT;
                    }
                    else if (gameObject.takeDamage)
                    {
                        gameObject.GetComponent<Animator>().SetBool("takeDamage", true);
                        gameObject.GetComponent<Animator>().SetBool("isCrouching", false);
                        gameObject.EndCrouching();
                        state = State.STATE_HURTING_RIGHT;
                    }
                    else if (!gameObject.crouch && gameObject.canGetUp)
                    {
                        gameObject.EndCrouching();
                        gameObject.GetComponent<Animator>().SetBool("isCrouching", false);
                        state = State.STATE_IDLE_RIGHT;
                    }
                    else if (gameObject.slideing)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.dashClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.GetComponent<Animator>().SetBool("isSlideing", true);
                        gameObject.GetComponent<Animator>().SetBool("isCrouching", false);
                        state = State.STATE_SLIDEING_RIGHT;
                    }
                    else if (gameObject.speed < -0.1f)
                    {
                        gameObject.GetComponent<Animator>().SetBool("lookingRight", false);
                        gameObject.lookingRight = -1;
                        state = State.STATE_CROUCHING_LEFT;
                    }
                }                    
                break;
            case State.STATE_CROUCHING_LEFT:
                if (!gameObject.waiting)
                {
                    if (gameObject.dead)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.deathClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.GetComponent<Animator>().SetBool("isDead", true);
                        state = State.STATE_DEAD_LEFT;
                    }
                    else if (gameObject.takeDamage)
                    {
                        gameObject.GetComponent<Animator>().SetBool("takeDamage", true);
                        gameObject.GetComponent<Animator>().SetBool("isCrouching", false);
                        gameObject.EndCrouching();
                        state = State.STATE_HURTING_LEFT;
                    }
                    else if (!gameObject.crouch && gameObject.canGetUp)
                    {
                        gameObject.EndCrouching();
                        gameObject.GetComponent<Animator>().SetBool("isCrouching", false);
                        state = State.STATE_IDLE_LEFT;
                    }
                    else if (gameObject.slideing)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.dashClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.GetComponent<Animator>().SetBool("isSlideing", true);
                        gameObject.GetComponent<Animator>().SetBool("isCrouching", false);
                        state = State.STATE_SLIDEING_LEFT;
                    }
                    else if (gameObject.speed > 0.1f)
                    {
                        gameObject.GetComponent<Animator>().SetBool("lookingRight", true);
                        gameObject.lookingRight = 1;
                        state = State.STATE_CROUCHING_RIGHT;
                    }
                }                    
                break;
            case State.STATE_SLIDEING_RIGHT:
                if (!gameObject.waiting)
                {
                    if (gameObject.dead)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.deathClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.GetComponent<Animator>().SetBool("isDead", true);
                        state = State.STATE_DEAD_RIGHT;
                    }
                    else if (gameObject.takeDamage)
                    {
                        gameObject.GetComponent<Animator>().SetBool("takeDamage", true);
                        gameObject.GetComponent<Animator>().SetBool("isSlideing", false);
                        state = State.STATE_HURTING_RIGHT;
                    }
                    else if (!gameObject.slideing)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isSlideing", false);
                        if (gameObject.crouch || !gameObject.canGetUp)
                        {
                            gameObject.GetComponent<Animator>().SetBool("isCrouching", true);
                            gameObject.Crouching();
                            state = State.STATE_CROUCHING_RIGHT;
                        }
                        else
                        {
                            gameObject.EndCrouching();
                            state = State.STATE_IDLE_RIGHT;
                        }
                    }
                }                    
                break;
            case State.STATE_SLIDEING_LEFT:
                if (!gameObject.waiting)
                {
                    if (gameObject.dead)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.deathClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.GetComponent<Animator>().SetBool("isDead", true);
                        state = State.STATE_DEAD_LEFT;
                    }
                    else if (gameObject.takeDamage)
                    {
                        gameObject.GetComponent<Animator>().SetBool("takeDamage", true);
                        gameObject.GetComponent<Animator>().SetBool("isSlideing", false);
                        state = State.STATE_HURTING_LEFT;
                    }
                    else if (!gameObject.slideing)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isSlideing", false);
                        if (gameObject.crouch || !gameObject.canGetUp)
                        {
                            gameObject.GetComponent<Animator>().SetBool("isCrouching", true);
                            gameObject.Crouching();
                            state = State.STATE_CROUCHING_LEFT;
                        }
                        else
                        {
                            gameObject.EndCrouching();
                            state = State.STATE_IDLE_LEFT;
                        }
                    }
                }                    
                break;
            case State.STATE_JUMPING_RIGHT:
                if (!gameObject.waiting)
                {
                    if (gameObject.dead)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.deathClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.GetComponent<Animator>().SetBool("isDead", true);
                        state = State.STATE_DEAD_RIGHT;
                    }
                    else if (gameObject.takeDamage)
                    {
                        gameObject.GetComponent<Animator>().SetBool("takeDamage", true);
                        state = State.STATE_HURTING_RIGHT;
                    }
                    else if (gameObject.storedAttack)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isAttacking", true);
                        gameObject.GetComponent<Animator>().SetBool("isJumping", false);
                        gameObject.GetComponent<Animator>().SetBool("isMoving", false);
                        gameObject.GetComponent<AudioSource>().clip = gameObject.attackClip;
                        gameObject.GetComponent<AudioSource>().Play();
                        gameObject.StartAttacking();
                        state = State.STATE_ATTACKING1_RIGHT;
                    }
                    else if (gameObject.dashing)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.dashClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.falling = true;
                        gameObject.GetComponent<Animator>().SetBool("isDashing", true);
                        gameObject.GetComponent<Animator>().SetBool("isJumping", false);
                        state = State.STATE_DASHING_RIGHT;
                    }
                    else if (gameObject.falling)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isFalling", true);
                        gameObject.GetComponent<Animator>().SetBool("isJumping", false);
                        state = State.STATE_FALLING_RIGHT;
                    }
                    else if (!gameObject.onAir)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isJumping", false);
                        state = State.STATE_IDLE_RIGHT;
                    }
                    else if (gameObject.speed < -0.1f)
                    {
                        gameObject.GetComponent<Animator>().SetBool("lookingRight", false);
                        gameObject.lookingRight = -1;
                        state = State.STATE_JUMPING_LEFT;
                    }
                }                    
                break;
            case State.STATE_JUMPING_LEFT:
                if (!gameObject.waiting)
                {
                    if (gameObject.dead)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.deathClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.GetComponent<Animator>().SetBool("isDead", true);
                        state = State.STATE_DEAD_LEFT;
                    }
                    else if (gameObject.takeDamage)
                    {
                        gameObject.GetComponent<Animator>().SetBool("takeDamage", true);
                        state = State.STATE_HURTING_LEFT;
                    }
                    else if (gameObject.storedAttack)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isAttacking", true);
                        gameObject.GetComponent<Animator>().SetBool("isJumping", false);
                        gameObject.GetComponent<Animator>().SetBool("isMoving", false);
                        gameObject.GetComponent<AudioSource>().clip = gameObject.attackClip;
                        gameObject.GetComponent<AudioSource>().Play();
                        gameObject.StartAttacking();
                        state = State.STATE_ATTACKING1_LEFT;
                    }
                    else if (gameObject.dashing)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.dashClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.falling = true;
                        gameObject.GetComponent<Animator>().SetBool("isDashing", true);
                        gameObject.GetComponent<Animator>().SetBool("isJumping", false);
                        state = State.STATE_DASHING_LEFT;
                    }
                    else if (gameObject.falling)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isFalling", true);
                        gameObject.GetComponent<Animator>().SetBool("isJumping", false);
                        state = State.STATE_FALLING_LEFT;
                    }
                    else if (!gameObject.onAir)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isJumping", false);
                        state = State.STATE_IDLE_LEFT;
                    }
                    else if (gameObject.speed > 0.1f)
                    {
                        gameObject.GetComponent<Animator>().SetBool("lookingRight", true);
                        gameObject.lookingRight = 1;
                        state = State.STATE_JUMPING_RIGHT;
                    }
                }                    
                break;
            case State.STATE_FALLING_RIGHT:
                if (!gameObject.waiting)
                {
                    if (gameObject.dead)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.deathClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.GetComponent<Animator>().SetBool("isDead", true);
                        state = State.STATE_DEAD_RIGHT;
                    }
                    else if (gameObject.takeDamage)
                    {
                        gameObject.GetComponent<Animator>().SetBool("takeDamage", true);
                        state = State.STATE_HURTING_RIGHT;
                    }
                    else if (gameObject.storedAttack)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isAttacking", true);
                        gameObject.GetComponent<Animator>().SetBool("isFalling", false);
                        gameObject.GetComponent<Animator>().SetBool("isMoving", false);
                        gameObject.GetComponent<AudioSource>().clip = gameObject.attackClip;
                        gameObject.GetComponent<AudioSource>().Play();
                        gameObject.StartAttacking();
                        state = State.STATE_ATTACKING1_RIGHT;
                    }
                    else if (gameObject.dashing)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.dashClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.GetComponent<Animator>().SetBool("isDashing", true);
                        state = State.STATE_DASHING_RIGHT;
                    }
                    else if (!gameObject.falling || !gameObject.onAir)
                    {
                        gameObject.falling = false;
                        gameObject.GetComponent<Animator>().SetBool("isFalling", false);
                        gameObject.GetComponent<Animator>().SetBool("isMoving", false);
                        state = State.STATE_IDLE_RIGHT;
                    }
                    else if (gameObject.speed < -0.1f)
                    {
                        gameObject.GetComponent<Animator>().SetBool("lookingRight", false);
                        gameObject.lookingRight = -1;
                        state = State.STATE_FALLING_LEFT;
                    }
                }                    
                break;
            case State.STATE_FALLING_LEFT:
                if (!gameObject.waiting)
                {
                    if (gameObject.dead)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.deathClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.GetComponent<Animator>().SetBool("isDead", true);
                        state = State.STATE_DEAD_LEFT;
                    }
                    else if (gameObject.takeDamage)
                    {
                        gameObject.GetComponent<Animator>().SetBool("takeDamage", true);
                        state = State.STATE_HURTING_LEFT;
                    }
                    else if (gameObject.storedAttack)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isAttacking", true);
                        gameObject.GetComponent<Animator>().SetBool("isFalling", false);
                        gameObject.GetComponent<Animator>().SetBool("isMoving", false);
                        gameObject.GetComponent<AudioSource>().clip = gameObject.attackClip;
                        gameObject.GetComponent<AudioSource>().Play();
                        gameObject.StartAttacking();
                        state = State.STATE_ATTACKING1_LEFT;
                    }
                    else if (gameObject.dashing)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.dashClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.GetComponent<Animator>().SetBool("isDashing", true);
                        state = State.STATE_DASHING_LEFT;
                    }
                    else if (!gameObject.falling || !gameObject.onAir)
                    {
                        gameObject.falling = false;
                        gameObject.GetComponent<Animator>().SetBool("isFalling", false);
                        gameObject.GetComponent<Animator>().SetBool("isMoving", false);
                        state = State.STATE_IDLE_LEFT;
                    }
                    else if (gameObject.speed > 0.1f)
                    {
                        gameObject.GetComponent<Animator>().SetBool("lookingRight", true);
                        gameObject.lookingRight = 1;
                        state = State.STATE_FALLING_RIGHT;
                    }
                }                    
                break;
            case State.STATE_DASHING_RIGHT:
                if (!gameObject.waiting)
                {
                    if (gameObject.dead)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.deathClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.GetComponent<Animator>().SetBool("isDead", true);
                        state = State.STATE_DEAD_RIGHT;
                    }
                    else if (gameObject.takeDamage)
                    {
                        gameObject.GetComponent<Animator>().SetBool("takeDamage", true);
                        gameObject.GetComponent<Animator>().SetBool("isDashing", false);
                        state = State.STATE_HURTING_RIGHT;
                    }
                    else if (!gameObject.dashing)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isDashing", false);
                        gameObject.GetComponent<Animator>().SetBool("isMoving", false);
                        gameObject.GetComponent<Animator>().SetBool("isFalling", false); 
                        state = State.STATE_IDLE_RIGHT;
                    }
                    else if (gameObject.storedAttack)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isDashing", false);
                        gameObject.GetComponent<Animator>().SetBool("isDashAttacking", true);
                        gameObject.GetComponent<AudioSource>().clip = gameObject.attackClip;
                        gameObject.GetComponent<AudioSource>().Play();
                        gameObject.StartAttacking();
                        state = State.STATE_DASHATTACKING_RIGHT;
                    }
                }                    
                break;
            case State.STATE_DASHING_LEFT:
                if (!gameObject.waiting)
                {
                    if (gameObject.dead)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.deathClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.GetComponent<Animator>().SetBool("isDead", true);
                        state = State.STATE_DEAD_LEFT;
                    }
                    else if (gameObject.takeDamage)
                    {
                        gameObject.GetComponent<Animator>().SetBool("takeDamage", true);
                        gameObject.GetComponent<Animator>().SetBool("isDashing", false);
                        state = State.STATE_HURTING_LEFT;
                    }
                    else if (!gameObject.dashing)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isDashing", false);
                        gameObject.GetComponent<Animator>().SetBool("isMoving", false);
                        gameObject.GetComponent<Animator>().SetBool("isFalling", false); 
                        state = State.STATE_IDLE_LEFT;
                    }
                    else if (gameObject.storedAttack)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isDashing", false);
                        gameObject.GetComponent<Animator>().SetBool("isDashAttacking", true);
                        gameObject.GetComponent<AudioSource>().clip = gameObject.attackClip;
                        gameObject.GetComponent<AudioSource>().Play();
                        gameObject.StartAttacking();
                        state = State.STATE_DASHATTACKING_LEFT;
                    }
                }                    
                break;
            case State.STATE_DASHATTACKING_RIGHT:
                if (!gameObject.waiting)
                {
                    if (gameObject.dead)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.deathClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.GetComponent<Animator>().SetBool("isDead", true);
                        state = State.STATE_DEAD_RIGHT;
                    }
                    else if (gameObject.takeDamage)
                    {
                        gameObject.GetComponent<Animator>().SetBool("takeDamage", true);
                        gameObject.GetComponent<Animator>().SetBool("isDashAttacking", false);
                        state = State.STATE_HURTING_RIGHT;
                    }
                    else if (!gameObject.attacking)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isDashAttacking", false);
                        gameObject.GetComponent<Animator>().SetBool("isMoving", false);
                        gameObject.GetComponent<Animator>().SetBool("isFalling", false); 
                        state = State.STATE_IDLE_RIGHT;
                    }
                }                    
                break;
            case State.STATE_DASHATTACKING_LEFT:
                if (!gameObject.waiting)
                {
                    if (gameObject.dead)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.deathClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.GetComponent<Animator>().SetBool("isDead", true);
                        state = State.STATE_DEAD_LEFT;
                    }
                    else if (gameObject.takeDamage)
                    {
                        gameObject.GetComponent<Animator>().SetBool("takeDamage", true);
                        gameObject.GetComponent<Animator>().SetBool("isDashAttacking", false);
                        state = State.STATE_HURTING_LEFT;
                    }
                    else if (!gameObject.attacking)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isDashAttacking", false);
                        gameObject.GetComponent<Animator>().SetBool("isMoving", false); 
                        gameObject.GetComponent<Animator>().SetBool("isFalling", false); 
                        state = State.STATE_IDLE_LEFT;
                    }
                }                    
                break;
            case State.STATE_ATTACKING1_RIGHT:
                if (!gameObject.waiting)
                {
                    if (gameObject.dead)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.deathClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.GetComponent<Animator>().SetBool("isDead", true);
                        state = State.STATE_DEAD_RIGHT;
                    }
                    else if (gameObject.takeDamage)
                    {
                        gameObject.GetComponent<Animator>().SetBool("takeDamage", true);
                        gameObject.GetComponent<Animator>().SetBool("isAttacking", false);
                        state = State.STATE_HURTING_RIGHT;
                    }
                    else if (!gameObject.attacking)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isAttacking", false);
                        if (gameObject.storedAttack)
                        {
                            gameObject.GetComponent<Animator>().SetBool("is2Attacking", true);
                            gameObject.GetComponent<AudioSource>().clip = gameObject.attackClip;
                            gameObject.GetComponent<AudioSource>().Play();
                            gameObject.StartAttacking();
                            state = State.STATE_ATTACKING2_RIGHT;
                        }
                        else state = State.STATE_IDLE_RIGHT;
                    }
                }                    
                break;
            case State.STATE_ATTACKING1_LEFT:
                if (!gameObject.waiting)
                {
                    if (gameObject.dead)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.deathClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.GetComponent<Animator>().SetBool("isDead", true);
                        state = State.STATE_DEAD_LEFT;
                    }
                    else if (gameObject.takeDamage)
                    {
                        gameObject.GetComponent<Animator>().SetBool("takeDamage", true);
                        gameObject.GetComponent<Animator>().SetBool("isAttacking", false);
                        state = State.STATE_HURTING_LEFT;
                    }
                    else if (!gameObject.attacking)
                    {
                        gameObject.GetComponent<Animator>().SetBool("isAttacking", false);
                        if (gameObject.storedAttack)
                        {
                            gameObject.GetComponent<Animator>().SetBool("is2Attacking", true);
                            gameObject.GetComponent<AudioSource>().clip = gameObject.attackClip;
                            gameObject.GetComponent<AudioSource>().Play();
                            gameObject.StartAttacking();
                            state = State.STATE_ATTACKING2_LEFT;
                        }
                        else state = State.STATE_IDLE_LEFT;
                    }
                }                    
                break;
            case State.STATE_ATTACKING2_RIGHT:
                if (!gameObject.waiting)
                {
                    if (gameObject.dead)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.deathClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.GetComponent<Animator>().SetBool("isDead", true);
                        state = State.STATE_DEAD_RIGHT;
                    }
                    else if (gameObject.takeDamage)
                    {
                        gameObject.GetComponent<Animator>().SetBool("takeDamage", true);
                        gameObject.GetComponent<Animator>().SetBool("is2Attacking", false);
                        state = State.STATE_HURTING_RIGHT;
                    }
                    else if (!gameObject.attacking)
                    {
                        gameObject.GetComponent<Animator>().SetBool("is2Attacking", false);
                        state = State.STATE_IDLE_RIGHT;
                    }
                }                    
                break;
            case State.STATE_ATTACKING2_LEFT:
                if (!gameObject.waiting)
                {
                    if (gameObject.dead)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.deathClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.GetComponent<Animator>().SetBool("isDead", true);
                        state = State.STATE_DEAD_LEFT;
                    }
                    else if (gameObject.takeDamage)
                    {
                        gameObject.GetComponent<Animator>().SetBool("takeDamage", true);
                        gameObject.GetComponent<Animator>().SetBool("is2Attacking", false);
                        state = State.STATE_HURTING_LEFT;
                    }
                    else if (!gameObject.attacking)
                    {
                        gameObject.GetComponent<Animator>().SetBool("is2Attacking", false);
                        state = State.STATE_IDLE_LEFT;
                    }
                }                    
                break;
            case State.STATE_HURTING_RIGHT:
                if (!gameObject.waiting)
                {
                    if (gameObject.dead)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.deathClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.GetComponent<Animator>().SetBool("isDead", true);
                        state = State.STATE_DEAD_RIGHT;
                    }
                    else if (!gameObject.takeDamage)
                    {
                        gameObject.GetComponent<Animator>().SetBool("takeDamage", false);
                        state = State.STATE_IDLE_RIGHT;
                    }
                }                    
                break;
            case State.STATE_HURTING_LEFT:
                if (!gameObject.waiting)
                {
                    if (gameObject.dead)
                    {
                        if (gameObject.isPlayer)
                        {
                            gameObject.GetComponent<AudioSource>().clip = gameObject.deathClip;
                            gameObject.GetComponent<AudioSource>().Play();
                        }
                        gameObject.GetComponent<Animator>().SetBool("isDead", true);
                        state = State.STATE_DEAD_LEFT;
                    }
                    else if (!gameObject.takeDamage)
                    {
                        gameObject.GetComponent<Animator>().SetBool("takeDamage", false);
                        state = State.STATE_IDLE_LEFT;
                    }
                }                    
                break;
        }        
    }
}
