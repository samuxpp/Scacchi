using System.Collections;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

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
    public GameObject panel;
    private bool ready = false;
    public void Awake()
    {
        ready = false;
        StartingPositions = GetComponent<StartingPositions>();
        BoardController = GetComponent<BoardController>();
        CalculateMoves = GetComponent<CalculateMoves>();
        MovingAnimation = GetComponent<MovingAnimation>();
    }
    public void Start()
    {
        StartCoroutine(SetUpBoardRoutine());
        StartCoroutine(TUT());
    }

    public void Continue()
    {
        ready = true;
    }

    private IEnumerator TUT()
    {
        yield return new WaitUntil(() => BoardController.isInitialized == true);
        BoardController.enable = true;
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
            if (BoardController.ChessBoardState[2, 4].piece != null)
            {
                StartingPositions.bases[3, 4].SetActive(false);
            }
            else if (BoardController.ChessBoardState[3, 4].piece != null)
            {
                StartingPositions.bases[4, 4].SetActive(false);
            }
        }
        sideText.GetComponent<TextMeshProUGUI>().text = "";
        panel.SetActive(false);
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
        panel.SetActive(true);
        sideText.GetComponent<TextMeshProUGUI>().text = "L'alfiere si muove solo in diagonale in ambedue le direzioni. Clicca sull'alfiere.";
        CalculateMoves.found = false;
        while (!CalculateMoves.found)
        {
            yield return null;
        }
        sideText.GetComponent<TextMeshProUGUI>().text = "Scegli dove muovere l'alfiere.";
        CalculateMoves.foundBase = false;
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
        Vector2Int pos = Vector2Int.zero;
        if (BoardController.ChessBoardState[1, 1].piece != null)
        {
            BoardController.ChessBoardState[5, 5] = BoardController.ChessBoardState[7, 3];
            MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 3].piece, BoardController.gridPositions[7, 3] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[5, 5]);
            pos = new Vector2Int(5, 5);
        }
        else if (BoardController.ChessBoardState[1, 3].piece != null)
        {
            BoardController.ChessBoardState[4, 6] = BoardController.ChessBoardState[7, 3];
            MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 3].piece, BoardController.gridPositions[7, 3] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[4, 6]);
            pos = new Vector2Int(4, 6);
        }
        else if (BoardController.ChessBoardState[2, 0].piece != null)
        {
            BoardController.ChessBoardState[6, 4] = BoardController.ChessBoardState[7, 3];
            MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 3].piece, BoardController.gridPositions[7, 3] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[6, 4]);
            pos = new Vector2Int(6, 4);
        }
        else if (BoardController.ChessBoardState[2, 4].piece != null)
        {
            BoardController.ChessBoardState[5, 1] = BoardController.ChessBoardState[7, 3];
            MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 3].piece, BoardController.gridPositions[7, 3] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[5, 1]);
            pos = new Vector2Int(5, 1);
        }
        else if (BoardController.ChessBoardState[3, 5].piece != null)
        {
            BoardController.ChessBoardState[6, 2] = BoardController.ChessBoardState[7, 3];
            MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 3].piece, BoardController.gridPositions[7, 3] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[6, 2]);
            pos = new Vector2Int(6, 2);
        }
        else if (BoardController.ChessBoardState[4, 6].piece != null)
        {
            BoardController.ChessBoardState[1, 3] = BoardController.ChessBoardState[7, 3];
            MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 3].piece, BoardController.gridPositions[7, 3] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[1, 3]);
            pos = new Vector2Int(1, 3);
        }
        else if (BoardController.ChessBoardState[5, 7].piece != null)
        {
            BoardController.ChessBoardState[1, 3] = BoardController.ChessBoardState[7, 3];
            MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 3].piece, BoardController.gridPositions[7, 3] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[1, 3]);
            pos = new Vector2Int(1, 3);
        }
        CalculateMoves.foundCapture = false;
        BoardController.ChessBoardState[7, 3] = BoardController.ChessBoardState[pos.x, pos.y];
        while (!CalculateMoves.foundCapture)
        {
            yield return null;
        }
        sideText.GetComponent<TextMeshProUGUI>().text = "";
        yield return new WaitForSeconds(3f);
        panel.SetActive(false);
        frontText.GetComponent<TextMeshProUGUI>().text = "Ora sai come si muove e come cattura un alfiere, procedi con il tutorial per imparare a muovere il cavallo.";
        frontText.SetActive(true);
        yield return new WaitForSeconds(1f);
        continua.SetActive(true);
        ready = false;
        while (ready == false)
        {
            yield return null;
        }
        //////////////////////////////////////////////////////////////////knight
        CleanBoard();
        BoardController.ChessBoardState[0, 1] = new PieceState
        {
            piece = StartingPositions.white_knights[0],
            pieceType = PieceType.knight,
            postion = new Vector2Int(0, 1),
            isWhite = true
        };
        frontText.SetActive(false);
        continua.SetActive(false);
        panel.SetActive(true);
        for (int i = 0; i < StartingPositions.bases.GetLength(0); ++i)
        {
            for (int j = 0; j < StartingPositions.bases.GetLength(1); ++j)
            {
                StartingPositions.bases[i, j].SetActive(false);
                StartingPositions.emptyBases[i, j].SetActive(false);
            }
        }
        BoardController.ChessBoardState[7, 3] = new PieceState
        {
            piece = StartingPositions.black_queen,
            pieceType = PieceType.queen,
            postion = new Vector2Int(7, 3),
            isWhite = false
        };
        StartingPositions.white_knights[0].SetActive(true);       
        MovingAnimation.AnimateAndMovePiece(StartingPositions.black_queen, CalculateMoves.finalpos.transform.position + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[7, 3]);
        StartingPositions.white_bishops[0].SetActive(false);
        sideText.GetComponent<TextMeshProUGUI>().text = "Il cavallo si muove ad L in tutte le direzioni. Può scavalcare qualsiasi pezzo. Clicca sul cavallo.";
        CalculateMoves.found = false;
        while (!CalculateMoves.found)
        {
            yield return null;
        }
        sideText.GetComponent<TextMeshProUGUI>().text = "Ora prova a catturare la donna.";
        CalculateMoves.foundCapture = false;
        while (!CalculateMoves.foundCapture)
        {
            yield return null;
        }
        sideText.GetComponent<TextMeshProUGUI>().text = "";
        yield return new WaitForSeconds(3f);
        panel.SetActive(false);
        frontText.GetComponent<TextMeshProUGUI>().text = "Ora sai come si muove e come cattura il cavallo, procedi con il tutorial per imparare a muovere la torre.";
        frontText.SetActive(true);
        yield return new WaitForSeconds(1f);
        continua.SetActive(true);
        ready = false;
        while (ready == false)
        {
            yield return null;
        }
        //////////////////////////////////////////////////////////////////rook
        BoardController.ChessBoardState[0, 0] = new PieceState
        {
            piece = StartingPositions.white_rooks[0],
            pieceType = PieceType.rook,
            postion = new Vector2Int(0, 0),
            isWhite = true
        };
        panel.SetActive(true);
        frontText.SetActive(false);
        continua.SetActive(false);
        for (int i = 0; i < StartingPositions.bases.GetLength(0); ++i)
        {
            for (int j = 0; j < StartingPositions.bases.GetLength(1); ++j)
            {
                StartingPositions.bases[i, j].SetActive(false);
                StartingPositions.emptyBases[i, j].SetActive(false);
            }
        }
        StartingPositions.white_rooks[0].SetActive(true);
        BoardController.ChessBoardState[7, 3] = new PieceState
        {
            piece = StartingPositions.black_queen,
            pieceType = PieceType.queen,
            postion = new Vector2Int(7, 3),
            isWhite = false
        };
        MovingAnimation.AnimateAndMovePiece(StartingPositions.black_queen, CalculateMoves.finalpos.transform.position + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[7, 3]);
        StartingPositions.white_knights[0].SetActive(false);
        sideText.GetComponent<TextMeshProUGUI>().text = "La torre si muove in verticale e in orizzontale. Clicca sulla torre.";
        CalculateMoves.found = false;
        while (!CalculateMoves.found)
        {
            yield return null;
        }
        sideText.GetComponent<TextMeshProUGUI>().text = "Prova a catturare la donna.";
        CalculateMoves.foundBase = false;
        while (!CalculateMoves.foundBase)
        {
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        MovingAnimation.AnimateAndMovePiece(StartingPositions.black_queen, BoardController.gridPositions[7, 3] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[3, 7]);
        BoardController.ChessBoardState[3, 7] = BoardController.ChessBoardState[7, 3];
        BoardController.ChessBoardState[7, 3] = emptyState;
        CalculateMoves.foundCapture = false;
        while (!CalculateMoves.foundCapture)
        {
            yield return null;
        }
        sideText.GetComponent<TextMeshProUGUI>().text = "";
        yield return new WaitForSeconds(3f);
        panel.SetActive(false);
        frontText.GetComponent<TextMeshProUGUI>().text = "Ora sai come si muove e come cattura la torre, procedi con il tutorial per imparare a muovere la regina.";
        frontText.SetActive(true);
        yield return new WaitForSeconds(1f);
        continua.SetActive(true);
        ready = false;
        while (ready == false)
        {
            yield return null;
        }
        //////////////////////////////////////////////////////////////////queen
        CleanBoard();
        BoardController.ChessBoardState[0, 3] = new PieceState
        {
            piece = StartingPositions.white_queen,
            pieceType = PieceType.queen,
            postion = new Vector2Int(0, 3),
            isWhite = true
        };
        panel.SetActive(true);
        frontText.SetActive(false);
        continua.SetActive(false);
        for (int i = 0; i < StartingPositions.bases.GetLength(0); ++i)
        {
            for (int j = 0; j < StartingPositions.bases.GetLength(1); ++j)
            {
                StartingPositions.bases[i, j].SetActive(false);
                StartingPositions.emptyBases[i, j].SetActive(false);
            }
        }      
        StartingPositions.white_queen.SetActive(true);
        BoardController.ChessBoardState[7, 3] = new PieceState
        {
            piece = StartingPositions.black_queen,
            pieceType = PieceType.queen,
            postion = new Vector2Int(7, 3),
            isWhite = false
        };
        MovingAnimation.AnimateAndMovePiece(StartingPositions.black_queen, CalculateMoves.finalpos.transform.position + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[7, 3]);
        StartingPositions.white_rooks[0].SetActive(false);
        sideText.GetComponent<TextMeshProUGUI>().text = "La regina si muove in tutte le direzioni: in diagonale come l'alfiere e in orizzontale e verticale come la torre. Clicca sulla regina.";
        BoardController.ChessBoardState[5, 5] = BoardController.ChessBoardState[7, 3];
        BoardController.ChessBoardState[7, 3] = emptyState;
        CalculateMoves.found = false;
        while (!CalculateMoves.found)
        {
            yield return null;
        }
        sideText.GetComponent<TextMeshProUGUI>().text = "Prova a catturare la regina avversaria.";
        MovingAnimation.AnimateAndMovePiece(StartingPositions.black_queen, BoardController.gridPositions[7, 3] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[5, 5]);
        CalculateMoves.foundCapture = false;
        while (!CalculateMoves.foundCapture)
        {
            yield return null;
        }
        sideText.GetComponent<TextMeshProUGUI>().text = "";
        yield return new WaitForSeconds(3f);
        panel.SetActive(false);
        frontText.GetComponent<TextMeshProUGUI>().text = "Ora sai come si muove e come cattura la regina, procedi con il tutorial per imparare a muovere il re.";
        frontText.SetActive(true);
        yield return new WaitForSeconds(1f);
        continua.SetActive(true);
        ready = false;
        while (ready == false)
        {
            yield return null;
        }
        //////////////////////////////////////////////////////////////////re
        CleanBoard();
        BoardController.ChessBoardState[0, 4] = new PieceState
        {
            piece = StartingPositions.white_king,
            pieceType = PieceType.king,
            postion = new Vector2Int(0, 4),
            isWhite = true
        };
        panel.SetActive(true);
        frontText.SetActive(false);
        continua.SetActive(false);
        for (int i = 0; i < StartingPositions.bases.GetLength(0); ++i)
        {
            for (int j = 0; j < StartingPositions.bases.GetLength(1); ++j)
            {
                StartingPositions.bases[i, j].SetActive(false);
                StartingPositions.emptyBases[i, j].SetActive(false);
            }
        }
        StartingPositions.white_king.SetActive(true);
        BoardController.ChessBoardState[7, 3] = new PieceState
        {
            piece = StartingPositions.black_queen,
            pieceType = PieceType.queen,
            postion = new Vector2Int(7, 3),
            isWhite = false
        };
        MovingAnimation.AnimateAndMovePiece(StartingPositions.black_queen, CalculateMoves.finalpos.transform.position + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[7, 3]);
        StartingPositions.white_queen.SetActive(false);
        sideText.GetComponent<TextMeshProUGUI>().text = "Il re è l'obiettivo da catturare per poter vincere la partita. Si muove in tutte le direzioni ma solo di una casella. Clicca sul re.";
        BoardController.ChessBoardState[5, 5] = emptyState;
        CalculateMoves.found = false;
        while (!CalculateMoves.found)
        {
            yield return null;
        }
        sideText.GetComponent<TextMeshProUGUI>().text = "Prova a catturare la regina.";
        CalculateMoves.foundBase = false;
        while (!CalculateMoves.foundBase)
        {
            yield return null;
        }
        MovingAnimation.AnimateAndMovePiece(StartingPositions.black_queen, BoardController.gridPositions[7, 3] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[3, 3]);
        BoardController.ChessBoardState[3, 3] = BoardController.ChessBoardState[7, 3];
        BoardController.ChessBoardState[7, 3] = emptyState;
        CalculateMoves.foundCapture = false;
        while (!CalculateMoves.foundCapture)
        {
            yield return null;
        }
        sideText.GetComponent<TextMeshProUGUI>().text = "";
        yield return new WaitForSeconds(3f);
        panel.SetActive(false);
        frontText.GetComponent<TextMeshProUGUI>().text = "Ora sai come si muove e come cattura il re, procedi con il tutorial per imparare l'arrocco.";
        frontText.SetActive(true);
        yield return new WaitForSeconds(1f);
        continua.SetActive(true);
        ready = false;
        while (ready == false)
        {
            yield return null;
        }
        //////////////////////////////////////////////////////////////////arrocco
        CleanBoard();
        BoardController.ChessBoardState[0, 4] = new PieceState
        {
            piece = StartingPositions.white_king,
            pieceType = PieceType.king,
            postion = new Vector2Int(0, 4),
            isWhite = true
        };
        panel.SetActive(true);
        frontText.SetActive(false);
        continua.SetActive(false);
        for (int i = 0; i < StartingPositions.bases.GetLength(0); ++i)
        {
            for (int j = 0; j < StartingPositions.bases.GetLength(1); ++j)
            {
                StartingPositions.bases[i, j].SetActive(false);
                StartingPositions.emptyBases[i, j].SetActive(false);
            }
        }
        StartingPositions.white_king.SetActive(true);
        BoardController.ChessBoardState[3, 3] = emptyState;
        MovingAnimation.AnimateAndMovePiece(StartingPositions.white_king, BoardController.gridPositions[3, 3] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[0, 4]);
        StartingPositions.black_queen.SetActive(false);
        BoardController.ChessBoardState[0, 7] = new PieceState
        {
            piece = StartingPositions.white_rooks[1],
            pieceType = PieceType.rook,
            postion = new Vector2Int(0, 7),
            isWhite = true
        };
        CalculateMoves.movedKing[0] = false;
        StartingPositions.white_rooks[1].SetActive(true);
        sideText.GetComponent<TextMeshProUGUI>().text = "L'arrocco è una mossa difensiva per proteggere il re. Consiste in uno scambio di posizioni tra il re e una delle torri.";
        CalculateMoves.found = false;
        while (!CalculateMoves.found)
        {
            yield return null;
        }
        sideText.GetComponent<TextMeshProUGUI>().text = "Per arroccare è necesario che nè il re e nè la torre da arroccare abbiano effettuato alcuna mossa.";
        int[,] coords = { { 0, 5 }, { 1, 5 }, { 1, 4 }, { 1, 3 }, { 0, 3 } };
        CalculateMoves.foundBase = false;
        while (!CalculateMoves.foundBase)
        {
            for (int i = 0; i < coords.GetLength(0); i++)
            {
                StartingPositions.bases[coords[i, 0], coords[i, 1]].SetActive(false);
            }
            yield return null;
        }          
        sideText.GetComponent<TextMeshProUGUI>().text = "";
        yield return new WaitForSeconds(3f);
        panel.SetActive(false);
        frontText.GetComponent<TextMeshProUGUI>().text = "Ora sai come funziona l'arrocco, procedi con il tutorial per imparare la promozione del pedone";
        frontText.SetActive(true);
        yield return new WaitForSeconds(1f);
        continua.SetActive(true);
        ready = false;
        while (ready == false)
        {
            yield return null;
        }
        //////////////////////////////////////////////////////////////////promozione  
        BoardController.ChessBoardState[0, 6] = emptyState;
        BoardController.ChessBoardState[0, 5] = emptyState;
        StartingPositions.white_king.SetActive(false);
        StartingPositions.white_rooks[1].SetActive(false);
        BoardController.ChessBoardState[6, 4] = new PieceState
        {
            piece = StartingPositions.white_ponds[4],
            pieceType = PieceType.pawn,
            postion = new Vector2Int(6, 4),
            isWhite = true
        };
        StartingPositions.white_ponds[4].transform.position = BoardController.gridPositions[6, 4];
        StartingPositions.white_ponds[4].SetActive(true);
        panel.SetActive(true);
        frontText.SetActive(false);
        continua.SetActive(false);
        sideText.GetComponent<TextMeshProUGUI>().text = "Quando un pedone raggiunge l'ultimo lato della scacchiera, può essere promosso in un pezzo a tua scelta. Clicca sul pedone";
        sideText.SetActive(true);
        CalculateMoves.foundBase = false;
        while (!CalculateMoves.foundBase)
        {
            yield return null;
        }
        sideText.SetActive(false);
        CalculateMoves.foundPromotion = false;
        while (!CalculateMoves.foundPromotion)
        {
            yield return null;
        }
        yield return new WaitForSeconds(3f);
        panel.SetActive(false);
        frontText.GetComponent<TextMeshProUGUI>().text = "complimenti hai completato il tutorial!";
        continua.GetComponentInChildren<TextMeshProUGUI>().text = "menù";
        frontText.SetActive(true);
        yield return new WaitForSeconds(1f);
        continua.SetActive(true);
        ready = false;
        while (ready == false)
        {
            yield return null;
        }
        SceneManager.LoadScene(0);
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
