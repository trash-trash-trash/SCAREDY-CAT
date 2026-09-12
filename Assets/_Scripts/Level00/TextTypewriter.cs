using System.Collections;
using TMPro;
using UnityEngine;

public class TextTypewriter : MonoBehaviour
{
    public TMP_Text text;
    public float speed = 0.05f;

    public void Type(string message)
    {
        StartCoroutine(TypeCoro(message));
    }

    IEnumerator TypeCoro(string message)
    {
        text.text = "";

        foreach (char letter in message)
        {
            text.text += letter;
            yield return new WaitForSeconds(speed);
        }
    }
}