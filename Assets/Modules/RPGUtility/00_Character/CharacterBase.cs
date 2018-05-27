using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CharacterBase : MonoBehaviour,ICollisionField
{

    //動きのステート オーバーライドして使用
    protected Dictionary<string,bool> _behaviourStates = new Dictionary<string, bool>();
    //現在の向きステート
    protected int _nowDirectionState = 0;

    public int _currentDepth { get; set; }//z座標
    public int _arrowPassableDepthDefference { get; set; }//通行可能なz座標の差    


    //ステート定義(_statesの定義)をする関数 オーバーライドして使用
    protected void SetStates<T>()
    {
        foreach (T p in Enum.GetValues(typeof(T)))
        {
            _behaviourStates.Add(p.ToString(), false);
        }
    }

    //stateが変更されたら値を反映
    protected int ChangeDirectionState(int state)
    {
        _nowDirectionState = state;
        return state;
    }
    //trueにされたらトグル
    protected void ChangeBehaviourState(string behaviour,bool flag)
    {
        if (_behaviourStates[behaviour] != flag)
        {
            _behaviourStates[behaviour] = flag;
        }
    }
    protected virtual void Awake()
    {
    }
    public Dictionary<string,bool> GetBehaviourStates()
    {
        return _behaviourStates;
    }


}
