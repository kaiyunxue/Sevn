using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachingSprite : MonoBehaviour
{
    public void DoJobs(TeachingJob[] jobs)
    {
        StartCoroutine(DoTheFuckingFobs(jobs));
    }
    IEnumerator DoTheFuckingFobs(TeachingJob[] jobs)
    {
        foreach (var j in jobs)
        {
            StartCoroutine(DoTheJobs(j));
            float wT = 0;
            foreach(var t in j.moveJobs)
            {
                wT += t.processTime;
            }
            yield return new WaitForSecondsRealtime(wT);
        }
    }
    IEnumerator DoTheJobs(TeachingJob job)
    {
        foreach(var j in job.moveJobs)
        {
            StartCoroutine(DoTheJob(j));
            yield return new WaitForSecondsRealtime(j.processTime);
        }
    }
    IEnumerator DoTheJob(SpriteMoveJob job)
    {
        Vector3 aimPos = transform.position;
        switch(job.moveType)
        {
            case SpriteMoveType.CrackPiece:
                //todo
                break;
            case SpriteMoveType.Stand:
                break;
            case SpriteMoveType.StandUpPiece:
                Piece p = GameManager.Instance.boardManager.nextPieces[job.pieceIdx];
                aimPos = GameManager.Instance.boardInstance.pieces[p.x, p.y].transform.position;
                break;
            case SpriteMoveType.Position:
                aimPos = job.MoveToPosition;
                break;
            case SpriteMoveType.UI:
                //todo
                break;
            default:
                break;
        }
        aimPos.y = transform.position.y;
        float len = Vector3.Distance(transform.position, aimPos);
        Vector3 speed = (aimPos - transform.position) / job.processTime * Time.fixedDeltaTime;
        float time = 0;
        while(time < job.processTime)
        {
            transform.position += speed;
            yield return new WaitForFixedUpdate();
            time += Time.fixedDeltaTime;
        }
    }
}
