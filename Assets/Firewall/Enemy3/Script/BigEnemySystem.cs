using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BigEnemy3System {

    public static class BigEnemy3 {

        /// <summary>
        /// 親オブジェクト(BigEnemy3を含むオブジェクト)を取得する処理
        /// </summary>
        /// <param name="targetObj">調査対象のオブジェクト</param>
        /// <returns>取得したGameObject</returns>
        public static GameObject RootObjectSearch(GameObject targetObj) {
            if (targetObj.transform.parent.name.Contains("BigEnemy3")) {
                return targetObj.transform.parent.gameObject;
            } else {
                return RootObjectSearch(targetObj.transform.parent.gameObject);
            }//if
        }//BigEnemy3ObjSearch

        /// <summary>
        /// 対象の角度表記の変更処理
        /// 0℃～360℃を-180℃～180℃に表記変更する
        /// </summary>
        /// <param name="targetAngle">表記変更する角度</param>
        /// <returns>表記変更後の度数</returns>
        public static float AngleNotationChange(float targetAngle) {
            if (targetAngle < -180) {
                targetAngle += 360;
            }//if
            if (targetAngle > 180) {
                targetAngle -= 360;
            }//if
            return targetAngle;
        }//AngleNotationChange

        /// <summary>
        /// 一定時間でランダムに変更するBool値の生成
        /// </summary>
        /// <param name="isBool">変更するためのBool値</param>
        /// <param name="randomCount">変更するまでの時間</param>
        /// <param name="counter">現在のカウント値</param>
        /// <returns>更新したBool値とカウント値</returns>
        public static (bool, float) RandomBool(bool isBool, float randomCount, float counter) {
            if (counter > randomCount) {
                counter = 0;
                return (Random.Range(0, 2) == 0, counter);
            } else {
                counter += Time.deltaTime;
            }
            return (isBool, counter);
        }//RandomBool

    }//BigEnemy3

}//BigEnemy3System

