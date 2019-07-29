using System;
using Terraria.ModLoader.Config;


namespace PlayerStatistics {
	public class PlayerStatisticsConfig : ModConfig {
		public override ConfigScope Mode => ConfigScope.ServerSide;

		public bool SaveStatsPerPlayer = false;
	}
}
