using HamstarHelpers.Components.DataStructures;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace PlayerStatistics {
	partial class PlayerStatisticsPlayer : ModPlayer {
		internal void RegisterNpcKillIfBoss( NPC npc ) {
			if( npc.boss ) {
				this.BossNpcKills.AddOrSet( npc.netID, 1 );
			}
		}


		////////////////

		public string FormatVanillaProgress() {
			if( this.player.whoAmI != Main.myPlayer && !string.IsNullOrEmpty(this.ProgressOveride) ) {
				return this.ProgressOveride;
			}

			if( this.BossNpcKills.ContainsKey(NPCID.MoonLordCore) ) {
				return "Moon Lord Defeated";
			}
			/*if( (this.EventsConquered & VanillaEventFlag.Martians) != 0 ) {
				return "Martian Invasion Defeated";
			}*/
			if( this.BossNpcKills.ContainsKey(NPCID.CultistBoss) ) {
				return "Lunatic Cultist Defeated";
			}
			if( this.BossNpcKills.ContainsKey(NPCID.Golem) ) {
				return "Golem Defeated";
			}
			if( this.BossNpcKills.ContainsKey(NPCID.Plantera) ) {
				return "Plantera Defeated";
			}
			if( this.BossNpcKills.ContainsKey(NPCID.TheDestroyer)
				|| this.BossNpcKills.ContainsKey(NPCID.SkeletronPrime)
				|| (this.BossNpcKills.ContainsKey(NPCID.Retinazer) && this.BossNpcKills.ContainsKey(NPCID.Spazmatism)) ) {
				return "Mech Bosses Defeated";
			}
			if( this.BossNpcKills.ContainsKey(NPCID.WallofFlesh) ) {
				return "Hard Mode Attained";
			}
			if( this.BossNpcKills.ContainsKey(NPCID.EaterofWorldsHead)
				|| this.BossNpcKills.ContainsKey(NPCID.BrainofCthulhu) ) {
				return "Evil Biome Conquered";
			}
			if( this.BossNpcKills.ContainsKey(NPCID.SkeletronHead) ) {
				return "Dungeon Accessed";
			}
			/*if( (this.EventsConquered & VanillaEventFlag.Goblins) != 0 ) {
				return "Goblins Defeated";
			}*/
			if( this.BossNpcKills.ContainsKey(NPCID.KingSlime)
				|| this.BossNpcKills.ContainsKey(NPCID.EyeofCthulhu)
				|| this.BossNpcKills.ContainsKey(NPCID.QueenBee) ) {
				return "Boss Kills Begun";
			}
			return "None";
		}
	}
}
