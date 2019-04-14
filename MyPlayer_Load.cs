using HamstarHelpers.Components.Network;
using HamstarHelpers.Helpers.DebugHelpers;
using PlayerStatistics.NetProtocols;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;


namespace PlayerStatistics {
	partial class PlayerStatisticsPlayer : ModPlayer {
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
	}
}
