using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UI�g���Ƃ��͖Y�ꂸ�ɁB
using UnityEngine.UI;

public class HPBarDirection : MonoBehaviour
{
    public Canvas canvas;

    void Update()
    {
        //EnemyCanvas��Main Camera�Ɍ�������
        canvas.transform.rotation =
            Camera.main.transform.rotation;
    }
}