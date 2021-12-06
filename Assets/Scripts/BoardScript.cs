using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : MonoBehaviour
{
    public GameObject gridPrefab_;
    public GameObject blackStonePrefab_;
    public GameObject whiteStonePrefab_;
    public GameObject StonePrefab_;

    static int colNum_ = 8; // 縦のグリッド数
    static int rowNum_ = 8; // 縦横のグリッド数
    int planeSize_ = 1; // 1グリッドの大きさ
    GameObject grid;
    GridScript gridScript;
    // GameObject[] grids_ = new GameObject[colNum_ * rowNum_];
    GameObject[,] grids_ = new GameObject[colNum_, rowNum_];

    public int GetColNum()
    {
        return colNum_;
    }

    public int GetRowNum()
    {
        return rowNum_;
    }

    public GameObject GetGrid(int colNo, int rowNo)
    {
        // return grids_[rowNo * colNum_ + colNo];
        return grids_[colNo, rowNo];
    }

    // グリッド群（オセロ盤）の生成
    void MakeGrids()
    {
        // 盤中心から端グリッド中心までの距離[グリッド分]
        float offsetX = colNum_ / 2 - 0.5f;
        float offsetZ = rowNum_ / 2 - 0.5f;

        for (int z = 0; z < rowNum_; z++)
        {
            float posZ = (offsetZ - z) * planeSize_; // グリッド中心のz座標

            for (int x = 0; x < colNum_; x++)
            {
                // グリッドの生成
                float posX = (x - offsetX) * planeSize_; // グリッド中心のx座標
                Vector3 pos = new Vector3(posX, 0f, posZ); // グリッド中心の三次元座標
                grid = (GameObject)Instantiate(gridPrefab_, pos, Quaternion.identity); // グリッドの生成
                grid.transform.parent = GameObject.Find("Grids").transform;

                // グリッドの登録
                grid.AddComponent<GridScript>();
                gridScript = grid.GetComponent<GridScript>();
                gridScript.SetColNo(x);
                gridScript.SetRowNo(z);

                gridScript.SetBlackStonePrefab(blackStonePrefab_);
                gridScript.SetWhiteStonePrefab(whiteStonePrefab_);
                gridScript.SetStonePrefab(StonePrefab_);
                gridScript.SetBoardScript(this);
                // grids_[z * colNum_ + x] = grid;
                grids_[x, z] = grid;
            }
        }

        // 初期配置の生成
        int right = colNum_ / 2;
        int left = right - 1;
        int bottom = rowNum_ / 2;
        int top = rowNum_ / 2 - 1;
        grids_[left, top].GetComponent<GridScript>().PutStone(false);
        grids_[right, bottom].GetComponent<GridScript>().PutStone(false);
        grids_[right, top].GetComponent<GridScript>().PutStone(true);
        grids_[left, bottom].GetComponent<GridScript>().PutStone(true);
    }

    void Start()
    {
        MakeGrids();
    }

    void Update()
    {
    }
}