using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PieceInstance : MonoBehaviour
{
    [HideInInspector] public int x;
    [HideInInspector] public int y;
    [HideInInspector] public PieceColor color;

    [SerializeField] GameController owner;
    [SerializeField] Animator animator;
    [SerializeField] AudioClip audioPlayerClick;
    [SerializeField] AudioClip audioAIClick;

    public Material crackMaterial;
    Material material;
    bool IsPieceUp;
    BoardInstance board;
    public AudioSource audioSource;

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
        material.SetFloat("_Rate", 1f);
        //var sprite = GetComponent<SpriteRenderer>();
        //if (sprite != null)
        //{
        //    Debug.Log("normalSprite x: " + x + " y: " + y);
        //    sprite.sprite = normalSprite;
        //}
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
        if (animator != null)
        {
            animator.SetTrigger("activeTrigger");
        }
        material.SetFloat("_Rate", 0f);
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
        Debug.Log("GoDown()");
        if (GameManager.Instance.gamePlayMode.gameMode == GameMode.VSAI && GameManager.Instance.GetCurRound() % 2 != 0)
        {
            //仅在AI对战且在AI回合播放AI消除音效
            audioSource.clip = audioAIClick;
        }
        else
        {
            audioSource.clip = audioPlayerClick;
        }
        AudioSource.PlayClipAtPoint(audioSource.clip, new Vector3(0, 0, 0));
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
        if(!GameManager.Instance.IsTurnStart())
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
            gameObject.SetActive(false);
            //Color c = material.GetColor("_Color");
            //c.a = 0;
            //material.SetColor("_Color", c);
            //material.SetTexture("_MainTex", deadTexture);
        }
        else if (GameManager.Instance.gamePlayMode.gameMode == GameMode.VSAI)
        {
            gameObject.SetActive(false);
            //Color c = material.GetColor("_Color");
            //c.a = 0f;
            //material.SetColor("_Color", c);
            //material.SetTexture("_MainTex", deadTexture);
        }
    }
}
