using HamstarHelpers.Components.DataStructures;
using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Helpers.TmlHelpers;
using System;
using Terraria;
using Terraria.ModLoader;


namespace PlayerStatistics {
	partial class PlayerStatisticsPlayer : ModPlayer {
		public static void AddKillForPlayer( int killerWho ) {
			if( killerWho >= 0 && killerWho < Main.player.Length ) {
				var otherPlayer = TmlHelpers.SafelyGetModPlayer<PlayerStatisticsPlayer>( Main.player[killerWho] );

				if( otherPlayer != null ) {
					otherPlayer.Logic.SetPvpKills( otherPlayer.Logic.PvpKills + 1 );
				} else {
					LogHelpers.Warn( "Invald ModPlayer for " + Main.player[killerWho].name );
				}
			} else {
				LogHelpers.Warn( "Invald player whoAmI: "+killerWho );
			}
		}



		////////////////

		public int GetPvpKills() {
			return this.Logic.PvpKills;
		}
		public int GetPvpDeaths() {
			return this.Logic.PvpDeaths;
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


		////////////////

		internal void SetStats( int killerWho, int pvpKills, int pvpDeaths, int totalDeaths, string progress, int progressAmount ) {
			this.Logic.SetStats( pvpKills, pvpDeaths, totalDeaths, progress, progressAmount );
			PlayerStatisticsPlayer.AddKillForPlayer( killerWho );
		}

		////

		internal void RegisterNpcKillIfBoss( NPC npc ) {
			if( npc.boss ) {
				this.Logic.BossNpcKills.AddOrSet( npc.netID, 1 );
			}
		}
	}
}
