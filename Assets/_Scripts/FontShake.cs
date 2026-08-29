using UnityEngine;
using TMPro;
public class FontShake : MonoBehaviour
{

    //suck on my underscores Lloyd
    public TMP_Text _textComponent;

    public float _shakeSpeed;
    public float _shakeAmount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _textComponent.ForceMeshUpdate();
        TMP_TextInfo textInfo = _textComponent.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];

            if (!charInfo.isVisible)
            {
                continue;
            }

            int materialIndex = charInfo.materialReferenceIndex;
            int vertexIndex = charInfo.vertexIndex;

            Vector3[] sourceVertices = textInfo.meshInfo[materialIndex].vertices;

            float seed = i * 1.5f;
            float offsetX = (Mathf.PerlinNoise(Time.time * _shakeSpeed, seed)) - 0;
            float offsetY = (Mathf.PerlinNoise(seed, Time.time * _shakeSpeed)) - 0;

            Vector3 offset = new Vector3(offsetX, offsetY, 0) * _shakeAmount;

            sourceVertices[vertexIndex + 0] += offset;
            sourceVertices[vertexIndex + 1] += offset;
            sourceVertices[vertexIndex + 2] += offset;
            sourceVertices[vertexIndex + 3] += offset;
        }

        for (int i = 0; (i < textInfo.meshInfo.Length); i++)
        {
            textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
            _textComponent.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
        }
    }
}
