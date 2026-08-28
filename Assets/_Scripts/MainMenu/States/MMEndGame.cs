using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MMEndGame : MainMenuStateBase
{
    public GameObject finalImageObj;
    public Image finalImage;
    public TMP_Text finalText;
    public TMP_Text theEndText;

    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float delayBetween = 0.25f;

    public override void OnEnable()
    {
        base.OnEnable();
        finalImageObj.SetActive(true);
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        SetAlpha(finalImage, 0f);
        SetAlpha(finalText, 0f);
        SetAlpha(theEndText, 0f);

        // Fade in image
        yield return FadeImage(finalImage);

        yield return new WaitForSeconds(delayBetween);

        // Fade in final text
        yield return FadeText(finalText);

        yield return new WaitForSeconds(delayBetween);

        // Fade in "The End"
        yield return FadeText(theEndText);
    }

    IEnumerator FadeImage(Image image)
    {
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Clamp01(time / fadeDuration);

            SetAlpha(image, alpha);

            yield return null;
        }

        SetAlpha(image, 1f);
    }

    IEnumerator FadeText(TMP_Text text)
    {
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Clamp01(time / fadeDuration);

            SetAlpha(text, alpha);

            yield return null;
        }

        SetAlpha(text, 1f);
    }

    void SetAlpha(Image image, float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }

    void SetAlpha(TMP_Text text, float alpha)
    {
        Color color = text.color;
        color.a = alpha;
        text.color = color;
    }
}