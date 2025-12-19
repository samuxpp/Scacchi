using UnityEngine;
using UnityEngine.UI;

public class SpriteLoader : MonoBehaviour
{
    public Sprite white;
    public Sprite black;
    private Image myImage;

    void Awake()
    {
        myImage = GetComponent<Image>();
    }
    public void SetIcon(bool isWhite)
    {
        if (isWhite)
        {
            myImage.sprite = white;
        }
        else
        {
            myImage.sprite = black;
        }           
    }
}