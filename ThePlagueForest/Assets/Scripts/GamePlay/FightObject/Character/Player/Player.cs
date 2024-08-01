using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class Player : Character
{
    private static Player sCurrent;
    protected List<Weapon> mWeapons;
    protected float mCollideProtect=0;
    protected bool mIsShoot=false;
    protected bool mShootflickerFlag=true;
    protected GameObject mAttackRangeArea=null;
    protected static PlayerLevelController mPlayerLevelController;
    protected virtual void Init(PropertySheet basePropertySheet)
    {
        base.Init(CharacterId.Player,basePropertySheet);
        mHealthBar=HealthBar.Create(this);
        sCurrent=this;
        mPlayerLevelController=new PlayerLevelController();
        mWeapons=new List<Weapon>();
        AttackRangeAreaCreate();
        Camera.main.transform.SetParent(this.transform,false);

    }
    public void Move()
    {
        float horizontalDistance=0f;
        float verticalDistance=0f;

        if(Input.GetKey(KeyCode.W))
        {
            verticalDistance+=1;
        }
        if(Input.GetKey(KeyCode.S))
        {
            verticalDistance-=1;
        }
        if(Input.GetKey(KeyCode.D))
        {
            horizontalDistance+=1;
        }
        if(Input.GetKey(KeyCode.A))
        {
            horizontalDistance-=1;
        }
        Vector3 direction=new Vector3(horizontalDistance,verticalDistance,0f).normalized;
        transform.Translate(direction*mCurrentPropertySheet.GetMoveSpeed()*Time.deltaTime);
    }

    public override void OnUpdate()
    {
        mIsShoot=false;
        base.OnUpdate();
        if(mCollideProtect>=0)
        {
            mCollideProtect-=Time.deltaTime;
        }
        Move();
        foreach(Weapon weapon in mWeapons)
        {
            if(weapon is Boomerang)
            {
                ChangeAttackRangeAreaSize(1.3f);
            }
            if(weapon.IsShoot())
            {
                mIsShoot=true;
            }
            weapon.OnUpdate();
        }
        AttackRangeAreaFlicker();
        
    }
    protected void AttackRangeAreaFlicker()
    {
        SpriteRenderer renderer=mAttackRangeArea.GetComponent<SpriteRenderer>();
        if(mIsShoot)
        {
            renderer.DOFade(0f,0.5f);
        }
        //未发射状态闪烁
        else
        {
            if(mShootflickerFlag==true)
            {
                renderer.DOFade(0f,0.75f).OnComplete(()=>
                {
                    mShootflickerFlag=false;
                });
            }
            else
            {
                renderer.DOFade(1f,0.75f).OnComplete(()=>
                {
                    mShootflickerFlag=true;
                });
            }
        }
    }

    protected void AttackRangeAreaCreate()
    {
        GameObject attackRangeArea=Instantiate(Resources.Load<GameObject>("FightObject/Area/AttackRange"),this.transform);
        this.mAttackRangeArea=attackRangeArea;
    }
    protected void ChangeAttackRangeAreaSize(float Size)
    {
        mAttackRangeArea.transform.localScale = new Vector3(Size,Size,0);
    }
    public override void PlayDestroyAnimation()
    {
        mHealthBar.gameObject.SetActive(false);
    }

    public  void AddWeapon(Weapon weapon)
    {
        mWeapons.Add(weapon);
    }
    public static Player GetCurrent()
    {
        return sCurrent;
    }
    public void SetCollideProtect(){mCollideProtect=0.2f;}
    public bool InCollideProtect(){return mCollideProtect>=0;}
    public PlayerLevelController GetPlayerLevelController(){return mPlayerLevelController;}
}
