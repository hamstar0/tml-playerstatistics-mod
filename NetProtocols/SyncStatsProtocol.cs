using HamstarHelpers.Components.Network;
using HamstarHelpers.Helpers.TmlHelpers;
using System;
using Terraria;


namespace PlayerStatistics.NetProtocols {
	class SyncStatsProtocol : PacketProtocolSentToEither {
		public static void SendToAll( Player killer, int killerWho ) {
			var protocol = new SyncStatsProtocol( killer, killerWho );
			protocol.SendToServer( true );
		}



		////////////////

		public int KillerWho;
		public int PvPKills;
		public int PvPDeaths;
		public int TotalDeaths;
		public string Progress;
		public int ProgressAmount;



		////////////////

		private SyncStatsProtocol() { }

		private SyncStatsProtocol( Player player, int killerWho ) {
			var myplayer = TmlHelpers.SafelyGetModPlayer<PlayerStatisticsPlayer>( player );

			this.KillerWho = killerWho;
			this.PvPDeaths = myplayer.GetPvPDeaths();
			this.PvPKills = myplayer.GetPvPKills();
			this.TotalDeaths = myplayer.GetTotalDeaths();
			this.Progress = myplayer.GetProgress( out this.ProgressAmount );
		}


		////////////////

		protected override void ReceiveOnClient() {
			var myplayer = TmlHelpers.SafelyGetModPlayer<PlayerStatisticsPlayer>( Main.LocalPlayer );

			myplayer.SyncStats( this.KillerWho, this.PvPKills, this.PvPDeaths, this.TotalDeaths, this.Progress, this.ProgressAmount );
		}

		protected override void ReceiveOnServer( int fromWho ) {
			var myplayer = TmlHelpers.SafelyGetModPlayer<PlayerStatisticsPlayer>( Main.player[fromWho] );

			myplayer.SyncStats( this.KillerWho, this.PvPKills, this.PvPDeaths, this.TotalDeaths, this.Progress, this.ProgressAmount );
		}
	}
}
