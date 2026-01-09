using System.Collections;
using TMPro;
using Unity.Multiplayer.Center.Common;
using UnityEngine;

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
    public GameObject continua;
    public void Continue()
    {
        ready = true;
    }
    public void Awake()
    {
        ready = false;
        StartingPositions = GetComponent<StartingPositions>();
        BoardController = GetComponent<BoardController>();
        CalculateMoves = GetComponent<CalculateMoves>();
        MovingAnimation = GetComponent<MovingAnimation>();
    }
    private void Start()
    {
        StartCoroutine(Puzzle());
    }
    private IEnumerator Puzzle()
    {
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
            postion = new Vector2Int(5, 2),
            isWhite = false
        };
        sideText.GetComponent<TextMeshProUGUI>().text = "in questa sezione ci sono dei \"puzzle\" ovvero delle posizioni da risolvere per vincere la partita o acquisire un vantaggio";
        yield return new WaitForSeconds(1f);
        continua.SetActive(true);
        ready = false;
        while (ready == false)
        {
            yield return null;
        }
        sideText.GetComponent<TextMeshProUGUI>().text = "in questo puzzle c'è matto in 1 per il bianco, prova a risolverlo";
        CalculateMoves.found = false;
        while (!CalculateMoves.found)
        {
            yield return null;
        }
        
        CalculateMoves.foundBase = false;
        CalculateMoves.foundCapture = false;
        while (!CalculateMoves.foundBase || !CalculateMoves.foundCapture)
        {
            selected = CalculateMoves.savedChessBoardState;
            yield return null;
        }
        //se la mossa è giusta 

        yield return null;
    }   
}