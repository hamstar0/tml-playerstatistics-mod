using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Services.UI.ControlPanel;
using PlayerStatistics.Logic;
using PlayerStatistics.NetProtocols;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;


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

		public override void PreUpdate() {
			var mymod = (PlayerStatisticsMod)this.mod;

			switch( Main.netMode ) {
			case NetmodeID.SinglePlayer:
				this.Logic.UpdateSingle();
				break;
			case NetmodeID.MultiplayerClient:
				this.Logic.UpdateClient();
				break;
			case NetmodeID.Server:
				this.Logic.UpdateServer();
				break;
			}
		}


		////////////////

		public override void Kill( double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource ) {
			if( this.player.whoAmI != Main.myPlayer ) { return; }

			if( pvp ) {
				this.Logic.PvpDeaths++;
			}
			this.Logic.TotalDeaths++;

			if( Main.netMode == 1 && this.player.whoAmI == Main.myPlayer ) {
				PlayerStatisticsPlayer.AddKillForPlayer( damageSource.SourcePlayerIndex );
				SyncStatsProtocol.SendToAll( this.player, damageSource.SourcePlayerIndex );
			}
		}


		////////////////

		public override void ProcessTriggers( TriggersSet triggersSet ) {
			var mymod = (PlayerStatisticsMod)this.mod;

			try {
				if( mymod.ControlPanelHotkey != null && mymod.ControlPanelHotkey.JustPressed ) {
					if( ControlPanelTabs.IsDialogOpen() ) {
						ControlPanelTabs.CloseDialog();
					} else {
						ControlPanelTabs.OpenTab( PlayerStatisticsMod.ControlPanelName );
					}
				}
			} catch { }
		}
	}
}
