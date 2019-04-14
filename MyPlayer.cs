﻿using HamstarHelpers.Components.DataStructures;
using HamstarHelpers.Helpers.DebugHelpers;
using PlayerStatistics.Logic;
using PlayerStatistics.NetProtocols;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;


namespace PlayerStatistics {
	partial class PlayerStatisticsPlayer : ModPlayer {
		private PlayerStatsLogic Logic;



		////////////////

		public override void PreUpdate() {
			if( this.player.whoAmI != Main.myPlayer ) { return; }

			var mymod = (PlayerStatisticsMod)this.mod;

			this.Logic.Update();
		}


		////////////////

		public override void Kill( double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource ) {
			if( pvp ) {
				this.Logic.PvPDeaths++;
			}
			this.Logic.TotalDeaths++;

			if( Main.netMode == 1 && this.player.whoAmI == Main.myPlayer ) {
				SyncStatsProtocol.SendToAll( this.player );
			}
		}

		public override void OnHitPvp( Item item, Player target, int damage, bool crit ) {
			if( target.dead ) {
				this.Logic.PvPKills++;
			}

			if( Main.netMode == 1 && this.player.whoAmI == Main.myPlayer ) {
				SyncStatsProtocol.SendToAll( this.player );
			}
		}

		public override void OnHitPvpWithProj( Projectile proj, Player target, int damage, bool crit ) {
			if( target.dead ) {
				this.Logic.PvPKills++;
			}

			if( Main.netMode == 1 && this.player.whoAmI == Main.myPlayer ) {
				SyncStatsProtocol.SendToAll( this.player );
			}
		}


		////////////////

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
			return this.Logic.ProgressOveride;
		}
		public int GetLatency() {
			return this.Logic.Latency;
		}
		public string GetProgress() {
			if( this.player.whoAmI != Main.myPlayer && !string.IsNullOrEmpty(this.Logic.ProgressOveride) ) {
				return this.Logic.ProgressOveride;
			}
			return this.Logic.FormatVanillaProgress();
		}

		////

		internal void SyncStats( int pvpKills, int pvpDeaths, int totalDeaths, string progress ) {
			this.Logic.SetStats( pvpKills, pvpDeaths, totalDeaths, progress );
		}

		////

		internal void RegisterNpcKillIfBoss( NPC npc ) {
			if( npc.boss ) {
				this.Logic.BossNpcKills.AddOrSet( npc.netID, 1 );
			}
		}
	}
}
