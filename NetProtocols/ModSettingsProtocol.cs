using HamstarHelpers.Components.Network;
using System;


namespace PlayerStatistics.NetProtocols {
	class ModSettingsProtocol : PacketProtocolRequestToServer {
		public PlayerStatisticsConfigData Settings;



		////////////////

		private ModSettingsProtocol() { }

		protected override void InitializeServerSendData( int toWho ) {
			this.Settings = PlayerStatisticsMod.Instance.Config;
		}

		////////////////

		protected override void ReceiveReply() {
			var mymod = PlayerStatisticsMod.Instance;
			//var myplayer = TmlHelpers.SafelyGetModPlayer<PlayerStatisticsPlayer>( Main.LocalPlayer );

			mymod.ConfigJson.SetData( this.Settings );

			//myplayer.FinishModSettingsSync();
		}
	}
}
