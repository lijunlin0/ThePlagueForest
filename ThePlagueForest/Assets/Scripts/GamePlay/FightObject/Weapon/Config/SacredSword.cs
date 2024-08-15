using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//圣剑
public class SacredSword:Weapon
{
    private const int Attack=30;
    private const float  ShootTime=1.8f;
    private const float MaxLayerBulletScale=1.5f;

    private const int AttackAddition=10;
    public SacredSword():base(EquipmentType.Active,EquipmentId.SacredSword)
    {
        
        mStatusEffectId=StatusEffectId.Equipment_SacredSword;
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
            //创建子弹
            BulletSacredSword bulletSacredSword = BulletSacredSword.Create(Player.GetCurrent(),attack);
            bulletSacredSword.transform.position=Player.GetCurrent().transform.position;
            if(layer==mMaxlayer)
            {
                bulletSacredSword.transform.localScale=new Vector3(MaxLayerBulletScale,MaxLayerBulletScale,1);
            }
           
            Vector3 direction=(nearEnemy.transform.position-bulletSacredSword.transform.position).normalized;
            bulletSacredSword.transform.rotation=FightUtility.DirectionToRotation(direction);
            FightModel.GetCurrent().AddPlayerBullet(bulletSacredSword);
        },shootTime,DefaultShootRange);
        Player.GetCurrent().AddBulletShooter(shooter);
    }

}