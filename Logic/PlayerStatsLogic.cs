using HamstarHelpers.Helpers.Net;
using HamstarHelpers.Services.UI.ControlPanel;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader.IO;


namespace PlayerStatistics.Logic {
	class PlayerStatsLogic {
		public IDictionary<int, int> BossNpcKills = new Dictionary<int, int>();
		//private VanillaEventFlag EventsConquered;

		public int PvpKills;
		public int PvpDeaths;
		public int TotalDeaths;
		public int Latency = 0;
		public string ProgressOverride;
		public int ProgressOverrideAmount;



		////////////////

		public PlayerStatsLogic() { }

		public void SetStats( int pvpKills, int pvpDeaths, int totalDeaths, string progress, int progressAmount ) {
			this.PvpKills = pvpKills;
			this.PvpDeaths = pvpDeaths;
			this.TotalDeaths = totalDeaths;
			this.ProgressOverride = progress;
			this.ProgressOverrideAmount = progressAmount;
		}

		public void SetPvpKills( int pvpKills ) {
			this.PvpKills = pvpKills;
		}

		////////////////

		public void Load( TagCompound tag ) {
			if( tag.ContainsKey("pvp_kills") ) {
				this.PvpKills = tag.GetInt( "pvp_kills" );
				this.PvpDeaths = tag.GetInt( "pvp_deaths" );
				this.TotalDeaths = tag.GetInt( "total_deaths" );
			}

			if( tag.ContainsKey("kill_count") ) {
				int amount = tag.GetInt( "kill_count" );

				for( int i=0; i<amount; i++ ) {
					int npcId = tag.GetInt( "kill_id_" + i );
					int npcKills = tag.GetInt( "kill_amt_" + i );

					this.BossNpcKills[ npcId ] = npcKills;
				}
			}
		}

		public TagCompound Save() {
			var mymod = PlayerStatisticsMod.Instance;
			var tag = new TagCompound();

			if( mymod.Config.SaveStatsPerPlayer ) {
				tag["pvp_kills"] = this.PvpKills;
				tag["pvp_deaths"] = this.PvpDeaths;
				tag["total_deaths"] = this.TotalDeaths;
			}

			tag["kill_count"] = this.BossNpcKills.Count;

			int i = 0;
			foreach( var kv in this.BossNpcKills ) {
				tag["kill_id_" + i] = kv.Key;
				tag["kill_amt_" + i] = kv.Value;
				i++;
			}

			return tag;
		}


		////////////////

		public void UpdateLocal() {
			var mymod = PlayerStatisticsMod.Instance;
			string currTab = ControlPanelTabs.GetCurrentTab();

			this.Latency = NetPlayHelpers.GetServerPing();

			if( mymod.PlayerStatsUI.IsInitialized && currTab == PlayerStatisticsMod.ControlPanelName ) {
				mymod.PlayerStatsUI.Update();
			}
		}

		public void UpdateServer() {
		}


		////////////////

		public string FormatVanillaProgress( out int progressAmount ) {
			if( this.BossNpcKills.ContainsKey( NPCID.MoonLordCore ) ) {
				progressAmount = 110;
				return "Post-Moon Lord";
			}
			/*if( (this.EventsConquered & VanillaEventFlag.Martians) != 0 ) {
				progressAmount = 100;
				return "Martian Invasion Defeated";
			}*/
			if( this.BossNpcKills.ContainsKey( NPCID.CultistBoss ) ) {
				progressAmount = 90;
				return "Post-Cultist";
			}
			if( this.BossNpcKills.ContainsKey( NPCID.Golem ) ) {
				progressAmount = 80;
				return "Post-Golem";
			}
			if( this.BossNpcKills.ContainsKey( NPCID.Plantera ) ) {
				progressAmount = 70;
				return "Post-Plantera";
			}
			if( this.BossNpcKills.ContainsKey( NPCID.TheDestroyer )
				|| this.BossNpcKills.ContainsKey( NPCID.SkeletronPrime )
				|| ( this.BossNpcKills.ContainsKey( NPCID.Retinazer ) && this.BossNpcKills.ContainsKey( NPCID.Spazmatism ) ) ) {
				progressAmount = 60;
				return "Post-Mech Bosses";
			}
			if( this.BossNpcKills.ContainsKey( NPCID.WallofFlesh ) ) {
				progressAmount = 50;
				return "Hard Mode";
			}
			if( this.BossNpcKills.ContainsKey( NPCID.EaterofWorldsHead )
				|| this.BossNpcKills.ContainsKey( NPCID.BrainofCthulhu ) ) {
				progressAmount = 40;
				return "Post-Evil Biome";
			}
			if( this.BossNpcKills.ContainsKey( NPCID.SkeletronHead ) ) {
				progressAmount = 30;
				return "Dungeon Open";
			}
			/*if( (this.EventsConquered & VanillaEventFlag.Goblins) != 0 ) {
				progressAmount = 20;
				return "Goblins Defeated";
			}*/
			if( this.BossNpcKills.ContainsKey( NPCID.KingSlime )
				|| this.BossNpcKills.ContainsKey( NPCID.EyeofCthulhu )
				|| this.BossNpcKills.ContainsKey( NPCID.QueenBee ) ) {
				progressAmount = 10;
				return "Bosses Killed";
			}

			progressAmount = 0;
			return "None";
		}
	}
}
