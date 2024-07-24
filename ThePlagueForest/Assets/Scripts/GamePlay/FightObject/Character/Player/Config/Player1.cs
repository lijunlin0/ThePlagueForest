using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player1 : Player
{
    public static Player1 Create()
    {
        GameObject playerPrefab=Resources.Load<GameObject>("FightObject/Character/Player1");
        GameObject playerObject=GameObject.Instantiate(playerPrefab);
        Player1 player=playerObject.AddComponent<Player1>();
        PropertySheet propertySheet=CharacterUtility.GetBasePropertySheet("Player1",1);
        player.Init(propertySheet);
        return player;
    }
    public static Vector3 GetPosition()
    {
        return new Vector3(0, 0,0);
    }
    protected override void Init(PropertySheet basePropertySheet)
    {
        base.Init(basePropertySheet);
        Weapon weapon=new SacredSword();
        Weapon weapon1=new StunGun();
        Weapon weapon2=new FireWand();
        Weapon weapon3=new Boomerang();
        Weapon weapon4=new Dagger();
        weapon.Init();
        weapon1.Init();
        weapon2.Init();
        weapon3.Init();
        weapon4.Init();
        AddWeapon(weapon);
        AddWeapon(weapon1);
        AddWeapon(weapon2);
        AddWeapon(weapon3);
        AddWeapon(weapon4);
    }
}
