using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class BoardController : MonoBehaviour
{
	public StartingPositions StartingPositions;
	public PieceState[,] ChessBoardState = new PieceState[8, 8];
    private const int board_Size = 8;
    public GameObject visualBoardPlane;
    public Vector3[,] gridPositions = new Vector3[board_Size, board_Size];

    private void Awake()
    {
		StartingPositions = GetComponent<StartingPositions>();
	}

    public void Start()
    {
        GenerateLogicalGrid();
        StartingPositions.SetPositions();
    }

    void GenerateLogicalGrid()
    {       
        Bounds bounds = visualBoardPlane.GetComponent<Renderer>().bounds;
        float boardWidth = bounds.size.x;
        float boardDepth = bounds.size.z;

        float squareSizeX = boardWidth / board_Size;
        float squareSizeZ = boardDepth / board_Size;

        Vector3 origin = bounds.min;

        for (int r = 0; r < board_Size; r++)
        {
            for (int c = 0; c < board_Size; c++)
            {
                float xPos = origin.x + (c * squareSizeX) + (squareSizeX / 2f);
                float yPos = bounds.max.y + 0.05f;
                float zPos = origin.z + (r * squareSizeZ) + (squareSizeZ / 2f);
                gridPositions[r, c] = new Vector3(xPos, yPos, zPos);
            }
        }
    }
    public void OnSelect(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            HandleMouseClick();
        }
    }
    private void HandleMouseClick()
    {
        if (Mouse.current == null) return;
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            GameObject clickedObject = hit.collider.gameObject;
            Debug.Log("Oggetto selezionato dal nuovo Input System: " + clickedObject.name);
        }
    }

    private void OnDrawGizmos()
    {
        if (gridPositions == null || gridPositions[0, 0] == Vector3.zero)
        {
            return;
        }

        Gizmos.color = Color.yellow;

        for (int r = 0; r < 8; r++)
        {
            for (int c = 0; c < 8; c++)
            {
                Vector3 position = gridPositions[r, c];
                Gizmos.DrawSphere(position, 0.2f);
            }
        }
    }
}