using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

//Boss子弹
public class BossBullet : Bullet
{
    protected Animator mAnimator;
    public static BossBullet Create(Enemy enemy,int points,float rotation)
    {
        GameObject bulletPrefab=Resources.Load<GameObject>("FightObject/Bullet/BulletBoss");
        GameObject bulletObject=Instantiate(bulletPrefab,enemy.transform.position,Quaternion.Euler(0,0,rotation));
        BossBullet bullet=bulletObject.AddComponent<BossBullet>();
        bullet.Init(enemy,points);
        return bullet;
    }
    protected override void Init(Character source, int points)
    {
        base.Init(source,points);
        mMoveSpeed=700;
        mAnimator=mDisplay.GetComponent<Animator>();
        mAnimator.Play("BulletBossMove");
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
    public override void PlayDestroyAnimation()
    {
        SpriteRenderer spriteRenderer=mDisplay.GetComponent<SpriteRenderer>();
        mAnimator.Play("BulletBossExplosion");
        spriteRenderer.material.DOFade(0,1f).OnComplete(()=>
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        });
    }

}