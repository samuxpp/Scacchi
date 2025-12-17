using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class CalculateMoves : MonoBehaviour
{
    private BoardController BoardController;
    private MovingAnimation MovingAnimation;
    private StartingPositions StartingPositions;
    public List<(int X, int Y)> legalMoves = new();
    public List<(int X, int Y)> legalCaptures = new();
    public List<(int X, int Y)> savedCaptures = new();
    public List<(int X, int Y)> oldClickedPieceCaptures = new();
    public GameObject finalpos;
    public PieceState savedChessBoardState;
    public Vector3 savedGridPosition;
    public bool found = false;
    public PieceState emptyState = new PieceState();

    private void Awake()
    {
        StartingPositions = GetComponent<StartingPositions>();
        BoardController = GetComponent<BoardController>();
        MovingAnimation = GetComponent<MovingAnimation>();
    }


    private void Pawn(int i, int j)
    {
        if (BoardController.ChessBoardState[i, j].isWhite == true)
        {
            if (i + 1 <= 7)
            {
                if (BoardController.ChessBoardState[i + 1, j].piece == null)
                {
                    legalMoves.Add((i + 1, j));
                }
                if (BoardController.ChessBoardState[i + 1, j].piece == null && BoardController.ChessBoardState[i + 2, j].piece == null && i == 1)
                {
                    legalMoves.Add((i + 2, j));
                }

                if (j - 1 >= 0)
                {
                    if (BoardController.ChessBoardState[i + 1, j - 1].piece != null && BoardController.ChessBoardState[i + 1, j - 1].isWhite == false)
                    {
                        legalCaptures.Add((i + 1, j - 1));
                    }
                }
                if (j + 1 <= 7)
                {
                    if (BoardController.ChessBoardState[i + 1, j + 1].piece != null && BoardController.ChessBoardState[i + 1, j + 1].isWhite == false)
                    {
                        legalCaptures.Add((i + 1, j + 1));
                    }
                }
            }
        }
        else if (BoardController.ChessBoardState[i, j].isWhite == false)
        {
            if (i - 1 >= 0)
            {
                if (BoardController.ChessBoardState[i - 1, j].piece == null)
                {
                    legalMoves.Add((i - 1, j));
                }
                if (BoardController.ChessBoardState[i - 1, j].piece == null && BoardController.ChessBoardState[i - 2, j].piece == null && i == 6)
                {
                    legalMoves.Add((i - 2, j));
                }
                if (j - 1 >= 0)
                {
                    if (BoardController.ChessBoardState[i - 1, j - 1].piece != null && BoardController.ChessBoardState[i - 1, j - 1].isWhite == true)
                    {
                        legalCaptures.Add((i - 1, j - 1));
                    }
                }
                if (j + 1 <= 7)
                {
                    if (BoardController.ChessBoardState[i - 1, j + 1].piece != null && BoardController.ChessBoardState[i - 1, j + 1].isWhite == true)
                    {
                        legalCaptures.Add((i - 1, j + 1));
                    }
                }
            }
        }
    }
    private void Rook(int i, int j)
    {
        for (int k = 0; k < 7; k++)
        {
            if (i + k + 1 <= 7 && BoardController.ChessBoardState[i + k + 1, j].piece == null)
            {
                legalMoves.Add((i + k + 1, j));
            }
            else if (i + k + 1 <= 7 && BoardController.ChessBoardState[i + k + 1, j].piece != null && BoardController.ChessBoardState[i + k + 1, j].isWhite == false && BoardController.ChessBoardState[i, j].isWhite == true)
            {
                legalCaptures.Add((i + k + 1, j));
                break;
            }
            else if (i + k + 1 <= 7 && BoardController.ChessBoardState[i + k + 1, j].piece != null && BoardController.ChessBoardState[i + k + 1, j].isWhite == true && BoardController.ChessBoardState[i, j].isWhite == false)
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
            if (i - k - 1 >= 0 && BoardController.ChessBoardState[i - k - 1, j].piece == null)
            {
                legalMoves.Add((i - k - 1, j));
            }
            else if (i - k - 1 >= 0 && BoardController.ChessBoardState[i - k - 1, j].piece != null && BoardController.ChessBoardState[i - k - 1, j].isWhite == false && BoardController.ChessBoardState[i, j].isWhite == true)
            {
                legalCaptures.Add((i - k - 1, j));
                break;
            }
            else if (i - k - 1 >= 0 && BoardController.ChessBoardState[i - k - 1, j].piece != null && BoardController.ChessBoardState[i - k - 1, j].isWhite == true && BoardController.ChessBoardState[i, j].isWhite == false)
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
            if (j + k + 1 <= 7 && BoardController.ChessBoardState[i, j + k + 1].piece == null)
            {
                legalMoves.Add((i, j + k + 1));
            }
            else if (j + k + 1 <= 7 && BoardController.ChessBoardState[i, j + k + 1].piece != null && BoardController.ChessBoardState[i, j + k + 1].isWhite == false && BoardController.ChessBoardState[i, j].isWhite == true)
            {
                legalCaptures.Add((i, j + k + 1));
                break;
            }
            else if (j + k + 1 <= 7 && BoardController.ChessBoardState[i, j + k + 1].piece != null && BoardController.ChessBoardState[i, j + k + 1].isWhite == true && BoardController.ChessBoardState[i, j].isWhite == false)
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
            if (j - k - 1 >= 0 && BoardController.ChessBoardState[i, j - k - 1].piece == null)
            {
                legalMoves.Add((i, j - k - 1));
            }
            else if (j - k - 1 >= 0 && BoardController.ChessBoardState[i, j - k - 1].piece != null && BoardController.ChessBoardState[i, j - k - 1].isWhite == false && BoardController.ChessBoardState[i, j].isWhite == true)
            {
                legalCaptures.Add((i, j - k - 1));
                break;
            }
            else if (j - k - 1 >= 0 && BoardController.ChessBoardState[i, j - k - 1].piece != null && BoardController.ChessBoardState[i, j - k - 1].isWhite == true && BoardController.ChessBoardState[i, j].isWhite == false)
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
    private void Bishop(int i, int j)
    {
        for (int k = 0; k < 7; k++)
        {
            if (i + k + 1 <= 7 && j + k + 1 <= 7 && BoardController.ChessBoardState[i + k + 1, j + k + 1].piece == null)
            {
                legalMoves.Add((i + k + 1, j + k + 1));
            }
            else if (i + k + 1 <= 7 && j + k + 1 <= 7 && BoardController.ChessBoardState[i + k + 1, j + k + 1].piece != null && BoardController.ChessBoardState[i + k + 1, j + k + 1].isWhite == false && BoardController.ChessBoardState[i, j].isWhite == true)
            {
                legalCaptures.Add((i + k + 1, j + k + 1));
                break;
            }
            else if (i + k + 1 <= 7 && j + k + 1 <= 7 && BoardController.ChessBoardState[i + k + 1, j + k + 1].piece != null && BoardController.ChessBoardState[i + k + 1, j + k + 1].isWhite == true && BoardController.ChessBoardState[i, j].isWhite == false)
            {
                legalCaptures.Add((i + k + 1, j + k + 1));
                break;
            }
            else
            {
                break;
            }
        }
        for (int k = 0; k < 7; k++)
        {
            if (i + k + 1 <= 7 && j - k - 1 >= 0 && BoardController.ChessBoardState[i + k + 1, j - k - 1].piece == null)
            {
                legalMoves.Add((i + k + 1, j - k - 1));
            }
            else if (i + k + 1 <= 7 && j - k - 1 >= 0 && BoardController.ChessBoardState[i + k + 1, j - k - 1].piece != null && BoardController.ChessBoardState[i + k + 1, j - k - 1].isWhite == false && BoardController.ChessBoardState[i, j].isWhite == true)
            {
                legalCaptures.Add((i + k + 1, j - k - 1));
                break;
            }
            else if (i + k + 1 <= 7 && j - k - 1 >= 0 && BoardController.ChessBoardState[i + k + 1, j - k - 1].piece != null && BoardController.ChessBoardState[i + k + 1, j - k - 1].isWhite == true && BoardController.ChessBoardState[i, j].isWhite == false)
            {
                legalCaptures.Add((i + k + 1, j - k - 1));
                break;
            }
            else
            {
                break;
            }
        }
        for (int k = 0; k < 7; k++)
        {
            if (i - k - 1 >= 0 && j - k - 1 >= 0 && BoardController.ChessBoardState[i - k - 1, j - k - 1].piece == null)
            {
                legalMoves.Add((i - k - 1, j - k - 1));
            }
            else if (i - k - 1 >= 0 && j - k - 1 >= 0 && BoardController.ChessBoardState[i - k - 1, j - k - 1].piece != null && BoardController.ChessBoardState[i - k - 1, j - k - 1].isWhite == false && BoardController.ChessBoardState[i, j].isWhite == true)
            {
                legalCaptures.Add((i - k - 1, j - k - 1));
                break;
            }
            else if (i - k - 1 >= 0 && j - k - 1 >= 0 && BoardController.ChessBoardState[i - k - 1, j - k - 1].piece != null && BoardController.ChessBoardState[i - k - 1, j - k - 1].isWhite == true && BoardController.ChessBoardState[i, j].isWhite == false)
            {
                legalCaptures.Add((i - k - 1, j - k - 1));
                break;
            }
            else
            {
                break;
            }
        }
        for (int k = 0; k < 7; k++)
        {
            if (i - k - 1 >= 0 && j + k + 1 <= 7 && BoardController.ChessBoardState[i - k - 1, j + k + 1].piece == null)
            {
                legalMoves.Add((i - k - 1, j + k + 1));
            }
            else if (i - k - 1 >= 0 && j + k + 1 <= 7 && BoardController.ChessBoardState[i - k - 1, j + k + 1].piece != null && BoardController.ChessBoardState[i - k - 1, j + k + 1].isWhite == false && BoardController.ChessBoardState[i, j].isWhite == true)
            {
                legalCaptures.Add((i - k - 1, j + k + 1));
                break;
            }
            else if (i - k - 1 >= 0 && j + k + 1 <= 7 && BoardController.ChessBoardState[i - k - 1, j + k + 1].piece != null && BoardController.ChessBoardState[i - k - 1, j + k + 1].isWhite == true && BoardController.ChessBoardState[i, j].isWhite == false)
            {
                legalCaptures.Add((i - k - 1, j + k + 1));
                break;
            }
            else
            {
                break;
            }
        }
    }
    private void Knight(int i, int j)
    {
        int[] dx = { 1, 1, -1, -1, 2, 2, -2, -2 };
        int[] dy = { 2, -2, 2, -2, 1, -1, 1, -1 };

        for (int k = 0; k < 8; k++)
        {
            int new_i = i + dx[k];
            int new_j = j + dy[k];

            if (new_i >= 0 && new_i <= 7 && new_j >= 0 && new_j <= 7)
            {
                if (BoardController.ChessBoardState[new_i, new_j].piece == null)
                {
                    legalMoves.Add((new_i, new_j));
                }
                else if (BoardController.ChessBoardState[new_i, new_j].piece != null && BoardController.ChessBoardState[new_i, new_j].isWhite == false && BoardController.ChessBoardState[i, j].isWhite == true)
                {
                    legalCaptures.Add((new_i, new_j));
                }
                else if (BoardController.ChessBoardState[new_i, new_j].piece != null && BoardController.ChessBoardState[new_i, new_j].isWhite == true && BoardController.ChessBoardState[i, j].isWhite == false)
                {
                    legalCaptures.Add((new_i, new_j));
                }
            }
        }
    }
    private void King(int i, int j)
    {
        int[] dx = { 1, 1, 1, 0, 0, -1, -1, -1 };
        int[] dy = { 0, 1, -1, -1, 1, 0, 1, -1 };
        for (int k = 0; k < 8; k++)
        {
            int new_i = i + dx[k];
            int new_j = j + dy[k];
            if (new_i >= 0 && new_i <= 7 && new_j >= 0 && new_j <= 7)
            {
                if (BoardController.ChessBoardState[new_i, new_j].piece == null)
                {
                    legalMoves.Add((new_i, new_j));
                }
                else if (BoardController.ChessBoardState[new_i, new_j].piece != null && BoardController.ChessBoardState[new_i, new_j].isWhite == false && BoardController.ChessBoardState[i, j].isWhite == true)
                {
                    legalCaptures.Add((new_i, new_j));
                }
                else if (BoardController.ChessBoardState[new_i, new_j].piece != null && BoardController.ChessBoardState[new_i, new_j].isWhite == true && BoardController.ChessBoardState[i, j].isWhite == false)
                {
                    legalCaptures.Add((new_i, new_j));
                }
            }
        }
    }


    private string ChessNotation((int X, int Y) coordinate)
    {
        char file = (char)('a' + coordinate.Y);
        int rank = coordinate.X + 1;
        return $"{file}{rank}";
    }


    public void prova()
    {
        legalMoves.Clear();
        legalCaptures.Clear();
        found = false;
        for (int i = 0; i < BoardController.gridPositions.GetLength(0); i++)
        {
            for (int j = 0; j < BoardController.gridPositions.GetLength(1); j++)
            {
                if (BoardController.ChessBoardState[i, j].piece == BoardController.clickedobject && CheckCaptures(BoardController.clickedobject) == false)  //new
                {
                    ResetY();
                    BoardController.ChessBoardState[i, j].piece.transform.position += new Vector3(0f, 0.7f, 0f);
                    if (BoardController.ChessBoardState[i, j].pieceType == PieceType.pawn)     //pawn
                    {
                        Pawn(i, j);
                    }
                    else if (BoardController.ChessBoardState[i, j].pieceType == PieceType.rook)    //rook
                    {
                        Rook(i, j);
                    }
                    else if (BoardController.ChessBoardState[i, j].pieceType == PieceType.bishop)    //bishop
                    {
                        Bishop(i, j);
                    }
                    else if (BoardController.ChessBoardState[i, j].pieceType == PieceType.queen)    //queen
                    {
                        Rook(i, j);
                        Bishop(i, j);
                    }
                    else if (BoardController.ChessBoardState[i, j].pieceType == PieceType.knight)    //knight
                    {
                        Knight(i, j);
                    }
                    else if (BoardController.ChessBoardState[i, j].pieceType == PieceType.king)    //king
                    {
                        King(i, j);
                    }
                    savedCaptures = new List<(int X, int Y)>(legalCaptures);
                    savedChessBoardState = BoardController.ChessBoardState[i, j];
                    savedGridPosition = BoardController.gridPositions[i, j];
                    found = true;
                }
                else if (StartingPositions.bases[i, j] == BoardController.clickedobject)  //bases
                {
                    MovingAnimation.AnimateAndMovePiece(savedChessBoardState.piece, savedGridPosition + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[i, j]);
                    BoardController.ChessBoardState[i, j] = savedChessBoardState;
                    BoardController.ChessBoardState[i, j].postion = new Vector2Int(i, j);
                    BoardController.ChessBoardState[savedChessBoardState.postion.x, savedChessBoardState.postion.y] = emptyState;
                    found = true;
                }
                else if (CheckCapturesPos(BoardController.clickedobject, i, j))
                {
                    MovingAnimation.AnimateAndMovePiece(savedChessBoardState.piece, savedGridPosition + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[i, j]);
                    MovingAnimation.AnimateAndMovePiece(BoardController.clickedobject, BoardController.gridPositions[i, j], finalpos.transform.position);
                    BoardController.ChessBoardState[i, j] = savedChessBoardState;
                    BoardController.ChessBoardState[i, j].postion = new Vector2Int(i, j);
                    BoardController.ChessBoardState[savedChessBoardState.postion.x, savedChessBoardState.postion.y] = emptyState;
                    savedCaptures.Clear();
                    found = true;
                }
            }
        }
        StringBuilder legalMovesString = new();
        StringBuilder legalCapturesString = new();
        foreach (var move in legalMoves)
        {
            legalMovesString.Append(ChessNotation(move));
            legalMovesString.Append(", ");
        }
        foreach (var capture in legalCaptures)
        {
            legalCapturesString.Append(ChessNotation(capture));
            legalCapturesString.Append(", ");
        }
        //Debug.Log($"Legal Moves {legalMoves.Count}: {legalMovesString}");
        //Debug.Log($"Legal Captures {legalCaptures.Count}: {legalCapturesString}");
        if (found == false)
        {
            ResetY();
        }
    }
    bool CheckCaptures(GameObject target)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (BoardController.ChessBoardState[i, j].piece == target)
                {
                    return savedCaptures.Contains((i, j));
                }
            }
        }
        return false;
    }
    bool CheckCapturesPos(GameObject target, int i, int j)
    {
        if (BoardController.ChessBoardState[i, j].piece == null)
        {
            return false;
        }
        if (BoardController.ChessBoardState[i, j].piece == target)
        {
            return savedCaptures.Contains((i, j));
        }

        return false;
    }
    public void ResetY()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (BoardController.ChessBoardState[i, j].piece == null)
                    continue;
                Vector3 changedPos = BoardController.ChessBoardState[i, j].piece.transform.position;
                changedPos.y = 0.29f;
                BoardController.ChessBoardState[i, j].piece.transform.position = changedPos;
            }
        }
    }
}
