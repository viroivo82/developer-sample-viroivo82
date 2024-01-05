using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DeveloperSample.Syncing
{
    public class SyncDebug
    {
        // this uses Task.WhenAll to run all the tasks in parallel
        public async Task<List<string>> InitializeList(IEnumerable<string> items)
        {
            var bag = new ConcurrentBag<string>();
            var tasks = items.Select(async i =>
            {
                var r = await Task.Run(() => i).ConfigureAwait(false);
                bag.Add(r);
            });

            await Task.WhenAll(tasks);
            var list = bag.ToList();
            return list;
        }

        // this uses Parallel.ForEachAsync to run all the tasks in parallel
        public async Task<List<string>> InitializeListParallel(IEnumerable<string> items)
        {
            var bag = new ConcurrentBag<string>();
            await Parallel.ForEachAsync(items, async (i, token) =>
            {
                var r = await Task.Run(() => i, token).ConfigureAwait(false);
                bag.Add(r);
            });
            var list = bag.ToList();
            return list;
        }

        public Dictionary<int, string> InitializeDictionary(Func<int, string> getItem)
        {
            var itemsToInitialize = Enumerable.Range(0, 100).ToList();

            var concurrentDictionary = new ConcurrentDictionary<int, string>();
            var threads = Enumerable.Range(0, 3)
                .Select(i => new Thread(() => {
                    // this is a bit of a hack to get the items to be distributed evenly
                    var itemsForThisThread = itemsToInitialize.Where((item, index) => index % 3 == i);
                    foreach (var item in itemsForThisThread)
                    {
                        concurrentDictionary.AddOrUpdate(item, getItem, (_, s) => s);
                    }
                }))
                .ToList();

            foreach (var thread in threads)
            {
                thread.Start();
            }
            foreach (var thread in threads)
            {
                thread.Join();
            }

            return concurrentDictionary.ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }
}