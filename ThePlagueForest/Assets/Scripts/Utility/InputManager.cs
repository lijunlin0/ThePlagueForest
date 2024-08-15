using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
public class InputManager:MonoBehaviour
{
    private bool IsSwipe=false;
    private Vector3 startPosition;
    private Vector3 currentTouchPosition;
    private Vector3 lastDirection;
    public static Joystick mJoystick;
    public static GameObject mJoystickCenter;
    public static Vector3 mJoystickBasePosition;
    public void OnDisable()
    {
        Touch.onFingerDown-=FingerDown;
        Touch.onFingerMove-=FingerSwipe;
        Touch.onFingerUp-=FingerSwipeEnd;
    }

    public void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        Touch.onFingerDown+=FingerDown;
        Touch.onFingerMove+=FingerSwipe;
        Touch.onFingerUp+=FingerSwipeEnd;
    }
    private void FingerDown(Finger finger)
    {
        if(mJoystick==null||FightManager.IsPause())
        {
            return;
        }
        currentTouchPosition=finger.screenPosition;
        startPosition=currentTouchPosition;
        mJoystick.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(currentTouchPosition.x, currentTouchPosition.y, -10));
    }
    private void FingerSwipe(Finger finger)
    {
        if(mJoystick==null||FightManager.IsPause())
        {
            return;
        }
        currentTouchPosition = finger.screenPosition;
        Vector3 swipeDelta = currentTouchPosition - startPosition;
        if(swipeDelta!=Vector3.zero)
        {
            lastDirection=swipeDelta.normalized;
            float distance = Mathf.Min(swipeDelta.magnitude, 150);
            mJoystickCenter.transform.localPosition=lastDirection*distance;
        }
        IsSwipe=true;
    }
    private void FingerSwipeEnd(Finger finger)
    {
        if(mJoystick==null||FightManager.IsPause())
        {
            return;
        }
        mJoystickCenter.transform.localPosition=Vector3.zero;
        mJoystick.transform.position=mJoystickBasePosition+Player.GetCurrent().transform.position;
        IsSwipe=false;
        Player.GetCurrent().GetAnimator().Play("Idle");
    }
     private void MovePlayer()
    {
        if(Player.GetCurrent() == null || lastDirection == Vector3.zero)
        {
            return;
        }
        Player.GetCurrent().Move(lastDirection);
    }

    private void Update()
    {
        if (IsSwipe&&!FightManager.IsPause())
        {
            MovePlayer();
        }
    }
}