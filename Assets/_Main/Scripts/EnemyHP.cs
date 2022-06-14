using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


//UI使うときは忘れずに。
using UnityEngine.UI;
using TMPro;

public class EnemyHP : MonoBehaviourPunCallbacks
{
    //最大HPと現在のHP。
    public int maxHp = 155;
    int currentHp;
    //HPbar用
    public Slider slider;
    private EnemyController enemy;
    public Canvas canvas;
    public TextMeshProUGUI text;

    void Start()
    {
        enemy = GetComponent<EnemyController>();
        //HPの初期化
        currentHp = maxHp;
        slider.value = (float)currentHp / (float)maxHp;
        text.text = currentHp + "/" + maxHp;
    }

    void Update()
    {
        //EnemyCanvasをMain Cameraに向かせる
        canvas.transform.rotation =
            Camera.main.transform.rotation;
    }

    //ColliderオブジェクトのIsTriggerにチェック入れること。
    private void OnTriggerEnter(Collider collider)
    {
        //PWeaponタグのオブジェクトに触れると発動
        if (collider.gameObject.tag == "PWeapon")
        {
            //ダメージは75
            int damage = 75;

            //現在のHPからダメージを引く　0より小さくなるようであれば0にする
            if (0 > currentHp - damage)
            {
                currentHp = 0;
            }
            else
            {
                currentHp = currentHp - damage;
            }
            checkHp();

        }
    }

    void checkHp()
    {
        //HPbarとHPtextの上書き
        slider.value = (float)currentHp / (float)maxHp;
        text.text = currentHp + "/" + maxHp;

        /***************************************************************/
        // SESSION4 モンスターの死亡判定の追加
        //HPが0ならステータスをDieに
        if (currentHp == 0)
        {
            //enemy.SetState(EnemyController.EnemyState.Die);
        }
        /***************************************************************/
    }
}