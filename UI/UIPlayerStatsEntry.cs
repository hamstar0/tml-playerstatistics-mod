using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Helpers.PlayerHelpers;
using HamstarHelpers.Helpers.TmlHelpers;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.UI.Elements;


namespace PlayerStatistics.UI {
	class UIPlayerStatsEntry : UIPanel {
		public static float[] ColumnOffsets => new float[] {
			0,		//Name
			116,	//Team
			64,		//PvP Kills
			56,		//PvP Deaths
			68,		//All Deaths
			64,		//Latency
			48		//Progress
		};



		////////////////

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

			int leftIdx = 0;
			float leftX = UIPlayerStatsEntry.ColumnOffsets[ leftIdx++ ];

			this.Width.Set( 0f, 1f );
			this.Height.Set( 32f, 0f );

			this.PlayerWho = player.whoAmI;

			////

			this.NameElement = new UIText( player.name, 0.7f );
			this.NameElement.Top.Set( 0f, 0f );
			this.NameElement.Left.Set( leftX, 0f );
			this.Append( this.NameElement );

			leftX += UIPlayerStatsEntry.ColumnOffsets[ leftIdx++ ];

			Color teamColor;
			string teamColorName = Enum.GetName( typeof(PlayerTeamName), PlayerHelpers.GetTeamName(player.team, out teamColor) );

			this.TeamElement = new UIText( teamColorName, 0.7f );
			this.TeamElement.TextColor = teamColor;
			this.TeamElement.Top.Set( 0f, 0f );
			this.TeamElement.Left.Set( leftX, 0f );
			this.Append( this.TeamElement );
			
			leftX += UIPlayerStatsEntry.ColumnOffsets[ leftIdx++ ];

			this.PvPKillsElement = new UIText( myplayer.GetPvPKills() + "", 0.7f );
			this.PvPKillsElement.Top.Set( 0f, 0f );
			this.PvPKillsElement.Left.Set( leftX, 0f );
			this.Append( this.PvPKillsElement );
			
			leftX += UIPlayerStatsEntry.ColumnOffsets[ leftIdx++ ];

			this.PvPDeathsElement = new UIText( myplayer.GetPvPDeaths() + "", 0.7f );
			this.PvPDeathsElement.Top.Set( 0f, 0f );
			this.PvPDeathsElement.Left.Set( leftX, 0f );
			this.Append( this.PvPDeathsElement );
			
			leftX += UIPlayerStatsEntry.ColumnOffsets[ leftIdx++ ];

			this.TotalDeathsElement = new UIText( myplayer.GetTotalDeaths() + "", 0.7f );
			this.TotalDeathsElement.Top.Set( 0f, 0f );
			this.TotalDeathsElement.Left.Set( leftX, 0f );
			this.Append( this.TotalDeathsElement );
			
			leftX += UIPlayerStatsEntry.ColumnOffsets[ leftIdx++ ];

			this.LatencyElement = new UIText( myplayer.GetLatency() + "", 0.7f );
			this.LatencyElement.Top.Set( 0f, 0f );
			this.LatencyElement.Left.Set( leftX, 0f );
			this.Append( this.LatencyElement );
			
			leftX += UIPlayerStatsEntry.ColumnOffsets[ leftIdx++ ];

			this.ProgressElement = new UIText( myplayer.GetProgress(), 0.7f );
			this.ProgressElement.Top.Set( 0f, 0f );
			this.ProgressElement.Left.Set( leftX, 0f );
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

			this.PvPKillsElement.SetText( myplayer.GetPvPKills() + "" );
			this.PvPDeathsElement.SetText( myplayer.GetPvPDeaths() + "" );
			this.TotalDeathsElement.SetText( myplayer.GetTotalDeaths() + "" );
			this.LatencyElement.SetText( myplayer.GetLatency() + "" );
			this.ProgressElement.SetText( myplayer.GetProgress() );
		}
	}
}
