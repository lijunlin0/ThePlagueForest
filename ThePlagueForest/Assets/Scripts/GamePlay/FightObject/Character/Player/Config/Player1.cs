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
        GameObject playerPrefab=Resources.Load<GameObject>("FightObject/Character/Player");
        GameObject playerObject=GameObject.Instantiate(playerPrefab);
        Player1 player=playerObject.AddComponent<Player1>();
        player.Init();
        return player;
    }
    public static Vector3 GetPosition()
    {
        return new Vector3(0, 0,0);
    }
    protected override void Init()
    {
        base.Init();
        Weapon weapon=new Dagger();
        weapon.Init();
        AddWeapon(weapon);
    }
}
