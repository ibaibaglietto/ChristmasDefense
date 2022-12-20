using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterInputComponent : InputComponent
{

    private Vector3[] targets;
    private int pos;
    NavMeshAgent agent;

    public override void Create(int numb, Skeleton gameObject)
    {
        pos = 0;
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        if(numb == 1) targets = new[] { new Vector3(-30.4f, 12.3f, 0.0f), new Vector3(-18.8f,12.3f,0.0f), new Vector3(-13.54f,9.4f,0.0f), new Vector3(-8.28f, 12.3f, 0.0f), new Vector3(-1.0f, 12.3f, 0.0f) };
        else if (numb == 2) targets = new[] { new Vector3(-30.4f, 4.28f, 0.0f), new Vector3(-22.5f, 4.28f, 0.0f), new Vector3(-13.54f, 9.4f, 0.0f), new Vector3(-8.28f, 12.3f, 0.0f), new Vector3(-1.0f, 12.3f, 0.0f) };
        else if (numb == 3) targets = new[] { new Vector3(-30.4f, -3.7f, 0.0f), new Vector3(-18.8f, -3.7f, 0.0f), new Vector3(-13.54f, -6.55f, 0.0f), new Vector3(-4.58f, -11.7f, 0.0f), new Vector3(-1.0f, -11.7f, 0.0f) };
        else if (numb == 4) targets = new[] { new Vector3(-30.4f, -11.7f, 0.0f), new Vector3(-22.5f, -11.7f, 0.0f), new Vector3(-13.54f, -6.55f, 0.0f), new Vector3(-4.58f, -11.7f, 0.0f), new Vector3(-1.0f, -11.7f, 0.0f) };
        else if (numb == 5) targets = new[] { new Vector3(30.4f, -11.7f, 0.0f), new Vector3(22.5f, -11.7f, 0.0f), new Vector3(13.54f, -6.55f, 0.0f), new Vector3(4.58f, -11.7f, 0.0f), new Vector3(1.0f, -11.7f, 0.0f) };
        else if (numb == 6) targets = new[] { new Vector3(30.4f, -3.7f, 0.0f), new Vector3(18.8f, -3.7f, 0.0f), new Vector3(13.54f, -6.55f, 0.0f), new Vector3(4.58f, -11.7f, 0.0f), new Vector3(1.0f, -11.7f, 0.0f) };
        else if (numb == 7) targets = new[] { new Vector3(30.4f, 4.28f, 0.0f), new Vector3(22.5f, 4.28f, 0.0f), new Vector3(13.54f, 9.4f, 0.0f), new Vector3(8.28f, 12.3f, 0.0f), new Vector3(1.0f, 12.3f, 0.0f) };
        else if (numb == 8) targets = new[] { new Vector3(30.4f, 12.3f, 0.0f), new Vector3(18.8f, 12.3f, 0.0f), new Vector3(13.54f, 9.4f, 0.0f), new Vector3(8.28f, 12.3f, 0.0f), new Vector3(1.0f, 12.3f, 0.0f) };

        gameObject.transform.position = targets[pos];
        pos = 1;
        agent.SetDestination(targets[pos]);
    }

    public override void FixedUpdate(Skeleton gameObject)
    {
        
    }

    public override void Update(Skeleton gameObject)
    {
        if(!gameObject.attack && Mathf.Abs(targets[pos].x - gameObject.transform.position.x) < 0.01f)
        {
            if(pos < 4)
            {
                pos += 1;
                agent.SetDestination(targets[pos]);
            }
            else
            {
                gameObject.StartAttack();
            }
        }
    }
}
