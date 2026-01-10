using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Badge : MonoBehaviour
{
    public RectTransform achievementPanel;
    private float onScreenTime = 5f;
    private float animationTime = 0.3f;
    private float offset = 420f;
    private Vector2 outOfScreenPos;
    private Vector2 onScreenPos;
    private bool isShowing = false;
    private float borderWidth = 3f;
    public Image badgeImage;


    void Start()
    {
        outOfScreenPos = achievementPanel.anchoredPosition;
        onScreenPos = outOfScreenPos + new Vector2(-offset, 0);
        achievementPanel.anchoredPosition = outOfScreenPos;


        Outline outline = GetComponent<Outline>();
        if (outline == null) outline = badgeImage.gameObject.AddComponent<Outline>();
        outline.effectColor = Color.black;
        outline.effectDistance = new Vector2(borderWidth, -borderWidth);
        Mask mask = badgeImage.gameObject.GetComponent<Mask>();
        if (mask == null) mask = badgeImage.gameObject.AddComponent<Mask>();
        mask.showMaskGraphic = true;

    }
    public void ShowAchievement()
    {
        if (isShowing) return;
        StartCoroutine(AchievementRoutine());
    }

    private IEnumerator AchievementRoutine()
    {
        isShowing = true;
        float timer = 0f;
        while (timer < animationTime)
        {
            timer += Time.deltaTime;
            float t = timer / animationTime;
            achievementPanel.anchoredPosition = Vector2.Lerp(outOfScreenPos, onScreenPos, t);
            yield return null;
        }
        achievementPanel.anchoredPosition = onScreenPos;
        yield return new WaitForSeconds(onScreenTime);
        timer = 0f;
        while (timer < animationTime)
        {
            timer += Time.deltaTime;
            float t = timer / animationTime;
            achievementPanel.anchoredPosition = Vector2.Lerp(onScreenPos, outOfScreenPos, t);
            yield return null;
        }
        achievementPanel.anchoredPosition = outOfScreenPos;
        isShowing = false;
    }
}