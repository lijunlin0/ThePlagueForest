using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//电击枪

public class StunGun:Weapon
{
    public StunGun():base(EquipmentType.Active,EquipmentId.StunGun)
    {
        mStatusEffectId=StatusEffectId.Equipment_StunGun;
    }
    private const int BulletCount=5;
    public override void OnGet(StatusEffect statusEffect,int layer)
    {
        mAttack=20;
        mShootTime=1f;
         BulletShooter shooter = new BulletShooter(()=>
         {
            List<Character> mTargets=new List<Character>(); 
            Character character=Player.GetCurrent();
            //立即造成伤害
            for(int i=0;i<BulletCount;i++)
            {
                Enemy nearEnemy=FightUtility.GetNearEnemy(character,mTargets);
                if(nearEnemy==null)
                {
                    break;
                }
                mTargets.Add(nearEnemy);
                BulletStunGun bullet=BulletStunGun.Create(nearEnemy,mAttack);
                DamageInfo damageInfo=new DamageInfo(Player.GetCurrent(),nearEnemy,mAttack,bullet,null);
                FightSystem.Damage(damageInfo);
                FightModel.GetCurrent().AddPlayerBullet(bullet);
                character=nearEnemy;
            }
            if(mTargets.Count==0)
            {
                mIsShoot=false;
            }
            else
            {
                mTargets.Insert(0,Player.GetCurrent());
                mIsShoot=true;
                FightUtility.ChainEffect(mTargets);
            }
           

        },mShootTime);
        mBulletShooter=shooter;
    }

}