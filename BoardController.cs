using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardController : MonoBehaviour
{
    public void Update()
    {
        GenerateLogicalGrid();
    }


    private const int Board_Size = 8;
    public GameObject visualBoardPlane;
    private Vector3[,] gridPositions = new Vector3[Board_Size, Board_Size];
    void GenerateLogicalGrid()
    {       
        Bounds bounds = visualBoardPlane.GetComponent<Renderer>().bounds;
        float boardWidth = bounds.size.x;
        float boardDepth = bounds.size.z;

        float squareSizeX = boardWidth / Board_Size;
        float squareSizeZ = boardDepth / Board_Size;

        Vector3 origin = bounds.min;

        for (int r = 0; r < Board_Size; r++)
        {
            for (int c = 0; c < Board_Size; c++)
            {
                float xPos = origin.x + (c * squareSizeX) + (squareSizeX / 2f);
                float yPos = bounds.max.y + 0.05f;
                float zPos = origin.z + (r * squareSizeZ) + (squareSizeZ / 2f);
                gridPositions[r, c] = new Vector3(xPos, yPos, zPos);
            }
        }
    }


    private void OnDrawGizmos()
    {
        if (gridPositions == null || gridPositions[0, 0] == Vector3.zero)
        {
            return;
        }

        Gizmos.color = Color.yellow;

        for (int r = 0; r < 8; r++)
        {
            for (int c = 0; c < 8; c++)
            {
                Vector3 position = gridPositions[r, c];
                Gizmos.DrawSphere(position, 0.2f);
            }
        }
    }
}