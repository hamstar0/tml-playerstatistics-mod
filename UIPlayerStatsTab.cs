﻿using HamstarHelpers.Components.DataStructures;
using HamstarHelpers.Components.UI;
using HamstarHelpers.Internals.ControlPanel;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;


namespace PlayerStatistics {
	class UIPlayerStatsTab : UIControlPanelTab {
		private UIList PlayerStatList;
		private IDictionary<int, UIPlayerStatsEntry> ActivePlayerElements = new Dictionary<int, UIPlayerStatsEntry>();



		////////////////

		public UIPlayerStatsTab( UITheme theme ) {
			this.Theme = theme;

			this.Width.Set( 0f, 1f );
			this.Height.Set( 0f, 1f );
		}


		////////////////

		public override void OnInitializeMe() {
			var modListPanel = new UIPanel();
			modListPanel.Top.Set( 0f, 0f );
			modListPanel.Width.Set( 0f, 1f );
			modListPanel.Height.Set( 300f, 0f );
			modListPanel.HAlign = 0f;
			modListPanel.SetPadding( 4f );
			modListPanel.PaddingTop = 0.0f;
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

			UIScrollbar scrollbar = new UIScrollbar();
			scrollbar.Top.Set( 8f, 0f );
			scrollbar.Height.Set( -16f, 1f );
			scrollbar.SetView( 100f, 1000f );
			scrollbar.HAlign = 1f;
			modListPanel.Append( (UIElement)scrollbar );
			this.PlayerStatList.SetScrollbar( scrollbar );
		}


		////////////////

		public void AddPlayer( Player player ) {
			var uiPlrStats = new UIPlayerStatsEntry( player );

			this.PlayerStatList.Add( uiPlrStats );
			this.ActivePlayerElements[player.whoAmI] = uiPlrStats;
		}
		
		public void RemovePlayer( int playerWho ) {
			UIPlayerStatsEntry uiPlrStats = this.ActivePlayerElements.GetOrDefault( playerWho );

			if( uiPlrStats != null ) {
				this.PlayerStatList.RemoveChild( uiPlrStats );
				uiPlrStats.Remove();
			}

			this.ActivePlayerElements.Remove( playerWho );
		}


		////////////////

		public void Update() {
			var mymod = PlayerStatisticsMod.Instance;
			
			for( int i = 0; i < Main.player.Length; i++ ) {
				Player plr = Main.player[i];

				if( plr == null || !plr.active ) {
					if( this.ActivePlayerElements.ContainsKey(i) ) {
						this.RemovePlayer( i );
					}
				} else {
					if( !this.ActivePlayerElements.ContainsKey( i ) ) {
						this.AddPlayer( plr );
					} else {
						this.ActivePlayerElements[i].UpdatePlayerInfo();
					}
				}
			}
		}
	}
}
