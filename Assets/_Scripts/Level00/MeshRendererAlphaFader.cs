using UnityEngine;

public class MeshRendererAlphaFader : MonoBehaviour
{
    public MeshRenderer[] meshRenderers;
    public float alphaSpeed = 1f;

    public void ChangeAlpha(float targetAlpha)
    {
        StopAllCoroutines();
        StartCoroutine(ChangeAlphaCoro(targetAlpha));
    }

    private System.Collections.IEnumerator ChangeAlphaCoro(float targetAlpha)
    {
        targetAlpha = Mathf.Clamp01(targetAlpha);

        while (true)
        {
            bool finished = true;

            foreach (MeshRenderer meshRenderer in meshRenderers)
            {
                foreach (Material material in meshRenderer.materials)
                {
                    Color color = material.color;
                    color.a = Mathf.MoveTowards(color.a, targetAlpha, alphaSpeed * Time.deltaTime);
                    material.color = color;

                    if (!Mathf.Approximately(color.a, targetAlpha))
                        finished = false;
                }
            }

            if (finished)
                yield break;

            yield return null;
        }
    }
}