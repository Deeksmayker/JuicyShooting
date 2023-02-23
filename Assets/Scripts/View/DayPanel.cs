using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayValue;
    
    //private Animation _animation;
    
    void Start()
    {
        //_animation = GetComponent<Animation>();
        dayValue.text = GameData.Instance.Level == 0 ? "??" : GameData.Instance.Level.ToString();
        
        //Invoke(nameof(PlayAnimation), 1);
    }

    private void PlayAnimation()
    {
        //_animation.Play();
    }
}
