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
        //　プレイヤーキャラクターを発見
        if (col.tag == "Player")
        {
            //　敵キャラクターの状態を取得
            EnemyController.EnemyState state = enemyController.GetState();
            //　敵キャラクターが追いかける状態でなければ追いかける設定に変更
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
            Debug.Log("見失う");
            enemyController.SetState(EnemyController.EnemyState.Wait);
        }
    }

}
