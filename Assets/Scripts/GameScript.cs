using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class GameScript : MonoBehaviour // PunCallbacks, IPunObservable
{

    public int data_x; // 選んだマスの座標
    public int data_z; // 選んだマスの座標
    // public byte decision; // どこかのマスを選んだことを伝える
    // private byte sendDecision;
    bool isBlackTurn;
    // private PhotonView photonView;
    // public int id; // 各プレイヤーを識別する為のID

    // タップを検知し、GridScriptを取得する
    bool DetectTap(out GridScript gridScript)
    {
        gridScript = null;
        // if (photonView.IsMine)
        // {

        Ray ray = new Ray();
        RaycastHit hit = new RaycastHit();
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
        {

            if (Input.GetMouseButtonDown(0)) // タップ検知
            {
                GameObject obj = hit.collider.gameObject; // タップしたGameObjectを取得
                gridScript = obj.GetComponent<GridScript>(); // GridScriptを取得
                return true;
            }

        }

        // }
        return false;
    }

    void Start()
    {
        isBlackTurn = true;

        //プレイヤーのIDを取得する。
        // photonView = this.GetComponent & lt; PhotonView & gt; ();
        // id = photonView.OwnerActorNr;
    }

    void Update()
    {
        // タップ検知
        GridScript gridScript;

        if (DetectTap(out gridScript))
        {
            // グリッドをタップしており、かつ、そこに石が無いなら
            if (gridScript && !gridScript.GetStone())
            {
                // そこに石を置けるなら
                if (gridScript.JudgeStonePutable(isBlackTurn))
                //  && sendDecision == 0
                {
                    // 石を置く
                    gridScript.PutStone(isBlackTurn);

                    data_x = gridScript.GetColNo();
                    Debug.Log(data_x);
                    data_z = gridScript.GetRowNo();
                    Debug.Log(data_z);
                    // ひっくり返す
                    gridScript.TurnStone(isBlackTurn);

                    // 相手のターンに変更
                    isBlackTurn = !isBlackTurn;
                }
            }
        }

        //何かしらをクリックした場合、送信用に保持しておく
        // if (decision != 0)
        // {
        //     sendDecision = decision;
        // }
    }

    // public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    // {
    //     // オーナーの場合
    //     if (stream.IsWriting)
    //     {
    //         stream.SendNext(this.data_x);
    //         stream.SendNext(this.data_z);
    //         stream.SendNext(this.sendDecision);
    //         sendDecision = 0;
    //     }
    //     // オーナー以外の場合
    //     else
    //     {
    //         this.data_x = (int)stream.ReceiveNext();
    //         this.data_z = (int)stream.ReceiveNext();
    //         this.decision = (byte)stream.ReceiveNext();
    //     }
    // }
}