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
    public List<(int X, int Y)> legalCaptures = new List<(int X, int Y)>();
    void CalculateMoves()
    {
        legalMoves.Clear();
        legalCaptures.Clear();
        for (int i = 0; i < gridPositions.GetLength(0); i++)
        {
            for (int j = 0; j < gridPositions.GetLength(1); j++)
            {
                if (ChessBoardState[i, j].piece == clickedobject)
                {
                    if (ChessBoardState[i, j].pieceType == PieceType.pawn)     //pawn
                    {
                        if (ChessBoardState[i, j].isWhite == true)
                        {
                            if (i + 1 <= 7)
                            {
                                if (ChessBoardState[i + 1, j].piece == null)
                                {
                                    legalMoves.Add((i + 1, j));
                                }
                                if (ChessBoardState[i + 2, j].piece == null && i == 1)
                                {
                                    legalMoves.Add((i + 2, j));
                                }
                                
                                if (j - 1 >= 0)
                                {
                                    if (ChessBoardState[i + 1, j - 1].piece != null && ChessBoardState[i + 1, j - 1].isWhite == false)
                                    {
                                        legalCaptures.Add((i + 1, j - 1));
                                    }
                                }
                                if (j + 1 <= 7)
                                {
                                    if (ChessBoardState[i + 1, j + 1].piece != null && ChessBoardState[i + 1, j - 1].isWhite == false)
                                    {
                                        legalCaptures.Add((i + 1, j + 1));
                                    }
                                }
                            }
                        }
                        else if (ChessBoardState[i, j].isWhite == false)
                        {
                            if (i - 1 >= 0)
                            {
                                if (ChessBoardState[i - 1, j].piece == null)
                                {
                                    legalMoves.Add((i - 1, j));
                                }
                                if (ChessBoardState[i - 2, j].piece == null && i == 6)
                                {
                                    legalMoves.Add((i - 2, j));
                                }
                                if (j - 1 >= 0)
                                {
                                    if (ChessBoardState[i - 1, j - 1].piece != null && ChessBoardState[i + 1, j - 1].isWhite == true)
                                    {
                                        legalCaptures.Add((i - 1, j - 1));
                                    }
                                }
                                if (j + 1 <= 7)
                                {
                                    if (ChessBoardState[i - 1, j + 1].piece != null && ChessBoardState[i + 1, j - 1].isWhite == true)
                                    {
                                        legalCaptures.Add((i - 1, j + 1));
                                    }
                                }
                            }
                        }
                    }
                    else if (ChessBoardState[i, j].pieceType == PieceType.rook)    //rook
                    {
                        for (int k = 0; k < 7; k++)
                        {
                            if (i + k + 1 <= 7 && ChessBoardState[i + k + 1, j].piece == null)
                            {
                                legalMoves.Add((i + k + 1, j));
                            }
                            else if (i + k + 1 <= 7 && ChessBoardState[i + k + 1, j].piece != null && ChessBoardState[i + k + 1, j].isWhite == false && ChessBoardState[i, j].isWhite == true)
                            {
                                legalCaptures.Add((i + k + 1, j));
                                break;
                            }
                            else if (i + k + 1 <= 7 && ChessBoardState[i + k + 1, j].piece != null && ChessBoardState[i + k + 1, j].isWhite == true && ChessBoardState[i, j].isWhite == false)
                            {
                                legalCaptures.Add((i + k + 1, j));
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        for (int k = 0; k < 7; k++)
                        {
                            if (i - k - 1 >= 0 && ChessBoardState[i - k - 1, j].piece == null)
                            {
                                legalMoves.Add((i - k - 1, j));
                            }
                            else if (i - k - 1 >= 0 && ChessBoardState[i - k - 1, j].piece != null && ChessBoardState[i - k - 1, j].isWhite == false && ChessBoardState[i, j].isWhite == true)
                            {
                                legalCaptures.Add((i - k - 1, j));
                                break;
                            }
                            else if (i - k - 1 >= 0 && ChessBoardState[i - k - 1, j].piece != null && ChessBoardState[i - k - 1, j].isWhite == true && ChessBoardState[i, j].isWhite == false)
                            {
                                legalCaptures.Add((i - k - 1, j));
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        for (int k = 0; k < 7; k++)
                        {
                            if (j + k + 1 <= 7 && ChessBoardState[i, j + k + 1].piece == null)
                            {
                                legalMoves.Add((i, j + k + 1));
                            }
                            else if (j + k + 1 <= 7 && ChessBoardState[i, j + k + 1].piece != null && ChessBoardState[i, j + k + 1].isWhite == false && ChessBoardState[i, j].isWhite == true)
                            {
                                legalCaptures.Add((i, j + k + 1));
                                break;
                            }
                            else if (j + k + 1 <= 7 && ChessBoardState[i, j + k + 1].piece != null && ChessBoardState[i, j + k + 1].isWhite == true && ChessBoardState[i, j].isWhite == false)
                            {
                                legalCaptures.Add((i, j + k + 1));
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        for (int k = 0; k < 7; k++)
                        {
                            if (j - k - 1 >= 0 && ChessBoardState[i, j - k - 1].piece == null)
                            {
                                legalMoves.Add((i, j - k - 1));
                            }
                            else if (j - k - 1 >= 0 && ChessBoardState[i, j - k - 1].piece != null && ChessBoardState[i, j - k - 1].isWhite == false && ChessBoardState[i, j].isWhite == true)
                            {
                                legalCaptures.Add((i, j - k - 1));
                                break;
                            }
                            else if (j - k - 1 >= 0 && ChessBoardState[i, j - k - 1].piece != null && ChessBoardState[i, j - k - 1].isWhite == true && ChessBoardState[i, j].isWhite == false)
                            {
                                legalCaptures.Add((i, j - k - 1));
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                                            
                    }
                }
            }
        }
        Debug.Log("Legal Moves:");
        foreach (var move in legalMoves)
        {
            Debug.Log($"Mossa: {ChessNotation(move)}");
        }
        Debug.Log("Legal Captures:");
        foreach (var move in legalCaptures)
        {
            Debug.Log($"Mossa: {ChessNotation(move)}");
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


    private string ChessNotation((int X, int Y) coordinate)
    {
        char file = (char)('a' + coordinate.Y);
        int rank = coordinate.X + 1;
        return $"{file}{rank}";
    }
}