using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//火焰魔杖
public class FireWand:Weapon
{
    public const int ShootRange=600;
    private const int Attack=100;
    private const float  ShootTime=2.5f;
    private const int AttackAddition=60;
    public FireWand():base(EquipmentType.Active,EquipmentId.FireWand)
    {
        
        mStatusEffectId=StatusEffectId.Equipment_FireWand;
    }
    public override void OnGet(StatusEffect statusEffect,int layer)
    {
        int attack=Attack+Attack*AttackAddition/100*(layer-1);
        float shootTime=ShootTime/layer;
         BulletShooter shooter = new BulletShooter(mEquipmentId,Player.GetCurrent(),()=>
         {
            Enemy nearEnemy=FightUtility.GetNearEnemy(Player.GetCurrent(),DefaultShootRange);
            if(nearEnemy==null)
            {
                return;
            }
            //创建子弹
            BulletFireWand bulletFireWand = BulletFireWand.Create(Player.GetCurrent(),attack,layer==mMaxlayer);
            bulletFireWand.transform.position=Player.GetCurrent().transform.position;

            //方向朝着最近的敌人
            Vector3 direction=(nearEnemy.transform.position-bulletFireWand.transform.position).normalized;
            bulletFireWand.transform.rotation=FightUtility.DirectionToRotation(direction);
            FightModel.GetCurrent().AddPlayerBullet(bulletFireWand);
        },shootTime,DefaultShootRange);
        Player.GetCurrent().AddBulletShooter(shooter);    }

}