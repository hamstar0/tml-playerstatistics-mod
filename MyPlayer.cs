using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;


namespace PlayerStatistics {
	class PlayerStatisticsPlayer : ModPlayer {
		private ISet<int> ActivePlayers = new HashSet<int>();



		public override void PreUpdate() {
			if( this.player.whoAmI != Main.myPlayer ) { return; }

			var mymod = (PlayerStatisticsMod)this.mod;
			mymod.PlayerStatsUI.Update();
		}
	}
}
