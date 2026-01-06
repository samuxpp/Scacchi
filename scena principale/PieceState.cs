using UnityEngine;

public struct PieceState
{
    public GameObject piece;
    public PieceType pieceType;
    public Vector2Int postion;
    public bool isWhite;
}

public enum PieceType
{
    empty,
    pawn,
    knight,
    bishop,
    rook,
    queen,
    king
}