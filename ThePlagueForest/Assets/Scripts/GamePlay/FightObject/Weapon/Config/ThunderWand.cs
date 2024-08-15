using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//闪电魔杖
public class ThunderWand:Weapon
{   
    private const int Attack=15;
    private const int ShootRange=10000;
    private const float  ShootTime=1.2f;

    private const int AttackAddition=10;
    public ThunderWand():base(EquipmentType.Active,EquipmentId.ThunderWand)
    {
        
        mStatusEffectId=StatusEffectId.Equipment_ThunderWand;
    }

    public override void OnGet(StatusEffect statusEffect,int layer)
    {
        int attack=Attack+Attack*AttackAddition/100*(layer-1);
        float shootTime=ShootTime/layer;
         BulletShooter shooter = new BulletShooter(mEquipmentId,Player.GetCurrent(),()=>
         {
            //随机选择一名敌人
            List<Enemy> enemyList = FightModel.GetCurrent().GetEnemies();
            if(enemyList.Count==0)
            {
                return;
            }
            int randomIndex=RandomHelper.RandomInt(0,enemyList.Count-1);
            Enemy targetEnemy=enemyList[randomIndex];
            
            //创建子弹
            BulletThunderWand bulletThunderWand = BulletThunderWand.Create(Player.GetCurrent(),attack);
            bulletThunderWand.transform.position=targetEnemy.transform.position;
            FightModel.GetCurrent().AddPlayerBullet(bulletThunderWand);
        },shootTime,ShootRange);
        Player.GetCurrent().AddBulletShooter(shooter);
    }
  

}