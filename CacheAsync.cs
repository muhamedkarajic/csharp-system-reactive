using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

// Error Message: Operations that change non-concurrent collections must have exclusive access. A concurrent update was performed on this collection and corrupted its state. The collection's state is no longer correct.
class CacheAsync
{
    Dictionary<string, int?> _cache = new();
    public int _counter = 0;

    public async Task<int?> getInvoiceAsync(string id)
    {
        if (!_cache.TryGetValue(id, out _))
        {
            _counter++;
            var client = new HttpClient();
            var response = await client.GetAsync("https://gorest.co.in/public/v1/users");

            _cache.TryAdd(id, response.IsSuccessStatusCode ? 5 : null);
        }

        return _cache[id];
    }
}