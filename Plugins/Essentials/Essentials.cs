/*
 * MIT License
 * 
 * Copyright © 2021 ReeceDeveloper
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
 * documentation files (the “Software”), to deal in the Software without restriction, including without limitation
 * the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and
 * to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

 * THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO
 * THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
 * CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
 * DEALINGS IN THE SOFTWARE.
 */

using Newtonsoft.Json;

namespace Oxide.Plugins
{
    [Info("Essentials", "ReeceDeveloper", "1.0.0")]
    [Description("Provides essentials functions to your Rust server.")]

    public class Essentials : CovalencePlugin
    {
        private class PluginConfig
        {
            [JsonProperty("(1). Enable the Essentials plugin?")]
            public bool PluginEnabled { get; private set; } = true;
        }
        
        private PluginConfig _config;
        
        protected override void LoadDefaultConfig()
        {
            Config.WriteObject(new PluginConfig(), true);
        }

        private new void SaveConfig()
        {
            Config.WriteObject(_config, true);
        }

        private void Init()
        {
            _config = Config.ReadObject<PluginConfig>();

            if (!_config.PluginEnabled)
            {
                
            }
        }
    }
}