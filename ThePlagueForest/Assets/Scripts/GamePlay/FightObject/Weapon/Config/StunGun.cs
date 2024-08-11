using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//电击枪

public class StunGun:Weapon
{
    private int mAttackAddition=10;
    private float mShootTimeReduce=0.1f;
    private int BulletCount=5;
    public StunGun():base(EquipmentType.Active,EquipmentId.StunGun)
    {
        mBaseAttack=20;
        mBaseShootTime=1.4f;
        mShootTime=mBaseShootTime;
        mAttack=mBaseAttack;
        mStatusEffectId=StatusEffectId.Equipment_StunGun;
    }

    public override void OnGet(StatusEffect statusEffect,int layer)
    {
        if(layer!=1)
        {
            if(layer==mMaxlayer)
            {
                BulletCount+=5;
            }
            mAttack+=mBaseAttack*mAttackAddition/100;
            mShootTime-=mShootTimeReduce;
            Debug.Log("攻击力:"+mAttack+"攻速间隔:"+mShootTime);
            Player.GetCurrent().RemoveWeaponWithStatusEffectId(mStatusEffectId);
        }

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