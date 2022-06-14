using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


//UI�g���Ƃ��͖Y�ꂸ�ɁB
using UnityEngine.UI;
using TMPro;

public class EnemyHP : MonoBehaviourPunCallbacks
{
    //�ő�HP�ƌ��݂�HP�B
    public int maxHp = 155;
    int currentHp;
    //HPbar�p
    public Slider slider;
    private EnemyController enemy;
    public Canvas canvas;
    public TextMeshProUGUI text;

    void Start()
    {
        enemy = GetComponent<EnemyController>();
        //HP�̏�����
        currentHp = maxHp;
        slider.value = (float)currentHp / (float)maxHp;
        text.text = currentHp + "/" + maxHp;
    }

    void Update()
    {
        //EnemyCanvas��Main Camera�Ɍ�������
        canvas.transform.rotation =
            Camera.main.transform.rotation;
    }

    //Collider�I�u�W�F�N�g��IsTrigger�Ƀ`�F�b�N����邱�ƁB
    private void OnTriggerEnter(Collider collider)
    {
        //PWeapon�^�O�̃I�u�W�F�N�g�ɐG���Ɣ���
        if (collider.gameObject.tag == "PWeapon")
        {
            //�_���[�W��75
            int damage = 75;

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

        /***************************************************************/
        // SESSION4 �����X�^�[�̎��S����̒ǉ�
        //HP��0�Ȃ�X�e�[�^�X��Die��
        if (currentHp == 0)
        {
            //enemy.SetState(EnemyController.EnemyState.Die);
        }
        /***************************************************************/
    }
}