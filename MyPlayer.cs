using HamstarHelpers.Components.Network;
using HamstarHelpers.Helpers.DebugHelpers;
using PlayerStatistics.Logic;
using PlayerStatistics.NetProtocols;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;


namespace PlayerStatistics {
	partial class PlayerStatisticsPlayer : ModPlayer {
		private PlayerStatsLogic Logic;



		////////////////

		public override void Initialize() {
			this.Logic = new PlayerStatsLogic();
		}

		public override void clientClone( ModPlayer clientClone ) {
			var myclone = (PlayerStatisticsPlayer)clientClone;
			myclone.Logic = this.Logic;
		}


		////////////////

		public override void Load( TagCompound tags ) {
			this.Logic.Load( tags );
		}

		public override TagCompound Save() {
			return this.Logic.Save();
		}


		////////////////

		public override void SyncPlayer( int toWho, int fromWho, bool newPlayer ) {
			var mymod = (PlayerStatisticsMod)this.mod;

			if( Main.netMode == 2 ) {
				if( toWho == -1 && fromWho == this.player.whoAmI ) {
					this.OnConnectServer( Main.player[fromWho] );
				}
			}
		}

		public override void OnEnterWorld( Player player ) {
			if( player.whoAmI != Main.myPlayer ) { return; }
			if( this.player.whoAmI != Main.myPlayer ) { return; }

			if( Main.netMode == 0 ) {
				this.OnConnectSingle();
			}
			if( Main.netMode == 1 ) {
				this.OnConnectCurrentClient();
			}
		}


		////////////////

		private void OnConnectSingle() {
			var mymod = (PlayerStatisticsMod)this.mod;

			mymod.ConfigJson.LoadFileAsync( ( success ) => {
				if( !success ) {
					//mymod.ConfigJson.SaveFile();
					LogHelpers.Alert( "Player Statistics config could not be loaded." );
				}
			} );

			mymod.PlayerStatsUI.ClearPlayers();
		}

		private void OnConnectCurrentClient() {
			var mymod = (PlayerStatisticsMod)this.mod;

			mymod.PlayerStatsUI.ClearPlayers();

			PacketProtocolRequestToServer.QuickRequestToServer<ModSettingsProtocol>( -1 );
		}

		private void OnConnectServer( Player givenPlayer ) { }


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
