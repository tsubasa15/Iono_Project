using System;
using Naninovel;
using UnityEngine;

namespace Iono.Game.Common
{
    /// <summary>
    /// Thin access layer for Naninovel local custom variables.
    /// Missing variables are treated as caller-provided defaults and are not created by getters.
    /// </summary>
    public sealed class NaninovelVariableService
    {
        private readonly ICustomVariableManager variables;

        public NaninovelVariableService()
            : this(Engine.GetServiceOrErr<ICustomVariableManager>())
        {
        }

        public NaninovelVariableService(ICustomVariableManager variables)
        {
            this.variables = variables ?? throw new ArgumentNullException(nameof(variables));
        }

        public bool Exists(string variableName)
        {
            EnsureLocalVariableName(variableName);
            return variables.VariableExists(variableName);
        }

        public float GetNumber(string variableName, float defaultValue = 0f)
        {
            EnsureLocalVariableName(variableName);
            return variables.TryGetVariableValue<float>(variableName, out var value) ? value : defaultValue;
        }

        public int GetInt(string variableName, int defaultValue = 0)
        {
            EnsureLocalVariableName(variableName);
            return variables.TryGetVariableValue<int>(variableName, out var value) ? value : defaultValue;
        }

        public void SetNumber(string variableName, float value)
        {
            EnsureLocalVariableName(variableName);
            variables.SetVariableValue(variableName, new CustomVariableValue(value));
        }

        public bool GetBool(string variableName, bool defaultValue = false)
        {
            EnsureLocalVariableName(variableName);
            return variables.TryGetVariableValue<bool>(variableName, out var value) ? value : defaultValue;
        }

        public void SetBool(string variableName, bool value)
        {
            EnsureLocalVariableName(variableName);
            variables.SetVariableValue(variableName, new CustomVariableValue(value));
        }

        public string GetString(string variableName, string defaultValue = "")
        {
            EnsureLocalVariableName(variableName);
            return variables.TryGetVariableValue<string>(variableName, out var value) ? value : defaultValue;
        }

        public void SetString(string variableName, string value)
        {
            EnsureLocalVariableName(variableName);
            variables.SetVariableValue(variableName, new CustomVariableValue(value ?? string.Empty));
        }

        public float GetNonNegativeNumber(string variableName, float defaultValue = 0f)
        {
            return ToNonNegative(GetNumber(variableName, defaultValue));
        }

        public float SetNonNegativeNumber(string variableName, float value)
        {
            var clampedValue = ToNonNegative(value);
            SetNumber(variableName, clampedValue);
            return clampedValue;
        }

        public float AddNumberClampedToZero(string variableName, float amount)
        {
            var nextValue = ToNonNegative(GetNumber(variableName) + amount);
            SetNumber(variableName, nextValue);
            return nextValue;
        }

        public bool TrySpendNumber(string variableName, float amount)
        {
            if (amount < 0f)
                throw new ArgumentOutOfRangeException(nameof(amount), amount, "Spend amount must be zero or greater.");

            var currentValue = GetNumber(variableName);
            if (currentValue < amount)
                return false;

            SetNumber(variableName, ToNonNegative(currentValue - amount));
            return true;
        }

        public static float ToNonNegative(float value)
        {
            return Mathf.Max(0f, value);
        }

        private static void EnsureLocalVariableName(string variableName)
        {
            if (string.IsNullOrWhiteSpace(variableName))
                throw new ArgumentException("Variable name must not be empty.", nameof(variableName));
            if (CustomVariablesConfiguration.HasGlobalPrefix(variableName))
                throw new ArgumentException("Use Naninovel local variables with this service.", nameof(variableName));
        }
    }
}
