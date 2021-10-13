using System.Net.Http;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

class CacheObservable
{
    readonly ReplaySubject<int?> _value = new();
    bool _valueLoading = false;
    public int _counter = 0;
    
    public async Task<int?> getInvoiceAsync(string id)
    {
        if (!_valueLoading)
        {
            _counter++;
            _valueLoading = true;

            var client = new HttpClient();
            var response = await client.GetAsync("https://gorest.co.in/public/v1/users");

            _value.OnNext(response.IsSuccessStatusCode ? 5 : null);
        }

        return await _value.FirstOrDefaultAsync();
    }
}