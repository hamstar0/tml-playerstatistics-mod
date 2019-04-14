using HamstarHelpers.Components.Config;
using System;


namespace PlayerStatistics {
	public class PlayerStatisticsConfigData : ConfigurationDataBase {
		public static string ConfigFileName => "Player Statistics Config.json";



		////////////////

		public string VersionSinceUpdate = "";



		////////////////

		private void SetDefaults() { }

		////////////////
		
		public bool CanUpdateVersion() {
			if( this.VersionSinceUpdate == "" ) { return true; }
			var versSince = new Version( this.VersionSinceUpdate );
			return versSince < PlayerStatisticsMod.Instance.Version;
		}

		public void UpdateToLatestVersion() {
			var mymod = PlayerStatisticsMod.Instance;
			var newConfig = new PlayerStatisticsConfigData();
			var versSince = this.VersionSinceUpdate != "" ?
				new Version( this.VersionSinceUpdate ) :
				new Version();

			if( this.VersionSinceUpdate == "" ) {
				this.SetDefaults();
			}

			this.VersionSinceUpdate = mymod.Version.ToString();
		}
	}
}
