using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PieceInstance : MonoBehaviour
{
    [Header("正常")] public Texture normalTexture;
    [Header("高亮")] public Texture highlightTexture;
    [Header("按掉")] public Texture deadTexture;
    [HideInInspector] public int x;
    [HideInInspector] public int y;
    [HideInInspector] public PieceColor color;

    [SerializeField] GameController owner;

    public Material crackMaterial;
    Material material;
    bool IsPieceUp;
    BoardInstance board;



    public void Init(int x, int y, PieceColor color, bool isCrackPiece, BoardInstance board)
    {
        IsPieceUp = false;
        this.x = x;
        this.y = y;
        this.color = color;
        this.board = board;
        //TODO: Set Crack Piece Material
        if(GameManager.Instance.gamePlayMode.doUseCrack)
        {
            if (isCrackPiece)
            {
                GetComponent<Renderer>().material = crackMaterial;
                Debug.Log("isCrackPiece!!!! x: " + x + " y: " + y);
            }
            else
            {
                Debug.Log("NotCrackPiece!!!!");
            }
        }
        material = GetComponent<Renderer>().material;
        switch (color)
        {
            //case PieceColor.Black:
            //    material.SetColor("_Color", Color.black);
            //    break;
            //case PieceColor.Blue:
            //    material.SetColor("_Color", Color.blue);
            //    break;
            //case PieceColor.Green:
            //    material.SetColor("_Color", Color.green);
            //    break;
            //case PieceColor.Grey:
            //    material.SetColor("_Color", Color.grey);
            //    break;
            //case PieceColor.Orange:
            //    material.SetColor("_Color", Color.yellow);
            //    break;
            //case PieceColor.Red:
            //    material.SetColor("_Color", Color.red);
            //    break;
            //case PieceColor.White:
            //    material.SetColor("_Color", Color.white);
            //    break;
            //default:
            //    break;
        }
        material.SetTexture("_MainTex", normalTexture);
    }
    // Start is called before the first frame update
    public IEnumerator CubeMove(float speed, int yPos)
    {
        if (Mathf.Abs(transform.position.y - yPos) <= 0.1f)
        {
            if (yPos < 0)
                Destroy(gameObject);
            transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
            yield return null;
        }
        else
        {
            transform.position += new Vector3(0, speed, 0);
            yield return new WaitForEndOfFrame();
            StartCoroutine(CubeMove(speed, yPos));
        }

    }
    public void GoUp()
    {
        if (!board.boardManager.pieces[x, y].isValid)
            return;
        material.SetTexture("_MainTex", highlightTexture);
        IsPieceUp = true;
    }
    public void GoBack()
    {
        //transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        IsPieceUp = false;
    }
    public void GoDown()
    {
        owner = GameManager.Instance.currentController;
        //if(GameManager.Instance.gamePlayMode.gameMode == GameMode.OneClientTwoPlayers)
        //{
        //    //不管玩家是谁永远显示获取得分是自己的
        //    effectTmp.SetActive(false);
        //    Color c = material.GetColor("_Color");
        //    c.a = 0.3f;
        //    material.SetColor("_Color", c);
        //    board.boardManager.pieces[x, y].isValid = false;
        //}
        //else if(GameManager.Instance.gamePlayMode.gameMode == GameMode.VSAI)
        //{
        UpdatePiece();
        board.boardManager.pieces[x, y].isValid = false;
        //}
    }
    public void StartDisappear()
    {
        StartCoroutine(Disappear());
    }
    IEnumerator Disappear()
    {
        while(material.GetColor("_Color").a > 0)
        {
            Color c = material.GetColor("_Color");
            c.a -=  Time.fixedDeltaTime;
            material.SetColor("_Color", c);
            yield return new WaitForEndOfFrame();
        }
    }
    void OnMouseUp()
    {
        if (GameManager.Instance.gamePlayMode.gameMode == GameMode.OneClientTwoPlayers)
        {
            SelectAndDropMe();
        }
        else if (GameManager.Instance.gamePlayMode.gameMode == GameMode.VSAI)
        {
            if (GameManager.Instance.currentController == GameManager.Instance.controller)
            {
                SelectAndDropMe();
            }
        }
    }
    public void SelectAndDropMe()
    {
        if (GameManager.Instance.IsGameEnded())
            return;
        if (!board.boardManager.pieces[x, y].isValid)
            return;
        if (IsPieceUp && board.boardManager.TrySelectFirstColor(color))
        {
            DropPiece();
        }
    }

    public void DropPiece()
    {
        GoDown();
        board.boardManager.PieceBeKilled(x, y);
    }

    public void UpdatePiece()
    {
        if (owner == null)
        {
            return;
        }
        if (GameManager.Instance.gamePlayMode.gameMode == GameMode.OneClientTwoPlayers)
        {
            Color c = material.GetColor("_Color");
            c.a = 0.3f;
            material.SetColor("_Color", c);
            material.SetTexture("_MainTex", deadTexture);
        }
        else if (GameManager.Instance.gamePlayMode.gameMode == GameMode.VSAI)
        {
            Color c = material.GetColor("_Color");
            c.a = 0.3f;
            material.SetColor("_Color", c);
            material.SetTexture("_MainTex", deadTexture);
        }
    }
}
