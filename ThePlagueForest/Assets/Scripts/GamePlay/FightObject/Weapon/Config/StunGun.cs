using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//电击枪

public class StunGun:Weapon
{
    private const int BulletCount=5;
    public override void Init()
    {
        mAttack=20;
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

    public override void OnUpdate()
    {
        mBulletShooter.OnUpdate();
    }

}