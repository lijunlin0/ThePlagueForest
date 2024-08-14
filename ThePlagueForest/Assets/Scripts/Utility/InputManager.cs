using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
public class InputManager:MonoBehaviour
{
    private bool IsSwipe=false;
    private Vector3 preTouchPosition;
    private Vector3 currentTouchPosition;
    private Vector3 lastDirection;
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
        currentTouchPosition=finger.screenPosition;
    }
    private void FingerSwipe(Finger finger)
    {
        preTouchPosition=currentTouchPosition;
        currentTouchPosition = finger.screenPosition;
        Vector3 swipeDelta = currentTouchPosition - preTouchPosition;
        if(swipeDelta!=Vector3.zero)
        {
            lastDirection=swipeDelta.normalized;
        }
        IsSwipe=true;
    }
    private void FingerSwipeEnd(Finger finger)
    {
        IsSwipe=false;
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