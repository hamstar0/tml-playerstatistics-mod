using HamstarHelpers.Components.UI;
using HamstarHelpers.Internals.ControlPanel;
using System;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;


namespace PlayerStatistics {
	class UIPlayerStatsTab : UIControlPanelTab {
		private UIList PlayerStatList;



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
			
			this.PlayerStatList.AddRange( new UIText[] {
				new UIText( "a" ),
				new UIText( "b" ),
				new UIText( "b" ),
				new UIText( "b" ),
				new UIText( "b" ),
				new UIText( "b" ),
				new UIText( "b" ),
				new UIText( "b" ),
				new UIText( "b" ),
				new UIText( "b" ),
				new UIText( "b" ),
				new UIText( "b" )
			} );
		}
	}
}
