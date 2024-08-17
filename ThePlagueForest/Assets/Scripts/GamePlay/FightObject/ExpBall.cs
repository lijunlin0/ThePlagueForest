using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class ExpBall : FightObject 
{
    private const float mPauseTime=2f;
    private const int MoveSpeedReductionFrame=30;
    private float mCreateTime=0;
    private int mExp;
    private SpriteRenderer mSpriteRenderer;
    protected AudioSource mSound;
    private bool mIsWaitingForSound;

    private float mMoveSpeed;
    
    public static ExpBall Create(Vector3 position, int exp,string expType)
    {
        GameObject gameObject=FightManager.GetCurrent().GetPoolManager().GetGameObject("Other/"+expType);
        gameObject.SetActive(true);
        gameObject.transform.position=position;
        ExpBall expBall = gameObject.AddComponent<ExpBall>();
        expBall.Init(exp);
        return expBall;
    }

    private void Init(int exp)
    {
        base.Init();
        mIsWaitingForSound=false;
        mIsPoolObject=true;
        mSpriteRenderer = mDisplay.GetComponent<SpriteRenderer>();
        mSpriteRenderer.enabled=true;
        mSpriteRenderer.color=new Color(1,1,1,0);
        mSpriteRenderer.DOFade(1,0.5f);
        mExp=exp;
        mSound=GetComponent<AudioSource>();
        mMoveSpeed=500;
    }

    public void Update()
    {
        if(mIsWaitingForSound)
        {
            return;
        }
        mCollider.OnUpdate();
        MyCollider collider2=Player.GetCurrent().GetCollider();
        
        if(CollisionHelper.IsColliding(mCollider,collider2))
        {
            mSpriteRenderer.enabled=false;
            mCollider.GetCollider().enabled=false;
            Player.GetCurrent().GetPlayerLevelController().AddExp(mExp);
            mSound.Play();
            mIsWaitingForSound=true;
            DOVirtual.DelayedCall(1,()=>
            {
                FightManager.GetCurrent().GetPoolManager().PutGameObject(gameObject);
                mCollider.GetCollider().enabled=true;
                Destroy(this);
            });
            return;
        }

        mCreateTime+=Time.deltaTime;
        if(mCreateTime<=mPauseTime)
        {
            return;
        }
        Vector3 direction=(Player.GetCurrent().transform.position-transform.position).normalized;
        transform.localRotation=FightUtility.DirectionToRotation(direction);
        mMoveSpeed+=MoveSpeedReductionFrame;
        FightUtility.Move(gameObject,mMoveSpeed);
    }


}