using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardInstance : MonoBehaviour
{
    public BoardManager boardManager;
    public int sideLength;
    public PieceInstance[,] pieces;
    //[Header("Prefabs")]
    //[SerializeField] PieceInstance piecePrefab;
    public void Init(PrefabsConfig pConfig)
    {
        boardManager = GameManager.Instance.boardManager;
        sideLength = boardManager.boardLength;
        pieces = new PieceInstance[sideLength, sideLength];
        int halfLength = (sideLength - 1) / 2;
        for (int i = 0; i < sideLength; i++)
        {
            for (int j = 0; j < sideLength; j++)
            {
                int colorID = (int)boardManager.pieces[i, j].pieceColor;
                //pieces[i, j] = Instantiate(piecePrefab, transform);
                pieces[i, j] = Instantiate(pConfig.pieceInstancePrefabs[colorID], transform);
                pieces[i, j].name += ("(" + i + " , " + j + ")");
                pieces[i, j].transform.position += new Vector3((i - halfLength), 0, (j - halfLength));
                pieces[i, j].Init(i, j, boardManager.pieces[i, j].pieceColor, boardManager.pieces[i, j].isCrackPiece, this);
            }
        }

    }
    public void UpdateBoard()
    {
        foreach(var p in pieces)
        {
            p.UpdatePiece();
        }
    }
    public void BoardDisappear()
    {
        foreach(var p in pieces)
        {
            p.StartDisappear();
        }
    }
}
