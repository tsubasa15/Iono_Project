using System;
using System.Collections.Generic;
using UnityEngine;

namespace Iono.Game.MasterData
{
    internal sealed class MasterDataCache<TData>
    {
        private readonly object cacheLock = new object();
        private readonly string masterName;
        private readonly string idName;
        private readonly Func<IReadOnlyList<TData>> loadAll;
        private readonly Func<TData, string> getId;
        private IReadOnlyList<TData> cachedItems;
        private Dictionary<string, TData> cachedById;

        public MasterDataCache(string masterName, string idName, Func<IReadOnlyList<TData>> loadAll, Func<TData, string> getId)
        {
            this.masterName = masterName;
            this.idName = idName;
            this.loadAll = loadAll;
            this.getId = getId;
        }

        public IReadOnlyList<TData> GetAll()
        {
            EnsureCache();
            return cachedItems;
        }

        public bool TryGetById(string id, out TData data)
        {
            data = default(TData);

            if (string.IsNullOrWhiteSpace(id))
            {
                Debug.LogWarning($"{masterName} master lookup failed. {idName} is empty.");
                return false;
            }

            EnsureCache();
            return cachedById.TryGetValue(id, out data);
        }

        public TData GetById(string id)
        {
            if (TryGetById(id, out var data))
                return data;

            throw new KeyNotFoundException($"{masterName} master not found. {idName}={MasterDataIdUtil.Display(id)}");
        }

        public bool Exists(string id)
        {
            return TryGetById(id, out _);
        }

        public void Clear()
        {
            lock (cacheLock)
            {
                cachedItems = null;
                cachedById = null;
            }
        }

        private void EnsureCache()
        {
            if (cachedItems != null && cachedById != null)
                return;

            lock (cacheLock)
            {
                if (cachedItems != null && cachedById != null)
                    return;

                var items = new List<TData>();
                var byId = new Dictionary<string, TData>(StringComparer.Ordinal);

                try
                {
                    var loadedItems = loadAll() ?? Array.Empty<TData>();
                    foreach (var item in loadedItems)
                    {
                        items.Add(item);
                        var id = MasterDataIdUtil.Safe(getId(item));

                        if (string.IsNullOrWhiteSpace(id))
                        {
                            Debug.LogWarning($"{masterName} master has an empty {idName}. This row was excluded from lookup cache.");
                            continue;
                        }

                        if (byId.ContainsKey(id))
                        {
                            Debug.LogWarning($"Duplicate {masterName} master {idName} detected. The first row is kept. {idName}={id}");
                            continue;
                        }

                        byId.Add(id, item);
                    }
                }
                catch (Exception exception)
                {
                    Debug.LogError($"Failed to load {masterName} master data from BGDatabase. {exception}");
                    items.Clear();
                    byId.Clear();
                }

                cachedItems = items.AsReadOnly();
                cachedById = byId;
            }
        }
    }
}
