# Asteroid_Game

- Player's health, movement and rotation speed can me modified through "Player" script that is attached on Player prefab present in game scene.

- The gameplay difficulty can be controlled through asteroid's speed and the damage that the asteroid gives to the player on collision (Each asteroid will give different damage according to its' type). Designers can manage the difficulty through GameManager script by playing through Difficulty inncrease duration, Astroid speed multiplier, Astroid damage multiplier.
After a certain period of time, the speed and damage power of new asteroids will increase repeatedly.

- Powerup manager have variables to configure how often and in what ratio/probablity powerups will appear. 

- Asteroid data, weapon data and shields data will be managed through Scriptable objects


========Asteroid Data Scriptable Object==========

- In AsteroidData scriptable object (preset in Assets>ScriptableObjects), the designer can set different type of AsteroidInfo int the list that will contain Asteroid spirte, speed, split count, damage power.

- The split variable of AsteroidInfo will handle the number of times the main asteroid will get splitted in 2 after getting hit by bullet. For example: If the split variable value is 3 then on getting hit by bullet, the main asteroid will get splitted into 2 parts and those 2 parts can further be splitted into 2 parts.(Assume 3 generations)

- The damage power holds the value that will be the amount of damage an asteroid will give to the player. If the Main asteroid have the 30 damage, then its splitted child astroids will also hold same damage power

- If the asteroid is splitted into two parts, then two new asteroids will carry same info as its parents (Speed and damage power)
=====================================================


========SpecialWeapon Data Scriptable Object==========

- In SpecialWeaponData scriptableObject(preset in Assets>ScriptableObjects), the designer can add all the weapons that are available for pickups. The scriptable object contains a a list of WeaponInfo which holds properties of the weapon like weapon sprite(that will appear as powerup), weapon time(lifetime of weapon), weaponPrefab(that will be fired), isBurstFireWeapon(which will decide if a weapon is burst weapon) etc.

- Put the weaponInfo in the list of SpecialWeaponData scriptableObject and player will get the option of weapon as a powerpickup which he can use as well.

- A new prefab needs be be created for every new weapon added. I cannot use the same prefab for every weapon just by changing the sprite because different weapon have different colliders. So every weapon will have a seperate prefab with "weapon" script attached on it.
=======================================================


=============ShieldData Scriptable Object===============

- In ShieldData scriptableObject(preset in Assets>ScriptableObjects), the designer can add all the shields that are available for pickups. The scriptable object contains a a list of ShildInfo that holds properties of the shield like shield sprite(that will appear as powerup), shieldPrefab(that will be spawned around player), hitCount(number of damage a shield can take from asteroid)

- Put the shieldInfo in the list of ShieldData scriptableObject and player will get the option of shield as a powerpickup which he can use as well.

- Same as weapon , i cant use the same prefab for every shield beacuse differend kind of shield have different collider that will protect the player. So every shield will have a seperate prefab with "shield" script attached on it.
=======================================================


=================Managers==================

GameManager- Handles game related functionalty like difficulty level and game related actions

WeaponManager- Handles weapon related functionality like firing weapon, switching weapon, firing single weapon, firing burst.

PowerupManager- Handles the powerup related functionalty like spawning powerUps at regular intervals and maintain the quantity of powerups.

AsteroidManager- Handles the Asteroid related functionalty like spawning asteroids, splitting asteroids, setting asteroid properties.

UIManager- Handles the UI part of the game.

CutsceneHandler- It will manage the cutscene part of the game.

=============================================

*If you think the collision of asteriods create too much bounce, then u can change the values in physics material or can remove the physics material from the asteroid prefab.





