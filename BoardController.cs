using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[ExecuteInEditMode]
public class BoardController : MonoBehaviour
{
	public StartingPositions StartingPositions;
	public PieceState[,] ChessBoardState = new PieceState[8, 8];
    private const int board_Size = 8;
    public GameObject visualBoardPlane;
    public Vector3[,] gridPositions = new Vector3[board_Size, board_Size];
    public GameObject clickedobject;

    private void Awake()
    {
		StartingPositions = GetComponent<StartingPositions>();
	}

    public void Start()
    {
        GenerateLogicalGrid();
        StartingPositions.SetPositions();
    }

    void GenerateLogicalGrid()
    {       
        Bounds bounds = visualBoardPlane.GetComponent<Renderer>().bounds;
        float boardWidth = bounds.size.x;
        float boardDepth = bounds.size.z;

        float squareSizeX = boardWidth / board_Size;
        float squareSizeZ = boardDepth / board_Size;

        Vector3 origin = bounds.min;

        for (int r = 0; r < board_Size; r++)
        {
            for (int c = 0; c < board_Size; c++)
            {
                float xPos = origin.x + (c * squareSizeX) + (squareSizeX / 2f);
                float yPos = bounds.max.y;
                float zPos = origin.z + (r * squareSizeZ) + (squareSizeZ / 2f);
                gridPositions[r, c] = new Vector3(xPos, yPos, zPos);
            }
        }
    }
    public void OnSelect(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            clickedobject = HandleMouseClick();
            if (clickedobject != null)
            {
                CalculateMoves();
            }
        }
    }
    private GameObject HandleMouseClick()
    {
        if (Mouse.current == null) return null;
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            GameObject clickedObject = hit.collider.gameObject;
            Debug.Log("Oggetto selezionato dal nuovo Input System: " + clickedObject.name);
            return hit.collider.gameObject;
        }
        return null;
    }




    public List<(int X, int Y)> legalMoves = new List<(int X, int Y)>();
    void CalculateMoves()
    {
        legalMoves.Clear();
        for (int i = 0; i < gridPositions.GetLength(0); i++)
        {
            for (int j = 0; j < gridPositions.GetLength(1); j++)
            {
                if (ChessBoardState[i, j].piece == clickedobject)
                {
                    if (ChessBoardState[i, j].pieceType == PieceType.pawn)
                    {
                        if (ChessBoardState[i, j].isWhite == true)
                        {
                            if (ChessBoardState[i + 1, j].piece == null)
                            {
                                legalMoves.Add((i + 1, j));
                            }
                            if (ChessBoardState[i + 2, j].piece == null && ChessBoardState[i, j].postion == new Vector2Int(1, j))
                            {
                                legalMoves.Add((i + 2, j));
                            }



                            foreach (var move in legalMoves)
                            {
                                Debug.Log($"   Riga: {move.X + 1}, Colonna: {move.Y + 1}");
                            }
                        }
                    }
                }
            }
        }
    }






    private void OnDrawGizmos()
    {
        Bounds bounds = visualBoardPlane.GetComponent<Renderer>().bounds;
        float yPos = bounds.max.y;
        float centerOffset = 8f / 2f;
        Gizmos.color = Color.yellow;

        if (gridPositions == null || gridPositions[0, 0] == Vector3.zero)
        {
            return;
        }

        for (int r = 0; r < 8; r++)
        {
            for (int c = 0; c < 8; c++)
            {
                Vector3 position = gridPositions[r, c];
                Gizmos.DrawSphere(position, 0.2f);
            }
        }

        for (int i = 0; i <= 8; i++)
        {
            float linePosition = (i * 1) - centerOffset;

            Vector3 startV = new Vector3(linePosition, yPos, -centerOffset);
            Vector3 endV = new Vector3(linePosition, yPos, 8 - centerOffset);
            Gizmos.DrawLine(startV, endV);

            Vector3 startH = new Vector3(-centerOffset, yPos, linePosition);
            Vector3 endH = new Vector3(8 - centerOffset, yPos, linePosition);
            Gizmos.DrawLine(startH, endH);
        }
    }
}