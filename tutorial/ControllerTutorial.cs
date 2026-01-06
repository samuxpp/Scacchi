using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
public class ControllerTutorial : MonoBehaviour
{
    private StartingPositions StartingPositions;
    private CalculateMoves CalculateMoves;
    private BoardController BoardController;
    private MovingAnimation MovingAnimation;
    public PieceState emptyState = new PieceState();
    public PieceState newState = new PieceState();
    public GameObject sideText;
    public GameObject frontText;
    public GameObject continua;
    private bool ready = false;
    public void Awake()
    {
        ready = false;
        StartingPositions = GetComponent<StartingPositions>();
        BoardController = GetComponent<BoardController>();
        CalculateMoves = GetComponent<CalculateMoves>();
        MovingAnimation = GetComponent<MovingAnimation>();
    }
    void Start()
    {
        StartCoroutine(SetUpBoardRoutine());
        StartCoroutine(TUT());
    }

    public void Continua()
    {
        ready = true;
    }

    private IEnumerator TUT()
    {
        BoardController.blackTurn = false;
        sideText.SetActive(true);
        frontText.SetActive(false);
        continua.SetActive(false);
        while (!CalculateMoves.found)
        {
            yield return null;
        }
        sideText.GetComponent<TextMeshProUGUI>().text = "Per muovere il pedone clicca nella zona nera della casella in cui lo vuoi muovere.";
        CalculateMoves.foundBase = false;
        while (!CalculateMoves.foundBase)
        {
            yield return null;
        }
        yield return new WaitForSeconds(2.5f);
        if (BoardController.ChessBoardState[2, 4].piece != null)
        {
            BoardController.ChessBoardState[3, 3] = BoardController.ChessBoardState[6, 3];
            MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[6, 3].piece, BoardController.gridPositions[6, 3] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[3, 3]);
        }
        else if (BoardController.ChessBoardState[3, 4].piece != null)
        {
            BoardController.ChessBoardState[4, 3] = BoardController.ChessBoardState[6, 3];
            MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[6, 3].piece, BoardController.gridPositions[6, 3] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[4, 3]);
        }
        sideText.GetComponent<TextMeshProUGUI>().text = "Il pedone può catturare qualsiasi pezzo ma può farlo soltanto in diagonale. Clicca sul pedone";
        CalculateMoves.found = false;
        while (!CalculateMoves.found)
        {
            yield return null;
        }
        sideText.GetComponent<TextMeshProUGUI>().text = "Clicca sul pezzo che vuoi cattuare.";
        CalculateMoves.foundCapture = false;
        while (!CalculateMoves.foundCapture)
        {
            yield return null;
        }
        sideText.GetComponent<TextMeshProUGUI>().text = "";
        yield return new WaitForSeconds(3f);
        frontText.SetActive(true);
        yield return new WaitForSeconds(1f);
        continua.SetActive(true);
        while (ready == false)
        {
            yield return null;
        }
        //////////////////////////////////////////////////////////////////bishop
        for (int i = 0; i < 7; i++)
        {
            StartingPositions.black_ponds[i].SetActive(false);
            StartingPositions.white_ponds[i].SetActive(false);    
        }
        StartingPositions.black_queen.SetActive(true);
        frontText.SetActive(false);
        continua.SetActive(false);
        newState = BoardController.ChessBoardState[0, 2];
        CleanBoard();    

        StartingPositions.white_bishops[0].SetActive(true);
        BoardController.ChessBoardState[0, 2] = newState;
        for (int i = 0; i < StartingPositions.bases.GetLength(0); ++i)
        {
            for (int j = 0; j < StartingPositions.bases.GetLength(1); ++j)
            {
                StartingPositions.bases[i, j].SetActive(false);
                StartingPositions.emptyBases[i, j].SetActive(false);
            }
        }
        sideText.GetComponent<TextMeshProUGUI>().text = "L'alfiere si muove solo in diagonale in ambedue le direzioni. Clicca sull'alfiere.";
        CalculateMoves.found = false;
        while (!CalculateMoves.found)
        {
            yield return null;
        }
        sideText.GetComponent<TextMeshProUGUI>().text = "Scegli dove muovere l'alfiere.";
        CalculateMoves.foundCapture = false;
        while (!CalculateMoves.foundBase)
        {
            yield return null;
        }
        BoardController.ChessBoardState[7, 3] = new PieceState{
                piece = StartingPositions.black_queen,
                pieceType = PieceType.queen,
                postion = new Vector2Int (7, 3),
                isWhite = false
        };
        yield return new WaitForSeconds(2.5f);
        sideText.GetComponent<TextMeshProUGUI>().text = "Anche l'alfiere come tutti i pezzi può cattuare, prova a cattuare la donna.";
        if (BoardController.ChessBoardState[1, 1].piece != null)
        {
            BoardController.ChessBoardState[5, 5] = BoardController.ChessBoardState[7, 3];
            MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 3].piece, BoardController.gridPositions[7, 3] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[5, 5]);
        }
        else if (BoardController.ChessBoardState[1, 3].piece != null)
        {
            BoardController.ChessBoardState[4, 6] = BoardController.ChessBoardState[7, 3];
            MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 3].piece, BoardController.gridPositions[7, 3] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[4, 6]);
        }
        else if (BoardController.ChessBoardState[2, 0].piece != null)
        {
            BoardController.ChessBoardState[6, 4] = BoardController.ChessBoardState[7, 3];
            MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 3].piece, BoardController.gridPositions[7, 3] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[6, 4]);
        }
        else if (BoardController.ChessBoardState[2, 4].piece != null)
        {
            BoardController.ChessBoardState[5, 1] = BoardController.ChessBoardState[7, 3];
            MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 3].piece, BoardController.gridPositions[7, 3] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[5, 1]);
        }
        else if (BoardController.ChessBoardState[3, 5].piece != null)
        {
            BoardController.ChessBoardState[6, 2] = BoardController.ChessBoardState[7, 3];
            MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 3].piece, BoardController.gridPositions[7, 3] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[6, 2]);
        }
        else if (BoardController.ChessBoardState[4, 6].piece != null)
        {
            BoardController.ChessBoardState[1, 3] = BoardController.ChessBoardState[7, 3];
            MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 3].piece, BoardController.gridPositions[7, 3] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[1, 3]);
        }
        else if (BoardController.ChessBoardState[5, 7].piece != null)
        {
            BoardController.ChessBoardState[1, 3] = BoardController.ChessBoardState[7, 3];
            MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 3].piece, BoardController.gridPositions[7, 3] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[1, 3]);
        }
        CalculateMoves.foundCapture = false;
        while (!CalculateMoves.foundBase)
        {
            yield return null;
        }
        sideText.GetComponent<TextMeshProUGUI>().text = "";
        yield return new WaitForSeconds(3f);
        frontText.SetActive(true);
        yield return new WaitForSeconds(1f);
        continua.SetActive(true);
        ready = false;
        while (ready == false)
        {
            yield return null;
        }
        //////////////////////////////////////////////////////////////////knight
    }




    private void CleanBoard()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                BoardController.ChessBoardState[i, j] = emptyState;
            }
        }
    }
    private IEnumerator SetUpBoardRoutine()
    {
        for (int i = 0; i < 8; i++)
        {
            if (i != 3)
            {
                StartingPositions.black_ponds[i].SetActive(false);
            }       
            if (i != 4)
            {
                StartingPositions.white_ponds[i].SetActive(false);
            }  
        }
        StartingPositions.white_queen.SetActive(false);
        StartingPositions.black_queen.SetActive(false);
        StartingPositions.white_king.SetActive(false);
        StartingPositions.black_king.SetActive(false);
        for (int i = 0; i < 2; i++)
        {
            StartingPositions.white_rooks[i].SetActive(false);
            StartingPositions.black_rooks[i].SetActive(false);
            StartingPositions.white_knights[i].SetActive(false);
            StartingPositions.black_knights[i].SetActive(false);
            StartingPositions.white_bishops[i].SetActive(false);
            StartingPositions.black_bishops[i].SetActive(false);
        }
        yield break;
    }
}
