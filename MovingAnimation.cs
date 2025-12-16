using System.Collections;
using UnityEngine;

public class MovingAnimation : MonoBehaviour
{
    private BoardController BoardController;
    public float moveDuration = 0.3f;
    public float heightOffset = 3.0f;
    public GameObject pieceToMove = null;

    private void Awake()
    {
        BoardController = GetComponent<BoardController>();
    }

    public void AnimateAndMovePiece(GameObject pieceObj, Vector3 startPos, Vector3 endPos)
    {
        pieceToMove = pieceObj;
        StartCoroutine(MovePieceRoutine(startPos, endPos));
    }

    private IEnumerator MovePieceRoutine(Vector3 startPos, Vector3 endPos)
    {
        float elapsedTime;

        Transform pieceTransform = pieceToMove.transform;

        Vector3 startPeak = startPos;
        startPeak.y += heightOffset;

        Vector3 endPeak = endPos;
        endPeak.y += heightOffset;

        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / 1f);
            float currentY = Mathf.Lerp(startPos.y, startPeak.y, t);
            pieceTransform.position = new Vector3(startPos.x, currentY, startPos.z);
            yield return null;
        }
        pieceTransform.position = startPeak;
        elapsedTime = 0f;
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveDuration);
            float currentX = Mathf.Lerp(startPeak.x, endPeak.x, t);
            float currentZ = Mathf.Lerp(startPeak.z, endPeak.z, t);
            pieceTransform.position = new Vector3(currentX, startPeak.y, currentZ);
            yield return null;
        }
        pieceTransform.position = endPeak;
        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / 1f);
            float currentY = Mathf.Lerp(endPeak.y, endPos.y, t);
            pieceTransform.position = new Vector3(endPos.x, currentY, endPos.z);
            yield return null;
        }
        pieceTransform.position = endPos;
    }
}