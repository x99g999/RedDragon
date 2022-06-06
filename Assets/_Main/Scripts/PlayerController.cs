using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public enum PlayerState
    {
        Idle,
        Die,
    };

    private PlayerState state;
    Animator animator;
    private PlayerHP playerHP;

    void Start()
    {
        playerHP = GetComponent<PlayerHP>();

    }
    void Awake()

    {
        //�R���|�[�l���g�֘A�t��
        TryGetComponent(out animator);

        //�J�[�\���̔�\��
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        //���̓x�N�g���̎擾
        if (state == PlayerState.Idle)
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            var horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
            var velocity = horizontalRotation * new Vector3(horizontal, 0, vertical).normalized;


            /***************************************************************/
            // SESSION1 �L�����N�^�[�������悤�ɂ��悤
            //�ړ����̃X�s�[�h
            var speed = 0;
            /***************************************************************/


            //�ړ�����������
            if (velocity.magnitude > 0.5f)
            {
                transform.rotation = Quaternion.LookRotation(velocity, Vector3.up);
            }

            //�ړ����x��Animator�ɔ��f����
            animator.SetFloat("Speed", velocity.magnitude * speed, 0.1f, Time.deltaTime);

            /***************************************************************/
            // SESSION2 �L�����N�^�[�ɍU��������ǉ����Ă݂悤
            //�U������
            if (Input.GetButtonDown("Fire1")) //�}�E�X���N���b�N�ōU��
            {
                
            }
            /***************************************************************/

            if (Input.GetButtonDown("Fire2"))
            {

            }
        }

        /***************************************************************/
        // SESSION3 �L�����N�^�[����������悤�ɂ��Ă݂悤
        else if (state == PlayerState.Die)
        {
            //���������@R�L�[�ŕ���
            if (Input.GetKey(KeyCode.R))
            {
               
            }
        }
        /***************************************************************/

    }
    //�@�G�L�����N�^�[�̏�ԕύX���\�b�h
    public void SetState(PlayerState tempState, Transform targetObj = null)
    {
        state = tempState;
        if (tempState == PlayerState.Idle)
        {
            animator.SetTrigger("Revive");
        }
        else if (tempState == PlayerState.Die)
        {
            animator.SetTrigger("Die");

        }

    }
    //�@�G�L�����N�^�[�̏�Ԏ擾���\�b�h
    public PlayerState GetState()
    {
        return state;
    }


}
