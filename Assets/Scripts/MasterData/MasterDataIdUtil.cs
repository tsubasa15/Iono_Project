using System;
using System.Collections.Generic;
using UnityEngine;

namespace Iono.Game.MasterData
{
    internal static class MasterDataIdUtil
    {
        public static string Safe(string value)
        {
            return value ?? string.Empty;
        }

        public static string Display(string value)
        {
            return string.IsNullOrEmpty(value) ? "<empty>" : value;
        }

        public static void AddIfNotEmpty(ICollection<string> ids, string id)
        {
            if (!string.IsNullOrWhiteSpace(id))
                ids.Add(id);
        }

        public static string ToTypeId(Iono.MasterData.type entity, string owner, string fieldName)
        {
            if (entity != null)
                return Safe(entity.typeId);

            WarnNullRelation(owner, fieldName);
            return string.Empty;
        }

        public static string ToSkillId(Iono.MasterData.skill entity, string owner, string fieldName)
        {
            if (entity != null)
                return Safe(entity.skillId);

            WarnNullRelation(owner, fieldName);
            return string.Empty;
        }

        public static string ToPatternId(Iono.MasterData.enemypattern entity, string owner, string fieldName)
        {
            if (entity != null)
                return Safe(entity.patternId);

            WarnNullRelation(owner, fieldName);
            return string.Empty;
        }

        public static string ToEnemySlotId(Iono.MasterData.enemyslotdetail entity, string owner, string fieldName)
        {
            if (entity != null)
                return Safe(entity.enemyslotId);

            WarnNullRelation(owner, fieldName);
            return string.Empty;
        }

        public static string ToJankenId(Iono.MasterData.janken entity, string owner, string fieldName)
        {
            if (entity != null)
                return Safe(entity.jankenId);

            WarnNullRelation(owner, fieldName);
            return string.Empty;
        }

        public static string ToBuffId(Iono.MasterData.buffdebuff entity, string owner, string fieldName)
        {
            if (entity != null)
                return Safe(entity.buffId);

            WarnNullRelation(owner, fieldName);
            return string.Empty;
        }

        public static string ToConditionId(Iono.MasterData.abnormalcondition entity, string owner, string fieldName)
        {
            if (entity != null)
                return Safe(entity.conditionId);

            WarnNullRelation(owner, fieldName);
            return string.Empty;
        }

        public static string ToSpecialEffectId(Iono.MasterData.specialeffect entity, string owner, string fieldName)
        {
            if (entity != null)
                return Safe(entity.spEffectId);

            WarnNullRelation(owner, fieldName);
            return string.Empty;
        }

        public static IReadOnlyList<string> ToTypeIdList(IEnumerable<Iono.MasterData.type> entities, string owner, string fieldName)
        {
            if (entities == null)
            {
                WarnNullRelation(owner, fieldName);
                return Array.Empty<string>();
            }

            var ids = new List<string>();
            foreach (var entity in entities)
            {
                if (entity == null)
                {
                    WarnNullRelation(owner, fieldName);
                    continue;
                }

                AddIfNotEmpty(ids, entity.typeId);
            }

            return ids.AsReadOnly();
        }

        private static void WarnNullRelation(string owner, string fieldName)
        {
            Debug.LogWarning($"{owner} relation is null. field={fieldName}");
        }
    }
}
