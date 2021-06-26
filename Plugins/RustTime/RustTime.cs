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
    [Info("RustTime", "ReeceDeveloper", "1.0.0")]
    [Description("Provides the ability to manage Rust's timescale.")]
    
    public class RustTime : CovalencePlugin
    {
        #region Configuration

        private class PluginConfig
        {
            [JsonProperty("(1). Enable the RustTime plugin?")] 
            public bool PluginEnabled { get; private set; } = true;
            
            [JsonProperty("(2). Enable a static (permanent) time?")]
            public bool StaticTimeEnabled { get; private set; }

            [JsonProperty("(3). If enabled, set the static time.")]
            public int StaticTime { get; private set; } = 12;
            
            [JsonProperty("(4). Speed up Rust's night phase?")]
            public bool NightSpeedEnabled { get; private set; }

            [JsonProperty("(5). If enabled, set how long night should last (minutes).")]
            public int NightSpeedLength { get; private set; } = 15;

            [JsonProperty("(6). Announce when the time becomes faster?")]
            public bool AnnounceTimeFast { get; private set; } = true;

            [JsonProperty("(7). Announce when the time returns to normal?")]
            public bool AnnounceTimeNormal { get; private set; } = true;
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
                ["TimeSpeedNormalEvent"] = "Time will now resume its normal speed.",
                ["TimeSpeedFastEvent"] = "Time will now speed up during night."
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
            
            InitStaticTime();
            SetTime();
            
            Puts("- Successfully been initialised.");
        }

        private void Unload()
        {
            if(_config.StaticTimeEnabled)
                UnityEngine.Object.FindObjectOfType<TOD_Time>().ProgressTime = true;

            if (_config.NightSpeedEnabled && TOD_Sky.Instance.Components.Time != null)
                TOD_Sky.Instance.Components.Time.OnSunset -= OnSunset;
            
            Puts("- Successfully been unloaded.");
        }

        #region StaticTime

        private void InitStaticTime()
        {
            if (!_config.StaticTimeEnabled)
                return;

            var time = UnityEngine.Object.FindObjectOfType<TOD_Time>();

            time.ProgressTime = false;
            
            server.Command($"env.time {_config.StaticTime}");
        }

        #endregion StaticTime

        #region NightSpeed

        private readonly TOD_Time _time = TOD_Sky.Instance.Components.Time;
        private readonly TOD_Sky _sky = TOD_Sky.Instance;
        
        private void SetTime()
        {
            if (!_config.NightSpeedEnabled)
                return;

            if (_sky == null)
            {
                Puts("- Could not obtain an instance of TOD_Sky; NightSpeed disabled.");
                return;
            }

            _time.ProgressTime = true;
            _time.UseTimeCurve = false;
            _time.OnSunset += OnSunset;

            if (_sky.Cycle.Hour > _sky.SunriseTime && _sky.Cycle.Hour < _sky.SunsetTime)
                OnSunrise();
            else
                OnSunset();
        }

        private void OnSunrise()
        {
            if(_config.AnnounceTimeNormal)
                server.Broadcast(lang.GetMessage("TimeSpeedNormalEvent", this));
        }
        
        private void OnSunset()
        {
            _time.DayLengthInMinutes = 
                _config.NightSpeedLength * (24.0f / (24.0f - (TOD_Sky.Instance.SunsetTime - TOD_Sky.Instance.SunriseTime)));

            if(_config.AnnounceTimeFast)
                server.Broadcast(lang.GetMessage("TimeSpeedFastEvent", this));
        }

        #endregion NightSpeed
    }
}