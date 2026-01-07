using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerAperture : MonoBehaviour
{
    private bool ready = false;
    public GameObject sideText;
    private CalculateMoves CalculateMoves;
    private BoardController BoardController;
    private StartingPositions StartingPositions;
    private MovingAnimation MovingAnimation;
    private int a;
    private int b;
    public void Continua()
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
        StartCoroutine(Aperture());
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
        ready = false;
        while (ready == false)
        {
            yield return null;
        }
        sideText.GetComponent<TextMeshProUGUI>().text = "la prima mossa è e4, quindi bisogna muovere il pedone bianco dalla casella e2 alla casella e4. la strategia iniziale è sempre quello di conquistare il centro della scacchiera. clicca sul pedone";
        a = 1;
        b = 4;
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

    }
}
