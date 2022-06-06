using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;

public class Pun2 : MonoBehaviourPunCallbacks
{

    // Use this for initialization
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        /***************************************************************/
        // SESSION5 �L�����N�^�[�ɍU��������ǉ����Ă݂悤
        //�v���C���[�l�[��
        PhotonNetwork.NickName = "Player";
        /***************************************************************/
    }

    void OnGUI()
    {
        //���O�C���̏�Ԃ���ʏ�ɏo��
        GUILayout.Label(PhotonNetwork.NetworkClientState.ToString());
    }


    //���[���ɓ����O�ɌĂяo�����
    public override void OnConnectedToMaster()
    {
        // "room"�Ƃ������O�̃��[���ɎQ������i���[����������΍쐬���Ă���Q������j
        PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
    }

    //���[���ɓ�����ɌĂяo�����
    public override void OnJoinedRoom()
    {
        //�L�����N�^�[�𐶐�
        var position = new Vector3(Random.Range(-3f, 3f), 0.5f, Random.Range(-3f, 3f));
        GameObject player = PhotonNetwork.Instantiate("Player", position, Quaternion.identity, 0);


        //��������������ł���悤�ɃX�N���v�g��L���ɂ���
        PlayerController controller = player.GetComponent<PlayerController>();
        controller.enabled = true;
        // PlayerHP playerHP = player.GetComponent<PlayerHP>();
        // playerHP.enabled = true;
        // ProcessPlayerAnimEvent processPlayerAnim = player.GetComponent<ProcessPlayerAnimEvent>();
        // processPlayerAnim.enabled = true;

        //�����X�^�[�𐶐�
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.InstantiateRoomObject("Monster", Vector3.zero, Quaternion.identity);
        }
    }
}