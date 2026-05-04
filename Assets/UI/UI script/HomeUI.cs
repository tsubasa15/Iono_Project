using System;
using System.Collections.Generic;
using System.Text;
using Naninovel;
using Naninovel.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityText = UnityEngine.UI.Text;

public class HomeUI : CustomUI
{
    private const string MoneyVariable = "n_money";
    private const string StaminaVariable = "n_stamina";
    private const string WeekVariable = "n_week";
    private const string DayOfWeekVariable = "n_dayOfWeek";

    private const string OptionUIName = "OptionUI";
    private const string InventoryUIName = "InventoryUI";
    private const string ExplorationUIName = "ExplorationUI";

    private const string TrainScriptName = "Train/TrainEntry";
    private const string ShopScriptName = "Shop/ShopEntry";
    private const string NextDayScriptName = "NextDay";
    private const string ReconScriptName = "Recon/ReconEntry";

    [Header("Header Texts")]
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text staminaText;
    [SerializeField] private TMP_Text weekText;
    [SerializeField] private TMP_Text dayOfWeekText;
    [SerializeField] private UnityText moneyLegacyText;
    [SerializeField] private UnityText staminaLegacyText;
    [SerializeField] private UnityText weekLegacyText;
    [SerializeField] private UnityText dayOfWeekLegacyText;

    [Header("Buttons")]
    [SerializeField] private Button optionButton;
    [SerializeField] private Button inventoryButton;
    [SerializeField] private Button trainButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button nextDayButton;
    [SerializeField] private Button explorationButton;

    private readonly HashSet<string> warnedMissingVariables = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
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
        RefreshHeader();
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

        if (!visible)
            return;

        RefreshHeader();
        RefreshButtonInteractable();
    }

    public void RefreshHeader()
    {
        var money = GetIntVariable(MoneyVariable);
        var stamina = GetIntVariable(StaminaVariable);
        var week = GetIntVariable(WeekVariable);
        var dayOfWeek = GetIntVariable(DayOfWeekVariable);

        SetText(moneyText, moneyLegacyText, $"所持金: {money}");
        SetText(staminaText, staminaLegacyText, $"スタミナ: {stamina}");
        SetText(weekText, weekLegacyText, $"Week: {week}");
        SetText(dayOfWeekText, dayOfWeekLegacyText, $"Day: {dayOfWeek}");
    }

    public void RefreshButtonInteractable()
    {
        var hasStamina = GetIntVariable(StaminaVariable) > 0;

        if (trainButton)
            trainButton.interactable = hasStamina;
        if (explorationButton)
            explorationButton.interactable = hasStamina;
    }

    private void RegisterListeners()
    {
        UnregisterListeners();

        AddListener(optionButton, OnOptionClicked);
        AddListener(inventoryButton, OnInventoryClicked);
        AddListener(trainButton, OnTrainClicked);
        AddListener(shopButton, OnShopClicked);
        AddListener(nextDayButton, OnNextDayClicked);
        AddListener(explorationButton, OnExplorationClicked);

        if (variables != null)
            variables.OnVariableUpdated += HandleVariableUpdated;
    }

    private void UnregisterListeners()
    {
        RemoveListener(optionButton, OnOptionClicked);
        RemoveListener(inventoryButton, OnInventoryClicked);
        RemoveListener(trainButton, OnTrainClicked);
        RemoveListener(shopButton, OnShopClicked);
        RemoveListener(nextDayButton, OnNextDayClicked);
        RemoveListener(explorationButton, OnExplorationClicked);

        if (variables != null)
            variables.OnVariableUpdated -= HandleVariableUpdated;
    }

    private void OnOptionClicked()
    {
        ShowUI(OptionUIName);
    }

    private void OnInventoryClicked()
    {
        ShowUI(InventoryUIName);
    }

    private void OnTrainClicked()
    {
        if (GetIntVariable(StaminaVariable) <= 0)
        {
            RefreshButtonInteractable();
            return;
        }

        Hide();
        PlayScript(TrainScriptName);
    }

    private void OnShopClicked()
    {
        Hide();
        PlayScript(ShopScriptName);
    }

    private void OnNextDayClicked()
    {
        Hide();
        PlayScript(NextDayScriptName);
    }

    private void OnExplorationClicked()
    {
        if (GetIntVariable(StaminaVariable) <= 0)
        {
            RefreshButtonInteractable();
            return;
        }

        if (uis != null && uis.GetUI(ExplorationUIName) != null)
        {
            ShowUI(ExplorationUIName);
            return;
        }

        Debug.Log("ExplorationUI が無いため ReconEntry.nani を再生");
        Hide();
        PlayScript(ReconScriptName);
    }

    private void PlayScript(string scriptName)
    {
        if (player == null)
        {
            Debug.LogWarning($"HomeUI: IScriptPlayer が取得できないため {scriptName} を再生できません。");
            return;
        }

        player.MainTrack.LoadAndPlay(scriptName).Forget();
    }

    private void ShowUI(string uiName)
    {
        if (uis == null)
        {
            Debug.LogWarning($"HomeUI: IUIManager が取得できないため {uiName} を表示できません。");
            return;
        }

        var ui = uis.GetUI(uiName);
        if (ui == null)
        {
            Debug.LogWarning($"HomeUI: {uiName} が見つからないため表示できません。");
            return;
        }

        ui.Show();
    }

    private void HandleVariableUpdated(CustomVariableUpdatedArgs args)
    {
        if (!IsHeaderVariable(args.Name))
            return;

        RefreshHeader();
        RefreshButtonInteractable();
    }

    private int GetIntVariable(string variableName)
    {
        if (variables != null && variables.TryGetVariableValue<int>(variableName, out var value))
            return value;

        if (warnedMissingVariables.Add(variableName))
            Debug.LogWarning($"HomeUI: Naninovel Local 変数 {variableName} が見つからないため 0 として表示します。");

        return 0;
    }

    private void ResolveReferences()
    {
        moneyText ??= FindComponentByName<TMP_Text>("money text");
        staminaText ??= FindComponentByName<TMP_Text>("stmina text", "stamina text");
        weekText ??= FindComponentByName<TMP_Text>("n_week text");
        dayOfWeekText ??= FindComponentByName<TMP_Text>("n_dayOfWeek text");

        moneyLegacyText ??= FindComponentByName<UnityText>("money text");
        staminaLegacyText ??= FindComponentByName<UnityText>("stmina text", "stamina text");
        weekLegacyText ??= FindComponentByName<UnityText>("n_week text");
        dayOfWeekLegacyText ??= FindComponentByName<UnityText>("n_dayOfWeek text");

        optionButton ??= FindComponentByName<Button>("Optionbutton");
        inventoryButton ??= FindComponentByName<Button>("Inventorybutton");
        trainButton ??= FindComponentByName<Button>("Trainbutton");
        shopButton ??= FindComponentByName<Button>("Shopbutton");
        nextDayButton ??= FindComponentByName<Button>("Nextdaybutton");
        explorationButton ??= FindComponentByName<Button>("Explorationbutton");
    }

    private void ResolveNaninovelServices()
    {
        variables = Engine.GetService<ICustomVariableManager>();
        uis = Engine.GetService<IUIManager>();
        player = Engine.GetService<IScriptPlayer>();

        if (variables == null)
            Debug.LogWarning("HomeUI: ICustomVariableManager が取得できません。");
        if (uis == null)
            Debug.LogWarning("HomeUI: IUIManager が取得できません。");
        if (player == null)
            Debug.LogWarning("HomeUI: IScriptPlayer が取得できません。");
    }

    private void ValidateReferences()
    {
        WarnIfMissingText(moneyText, moneyLegacyText, nameof(moneyText));
        WarnIfMissingText(staminaText, staminaLegacyText, nameof(staminaText));
        WarnIfMissingText(weekText, weekLegacyText, nameof(weekText));
        WarnIfMissingText(dayOfWeekText, dayOfWeekLegacyText, nameof(dayOfWeekText));

        WarnIfMissing(optionButton, nameof(optionButton));
        WarnIfMissing(inventoryButton, nameof(inventoryButton));
        WarnIfMissing(trainButton, nameof(trainButton));
        WarnIfMissing(shopButton, nameof(shopButton));
        WarnIfMissing(nextDayButton, nameof(nextDayButton));
        WarnIfMissing(explorationButton, nameof(explorationButton));
    }

    private T FindComponentByName<T>(params string[] objectNames) where T : Component
    {
        var components = GetComponentsInChildren<T>(true);
        foreach (var objectName in objectNames)
        {
            var normalizedTarget = NormalizeName(objectName);
            foreach (var component in components)
                if (NormalizeName(component.gameObject.name) == normalizedTarget)
                    return component;
        }

        return null;
    }

    private static string NormalizeName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return string.Empty;

        var builder = new StringBuilder(value.Normalize(NormalizationForm.FormKC).Length);
        foreach (var character in value.Normalize(NormalizationForm.FormKC))
        {
            if (!char.IsWhiteSpace(character) && character != '_' && character != '-')
                builder.Append(char.ToLowerInvariant(character));
        }

        return builder.ToString();
    }

    private static bool IsHeaderVariable(string variableName)
    {
        return string.Equals(variableName, MoneyVariable, StringComparison.OrdinalIgnoreCase) ||
               string.Equals(variableName, StaminaVariable, StringComparison.OrdinalIgnoreCase) ||
               string.Equals(variableName, WeekVariable, StringComparison.OrdinalIgnoreCase) ||
               string.Equals(variableName, DayOfWeekVariable, StringComparison.OrdinalIgnoreCase);
    }

    private static void SetText(TMP_Text tmpText, UnityText legacyText, string value)
    {
        if (tmpText)
            tmpText.text = value;
        else if (legacyText)
            legacyText.text = value;
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

    private static void WarnIfMissing(UnityEngine.Object reference, string fieldName)
    {
        if (!reference)
            Debug.LogWarning($"HomeUI: {fieldName} が未設定です。");
    }

    private static void WarnIfMissingText(TMP_Text tmpText, UnityText legacyText, string fieldName)
    {
        if (!tmpText && !legacyText)
            Debug.LogWarning($"HomeUI: {fieldName} が未設定です。");
    }
}
