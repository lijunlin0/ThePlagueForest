using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

//Boss来袭警告UI
public class WarningUI : MonoBehaviour
{
    protected static Animator mAnimator;
    private Animation mFickerBorderAnimation;
    private Animator mWarningImageAnimator;
    private CanvasGroup mCanvasGroup;
    private AudioSource mAudioSource;
    public static WarningUI Create()
    {
        GameObject parent=GameObject.Find("WindowCanvas");
        GameObject prefab=Resources.Load<GameObject>("UI/WarningUI");
        GameObject gameObject=Instantiate(prefab,parent.transform);
        WarningUI ui=gameObject.AddComponent<WarningUI>();
        ui.Init();
        return ui;
    }
    protected void Init()
    {
        mAnimator=gameObject.GetComponent<Animator>();
        mCanvasGroup=transform.GetComponent<CanvasGroup>();
        mCanvasGroup.alpha=0;
        mFickerBorderAnimation=transform.Find("FickerBorder").GetComponent<Animation>();
        mWarningImageAnimator=transform.Find("WarningImage").GetComponent<Animator>();
        mAudioSource=GetComponent<AudioSource>();
        mWarningImageAnimator.speed=0;
        mFickerBorderAnimation.wrapMode=WrapMode.Loop;
    }

    public void Play(Callback callback)
    {
        mCanvasGroup.alpha=1;
        mFickerBorderAnimation.Play();
        mAudioSource.Play();
        mWarningImageAnimator.speed=1;
        DOVirtual.DelayedCall(4,()=>{
            mFickerBorderAnimation.Stop();
            mWarningImageAnimator.speed=0;
            mCanvasGroup.alpha=0;
            mAudioSource.Stop();
        }).OnComplete(()=>
        {
            callback();
        });
    }

}