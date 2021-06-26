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
using Oxide.Core.Libraries.Covalence;

namespace Oxide.Plugins
{
    [Info("Essentials", "ReeceDeveloper", "1.0.1")]
    [Description("Provides essentials functions to your Rust server.")]

    public class Essentials : CovalencePlugin
    {
        #region Configuration
        
        private class PluginConfig
        {
            [JsonProperty("(1). Enable the Essentials plugin?")] 
            public bool PluginEnabled { get; private set; } = true;

            [JsonProperty("(2). Enable the server's whitelist?")] 
            public bool WhitelistEnabled { get; private set; }
            
            [JsonProperty("(3). Announce when the server is saved?")]
            public bool AnnounceWorldSave { get; private set; }
            
            [JsonProperty("(4). Disable F1-give messages?")]
            public bool DisableGiveMessages { get; private set; }

            [JsonProperty("(5). Announce when a player joins the server?")]
            public bool AnnounceJoinEvents { get; private set; } = true;

            [JsonProperty("(6). Announce when a player leaves the server?")]
            public bool AnnounceLeaveEvents { get; private set; } = true;
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
                ["WhitelistDenyEvent"] = "You are not whitelisted on this server.",
                ["WorldSaveEvent"] = "The server is now saving.",
                ["PlayerJoinEvent"] = "{0} has joined the server!",
                ["PlayerLeaveEvent"] = "{0} has left the server!"
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
            
            if(_config.WhitelistEnabled)
                InitWhitelist();
            
            Puts("- Successfully been initialised.");
        }

        private void Unload()
        {
            Puts("- Successfully been unloaded.");
        }
        
        #region Whitelisting

        private const string WhitelistPerm = "essentials.whitelist.allow";

        private void InitWhitelist()
        {
            permission.RegisterPermission(WhitelistPerm, this);
        }
        
        bool IsWhitelisted(string id)
        {
            var player = players.FindPlayerById(id);

            return player != null && permission.UserHasPermission(id, WhitelistPerm);
        }

        object CanUserLogin(string name, string id)
        {
            if (!_config.WhitelistEnabled)
                return null;

            if (IsWhitelisted(id))
                return null;

            return lang.GetMessage("WhitelistDenyEvent", this);
        }

        #endregion Whitelisting
        
        #region SaveAnnounce

        void OnServerSave()
        {
            if (!_config.AnnounceWorldSave)
                return;

            server.Broadcast(lang.GetMessage("WorldSaveEvent", this));
            
        }
        
        #endregion SaveAnnounce

        #region GiveMessages

        object OnServerMessage(string message, string name)
        {
            if (!_config.DisableGiveMessages)
                return null;

            if (!name.Equals("SERVER") && message.Contains("gave"))
                return null;

            return true;
        }
        
        #endregion GiveMessages

        #region PlayerEvents

        void OnUserConnected(IPlayer player)
        {
            if (!_config.AnnounceJoinEvents)
                return;

            var message = lang.GetMessage("PlayerJoinEvent", this);
            server.Broadcast(string.Format(message, player.Name));
        }

        void OnUserDisconnected(IPlayer player)
        {
            if (!_config.AnnounceLeaveEvents)
                return;

            var message = lang.GetMessage("PlayerLeaveEvent", this);
            server.Broadcast(string.Format(message, player.Name));
        }

        #endregion PlayerEvents
    }
}