using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneleteMainsweeper : MonoBehaviour
{
    [SerializeField] Tile mTilePrefab;
    Vector2Int mFieldSize;
    [SerializeField] int mTotalBoomCount = 20;
    [SerializeField] GameObject mTilesRoot;
    [SerializeField] GameObject mGameOverImage;
    [SerializeField] GameObject mGameClearTile;

    int easyTotalBoomCount = 5;
    int nomalTotalBoomCount = 20;
    int hardTotalBoomCount = 30;

    int easyFieldSizex = 5;
    int easyFieldSizey = 5;
    int normalFieldSizex = 10;
    int normalFieldSizey = 10;
    int hardFiieldeSizex = 10;
    int hardFiieldeSizey = 19;
    string sceneName;



    Tile[] mTiles;
    Tile[] mBoomTiles;

    int mDiggedTileCount;


    private void Start()
    {
        GenerateTiles(GameManeger.Instance.difficultyLevel);
        SetBooms();
        SetCounts();
        
    }


    //タイルの設置
    void GenerateTiles(string level)
    {

        switch (level)
        {
            case "easy":
                mFieldSize = new Vector2Int(easyFieldSizey, easyFieldSizex);
                mTotalBoomCount = easyTotalBoomCount;
                break;
            case "normal":
                mFieldSize = new Vector2Int(normalFieldSizey, normalFieldSizex);
                mTotalBoomCount = nomalTotalBoomCount;
                break;
            case "hard":
                mFieldSize = new Vector2Int(hardFiieldeSizey, hardFiieldeSizex);
                mTotalBoomCount = hardTotalBoomCount;
                break;
        }

        Vector2 canvasCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);

        float x = (mFieldSize.x - 1f) / 2f;
        float y = (mFieldSize.y - 1f) / 2f;
        Vector2 leftUnderTilePos = canvasCenter - new Vector2(x, y) * 100f;

        mTiles = new Tile[mFieldSize.x * mFieldSize.y];

        Debug.Log("fieldX: " + mFieldSize.x + "fieldY: " + mFieldSize.y);

        for (int i = 0; i < mTiles.Length; i++)
        {
            Vector2 thisTilePos = leftUnderTilePos + new Vector2(i % mFieldSize.x, i / mFieldSize.x) * 100f;

            Tile tile = Instantiate(mTilePrefab, mTilesRoot.transform);
            tile.GetComponent<RectTransform>().position = thisTilePos;

            tile.Initialize(this, i);
            mTiles[i] = tile;
        }
    }

    // タイルをすべて消す関数
    void EraseTile()
    {
        // this.transformにあるチャイルドゲームオブジェクトをすべて削除する

    }

    //地雷の設置
    void SetBooms()
    {
        List<Tile> safeTiles = new List<Tile>(mTiles.Length);
        // mTilesの要素をsafeTilesに追加
        safeTiles.AddRange(mTiles);

        mBoomTiles = new Tile[mTotalBoomCount];

        for (int i = 0; i < mTotalBoomCount; i++)
        {
            int Index = Random.Range(0, safeTiles.Count);
            Debug.Log("爆誕を生成するインデックス: " +  Index);
            Debug.Log("safeTilesのサイズ: " + safeTiles.Count);

            // 爆弾を置くタイルを取得
            Tile boomTile = safeTiles[Index];

            mBoomTiles[i] = boomTile;

            // 爆弾を設置するのでsafeTilesから削除
            safeTiles.RemoveAt(Index);

            // 爆弾を設置
            boomTile.SetBoom();
        }
    }

    void SetCounts()
    {
        Debug.Log(mTiles);
        Debug.Log(mTiles.Length);
        int[] boomCountEach = new int[mTiles.Length];

        foreach (var tile in mBoomTiles)
        {
            int[] aroundTileIndices = GetAroundIndices(tile.Index);

            foreach (var aroundIndex in aroundTileIndices)
            {
                boomCountEach[aroundIndex] = boomCountEach[aroundIndex] + 1;

            }
        }

        for(int i = 0; i < boomCountEach.Length; i++)
        {
            int boomCount = boomCountEach[i];
            if(boomCount != 0 && mTiles[i].TileType != TileType.BOOM)
                mTiles[i].SetCount(boomCount);
        }
    }

    //緑のタイルを開く
    public void DigAround(int index)
    {
        foreach(var aroundTileIndex in GetAroundIndices(index))
            mTiles[aroundTileIndex].OnDigged(); 
    }

     int[] GetAroundIndices(int index)
    {
        List<int> result = new List<int>(8);

        int index0 = index - mFieldSize.x - 1;
        if (index0 >= 0 && index0 % mFieldSize.x != mFieldSize.x - 1)
            result.Add(index0);

        int index1 = index - mFieldSize.x;
        if (index1 >= 0)
            result.Add(index1);

        int index2 = index - mFieldSize.x + 1;
        if (index2 >= 0 && index2 % mFieldSize.x != 0)
            result.Add(index2);

        int index3 = index - 1;
        if (index3 >= 0 && index3 % mFieldSize.x != mFieldSize.x - 1)
            result.Add(index3);

        int index4 = index + 1;
        if (index4 % mFieldSize.x != 0)
            result.Add(index4);

        int index5 = index + mFieldSize.x - 1;
        if (index5 < mTiles.Length && index5 % mFieldSize.x != mFieldSize.x - 1)
            result.Add(index5);

        int index6 = index + mFieldSize.x;
        if (index6 < mTiles.Length)
            result.Add(index6);

        int index7 = index + mFieldSize.x + 1;
        if (index7 < mTiles.Length && index7 % mFieldSize.x != 0)
            result.Add(index7);

        return result.ToArray();
    }

    //ゲームオーバー
    public void GameOver()
    {
        mGameOverImage.SetActive(true);
    }

    public void CountDiggedTile()
    {
        mDiggedTileCount++;
        if (mDiggedTileCount == mFieldSize.x * mFieldSize.y - mTotalBoomCount)
            GameClear();
    }

    //ゲームクリア
    void GameClear()
    {
        mGameClearTile.SetActive(true);
    }
}
