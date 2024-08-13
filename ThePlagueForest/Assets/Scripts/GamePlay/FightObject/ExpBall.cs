using DG.Tweening;
using UnityEditor;
using UnityEngine;

public class ExpBall : FightObject 
{
    private const float mPauseTime=2f;
    private const int MoveSpeedReductionFrame=30;
    private float mCreateTime=0;
    private int mExp;
    protected AudioSource mSound;

    private float mMoveSpeed;
    
    public static ExpBall Create(Vector3 position, int exp,string expType)
    {
        GameObject prefab=Resources.Load<GameObject>("FightObject/Other/"+expType);
        GameObject gameObject=GameObject.Instantiate(prefab);
        gameObject.transform.position=position;
        ExpBall expBall=gameObject.AddComponent<ExpBall>();
        expBall.Init(exp);
        return expBall;
    }

    private void Init(int exp)
    {
        base.Init();
        SpriteRenderer renderer = mDisplay.GetComponent<SpriteRenderer>();
        renderer.color=new Color(1,1,1,0);
        renderer.DOFade(1,0.5f);
        mExp=exp;
        mSound=GetComponent<AudioSource>();
        mMoveSpeed=500;
    }

    public void Update()
    {
        mCollider.OnUpdate();
        MyCollider collider2=Player.GetCurrent().GetCollider();
        
        if(CollisionHelper.IsColliding(mCollider,collider2))
        {
            mCollider.GetCollider().enabled=false;
            mDisplay.SetActive(false);
            Player.GetCurrent().GetPlayerLevelController().AddExp(mExp);
            mSound.Play();
            //Debug.Log("获取经验:"+mExp);
            Destroy(gameObject,mSound.clip.length);
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