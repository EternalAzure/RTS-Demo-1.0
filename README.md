# RTS-Demo-1.0
<img src="https://github.com/EternalAzure/RTS-Demo-1.0/blob/main/RTS%20kuva.png" width="961" height="502" />

**Play here: https://eternalazure-web-hosting.herokuapp.com/<br/>**

### Game Features ###
  * Combat camera
  * Click and drag unitcontroller
  * Combat mechanics
  * Animations
  * Spawner
  
Explanations out of date!
----
#### Click and drag camera ####

   [CameraController](Scripts/CameraController.cs)
 
  <p>
    The camera pivots around the battle arena always facing towards the center. </br>
    The camera is moved by clicking anywhere on the screen and dragging the mouse. </br>
    The movement is limited to 180 degrees on one drag.
  </p> 

#### Point and click unitcontroller ####

  [UnitSelection](Scripts/UnitSelection.cs)
  [UnitController](Scripts/UnitController.cs)
  [SoldierController](Scripts/SoldierController.cs)
  
  <p>
   UnitSelection retrieves UnitController from a selected soldier and sets it active. </br>
   Active UnitControllers hold lists of soldiers in the unit. They listen to mouse clicks, </br>
   calculate and convey new relative positions to SoldierControllers. The formation is not held while on the move.
  </p>

#### Combat mechanics ####

 [Soldier](Scripts/Soldier.cs)
 [Swordmen](Scripts/Swordmen.cs)
 [UnitController](Scripts/UnitController.cs)
 
  <p>
    The combat is done solely in Soldier script. A Soldier scans the immediate surroundings </br>
    and inflicts damage to the nearby enemies. Soldiers use stats contained in Swordmen </br>
    but modular design allows variety of soldier types. Upon death Soldier messages UnitController. </br>
    UnitController has a safe way of removing Soldier from its list without causing concurrent modification error.
  </p> 

#### Enemy movement ####

  [AI](Scripts/AI.cs)
  [AIController](Scripts/AIController.cs)
 
  <p>
   The previous design allowed enemy unit to wander blindly between multiple waypoints in 15s intervals, 
   but this design separates movement from movement logic allowing reactivity. 
   Alas, AI was never fully implemented.
  </p> 

#### Animations ####

[CharacterAnimator](Scripts/CharacterAnimator.cs)
  
  <p>
    Script facilitates several attack animations but only one attack animation was animated. 
    Animator uses blend tree to blend between idle and walk animations using character speed. 
    The animator was designed inheritance and subclasses of soldiers in mind.
  </p> 

#### Battle configuration ####

[SelectedUnits](Scripts/SelectedUnits.cs)

  <p>
    The player can choose the number of soldiers in an unit from 1 to 9. GUI only supports one unit 
    for the player and the AI. Furthermore player can adjust starting hp on both teams.
  </p> 
  
#### Simple GUI ####

  [ColorButtons](Scripts/ColorButtons.cs)
  [ChangeScene](Scripts/ChangeScene.cs)
  
  <img src="https://github.com/EternalAzure/RTS-Demo-1.0/blob/main/RTS%20GUI%20kuva%2001.png" width="200" height="180" /> &nbsp;&nbsp;
  <img src="https://github.com/EternalAzure/RTS-Demo-1.0/blob/main/RTS%20GUI%20kuva%2002.png" width="200" height="180" />
  <p>
    Has a restart button to reset ongoing battle using set stats. Has a menu button to get to the battle configuration scene. </br>
    In the battle configuration, grids are dynamically colored using ColorButtons to signal the amount of soldiers selected to fight.
  </p> 

#### Spawner ####

  [Spawner](Scripts/Spawner.cs)
  <p>
    Used to facilitate spawning unlimited amount of units with unlimited amount of soldiers each, </br>
    but got simplified due to scaling down of the project. Builds units using prefabricated gameobject 'axe warrior'.
  </p> 
