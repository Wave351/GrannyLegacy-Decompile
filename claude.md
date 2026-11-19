--for if you want to port to roblox idk--


Read CLAUDE.md. Milestone 1 now, files not plans.

Create the Rojo project skeleton, then port the player controller from MobileFPS.cs and CrouchHolder.cs (walk, sprint, crouch, FOV kick from PlayerStatus.cs's SpeedFOVFade/MaxFOV) and a greybox of the ground floor from the door/drawer naming in Days.cs.

Server-authoritative movement with client prediction is out of scope — client movement, server-validated teleport checks only. First person, camera pivot at head height matching PlayerStatus's camera setup.

Write the .project.json, the PlayerModule, and the greybox builder. Stop when those run in Studio.
