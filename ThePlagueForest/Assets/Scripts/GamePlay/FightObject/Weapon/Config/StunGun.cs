using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//电击枪

public class StunGun:Weapon
{
    private const int BulletCount=5;
    private const int MaxLayerBulletCount=10;

    private const int Attack=30;
    private const float  ShootTime=1f;

    private const int AttackAddition=30;
    public StunGun():base(EquipmentType.Active,EquipmentId.StunGun)
    {
        
        mStatusEffectId=StatusEffectId.Equipment_StunGun;
    }

    public override void OnGet(StatusEffect statusEffect,int layer)
    {
        int attack=Attack+Attack*AttackAddition/100*(layer-1);
        float shootTime=ShootTime/layer;

         BulletShooter shooter = new BulletShooter(mEquipmentId,Player.GetCurrent(),()=>
         {
            List<Character> targets=new List<Character>(); 
            Character character=Player.GetCurrent();
            int bulletCount=BulletCount;
            if(layer==mMaxlayer)
            {
                bulletCount=MaxLayerBulletCount;
            }
            //立即造成伤害
            for(int i=0;i<bulletCount;i++)
            {
                Enemy nearEnemy=FightUtility.GetNearEnemy(character,DefaultShootRange,targets);
                if(nearEnemy==null)
                {
                    break;
                }
                targets.Add(nearEnemy);
                BulletStunGun bullet=BulletStunGun.Create(nearEnemy,attack);
                DamageInfo damageInfo=new DamageInfo(Player.GetCurrent(),nearEnemy,attack,bullet,null);
                FightSystem.Damage(damageInfo);
                FightModel.GetCurrent().AddPlayerBullet(bullet);
                character=nearEnemy;
            }
            if(targets.Count!=0)
            {
                targets.Insert(0,Player.GetCurrent());
                if(layer!=mMaxlayer)
                {
                    FightUtility.ChainEffect(targets,false);
                }
                else
                {
                    FightUtility.ChainEffect(targets,true);
                }
                
            }    

        },shootTime,DefaultShootRange);
        Player.GetCurrent().AddBulletShooter(shooter);
    }

}