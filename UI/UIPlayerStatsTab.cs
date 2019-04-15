using HamstarHelpers.Components.DataStructures;
using HamstarHelpers.Components.UI;
using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Internals.ControlPanel;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;


namespace PlayerStatistics.UI {
	class UIPlayerStatsTab : UIControlPanelTab {
		private UIList PlayerStatList;
		private UIHideableScrollbar Scrollbar;

		private IDictionary<int, UIPlayerStatsEntry> ActivePlayerElements = new Dictionary<int, UIPlayerStatsEntry>();


		////////////////

		public int PlayerCount => this.ActivePlayerElements.Count;



		////////////////

		public UIPlayerStatsTab( UITheme theme ) {
			this.Theme = theme;

			this.Width.Set( 0f, 1f );
			this.Height.Set( 0f, 1f );
		}


		////////////////

		public override void OnInitializeMe() {
			this.InitializeHeader();

			////

			var modListPanel = new UIPanel();
			modListPanel.Top.Set( 24f, 0f );
			modListPanel.Width.Set( 0f, 1f );
			modListPanel.Height.Set( 480f, 0f );
			modListPanel.HAlign = 0f;
			modListPanel.SetPadding( 4f );
			//modListPanel.PaddingTop = 0.0f;
			modListPanel.BackgroundColor = this.Theme.ListBgColor;
			modListPanel.BorderColor = this.Theme.ListEdgeColor;
			this.Append( (UIElement)modListPanel );

			this.PlayerStatList = new UIList();
			this.PlayerStatList.Width.Set( -25f, 1f );
			this.PlayerStatList.Height.Set( 0f, 1f );
			this.PlayerStatList.HAlign = 0f;
			this.PlayerStatList.ListPadding = 4f;
			this.PlayerStatList.SetPadding( 0f );
			modListPanel.Append( (UIElement)this.PlayerStatList );

			this.Scrollbar = new UIHideableScrollbar( this.PlayerStatList, true );
			this.Scrollbar.Top.Set( 8f, 0f );
			this.Scrollbar.Height.Set( -16f, 1f );
			this.Scrollbar.SetView( 100f, 1000f );
			this.Scrollbar.HAlign = 1f;
			modListPanel.Append( (UIElement)this.Scrollbar );
			this.PlayerStatList.SetScrollbar( this.Scrollbar );
		}


		private void InitializeHeader() {
			int leftIdx = 0;
			float leftX = 16f + UIPlayerStatsEntry.ColumnOffsets[leftIdx++];

			var nameLabel = new UIText( "Name", 0.7f );
			nameLabel.Top.Set( 0f, 0f );
			nameLabel.Left.Set( leftX, 0f );
			this.Append( (UIElement)nameLabel );

			leftX += UIPlayerStatsEntry.ColumnOffsets[leftIdx++];

			var teamLabel = new UIText( "Team", 0.7f );
			teamLabel.Top.Set( 0f, 0f );
			teamLabel.Left.Set( leftX, 0f );
			this.Append( (UIElement)teamLabel );

			leftX += UIPlayerStatsEntry.ColumnOffsets[leftIdx++];

			var pvpKillsLabel = new UIText( "PvP Kills", 0.7f );
			pvpKillsLabel.Top.Set( 0f, 0f );
			pvpKillsLabel.Left.Set( leftX, 0f );
			this.Append( (UIElement)pvpKillsLabel );

			leftX += UIPlayerStatsEntry.ColumnOffsets[leftIdx++];

			var pvpDeathsLabel = new UIText( "PvP Deaths", 0.7f );
			pvpDeathsLabel.Top.Set( 0f, 0f );
			pvpDeathsLabel.Left.Set( leftX, 0f );
			this.Append( (UIElement)pvpDeathsLabel );

			leftX += UIPlayerStatsEntry.ColumnOffsets[leftIdx++];

			var totalDeathsLabel = new UIText( "All Deaths", 0.7f );
			totalDeathsLabel.Top.Set( 0f, 0f );
			totalDeathsLabel.Left.Set( leftX, 0f );
			this.Append( (UIElement)totalDeathsLabel );

			leftX += UIPlayerStatsEntry.ColumnOffsets[leftIdx++];

			var latencyLabel = new UIText( "Latency", 0.7f );
			latencyLabel.Top.Set( 0f, 0f );
			latencyLabel.Left.Set( leftX, 0f );
			this.Append( (UIElement)latencyLabel );

			leftX += UIPlayerStatsEntry.ColumnOffsets[leftIdx++];

			var progressLabel = new UIText( "Progress", 0.7f );
			progressLabel.Top.Set( 0f, 0f );
			progressLabel.Left.Set( leftX, 0f );
			this.Append( (UIElement)progressLabel );
		}


		////////////////

		public bool AddPlayer( Player player ) {
			if( this.PlayerStatList == null ) {
				return false;
			}

			var uiPlrStats = new UIPlayerStatsEntry( player );

			this.PlayerStatList?.Add( uiPlrStats );
			this.PlayerStatList?.UpdateOrder();
			this.ActivePlayerElements[ player.whoAmI ] = uiPlrStats;

			this.Recalculate();
			return true;
		}

		public void RemovePlayer( int playerWho ) {
			UIPlayerStatsEntry uiPlrStats = this.ActivePlayerElements.GetOrDefault( playerWho );

			if( uiPlrStats != null ) {
				this.PlayerStatList?.RemoveChild( uiPlrStats );
				uiPlrStats.Remove();
				this.PlayerStatList?.UpdateOrder();
			}

			this.ActivePlayerElements.Remove( playerWho );

			this.Recalculate();
		}

		public void ClearPlayers() {
			this.PlayerStatList?.Clear();
			this.ActivePlayerElements.Clear();

			this.Recalculate();
		}


		////////////////

		public override void Draw( SpriteBatch spriteBatch ) {
			bool listChanged;

			try {
				this.Scrollbar.IsHidden = UIHideableScrollbar.IsScrollbarHidden( this.PlayerCount, this.PlayerStatList.Parent );

				if( this.Scrollbar.IsHidden ) {
					listChanged = this.PlayerStatList.Width.Pixels != 0;
					this.PlayerStatList.Width.Pixels = 0;
				} else {
					listChanged = this.PlayerStatList.Width.Pixels != -25;
					this.PlayerStatList.Width.Pixels = -25;
				}

				if( listChanged ) {
					this.Recalculate();
					this.PlayerStatList.Recalculate();
				}
			} catch { }

			base.Draw( spriteBatch );
		}


		////////////////

		public void Update() {
			var mymod = PlayerStatisticsMod.Instance;
			
			for( int playerWho = 0; playerWho < Main.player.Length; playerWho++ ) {
				Player plr = Main.player[playerWho];

				if( plr == null || !plr.active ) {
					if( this.ActivePlayerElements.ContainsKey(playerWho) ) {
						this.RemovePlayer( playerWho );
					}
				} else {
					if( !this.ActivePlayerElements.ContainsKey(playerWho) ) {
						this.AddPlayer( plr );
					} else {
						this.ActivePlayerElements[ playerWho ].UpdatePlayerInfo();
					}
				}
			}
		}
	}
}
