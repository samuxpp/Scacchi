using UnityEngine;
using UnityEngine.InputSystem;

public class Promotion : MonoBehaviour
{
    private BoardController BoardController;
    private CalculateMoves CalculateMoves;
    public GameObject pauseMenuUI;
    public GameObject white_queen;
    public GameObject black_queen;
    public GameObject white_knight;
    public GameObject black_knight;
    public GameObject white_rook;
    public GameObject black_rook;
    public GameObject white_bishop;
    public GameObject black_bishop;
    private int k = 0;
    private int l = 0;
    private bool isWhite;

    private void Start()
    {
        Resume();
    }
    private void Awake()
    {
        BoardController = GetComponent<BoardController>();
        CalculateMoves = GetComponent<CalculateMoves>();
    }

    public void Pause(int i, int j, bool white)
    {
        k = i;
        l = j;
        isWhite = white;
        pauseMenuUI.SetActive(true);      
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);    
        Time.timeScale = 1f;
    }
    public void Queen()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        if (isWhite)
        {
            GameObject whiteQueen = Instantiate(white_queen);
            whiteQueen.SetActive(true);
            whiteQueen.transform.SetParent(CalculateMoves.savedChessBoardState.piece.transform);
            whiteQueen.transform.localPosition = Vector3.zero;
            whiteQueen.name = $"Queen_{k}_{l}";
            BoardController.ChessBoardState[k, l].pieceType = PieceType.queen;
            GameObject targetPiece = BoardController.ChessBoardState[k, l].piece;
            BoardController.ChessBoardState[k, l].piece = whiteQueen;
            targetPiece.GetComponent<MeshRenderer>().enabled = false;
            targetPiece.GetComponent<Collider>().enabled = false;
        }
        else
        {
            GameObject blackQueen = Instantiate(black_queen);
            blackQueen.SetActive(true);
            blackQueen.transform.SetParent(CalculateMoves.savedChessBoardState.piece.transform);
            blackQueen.transform.localPosition = Vector3.zero;
            blackQueen.name = $"Queen_{k}_{l}";
            BoardController.ChessBoardState[k, l].pieceType = PieceType.queen;
            GameObject targetPiece = BoardController.ChessBoardState[k, l].piece;
            BoardController.ChessBoardState[k, l].piece = blackQueen;
            targetPiece.GetComponent<MeshRenderer>().enabled = false;
            targetPiece.GetComponent<Collider>().enabled = false;
        }
        CalculateMoves.foundPromotion = true;
    }
    public void Knight()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        if (isWhite)
        {
            GameObject whiteKnight = Instantiate(white_knight);
            whiteKnight.SetActive(true);
            whiteKnight.transform.SetParent(CalculateMoves.savedChessBoardState.piece.transform);
            whiteKnight.transform.localPosition = Vector3.zero;
            whiteKnight.name = $"Knight_{k}_{l}";
            BoardController.ChessBoardState[k, l].pieceType = PieceType.knight;
            GameObject targetPiece = BoardController.ChessBoardState[k, l].piece;
            BoardController.ChessBoardState[k, l].piece = whiteKnight;
            targetPiece.GetComponent<MeshRenderer>().enabled = false;
            targetPiece.GetComponent<Collider>().enabled = false;
        }
        else
        {
            GameObject blackKnight = Instantiate(black_knight);
            blackKnight.SetActive(true);
            blackKnight.transform.SetParent(CalculateMoves.savedChessBoardState.piece.transform);
            blackKnight.transform.localPosition = Vector3.zero;
            blackKnight.name = $"Knight_{k}_{l}";
            BoardController.ChessBoardState[k, l].pieceType = PieceType.knight;
            GameObject targetPiece = BoardController.ChessBoardState[k, l].piece;
            BoardController.ChessBoardState[k, l].piece = blackKnight;
            targetPiece.GetComponent<MeshRenderer>().enabled = false;
            targetPiece.GetComponent<Collider>().enabled = false;
        }
        CalculateMoves.foundPromotion = true;
    }
    public void Rook()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        if (isWhite)
        {
            GameObject whiteRook = Instantiate(white_rook);
            whiteRook.SetActive(true);
            whiteRook.transform.SetParent(CalculateMoves.savedChessBoardState.piece.transform);
            whiteRook.transform.localPosition = Vector3.zero;
            whiteRook.name = $"Rook_{k}_{l}";
            BoardController.ChessBoardState[k, l].pieceType = PieceType.rook;
            GameObject targetPiece = BoardController.ChessBoardState[k, l].piece;
            BoardController.ChessBoardState[k, l].piece = whiteRook;
            targetPiece.GetComponent<MeshRenderer>().enabled = false;
            targetPiece.GetComponent<Collider>().enabled = false;
        }
        else
        {
            GameObject blackRook = Instantiate(black_rook);
            blackRook.SetActive(true);
            blackRook.transform.SetParent(CalculateMoves.savedChessBoardState.piece.transform);
            blackRook.transform.localPosition = Vector3.zero;
            blackRook.name = $"Rook_{k}_{l}";
            BoardController.ChessBoardState[k, l].pieceType = PieceType.rook;
            GameObject targetPiece = BoardController.ChessBoardState[k, l].piece;
            BoardController.ChessBoardState[k, l].piece = blackRook;
            targetPiece.GetComponent<MeshRenderer>().enabled = false;
            targetPiece.GetComponent<Collider>().enabled = false;
        }
        CalculateMoves.foundPromotion = true;
    }
    public void Bishop()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        if (isWhite)
        {
            GameObject whiteBishop = Instantiate(white_bishop);
            whiteBishop.SetActive(true);
            whiteBishop.transform.SetParent(CalculateMoves.savedChessBoardState.piece.transform);
            whiteBishop.transform.localPosition = Vector3.zero;
            whiteBishop.name = $"Bishop_{k}_{l}";
            BoardController.ChessBoardState[k, l].pieceType = PieceType.bishop;
            GameObject targetPiece = BoardController.ChessBoardState[k, l].piece;
            BoardController.ChessBoardState[k, l].piece = whiteBishop;
            targetPiece.GetComponent<MeshRenderer>().enabled = false;
            targetPiece.GetComponent<Collider>().enabled = false;
        }
        else
        {
            GameObject blackBishop = Instantiate(black_bishop);
            blackBishop.SetActive(true);
            blackBishop.transform.SetParent(CalculateMoves.savedChessBoardState.piece.transform);
            blackBishop.transform.localPosition = Vector3.zero;
            blackBishop.name = $"Bishop_{k}_{l}";
            BoardController.ChessBoardState[k, l].pieceType = PieceType.bishop;
            GameObject targetPiece = BoardController.ChessBoardState[k, l].piece;
            BoardController.ChessBoardState[k, l].piece = blackBishop;
            targetPiece.GetComponent<MeshRenderer>().enabled = false;
            targetPiece.GetComponent<Collider>().enabled = false;
        }
        CalculateMoves.foundPromotion = true;
    }
}
