using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class HitPopup : MonoBehaviour
{
    private TextMeshPro _textMesh;

    private void Awake()
    {
        _textMesh = GetComponent<TextMeshPro>();
    }

    public void SetupText(string text, Color textColor)
    {
        _textMesh.SetText(text);
        //_textMesh.color = textColor;
    }
}
