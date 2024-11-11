
using DG.Tweening;
using UnityEngine;

public class FightManager
{
    private static bool mPause;
    private static FightManager sCurrent;
    private PoolManager mPoolManager;
    protected FightModel mFightModel;
    protected ExpBar mExpBar; 
    protected SettingsButton mSettingButton; 
    private InputManager mInputManager; 
    protected LiveTime mLiveTime; 
    public static FightManager GetCurrent()
    {
        return sCurrent;
    }
    public FightManager()
    {
        mInputManager=GameObject.Find("Canvas").AddComponent<InputManager>();
        Utility.CameraAnimation();
        mPause=false;
        sCurrent=this;
        mFightModel=new FightModel();
        mPoolManager=new PoolManager();
    }
    public void OnUpdate()
    {
        if(Player.GetCurrent().IsDead()&&!EndWindow.IsOpen())
        {
            mPause=true;
            EndWindow.SetOpen(true);
            DOVirtual.DelayedCall(2,()=>
            {
                EndWindow.Open();
            });
        }
        if(mPause)
        {
            return;
        }
        mFightModel.OnUpdate();
    }
    public void SetPause(bool pause)
    {
        mPause=pause;
    }
    public static bool IsPause(){return mPause;}

    public PoolManager GetPoolManager(){return mPoolManager;}

    public ExpBar GetExpBar(){return mExpBar;}

    public void CreateUI()
    {
        //生成摇杆
        if(!Utility.IsPC)
        {
            Joystick joystick=Joystick.Create();
            mInputManager.Init(joystick);
        }
        mSettingButton=SettingsButton.Create();
        mLiveTime=LiveTime.Create();
        mExpBar=ExpBar.Create();
        CanvasGroup group1=mExpBar.GetComponent<CanvasGroup>();
        CanvasGroup group2=mSettingButton.GetComponent<CanvasGroup>();
        CanvasGroup group3=mLiveTime.GetComponent<CanvasGroup>();
        int duration=1;
        group1.DOFade(1, duration);
        group2.DOFade(1, duration);
        group3.DOFade(1, duration);
        Player.GetCurrent().GetPlayerLevelController().Init();
        EquipmentSelectWindow.Open(EquipmentUtility.GetStartWeapon(),"选择一把武器");
        FightModel.GetCurrent().TimeStart();
    }

    public LiveTime GetLiveTime(){return mLiveTime;}

}
