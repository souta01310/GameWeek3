using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;


public enum TileType { EMPTY, BOOM, COUNT}

public class Tile : MonoBehaviour
{

    enum MarkState {  NO_MARK, FLAG, QUESTION }

    public TileType TileType {  get { return mTileType; } }

    public int Index { get { return mIndex; } }

    GeneleteMainsweeper mGeneleteMainsweeper;
    TileType mTileType = TileType.EMPTY;
    MarkState mMarkState = MarkState.NO_MARK;
    int mIndex;
    bool mIDigged = false;

    [SerializeField] GameObject mCover;
    [SerializeField] GameObject mFlag;
    [SerializeField] GameObject mQuestion;
    [SerializeField] GameObject mRedBG;
    [SerializeField] GameObject mRedCross;
    [SerializeField] Text mCount;
    [SerializeField] GameObject mBoom;
    [SerializeField]Color[] mCountCols = new Color[8];

    public void Initialize(GeneleteMainsweeper geneleteMainsweeper, int index)
    {
        mGeneleteMainsweeper = geneleteMainsweeper;
        mIndex = index;
    }

    //タイルを掘る
    public void OnDigged()
    {
        if (mIDigged || mMarkState == MarkState.FLAG)
            return;

        if (mMarkState == MarkState.QUESTION)
            mQuestion.SetActive(false);

        mIDigged = true;

        mCover.SetActive(false);

        switch (mTileType)
        {
            case TileType.EMPTY:
                //マネージャーに隣接タイルを掘ってもらう
                mGeneleteMainsweeper.DigAround(mIndex);
                mGeneleteMainsweeper.CountDiggedTile();
                break;
            case TileType.BOOM:
                //マネージャーにゲームオーバー処理をしてもらう
                mRedBG.SetActive(true);
                mGeneleteMainsweeper.GameOver();
                break;
            case TileType.COUNT:
                mGeneleteMainsweeper.CountDiggedTile();
                break;
        }
    }

    //周りの地雷数を表示
    public void SetCount(int count)
    {
        mTileType = TileType.COUNT;

        //カウントを設定して表示
        mCount.gameObject.SetActive(true);
        mCount.text = count.ToString();

        //数字によって文字の色を変える
        mCount.color = mCountCols[count - 1];
    }

    public void SetBoom()
    {
        mTileType= TileType.BOOM;
        mBoom.SetActive(true);
    }

    //マークを付ける
    public void SetMark()
    {
        if (mIDigged)
            return;

        switch (mMarkState)
        {
            case MarkState.NO_MARK:
                mFlag.SetActive(true);
                break;
            case MarkState.FLAG:
                mFlag.SetActive(false);
                mQuestion.SetActive(true);
                break;
            case MarkState.QUESTION:
                mQuestion.SetActive(false);
                break;
            default:
                break;
        }
        mMarkState++;
        int markStateLength = System.Enum.GetNames(typeof(MarkState)).Length;
        if((int)mMarkState == markStateLength)
            mMarkState -= markStateLength;

    }
}
