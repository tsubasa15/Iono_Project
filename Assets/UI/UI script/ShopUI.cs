using Naninovel.UI;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : CustomUI
{
    [SerializeField] private Button backButton;

    protected override void Awake()
    {
        base.Awake();

        ResolveReferences();
        ValidateReferences();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        RegisterListeners();
    }

    protected override void OnDisable()
    {
        UnregisterListeners();

        base.OnDisable();
    }

    private void RegisterListeners()
    {
        UnregisterListeners();

        if (backButton != null)
            backButton.onClick.AddListener(OnBackClicked);
    }

    private void UnregisterListeners()
    {
        if (backButton != null)
            backButton.onClick.RemoveListener(OnBackClicked);
    }

    private void OnBackClicked()
    {
        Hide();
    }

    private void ResolveReferences()
    {
        if (backButton != null)
            return;

        foreach (var button in GetComponentsInChildren<Button>(true))
        {
            var objectName = button.gameObject.name.ToLowerInvariant();
            if (objectName.Contains("back") || objectName.Contains("close") || objectName.Contains("return") ||
                objectName.Contains("戻") || objectName.Contains("閉"))
            {
                backButton = button;
                return;
            }
        }
    }

    private void ValidateReferences()
    {
        if (backButton == null)
            Debug.LogWarning("ShopUI: backButton が未設定です。");
    }
}
