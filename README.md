# RTS-Demo-1.0
<img src="https://github.com/EternalAzure/RTS-Demo-1.0/blob/main/RTS%20kuva.png" width="961" height="502" />

**Purpose of this demo is to showcase my coding and ability to finish a project on my own.<br/>
This demo project is finished but I keep developing the game presented here.**

### Game Features ###
  * Click and drag camera
  * Point and click unitcontroller
  * Combat mechanics
  * Enemy movement
  * Animations
  * Battle configuration
  * Simple GUI
  * Spawner
  
----
#### Click and drag camera ####
<details>
 
  [CameraController](Scripts/CameraController.cs)
 
  <p>
    The camera pivots around center of the battle arena always facing towards the center.</br>
    The camera is moved by clicking anywhere on screen and dragging move around. Movement </br> 
    is limited to 180 degrees on one drag.
  </p> 
</details>

#### Point and click unitcontroller ####
<details>
 
  [UnitSelection](Scripts/UnitSelection.cs)
  [UnitController](Scripts/UnitController.cs)
  [SoldierController](Scripts/SoldierController.cs)
  
  <p>
   UnitSelection retrieves UnitController from selected soldier and sets it active.
   Active UnitControllers hold list of soldiers in the unit. They listen to mouse clicks,
   calculate and convey new relative positions to SoldierControllers.
  </p>
</details>

#### Combat mechanics ####
<details>
 
 [Soldier](Scripts/Soldier.cs)
 [Swordmen](Scripts/Swordmen.cs)
 [UnitController](Scripts/UnitController.cs)
 
  <p>
    Combat is done solely in Soldier scripts. A Soldier scans the immediate surroundings
    and inflicts damage to the nearby enemies. Soldiers use stats contained in Swordmen
    but modular design allows variety of soldier types. Upon death Soldier messages UnitController.
    UnitController has safe way to remove Soldier from it's list without causing concurrent modification error.
  </p> 
</details>

#### Enemy movement ####
<details>
 
  [AI](Scripts/AI.cs)
  [AIController](Scripts/AIController.cs)
 
  <p>
    Previous design allowed enemy unit/s to wander blindly between two(or more) waypoints in 15s intervals, 
   but this design separates movement from movement logic allowing   reactivity. 
   Alas, AI was never implemented. I'm going to implement it in final game.
  </p> 
</details>

#### Animations ####
<details>
 
  [CharacterAnimator](Scripts/CharacterAnimator.cs)
  
  <p>
    Facilitates several attack animations but only one attack and one parry animation were animated. 
    Uses blend tree to blend between idle and walk animations using character speed. 
    The animator was designed inheritance and subclasses of soldiers in mind.
  </p> 
</details>

#### Battle configuration ####
<details>
 
  [SelectedUnits](Scripts/SelectedUnits.cs)

  <p>
    Explanation
  </p> 
</details>

#### Simple GUI ####
<details>
 
  [ColorButtons](Scripts/ColorButtons.cs)
  <img src="" width="961" height="502" />
  <p>
    Explanation
  </p> 
</details>

#### Spawner ####
<details>
  [Source code](Scripts)
  <p>
    Explanation
  </p> 
</details>
