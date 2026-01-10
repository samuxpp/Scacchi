using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControllerPuzzle : MonoBehaviour
{
    private CalculateMoves CalculateMoves;
    private BoardController BoardController;
    private StartingPositions StartingPositions;
    private MovingAnimation MovingAnimation;
    public PieceState emptyState = new PieceState();
    public PieceState selected = new PieceState();
    private bool ready = false;
    public GameObject sideText;
    public GameObject frontText;
    public GameObject continua;
    public GameObject panel;
    public GameObject menu;
    private Vector3 pos;
    private Camera mainCam;
    public GameObject mainCamera;
    public GameObject pivot;
    private Badge Badge;
    public Image puzzleImage;
    public Sprite puzzleSprite;
    public void Continue()
    {
        ready = true;
    }
    public void Awake()
    {
        ready = false;
        mainCam = Camera.main;
        StartingPositions = GetComponent<StartingPositions>();
        BoardController = GetComponent<BoardController>();
        CalculateMoves = GetComponent<CalculateMoves>();
        MovingAnimation = GetComponent<MovingAnimation>();
        Badge = GetComponent<Badge>();
    }
    private void Start()
    {
        StartCoroutine(Puzzle());
    }
    private IEnumerator Puzzle()
    {
        ////////////////////////////////////////////////////////////////////////////////first puzzle
        yield return new WaitUntil(() => BoardController.isInitialized == true);
        //white setup
        for (int i = 0; i < 4; i++)
        {
            StartingPositions.white_ponds[i].SetActive(false);
            StartingPositions.black_ponds[i].SetActive(false);
            BoardController.ChessBoardState[1, i] = emptyState;
            BoardController.ChessBoardState[6, i] = emptyState;
        }
        for (int i = 0; i < 8; i++)
        {
            BoardController.ChessBoardState[0, i] = emptyState;
            BoardController.ChessBoardState[7, i] = emptyState;
        }
        BoardController.ChessBoardState[0, 0] = new PieceState
        {
            piece = StartingPositions.white_rooks[0],
            pieceType = PieceType.rook,
            postion = new Vector2Int(0, 0),
            isWhite = true
        };
        StartingPositions.white_rooks[1].SetActive(false);
        StartingPositions.white_knights[1].SetActive(false);
        StartingPositions.white_bishops[0].transform.position = BoardController.gridPositions[1, 3];
        StartingPositions.white_bishops[0].transform.position = BoardController.gridPositions[2, 7];
        BoardController.ChessBoardState[1, 3] = new PieceState
        {
            piece = StartingPositions.white_bishops[0],
            pieceType = PieceType.bishop,
            postion = new Vector2Int(1, 3),
            isWhite = true
        };
        BoardController.ChessBoardState[2, 7] = new PieceState
        {
            piece = StartingPositions.white_bishops[1],
            pieceType = PieceType.bishop,
            postion = new Vector2Int(2, 7),
            isWhite = true
        };
        StartingPositions.white_knights[0].transform.position = BoardController.gridPositions[2, 6];
        BoardController.ChessBoardState[2, 6] = new PieceState
        {
            piece = StartingPositions.white_knights[0],
            pieceType = PieceType.knight,
            postion = new Vector2Int(2, 6),
            isWhite = true
        };
        StartingPositions.white_queen.SetActive(false);
        StartingPositions.white_king.transform.position = BoardController.gridPositions[0, 6];
        BoardController.ChessBoardState[0, 6] = new PieceState
        {
            piece = StartingPositions.white_king,
            pieceType = PieceType.king,
            postion = new Vector2Int(0, 6),
            isWhite = true
        };
        StartingPositions.white_ponds[3].transform.position = BoardController.gridPositions[3, 3];
        BoardController.ChessBoardState[3, 3] = new PieceState
        {
            piece = StartingPositions.white_ponds[3],
            pieceType = PieceType.pawn,
            postion = new Vector2Int(3, 3),
            isWhite = true
        };
        CalculateMoves.movedKing[0] = true;
        //black setup
        StartingPositions.black_rooks[0].SetActive(false);
        StartingPositions.black_rooks[1].SetActive(false);
        StartingPositions.black_knights[0].transform.position = BoardController.gridPositions[4, 3];
        BoardController.ChessBoardState[4, 3] = new PieceState
        {
            piece = StartingPositions.white_knights[0],
            pieceType = PieceType.knight,
            postion = new Vector2Int(4, 3),
            isWhite = false
        };
        StartingPositions.black_knights[1].transform.position = BoardController.gridPositions[5, 7];
        BoardController.ChessBoardState[5, 7] = new PieceState
        {
            piece = StartingPositions.white_knights[1],
            pieceType = PieceType.knight,
            postion = new Vector2Int(5, 7),
            isWhite = false
        };
        StartingPositions.black_bishops[0].transform.position = BoardController.gridPositions[4, 1];
        BoardController.ChessBoardState[4, 1] = new PieceState
        {
            piece = StartingPositions.black_bishops[0],
            pieceType = PieceType.bishop,
            postion = new Vector2Int(4, 1),
            isWhite = false
        };
        StartingPositions.black_bishops[1].SetActive(false);
        StartingPositions.black_queen.SetActive(false);
        StartingPositions.black_king.transform.position = BoardController.gridPositions[7, 6];
        BoardController.ChessBoardState[7, 6] = new PieceState
        {
            piece = StartingPositions.black_king,
            pieceType = PieceType.king,
            postion = new Vector2Int(7, 6),
            isWhite = false
        };
        StartingPositions.black_ponds[1].transform.position = BoardController.gridPositions[6, 1];
        BoardController.ChessBoardState[6, 1] = new PieceState
        {
            piece = StartingPositions.black_ponds[1],
            pieceType = PieceType.pawn,
            postion = new Vector2Int(6, 1),
            isWhite = false
        };
        StartingPositions.black_ponds[2].transform.position = BoardController.gridPositions[5, 2];
        BoardController.ChessBoardState[5, 2] = new PieceState
        {
            piece = StartingPositions.black_ponds[2],
            pieceType = PieceType.pawn,
            postion = new Vector2Int(5, 2),
            isWhite = false
        };
        StartingPositions.black_ponds[4].transform.position = BoardController.gridPositions[3, 4];
        BoardController.ChessBoardState[3, 4] = new PieceState
        {
            piece = StartingPositions.black_ponds[4],
            pieceType = PieceType.pawn,
            postion = new Vector2Int(3, 4),
            isWhite = false
        };
        BoardController.enable = false;
        sideText.GetComponent<TextMeshProUGUI>().text = "in questa sezione ci sono dei \"puzzle\" ovvero delle posizioni da risolvere per vincere la partita o acquisire un vantaggio";
        yield return new WaitForSeconds(1f);
        continua.SetActive(true);
        ready = false;
        while (ready == false)
        {
            yield return null;
        }
        continua.SetActive(false);
        BoardController.enable = true;
        sideText.GetComponent<TextMeshProUGUI>().text = "in questo puzzle c'è matto in 1 per il bianco, prova a risolverlo";
        bool loop = true;
        while (loop)
        {
            sideText.GetComponent<TextMeshProUGUI>().text = "in questo puzzle c'è matto in 1 per il bianco, prova a risolverlo";
            CalculateMoves.foundBase = false;
            CalculateMoves.foundCapture = false;
            while (!CalculateMoves.foundBase && !CalculateMoves.foundCapture)
            {
                yield return null;
            }
            BoardController.enable = false;
            if (CalculateMoves.foundCapture)
            {
                sideText.GetComponent<TextMeshProUGUI>().text = "mossa sbagliata. riprova";
                yield return new WaitForSeconds(3f);
                continua.SetActive(true);
                ready = false;
                while (ready == false)
                {
                    yield return null;
                }
                continua.SetActive(false);            
                MovingAnimation.AnimateAndMovePiece(CalculateMoves.oldClickedPiecePuzzle.piece, BoardController.gridPositions[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[CalculateMoves.oldClickedPiecePuzzle.postion.x, CalculateMoves.oldClickedPiecePuzzle.postion.y]);
                MovingAnimation.AnimateAndMovePiece(CalculateMoves.newClickedPiecePuzzle.piece, CalculateMoves.newClickedPiecePuzzle.piece.transform.position + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y]);
                BoardController.ChessBoardState[CalculateMoves.oldClickedPiecePuzzle.postion.x, CalculateMoves.oldClickedPiecePuzzle.postion.y] = CalculateMoves.oldClickedPiecePuzzle;
                BoardController.ChessBoardState[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y] = CalculateMoves.newClickedPiecePuzzle;
                yield return new WaitForSeconds(3f);
                BoardController.enable = true;
            }
            else if (CalculateMoves.foundBase)
            {
                if (BoardController.clickedobject == StartingPositions.bases[7, 0] && CalculateMoves.oldClickedPiecePuzzle.piece == StartingPositions.white_rooks[0])
                {
                    loop = false;
                }
                else
                {     
                    sideText.GetComponent<TextMeshProUGUI>().text = "mossa sbagliata. riprova";
                    yield return new WaitForSeconds(3f);
                    continua.SetActive(true);
                    ready = false;
                    while (ready == false)
                    {
                        yield return null;
                    }
                    continua.SetActive(false);                    
                    MovingAnimation.AnimateAndMovePiece(CalculateMoves.oldClickedPiecePuzzle.piece, BoardController.gridPositions[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[CalculateMoves.oldClickedPiecePuzzle.postion.x, CalculateMoves.oldClickedPiecePuzzle.postion.y]);
                    BoardController.ChessBoardState[CalculateMoves.oldClickedPiecePuzzle.postion.x, CalculateMoves.oldClickedPiecePuzzle.postion.y] = BoardController.ChessBoardState[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y];
                    BoardController.ChessBoardState[CalculateMoves.oldClickedPiecePuzzle.postion.x, CalculateMoves.oldClickedPiecePuzzle.postion.y].postion = CalculateMoves.oldClickedPiecePuzzle.postion;
                    BoardController.ChessBoardState[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y] = emptyState;
                    yield return new WaitForSeconds(3f);
                    BoardController.enable = true;
                }
            }
        }
        yield return new WaitForSeconds(3f);
        sideText.GetComponent<TextMeshProUGUI>().text = "Bravo! hai completato il primo puzzle clicca su continua per passare al secondo";
        yield return new WaitForSeconds(1f);
        continua.SetActive(true);
        ready = false;
        while (ready == false)
        {
            yield return null;
        }
        continua.SetActive(false);
        ////////////////////////////////////////////////////////////////////////////////secondo puzzle
        sideText.GetComponent<TextMeshProUGUI>().text = "un secondo stiamo preparando la scacchiera...";
        BoardController.ResetChessBoard();
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[1, 4].piece, BoardController.gridPositions[1, 4] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[3, 4]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[6, 4].piece, BoardController.gridPositions[6, 4] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[4, 4]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[0, 5].piece, BoardController.gridPositions[0, 5] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[3, 2]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[0, 3].piece, BoardController.gridPositions[0, 3] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[4, 7]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 1].piece, BoardController.gridPositions[7, 1] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[5, 2]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 6].piece, BoardController.gridPositions[7, 6] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[5, 5]); yield return new WaitForSeconds(1f);
        MovePieceLogic(new Vector2Int(1, 4), new Vector2Int(3, 4));
        MovePieceLogic(new Vector2Int(0, 5), new Vector2Int(3, 2));
        MovePieceLogic(new Vector2Int(0, 3), new Vector2Int(4, 7));
        MovePieceLogic(new Vector2Int(6, 4), new Vector2Int(4, 4));
        MovePieceLogic(new Vector2Int(1, 7), new Vector2Int(5, 2));
        MovePieceLogic(new Vector2Int(7, 6), new Vector2Int(5, 5));
        yield return new WaitForSeconds(3f);
        loop = true;
        BoardController.enable = true;
        while (loop)
        {
            sideText.GetComponent<TextMeshProUGUI>().text = "in questo puzzle c'è matto in 1 per il bianco, prova a risolverlo";
            CalculateMoves.foundBase = false;
            CalculateMoves.foundCapture = false;
            while (!CalculateMoves.foundBase && !CalculateMoves.foundCapture)
            {
                yield return null;
            }
            BoardController.enable = false;
            if (CalculateMoves.foundCapture)
            {
                if (CalculateMoves.oldClickedPiecePuzzle.piece == StartingPositions.white_queen && CalculateMoves.newClickedPiecePuzzle.piece == StartingPositions.black_ponds[5])
                {
                    loop = false;
                }
                else
                {
                    sideText.GetComponent<TextMeshProUGUI>().text = "mossa sbagliata. riprova";
                    yield return new WaitForSeconds(3f);
                    continua.SetActive(true);
                    ready = false;
                    while (ready == false)
                    {
                        yield return null;
                    }
                    continua.SetActive(false);
                    MovingAnimation.AnimateAndMovePiece(CalculateMoves.oldClickedPiecePuzzle.piece, BoardController.gridPositions[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[CalculateMoves.oldClickedPiecePuzzle.postion.x, CalculateMoves.oldClickedPiecePuzzle.postion.y]);
                    MovingAnimation.AnimateAndMovePiece(CalculateMoves.newClickedPiecePuzzle.piece, CalculateMoves.newClickedPiecePuzzle.piece.transform.position + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y]);
                    BoardController.ChessBoardState[CalculateMoves.oldClickedPiecePuzzle.postion.x, CalculateMoves.oldClickedPiecePuzzle.postion.y] = CalculateMoves.oldClickedPiecePuzzle;
                    BoardController.ChessBoardState[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y] = CalculateMoves.newClickedPiecePuzzle;
                    yield return new WaitForSeconds(3f);
                    BoardController.enable = true;
                }
            }
            else if (CalculateMoves.foundBase)
            {
                    sideText.GetComponent<TextMeshProUGUI>().text = "mossa sbagliata. riprova";
                    yield return new WaitForSeconds(3f);
                    continua.SetActive(true);
                    ready = false;
                    while (ready == false)
                    {
                        yield return null;
                    }
                    continua.SetActive(false);
                    MovingAnimation.AnimateAndMovePiece(CalculateMoves.oldClickedPiecePuzzle.piece, BoardController.gridPositions[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[CalculateMoves.oldClickedPiecePuzzle.postion.x, CalculateMoves.oldClickedPiecePuzzle.postion.y]);
                    BoardController.ChessBoardState[CalculateMoves.oldClickedPiecePuzzle.postion.x, CalculateMoves.oldClickedPiecePuzzle.postion.y] = BoardController.ChessBoardState[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y];
                    BoardController.ChessBoardState[CalculateMoves.oldClickedPiecePuzzle.postion.x, CalculateMoves.oldClickedPiecePuzzle.postion.y].postion = CalculateMoves.oldClickedPiecePuzzle.postion;
                    BoardController.ChessBoardState[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y] = emptyState;
                    yield return new WaitForSeconds(3f);
                    BoardController.enable = true;
            }
        }
        yield return new WaitForSeconds(3f);
        sideText.GetComponent<TextMeshProUGUI>().text = "Bravo! hai completato il secondo puzzle clicca su continua per passare al terzo";
        ////////////////////////////////////////////////////////////////////////////////third puzzle
        yield return new WaitForSeconds(1f);
        continua.SetActive(true);
        ready = false;
        while (ready == false)
        {
            yield return null;
        }
        continua.SetActive(false);
        sideText.GetComponent<TextMeshProUGUI>().text = "attendi un attimo stiamo sistemando la scacchiera...";
        BoardController.ResetChessBoard();
        BoardController.ChessBoardState[1, 4] = emptyState; StartingPositions.white_ponds[4].SetActive(false);
        BoardController.ChessBoardState[1, 6] = emptyState; StartingPositions.white_ponds[6].SetActive(false);
        BoardController.ChessBoardState[1, 7] = emptyState; StartingPositions.white_ponds[7].SetActive(false);
        BoardController.ChessBoardState[6, 2] = emptyState; StartingPositions.black_ponds[2].SetActive(false);
        BoardController.ChessBoardState[6, 4] = emptyState; StartingPositions.black_ponds[4].SetActive(false);
        BoardController.ChessBoardState[6, 5] = emptyState; StartingPositions.black_ponds[5].SetActive(false);
        BoardController.ChessBoardState[7, 7] = emptyState; StartingPositions.black_rooks[1].SetActive(false);
        BoardController.ChessBoardState[0, 6] = emptyState; StartingPositions.white_knights[1].SetActive(false);
        BoardController.ChessBoardState[7, 6] = emptyState; StartingPositions.black_knights[1].SetActive(false);
        pos = mainCam.transform.position;
        StartCoroutine(MovePieceRoutine(pos, new Vector3(0, 7.07f, 7.45f)));
        yield return new WaitForSeconds(.5f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[1, 3].piece, BoardController.gridPositions[1, 3] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[3, 3]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[6, 7].piece, BoardController.gridPositions[6, 7] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[5, 7]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[0, 2].piece, BoardController.gridPositions[0, 2] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[2, 4]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[6, 6].piece, BoardController.gridPositions[6, 6] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[1, 6]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[0, 4].piece, BoardController.gridPositions[0, 4] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[0, 6]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 5].piece, BoardController.gridPositions[7, 5] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[6, 6]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[0, 1].piece, BoardController.gridPositions[0, 1] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[7, 7]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[6, 1].piece, BoardController.gridPositions[6, 1] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[5, 1]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[1, 2].piece, BoardController.gridPositions[1, 2] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[5, 3]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 2].piece, BoardController.gridPositions[7, 2] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[6, 1]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[0, 5].piece, BoardController.gridPositions[0, 5] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[4, 3]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 1].piece, BoardController.gridPositions[7, 1] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[5, 2]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[0, 7].piece, BoardController.gridPositions[0, 7] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[0, 4]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 4].piece, BoardController.gridPositions[7, 4] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[7, 2]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[0, 0].piece, BoardController.gridPositions[0, 0] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[0, 2]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 3].piece, BoardController.gridPositions[7, 3] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[3, 7]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 0].piece, BoardController.gridPositions[7, 0] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[7, 3]); yield return new WaitForSeconds(1f);
        MovePieceLogic(new Vector2Int(1, 3), new Vector2Int(3, 3));
        MovePieceLogic(new Vector2Int(6, 7), new Vector2Int(5, 7));
        MovePieceLogic(new Vector2Int(0, 2), new Vector2Int(2, 4));
        MovePieceLogic(new Vector2Int(6, 6), new Vector2Int(1, 6));        
        MovePieceLogic(new Vector2Int(0, 4), new Vector2Int(0, 6));
        MovePieceLogic(new Vector2Int(7, 5), new Vector2Int(6, 6));
        MovePieceLogic(new Vector2Int(0, 1), new Vector2Int(7, 7));
        MovePieceLogic(new Vector2Int(6, 1), new Vector2Int(5, 1));
        MovePieceLogic(new Vector2Int(1, 2), new Vector2Int(5, 3));
        MovePieceLogic(new Vector2Int(7, 2), new Vector2Int(6, 1));
        MovePieceLogic(new Vector2Int(0, 5), new Vector2Int(4, 3));
        MovePieceLogic(new Vector2Int(7, 1), new Vector2Int(5, 2));
        MovePieceLogic(new Vector2Int(0, 7), new Vector2Int(0, 4));
        MovePieceLogic(new Vector2Int(7, 4), new Vector2Int(7, 2));
        MovePieceLogic(new Vector2Int(0, 0), new Vector2Int(0, 2));
        MovePieceLogic(new Vector2Int(7, 3), new Vector2Int(3, 7));
        MovePieceLogic(new Vector2Int(7, 0), new Vector2Int(7, 3));
        yield return new WaitForSeconds(3f);
        loop = true;
        BoardController.blackTurn = true;
        BoardController.enable = true;
        while (loop)
        {
            sideText.GetComponent<TextMeshProUGUI>().text = "anche in questo puzzle c'è matto in 1 per il nero, prova a risolverlo";
            CalculateMoves.foundBase = false;
            CalculateMoves.foundCapture = false;
            while (!CalculateMoves.foundBase && !CalculateMoves.foundCapture)
            {
                yield return null;
            }
            BoardController.enable = false;
            if (CalculateMoves.foundCapture)
            {
                sideText.GetComponent<TextMeshProUGUI>().text = "mossa sbagliata. riprova";
                yield return new WaitForSeconds(3f);
                continua.SetActive(true);
                ready = false;
                while (ready == false)
                {
                    yield return null;
                }
                continua.SetActive(false);
                MovingAnimation.AnimateAndMovePiece(CalculateMoves.oldClickedPiecePuzzle.piece, BoardController.gridPositions[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[CalculateMoves.oldClickedPiecePuzzle.postion.x, CalculateMoves.oldClickedPiecePuzzle.postion.y]);
                MovingAnimation.AnimateAndMovePiece(CalculateMoves.newClickedPiecePuzzle.piece, CalculateMoves.newClickedPiecePuzzle.piece.transform.position + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y]);
                BoardController.ChessBoardState[CalculateMoves.oldClickedPiecePuzzle.postion.x, CalculateMoves.oldClickedPiecePuzzle.postion.y] = CalculateMoves.oldClickedPiecePuzzle;
                BoardController.ChessBoardState[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y] = CalculateMoves.newClickedPiecePuzzle;
                yield return new WaitForSeconds(3f);
                BoardController.enable = true;
            }
            else if (CalculateMoves.foundBase)
            {
                if (BoardController.clickedobject == StartingPositions.bases[0, 7] && CalculateMoves.oldClickedPiecePuzzle.piece == StartingPositions.black_queen)
                {
                    loop = false;
                }
                else
                {
                    sideText.GetComponent<TextMeshProUGUI>().text = "mossa sbagliata. riprova";
                    yield return new WaitForSeconds(3f);
                    continua.SetActive(true);
                    ready = false;
                    while (ready == false)
                    {
                        yield return null;
                    }
                    continua.SetActive(false);
                    MovingAnimation.AnimateAndMovePiece(CalculateMoves.oldClickedPiecePuzzle.piece, BoardController.gridPositions[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[CalculateMoves.oldClickedPiecePuzzle.postion.x, CalculateMoves.oldClickedPiecePuzzle.postion.y]);
                    BoardController.ChessBoardState[CalculateMoves.oldClickedPiecePuzzle.postion.x, CalculateMoves.oldClickedPiecePuzzle.postion.y] = BoardController.ChessBoardState[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y];
                    BoardController.ChessBoardState[CalculateMoves.oldClickedPiecePuzzle.postion.x, CalculateMoves.oldClickedPiecePuzzle.postion.y].postion = CalculateMoves.oldClickedPiecePuzzle.postion;
                    BoardController.ChessBoardState[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y] = emptyState;
                    yield return new WaitForSeconds(3f);
                    BoardController.enable = true;
                }
            }
        }
        yield return new WaitForSeconds(3f);
        sideText.GetComponent<TextMeshProUGUI>().text = "Bravo! hai completato il terzo puzzle clicca su continua per passare al quarto";
        ////////////////////////////////////////////////////////////////////////////////fourth puzzle
        yield return new WaitForSeconds(1f);
        continua.SetActive(true);
        ready = false;
        while (ready == false)
        {
            yield return null;
        }
        continua.SetActive(false);
        BoardController.ResetChessBoard();
        BoardController.ChessBoardState[1, 2] = emptyState; StartingPositions.white_ponds[2].SetActive(false);
        BoardController.ChessBoardState[1, 3] = emptyState; StartingPositions.white_ponds[3].SetActive(false);
        BoardController.ChessBoardState[1, 4] = emptyState; StartingPositions.white_ponds[4].SetActive(false);
        BoardController.ChessBoardState[6, 1] = emptyState; StartingPositions.black_ponds[1].SetActive(false);
        BoardController.ChessBoardState[6, 5] = emptyState; StartingPositions.black_ponds[5].SetActive(false);
        BoardController.ChessBoardState[6, 6] = emptyState; StartingPositions.black_ponds[6].SetActive(false);
        BoardController.ChessBoardState[0, 3] = emptyState; StartingPositions.white_queen.SetActive(false);
        BoardController.ChessBoardState[2, 7] = emptyState; StartingPositions.black_bishops[0].SetActive(false);
        pos = mainCam.transform.position;
        StartCoroutine(MovePieceRoutine(pos, new Vector3(0, 7.07f, -7.45f)));
        yield return new WaitForSeconds(.5f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[1, 0].piece, BoardController.gridPositions[1, 0] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[2, 0]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[6, 0].piece, BoardController.gridPositions[6, 0] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[4, 0]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[1, 1].piece, BoardController.gridPositions[1, 1] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[3, 1]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[6, 2].piece, BoardController.gridPositions[6, 2] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[2, 2]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[1, 6].piece, BoardController.gridPositions[1, 6] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[2, 6]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[6, 3].piece, BoardController.gridPositions[6, 3] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[3, 3]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[1, 7].piece, BoardController.gridPositions[1, 7] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[3, 7]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[6, 7].piece, BoardController.gridPositions[6, 7] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[4, 7]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[0, 4].piece, BoardController.gridPositions[0, 4] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[1, 6]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[0, 7].piece, BoardController.gridPositions[0, 7] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[1, 4]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[0, 6].piece, BoardController.gridPositions[0, 6] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[3, 4]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 0].piece, BoardController.gridPositions[7, 0] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[5, 0]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[0, 2].piece, BoardController.gridPositions[0, 2] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[3, 5]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 6].piece, BoardController.gridPositions[7, 6] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[5, 3]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[0, 5].piece, BoardController.gridPositions[0, 5] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[1, 0]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 3].piece, BoardController.gridPositions[7, 3] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[4, 1]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[0, 0].piece, BoardController.gridPositions[0, 0] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[0, 2]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 1].piece, BoardController.gridPositions[7, 1] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[7, 3]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 7].piece, BoardController.gridPositions[7, 7] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[6, 2]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[0, 1].piece, BoardController.gridPositions[0, 1] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[4, 2]); yield return new WaitForSeconds(1f);
        MovePieceLogic(new Vector2Int(1, 0), new Vector2Int(2, 0));
        MovePieceLogic(new Vector2Int(6, 0), new Vector2Int(4, 0));
        MovePieceLogic(new Vector2Int(1, 1), new Vector2Int(3, 1));
        MovePieceLogic(new Vector2Int(6, 2), new Vector2Int(2, 2));
        MovePieceLogic(new Vector2Int(1, 6), new Vector2Int(2, 6));
        MovePieceLogic(new Vector2Int(6, 3), new Vector2Int(3, 3));
        MovePieceLogic(new Vector2Int(1, 7), new Vector2Int(3, 7));
        MovePieceLogic(new Vector2Int(6, 7), new Vector2Int(4, 7));
        MovePieceLogic(new Vector2Int(0, 4), new Vector2Int(1, 6));
        MovePieceLogic(new Vector2Int(0, 7), new Vector2Int(1, 4));
        MovePieceLogic(new Vector2Int(0, 6), new Vector2Int(3, 4));
        MovePieceLogic(new Vector2Int(7, 0), new Vector2Int(5, 0));
        MovePieceLogic(new Vector2Int(0, 2), new Vector2Int(3, 5));
        MovePieceLogic(new Vector2Int(7, 6), new Vector2Int(5, 3));
        MovePieceLogic(new Vector2Int(0, 5), new Vector2Int(1, 0));
        MovePieceLogic(new Vector2Int(7, 3), new Vector2Int(4, 1));
        MovePieceLogic(new Vector2Int(0, 0), new Vector2Int(0, 2));
        MovePieceLogic(new Vector2Int(7, 1), new Vector2Int(7, 3));
        MovePieceLogic(new Vector2Int(7, 7), new Vector2Int(6, 2));
        MovePieceLogic(new Vector2Int(0, 1), new Vector2Int(4, 2));
        yield return new WaitForSeconds(3f);
        loop = true;
        BoardController.blackTurn = false;
        BoardController.enable = true;
        while (loop)
        {
            sideText.GetComponent<TextMeshProUGUI>().text = "Questo problema è un po' più impegnativo, ma il Bianco ha comunque il matto in 1. Mettiti alla prova e trova la soluzione!";
            CalculateMoves.foundBase = false;
            CalculateMoves.foundCapture = false;
            while (!CalculateMoves.foundBase && !CalculateMoves.foundCapture)
            {
                yield return null;
            }
            BoardController.enable = false;
            if (CalculateMoves.foundCapture)
            {
                if (CalculateMoves.oldClickedPiecePuzzle.piece == StartingPositions.white_knights[1] && CalculateMoves.newClickedPiecePuzzle.piece == StartingPositions.black_knights[1])
                {
                    loop = false;
                }
                else
                {
                    sideText.GetComponent<TextMeshProUGUI>().text = "mossa sbagliata. riprova";
                    yield return new WaitForSeconds(3f);
                    continua.SetActive(true);
                    ready = false;
                    while (ready == false)
                    {
                        yield return null;
                    }
                    continua.SetActive(false);
                    MovingAnimation.AnimateAndMovePiece(CalculateMoves.oldClickedPiecePuzzle.piece, BoardController.gridPositions[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[CalculateMoves.oldClickedPiecePuzzle.postion.x, CalculateMoves.oldClickedPiecePuzzle.postion.y]);
                    MovingAnimation.AnimateAndMovePiece(CalculateMoves.newClickedPiecePuzzle.piece, CalculateMoves.newClickedPiecePuzzle.piece.transform.position + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y]);
                    BoardController.ChessBoardState[CalculateMoves.oldClickedPiecePuzzle.postion.x, CalculateMoves.oldClickedPiecePuzzle.postion.y] = CalculateMoves.oldClickedPiecePuzzle;
                    BoardController.ChessBoardState[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y] = CalculateMoves.newClickedPiecePuzzle;
                    yield return new WaitForSeconds(3f);
                    BoardController.enable = true;
                }
            }
            else if (CalculateMoves.foundBase)
            {
                sideText.GetComponent<TextMeshProUGUI>().text = "mossa sbagliata. riprova";
                yield return new WaitForSeconds(3f);
                continua.SetActive(true);
                ready = false;
                while (ready == false)
                {
                    yield return null;
                }
                continua.SetActive(false);
                MovingAnimation.AnimateAndMovePiece(CalculateMoves.oldClickedPiecePuzzle.piece, BoardController.gridPositions[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[CalculateMoves.oldClickedPiecePuzzle.postion.x, CalculateMoves.oldClickedPiecePuzzle.postion.y]);
                BoardController.ChessBoardState[CalculateMoves.oldClickedPiecePuzzle.postion.x, CalculateMoves.oldClickedPiecePuzzle.postion.y] = BoardController.ChessBoardState[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y];
                BoardController.ChessBoardState[CalculateMoves.oldClickedPiecePuzzle.postion.x, CalculateMoves.oldClickedPiecePuzzle.postion.y].postion = CalculateMoves.oldClickedPiecePuzzle.postion;
                BoardController.ChessBoardState[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y] = emptyState;
                yield return new WaitForSeconds(3f);
                BoardController.enable = true;
            }
        }
        yield return new WaitForSeconds(3f);
        sideText.GetComponent<TextMeshProUGUI>().text = "Bravo! hai completato il quarto puzzle clicca su continua per passare all'ultimo puzzle";
        ////////////////////////////////////////////////////////////////////////////////fifth puzzle
        yield return new WaitForSeconds(1f);
        continua.SetActive(true);
        ready = false;
        while (ready == false)
        {
            yield return null;
        }
        Badge.ShowAchievement();
        continua.SetActive(false);
        sideText.GetComponent<TextMeshProUGUI>().text = "Questo puzzle è il boss finale! c'è matto in 3 per il bianco, prenditi tutto il tempo che serve per risolverlo!";
        BoardController.ResetChessBoard();
        CalculateMoves.movedKing[0] = true;
        CalculateMoves.movedKing[1] = true;
        BoardController.ChessBoardState[1, 2] = emptyState; StartingPositions.white_ponds[2].SetActive(false);
        BoardController.ChessBoardState[1, 3] = emptyState; StartingPositions.white_ponds[3].SetActive(false);
        BoardController.ChessBoardState[0, 2] = emptyState; StartingPositions.white_bishops[0].SetActive(false);
        BoardController.ChessBoardState[0, 5] = emptyState; StartingPositions.white_bishops[1].SetActive(false);
        BoardController.ChessBoardState[0, 1] = emptyState; StartingPositions.white_knights[0].SetActive(false);
        BoardController.ChessBoardState[6, 3] = emptyState; StartingPositions.black_ponds[3].SetActive(false);
        BoardController.ChessBoardState[6, 4] = emptyState; StartingPositions.black_ponds[4].SetActive(false);
        BoardController.ChessBoardState[6, 5] = emptyState; StartingPositions.black_ponds[5].SetActive(false);
        BoardController.ChessBoardState[7, 2] = emptyState; StartingPositions.black_bishops[0].SetActive(false);
        BoardController.ChessBoardState[7, 5] = emptyState; StartingPositions.black_bishops[1].SetActive(false);
        BoardController.ChessBoardState[7, 3] = emptyState; StartingPositions.black_queen.SetActive(false);
        BoardController.ChessBoardState[7, 1] = emptyState; StartingPositions.black_knights[0].SetActive(false);
        pos = mainCam.transform.position;
        StartCoroutine(MovePieceRoutine(pos, new Vector3(0, 7.07f, -7.45f)));
        yield return new WaitForSeconds(.5f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[1, 4].piece, BoardController.gridPositions[1, 4] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[2, 4]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[6, 2].piece, BoardController.gridPositions[6, 2] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[5, 2]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[0, 6].piece, BoardController.gridPositions[0, 6] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[6, 5]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 6].piece, BoardController.gridPositions[7, 6] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[5, 5]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 0].piece, BoardController.gridPositions[7, 0] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[7, 3]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[0, 7].piece, BoardController.gridPositions[0, 7] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[0, 5]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 4].piece, BoardController.gridPositions[7, 4] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[7, 6]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[0, 4].piece, BoardController.gridPositions[0, 4] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[0, 6]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 7].piece, BoardController.gridPositions[7, 7] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[7, 4]); yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[0, 3].piece, BoardController.gridPositions[0, 3] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[2, 1]); yield return new WaitForSeconds(1f);
        MovePieceLogic(new Vector2Int(1, 4), new Vector2Int(2, 4));
        MovePieceLogic(new Vector2Int(6, 2), new Vector2Int(5, 2));
        MovePieceLogic(new Vector2Int(0, 6), new Vector2Int(6, 5));
        MovePieceLogic(new Vector2Int(7, 6), new Vector2Int(5, 5));
        MovePieceLogic(new Vector2Int(7, 0), new Vector2Int(7, 3));
        MovePieceLogic(new Vector2Int(0, 7), new Vector2Int(0, 5));
        MovePieceLogic(new Vector2Int(7, 4), new Vector2Int(7, 6));
        MovePieceLogic(new Vector2Int(0, 4), new Vector2Int(0, 6));
        MovePieceLogic(new Vector2Int(7, 7), new Vector2Int(7, 4));
        MovePieceLogic(new Vector2Int(0, 3), new Vector2Int(2, 1));
        yield return new WaitForSeconds(3f);
        loop = true;
        BoardController.enable = true;
        while (loop)
        {
            sideText.GetComponent<TextMeshProUGUI>().text = "Questo puzzle è il boss finale! c'è matto in 3 per il bianco, prenditi tutto il tempo che serve per risolverlo!";
            CalculateMoves.foundBase = false;
            CalculateMoves.foundCapture = false;
            while (!CalculateMoves.foundBase && !CalculateMoves.foundCapture)
            {
                yield return null;
            }
            BoardController.enable = false;
            if (CalculateMoves.foundCapture)
            {
                sideText.GetComponent<TextMeshProUGUI>().text = "mossa sbagliata. riprova";
                yield return new WaitForSeconds(3f);
                continua.SetActive(true);
                ready = false;
                while (ready == false)
                {
                    yield return null;
                }
                continua.SetActive(false);
                MovingAnimation.AnimateAndMovePiece(CalculateMoves.oldClickedPiecePuzzle.piece, BoardController.gridPositions[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[CalculateMoves.oldClickedPiecePuzzle.postion.x, CalculateMoves.oldClickedPiecePuzzle.postion.y]);
                MovingAnimation.AnimateAndMovePiece(CalculateMoves.newClickedPiecePuzzle.piece, CalculateMoves.newClickedPiecePuzzle.piece.transform.position + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y]);
                BoardController.ChessBoardState[CalculateMoves.oldClickedPiecePuzzle.postion.x, CalculateMoves.oldClickedPiecePuzzle.postion.y] = CalculateMoves.oldClickedPiecePuzzle;
                BoardController.ChessBoardState[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y] = CalculateMoves.newClickedPiecePuzzle;
                yield return new WaitForSeconds(3f);
                BoardController.enable = true;
            }
            else if (CalculateMoves.foundBase)
            {
                if (BoardController.clickedobject == StartingPositions.bases[5, 7] && CalculateMoves.oldClickedPiecePuzzle.piece == StartingPositions.white_knights[1])
                {
                    loop = false;
                }
                else
                {
                    sideText.GetComponent<TextMeshProUGUI>().text = "mossa sbagliata. riprova";
                    yield return new WaitForSeconds(3f);
                    continua.SetActive(true);
                    ready = false;
                    while (ready == false)
                    {
                        yield return null;
                    }
                    continua.SetActive(false);
                    MovingAnimation.AnimateAndMovePiece(CalculateMoves.oldClickedPiecePuzzle.piece, BoardController.gridPositions[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[CalculateMoves.oldClickedPiecePuzzle.postion.x, CalculateMoves.oldClickedPiecePuzzle.postion.y]);
                    BoardController.ChessBoardState[CalculateMoves.oldClickedPiecePuzzle.postion.x, CalculateMoves.oldClickedPiecePuzzle.postion.y] = BoardController.ChessBoardState[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y];
                    BoardController.ChessBoardState[CalculateMoves.oldClickedPiecePuzzle.postion.x, CalculateMoves.oldClickedPiecePuzzle.postion.y].postion = CalculateMoves.oldClickedPiecePuzzle.postion;
                    BoardController.ChessBoardState[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y] = emptyState;
                    yield return new WaitForSeconds(3f);
                    BoardController.enable = true;
                }
            }
        }
        yield return new WaitForSeconds(3f);
        sideText.GetComponent<TextMeshProUGUI>().text = "questa mossa viene definita un \"doppio scacco\" e il nero è costretto a muovere il re per difendersi";
        yield return new WaitForSeconds(1f);
        continua.SetActive(true);
        ready = false;
        while (ready == false)
        {
            yield return null;
        }
        continua.SetActive(false);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 6].piece, BoardController.gridPositions[7, 6] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[7, 7]);
        BoardController.ChessBoardState[7, 7] = BoardController.ChessBoardState[7, 6];
        BoardController.ChessBoardState[7, 7].postion = new Vector2Int(7, 7);
        BoardController.ChessBoardState[7, 6] = emptyState;
        yield return new WaitForSeconds(3f);
        loop = true;
        BoardController.enable = true;
        while (loop)
        {
            sideText.GetComponent<TextMeshProUGUI>().text = "trova la prossima mossa per risolvere il puzzle!";
            CalculateMoves.foundBase = false;
            CalculateMoves.foundCapture = false;
            while (!CalculateMoves.foundBase && !CalculateMoves.foundCapture)
            {
                yield return null;
            }
            BoardController.enable = false;
            if (CalculateMoves.foundCapture)
            {
                sideText.GetComponent<TextMeshProUGUI>().text = "mossa sbagliata. riprova";
                yield return new WaitForSeconds(3f);
                continua.SetActive(true);
                ready = false;
                while (ready == false)
                {
                    yield return null;
                }
                continua.SetActive(false);
                MovingAnimation.AnimateAndMovePiece(CalculateMoves.oldClickedPiecePuzzle.piece, BoardController.gridPositions[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[CalculateMoves.oldClickedPiecePuzzle.postion.x, CalculateMoves.oldClickedPiecePuzzle.postion.y]);
                MovingAnimation.AnimateAndMovePiece(CalculateMoves.newClickedPiecePuzzle.piece, CalculateMoves.newClickedPiecePuzzle.piece.transform.position + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y]);
                BoardController.ChessBoardState[CalculateMoves.oldClickedPiecePuzzle.postion.x, CalculateMoves.oldClickedPiecePuzzle.postion.y] = CalculateMoves.oldClickedPiecePuzzle;
                BoardController.ChessBoardState[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y] = CalculateMoves.newClickedPiecePuzzle;
                yield return new WaitForSeconds(3f);
                BoardController.enable = true;
            }
            else if (CalculateMoves.foundBase)
            {
                if (BoardController.clickedobject == StartingPositions.bases[7, 6] && CalculateMoves.oldClickedPiecePuzzle.piece == StartingPositions.white_queen)
                {
                    loop = false;
                }
                else
                {
                    sideText.GetComponent<TextMeshProUGUI>().text = "mossa sbagliata. riprova";
                    yield return new WaitForSeconds(3f);
                    continua.SetActive(true);
                    ready = false;
                    while (ready == false)
                    {
                        yield return null;
                    }
                    continua.SetActive(false);
                    MovingAnimation.AnimateAndMovePiece(CalculateMoves.oldClickedPiecePuzzle.piece, BoardController.gridPositions[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[CalculateMoves.oldClickedPiecePuzzle.postion.x, CalculateMoves.oldClickedPiecePuzzle.postion.y]);
                    BoardController.ChessBoardState[CalculateMoves.oldClickedPiecePuzzle.postion.x, CalculateMoves.oldClickedPiecePuzzle.postion.y] = BoardController.ChessBoardState[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y];
                    BoardController.ChessBoardState[CalculateMoves.oldClickedPiecePuzzle.postion.x, CalculateMoves.oldClickedPiecePuzzle.postion.y].postion = CalculateMoves.oldClickedPiecePuzzle.postion;
                    BoardController.ChessBoardState[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y] = emptyState;
                    yield return new WaitForSeconds(3f);
                    BoardController.enable = true;
                }
            }
        }
        yield return new WaitForSeconds(3f);
        sideText.GetComponent<TextMeshProUGUI>().text = "Bella mossa! con questa mossa metti sotto scacco il re nero forzandolo a catturare con la torre perchè la tua regina è difesa dal cavallo!";
        yield return new WaitForSeconds(1f);
        continua.SetActive(true);
        ready = false;
        while (ready == false)
        {
            yield return null;
        }
        continua.SetActive(false);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 4].piece, BoardController.gridPositions[7, 4] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[7, 6]);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 6].piece, BoardController.gridPositions[7, 6] + new Vector3(0f, 0.7f, 0f), CalculateMoves.finalpos.transform.position);
        BoardController.ChessBoardState[7, 6] = BoardController.ChessBoardState[7, 4];
        BoardController.ChessBoardState[7, 6].postion = new Vector2Int(7, 6);
        BoardController.ChessBoardState[7, 4] = emptyState;
        BoardController.ChessBoardState[2, 1] = emptyState;
        yield return new WaitForSeconds(3f);
        loop = true;
        BoardController.enable = true;
        while (loop)
        {
            sideText.GetComponent<TextMeshProUGUI>().text = "ora manca solo la mossa finale per finire il puzzle e vincere la partita!";
            CalculateMoves.foundBase = false;
            CalculateMoves.foundCapture = false;
            while (!CalculateMoves.foundBase && !CalculateMoves.foundCapture)
            {
                yield return null;
            }
            BoardController.enable = false;
            if (CalculateMoves.foundCapture)
            {
                sideText.GetComponent<TextMeshProUGUI>().text = "mossa sbagliata. riprova";
                yield return new WaitForSeconds(3f);
                continua.SetActive(true);
                ready = false;
                while (ready == false)
                {
                    yield return null;
                }
                continua.SetActive(false);
                MovingAnimation.AnimateAndMovePiece(CalculateMoves.oldClickedPiecePuzzle.piece, BoardController.gridPositions[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[CalculateMoves.oldClickedPiecePuzzle.postion.x, CalculateMoves.oldClickedPiecePuzzle.postion.y]);
                MovingAnimation.AnimateAndMovePiece(CalculateMoves.newClickedPiecePuzzle.piece, CalculateMoves.newClickedPiecePuzzle.piece.transform.position + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y]);
                BoardController.ChessBoardState[CalculateMoves.oldClickedPiecePuzzle.postion.x, CalculateMoves.oldClickedPiecePuzzle.postion.y] = CalculateMoves.oldClickedPiecePuzzle;
                BoardController.ChessBoardState[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y] = CalculateMoves.newClickedPiecePuzzle;
                yield return new WaitForSeconds(3f);
                BoardController.enable = true;
            }
            else if (CalculateMoves.foundBase)
            {
                if (BoardController.clickedobject == StartingPositions.bases[6, 5] && CalculateMoves.oldClickedPiecePuzzle.piece == StartingPositions.white_knights[1])
                {
                    loop = false;
                }
                else
                {
                    sideText.GetComponent<TextMeshProUGUI>().text = "mossa sbagliata. riprova";
                    yield return new WaitForSeconds(3f);
                    continua.SetActive(true);
                    ready = false;
                    while (ready == false)
                    {
                        yield return null;
                    }
                    continua.SetActive(false);
                    MovingAnimation.AnimateAndMovePiece(CalculateMoves.oldClickedPiecePuzzle.piece, BoardController.gridPositions[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[CalculateMoves.oldClickedPiecePuzzle.postion.x, CalculateMoves.oldClickedPiecePuzzle.postion.y]);
                    BoardController.ChessBoardState[CalculateMoves.oldClickedPiecePuzzle.postion.x, CalculateMoves.oldClickedPiecePuzzle.postion.y] = BoardController.ChessBoardState[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y];
                    BoardController.ChessBoardState[CalculateMoves.oldClickedPiecePuzzle.postion.x, CalculateMoves.oldClickedPiecePuzzle.postion.y].postion = CalculateMoves.oldClickedPiecePuzzle.postion;
                    BoardController.ChessBoardState[CalculateMoves.newClickedPiecePuzzle.postion.x, CalculateMoves.newClickedPiecePuzzle.postion.y] = emptyState;
                    yield return new WaitForSeconds(3f);
                    BoardController.enable = true;
                }
            }
        }
        yield return new WaitForSeconds(3f);
        sideText.GetComponent<TextMeshProUGUI>().text = "mossa corretta! complimenti hai completato anche l'ultimo puzzle!";
        yield return new WaitForSeconds(1f);
        continua.SetActive(true);
        ready = false;
        while (ready == false)
        {
            yield return null;
        }
        puzzleImage.sprite = puzzleSprite;
        Badge.ShowAchievement();
        continua.SetActive(false);
        sideText.SetActive(false);
        panel.SetActive(false);
        frontText.GetComponent<TextMeshProUGUI>().text = "complimenti hai completato la sezione dei puzzle!";
        frontText.SetActive(true);
        yield return new WaitForSeconds(1f);
        menu.SetActive(true);
        ready = false;
        while (ready == false)
        {
            yield return null;
        }
        SceneManager.LoadScene(0);
    }







    void MovePieceLogic(Vector2Int from, Vector2Int to)
    {
        BoardController.ChessBoardState[to.x, to.y] = BoardController.ChessBoardState[from.x, from.y];
        BoardController.ChessBoardState[to.x, to.y].postion = to;
        BoardController.ChessBoardState[from.x, from.y] = emptyState;
    }
    private IEnumerator MovePieceRoutine(Vector3 startPos, Vector3 endPos)
    {
        float elapsedTime = 0f;
        float duration = 1f;
        Quaternion startRotation = pivot.transform.rotation;
        Vector3 directionToCenter = Vector3.zero - endPos;
        Quaternion endRotation = Quaternion.LookRotation(directionToCenter);
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            float smoothT = t * t * (3f - 2f * t);
            mainCamera.transform.position = Vector3.Lerp(startPos, endPos, smoothT);
            pivot.transform.rotation = Quaternion.Slerp(startRotation, endRotation, smoothT);
            yield return null;
        }
        mainCamera.transform.position = endPos;
        pivot.transform.rotation = endRotation;
    }
}