using System;
using HamstarHelpers.Helpers.Debug;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;


namespace PlayerStatistics.UI {
	class UIHideableScrollbar : UIScrollbar {
		public static bool IsScrollbarHidden( int playerCount, UIElement container ) {
			int listItemHeight = playerCount * (int)( UIPlayerStatsEntry.DefaultHeight + 4 );
			int listHeight = (int)container.Height.Pixels - 8;
			
			return listItemHeight < listHeight;
		}



		////////////////

		private UIList List;
		public bool IsHidden;
		
		

		////////////////

		public UIHideableScrollbar( UIList list, bool isHidden ) {
			this.List = list;
			this.IsHidden = isHidden;
		}

		////////////////
		
		public override void Draw( SpriteBatch sb ) {
			if( !this.IsHidden ) {
				base.Draw( sb );
			}
		}
	}
}
