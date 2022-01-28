using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BigEnemy3System {

    public static class BigEnemy3 {

        /// <summary>
        /// �e�I�u�W�F�N�g(BigEnemy3���܂ރI�u�W�F�N�g)���擾���鏈��
        /// </summary>
        /// <param name="targetObj">�����Ώۂ̃I�u�W�F�N�g</param>
        /// <returns>�擾����GameObject</returns>
        public static GameObject RootObjectSearch(GameObject targetObj) {
            if (targetObj.transform.parent.name.Contains("BigEnemy3")) {
                return targetObj.transform.parent.gameObject;
            } else {
                return RootObjectSearch(targetObj.transform.parent.gameObject);
            }//if
        }//BigEnemy3ObjSearch

        /// <summary>
        /// �Ώۂ̊p�x�\�L�̕ύX����
        /// 0���`360����-180���`180���ɕ\�L�ύX����
        /// </summary>
        /// <param name="targetAngle">�\�L�ύX����p�x</param>
        /// <returns>�\�L�ύX��̓x��</returns>
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
        /// ��莞�ԂŃ����_���ɕύX����Bool�l�̐���
        /// </summary>
        /// <param name="isBool">�ύX���邽�߂�Bool�l</param>
        /// <param name="randomCount">�ύX����܂ł̎���</param>
        /// <param name="counter">���݂̃J�E���g�l</param>
        /// <returns>�X�V����Bool�l�ƃJ�E���g�l</returns>
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

