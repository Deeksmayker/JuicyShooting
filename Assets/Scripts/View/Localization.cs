using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Localization : MonoBehaviour
{
    public static UnityEvent LanguageChanged = new();

    private void Start()
    {
        LanguageChanged.Invoke();
    }
    
    public void SetLanguageToRussian()
    {
        LanguageInfo.Language = LanguageInfo.Languages.Russian;
        LanguageChanged.Invoke();
    }

    public void SetLanguageToEnglish()
    {
        LanguageInfo.Language = LanguageInfo.Languages.English;
        LanguageChanged.Invoke();
    }
}

public static class LanguageInfo
{
    public enum Languages
    {
        English,
        Russian
    }
    
    public static Languages Language = Languages.Russian;
}