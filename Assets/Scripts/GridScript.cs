using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;
public class GridScript : MonoBehaviour
{

    GameObject stone_;
    GameObject blackStonePrefab_, whiteStonePrefab_;
    GameObject StonePrefab_;
    BoardScript boardScript_;
    int colNo_, rowNo_;
    int colNum_, rowNum_;
    int dirNum_ = 8; // 8方向
    int[] dirCol_ = new int[8] { -1, 0, 1, -1, 1, -1, 0, 1 }; // 各方向の移動量
    int[] dirRow_ = new int[8] { -1, -1, -1, 0, 0, 1, 1, 1 }; // 各方向の移動量

    public void SetColNo(int colNo)
    {
        colNo_ = colNo;
    }

    public int GetColNo()
    {
        return colNo_;
    }

    public void SetRowNo(int rowNo)
    {
        rowNo_ = rowNo;
    }

    public int GetRowNo()
    {
        return rowNo_;
    }

    public void SetBlackStonePrefab(GameObject blackStonePrefab)
    {
        blackStonePrefab_ = blackStonePrefab;
    }

    public void SetWhiteStonePrefab(GameObject whiteStonePrefab)
    {
        whiteStonePrefab_ = whiteStonePrefab;
    }

    public void SetStonePrefab(GameObject StonePrefab)
    {
        StonePrefab_ = StonePrefab;
    }

    public GameObject GetStone()
    {
        return stone_;
    }

    public void SetBoardScript(BoardScript boardScript)
    {
        boardScript_ = boardScript;
    }

    bool SearchSameColorStone(bool isBlackTurn, int dirNum, int colNo, int rowNo)
    {
        if (colNo >= 0 && colNo < boardScript_.GetColNum() && rowNo >= 0 && rowNo < boardScript_.GetRowNum())
        {
            GameObject grid = boardScript_.GetGrid(colNo, rowNo);
            GameObject stone = grid.GetComponent<GridScript>().GetStone();
            if (stone && stone.GetComponent<StoneScript>().IsBlack() == isBlackTurn)
            {
                return true;
            }
            else
            {
                return SearchSameColorStone(isBlackTurn, dirNum, colNo + dirCol_[dirNum], rowNo + dirRow_[dirNum]);
            }
        }
        return false;
    }

    bool JudgeStonePutableDir(bool isBlackTurn, int dir)
    {
        // 1個目が自分と異なる色か確認
        int colNo = colNo_ + dirCol_[dir];
        int rowNo = rowNo_ + dirRow_[dir];
        if (colNo >= 0 && colNo < boardScript_.GetColNum() && rowNo >= 0 && rowNo < boardScript_.GetRowNum())
        {
            GameObject grid = boardScript_.GetGrid(colNo, rowNo);
            GameObject stone = grid.GetComponent<GridScript>().GetStone();
            if (stone && stone.GetComponent<StoneScript>().IsBlack() != isBlackTurn)
            {
                // 自分と同じ色の石を探索していく
                return SearchSameColorStone(isBlackTurn, dir, colNo + dirCol_[dir], rowNo + dirRow_[dir]);
            }

        }
        return false;
    }

    public bool JudgeStonePutable(bool isBlackTurn)
    {
        for (int d = 0; d < dirNum_; ++d)
        {
            if (JudgeStonePutableDir(isBlackTurn, d))
            {
                return true;
            }
        }
        return false;
    }

    void TurnStoneDir(bool isBlackTurn, int dir, int colNo, int rowNo)
    {
        if (colNo >= 0 && colNo < boardScript_.GetColNum() && rowNo >= 0 && rowNo < boardScript_.GetRowNum())
        {
            GameObject grid = boardScript_.GetGrid(colNo, rowNo);
            GameObject stone = grid.GetComponent<GridScript>().GetStone();
            if (stone && stone.GetComponent<StoneScript>().IsBlack() != isBlackTurn)
            {
                Destroy(stone);
                grid.GetComponent<GridScript>().PutStone(isBlackTurn);
                // stone.transform.transform.Rotate(new Vector3(0, 180, 0));
                TurnStoneDir(isBlackTurn, dir, colNo + dirCol_[dir], rowNo + dirRow_[dir]);
            }
            else
            {
                return;
            }
        }
    }

    public void TurnStone(bool isBlackTurn)
    {
        for (int d = 0; d < dirNum_; ++d)
        {
            if (JudgeStonePutableDir(isBlackTurn, d))
            {
                TurnStoneDir(isBlackTurn, d, colNo_ + dirCol_[d], rowNo_ + dirRow_[d]);
            }
        }
    }
    public void PutStone(bool isBlack)
    // public async Task PutStone(bool isBlack)
    {
        // 黒か白のPrefabを設定
        GameObject stonePrefab;
        if (isBlack)
        {
            stonePrefab = blackStonePrefab_;

        }
        else
        {
            stonePrefab = whiteStonePrefab_;
        }
        // 石を置く
        Vector3 pos = transform.position;
        pos.y += 0.25f;
        stone_ = (GameObject)Instantiate(stonePrefab, pos, Quaternion.identity);
        stone_.transform.parent = GameObject.Find("stones").transform;
        // 石の色を設定
        stone_.AddComponent<StoneScript>();
        stone_.GetComponent<StoneScript>().IsBlack(isBlack);
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}