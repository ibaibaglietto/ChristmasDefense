using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//We will use the component pattern to create a skeleton that both the player and the follower can use
public class Skeleton : MonoBehaviour
{
    //The components
    private InputComponent input;
    private PhysicsComponent physics;
    private GraphicsComponent graphics;
    public InputHandler inputHandler;
    private GameController gameController;
    //The public variables that the components will need
    public bool isPlayer;
    public bool jump = false;
    public float speed = 0.0f;
    public bool falling = false;
    public bool onAir = false;
    public bool check = true;
    public bool dash = false;
    public bool dashing = false;
    public bool slideing = false;
    public int lookingRight = 1;
    public bool dashShadow = false;
    public bool takeDamage = false;
    public bool takingDamage = false;
    public bool dead = false;
    public bool crouch = false;
    public bool crouching = false;
    public bool canGetUp = true;
    public bool gravity = false;
    public bool attack = false;
    public bool attacking = false;
    public bool storedAttack = false;
    public bool waiting;
    public int monsterPos;
    public int monsterHealth;
    public AudioClip jumpClip;
    public AudioClip dashClip;
    public AudioClip deathClip;
    public AudioClip attackClip;
    //We will initialize the components looking if it is the player and the delay we need to apply
    void Start()
    {
        if (isPlayer)
        {
            input = new PlayerInputComponent();
            physics = new NormalPhysicsComponent();
            graphics = new MariDomingiGraphicsComponent();
        }
        else
        {
            input = new MonsterInputComponent();
            physics = new MonsterPhysicsComponent();
            graphics = new MonsterGraphicsComponent();
            input.Create(monsterPos, this);
            gameController = GameObject.Find("GameController").GetComponent<GameController>();
        }
    }

    // In update and fixed update we update the components of our skeleton: input, graphics and physics
    void Update()
    {
        input.Update(this);
        graphics.Update(this);
    }
    private void FixedUpdate()
    {
        physics.FixedUpdate(this);
    }

    //A function to manage the waiting state of the skeleton, for example for the initial countdown
    public void SetWait(bool w)
    {
        waiting = w;
    }

    //Functions to change the variables of the movement of the skeleton
    public void Jump()
    {
        jump = true;
    }

    public void StopJump()
    {
        jump = false;
        gravity = true;
    }

    public void MoveRight()
    {
        speed += 1.0f;
    }
    public void StopMoveRight()
    {
        speed -= 1.0f;
    }

    public void MoveLeft()
    {
        speed -= 1.0f;
    }
    public void StopMoveLeft()
    {
        speed += 1.0f;
    }

    public void Dash()
    {
        dash = true;
    }
    public void EndDash()
    {
        dash = false;
    }

    public void Crouch()
    {
        crouch = true;
    }
    public void EndCrouch()
    {
        crouch = false;
    }

    public void StartAttack()
    {
        if (!waiting)
        {
            attack = true;
            storedAttack = true;
        }
    }
    public void EndAttack()
    {
        attack = false;
    }

    //Functions to set when the player is actually crouching
    public void Crouching()
    {
        crouching = true;
    }
    public void EndCrouching()
    {
        crouching = false;
    }
    //Functions to set when the player ends dashing
    public void StopDashing()
    {
        dashing = false;
    }
    //Functions to deal damage to the monsters and to end the damage.
    public void StartDamage(int d)
    {
        if (!isPlayer)
        {
            monsterHealth -= d;
            if (monsterHealth <= 0) dead = true;
        }
        takeDamage = true;
    }
    public void EndDamage()
    {
        takeDamage = false;
    }

    //Functions to start and end the taking damage state.
    public void StartTakingDamage()
    {
        takingDamage = true;
    }

    public void EndTakingDamage()
    {
        takingDamage = false;
    }
    

    //Functions to set when the skeleton is actually attacking
    public void StartAttacking()
    {
        attacking = true;
        storedAttack = false;
    }
    public void EndAttacking()
    {
        attacking = false;
    }

    //When the player alt tabs we reset all the commands.
    private void OnApplicationFocus(bool focus)
    {
        if(isPlayer && !focus)
        {
            EndAttack();
            EndCrouch();
            EndDash();
            StopJump();
            speed = 0.0f;
        }
    }
    //Function to deal damage to the chests.
    public void DealDamage(int d)
    {
        gameObject.GetComponent<AudioSource>().clip = attackClip;
        gameObject.GetComponent<AudioSource>().Play();
        gameController.DealDamage(d);
    }
    //Function to set the canGetUp boolean
    public void SetCanGetUp(bool g)
    {
        canGetUp = g;
    }
    //Function to kill the skeleton
    public void Die()
    {
        dead = true;
    }

    //Function to destroy an enemy
    public void SelfDestroy()
    {
        Destroy(gameObject);
    }

}
