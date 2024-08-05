using UnityEditor;
using UnityEngine;

public class ExpBall : FightObject 
{
    private const float mPauseTime=1f;
    private const int MoveSpeedReductionFrame=30;
    private float mCreateTime=0;
    private int mExp;

    private float mMoveSpeed;
    
    public static ExpBall Create(Vector3 position, int exp)
    {
        GameObject prefab=Resources.Load<GameObject>("FightObject/Other/ExpBall");
        GameObject gameObject=GameObject.Instantiate(prefab);
        gameObject.transform.position=position;
        ExpBall expBall=gameObject.AddComponent<ExpBall>();
        expBall.Init(exp);
        return expBall;
    }

    private void Init(int exp)
    {
        mExp=exp;
        mCollider=new MyCollider(GetComponent<PolygonCollider2D>());
        mMoveSpeed=500;
    }

    public void Update()
    {
        mCollider.OnUpdate();
        MyCollider collider2=Player.GetCurrent().GetCollider();
        
        if(CollisionHelper.IsColliding(mCollider,collider2))
        {

            Player.GetCurrent().GetPlayerLevelController().AddExp(mExp);
            //Debug.Log("获取经验:"+mExp);
            Destroy(gameObject);
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