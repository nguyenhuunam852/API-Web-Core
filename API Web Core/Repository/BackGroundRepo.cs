using API_Web_Core.Interface;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading.Tasks;

namespace API_Web_Core.Repository
{
    public class BackGroundRepo: IBackGroundRepo
    {
        private readonly IDistributedCache _distributedCache;
        private readonly string _cacheKey;

        public BackGroundRepo(IDistributedCache distributedCache)
        {
            this._distributedCache = distributedCache;
            this._cacheKey = "Process";
            //Excute test
            this.autoIncreaseValue();
        }
        public int getCurrentValue()
        {
            return int.Parse(_distributedCache.GetString(this._cacheKey));
        }

       
        private async Task autoIncreaseValue()
        {
            while (true)
            {
                var currentValue = 1;
                int? cachedValue = null;
                if (!string.IsNullOrEmpty(_distributedCache.GetString(this._cacheKey)))
                {
                    cachedValue = int.Parse(_distributedCache.GetString(this._cacheKey));
                }
                if (cachedValue != null)
                {
                    cachedValue += 1;
                    _distributedCache.SetString(this._cacheKey, cachedValue.ToString());
                }
                else
                {
                    _distributedCache.SetString(this._cacheKey, currentValue.ToString());
                }
                await Task.Delay(1000);
            }
        }



    }
}
