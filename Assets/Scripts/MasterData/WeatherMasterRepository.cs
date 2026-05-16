using System.Collections.Generic;

namespace Iono.Game.MasterData
{
    public sealed class WeatherMasterRepository
    {
        private readonly MasterDataCache<WeatherMasterData> cache;

        public WeatherMasterRepository()
        {
            cache = new MasterDataCache<WeatherMasterData>("Weather", "weatherId", LoadAll, data => data.WeatherId);
        }

        public IReadOnlyList<WeatherMasterData> GetAll() => cache.GetAll();
        public bool TryGetById(string id, out WeatherMasterData data) => cache.TryGetById(id, out data);
        public WeatherMasterData GetById(string id) => cache.GetById(id);
        public bool Exists(string id) => cache.Exists(id);
        public void ClearCache() => cache.Clear();
        public bool TryGetByWeatherId(string weatherId, out WeatherMasterData data) => TryGetById(weatherId, out data);
        public WeatherMasterData GetByWeatherId(string weatherId) => GetById(weatherId);

        private static IReadOnlyList<WeatherMasterData> LoadAll()
        {
            var items = new List<WeatherMasterData>();
            Iono.MasterData.weather.ForEachEntity(entity => items.Add(Convert(entity)));
            return items;
        }

        private static WeatherMasterData Convert(Iono.MasterData.weather entity)
        {
            var owner = $"Weather weatherId={MasterDataIdUtil.Display(entity.weatherId)}";
            return new WeatherMasterData(entity.weatherId, entity.name, entity.memo, entity.plusDamageRate, entity.minusDamageRate, entity.buffDefRate, entity.buffSpDefRate, entity.preventOverwriteFlag, entity.duration, MasterDataIdUtil.ToTypeIdList(entity.slipDamageImmuneTypeId, owner, nameof(entity.slipDamageImmuneTypeId)), MasterDataIdUtil.ToConditionId(entity.negatedConditionId, owner, nameof(entity.negatedConditionId)), MasterDataIdUtil.ToTypeId(entity.plusDamageTypeId, owner, nameof(entity.plusDamageTypeId)), MasterDataIdUtil.ToTypeId(entity.minusDamageTypeId, owner, nameof(entity.minusDamageTypeId)), MasterDataIdUtil.ToTypeId(entity.buffDefTypeId, owner, nameof(entity.buffDefTypeId)), MasterDataIdUtil.ToTypeId(entity.buffSpDefTypeId, owner, nameof(entity.buffSpDefTypeId)), entity.slipDamageRate);
        }
    }
}
