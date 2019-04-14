using HamstarHelpers.Helpers.NetHelpers;
using HamstarHelpers.Services.ControlPanel;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader.IO;


namespace PlayerStatistics.Logic {
	class PlayerStatsLogic {
		public IDictionary<int, int> BossNpcKills = new Dictionary<int, int>();
		//private VanillaEventFlag EventsConquered;

		public int PvPKills;
		public int PvPDeaths;
		public int TotalDeaths;
		public int Latency = 0;
		public string ProgressOveride;



		////////////////

		public PlayerStatsLogic() { }

		public void SetStats( int pvpKills, int pvpDeaths, int totalDeaths, string progress ) {
			this.PvPKills = pvpKills;
			this.PvPDeaths = pvpDeaths;
			this.TotalDeaths = totalDeaths;
			this.ProgressOveride = progress;
		}

		////////////////

		public void Load( TagCompound tag ) {
			if( tag.ContainsKey("pvp_kills") ) {
				this.PvPKills = tag.GetInt( "pvp_kills" );
				this.PvPDeaths = tag.GetInt( "pvp_deaths" );
				this.TotalDeaths = tag.GetInt( "total_deaths" );
			}
		}

		public TagCompound Save() {
			var mymod = PlayerStatisticsMod.Instance;

			if( mymod.Config.SaveStatsPerPlayer ) {
				return new TagCompound {
					{ "pvp_kills", this.PvPKills },
					{ "pvp_deaths", this.PvPDeaths },
					{ "total_deaths", this.TotalDeaths }
				};
			} else {
				return new TagCompound();
			}
		}


		////////////////

		public void Update() {
			var mymod = PlayerStatisticsMod.Instance;

			if( Main.netMode == 1 ) {
				this.Latency = NetHelpers.GetServerPing();
			}

			if( mymod.PlayerStatsUI.IsInitialized && ControlPanelTabs.GetCurrentTab() == PlayerStatisticsMod.ControlPanelName ) {
				mymod.PlayerStatsUI.Update();
			}
		}


		////////////////

		public string FormatVanillaProgress() {
			if( this.BossNpcKills.ContainsKey( NPCID.MoonLordCore ) ) {
				return "Post-Moon Lord";
			}
			/*if( (this.EventsConquered & VanillaEventFlag.Martians) != 0 ) {
				return "Martian Invasion Defeated";
			}*/
			if( this.BossNpcKills.ContainsKey( NPCID.CultistBoss ) ) {
				return "Post-Cultist";
			}
			if( this.BossNpcKills.ContainsKey( NPCID.Golem ) ) {
				return "Post-Golem";
			}
			if( this.BossNpcKills.ContainsKey( NPCID.Plantera ) ) {
				return "Post-Plantera";
			}
			if( this.BossNpcKills.ContainsKey( NPCID.TheDestroyer )
				|| this.BossNpcKills.ContainsKey( NPCID.SkeletronPrime )
				|| ( this.BossNpcKills.ContainsKey( NPCID.Retinazer ) && this.BossNpcKills.ContainsKey( NPCID.Spazmatism ) ) ) {
				return "Post-Mech Bosses";
			}
			if( this.BossNpcKills.ContainsKey( NPCID.WallofFlesh ) ) {
				return "Hard Mode";
			}
			if( this.BossNpcKills.ContainsKey( NPCID.EaterofWorldsHead )
				|| this.BossNpcKills.ContainsKey( NPCID.BrainofCthulhu ) ) {
				return "Post-Evil Biome";
			}
			if( this.BossNpcKills.ContainsKey( NPCID.SkeletronHead ) ) {
				return "Dungeon Open";
			}
			/*if( (this.EventsConquered & VanillaEventFlag.Goblins) != 0 ) {
				return "Goblins Defeated";
			}*/
			if( this.BossNpcKills.ContainsKey( NPCID.KingSlime )
				|| this.BossNpcKills.ContainsKey( NPCID.EyeofCthulhu )
				|| this.BossNpcKills.ContainsKey( NPCID.QueenBee ) ) {
				return "Bosses Killed";
			}
			return "None";
		}
	}
}
