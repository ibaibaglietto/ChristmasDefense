using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//We will use the component pattern to create a skeleton that both the player and the follower can use
public class Skeleton : MonoBehaviour
{
    [SerializeField] private float delay;
    private InputComponent input;
    private PhysicsComponent physics;
    private GraphicsComponent graphics;
    public InputHandler inputHandler;
    //The prefab of the dash and the gameobject where we are going to save it
    [SerializeField] private GameObject dashPrefab;
    private GameObject shadow;
    //The public variables that the components will need
    public bool isPlayer;
    public bool jump = false;
    public float speed = 0.0f;
    public bool falling = false;
    public bool onAir = false;
    public bool check = true;
    public bool dash = false;
    public bool dashing = false;
    public int lookingRight = 1;
    public bool dashShadow = false;
    public bool takeDamage = false;
    public bool dead = false;
    public bool crouch = false;
    public bool crouching = false;
    public bool canGetUp = true;
    public bool gravity = false;
    public bool attack = false;
    public bool attacking = false;
    public bool storedAttack = false;
    public bool waiting;
    public AudioClip jumpClip;
    public AudioClip dashClip;
    public AudioClip deathClip;
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
        input.FixedUpdate(this);
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
    public void Crouching()
    {
        crouching = true;
    }
    public void EndCrouching()
    {
        crouching = false;
    }
    public void StopDashing()
    {
        dashing = false;
    }

    public void StartDamage()
    {
        takeDamage = true;
    }

    public void EndDamage()
    {
        takeDamage = false;
    }
    public void StartAttack()
    {
        attack = true;
        storedAttack = true;
    }

    public void EndAttack()
    {
        attack = false;
    }

    public void StartAttacking()
    {
        attacking = true;
        storedAttack = false;
    }

    public void EndAttacking()
    {
        attacking = false;
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


    //A function to create shadows when a skeleton dashes
    public void CreateDashShadow()
    {
        shadow = Instantiate(dashPrefab, transform.position, transform.rotation);
        if (lookingRight != 1)
        {
            Vector3 theScale = shadow.transform.localScale;
            theScale.x *= -1;
            shadow.transform.localScale = theScale;
        }
        else
        {
            Vector3 theScale = shadow.transform.localScale;
            theScale.x *= 1;
            shadow.transform.localScale = theScale;
        }
    }
}
