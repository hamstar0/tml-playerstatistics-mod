using HamstarHelpers.Helpers.PlayerHelpers;
using HamstarHelpers.Helpers.TmlHelpers;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.UI.Elements;


namespace PlayerStatistics {
	class UIPlayerStatsEntry : UIPanel {
		private int PlayerWho;

		public UIText NameElement;
		public UIText TeamElement;
		public UIText PvPKillsElement;
		public UIText PvPDeathsElement;
		public UIText TotalDeathsElement;
		public UIText LatencyElement;
		public UIText ProgressElement;



		////////////////

		public UIPlayerStatsEntry( Player player ) {
			var myplayer = TmlHelpers.SafelyGetModPlayer<PlayerStatisticsPlayer>( player );
			float left = 0;

			this.PlayerWho = player.whoAmI;

			this.NameElement = new UIText( player.name );
			this.NameElement.Top.Set( 0f, 0f );
			this.NameElement.Left.Set( left, 0f );
			this.Append( this.NameElement );

			left += 128f;

			Color teamColor;
			string teamColorName = Enum.GetName( typeof(PlayerTeamName), PlayerHelpers.GetTeamName(player.team, out teamColor) );

			this.TeamElement = new UIText( teamColorName );
			this.TeamElement.TextColor = teamColor;
			this.TeamElement.Top.Set( 0f, 0f );
			this.TeamElement.Left.Set( left, 0f );
			this.Append( this.TeamElement );

			left += 64;

			this.PvPKillsElement = new UIText( myplayer.PvPKills+"" );
			this.PvPKillsElement.Top.Set( 0f, 0f );
			this.PvPKillsElement.Left.Set( left, 0f );
			this.Append( this.PvPKillsElement );

			left += 48;

			this.PvPDeathsElement = new UIText( myplayer.PvPDeaths + "" );
			this.PvPDeathsElement.Top.Set( 0f, 0f );
			this.PvPDeathsElement.Left.Set( left, 0f );
			this.Append( this.PvPDeathsElement );

			left += 48;

			this.TotalDeathsElement = new UIText( myplayer.TotalDeaths + "" );
			this.TotalDeathsElement.Top.Set( 0f, 0f );
			this.TotalDeathsElement.Left.Set( left, 0f );
			this.Append( this.TotalDeathsElement );

			left += 48;

			this.LatencyElement = new UIText( myplayer.Latency + "" );
			this.LatencyElement.Top.Set( 0f, 0f );
			this.LatencyElement.Left.Set( left, 0f );
			this.Append( this.LatencyElement );

			left += 48;

			this.ProgressElement = new UIText( myplayer.FormatVanillaProgress() );
			this.ProgressElement.Top.Set( 0f, 0f );
			this.ProgressElement.Left.Set( left, 0f );
			this.Append( this.ProgressElement );
		}


		////////////////

		public void UpdatePlayerInfo() {
			Player plr = Main.player[ this.PlayerWho ];
			var myplayer = TmlHelpers.SafelyGetModPlayer<PlayerStatisticsPlayer>( plr );

			Color teamColor;
			string teamColorName = Enum.GetName( typeof(PlayerTeamName), PlayerHelpers.GetTeamName(plr.team, out teamColor) );

			this.TeamElement.SetText( teamColorName );
			this.TeamElement.TextColor = teamColor;

			this.PvPKillsElement.SetText( myplayer.PvPKills + "" );
			this.PvPDeathsElement.SetText( myplayer.PvPDeaths + "" );
			this.TotalDeathsElement.SetText( myplayer.TotalDeaths + "" );
			this.LatencyElement.SetText( myplayer.Latency + "" );
			this.ProgressElement.SetText( myplayer.FormatVanillaProgress() );
		}
	}
}
