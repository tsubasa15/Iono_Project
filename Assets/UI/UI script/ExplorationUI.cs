using System;
using System.Collections.Generic;
using Naninovel;
using Naninovel.UI;
using UnityEngine;
using UnityEngine.UI;

public class ExplorationUI : CustomUI
{
    private const string StaminaVariable = "n_stamina";

    [SerializeField] private Button[] workButtons;
    [SerializeField] private Button[] reconButtons;
    [SerializeField] private Button optionButton;
    [SerializeField] private string workScriptName = "Work/WorkEntry";
    [SerializeField] private string reconScriptName = "Recon/ReconEntry";
    [SerializeField] private string optionUIName = "OptionUI";

    private ICustomVariableManager variables;
    private IUIManager uis;
    private IScriptPlayer player;

    protected override void Awake()
    {
        base.Awake();

        ResolveReferences();
        ResolveNaninovelServices();
        ValidateReferences();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        RegisterListeners();
        RefreshButtonInteractable();
    }

    protected override void OnDisable()
    {
        UnregisterListeners();

        base.OnDisable();
    }

    protected override void HandleVisibilityChanged(bool visible)
    {
        base.HandleVisibilityChanged(visible);

        if (visible)
            RefreshButtonInteractable();
    }

    public void RefreshButtonInteractable()
    {
        var hasStamina = GetIntVariable(StaminaVariable, 0) > 0;
        SetButtonsInteractable(workButtons, hasStamina);
        SetButtonsInteractable(reconButtons, hasStamina);
    }

    private void RegisterListeners()
    {
        UnregisterListeners();

        AddListeners(workButtons, PlayWorkEntry);
        AddListeners(reconButtons, PlayReconEntry);
        AddListener(optionButton, ShowOptionUI);

        if (variables != null)
            variables.OnVariableUpdated += HandleVariableUpdated;
    }

    private void UnregisterListeners()
    {
        RemoveListeners(workButtons, PlayWorkEntry);
        RemoveListeners(reconButtons, PlayReconEntry);
        RemoveListener(optionButton, ShowOptionUI);

        if (variables != null)
            variables.OnVariableUpdated -= HandleVariableUpdated;
    }

    private void PlayWorkEntry()
    {
        if (GetIntVariable(StaminaVariable, 0) <= 0)
        {
            RefreshButtonInteractable();
            return;
        }

        Hide();
        PlayScript(workScriptName);
    }

    private void PlayReconEntry()
    {
        if (GetIntVariable(StaminaVariable, 0) <= 0)
        {
            RefreshButtonInteractable();
            return;
        }

        Hide();
        PlayScript(reconScriptName);
    }

    private void ShowOptionUI()
    {
        ShowUI(optionUIName);
    }

    private void PlayScript(string scriptName)
    {
        if (string.IsNullOrWhiteSpace(scriptName))
        {
            Debug.LogWarning("ExplorationUI: scriptName が未設定です。");
            return;
        }

        if (player == null)
        {
            Debug.LogWarning($"ExplorationUI: IScriptPlayer が取得できないため {scriptName} を再生できません。");
            return;
        }

        player.MainTrack.LoadAndPlay(scriptName).Forget();
    }

    private void ShowUI(string uiName)
    {
        if (string.IsNullOrWhiteSpace(uiName))
        {
            Debug.LogWarning("ExplorationUI: uiName が未設定です。");
            return;
        }

        if (uis == null)
        {
            Debug.LogWarning($"ExplorationUI: IUIManager が取得できないため {uiName} を表示できません。");
            return;
        }

        var ui = uis.GetUI(uiName);
        if (ui == null)
        {
            Debug.LogWarning($"ExplorationUI: {uiName} が見つからないため表示できません。");
            return;
        }

        ui.Show();
    }

    private int GetIntVariable(string variableName, int defaultValue)
    {
        if (variables != null && variables.TryGetVariableValue<int>(variableName, out var value))
            return value;

        return defaultValue;
    }

    private void HandleVariableUpdated(CustomVariableUpdatedArgs args)
    {
        if (string.Equals(args.Name, StaminaVariable, StringComparison.OrdinalIgnoreCase))
            RefreshButtonInteractable();
    }

    private void ResolveReferences()
    {
        workButtons = HasAssignedButtons(workButtons) ? workButtons : FindButtonsByNamePrefix("work");
        reconButtons = HasAssignedButtons(reconButtons) ? reconButtons : FindButtonsByNamePrefix("recon");
        optionButton = optionButton ? optionButton : FindOptionButton();
    }

    private void ResolveNaninovelServices()
    {
        variables = Engine.GetService<ICustomVariableManager>();
        uis = Engine.GetService<IUIManager>();
        player = Engine.GetService<IScriptPlayer>();

        if (variables == null)
            Debug.LogWarning("ExplorationUI: ICustomVariableManager が取得できません。");
        if (uis == null)
            Debug.LogWarning("ExplorationUI: IUIManager が取得できません。");
        if (player == null)
            Debug.LogWarning("ExplorationUI: IScriptPlayer が取得できません。");
    }

    private void ValidateReferences()
    {
        if (!HasAssignedButtons(workButtons))
            Debug.LogWarning("ExplorationUI: workButtons が未設定です。");
        if (!HasAssignedButtons(reconButtons))
            Debug.LogWarning("ExplorationUI: reconButtons が未設定です。");
        if (!optionButton)
            Debug.LogWarning("ExplorationUI: optionButton が未設定です。");
    }

    private Button[] FindButtonsByNamePrefix(string namePrefix)
    {
        var result = new List<Button>();
        foreach (var button in GetComponentsInChildren<Button>(true))
        {
            if (button && NormalizeName(button.gameObject.name).StartsWith(namePrefix, StringComparison.OrdinalIgnoreCase))
                result.Add(button);
        }

        result.Sort((left, right) => string.CompareOrdinal(left.gameObject.name, right.gameObject.name));
        return result.ToArray();
    }

    private Button FindOptionButton()
    {
        foreach (var button in GetComponentsInChildren<Button>(true))
        {
            var normalizedName = NormalizeName(button.gameObject.name);
            if (normalizedName.Contains("option") || normalizedName.Contains("オプション"))
                return button;
        }

        return null;
    }

    private static void SetButtonsInteractable(Button[] buttons, bool interactable)
    {
        if (buttons == null)
            return;

        foreach (var button in buttons)
            if (button)
                button.interactable = interactable;
    }

    private static void AddListeners(Button[] buttons, UnityEngine.Events.UnityAction listener)
    {
        if (buttons == null)
            return;

        foreach (var button in buttons)
            AddListener(button, listener);
    }

    private static void RemoveListeners(Button[] buttons, UnityEngine.Events.UnityAction listener)
    {
        if (buttons == null)
            return;

        foreach (var button in buttons)
            RemoveListener(button, listener);
    }

    private static void AddListener(Button button, UnityEngine.Events.UnityAction listener)
    {
        if (button)
            button.onClick.AddListener(listener);
    }

    private static void RemoveListener(Button button, UnityEngine.Events.UnityAction listener)
    {
        if (button)
            button.onClick.RemoveListener(listener);
    }

    private static bool HasAssignedButtons(Button[] buttons)
    {
        if (buttons == null || buttons.Length == 0)
            return false;

        foreach (var button in buttons)
            if (button)
                return true;

        return false;
    }

    private static string NormalizeName(string value)
    {
        return string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim().ToLowerInvariant();
    }
}
