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
	}
}
