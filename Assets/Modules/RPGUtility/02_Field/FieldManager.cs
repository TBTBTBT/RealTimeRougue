using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang.Environments;
using UnityEngine;
using UnityEngine.Events;


public enum FieldParam
{
    IsPassable,//通行可能か
    IsEnemySpawner,//敵のスポーン地点か
    IsShop,//ショップか
    IsTresure,//宝箱のスポーン地点か
}
/// <summary>
/// 通行できる場所や、床の状態を管理するクラス
/// </summary>
public class FieldInfo
{
    //管理する情報
    public int _depth = 0;

    public FieldInfo(int d)
    {
        _depth = d;
    }
    public int GetDepth(){
        return _depth;
    }
}
/// <summary>
/// フィールドに関する処理を行うクラス
/// </summary>
public class FieldManager : SingletonMonoBehaviourCanDestroy<FieldManager>
{

    public Grid _grid;

    private FieldInfo[,] _field;

	protected override void Awake()
	{
        base.Awake();
        _field = InitField(10, 10);
	}
    protected FieldInfo[,] InitField(int w,int h){
        FieldInfo[,] ret = new FieldInfo[w, h];
        for (int i = 0; i < w;i ++){
            for (int j = 0; j < h; j++)
            {
                ret[i, j] = new FieldInfo(0);
            }
        }
        return ret;
    }
	#region 座標
	public static Vector3Int LocalToBlockPos(Vector3 pos)
    {
        Vector3Int ret = Instance._grid.LocalToCell(pos);
        return ret;
    }
    public static Vector3Int WorldToBlockPos(Vector3 pos)
    {
        return Instance._grid.WorldToCell(pos);
    }
    public static Vector3 BlockPosToLocal(Vector3Int pos)
    {
        return Instance._grid.GetCellCenterLocal(pos);
    }
    public static Vector3 BlockPosToWorld(Vector3Int pos)
    {
        return Instance._grid.GetCellCenterWorld(pos);
    }
#endregion


}
