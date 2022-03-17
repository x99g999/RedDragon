using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchPlayer : MonoBehaviour
{
    private EnemyController enemyController;

    void Start()
    {
        enemyController = GetComponentInParent<EnemyController>();
    }

    void OnTriggerStay(Collider col)
    {
        //�@�v���C���[�L�����N�^�[�𔭌�
        if (col.tag == "Player")
        {
            //�@�G�L�����N�^�[�̏�Ԃ��擾
            EnemyController.EnemyState state = enemyController.GetState();
            //�@�G�L�����N�^�[���ǂ��������ԂłȂ���Βǂ�������ݒ�ɕύX
            if (state == EnemyController.EnemyState.Wait || state == EnemyController.EnemyState.Walk)
            {
                enemyController.SetState(EnemyController.EnemyState.Chase, col.transform);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("������");
            enemyController.SetState(EnemyController.EnemyState.Wait);
        }
    }

}
