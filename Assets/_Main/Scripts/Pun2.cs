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
        // SESSION5 キャラクターに攻撃処理を追加してみよう
        //プレイヤーネーム
        PhotonNetwork.NickName = "Player";
        /***************************************************************/
    }

    void OnGUI()
    {
        //ログインの状態を画面上に出力
        GUILayout.Label(PhotonNetwork.NetworkClientState.ToString());
    }


    //ルームに入室前に呼び出される
    public override void OnConnectedToMaster()
    {
        // "room"という名前のルームに参加する（ルームが無ければ作成してから参加する）
        PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
    }

    //ルームに入室後に呼び出される
    public override void OnJoinedRoom()
    {
        //キャラクターを生成
        var position = new Vector3(Random.Range(-3f, 3f), 0.5f, Random.Range(-3f, 3f));
        GameObject player = PhotonNetwork.Instantiate("Player", position, Quaternion.identity, 0);


        //自分だけが操作できるようにスクリプトを有効にする
        PlayerController controller = player.GetComponent<PlayerController>();
        controller.enabled = true;
        // PlayerHP playerHP = player.GetComponent<PlayerHP>();
        // playerHP.enabled = true;
        // ProcessPlayerAnimEvent processPlayerAnim = player.GetComponent<ProcessPlayerAnimEvent>();
        // processPlayerAnim.enabled = true;

        //モンスターを生成
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.InstantiateRoomObject("Monster", Vector3.zero, Quaternion.identity);
        }
    }
}