using HamstarHelpers.Components.DataStructures;
using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Helpers.TmlHelpers;
using System;
using Terraria;
using Terraria.ModLoader;


namespace PlayerStatistics {
	partial class PlayerStatisticsPlayer : ModPlayer {
		public int GetPvPDeaths() {
			return this.Logic.PvPDeaths;
		}
		public int GetPvPKills() {
			return this.Logic.PvPKills;
		}
		public int GetTotalDeaths() {
			return this.Logic.TotalDeaths;
		}
		public string GetProgressOverride() {
			return this.Logic.ProgressOverride;
		}
		public int GetLatency() {
			return this.Logic.Latency;
		}
		public string GetProgress( out int progressAmount ) {
			if( this.player.whoAmI != Main.myPlayer && !string.IsNullOrEmpty(this.Logic.ProgressOverride) ) {
				progressAmount = this.Logic.ProgressOverrideAmount;
				return this.Logic.ProgressOverride;
			}
			return this.Logic.FormatVanillaProgress( out progressAmount );
		}

		////

		internal void SyncStats( int killerWho, int pvpKills, int pvpDeaths, int totalDeaths, string progress, int progressAmount ) {
			if( killerWho >= 0 && killerWho < Main.player.Length ) {
				var otherPlayer = TmlHelpers.SafelyGetModPlayer<PlayerStatisticsPlayer>( Main.player[killerWho] );
				if( otherPlayer != null ) {
					var logic = otherPlayer.Logic;

					otherPlayer.Logic.SetStats( logic.PvPKills + 1, logic.PvPDeaths, logic.TotalDeaths, logic.ProgressOverride, logic.ProgressOverrideAmount );
				}
			}

			this.Logic.SetStats( pvpKills, pvpDeaths, totalDeaths, progress, progressAmount );
		}

		////

		internal void RegisterNpcKillIfBoss( NPC npc ) {
			if( npc.boss ) {
				this.Logic.BossNpcKills.AddOrSet( npc.netID, 1 );
			}
		}
	}
}
