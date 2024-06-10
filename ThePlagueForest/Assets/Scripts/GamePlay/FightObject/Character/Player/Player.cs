using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : Character
{
    private static Player sCurrent;
    protected static List<Weapon> mWeapons;
    protected override void Init(PropertySheet basePropertySheet)
    {
        base.Init(basePropertySheet);
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

    public void OnUpdate()
    {
        Move();
        foreach(Weapon weapon in mWeapons)
        {
            weapon.OnUpdate();
        }
    }
    public static void AddWeapon(Weapon weapon)
    {
        mWeapons.Add(weapon);
    }
    public static Player GetCurrent()
    {
        return sCurrent;
    }
}
