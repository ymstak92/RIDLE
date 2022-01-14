using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �I�u�W�F�N�g�̃��C���[���@��ύX���鏈��
/// �X�V����:20220113
/// </summary>
public static class LayerChange {

    /// <summary>
    /// �e�I�u�W�F�N�g�݂̂̃��C���[��ύX���鏈��
    /// </summary>
    /// <param name="parentObject">�Ώۂ̐e�I�u�W�F�N�g</param>
    /// <param name="layerName">�ύX���郌�C���[��</param>
    public static void ParentChange(GameObject parentObject,string layerName) {
        parentObject.layer = LayerMask.NameToLayer(layerName);
    }//ParentChange

    /// <summary>
    /// �e�I�u�W�F�N�g�Ǝq�I�u�W�F�N�g�S�Ẵ��C���[��ύX���鏈��
    /// </summary>
    /// <param name="parentObject">�Ώۂ̐e�I�u�W�F�N�g</param>
    /// <param name="layerName">�ύX���郌�C���[��</param>
    public static void ParentAndChildChange(GameObject parentObject,string layerName) {
        ParentChange(parentObject, layerName);
        ChildChange(parentObject, layerName);
    }//ParentAndChildChange

    /// <summary>
    /// �q�I�u�W�F�N�g�̃��C���[��ύX���鏈��
    /// </summary>
    /// <param name="parentObject">�Ώۂ̎q�I�u�W�F�N�g</param>
    /// <param name="layerName">�ύX���郌�C���[��</param>
    private static void ChildChange(GameObject parentObject,string layerName) {
        foreach(Transform childTF in parentObject.transform) {
            childTF.gameObject.layer = LayerMask.NameToLayer(layerName);
            ChildChange(childTF.gameObject, layerName);
        }//foreach
    }//ChildChange

}//LayerChange
