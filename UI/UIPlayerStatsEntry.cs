using HamstarHelpers.Helpers.Debug;
using HamstarHelpers.Helpers.Players;
using HamstarHelpers.Helpers.TModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.UI.Elements;


namespace PlayerStatistics.UI {
	class UIPlayerStatsEntry : UIPanel {
		public static float DefaultHeight => 32f;
		public static float[] ColumnOffsets => new float[] {
			0f,		//Name
			116f,	//Team
			64f,	//PvP Kills
			56f,	//PvP Deaths
			68f,	//All Deaths
			64f,	//Latency
			48f		//Progress
		};



		////////////////

		private readonly int PlayerWho;

		public UIText NameElement;
		public UIText TeamElement;
		public UIText PvPKillsElement;
		public UIText PvPDeathsElement;
		public UIText TotalDeathsElement;
		public UIText LatencyElement;
		public UIText ProgressElement;

		private int PvpKills;
		private int PvpDeaths;
		private int TotalDeaths;
		private int Latency;
		private int ProgressAmount;
		


		////////////////

		public UIPlayerStatsEntry( Player player ) {
			var myplayer = TmlHelpers.SafelyGetModPlayer<PlayerStatisticsPlayer>( player );

			int leftIdx = 0;
			float leftX = UIPlayerStatsEntry.ColumnOffsets[ leftIdx++ ];

			this.Width.Set( 0f, 1f );
			this.Height.Set( UIPlayerStatsEntry.DefaultHeight, 0f );

			this.PlayerWho = player.whoAmI;

			////

			this.NameElement = new UIText( player.name, 0.7f );
			this.NameElement.Top.Set( 0f, 0f );
			this.NameElement.Left.Set( leftX, 0f );
			this.Append( this.NameElement );

			leftX += UIPlayerStatsEntry.ColumnOffsets[ leftIdx++ ];

			Color teamColor;
			string teamColorName = Enum.GetName( typeof(PlayerTeamName), PlayerTeamHelpers.GetTeamName(player.team, out teamColor) );

			this.TeamElement = new UIText( teamColorName, 0.7f );
			this.TeamElement.TextColor = teamColor;
			this.TeamElement.Top.Set( 0f, 0f );
			this.TeamElement.Left.Set( leftX, 0f );
			this.Append( this.TeamElement );
			
			leftX += UIPlayerStatsEntry.ColumnOffsets[ leftIdx++ ];

			this.PvPKillsElement = new UIText( myplayer.GetPvpKills() + "", 0.7f );
			this.PvPKillsElement.Top.Set( 0f, 0f );
			this.PvPKillsElement.Left.Set( leftX, 0f );
			this.Append( this.PvPKillsElement );
			
			leftX += UIPlayerStatsEntry.ColumnOffsets[ leftIdx++ ];

			this.PvPDeathsElement = new UIText( myplayer.GetPvpDeaths() + "", 0.7f );
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
			
			this.ProgressElement = new UIText( myplayer.GetProgress(out this.ProgressAmount), 0.7f );
			this.ProgressElement.Top.Set( 0f, 0f );
			this.ProgressElement.Left.Set( leftX, 0f );
			this.Append( this.ProgressElement );
		}


		////////////////

		public override int CompareTo( object obj ) {
			var other = obj as UIPlayerStatsEntry;
			if( other == null ) {
				return -1;
			}

			if( this.PvpKills > other.PvpKills ) {
				return -1;
			} else if( this.PvpKills < other.PvpKills ) {
				return 1;
			} else {
				return 0;
			}
		}


		////////////////

		public void UpdatePlayerInfo() {
			Player plr = Main.player[ this.PlayerWho ];
			var myplayer = TmlHelpers.SafelyGetModPlayer<PlayerStatisticsPlayer>( plr );

			Color teamColor;
			string teamColorName = Enum.GetName( typeof(PlayerTeamName), PlayerTeamHelpers.GetTeamName(plr.team, out teamColor) );

			this.TeamElement.SetText( teamColorName );
			this.TeamElement.TextColor = teamColor;

			this.PvpKills = myplayer.GetPvpKills();
			this.PvpDeaths = myplayer.GetPvpDeaths();
			this.TotalDeaths = myplayer.GetTotalDeaths();
			this.Latency = myplayer.GetLatency();
			string progress = myplayer.GetProgress( out this.ProgressAmount );

			this.PvPKillsElement.SetText( this.PvpKills + "" );
			this.PvPDeathsElement.SetText( this.PvpDeaths + "" );
			this.TotalDeathsElement.SetText( this.TotalDeaths + "" );
			this.LatencyElement.SetText( this.Latency + "" );
			this.ProgressElement.SetText( progress );

			this.Recalculate();
		}
	}
}
