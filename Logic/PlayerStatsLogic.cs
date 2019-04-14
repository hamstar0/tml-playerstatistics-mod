using System;
using System.Collections.Generic;

namespace PlayerStatistics.Logic {
	class PlayerStatsLogic {
		private IDictionary<int, int> BossNpcKills = new Dictionary<int, int>();
		//private VanillaEventFlag EventsConquered;

		public int PvPKills;
		public int PvPDeaths;
		public int TotalDeaths;
		public int Latency;
		public string ProgressOveride;



		////////////////

		public void SetStats( int pvpKills, int pvpDeaths, int totalDeaths, string progress) {
			this.PvPKills = pvpKills;
			this.PvPDeaths = pvpDeaths;
			this.TotalDeaths = totalDeaths;
			this.ProgressOveride = progress;
		}
	}
}
