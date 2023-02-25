using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JavascriptComponent.Sample
{
    public class ChartInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public ChartInterop(IJSRuntime jsRuntime)
        {
            string projectName = Assembly.GetCallingAssembly().GetName().Name;

            moduleTask = new (() => jsRuntime.InvokeAsync<IJSObjectReference>(
              "import", $"./_content/{projectName}/chartScript.js").AsTask());
        }

        public async ValueTask ShowChart()
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("showChart");
        }
        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
