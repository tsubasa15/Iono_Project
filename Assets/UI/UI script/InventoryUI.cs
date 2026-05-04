using Naninovel.UI;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : CustomUI
{
    [SerializeField] private Button closeButton;

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

        if (closeButton != null)
            closeButton.onClick.AddListener(OnCloseClicked);
    }

    private void UnregisterListeners()
    {
        if (closeButton != null)
            closeButton.onClick.RemoveListener(OnCloseClicked);
    }

    private void OnCloseClicked()
    {
        Hide();
    }

    private void ResolveReferences()
    {
        if (closeButton != null)
            return;

        foreach (var button in GetComponentsInChildren<Button>(true))
        {
            var objectName = button.gameObject.name.ToLowerInvariant();
            if (objectName.Contains("close") || objectName.Contains("back") || objectName.Contains("return") ||
                objectName.Contains("閉") || objectName.Contains("戻"))
            {
                closeButton = button;
                return;
            }
        }
    }

    private void ValidateReferences()
    {
        if (closeButton == null)
            Debug.LogWarning("InventoryUI: closeButton が未設定です。");
    }
}
