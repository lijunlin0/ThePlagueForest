using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//火焰魔杖子弹
public class BulletFireWand: Bullet
{ 
    public static BulletFireWand Create(Character character,int points)
    {
        GameObject bulletPrefab=Resources.Load<GameObject>("FightObject/Bullet/BulletFireWand");
        GameObject bulletObject=GameObject.Instantiate(bulletPrefab);
        BulletFireWand bullet=bulletObject.AddComponent<BulletFireWand>();
        bullet.Init(character,points);
        return bullet;
    }
   public override void OnUpdate()
    {
        base.OnUpdate();
        if(mIsDead)
        {
            return;
        }
        FightUtility.Move(gameObject,mMoveSpeed);
    }

    public override void OnCollideCharacter(Character target)
    {
        base.OnCollideCharacter(target);
        Player player=Player.GetCurrent();
        EffectArea area= EffectArea.CircleWithPositonCreate("FireWandCircle",target.gameObject.transform.position,(Character enemy)=>
            {
                //添加燃烧状态
                StatusEffect statusEffect=StatusEffectUtility.Burn(enemy);
                StatusEffectChangeInfo statusEffectChangeInfo=new StatusEffectChangeInfo(statusEffect,StatusEffectChangeReason.Fight,player);
                FightSystem.AddStatusEffect(statusEffectChangeInfo);

            });
            area.SetCollisionEnabledCallback(()=>
            {
                area.PlayDestroyAnimation(0.15f);
                area.Collide();
            });
    }
}
