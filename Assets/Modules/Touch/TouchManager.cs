using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
/// <summary>
/// イベントシステムを利用したタッチ制御
/// 入力 uguiのイベントトリガー
/// </summary>
public class TouchManager : SingletonMonoBehaviourCanDestroy<TouchManager>
{
    public class TouchEvent<A> : UnityEvent<A> { }
    public EventTrigger _trigger;
    public Camera _camera;
    public TouchEvent<PointerEventData> OnTouchDown = new TouchEvent<PointerEventData>();
    public TouchEvent<PointerEventData> OnTouchMove = new TouchEvent<PointerEventData>();
    public TouchEvent<PointerEventData> OnTouchUp = new TouchEvent<PointerEventData>();

    #region In
    /// <summary>
    /// コールバック追加
    /// </summary>
    /// <param name="type">Type.</param>
    /// <param name="cb">Cb.</param>
    public static void AddListener(EventTriggerType type, UnityAction<PointerEventData> cb)
    {
        if (Instance)
        {
            switch (type)
            {
                case EventTriggerType.PointerDown:
                    Instance.OnTouchDown.AddListener(cb);
                    break;
                case EventTriggerType.Drag:
                    Instance.OnTouchMove.AddListener(cb);
                    break;
                case EventTriggerType.PointerUp:
                    Instance.OnTouchUp.AddListener(cb);
                    break;
            }
        }
    }
    #endregion


    #region out
    public static Vector2 WorldPosition(Vector2 screenPos){
        return Instance._camera.ScreenToWorldPoint(screenPos);
    }
    public static Vector2 GetTouchDistance(){

        Vector2 ret = Vector2.zero;
        if(Instance._touchAnchor !=null && Instance._touchCurrent != null){

            ret = (Vector2)Instance._touchCurrent - (Vector2)Instance._touchAnchor;
        }
        return ret;
    }
    #endregion



    #region Process
    Vector2? _touchAnchor = null;
    Vector2? _touchCurrent = null;
    protected override void Awake()
    {
        base.Awake();
        AddTrigger(EventTriggerType.PointerDown, SetAnchor);
        AddTrigger(EventTriggerType.Drag, SetCurrent);
        AddTrigger(EventTriggerType.PointerUp, EndTouch);

        AddTrigger(EventTriggerType.PointerDown, OnTouchDown.Invoke);
        AddTrigger(EventTriggerType.Drag, OnTouchMove.Invoke);
        AddTrigger(EventTriggerType.PointerUp, OnTouchUp.Invoke);

        //debug
        /*
        AddListener(EventTriggerType.PointerDown, e => Debug.Log("Down"));
        AddListener(EventTriggerType.Drag, e => Debug.Log("Move"));
        AddListener(EventTriggerType.PointerUp, e => Debug.Log("Up"));
        */
    }
    void SetAnchor(PointerEventData pe){
        _touchAnchor = pe.position;
        _touchCurrent = pe.position;
    }
    void SetCurrent(PointerEventData pe){
        _touchCurrent = pe.position;
    }
    void EndTouch(PointerEventData pe){
        _touchAnchor = null;
        _touchCurrent = null;
    }
    void AddTrigger(EventTriggerType type, UnityAction<PointerEventData> cb)
    {
        var entry = new EventTrigger.Entry();
        entry.eventID = type; // 他のイベントを設定したい場合はここを変える
        entry.callback.AddListener((e) => { cb((PointerEventData)e); });
        _trigger.triggers.Add(entry);
    }
#endregion
}
