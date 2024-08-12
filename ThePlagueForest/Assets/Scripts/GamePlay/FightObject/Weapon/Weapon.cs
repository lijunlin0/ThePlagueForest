using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class Weapon : Equipment
{
    public const int DefaultShootRange=400;
    public Weapon(EquipmentType equipmentType,EquipmentId equipmentId):base(equipmentType,equipmentId)
    {
        mMaxlayer=5;
    }
}