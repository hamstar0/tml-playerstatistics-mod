using HamstarHelpers.Components.Network;
using HamstarHelpers.Helpers.TmlHelpers;
using System;
using Terraria;


namespace PlayerStatistics.NetProtocols {
	class SyncStatsProtocol : PacketProtocolSentToEither {
		public static void SendToAll( Player killer ) {
			var protocol = new SyncStatsProtocol( killer );
			protocol.SendToServer( true );
		}



		////////////////
		
		public int PvPKills;
		public int PvPDeaths;
		public int TotalDeaths;
		public string Progress;
		public int ProgressAmount;



		////////////////

		private SyncStatsProtocol() { }

		private SyncStatsProtocol( Player player ) {
			var myplayer = TmlHelpers.SafelyGetModPlayer<PlayerStatisticsPlayer>( player );
			
			this.PvPDeaths = myplayer.GetPvPDeaths();
			this.PvPKills = myplayer.GetPvPKills();
			this.TotalDeaths = myplayer.GetTotalDeaths();
			this.Progress = myplayer.GetProgress( out this.ProgressAmount );
		}


		////////////////

		protected override void ReceiveOnClient() {
			var myplayer = TmlHelpers.SafelyGetModPlayer<PlayerStatisticsPlayer>( Main.LocalPlayer );

			myplayer.SyncStats( this.PvPKills, this.PvPDeaths, this.TotalDeaths, this.Progress, this.ProgressAmount );
		}

		protected override void ReceiveOnServer( int fromWho ) {
			var myplayer = TmlHelpers.SafelyGetModPlayer<PlayerStatisticsPlayer>( Main.player[fromWho] );

			myplayer.SyncStats( this.PvPKills, this.PvPDeaths, this.TotalDeaths, this.Progress, this.ProgressAmount );
		}
	}
}
