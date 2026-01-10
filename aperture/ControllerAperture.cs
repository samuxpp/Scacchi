using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ControllerAperture : MonoBehaviour
{
    private bool ready = false;
    public GameObject sideText;
    public GameObject continua;
    public GameObject continua2;
    public GameObject frontText;
    public GameObject panel;
    private Vector3 pos;
    private Camera mainCam;
    public GameObject pivot;
    public GameObject mainCamera;
    private CalculateMoves CalculateMoves;
    private BoardController BoardController;
    private StartingPositions StartingPositions;
    private MovingAnimation MovingAnimation;
    public PieceState emptyState = new PieceState();
    private int a = 5;
    private int b = 5;
    private Badge Badge;
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
        StartCoroutine(Aperture());
        BoardController.enable = false;
    }
    public void OnSelect(InputAction.CallbackContext context)
    {
        if (!BoardController.enable)
        {
            if (context.performed)
            {
                BoardController.clickedobject = BoardController.HandleMouseClick();
                if (BoardController.clickedobject != null)
                {
                    if (BoardController.clickedobject == BoardController.ChessBoardState[a, b].piece || BoardController.clickedobject == StartingPositions.bases[a, b])
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
                        BoardController.ShowMoves();
                    }
                }
            }
        }
    }
    private IEnumerator Aperture()
    {
        yield return new WaitUntil(() => BoardController.isInitialized == true);
        //////////////////////////////////////////////////////////////////italiana
        ready = false;
        while (ready == false)
        {
            yield return null;
        }
        continua.SetActive(false);
        sideText.GetComponent<TextMeshProUGUI>().text = "la prima mossa è e4, quindi bisogna muovere il pedone bianco dalla casella e2 alla casella e4. la strategia iniziale è sempre quello di conquistare il centro della scacchiera. clicca sul pedone";
        a = 1;b = 4;
        CalculateMoves.found = false;
        while (!CalculateMoves.found)
        {
            yield return null;
        }
        sideText.GetComponent<TextMeshProUGUI>().text = "muovi il pedone in e4";
        a = 3;
        StartingPositions.bases[2, 4].SetActive(false);
        CalculateMoves.foundBase = false;
        while (!CalculateMoves.foundBase)
        {
            yield return null;
        }
        sideText.GetComponent<TextMeshProUGUI>().text = "L'avversario muove il pedone in e5; adesso attaccalo muovendo il cavallo in f3";
        yield return new WaitForSeconds(3f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[6, 4].piece, BoardController.gridPositions[6, 4] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[4, 4]);
        BoardController.ChessBoardState[4, 4] = BoardController.ChessBoardState[6, 4];
        BoardController.ChessBoardState[6, 4] = emptyState;
        a = 0;b = 6;
        CalculateMoves.found = false;
        while (!CalculateMoves.found)
        {
            yield return null;
        }
        StartingPositions.bases[2, 7].SetActive(false);
        StartingPositions.bases[1, 4].SetActive(false);
        a = 2;b = 5;
        CalculateMoves.foundBase = false;
        while (!CalculateMoves.foundBase)
        {
            yield return null;
        }
        sideText.GetComponent<TextMeshProUGUI>().text = "L'avversario muove il cavallo in c6 per difendere il suo pedone; muovi l'alfiere campochiaro in c4 per conquistare il centro.";
        yield return new WaitForSeconds(3f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 1].piece, BoardController.gridPositions[7, 1] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[5, 2]);
        BoardController.ChessBoardState[5, 2] = BoardController.ChessBoardState[7, 1];
        BoardController.ChessBoardState[7, 1] = emptyState;
        a = 0; b = 5;
        CalculateMoves.found = false;
        while (!CalculateMoves.found)
        {
            yield return null;
        }
        StartingPositions.bases[1, 4].SetActive(false);
        StartingPositions.bases[2, 3].SetActive(false);
        StartingPositions.bases[4, 1].SetActive(false);
        StartingPositions.bases[5, 0].SetActive(false);
        a = 3; b = 2;
        CalculateMoves.foundBase = false;
        while (!CalculateMoves.foundBase)
        {
            yield return null;
        }
        sideText.GetComponent<TextMeshProUGUI>().text = "questa è l'apertura italiana che ti permette di iniziare la partita con un leggero vantaggio posizionale";
        a = 3; b = 7;
        yield return new WaitForSeconds(3f);
        continua.SetActive(true);
        ready = false;
        while (ready == false)
        {
            yield return null;
        }
        panel.SetActive(false);
        sideText.SetActive(false);
        continua.SetActive(false);
        frontText.SetActive(true);
        yield return new WaitForSeconds(1f);
        continua2.SetActive(true);
        //////////////////////////////////////////////////////////////////london
        ready = false;
        while (ready == false)
        {
            yield return null;
        }
        BoardController.ResetChessBoard();
        frontText.SetActive(false);
        continua2.SetActive(false);
        panel.SetActive(true);
        sideText.SetActive(true);
        sideText.GetComponent<TextMeshProUGUI>().text = "la london è un'apertura che può essere usata sempre e non dipende dalle mosse dell'avversario";
        yield return new WaitForSeconds(1f);
        continua.SetActive(true);
        ready = false;
        while (ready == false)
        {
            yield return null;
        }
        continua.SetActive(false);
        sideText.GetComponent<TextMeshProUGUI>().text = "la prima mossa è pedone d4";
        a = 1;b = 3;
        CalculateMoves.found = false;
        while (!CalculateMoves.found)
        {
            yield return null;
        }
        a = 3; b = 3;
        StartingPositions.bases[2, 3].SetActive(false);
        CalculateMoves.foundBase = false;
        while (!CalculateMoves.foundBase)
        {
            yield return null;
        }
        sideText.GetComponent<TextMeshProUGUI>().text = "L'avversario muove il pedone in g5, sviluppa il tuo cavallo in f3 per conquistare il centro";
        yield return new WaitForSeconds(3f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[6, 6].piece, BoardController.gridPositions[6, 6] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[5, 6]);
        BoardController.ChessBoardState[5, 6] = BoardController.ChessBoardState[6, 6];
        BoardController.ChessBoardState[6, 6] = emptyState;
        a = 0; b = 6;
        CalculateMoves.found = false;
        while (!CalculateMoves.found)
        {
            yield return null;
        }
        a = 2; b = 5;
        StartingPositions.bases[2, 7].SetActive(false);
        CalculateMoves.foundBase = false;
        while (!CalculateMoves.foundBase)
        {
            yield return null;
        }
        sideText.GetComponent<TextMeshProUGUI>().text = "L'avversario muove il cavallo in f6, sviluppa il tuo alfiere in f4";
        yield return new WaitForSeconds(3f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[7, 6].piece, BoardController.gridPositions[7, 6] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[5, 5]);
        BoardController.ChessBoardState[5, 5] = BoardController.ChessBoardState[7, 6];
        BoardController.ChessBoardState[7, 6] = emptyState;
        a = 0; b = 2;
        CalculateMoves.found = false;
        while (!CalculateMoves.found)
        {
            yield return null;
        }
        a = 3; b = 5;
        StartingPositions.bases[1, 3].SetActive(false);
        StartingPositions.bases[2, 4].SetActive(false);
        StartingPositions.bases[4, 6].SetActive(false);
        StartingPositions.bases[5, 7].SetActive(false);
        CalculateMoves.foundBase = false;
        while (!CalculateMoves.foundBase)
        {
            yield return null;
        }
        sideText.GetComponent<TextMeshProUGUI>().text = "queste sono le basi dell'apertura london, una delle aperture più semplici da giocare";
        yield return new WaitForSeconds(3f);
        continua.SetActive(true);
        ready = false;
        while (ready == false)
        {
            yield return null;
        }
        a = 3; b = 7;
        panel.SetActive(false);
        sideText.SetActive(false);
        frontText.SetActive(true);
        continua.SetActive(false);
        frontText.GetComponent<TextMeshProUGUI>().text = "ora conosci l'apertura london, procedi per imparare la difesa scandinava";
        yield return new WaitForSeconds(1f);
        continua2.SetActive(true);
        //////////////////////////////////////////////////////////////////scandinava
        BoardController.ResetChessBoard();
        ready = false;
        while (ready == false)
        {
            yield return null;
        }
        frontText.SetActive(false);
        continua2.SetActive(false);
        pos = mainCam.transform.position;
        StartCoroutine(MovePieceRoutine(pos, new Vector3(0, 7.07f, 7.45f)));
        panel.SetActive(true);
        sideText.SetActive(true);
        sideText.GetComponent<TextMeshProUGUI>().text = "La Difesa Scandinava è un'apertura per il Nero che, pur concedendo un lieve svantaggio teorico in apertura, pone le basi per un contrattacco aggressivo nelle fasi successive della partita";
        yield return new WaitForSeconds(1f);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[1, 4].piece, BoardController.gridPositions[1, 4] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[3, 4]);
        BoardController.ChessBoardState[3, 4] = BoardController.ChessBoardState[1, 4];
        BoardController.ChessBoardState[1, 4] = emptyState;
        continua.SetActive(true);
        ready = false;
        while (ready == false)
        {
            yield return null;
        }
        continua.SetActive(false);
        sideText.GetComponent<TextMeshProUGUI>().text = "Contro l'apertura pedone e4 del Bianco, la tua risposta sarà la spinta in d5, entrando immediatamente nelle linee della Difesa Scandinava";
        BoardController.blackTurn = true;
        a = 6; b = 3;
        CalculateMoves.found = false;
        while (!CalculateMoves.found)
        {
            yield return null;
        }
        StartingPositions.bases[5, 3].SetActive(false);
        CalculateMoves.foundBase = false;
        a = 4; b = 3;
        while (!CalculateMoves.foundBase)
        {
            yield return null;
        }
        yield return new WaitForSeconds(3f);
        sideText.GetComponent<TextMeshProUGUI>().text = "Il Bianco ha preso il tuo pedone. Rispondi alla mossa catturando il pezzo avversario con la Regina";
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[3, 4].piece, BoardController.gridPositions[3, 4] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[4, 3]);
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[4, 3].piece, BoardController.gridPositions[4, 3] + new Vector3(0f, 0.7f, 0f), CalculateMoves.finalpos.transform.position);
        BoardController.ChessBoardState[4, 3] = BoardController.ChessBoardState[3, 4];
        BoardController.ChessBoardState[3, 4] = emptyState;
        a = 7; b = 3;
        CalculateMoves.found = false;
        while (!CalculateMoves.found)
        {
            yield return null;
        }
        a = 4; b = 3;
        int[] active = { 6, 5, 3, 2 };
        foreach (int i in active)
        {
            StartingPositions.bases[i, 3].SetActive(false);
        }
        CalculateMoves.foundCapture = false;
        while (!CalculateMoves.foundCapture)
        {
            yield return null;
        }
        yield return new WaitForSeconds(3f);
        sideText.GetComponent<TextMeshProUGUI>().text = "Il Bianco attacca la tua regina con il suo cavallo, spostala in una casella più sicura ad esempio a5";
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[0, 1].piece, BoardController.gridPositions[0, 1] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[2, 2]);
        BoardController.ChessBoardState[2, 2] = BoardController.ChessBoardState[0, 1];
        BoardController.ChessBoardState[0, 1] = emptyState;
        CalculateMoves.found = false;
        while (!CalculateMoves.found)
        {
            yield return null;
        }
        a = 4; b = 0;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (!(i == a && j == b))
                {
                    StartingPositions.bases[i, j].SetActive(false);
                }
                StartingPositions.emptyBases[i, j].SetActive(false);
            }
        }
        CalculateMoves.foundBase = false;
        while (!CalculateMoves.foundBase)
        {
            yield return null;
        }
        yield return new WaitForSeconds(3f);
        sideText.GetComponent<TextMeshProUGUI>().text = "Il Bianco prende spazio al centro con d4. Gioca c6, questa mossa prepara un'uscita sicura per la tua Regina nelle fasi successive";
        MovingAnimation.AnimateAndMovePiece(BoardController.ChessBoardState[1, 3].piece, BoardController.gridPositions[1, 3] + new Vector3(0f, 0.7f, 0f), BoardController.gridPositions[3, 3]);
        BoardController.ChessBoardState[3, 3] = BoardController.ChessBoardState[1, 3];
        BoardController.ChessBoardState[1, 3] = emptyState;
        a = 6; b = 2;
        CalculateMoves.found = false;
        while (!CalculateMoves.found)
        {
            yield return null;
        }
        a = 5; b = 2;
        StartingPositions.bases[4, 2].SetActive(false);
        CalculateMoves.foundBase = false;
        while (!CalculateMoves.foundBase)
        {
            yield return null;
        }
        yield return new WaitForSeconds(3f);
        sideText.GetComponent<TextMeshProUGUI>().text = "queste sono le mosse basi della difesa scandinava, una delle risposte più utilizzare quando si gioca con il nero";
        yield return new WaitForSeconds(1f);
        continua.SetActive(true);
        ready = false;
        while (ready == false)
        {
            yield return null;
        }
        Badge.ShowAchievement();
        panel.SetActive(false);
        sideText.SetActive(false);    
        continua.SetActive(false);
        frontText.GetComponent<TextMeshProUGUI>().text = "complimenti hai completato il corso sulle aperture!";
        frontText.SetActive(true);
        yield return new WaitForSeconds(4f);
        continua2.SetActive(true);
        continua2.GetComponentInChildren<TextMeshProUGUI>().text = "menù";
        ready = false;
        while (ready == false)
        {
            yield return null;
        }
        SceneManager.LoadScene(0);
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
