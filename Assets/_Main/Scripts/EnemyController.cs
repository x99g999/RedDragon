using UnityEngine;
using System.Collections;

using Photon.Pun;

public class EnemyController : MonoBehaviourPunCallbacks

{

    public enum EnemyState
    {
        Walk,
        Wait,
        Chase,
        Attack,
        Freeze,
        Die
    };

    private CharacterController enemyController;
    private Animator animator;
    //�@�ړI�n
    private Vector3 destination;
    //�@�����X�s�[�h
    [SerializeField]
    private float walkSpeed = 1.0f;
    //�@���x
    private Vector3 velocity;
    //�@�ړ�����
    private Vector3 direction;
    //�@�����t���O
    private bool arrived;
    //�@SetPosition�X�N���v�g
    private SetPosition setPosition;
    //�@�҂�����
    [SerializeField]
    private float waitTime = 5f;
    //�@�o�ߎ���
    private float elapsedTime;
    // �G�̏��
    private EnemyState state;
    //�@�v���C���[Transform
    private Transform playerTransform;
    //�U������
    public float AttackDistance = 1.5f;

    // Use this for initialization
    void Start()
    {
        enemyController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        setPosition = GetComponent<SetPosition>();
        setPosition.CreateRandomPosition();
        velocity = Vector3.zero;
        arrived = false;
        elapsedTime = 0f;
        SetState(EnemyState.Walk);
    }

    //�@�U��������̃t���[�Y����
    [SerializeField]
    private float freezeTime = 2f;

    void Update()
    {
        if (photonView.IsMine)
        {
            //�@�����܂��̓L�����N�^�[��ǂ���������
            if (state == EnemyState.Walk || state == EnemyState.Chase)
            {
                //�@�L�����N�^�[��ǂ��������Ԃł���΃L�����N�^�[�̖ړI�n���Đݒ�
                if (state == EnemyState.Chase)
                {
                    setPosition.SetDestination(playerTransform.position);
                }
                if (enemyController.isGrounded)
                {
                    velocity = Vector3.zero;
                    animator.SetFloat("Speed", 2.0f);
                    direction = (setPosition.GetDestination() - transform.position).normalized;
                    transform.LookAt(new Vector3(setPosition.GetDestination().x, transform.position.y, setPosition.GetDestination().z));
                    velocity = direction * walkSpeed;
                }

                if (state == EnemyState.Walk)
                {
                    //�@�ړI�n�ɓ����������ǂ����̔���
                    if (Vector3.Distance(transform.position, setPosition.GetDestination()) < 0.7f)
                    {
                        SetState(EnemyState.Wait);
                        animator.SetFloat("Speed", 0.0f);
                    }
                }
                else if (state == EnemyState.Chase)
                {
                    //�@�U�����鋗����������U��
                    if (Vector3.Distance(transform.position, setPosition.GetDestination()) < AttackDistance)
                    {
                        SetState(EnemyState.Attack);
                    }
                }
                //�@�������Ă������莞�ԑ҂�
            }
            else if (state == EnemyState.Wait)
            {
                elapsedTime += Time.deltaTime;

                //�@�҂����Ԃ��z�����玟�̖ړI�n��ݒ�
                if (elapsedTime > waitTime)
                {
                    SetState(EnemyState.Walk);
                }
                //�@�U����̃t���[�Y���
            }
            else if (state == EnemyState.Freeze)
            {
                elapsedTime += Time.deltaTime;

                if (elapsedTime > freezeTime)
                {
                    SetState(EnemyState.Walk);
                }
            }

            velocity.y += Physics.gravity.y * Time.deltaTime;
            enemyController.Move(velocity * Time.deltaTime);
        }
    }


    //�@�G�L�����N�^�[�̏�ԕύX���\�b�h
    public void SetState(EnemyState tempState, Transform targetObj = null)
    {
        state = tempState;
        if (tempState == EnemyState.Walk)
        {
            arrived = false;
            elapsedTime = 0f;
            setPosition.CreateRandomPosition();
        }
        else if (tempState == EnemyState.Chase)
        {
            //�@�ҋ@��Ԃ���ǂ�������ꍇ������̂�Off
            arrived = false;
            //�@�ǂ�������Ώۂ��Z�b�g
            playerTransform = targetObj;
        }
        else if (tempState == EnemyState.Wait)
        {
            elapsedTime = 0f;
            arrived = true;
            velocity = Vector3.zero;
            animator.SetFloat("Speed", 0f);
        }
        else if (tempState == EnemyState.Attack)
        {
            velocity = Vector3.zero;
            animator.SetFloat("Speed", 0f);
            animator.SetBool("Attack", true);
        }
        else if (tempState == EnemyState.Freeze)
        {
            elapsedTime = 0f;
            velocity = Vector3.zero;
            animator.SetFloat("Speed", 0f);
            animator.SetBool("Attack", false);
        }
        else if (tempState == EnemyState.Die)
        {
            animator.SetFloat("Speed", 0f);
            animator.SetTrigger("Die");
        }
    }
    //�@�G�L�����N�^�[�̏�Ԏ擾���\�b�h
    public EnemyState GetState()
    {
        return state;
    }
}