using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour {

    bool isBlackTurn;

    // タップを検知し、GridScriptを取得する
    bool DetectTap(out GridScript gridScript)
    {
        gridScript = null;
        if (Input.GetMouseButtonDown(0)) // タップ検知
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit))
            {
                GameObject obj = hit.collider.gameObject; // タップしたGameObjectを取得
                gridScript = obj.GetComponent<GridScript>(); // GridScriptを取得
                return true;
            }
        }
        return false;
    }

    void Start () {
        isBlackTurn = true;
    }
	
	void Update () {
        // タップ検知
        GridScript gridScript;
        if (DetectTap(out gridScript))
        {
            // グリッドをタップしており、かつ、そこに石が無いなら
            if (gridScript && !gridScript.GetStone())
            {
                // そこに石を置けるなら
                if (gridScript.JudgeStonePutable(isBlackTurn))
                {
                    // 石を置く
                    gridScript.PutStone(isBlackTurn);

                    // ひっくり返す
                    gridScript.TurnStone(isBlackTurn);

                    // 相手のターンに変更
                    isBlackTurn = !isBlackTurn;
                }
            }
        }
	}
}