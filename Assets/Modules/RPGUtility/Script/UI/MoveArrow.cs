using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MoveArrow : MonoBehaviour {
    Player _player;
    public Animator _anim;
    State _nowState = State.Normal;
    enum State
    {
        Normal,
        Battle,
        Talk
    }
    void ChangeState(State s){
        if(_nowState != s){
            _nowState = s;
            _anim.SetTrigger(s.ToString());
        }
    }
	// Use this for initialization
    //Todo:PlayerManager作成してシングルトンからplayerを取得してくる
	void Start () {
        TouchManager.AddListener(EventTriggerType.PointerDown,ArrowMove);
        TouchManager.AddListener(EventTriggerType.Drag,ArrowMove);
        TouchManager.AddListener(EventTriggerType.PointerUp,(i)=>ArrowResize(1f));
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();

	}
    void ArrowResize(float extend){
        transform.localScale = new Vector3(extend,extend,1);
    }
    void ArrowMove(PointerEventData pe){
            ArrowResize(3f);
            if (_player)
            {
                switch (_player.GetTargetType())
                {
                    case TargetType.None:
                        ChangeState(State.Normal);
                        break;
                    case TargetType.Attack:
                        ChangeState(State.Battle);
                        break;
                    case TargetType.Talk:
                        ChangeState(State.Talk);
                        break;
                }
            }


        Vector2 touch = (Vector2)TouchManager.WorldPosition(pe.position);
        transform.position = touch;
    }
	// Update is called once per frame
	void Update () {
        if(_player){
            if(_player.IsTarget()){
                transform.position = _player.TargetPos() + new Vector2(0,0.5f);
            }
        }
	}
}
