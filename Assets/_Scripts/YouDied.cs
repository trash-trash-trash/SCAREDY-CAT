using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class YouDied : MonoBehaviour
{
    [System.Serializable]
    public class FadeGroup
    {
        public Image[] images;
        public TMP_Text[] texts;
    }

    public GameObject youDiedObj;
    public PlayerBrain playerBrain;
    public TMP_Text lifeTrackerText;

    public GameObject gameOverButton;
    public GameObject resetButton;
    
    [SerializeField] private Image backgroundImage;
    [SerializeField] private float mainFadeDuration = 2f;

    [Header("Other UI")]
    [SerializeField] private FadeGroup[] fadeGroups;
    [SerializeField] private float groupFadeDuration = 1f;
    [SerializeField] private float delayBetweenGroups = 0.5f;
    
    public void BENDROWNED()
    {
        lifeTrackerText.text = playerBrain.playerLives.currentLives.ToString();
        StartCoroutine(FadeIn());
    }

    public void BENGOTCPR()
    {
        youDiedObj.SetActive(false);
    }
    
    private IEnumerator FadeIn()
    {
        // Start everything invisible
        SetAlpha(backgroundImage, 0f);

        youDiedObj.SetActive(true);
        foreach (FadeGroup group in fadeGroups)
        {
            foreach (Image image in group.images)
                SetAlpha(image, 0f);

            foreach (TMP_Text text in group.texts)
                SetAlpha(text, 0f);
        }

        // Fade in the main image
        yield return StartCoroutine(
            FadeImage(backgroundImage, 0f, 1f, mainFadeDuration)
        );

        // Fade in the other groups
        foreach (FadeGroup group in fadeGroups)
        {
            yield return StartCoroutine(
                FadeGroupIn(group)
            );

            yield return new WaitForSeconds(delayBetweenGroups);
        }
    }

    private IEnumerator FadeGroupIn(FadeGroup group)
    {
        float elapsed = 0f;

        while (elapsed < groupFadeDuration)
        {
            elapsed += Time.deltaTime;

            float alpha = Mathf.Clamp01(
                elapsed / groupFadeDuration
            );

            foreach (Image image in group.images)
                SetAlpha(image, alpha);

            foreach (TMP_Text text in group.texts)
                SetAlpha(text, alpha);

            yield return null;
        }

        foreach (Image image in group.images)
            SetAlpha(image, 1f);

        foreach (TMP_Text text in group.texts)
            SetAlpha(text, 1f);
        
        playerBrain.playerLives.LoseALife();
        lifeTrackerText.text = playerBrain.playerLives.currentLives.ToString();

        if (playerBrain.playerLives.currentLives <= 0)
        {
            gameOverButton.SetActive(true);
            resetButton.SetActive(false);
        }
    }

    private IEnumerator FadeImage(
        Image image,
        float from,
        float to,
        float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float progress = Mathf.Clamp01(
                elapsed / duration
            );

            SetAlpha(
                image,
                Mathf.Lerp(from, to, progress)
            );

            yield return null;
        }

        SetAlpha(image, to);
    }

    private void SetAlpha(Image image, float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }

    private void SetAlpha(TMP_Text text, float alpha)
    {
        Color color = text.color;
        color.a = alpha;
        text.color = color;
    }
}
