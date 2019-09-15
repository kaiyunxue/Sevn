using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceInstance : MonoBehaviour
{
    public int x;
    public int y;
    public PieceColor color;
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
        if (isCrackPiece)
        {
            GetComponent<Renderer>().material = crackMaterial;
            Debug.Log("isCrackPiece!!!! x: " + x + " y: " + y);
        }
        else
        {
            Debug.Log("NotCrackPiece!!!!");
        }
        material = GetComponent<Renderer>().material;
        switch (color)
        {
            case PieceColor.Black:
                material.SetColor("_Color", Color.black);
                break;
            case PieceColor.Blue:
                material.SetColor("_Color", Color.blue);
                break;
            case PieceColor.Green:
                material.SetColor("_Color", Color.green);
                break;
            case PieceColor.Grey:
                material.SetColor("_Color", Color.grey);
                break;
            case PieceColor.Orange:
                material.SetColor("_Color", Color.yellow);
                break;
            case PieceColor.Red:
                material.SetColor("_Color", Color.red);
                break;
            case PieceColor.White:
                material.SetColor("_Color", Color.white);
                break;
            default:
                break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }
    public void MoveTo(int yPos)
    {
        if (Mathf.Abs(yPos - transform.position.y) <= 0.1f)
            return;
        StartCoroutine(CubeMove(yPos * Time.deltaTime, yPos));
        GetComponent<AudioSource>().Play();
    }
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
        //transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        MoveTo(1);
        IsPieceUp = true;
    }
    public void GoBack()
    {
        //transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        MoveTo(0);
        IsPieceUp = false;
    }
    public void GoDown()
    {
        //todo
        board.boardManager.DeletePiece(x, y);
        MoveTo(-99);
        //Destroy(gameObject);
    }
    void OnMouseUp()
    {
        if (!board.boardManager.pieces[x, y].isValid)
            return;
        //Debug.Log(x + "," + y);
        //todo
        if (IsPieceUp && board.boardManager.TrySelectFirstColor(color))
        {
            GoDown();
            board.boardManager.PieceBeKilled(x, y);
        }
    }
}
