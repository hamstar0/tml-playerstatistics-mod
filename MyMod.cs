using HamstarHelpers.Components.Config;
using HamstarHelpers.Components.Errors;
using HamstarHelpers.Components.UI;
using HamstarHelpers.Helpers.DebugHelpers;
using HamstarHelpers.Helpers.TmlHelpers;
using HamstarHelpers.Helpers.TmlHelpers.ModHelpers;
using HamstarHelpers.Services.ControlPanel;
using Terraria;
using Terraria.ModLoader;


namespace PlayerStatistics {
	class PlayerStatisticsMod : Mod {
		public static PlayerStatisticsMod Instance { get; private set; }



		////////////////

		public JsonConfig<PlayerStatisticsConfigData> ConfigJson { get; private set; }
		public PlayerStatisticsConfigData Config => this.ConfigJson.Data;

		public UIPlayerStatsTab PlayerStats;



		////////////////

		public PlayerStatisticsMod() {
			this.ConfigJson = new JsonConfig<PlayerStatisticsConfigData>(
				PlayerStatisticsConfigData.ConfigFileName,
				ConfigurationDataBase.RelativePath,
				new PlayerStatisticsConfigData()
			);
		}

		public override void Load() {
			string depErr = TmlHelpers.ReportBadDependencyMods( this );
			if( depErr != null ) { throw new HamstarException( depErr ); }

			PlayerStatisticsMod.Instance = this;

			this.LoadConfig();
		}
		
		public override void PostSetupContent() {
			if( !Main.dedServ ) {
				this.PlayerStats = new UIPlayerStatsTab( UITheme.Vanilla );

				ControlPanelTabs.AddTab( "Player Stats", this.PlayerStats );
			}
		}


		////

		private void LoadConfig() {
			if( !this.ConfigJson.LoadFile() ) {
				this.ConfigJson.SaveFile();
			}

			if( this.Config.CanUpdateVersion() ) {
				this.Config.UpdateToLatestVersion();

				LogHelpers.Log( "Player Statistics updated to " + this.Version.ToString() );
				this.ConfigJson.SaveFile();
			}
		}

		////

		public override void Unload() {
			PlayerStatisticsMod.Instance = null;
		}


		////////////////

		public override object Call( params object[] args ) {
			return ModBoilerplateHelpers.HandleModCall( typeof( PlayerStatisticsAPI ), args );
		}
	}
}
