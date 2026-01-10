using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public MenuOrbit MenuOrbit;
    public GameObject mainCamera;
    public GameObject pivot;
    private float moveDuration = 1f;
    private Vector3 pos;
    public GameObject canvas;
    public Image logoImage;

    private Camera mainCam;

    void Awake()
    {
        mainCam = Camera.main;
        canvas.SetActive(true);
    }

    public void Start()
    {
        Outline outl = logoImage.gameObject.GetComponent<Outline>();
        if (outl == null) outl = logoImage.gameObject.AddComponent<Outline>();
        outl.effectColor = Color.black;
        outl.effectDistance = new Vector2(2f, -2f);
    }

    public void Tutorial()
    {
        MenuOrbit.orbit = false;
        pos = mainCam.transform.position;
        canvas.SetActive(false);
        StartCoroutine(MovePieceRoutineTUT(pos, new Vector3(0, 7.07f, -7.45f)));
    }
    public void Aperture()
    {
        MenuOrbit.orbit = false;
        pos = mainCam.transform.position;
        canvas.SetActive(false);
        StartCoroutine(MovePieceRoutineAP(pos, new Vector3(0, 7.07f, -7.45f)));
    }
    public void Puzzle()
    {
        MenuOrbit.orbit = false;
        pos = mainCam.transform.position;
        canvas.SetActive(false);
        StartCoroutine(MovePieceRoutinePuzzle(pos, new Vector3(0, 7.07f, -7.45f)));
    }

    private IEnumerator MovePieceRoutineTUT(Vector3 startPos, Vector3 endPos)
    {
        float elapsedTime = 0f;
        Quaternion startRotation = pivot.transform.rotation;
        Transform cameraTransform = mainCamera.transform;
        Transform pivotTransform = pivot.transform;
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveDuration);
            float smoothT = t * t * (3f - 2f * t);
            cameraTransform.position = Vector3.Lerp(startPos, endPos, t);
            pivotTransform.rotation = Quaternion.Slerp(startRotation, Quaternion.identity, smoothT);
            yield return null;
        }
        cameraTransform.position = endPos;
        pivotTransform.rotation = Quaternion.identity;
        SceneManager.LoadScene(1);
    }
    private IEnumerator MovePieceRoutineAP(Vector3 startPos, Vector3 endPos)
    {
        float elapsedTime = 0f;
        Quaternion startRotation = pivot.transform.rotation;
        Transform cameraTransform = mainCamera.transform;
        Transform pivotTransform = pivot.transform;
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveDuration);
            float smoothT = t * t * (3f - 2f * t);
            cameraTransform.position = Vector3.Lerp(startPos, endPos, t);
            pivotTransform.rotation = Quaternion.Slerp(startRotation, Quaternion.identity, smoothT);
            yield return null;
        }
        cameraTransform.position = endPos;
        pivotTransform.rotation = Quaternion.identity;
        SceneManager.LoadScene(2);
    }
    private IEnumerator MovePieceRoutinePuzzle(Vector3 startPos, Vector3 endPos)
    {
        float elapsedTime = 0f;
        Quaternion startRotation = pivot.transform.rotation;
        Transform cameraTransform = mainCamera.transform;
        Transform pivotTransform = pivot.transform;
        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveDuration);
            float smoothT = t * t * (3f - 2f * t);
            cameraTransform.position = Vector3.Lerp(startPos, endPos, t);
            pivotTransform.rotation = Quaternion.Slerp(startRotation, Quaternion.identity, smoothT);
            yield return null;
        }
        cameraTransform.position = endPos;
        pivotTransform.rotation = Quaternion.identity;
        SceneManager.LoadScene(3);
    }
}