using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpriteMoveType
{
    Position,
    Stand,
    StandUpPiece,
    CrackPiece,
    UI
}
[System.Serializable]
public struct SpriteMoveJob
{
    public SpriteMoveType moveType;
    public Vector3 MoveToPosition;
    public int pieceIdx;
    public float processTime;
}
[System.Serializable]
public struct TeachingJob
{
    public SpriteMoveJob[] moveJobs;
    public string teachingWords;
}
public class TeachingManager : MonoBehaviour
{
    public TeachingJob[] teachingJobs;
    public TeachingSprite sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite.DoJobs(teachingJobs);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
