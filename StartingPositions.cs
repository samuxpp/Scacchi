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

	void Update()
    {
        
    }

    public void SetPositions()
    {
		for (int i = 0; i < BoardController.gridPositions.GetLength(0); ++i)
        {
            white_ponds[i].transform.position = BoardController.gridPositions[1, i];
			BoardController.ChessBoardState[1, i] = new PieceState
			{
				piece = white_ponds[i],
				postion = new Vector2Int(1, i),
				isWhite = true
			};
			black_ponds[i].transform.position = BoardController.gridPositions[6, i];
			BoardController.ChessBoardState[6, i] = new PieceState
			{
				piece = black_ponds[i],
				postion = new Vector2Int(6, i),
				isWhite = false
			};
		}
		//rooks
		white_rooks[0].transform.position = BoardController.gridPositions[0, 0];
		BoardController.ChessBoardState[0, 0] = new PieceState
		{
			piece = white_rooks[0],
			postion = new Vector2Int(0, 0),
			isWhite = true
		};
		white_rooks[1].transform.position = BoardController.gridPositions[0, 7];
		BoardController.ChessBoardState[0, 7] = new PieceState
		{
			piece = white_rooks[1],
			postion = new Vector2Int(0, 7),
			isWhite = true
		};
		black_rooks[0].transform.position = BoardController.gridPositions[7, 0];
		BoardController.ChessBoardState[7, 0] = new PieceState
		{
			piece = black_rooks[0],
			postion = new Vector2Int(7, 0),
			isWhite = false
		};
		black_rooks[1].transform.position = BoardController.gridPositions[7, 7];
		BoardController.ChessBoardState[7, 7] = new PieceState
		{
			piece = black_rooks[1],
			postion = new Vector2Int(7, 7),
			isWhite = false
		};
		//knights
		white_knights[0].transform.position = BoardController.gridPositions[0, 1];
		BoardController.ChessBoardState[0, 1] = new PieceState
		{
			piece = white_knights[0],
			postion = new Vector2Int(0, 1),
			isWhite = true
		};
		white_knights[1].transform.position = BoardController.gridPositions[0, 6];
		BoardController.ChessBoardState[0, 6] = new PieceState
		{
			piece = white_knights[1],
			postion = new Vector2Int(0, 6),
			isWhite = true
		};
		black_knights[0].transform.position = BoardController.gridPositions[7, 1];
		BoardController.ChessBoardState[7, 1] = new PieceState
		{
			piece = black_knights[0],
			postion = new Vector2Int(7, 1),
			isWhite = false
		};
		black_knights[1].transform.position = BoardController.gridPositions[7, 6];
		BoardController.ChessBoardState[7, 6] = new PieceState
		{
			piece = white_knights[1],
			postion = new Vector2Int(7, 6),
			isWhite = false
		};
		//bishops
		white_bishops[0].transform.position = BoardController.gridPositions[0, 2];
		BoardController.ChessBoardState[0, 2] = new PieceState
		{
			piece = white_bishops[0],
			postion = new Vector2Int(0, 2),
			isWhite = true
		};
		white_bishops[1].transform.position = BoardController.gridPositions[0, 5];
		BoardController.ChessBoardState[0, 5] = new PieceState
		{
			piece = white_bishops[1],
			postion = new Vector2Int(0, 5),
			isWhite = true
		};
		black_bishops[0].transform.position = BoardController.gridPositions[7, 2];
		BoardController.ChessBoardState[7, 2] = new PieceState
		{
			piece = black_bishops[0],
			postion = new Vector2Int(7, 2),
			isWhite = false
		};
		black_bishops[1].transform.position = BoardController.gridPositions[7, 5];
		BoardController.ChessBoardState[7, 5] = new PieceState
		{
			piece = black_bishops[1],
			postion = new Vector2Int(7, 5),
			isWhite = false
		};
		//queens
		white_queen.transform.position = BoardController.gridPositions[0, 3];
		BoardController.ChessBoardState[0, 3] = new PieceState
		{
			piece = white_queen,
			postion = new Vector2Int(0, 3),
			isWhite = true
		};
		black_queen.transform.position = BoardController.gridPositions[7, 3];
		BoardController.ChessBoardState[7, 3] = new PieceState
		{
			piece = black_queen,
			postion = new Vector2Int(7, 3),
			isWhite = false
		};
		//kings
		white_king.transform.position = BoardController.gridPositions[0, 4];
		BoardController.ChessBoardState[0, 4] = new PieceState
		{
			piece = white_king,
			postion = new Vector2Int(0, 4),
			isWhite = true
		};
		black_king.transform.position = BoardController.gridPositions[7, 4];
		BoardController.ChessBoardState[7, 4] = new PieceState
		{
			piece = black_king,
			postion = new Vector2Int(7, 4),
			isWhite = false
		};
	}
}
