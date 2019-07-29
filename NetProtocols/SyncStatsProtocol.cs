using HamstarHelpers.Components.Errors;
using HamstarHelpers.Components.Protocols.Packet.Interfaces;
using HamstarHelpers.Helpers.TModLoader;
using System;
using Terraria;


namespace PlayerStatistics.NetProtocols {
	class SyncStatsProtocol : PacketProtocolSentToEither {
		public static void SendToAll( Player player, int killerWho ) {
			var protocol = new SyncStatsProtocol( player, killerWho );
			protocol.SendToServer( true );
		}



		////////////////

		public int ForWho;
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

			this.ForWho = player.whoAmI;
			this.KillerWho = killerWho;

			this.PvPDeaths = myplayer.GetPvpDeaths();
			this.PvPKills = myplayer.GetPvpKills();
			this.TotalDeaths = myplayer.GetTotalDeaths();
			this.Progress = myplayer.GetProgress( out this.ProgressAmount );
		}


		////////////////

		protected override void ReceiveOnClient() {
			if( this.ForWho < 0 || this.ForWho >= Main.player.Length ) {
				throw new ModHelpersException( "Invalid ForWho: " + this.ForWho );
			}
			Player forPlayer = Main.player[ this.ForWho ];
			if( forPlayer == null ) {
				throw new ModHelpersException( "Invalid ForWho player: " + this.ForWho );
			}

			var forMyPlayer = TmlHelpers.SafelyGetModPlayer<PlayerStatisticsPlayer>( Main.player[this.ForWho] );
			if( forMyPlayer == null ) {
				throw new ModHelpersException( "Invalid mod player " + forPlayer.name );
			}

			forMyPlayer.SetStats( this.KillerWho, this.PvPKills, this.PvPDeaths, this.TotalDeaths, this.Progress, this.ProgressAmount );
		}

		protected override void ReceiveOnServer( int fromWho ) {
			var myplayer = TmlHelpers.SafelyGetModPlayer<PlayerStatisticsPlayer>( Main.player[fromWho] );
			
			myplayer.SetStats( this.KillerWho, this.PvPKills, this.PvPDeaths, this.TotalDeaths, this.Progress, this.ProgressAmount );
		}
	}
}
