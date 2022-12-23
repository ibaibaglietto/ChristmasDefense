using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPhysicsComponent : PhysicsComponent
{

    //A mask determining what is ground to the character
    private LayerMask m_WhatIsGround = LayerMask.GetMask("Ground");
    //Radius of the overlap circle to determine if grounded
    const float k_GroundedRadius = .45f;
    //Radius of the overlap circle to determine if it can get up
    const float k_GetUpRadius = .25f;
    //Whether or not the player is grounded.
    public bool m_Grounded;
    //The last time the skeleton dashed
    private float lastDash = Time.fixedTime - 0.5f;
    //A boolean to know if the skeleton can dash
    private bool canDash = true;
    //The velocity
    private float velX = 0.0f;
    private float velY = 0.0f;

    //We check the physics 50 times every second to make them more consistent. We will check the movement variables that are stored on the skeleton and move it depending on them.
    public override void FixedUpdate(Skeleton gameObject)
    {
        if (!gameObject.dead && !gameObject.waiting)
        {
            //We initialize the velocity
            velX = gameObject.speed * 10f;
            velY = gameObject.GetComponent<Rigidbody2D>().velocity.y;
            //We save when the skeleton is in the ground
            Collider2D[] colliders = Physics2D.OverlapCircleAll(gameObject.transform.GetChild(0).position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject && Mathf.Abs(gameObject.GetComponent<Rigidbody2D>().velocity.y) < 0.01f)
                {
                    m_Grounded = true;
                    gameObject.gravity = false;
                    gameObject.falling = false;
                    gameObject.onAir = false;
                    canDash = true;
                }
            }
            //We save when the skeleton can get up
            gameObject.SetCanGetUp(true);
            colliders = Physics2D.OverlapCircleAll(gameObject.transform.GetChild(1).position, k_GetUpRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    gameObject.SetCanGetUp(false);
                }
            }
            //We change the gravity to make the skeleton fall faster when the player isn't pressing the jump button
            if (gameObject.gravity || gameObject.slideing) gameObject.GetComponent<Rigidbody2D>().gravityScale = 5;
            else gameObject.GetComponent<Rigidbody2D>().gravityScale = 2;
            //We only jump if the skeleton is in the ground and it isn't crouching nor attacking
            if (gameObject.jump && m_Grounded && !gameObject.crouching && !gameObject.attacking && !gameObject.slideing)
            {
                m_Grounded = false;
                velY = 16.0f;
                gameObject.onAir = true;
            }
            //We put the skeleton in falling state if the y velocity is negative.
            if (gameObject.GetComponent<Rigidbody2D>().velocity.y < -0.1f)
            {
                gameObject.falling = true;
                m_Grounded = false;
                gameObject.onAir = true;
            }
            //When the skeleton is attacking or crouching we don't move it in x
            if ((gameObject.attacking && !gameObject.onAir) || gameObject.crouching) velX = 0.0f;
            //We save the velocity that we calculated
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(velX, velY);
            //We only dash if the time has passed and the skeleton hasn't dashed this jump
            if (gameObject.dash && !gameObject.crouching && !gameObject.slideing && !gameObject.attacking && canDash && (Time.fixedTime - lastDash) > 0.5f)
            {
                gameObject.dashing = true;
                lastDash = Time.fixedTime;
                canDash = false;
            }
            //When the skeleton is dashing we mantain a constant velocity
            if (gameObject.dashing)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.lookingRight * 20.0f, 0.0f);
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
                gameObject.onAir = true;
                if (Time.fixedTime - lastDash > 0.4f)
                {
                    gameObject.dashing = false;
                    gameObject.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
                }
            }
            //We let the player slide if they are crouching and dash
            if(gameObject.dash && gameObject.crouching && !gameObject.attacking && canDash && (Time.fixedTime - lastDash) > 0.5f)
            {
                gameObject.EndCrouching();
                gameObject.slideing = true;
                lastDash = Time.fixedTime;
                canDash = false;
            }
            //When the player is slideing the Y speed works the same as when they are not dashing. The slide is not capped.
            if (gameObject.slideing && (gameObject.dash || Time.fixedTime - lastDash <= 0.25f || !gameObject.canGetUp))
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.lookingRight * 20.0f, velY);
            }
            else if (gameObject.slideing && !gameObject.dash && Time.fixedTime - lastDash > 0.25f && gameObject.canGetUp) gameObject.slideing = false;
        }
        //If the skeleton is dead or waiting the x velocity will be 0
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }
    }
}
