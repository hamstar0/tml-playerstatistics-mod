using System;
using Terraria;
using Terraria.GameContent.UI.Elements;


namespace PlayerStatistics {
	class UIPlayerStats : UIPanel {
		private int PlayerWho;

		public UIText NameElement;
		public UIText TeamElement;
		public UIText PvPKillsElement;
		public UIText PvPDeathsElement;
		public UIText TotalDeathsElement;
		public UIText LatencyElement;
		public UIText ProgressElement;



		////////////////

		public UIPlayerStats( Player player ) {
			this.PlayerWho = player.whoAmI;

			this.NameElement = new UIText( player.name );
			this.NameElement.Top.Set( 0f, 0f );
			this.NameElement.Left.Set( 0f, 0f );
			this.Append( this.NameElement );
		}


		////////////////

		public void UpdatePlayerInfo() {

		}
	}
}
