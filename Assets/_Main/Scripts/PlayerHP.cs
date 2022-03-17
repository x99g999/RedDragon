using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//UI�g���Ƃ��͖Y�ꂸ�ɁB
using UnityEngine.UI;
using TMPro;

public class PlayerHP : MonoBehaviourPunCallbacks, IPunObservable
{ 
    //�ő�HP�ƌ��݂�HP�B
    public int maxHp = 155;
    int currentHp;
    bool reviveFlg=false;
    //HPbar�p
    public Slider slider;
    //HPtext�p
    public TextMeshProUGUI text;
    private PlayerController player;

    void Start()
    {
        player = GetComponent<PlayerController>();
        //HP�̏�����
        currentHp = maxHp;
        slider.value = (float)currentHp / (float)maxHp;
        text.text = currentHp + "/" + maxHp;

    }

    public void Revive()
    {
        //HP���ő�HP�Ɠ����ɁB
        currentHp = maxHp;
        reviveFlg = true;
        checkHp();
    }


    private void OnTriggerEnter(Collider collider)
    {
        //Enemy�^�O�̃I�u�W�F�N�g�ɐG���Ɣ���
        if (collider.gameObject.tag == "EWeapon"&& photonView.IsMine)
        {
            //�_���[�W��50�`100�̒��Ń����_���Ɍ��߂�B
            int damage = Random.Range(50, 100);

            //���݂�HP����_���[�W�������@0��菬�����Ȃ�悤�ł����0�ɂ���
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
        //HPbar��HPtext�̏㏑��
        slider.value = (float)currentHp / (float)maxHp;
        text.text = currentHp + "/" + maxHp;

        //HP��0�������̃L�����Ȃ�X�e�[�^�X��Die��
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
            // �v���C���[��hp�𑗐M����
            stream.SendNext(currentHp);
            stream.SendNext(maxHp);
         }
        else
        {
            // ���v���C���[��hp����M����
            currentHp = (int)stream.ReceiveNext();
            maxHp = (int)stream.ReceiveNext();
            checkHp();
        }
    }

}
