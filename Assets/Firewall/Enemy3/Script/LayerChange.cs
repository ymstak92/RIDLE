using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オブジェクトのレイヤー所法を変更する処理
/// 更新日時:20220113
/// </summary>
public static class LayerChange {

    /// <summary>
    /// 親オブジェクトのみのレイヤーを変更する処理
    /// </summary>
    /// <param name="parentObject">対象の親オブジェクト</param>
    /// <param name="layerName">変更するレイヤー名</param>
    public static void ParentChange(GameObject parentObject,string layerName) {
        parentObject.layer = LayerMask.NameToLayer(layerName);
    }//ParentChange

    /// <summary>
    /// 親オブジェクトと子オブジェクト全てのレイヤーを変更する処理
    /// </summary>
    /// <param name="parentObject">対象の親オブジェクト</param>
    /// <param name="layerName">変更するレイヤー名</param>
    public static void ParentAndChildChange(GameObject parentObject,string layerName) {
        ParentChange(parentObject, layerName);
        ChildChange(parentObject, layerName);
    }//ParentAndChildChange

    /// <summary>
    /// 子オブジェクトのレイヤーを変更する処理
    /// </summary>
    /// <param name="parentObject">対象の子オブジェクト</param>
    /// <param name="layerName">変更するレイヤー名</param>
    private static void ChildChange(GameObject parentObject,string layerName) {
        foreach(Transform childTF in parentObject.transform) {
            childTF.gameObject.layer = LayerMask.NameToLayer(layerName);
            ChildChange(childTF.gameObject, layerName);
        }//foreach
    }//ChildChange

}//LayerChange
