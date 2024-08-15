using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//匕首
public class Dagger:Weapon
{
    private const int Attack=30;
    private const float  ShootTime=0.5f;
    private const int FinalPenetrateCount=5;
    private const int AttackAddition=10;
    public Dagger():base(EquipmentType.Active,EquipmentId.Dagger)
    {
        mStatusEffectId=StatusEffectId.Equipment_Dagger;
    }
    public override void OnGet(StatusEffect statusEffect,int layer)
    {
        int attack=Attack+Attack*AttackAddition/100*(layer-1);
        float shootTime=ShootTime/layer;
         BulletShooter shooter = new BulletShooter(mEquipmentId,Player.GetCurrent(),()=>
         {
            //方向朝着最近的敌人
            Enemy nearEnemy=FightUtility.GetNearEnemy(Player.GetCurrent(),DefaultShootRange);
            if(nearEnemy==null)
            {
                return;
            }
            int penetrateCount=layer==mMaxlayer?FinalPenetrateCount:0;
            //创建子弹
            BulletDagger bulletDagger = BulletDagger.Create(Player.GetCurrent(),attack,penetrateCount);
            bulletDagger.transform.position=Player.GetCurrent().transform.position;
           
            Vector3 direction=(nearEnemy.transform.position-bulletDagger.transform.position).normalized;
            bulletDagger.transform.rotation=FightUtility.DirectionToRotation(direction);
            FightModel.GetCurrent().AddPlayerBullet(bulletDagger);
        },shootTime,DefaultShootRange);
        Player.GetCurrent().AddBulletShooter(shooter);
    }
}