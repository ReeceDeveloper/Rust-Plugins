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

using System.Collections.Generic;
using Newtonsoft.Json;

namespace Oxide.Plugins
{
    [Info("HackableCrates", "ReeceDeveloper", "1.0.0")]
    [Description("Provides configurable values for Rust's time-based hackable crates.")]
    
    public class HackableCrates : CovalencePlugin
    {
        #region Configuration
        
        private class PluginConfig
        {
            [JsonProperty("(1). Enable the HackableCrates plugin?")]
            public bool PluginEnabled { get; private set; } = true;
            
            [JsonProperty("(2). Enable a non-default crate timer?")]
            public bool EnableCustomTimer { get; private set; }

            [JsonProperty("(3). If enabled, set the custom timer's time (minutes).")]
            public double CustomTimerValue { get; private set; } = 15.0;
            
            [JsonProperty("(4). Revert unlock time to default on plugin unload?")]
            public bool RevertToDefaults { get; private set; } = true;
            
            [JsonProperty("(5). Announce when a crate starts to be unlocked?")]
            public bool AnnounceNewUnlock { get; private set; }

            [JsonProperty("(6). Announce when a crate has finished unlocking?")]
            public bool AnnounceEndUnlock { get; private set; }
        }
        
        private PluginConfig _config;
        
        protected override void LoadDefaultConfig()
        {
            Config.WriteObject(new PluginConfig(), true);
        }
        
        #endregion Configuration
        
        #region Localisation

        protected override void LoadDefaultMessages()
        {
            lang.RegisterMessages(new Dictionary<string, string>
            {
                ["NewUnlockEvent"] = "A hackable crate has begun to be unlocked!",
                ["EndUnlockEvent"] = "A hackable crate has just unlocked!"
            }, this);
        }
        
        #endregion Localisation

        private void Init()
        {
            _config = Config.ReadObject<PluginConfig>();

            if (!_config.PluginEnabled)
            {
                Puts("- Currently disabled via the configuration file.");
                return;
            }
            
            InitHackableCrates();
            
            Puts("- Successfully been initialised.");
        }

        private void OnServerInitialized(bool initial)
        {
            if(initial)
                Init();
        }

        private void Unload()
        {
            if(_config.RevertToDefaults)
                server.Command("hackablelockedcrate.requiredhackseconds", 900);
            
            Puts("- Successfully been unloaded.");
        }

        #region HackableCrates

        private void InitHackableCrates()
        {
            if (_config.EnableCustomTimer)
                server.Command("hackablelockedcrate.requiredhackseconds", _config.CustomTimerValue * 60);
        }

        private void OnCrateHack()
        {
            if (!_config.AnnounceNewUnlock)
                return;
            
            server.Broadcast(lang.GetMessage("NewUnlockEvent", this));
        }

        private void OnCrateHackEnd()
        {
            if (!_config.AnnounceEndUnlock)
                return;
            
            server.Broadcast(lang.GetMessage("EndUnlockEvent", this));
        }

        #endregion HackableCrates
    }
}