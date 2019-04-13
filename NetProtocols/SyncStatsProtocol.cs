using HamstarHelpers.Components.Network;
using System;
using Terraria;


namespace PlayerStatistics.NetProtocols {
	class SyncStatsProtocol : PacketProtocolRequestToServer {
		protected override void InitializeServerSendData( int toWho ) {
			Player plr = Main.player[ toWho ];d
		}

		protected override void ReceiveReply() {d
		}
	}
}
