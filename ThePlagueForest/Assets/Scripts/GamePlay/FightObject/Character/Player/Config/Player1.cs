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
        Weapon weapon=new Dagger();
        weapon.Init();
        AddWeapon(weapon);
    }
}
