using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//回旋镖
public class Boomerang:Weapon
{
    public const int ShootRange=600;
    private const int Attack=45;
    private const float  ShootTime=1.3f;
    private const float MaxLayerBulletScale=2;
    private const int AttackAddition=20;
    public Boomerang():base(EquipmentType.Active,EquipmentId.Boomerang)
    {
        mStatusEffectId=StatusEffectId.Equipment_Boomerang;
    }
    public override void OnGet(StatusEffect statusEffect,int layer)
    {
        int attack=Attack+Attack*AttackAddition/100*(layer-1);
        float shootTime=ShootTime/layer;
        
        BulletShooter shooter = new BulletShooter(mEquipmentId,Player.GetCurrent(),()=>
        {
            //方向朝着最近的敌人
            Enemy nearEnemy=FightUtility.GetNearEnemy(Player.GetCurrent(),ShootRange);
            if(nearEnemy==null)
            {
                return;
            }
            //创建子弹
            BulletBoomerang bulletBoomerang = BulletBoomerang.Create(Player.GetCurrent(),attack);
            bulletBoomerang.transform.position=Player.GetCurrent().transform.position;
            if(layer==mMaxlayer)
            {
                bulletBoomerang.transform.localScale=new Vector3(MaxLayerBulletScale,MaxLayerBulletScale,1);
            }
            Vector3 direction=(nearEnemy.transform.position-bulletBoomerang.transform.position).normalized;
            bulletBoomerang.transform.rotation=FightUtility.DirectionToRotation(direction);
            FightModel.GetCurrent().AddPlayerBullet(bulletBoomerang);
        },shootTime,ShootRange);
        Player.GetCurrent().AddBulletShooter(shooter);
    }

}