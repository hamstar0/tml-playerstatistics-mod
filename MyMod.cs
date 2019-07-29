using HamstarHelpers.Components.UI;
using HamstarHelpers.Helpers.TModLoader;
using HamstarHelpers.Helpers.TModLoader.Mods;
using HamstarHelpers.Services.Hooks.ExtendedHooks;
using HamstarHelpers.Services.UI.ControlPanel;
using PlayerStatistics.UI;
using Terraria;
using Terraria.ModLoader;


namespace PlayerStatistics {
	class PlayerStatisticsMod : Mod {
		public const string ControlPanelName = "Player Stats";

		public static string GithubUserName => "hamstar0";
		public static string GithubProjectName => "tml-playerstatistics-mod";


		////////////////

		public static PlayerStatisticsMod Instance { get; private set; }



		////////////////

		public PlayerStatisticsConfig Config => this.GetConfig<PlayerStatisticsConfig>();

		public UIPlayerStatsTab PlayerStatsUI;

		public ModHotKey ControlPanelHotkey;



		////////////////

		public PlayerStatisticsMod() {
			PlayerStatisticsMod.Instance = this;
		}

		////

		public override void Load() {
			this.ControlPanelHotkey = this.RegisterHotKey( "Toggle Player Statistics", "OemTilde" );
		}
		
		public override void PostSetupContent() {
			if( !Main.dedServ ) {
				// Add player stats tab
				this.PlayerStatsUI = new UIPlayerStatsTab( UITheme.Vanilla );
				ControlPanelTabs.AddTab( PlayerStatisticsMod.ControlPanelName, this.PlayerStatsUI );

				// Register boss kills to indicate progress
				ExtendedPlayerHooks.AddNpcKillHook( (plr, npc) => {
					var myplayer = TmlHelpers.SafelyGetModPlayer<PlayerStatisticsPlayer>( plr );
					myplayer.RegisterNpcKillIfBoss( npc );
				} );
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
