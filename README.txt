Jake Apfel
Etched - MVP

In ​Etched​, you are an archeologist who has uncovered the ruins of a great structure from an ancient civilization, all built by a king who was obsessed with geometry. What remains is a series of puzzles reflecting the king’s obsession, with each one based on a particular geometric proof. Traverse unstable platforms, fend off undead enemies, and use ancient machines to construct your proofs and complete the puzzles. Put your life on the line to reach the center of the ruins and obtain a valuable reward.


Tutorial:

1) Machines:
    Cyan draws circles. Pick a center point, then a point to be on the radius. Pick the same point twice to cancel.
    Green (emerald) extends lines. Pick a line segment to extend it "infinitely" in both directions. Pick a point to cancel.
    Lavender draws line segments. Pick one end point and then another. Pick the same point twice to cancel.
    Pink draws parallel lines. Pick a line segment and then a point not on the same line as the line segment to draw a line through the point with slope equal to that of the line segment. Pick a point on the line containing the line segment to cancel.
    Red draws right angles. Pick three points (A, then B, then C). If angle ABC is a right angle, an indication will be made.
    Gold is used to justify constructions (see later).

To use a machine, press "i" while directly next to it.
To pick a point/segment, press "i" while standing on it (trust that it works, ideally I would have added a change of color or something if you interacted with it). Points have priority (really only important for cancelling extending a line segment).
Upon drawing a new line, line segment, or circle, the intersections of the new shape with all existing ones will be calculated and points will be added at any new intersections.
Using a machine creates a new checkpoint that you can restart from if you die, even if the machine doesn't technically do anything (e.g. you "cancel" the use or try to make a line/segment/circle/right angle that already exists).

2) Justifying:
    Only works for the easy level. I have no idea why it doesn't work for intermediate, I really thought I got it right. Ran out of time before implementing  intermediate+1 justification. Follow the steps in the top right of the level. They are intentionally vague but can provide some hints. When complete, the gold platform will come out of the wall so you can stand on it and win.

Details on justifications (and spoilers, at least for the easy level where justification always works):

    Easy: select two pair of segments that are radii of the same circle, one pair for each use of the justification machine. Then the segments in a pair are congruent. If both pairs include the original segment and the other segments form a triangle with the original segment, then you are done by transitivity. You can choose any segments you want, but the game checks to make sure that you have an equilateral triangle and that some form of the above logic follows.

    Intermediate (The logic behind the scenes doesn't always work, but I'm not sure why): select the bisector with the first use of the justification machine, then two congruent triangles (by selecting their vertices) in the next two uses of the machine. If the triangles share the bisector and each has a side on one of the original segments, then the angle has been bisected. You can know the triangles are congruent by SSS and with reasoning similar to that of the easy level justification (sides being radii of the same circles).

    Intermediate + 1 (Not implemented): select the bisector with the first use of the justification machine, then two congruent triangles (by selecting their vertices) in the next two uses of the machine. The correct triangles are known to be congruent by SAS, where the A comes from the previous level. The correct triangles will share part of the bisector and each have half the original segment as a side.

3) Controls:
    q/e to rotate camera.
    wasd to move player.
    left click to attack (relocates sphere enemies that are hit, stuns others that are hit).
    right click to stun (stun all nearby enemies). This is on a cool down of about 8 seconds.
    spacebar to jump. spacebar again while falling to double jump.
    i to interact with a machine/point/segment.
    f to stop/start the camera following the player.
    p to pause/unpause the game.

4) Mastery learning:
    You can't start a level without completing all previous ones. This is functional in the MVP. However, for the MVP, you can lock/unlock both intermediate levels with the button in the bottom right.

5) Enemies:
    There are two in the MVP. While in the "constructing" state, spheres chase you and bother you by pushing you around. They get more aggressive in later levels. The other enemies will always shoot projectiles that cause you to flinch if hit. This slows you down and can cancel jumps, causing you to fall and die. There are more enemies in later levels. See Controls for attacking enemies.

6) Health and losing:
    Health carries over between levels. Lose all lives (10 for the small group of levels in the MVP) and you have to restart from the beginning of the level group (the easy level for MVP, but again, you can unlock the others whenever you want in the MVP). You lose a life only by falling from the platforms. When you lose a life and still have lives remaining, you can restart the current level, start from the last machine use, or exit to the main menu.

7) Winning and score:
    Win by stepping on the golden platform that becomes available upon justification. Score is then calculated based on how quickly you completed the level, how many times you died while in the level, and how many machine uses you needed. "Cancelled" machine uses still count, since they function as checkpoints. When you complete a level, you will be able to go to the next level or exit to the main menu.


Acknowledgements:
    Credit to ExplosiveLLC for the player model and animations, as well as some parts of the player/animator controller scripts. (https://assetstore.unity.com/packages/3d/animations/rpg-character-mecanim-animation-pack-free-65284)
