using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject dualPerkActivationPanel;
    [SerializeField] private Button dualActivationButton;
    [SerializeField] private TextMeshProUGUI dualUsesCount;

    private DualPerkInGameHandler _dualPerkHandler;

    private void Start()
    {
        _dualPerkHandler = FindObjectOfType<DualPerkInGameHandler>();

        if (_dualPerkHandler == null || _dualPerkHandler.DualUsesCount == 0)
        {
            dualPerkActivationPanel.SetActive(false);
            return;
        }
        dualUsesCount.text = _dualPerkHandler.DualUsesCount.ToString();
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

    private void EnableDualPerkPanel()
    {
        dualActivationButton.interactable = true;
    }
}
