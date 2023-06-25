using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginComponent
{
    public class ComponentStyleInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public ComponentStyleInterop(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>("import", "/Plugins/PluginComponent/wwwroot/loadCSS.js").AsTask());
        }

        public async ValueTask<string> LoadStyle()
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<string>("addComponentCSS");
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
