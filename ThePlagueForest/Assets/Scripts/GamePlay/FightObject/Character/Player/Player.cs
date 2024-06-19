using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : Character
{
    private static Player sCurrent;
    protected static List<Weapon> mWeapons;
    protected float mCollideProtect=0;
    protected override void Init(PropertySheet basePropertySheet)
    {
        base.Init(basePropertySheet);
        mHealthBar=HealthBar.Create(this);
        sCurrent=this;
        mWeapons=new List<Weapon>();
        Camera.main.transform.SetParent(this.transform,false);

    }
    public void Move()
    {
        float horizontalDistance=0f;
        float verticalDistance=0f;

        if(Input.GetKey(KeyCode.W))
        {
            verticalDistance+=1;
        }
        if(Input.GetKey(KeyCode.S))
        {
            verticalDistance-=1;
        }
        if(Input.GetKey(KeyCode.D))
        {
            horizontalDistance+=1;
        }
        if(Input.GetKey(KeyCode.A))
        {
            horizontalDistance-=1;
        }
        Vector3 direction=new Vector3(horizontalDistance,verticalDistance,0f).normalized;
        transform.Translate(direction*mCurrentPropertySheet.GetMoveSpeed()*Time.deltaTime);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if(mCollideProtect>=0)
        {
            mCollideProtect-=Time.deltaTime;
        }
        Move();
        foreach(Weapon weapon in mWeapons)
        {
            weapon.OnUpdate();
        }
    }
    public override void PlayDestroyAnimation()
    {
        mHealthBar.gameObject.SetActive(false);
    }

    public static void AddWeapon(Weapon weapon)
    {
        mWeapons.Add(weapon);
    }
    public static Player GetCurrent()
    {
        return sCurrent;
    }
    public void SetCollideProtect(){mCollideProtect=0.2f;}
    public bool InCollideProtect(){return mCollideProtect>=0;}
}
