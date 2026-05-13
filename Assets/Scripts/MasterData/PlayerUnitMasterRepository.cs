using System;
using System.Collections.Generic;
using UnityEngine;

namespace Iono.Game.MasterData
{
    public sealed class PlayerUnitMasterRepository
    {
        private readonly object cacheLock = new object();
        private IReadOnlyList<PlayerUnitMasterData> cachedUnits;
        private Dictionary<string, PlayerUnitMasterData> cachedUnitsById;

        public IReadOnlyList<PlayerUnitMasterData> GetAll()
        {
            EnsureCache();
            return cachedUnits;
        }

        public bool TryGetByUnitId(string unitId, out PlayerUnitMasterData data)
        {
            data = null;

            if (string.IsNullOrWhiteSpace(unitId))
            {
                Debug.LogWarning("PlayerUnit master lookup failed. unitId is empty.");
                return false;
            }

            EnsureCache();
            return cachedUnitsById.TryGetValue(unitId, out data);
        }

        public PlayerUnitMasterData GetByUnitId(string unitId)
        {
            if (TryGetByUnitId(unitId, out var data))
                return data;

            throw new KeyNotFoundException($"PlayerUnit master not found. unitId={unitId ?? "<null>"}");
        }

        public bool Exists(string unitId)
        {
            return TryGetByUnitId(unitId, out _);
        }

        public void ClearCache()
        {
            lock (cacheLock)
            {
                cachedUnits = null;
                cachedUnitsById = null;
            }
        }

        public void LogAllUnitsForDebug()
        {
            foreach (var unit in GetAll())
                Debug.Log($"PlayerUnit master: unitId={unit.UnitId}, name={unit.Name}");
        }

        private void EnsureCache()
        {
            if (cachedUnits != null && cachedUnitsById != null)
                return;

            lock (cacheLock)
            {
                if (cachedUnits != null && cachedUnitsById != null)
                    return;

                var units = new List<PlayerUnitMasterData>();
                var unitsById = new Dictionary<string, PlayerUnitMasterData>(StringComparer.Ordinal);

                try
                {
                    Iono.MasterData.playerunit.ForEachEntity(entity =>
                    {
                        var data = Convert(entity);
                        if (data == null)
                            return;

                        units.Add(data);

                        if (string.IsNullOrWhiteSpace(data.UnitId))
                        {
                            Debug.LogWarning("PlayerUnit master has an empty unitId. This row was excluded from unitId lookup cache.");
                            return;
                        }

                        if (unitsById.ContainsKey(data.UnitId))
                        {
                            Debug.LogWarning($"Duplicate PlayerUnit master unitId detected. The first row is kept. unitId={data.UnitId}");
                            return;
                        }

                        unitsById.Add(data.UnitId, data);
                    });
                }
                catch (Exception exception)
                {
                    Debug.LogError($"Failed to load PlayerUnit master data from BGDatabase. {exception}");
                    units.Clear();
                    unitsById.Clear();
                }

                cachedUnits = units.AsReadOnly();
                cachedUnitsById = unitsById;
            }
        }

        private PlayerUnitMasterData Convert(Iono.MasterData.playerunit entity)
        {
            if (entity == null)
            {
                Debug.LogWarning("PlayerUnit master conversion skipped because entity is null.");
                return null;
            }

            var skillIds = CollectSkillIds(entity);

            if (entity.skillCount != skillIds.Count)
            {
                Debug.LogWarning(
                    $"PlayerUnit master skillCount mismatch. unitId={SafeString(entity.unitId)}, skillCount={entity.skillCount}, actualSkillIds={skillIds.Count}");
            }

            return new PlayerUnitMasterData(
                entity.unitId,
                entity.name,
                entity.nameTextId,
                entity.descTextId,
                entity.memo,
                entity.imageId,
                entity.rarity,
                entity.gender,
                entity.typeId,
                entity.passiveSkillId,
                entity.skillCount,
                skillIds.AsReadOnly(),
                entity.maxLevel,
                entity.hp,
                entity.atk,
                entity.def,
                entity.spAtk,
                entity.spDef,
                entity.spd,
                entity.resPoison,
                entity.resSleep,
                entity.resParalysis,
                entity.resConfusion,
                entity.tag1,
                entity.tag2);
        }

        private static List<string> CollectSkillIds(Iono.MasterData.playerunit entity)
        {
            var skillIds = new List<string>(4);
            AddSkillId(skillIds, entity.skillId1);
            AddSkillId(skillIds, entity.skillId2);
            AddSkillId(skillIds, entity.skillId3);
            AddSkillId(skillIds, entity.skillId4);
            return skillIds;
        }

        private static void AddSkillId(ICollection<string> skillIds, string skillId)
        {
            if (string.IsNullOrWhiteSpace(skillId))
                return;

            skillIds.Add(skillId);
        }

        private static string SafeString(string value)
        {
            return string.IsNullOrEmpty(value) ? "<empty>" : value;
        }
    }
}
