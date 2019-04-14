using System;


namespace PlayerStatistics {
	public static class PlayerStatisticsAPI {
		public static PlayerStatisticsConfigData GetModSettings() {
			return PlayerStatisticsMod.Instance.Config;
		}
	}
}
