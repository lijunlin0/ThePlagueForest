using UnityEngine;

public class MyCollider
{
    private PolygonCollider2D mCollider;
    private bool mEnable;
    private float mCreateTime;

    public MyCollider(PolygonCollider2D collider)
    {
        mCreateTime=0;
        mCollider = collider;
        mEnable=false;
    }
    public void OnUpdate()
    {
        mCreateTime+= Time.deltaTime;
        if(mCreateTime>=0.05)
        {
            mEnable = true;
        }
    }

    public PolygonCollider2D GetCollider(){return mCollider;}
    public bool IsEnable(){return mEnable;}
}