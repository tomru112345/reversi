using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace PhotonPractice
{
    public class PhotonLogin : MonoBehaviourPunCallbacks
    {

        //ゲームバージョン指定（設定しないと警告が出る）
        string GameVersion = "Ver1.0";

        //ルームオプションのプロパティー
        static RoomOptions RoomOPS = new RoomOptions()
        {
            MaxPlayers = 2, //0だと人数制限なし
            IsOpen = true, //部屋に参加できるか
            IsVisible = true, //この部屋がロビーにリストされるか
        };



        // Use this for initialization
        void Start()
        {

            //PhotonCloudに接続
            Debug.Log("PhotonLoing");
            //ゲームバージョン設定
            PhotonNetwork.GameVersion = GameVersion;
            //PhotonServerSettingsファイルで構成されたPhotonに接続。
            PhotonNetwork.ConnectUsingSettings();

        }

        //クライアントがマスターサーバーに接続されたときに呼び出される。
        public override void OnConnectedToMaster()
        {

            //ルームへの参加　or 新規作成
            PhotonNetwork.JoinOrCreateRoom("Photonroom", RoomOPS, null);
            //("ルームの名前",ルームオプションの変数,新規ルームを一覧したいロビー。nullで無視)

        }

        //ルーム作成して入室に成功したときに呼び出される。
        public override void OnJoinedRoom()
        {
            //Room型とPlayer型の指定。
            Room myroom = PhotonNetwork.CurrentRoom; //myroom変数にPhotonnetworkの部屋の現在状況を入れる。
            Photon.Realtime.Player player = PhotonNetwork.LocalPlayer; //playerをphotonnetworkのローカルプレイヤーとする
            Debug.Log("ルーム名:" + myroom.Name);
            Debug.Log("PlayerNo" + player.ActorNumber);
            Debug.Log("プレイヤーID" + player.UserId);


            //この部分はニックネームを決めるためのもので、入力は不要です。
            if (player.ActorNumber == 1)
            {
                player.NickName = "わたしは1です";
            }

            Debug.Log("プレイヤー名" + player.NickName);
            Debug.Log("ルームマスター" + player.IsMasterClient); //ルームマスターならTrur。最初に部屋を作成した場合は、基本的にルームマスターなはず。

        }

        //入室失敗したときに呼び出される動作。
        public override void OnJoinRandomFailed(short returnCode, string message)
        {

            Debug.Log("入室失敗");
            //ルームを作成する。
            PhotonNetwork.CreateRoom(null, RoomOPS); //JoinOrCreateroomと同じ引数が使用可能。nullはルーム名を作成したくない場合roomNameを勝手に割り当てる。
        }

        //ルーム作成失敗したときの動作。
        public override void OnCreateRoomFailed(short returnCode, string message)
        {

            Debug.Log("作成失敗");

        }

    }

}

