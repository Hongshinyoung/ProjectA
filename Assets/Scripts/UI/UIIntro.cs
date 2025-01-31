using System.Collections;
using TMPro;
using UnityEngine;

public class UIIntro : UIBase
{
    [SerializeField] private TextMeshProUGUI touchToPlay;
    private bool isFadeIn = false;
    private float alpha;

    private void Awake()
    {
        StartCoroutine(FadeLoopText());
    }

    private IEnumerator FadeLoopText()
    {
        while (true)
        {
            if (isFadeIn)
            {
                yield return StartCoroutine(FadeInText());
            }
            else
            {
                yield return StartCoroutine(FadeOutText());
            }
        }
    }

    private IEnumerator FadeInText()
    {
        for (alpha = 1f; alpha >= 0f; alpha -= Time.deltaTime)
        {
            SetAlpha(alpha);
            yield return null;
        }
        SetAlpha(0f);
        isFadeIn = false;
    }

    private IEnumerator FadeOutText()
    {
        for (alpha = 0f; alpha <= 1f; alpha += Time.deltaTime)
        {
            SetAlpha(alpha);
            yield return null;
        }
        SetAlpha(1f);
        isFadeIn = true;
    }

    private void SetAlpha(float alpha)
    {
        Color color = touchToPlay.color;
        color.a = Mathf.Clamp01(alpha);
        touchToPlay.color = color;
    }
}