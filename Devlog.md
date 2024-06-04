# Addvon

Genre: 2D platformer, action adventure, rpg, coop

Addvon is about exploring and completing quest to provide for and protect your village. player will start of as newbie adventure completing basic quests earning money for armour and wepons. when player has explored all of there hometown and ranked up new worlds await for player to discover.

## Goals:
- build level.
- Add enemeis.
- Loot/Inventory system.
- boss fight to end Levl One


## Game logic:
- Player controller script: takes keyboard/gamepad input to initiate player states. base script for Player script.
- Scene managment script: monitor and initiate level and game components when changing scenes.
- UI Management script: dependent on player script, changing health and inventory UI.
Player logic:
- Player script: sub class of Parent class HUMAN.monitores player state changing players behaviour and animation from Idle, to walking, attacking, and jumping.
- Enemy Script: sub class of Parent class HUMAN. follows same logic as Player changing enemy logic based of 
enemy state. changing enemy state from patrolling between to walls to chasing player and attacking player.

### Build One{COMPLETE}
above goals completed execpt boss fight
base set for multiple level creation, after boss imlementation create level 2 for build 2 release.

BuildOne.Two:
Level 2 designed and implemented, decorated with envirnmental peices and active patrolling enemies.
UI improved with more features implemeted such as setting menu, provided music audio scaling, UI scaling.
Boss put in place with additional UI boss on boss fight start.

Next To Implement:
<BOSS FIGHT LOGIC>
switch statement:
	Idle behvaiour => stay one position, idle animation, if(in fight => waitForSeconds(RO seitch To different state));
	Chasing Behaviour => walk animation, fallow player position, when in position switch to different attack mode.
	MultipleAttackModes => play attack animations, randomize attack if muliple, if distance atttack available play distance attack when durther from player, change to IDLE or Chasing state after attack.


	

# CREDITS:
### ASSETS USED FROM:

https://craftpix.net/freebies/free-knight-character-sprites-pixel-art/
https://craftpix.net/product/fantasy-knight-sprite-sheets-pixel-art/   //RETURN FORE ASSETS//Continuation of previous asset

https://cainos.itch.io/

https://assetstore.unity.com/publishers/40001

https://karwisch.itch.io/pxui-basic

[NEED TO DONATE TO THESE GUYS FOR THERE AMAZING WORK THEY LOAN FOR FREE]
https://adwitr.itch.io/pixel-health-bar-asset-pack-2?download 
- Health Bar Asset Pack 2 by Adwit Rahman
https://luizmelo.itch.io/monsters-creatures-fantasy?download

https://msfrantz.itch.io/free-fire-ball-pixel-art

https://pixabay.com/sound-effects/civil-war-fanfares-15645/


