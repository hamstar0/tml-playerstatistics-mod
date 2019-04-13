using HamstarHelpers.Components.Network;
using PlayerStatistics.NetProtocols;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;


namespace PlayerStatistics {
	partial class PlayerStatisticsPlayer : ModPlayer {
		private IDictionary<int, int> BossNpcKills = new Dictionary<int, int>();
		//private VanillaEventFlag EventsConquered;


		////////////////

		public int PvPKills { get; private set; }
		public int PvPDeaths { get; private set; }
		public int TotalDeaths { get; private set; }
		public int Latency { get; private set; }
		public string ProgressOveride { get; private set; }



		////////////////

		public override void PreUpdate() {
			if( this.player.whoAmI != Main.myPlayer ) { return; }

			var mymod = (PlayerStatisticsMod)this.mod;
			mymod.PlayerStatsUI.Update();
		}


		////////////////

		public override void Kill( double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource ) {
			if( pvp ) {
				this.PvPDeaths++;
			}
			this.TotalDeaths++;

			PacketProtocolRequestToServer.QuickSyncToServerAndClients<SyncStatsProtocol>();
		}


		////////////////

		internal void SyncStats( int pvpKills, int pvpDeaths, int totalDeaths, int latency, string progress ) {
			this.PvPKills = pvpKills;
			this.PvPDeaths = pvpDeaths;
			this.TotalDeaths = totalDeaths;
			this.Latency = latency;
			this.ProgressOveride = progress;
		}
	}
}
