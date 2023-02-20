using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject dualPerkActivationPanel;
    [SerializeField] private Button dualActivationButton;
    [SerializeField] private TextMeshProUGUI dualUsesCount;

    [Header("Game End Panel")]
    [SerializeField] private GameObject gameEndPanel;
    
    [SerializeField] private TextMeshProUGUI loseText;
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private TextMeshProUGUI headshotMoneyText;
    [SerializeField] private TextMeshProUGUI killMoneyText;
    [SerializeField] private TextMeshProUGUI moneyValueText;

    [SerializeField] private Button doubleMoneyButton;
    [SerializeField] private Slider doubleMoneyTimerSlider;

    private int _startMoney;
    private int _additionalKillMoney;
    private int _additionalHeadshotMoney;

    private bool _needToSetCoinCounters;

    private DualPerkInGameHandler _dualPerkHandler;

    private void Start()
    {
        _dualPerkHandler = FindObjectOfType<DualPerkInGameHandler>();
        
        if (_dualPerkHandler == null || _dualPerkHandler.DualUsesCount == 0)
        {
            dualPerkActivationPanel.SetActive(false);
        }
        else
        {
            dualUsesCount.text = _dualPerkHandler.DualUsesCount.ToString();
        }

        _startMoney = GameData.Instance.Money;
        
        Enemy.EnemyDied.AddListener(() => _additionalKillMoney += GameData.Instance.MoneyByKill);
        Enemy.EnemyDiedByWeakPoint.AddListener(() => _additionalHeadshotMoney += GameData.Instance.MoneyByKillInWeakPoint);

        GameManager.OnWin.AddListener(HandleWin);
        GameManager.OnLose.AddListener(HandleLose);
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(0);
    }
        
    public void OnUseDualPerk()
    {
        dualActivationButton.interactable = false;
        dualUsesCount.text = _dualPerkHandler.DualUsesCount.ToString();

        if (_dualPerkHandler.DualUsesCount > 0)
            Invoke(nameof(EnableDualPerkPanel), GameData.Instance.PlayerDualPerk.GetCurrentDuration());
    }

    public void ShowRewardAd(int id)
    {
        Debug.LogWarning("No ad showing rn");
        HandleReward(id);
    }
    
    private void HandleReward(int id)
    {
        if (id == 1)
        {
            GameData.Instance.AddMoney(_additionalKillMoney + _additionalHeadshotMoney);
            _additionalHeadshotMoney *= 2;
            _additionalKillMoney *= 2;
            _needToSetCoinCounters = true;

            UpdateMoneyTexts();
        }

        /*if (id == 2)
        {
            SavesManager.Coins += _earnedCoins;
            YandexGame.savesData.coins = SavesManager.Coins;
            YandexGame.SaveProgress();

            _earnedCoins *= 2;
            additionalCoinCounterText.text = "+" + _earnedCoins.ToString();
            _needToSetCoinCounters = true;
        }*/
    }

    private void HandleWin()
    {
        winText.gameObject.SetActive(true);
        loseText.gameObject.SetActive(false);
        Invoke(nameof(OpenAndPrepareGameEndPanel), 1.5f);
    }

    private void HandleLose()
    {
        winText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(true);
        OpenAndPrepareGameEndPanel();
    }

    private void OpenAndPrepareGameEndPanel()
    {
        gameEndPanel.SetActive(true);
        if (GameData.Instance.Level == 0)
        {
            doubleMoneyButton.gameObject.SetActive(false);
            return;
        }
        doubleMoneyButton.gameObject.SetActive(true);
        UpdateMoneyTexts();
        StartCoroutine(SetCoinCounters());
    }

    private void UpdateMoneyTexts()
    {
        killMoneyText.text = $"+{_additionalKillMoney}";
        headshotMoneyText.text = $"+{_additionalHeadshotMoney}";
        moneyValueText.text = _startMoney.ToString();
    }

    private void EnableDualPerkPanel()
    {
        dualActivationButton.interactable = true;
    }
    
    private IEnumerator SetCoinCounters()
    {
        var timer = 1f;

        while (timer > 0 && !_needToSetCoinCounters)
        {
            timer -= Time.deltaTime / 10;
            doubleMoneyTimerSlider.value = timer;
            yield return null;
        }
        
        doubleMoneyButton.gameObject.SetActive(false);
        
        yield return new WaitForSeconds(1);

        while (_additionalHeadshotMoney > 0 || _additionalKillMoney > 0)
        {
            var killAddMoney = _additionalKillMoney >= 20 ? 20 : _additionalKillMoney % 20;
            var headshotAddMoney = _additionalHeadshotMoney >= 20 ? 20 : _additionalHeadshotMoney % 20;

            _startMoney += killAddMoney;
            _startMoney += headshotAddMoney;

            _additionalHeadshotMoney -= headshotAddMoney;
            _additionalKillMoney -= killAddMoney;
            
            //Coin.CoinSoundPool.Get();
            
            UpdateMoneyTexts();
            yield return new WaitForSeconds(0.05f);
        }
    }
}
