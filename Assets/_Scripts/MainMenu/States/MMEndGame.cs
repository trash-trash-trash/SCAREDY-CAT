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

    public GameObject returnToMenuButton;
    
    private Coroutine fadeCoroutine;

    public override void OnEnable()
    {
        base.OnEnable();
        
        Debug.Log("END GAME STATE");

        mainMenu.finishedGame = true;

        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        // Start completely invisible
        Color imageColor = finalImage.color;
        imageColor.a = 0f;
        finalImage.color = imageColor;

        Color finalTextColor = finalText.color;
        finalTextColor.a = 0f;
        finalText.color = finalTextColor;

        Color endTextColor = theEndText.color;
        endTextColor.a = 0f;
        theEndText.color = endTextColor;

        finalImageObj.SetActive(true);

        fadeCoroutine = StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        // IMAGE
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            Color color = finalImage.color;
            color.a = Mathf.Clamp01(time / fadeDuration);
            finalImage.color = color;

            yield return null;
        }

        yield return new WaitForSeconds(delayBetween);

        // FINAL TEXT
        time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            Color color = finalText.color;
            color.a = Mathf.Clamp01(time / fadeDuration);
            finalText.color = color;

            yield return null;
        }

        yield return new WaitForSeconds(delayBetween);

        // THE END
        time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            Color color = theEndText.color;
            color.a = Mathf.Clamp01(time / fadeDuration);
            theEndText.color = color;

            yield return null;
        }

        returnToMenuButton.SetActive(true);
    }

    private void OnDisable()
    {
        returnToMenuButton.SetActive(false);
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        if(finalImageObj != null)
            finalImageObj.SetActive(false);
    }
}