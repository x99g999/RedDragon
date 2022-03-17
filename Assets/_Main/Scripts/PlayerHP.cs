using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//UI使うときは忘れずに。
using UnityEngine.UI;
using TMPro;

public class PlayerHP : MonoBehaviourPunCallbacks, IPunObservable
{ 
    //最大HPと現在のHP。
    public int maxHp = 155;
    int currentHp;
    bool reviveFlg=false;
    //HPbar用
    public Slider slider;
    //HPtext用
    public TextMeshProUGUI text;
    private PlayerController player;

    void Start()
    {
        player = GetComponent<PlayerController>();
        //HPの初期化
        currentHp = maxHp;
        slider.value = (float)currentHp / (float)maxHp;
        text.text = currentHp + "/" + maxHp;

    }

    public void Revive()
    {
        //HPを最大HPと同じに。
        currentHp = maxHp;
        reviveFlg = true;
        checkHp();
    }


    private void OnTriggerEnter(Collider collider)
    {
        //Enemyタグのオブジェクトに触れると発動
        if (collider.gameObject.tag == "EWeapon"&& photonView.IsMine)
        {
            //ダメージは50～100の中でランダムに決める。
            int damage = Random.Range(50, 100);

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

        //HPが0かつ自分のキャラならステータスをDieに
        if(photonView.IsMine)
        {
            if (currentHp == 0)
            {
                player.SetState(PlayerController.PlayerState.Die);
            }
            if (reviveFlg)
            {
                player.SetState(PlayerController.PlayerState.Idle);
                reviveFlg = false;
            }
        }      
    }
   

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // プレイヤーのhpを送信する
            stream.SendNext(currentHp);
            stream.SendNext(maxHp);
         }
        else
        {
            // 他プレイヤーのhpを受信する
            currentHp = (int)stream.ReceiveNext();
            maxHp = (int)stream.ReceiveNext();
            checkHp();
        }
    }

}
