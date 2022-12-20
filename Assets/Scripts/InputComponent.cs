using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputComponent 
{
    public abstract void Create(int numb, Skeleton gameObject);
    public abstract void Update(Skeleton gameObject);
    public abstract void FixedUpdate(Skeleton gameObject);
}
