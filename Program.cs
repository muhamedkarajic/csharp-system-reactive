using System;
using System.Collections.Generic;
using System.Threading.Tasks;

await fetchFromObservableCache();
await fetchFromAsyncCache();

async Task fetchFromObservableCache()
{
    var cacheObservable = new CacheObservable();

    var listOfRequestsForObservableCache = new List<Task<int?>>();
    for (int i = 0; i < 10; i++)
        listOfRequestsForObservableCache.Add(cacheObservable.getInvoiceAsync("1"));
    var observableItems = await Task.WhenAll(listOfRequestsForObservableCache);
    Console.Write($"fetchFromObservableCache({cacheObservable._counter}): "); // Will call endpoint only once and cache the response
    foreach (var observableItem in observableItems)
        Console.Write($"{observableItem}, ");
    Console.WriteLine("\n");
}

// Will result in errors depending on luck, see error message inside CacheAsync class
async Task fetchFromAsyncCache()
{
    var cacheAsync = new CacheAsync();

    var listOfRequestsForCache = new List<Task<int?>>();
    for (int i = 0; i < 10; i++)
        listOfRequestsForCache.Add(cacheAsync.getInvoiceAsync("1"));

    var cacheItems = await Task.WhenAll(listOfRequestsForCache);
    Console.Write($"fetchFromAsyncCache({cacheAsync._counter}): ");
    foreach (var cacheItem in cacheItems)
        Console.Write($"{cacheItem}, ");
}