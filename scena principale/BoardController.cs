using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

[ExecuteInEditMode]
public class BoardController : MonoBehaviour
{
	private StartingPositions StartingPositions;
    private CalculateMoves CalculateMoves;
    public PieceState[,] ChessBoardState = new PieceState[8, 8];
    private const int board_Size = 8;
    public GameObject visualBoardPlane;
    public Vector3[,] gridPositions = new Vector3[board_Size, board_Size];
    public GameObject clickedobject;
    public bool blackTurn = false;
    public bool enable = true;
    [HideInInspector] public bool isInitialized = false; //important

    private void Awake()
    {
		StartingPositions = GetComponent<StartingPositions>();
        CalculateMoves = GetComponent<CalculateMoves>();
    }

    public void Start()
    {
        GenerateLogicalGrid();
        StartingPositions.SetPositions();
        LogBoardState(); //important
        Screen.SetResolution(1920, 1080, true);
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
        if (enable)
        {
            if (context.performed)
            {
                clickedobject = HandleMouseClick();
                if (clickedobject != null)
                {
                    for (int i = 0; i < StartingPositions.bases.GetLength(0); ++i)
                    {
                        for (int j = 0; j < StartingPositions.bases.GetLength(1); ++j)
                        {
                            StartingPositions.bases[i, j].SetActive(false);
                            StartingPositions.emptyBases[i, j].SetActive(false);
                        }
                    }
                    CalculateMoves.Calculate();
                    ShowMoves();
                }
            }         
        }
    }
    public GameObject HandleMouseClick()
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

    public void ShowMoves()
    {
        for (int i = 0; i < StartingPositions.bases.GetLength(0); ++i)
        {
            for (int j = 0; j < StartingPositions.bases.GetLength(1); ++j)
            {
                (int X, int Y) pos = (i, j);
                if (CalculateMoves.legalMoves.Contains(pos) == true)
                {
                    StartingPositions.bases[i, j].SetActive(true);
                }
                if (CalculateMoves.legalCaptures.Contains(pos) == true)
                {
                    StartingPositions.emptyBases[i, j].SetActive(true);
                }
            }
        }
    }

    public void ResetChessBoard()
    {
        for (int i = 0; i < 8; i++)
        {
            StartingPositions.white_ponds[i].SetActive(true);
            StartingPositions.black_ponds[i].SetActive(true);
        }
        for (int i = 0; i < 2; i++)
        {
            StartingPositions.white_rooks[i].SetActive(true);
            StartingPositions.white_knights[i].SetActive(true);
            StartingPositions.white_bishops[i].SetActive(true);
            StartingPositions.black_rooks[i].SetActive(true);
            StartingPositions.black_knights[i].SetActive(true);
            StartingPositions.black_bishops[i].SetActive(true);
        }
        StartingPositions.white_queen.SetActive(true);
        StartingPositions.black_queen.SetActive(true);
        StartingPositions.white_king.SetActive(true);
        StartingPositions.black_king.SetActive(true);
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                ChessBoardState[i, j] = new PieceState();
            }
        }
        GenerateLogicalGrid();
        StartingPositions.SetPositions();
        for (int i = 0; i < 4; i++)
        {
            CalculateMoves.movedRooks[i] = false;
            CalculateMoves.movedKing[i / 2] = false;
        }
        LogBoardState();
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

            Vector3 startV = new(linePosition, yPos, -centerOffset);
            Vector3 endV = new(linePosition, yPos, 8 - centerOffset);
            Gizmos.DrawLine(startV, endV);

            Vector3 startH = new(-centerOffset, yPos, linePosition);
            Vector3 endH = new(8 - centerOffset, yPos, linePosition);
            Gizmos.DrawLine(startH, endH);
        }
    }


    public void LogBoardState()
    {
        PieceState[,] board = this.ChessBoardState;

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("=================================================");
        sb.AppendLine("       STATO ATTUALE DELLA SCACCHIERA (8x8)      ");
        sb.AppendLine("=================================================");
        sb.Append("Rank \\ File | ");
        for (int j = 0; j < 8; j++)
        {
            char file = (char)('a' + j);
            sb.Append($" {file} |");
        }
        sb.AppendLine();
        sb.AppendLine("-------------------------------------------------");

        for (int i = 7; i >= 0; i--)
        {
            int rank = i + 1;
            sb.Append($"    {rank}     | ");


            for (int j = 0; j < 8; j++)
            {
                PieceState currentState = board[i, j];

                string cellContent;

                if (currentState.piece == null)
                {
                    cellContent = " . ";
                }
                else
                {
                    char pieceChar = GetPieceChar(currentState.pieceType, currentState.isWhite);
                    cellContent = $" {pieceChar} ";
                }

                sb.Append(cellContent + "|");
            }
            sb.AppendLine();
        }
        sb.AppendLine("=================================================");

        Debug.Log(sb.ToString());
        isInitialized = true; //important
    }
    private char GetPieceChar(PieceType type, bool isWhite)
    {
        char pieceChar = ' ';

        switch (type)
        {
            case PieceType.pawn: pieceChar = isWhite ? 'P' : 'p'; break;
            case PieceType.knight: pieceChar = isWhite ? 'N' : 'n'; break;
            case PieceType.bishop: pieceChar = isWhite ? 'B' : 'b'; break;
            case PieceType.rook: pieceChar = isWhite ? 'R' : 'r'; break;
            case PieceType.queen: pieceChar = isWhite ? 'Q' : 'q'; break;
            case PieceType.king: pieceChar = isWhite ? 'K' : 'k'; break;
            default: break;
        }
        return pieceChar;
    }

}