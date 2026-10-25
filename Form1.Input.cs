using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Gui
{
    public partial class Form1 : Form
    {
<<<<<<< HEAD
<<<<<<< HEAD
        private void TogglePause()
        {
            paused = !paused;
            Sfx.Play(paused ? 420 : 700, 70);
        }

        // Clicking the small PAUSE button at the top of the screen
        private void Form1_MouseClick(object? sender, MouseEventArgs e)
        {
            bool inFight = currentState == GameState.FightIntro || currentState == GameState.Battle || currentState == GameState.KO;
            if (!inFight || screenDevice.Width <= 0) return;

            int gx = (int)((e.X - screenDevice.X) * GameWidth / (float)screenDevice.Width);
            int gy = (int)((e.Y - screenDevice.Y) * GameHeight / (float)screenDevice.Height);
            if (PauseButtonRect.Contains(gx, gy)) TogglePause();
        }

        private void Form1_KeyUp(object? sender, KeyEventArgs e)
        {
            pressedKeys.Remove(e.KeyCode);
        }
=======
=======
>>>>>>> d65bdb8cefa2138fe9b989a0160f4098ba193fc7
        // Cooldown Timer para hindi ma-spam ang Teleport/Skills
        // (bumababa kada frame sa UpdateBattlePhysics, hindi na sa KeyDown)
        private int p1SkillCooldown = 0;
        private int p2SkillCooldown = 0;
<<<<<<< HEAD
>>>>>>> d65bdb8cefa2138fe9b989a0160f4098ba193fc7
=======
>>>>>>> d65bdb8cefa2138fe9b989a0160f4098ba193fc7

        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            // false while the key is just auto-repeating because it is held down
            bool fresh = pressedKeys.Add(e.KeyCode);

            // PAUSE: Esc or the MENU button (Backspace) during a fight. While paused, Enter also resumes and R quits to the menu.
            bool inFight = currentState == GameState.FightIntro || currentState == GameState.Battle || currentState == GameState.KO;
            if (inFight && fresh && (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Back || (paused && e.KeyCode == Keys.Enter)))
            {
                TogglePause();
                return;
            }
            if (paused)
            {
                if (e.KeyCode == Keys.R)
                {
                    paused = false;
                    currentState = GameState.ModeSelect;
                    p1Locked = false;
                }
                return;
            }

            if (currentState == GameState.ModeSelect)
            {
                if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up) selectedMode = 0;
                if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down) selectedMode = 1;
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
                {
                    isVsCPU = (selectedMode == 1);
                    currentState = isVsCPU ? GameState.DifficultySelect : GameState.CharSelect;   // arcade mode picks the CPU difficulty first
                    p1Locked = false;
                    Sfx.Play(650, 90);
                }
            }
            else if (currentState == GameState.DifficultySelect)
            {
                if ((e.KeyCode == Keys.Left || e.KeyCode == Keys.Up) && cpuDifficulty > CpuDifficulty.Easy)
                {
                    cpuDifficulty--;
                    Sfx.Play(520, 40);
                }
                if ((e.KeyCode == Keys.Right || e.KeyCode == Keys.Down) && cpuDifficulty < CpuDifficulty.Hard)
                {
                    cpuDifficulty++;
                    Sfx.Play(520, 40);
                }
                if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
                {
                    currentState = GameState.CharSelect;
                    p1Locked = false;
                    Sfx.Play(650, 90);
                }
                if (e.KeyCode == Keys.Back) currentState = GameState.ModeSelect;
            }
            else if (currentState == GameState.CharSelect)
            {
                if (!p1Locked)
                {
                    if (e.KeyCode == Keys.Left) p1CharIndex = (p1CharIndex - 1 + GameData.Roster.Length) % GameData.Roster.Length;
                    if (e.KeyCode == Keys.Right) p1CharIndex = (p1CharIndex + 1) % GameData.Roster.Length;
                    if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
                    {
                        p1Locked = true;
                        Sfx.Play(600, 80);
                        if (isVsCPU)
                        {
                            p2CharIndex = (p1CharIndex + 1) % GameData.Roster.Length;
                            currentState = GameState.ArenaSelect;
                            Sfx.Play(750, 100);
                        }
                    }
                    if (e.KeyCode == Keys.Back) currentState = isVsCPU ? GameState.DifficultySelect : GameState.ModeSelect;
                }
                else if (!isVsCPU)
                {
                    if (e.KeyCode == Keys.Left) p2CharIndex = (p2CharIndex - 1 + GameData.Roster.Length) % GameData.Roster.Length;
                    if (e.KeyCode == Keys.Right) p2CharIndex = (p2CharIndex + 1) % GameData.Roster.Length;
                    if (e.KeyCode == Keys.Enter) { currentState = GameState.ArenaSelect; Sfx.Play(750, 100); }
                    if (e.KeyCode == Keys.Back) p1Locked = false;
                }
            }
            else if (currentState == GameState.ArenaSelect)
            {
                if (e.KeyCode == Keys.Left) ChangeArena(-1);
                if (e.KeyCode == Keys.Right) ChangeArena(1);

                if (e.KeyCode == Keys.Enter) StartMatch();

                if (e.KeyCode == Keys.Back)
                {
                    currentState = GameState.CharSelect;
                    if (isVsCPU) p1Locked = false;
                }
            }
            else if (currentState == GameState.Battle)
            {
<<<<<<< HEAD
<<<<<<< HEAD
                HandleBattleKey(e.KeyCode, fresh);
            }
            else if (currentState == GameState.KO && matchOver && e.KeyCode == Keys.R)
=======
=======
>>>>>>> d65bdb8cefa2138fe9b989a0160f4098ba193fc7
                // --- PLAYER 1 MOVEMENT & FACING DIRECTION ---
                if (e.KeyCode == Keys.A && p1X > LeftWall)
                {
                    p1VelX = -13 * SpeedFactor(1);
                    p1FacingRight = false; // Haharap sa KALIWA
                }
                if (e.KeyCode == Keys.D && p1X < RightWall)
                {
                    p1VelX = 13 * SpeedFactor(1);
                    p1FacingRight = true;  // Haharap sa KANAN
                }
                if (e.KeyCode == Keys.W && p1Y >= GroundLevel) { p1VelY = -22; Sfx.Play(440, 60); }

                // --- PLAYER 1 ATTACKS & SKILLS ---
                if (e.KeyCode == Keys.F)
                {
                    ExecuteAttack(1, 0); // Quick Jab / Attack
                }
                if (e.KeyCode == Keys.G)
                {
                    if (p1SkillCooldown <= 0)
                    {
                        UseHeroSkill(1, 2);
                        p1SkillCooldown = 120; // 2 Seconds Cooldown
                    }
                    else
                    {
                        AddDamagePopup(p1X, p1Y - 30, "RECHARGING...", Color.Gray);
                    }
                }
                if (e.KeyCode == Keys.T)
                {
                    if (p1Heat >= 100)
                    {
                        UseHeroSkill(1, 3);
                        p1Heat = 0;
                    }
                    else
                    {
                        AddDamagePopup(p1X, p1Y - 30, "NEED FULL HEAT!", Color.Red);
                    }
                }

                if (!isVsCPU)
                {
                    // --- PLAYER 2 MOVEMENT & FACING DIRECTION ---
                    if (e.KeyCode == Keys.J && p2X > LeftWall)
                    {
                        p2VelX = -13 * SpeedFactor(2);
                        p2FacingRight = false; // Haharap sa KALIWA
                    }
                    if (e.KeyCode == Keys.L && p2X < RightWall)
                    {
                        p2VelX = 13 * SpeedFactor(2);
                        p2FacingRight = true;  // Haharap sa KANAN
                    }
                    if (e.KeyCode == Keys.I && p2Y >= GroundLevel) { p2VelY = -22; Sfx.Play(440, 60); }

                    // --- PLAYER 2 ATTACKS & SKILLS ---
                    if (e.KeyCode == Keys.K)
                    {
                        ExecuteAttack(2, 0);
                    }
                    if (e.KeyCode == Keys.O)
                    {
                        if (p2SkillCooldown <= 0)
                        {
                            UseHeroSkill(2, 2);
                            p2SkillCooldown = 120;
                        }
                        else
                        {
                            AddDamagePopup(p2X, p2Y - 30, "RECHARGING...", Color.Gray);
                        }
                    }
                    if (e.KeyCode == Keys.P)
                    {
                        if (p2Heat >= 100)
                        {
                            UseHeroSkill(2, 3);
                            p2Heat = 0;
                        }
                        else
                        {
                            AddDamagePopup(p2X, p2Y - 30, "NEED FULL HEAT!", Color.Red);
                        }
                    }
                }
            }
            // Results screen lang (pagkatapos ng FINAL ROUND / may nanalo na ng 2 rounds)
            else if (currentState == GameState.MatchResult && (e.KeyCode == Keys.R || e.KeyCode == Keys.Enter))
<<<<<<< HEAD
>>>>>>> d65bdb8cefa2138fe9b989a0160f4098ba193fc7
=======
>>>>>>> d65bdb8cefa2138fe9b989a0160f4098ba193fc7
            {
                currentState = GameState.ModeSelect;
                p1Locked = false;
                Sfx.Play(650, 90);
            }
        }

        /// <summary>
        /// One-shot battle keys. Walking, ducking and blocking are held keys and are read every tick instead
        /// (see ApplyHeldInputs in Form1.Battle.cs).
        ///
        ///   Player 1:  A / D move (double-tap to dash)   W jump   S duck   Q attack   E kick   R skill   T block
        ///   Player 2:  J / L move (double-tap to dash)   I jump   K duck   U attack   O kick   P skill   Y block
        ///
        /// Attack:  mash it for a string (punch, cross, punch...). Duck + attack = down attack, in the air = jump attack.
        /// Kick:    ground kick. Duck + kick or in the air it does the same as attack.
        /// Skill:   the hero skill of the character.
        ///            skill alone        = skill 1
        ///            hold duck + skill  = skill 2
        ///            hold block + skill = skill 3
        ///          Pressed in the middle of an attack, the skill key drops a Chrono-Shadow instead.
        /// </summary>
        private void HandleBattleKey(Keys key, bool fresh)
        {
            if (!fresh) return;

            if (key == Keys.A) RegisterDirectionTap(1, key, -1f);
            else if (key == Keys.D) RegisterDirectionTap(1, key, 1f);
            else if (key == Keys.W) TryJump(1);
            else if (key == Keys.Q) PlayerAttack(1, false);
            else if (key == Keys.E) PlayerAttack(1, true);
            else if (key == Keys.R) PlayerSkill(1);

            if (isVsCPU) return;

            if (key == Keys.J) RegisterDirectionTap(2, key, -1f);
            else if (key == Keys.L) RegisterDirectionTap(2, key, 1f);
            else if (key == Keys.I) TryJump(2);
            else if (key == Keys.U) PlayerAttack(2, false);
            else if (key == Keys.O) PlayerAttack(2, true);
            else if (key == Keys.P) PlayerSkill(2);
        }
    }
}   