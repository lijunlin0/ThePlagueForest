using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//攻击目标动画
public class AttackTargetUI : MonoBehaviour
{
    protected static Animator mAnimator;
    public static AttackTargetUI Create(Enemy enemy)
    {
        GameObject prefab=Resources.Load<GameObject>("Other/AttackTargetUI");
        GameObject gameObject=Instantiate(prefab,enemy.transform);
        AttackTargetUI ui=gameObject.AddComponent<AttackTargetUI>();
        ui.Init();
        return ui;
    }
    protected void Init()
    {
        mAnimator=gameObject.GetComponent<Animator>();
        mAnimator.Play("AttackTarget");
    }

}