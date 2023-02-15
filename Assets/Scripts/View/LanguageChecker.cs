using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LanguageChecker : MonoBehaviour
{
    [SerializeField] private string russianText, englishText;

    private void Start()
    {
        UpdateLanguage();
        
        Localization.LanguageChanged.AddListener(UpdateLanguage);
    }

    public void UpdateLanguage()
    {
        GetComponent<TextMeshProUGUI>().text =
            LanguageInfo.Language == LanguageInfo.Languages.Russian ? russianText : englishText;
    }
}
