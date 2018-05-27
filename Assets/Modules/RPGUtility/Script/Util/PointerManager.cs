using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerManager : SingletonMonoBehaviourCanDestroy<PointerManager>
{
    private GameObject _player;

    private GameObject _target;

    [Header("rayの太さ")] public float _radius = 0.5f;
    // Use this for initialization
    void Start()
    {
        _player = _player = GameObject.FindWithTag("Player");
        TouchManager.AddListener(EventTriggerType.PointerDown,FindTarget);
        TouchManager.AddListener(EventTriggerType.Drag,FindTarget);
        TouchManager.AddListener(EventTriggerType.PointerUp,i=>_target = null);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    float Dist(GameObject a,GameObject b)
    {
        return (a.transform.position - b.transform.position).magnitude;
    }
    /// <summary>
    /// RayCastでターゲットを探す
    /// </summary>
    void FindTarget(PointerEventData pe)
    {

            Vector2 touch = (Vector2)TouchManager.WorldPosition(pe.position);
            RaycastHit2D[] hit = Physics2D.CircleCastAll(touch, _radius, Vector2.zero);
            foreach (var h in hit)
            {
                if (h.collider != null)
                {
                    ITargetable target = h.collider.transform.root.GetInterface<ITargetable>();
                    if (target != null)
                    {
//                        Debug.Log("HitTarget");
                        if (_target == null)
                        {
                            _target = h.collider.transform.root.gameObject;
                            continue;
                        }
                        if (Dist(_target, _player) > Dist(h.collider.gameObject, _player))
                        {
                            _target = h.collider.transform.root.gameObject;
                        }
                       
                    }
                }
            }
            if(hit.Length == 0)
            _target = null;
        
    }

    public static GameObject GetTarget()
    {
        if(Instance)
        return Instance._target;
        return null;
    }
 
}
