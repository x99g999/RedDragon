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
        //コンポーネント関連付け
        TryGetComponent(out animator);

        //カーソルの非表示
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        //入力ベクトルの取得
        if (state == PlayerState.Idle)
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            var horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
            var velocity = horizontalRotation * new Vector3(horizontal, 0, vertical).normalized;


            /***************************************************************/
            // SESSION1 キャラクターが歩くようにしよう
            //移動時のスピード
            var speed = 0;
            /***************************************************************/


            //移動方向を向く
            if (velocity.magnitude > 0.5f)
            {
                transform.rotation = Quaternion.LookRotation(velocity, Vector3.up);
            }

            //移動速度をAnimatorに反映する
            animator.SetFloat("Speed", velocity.magnitude * speed, 0.1f, Time.deltaTime);

            /***************************************************************/
            // SESSION2 キャラクターに攻撃処理を追加してみよう
            //攻撃処理
            if (Input.GetButtonDown("Fire1")) //マウス左クリックで攻撃
            {
                
            }
            /***************************************************************/

            if (Input.GetButtonDown("Fire2"))
            {

            }
        }

        /***************************************************************/
        // SESSION3 キャラクターが復活するようにしてみよう
        else if (state == PlayerState.Die)
        {
            //復活処理　Rキーで復活
            if (Input.GetKey(KeyCode.R))
            {
               
            }
        }
        /***************************************************************/

    }
    //　敵キャラクターの状態変更メソッド
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
    //　敵キャラクターの状態取得メソッド
    public PlayerState GetState()
    {
        return state;
    }


}
