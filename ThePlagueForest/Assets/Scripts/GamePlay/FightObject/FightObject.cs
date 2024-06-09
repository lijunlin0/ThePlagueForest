using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightObject : MonoBehaviour
{    
    protected bool mIsDead;

    protected virtual void Init()
    {
        mIsDead=false;
    }

    public virtual void PlayDestroyAnimation()
    {
        Destroy(gameObject);
    }
    public bool IsDead() {return mIsDead;}
}
