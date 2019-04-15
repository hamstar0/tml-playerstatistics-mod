using System;
using HamstarHelpers.Helpers.DebugHelpers;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;


namespace PlayerStatistics.UI {
	class UIHideableScrollbar : UIScrollbar {
		public static bool IsScrollbarHidden( int playerCount, UIList list ) {
			int listItemHeight = playerCount * (int)( UIPlayerStatsEntry.DefaultHeight + 4 );
			int listHeight = (int)list.Height.Pixels - 8;

			return listItemHeight < listHeight;
		}



		////////////////

		public override void Draw( SpriteBatch sb ) {
			var tab = this.Parent.Parent as UIPlayerStatsTab;
			if( tab == null ) { return; }

			if( UIHideableScrollbar.IsScrollbarHidden( tab.PlayerCount, this.Parent as UIList ) ) {
				base.Draw( sb );
			}
		}
	}
}
