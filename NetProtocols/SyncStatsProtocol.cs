using HamstarHelpers.Components.Network;
using HamstarHelpers.Helpers.TmlHelpers;
using System;
using Terraria;

namespace PlayerStatistics.NetProtocols {
	class SyncStatsProtocol : PacketProtocolSentToEither {
		public static void SendToAll( Player player ) {
			var protocol = new SyncStatsProtocol( player );
			protocol.SendToServer( true );
		}



		////////////////
		
		public int PvPKills;
		public int PvPDeaths;
		public int TotalDeaths;
		public int Latency;
		public string Progress;



		////////////////

		public SyncStatsProtocol( Player player ) {
			var myplayer = TmlHelpers.SafelyGetModPlayer<PlayerStatisticsPlayer>( player );

			this.PvPDeaths = myplayer.PvPDeaths;
			this.PvPKills = myplayer.PvPKills;
			this.TotalDeaths = myplayer.TotalDeaths;
			this.Latency = myplayer.Latency;
			this.Progress = myplayer.FormatVanillaProgress();
		}


		////////////////

		protected override void ReceiveOnClient() {
			var myplayer = TmlHelpers.SafelyGetModPlayer<PlayerStatisticsPlayer>( Main.LocalPlayer );

			myplayer.SyncStats( this.PvPKills, this.PvPDeaths, this.TotalDeaths, this.Latency, this.Progress );
		}

		protected override void ReceiveOnServer( int fromWho ) {
			var myplayer = TmlHelpers.SafelyGetModPlayer<PlayerStatisticsPlayer>( Main.player[fromWho] );

			myplayer.SyncStats( this.PvPKills, this.PvPDeaths, this.TotalDeaths, this.Latency, this.Progress );
		}
	}
}
