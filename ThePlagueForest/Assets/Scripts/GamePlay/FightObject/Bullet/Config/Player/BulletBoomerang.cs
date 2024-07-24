using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

//匕首子弹
public class BulletBoomerang: Bullet
{ 
    private const float MoveSpeedReduction=100;
    private const float MinMoveSpeed=300;
    private const float MoveSpeedReductionFrame=3.5f;
    private float mMoveSpeedReduction;
    private bool mOutFlag=true;
    private Tween mRotateTween;
    public static BulletBoomerang Create(Character character,int points)
    {
        GameObject bulletPrefab=Resources.Load<GameObject>("FightObject/Bullet/BulletBoomerang");
        GameObject bulletObject=GameObject.Instantiate(bulletPrefab);
        BulletBoomerang bullet=bulletObject.AddComponent<BulletBoomerang>();
        bullet.Init(character,points);
        return bullet;
    }

    protected override void Init(Character source,int points)
    {
        base.Init(source,points);
        mIsPenetrate=true;
        mMoveSpeed=3000;
        mMoveSpeedReduction=100;
        mRotateTween = mDisplay.transform.DOLocalRotate(new Vector3(0, 0, 360), 0.5f,RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
    }
   public override void OnUpdate()
    {
        base.OnUpdate();
        if(mIsDead)
        {
            return;
        }
        Player player=Player.GetCurrent();
        float distance=Vector2.Distance(gameObject.transform.position,player.gameObject.transform.position);
        //飞回来后销毁子弹
        if(!mOutFlag&&distance<=100)
        {
            mIsDead=true;
            mRotateTween.Kill();
        }
        if(mOutFlag&&mMoveSpeed<MinMoveSpeed+1)
        {
            OnMoveFirstEnd();
        }
        //飞出去
        if(mOutFlag)
        {
            mMoveSpeed=Mathf.Max(MinMoveSpeed,mMoveSpeed-mMoveSpeedReduction);
            FightUtility.Move(gameObject,mMoveSpeed);
            mMoveSpeedReduction+=MoveSpeedReductionFrame;
        }
        //飞回来
        else
        {
            mMoveSpeedReduction+=MoveSpeedReductionFrame;
            mMoveSpeed=Mathf.Max(MinMoveSpeed,mMoveSpeed+mMoveSpeedReduction);
            gameObject.transform.rotation=FightUtility.DirectionToRotation(player.transform.position-gameObject.transform.position).normalized;
            FightUtility.Move(gameObject,mMoveSpeed);
        }
        
    }

    private void OnMoveFirstEnd()
    {
        mIgnoreList.Clear();
        mOutFlag=false;
        mMoveSpeedReduction=MoveSpeedReduction;
        mMoveSpeed=MinMoveSpeed;

    }
}
