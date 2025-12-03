using UnityEngine;

public class StartingPositions : MonoBehaviour
{
    public BoardController BoardController;
	public GameObject[] white_ponds;
	public GameObject[] black_ponds;
	public GameObject[] white_rooks;
    public GameObject[] black_rooks;
    public GameObject[] white_knights;
	public GameObject[] black_knights;
	public GameObject[] white_bishops;
	public GameObject[] black_bishops;
	public GameObject white_queen;
	public GameObject black_queen;
	public GameObject white_king;
	public GameObject black_king;

	void Start()
    {
        
    }

    void Update()
    {
        for (int i = 0; i < BoardController.gridPositions.GetLength(0); ++i)
        {
            white_ponds[i].transform.position = BoardController.gridPositions[1, i];
            black_ponds[i].transform.position = BoardController.gridPositions[6, i];
        }
		//rooks
		white_rooks[0].transform.position = BoardController.gridPositions[0, 0];
		white_rooks[1].transform.position = BoardController.gridPositions[0, 7];

		black_rooks[0].transform.position = BoardController.gridPositions[7, 0];
		black_rooks[1].transform.position = BoardController.gridPositions[7, 7];
		//knights
		white_knights[0].transform.position = BoardController.gridPositions[0, 1];
		white_knights[1].transform.position = BoardController.gridPositions[0, 6];

		black_knights[0].transform.position = BoardController.gridPositions[7, 1];
		black_knights[1].transform.position = BoardController.gridPositions[7, 6];
		//bishops
		white_bishops[0].transform.position = BoardController.gridPositions[0, 2];
		white_bishops[1].transform.position = BoardController.gridPositions[0, 5];

		black_bishops[0].transform.position = BoardController.gridPositions[7, 2];
		black_bishops[1].transform.position = BoardController.gridPositions[7, 5];
		//queens
		white_queen.transform.position = BoardController.gridPositions[0, 3];
		black_queen.transform.position = BoardController.gridPositions[7, 3];
		//kings
		white_king.transform.position = BoardController.gridPositions[0, 4];
		black_king.transform.position = BoardController.gridPositions[7, 4];
	}
}
