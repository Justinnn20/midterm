using System.Drawing.Drawing2D;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Gui
{
    public partial class Form1 : Form
    {
        // ------------------------------------------------------------------
        // Helper: nagdo-draw ng text na SIGURADONG kasya sa maxWidth.
        // Kapag masyadong malapad, pinaliliit ang font hanggang kumasya.
        // centered = true  -> ang x ay GITNA ng text
        // centered = false -> ang x ay KALIWANG GILID ng text
        // ------------------------------------------------------------------
        private void DrawFit(Graphics g, string text, string family, float size, FontStyle style,
                             Color color, float x, float y, float maxWidth, bool centered = false)
        {
            StringFormat fmt = StringFormat.GenericTypographic;
            float s = size;
            Font f = new Font(family, s, style);
            while (s > 6f && g.MeasureString(text, f, int.MaxValue, fmt).Width > maxWidth)
            {
                f.Dispose();
                s -= 0.5f;
                f = new Font(family, s, style);
            }

            using (f)
            using (SolidBrush b = new SolidBrush(color))
            {
                float tw = g.MeasureString(text, f, int.MaxValue, fmt).Width;
                float dx = centered ? x - (tw / 2f) : x;
                g.DrawString(text, f, b, dx, y, fmt);
            }
        }

        // ------------------------------------------------------------------
        // MODE SELECT
        // ------------------------------------------------------------------
        private void DrawModeSelection(Graphics g)
        {
            int w = this.ClientSize.Width, h = this.ClientSize.Height;
            g.Clear(Color.FromArgb(10, 10, 15));
            float cx = w / 2f;

<<<<<<< HEAD
<<<<<<< HEAD
            using (Font titleF = new Font("Impact", 44, FontStyle.Italic))
            {
                const string gameTitle = "Clash Arena";
                float titleX = (w / 2f) - (g.MeasureString(gameTitle, titleF).Width / 2f);
                g.DrawString(gameTitle, titleF, Brushes.OrangeRed, titleX + 3, 36);
                g.DrawString(gameTitle, titleF, Brushes.Gold, titleX, 33);
            }
            using (Font subF = new Font("Segoe UI", 14, FontStyle.Bold))
            {
                DrawTextCentered(g, "SELECT BATTLE MODE", subF, Brushes.Cyan, w, 140);
            }

            int cardW = 320, cardH = 260, gap = 80, y = 190;
            int startX = (w - ((cardW * 2) + gap)) / 2;
            DrawModeCard(g, startX, y, cardW, cardH, "2-PLAYER BATTLE", "Player 1 vs Player 2\n(Local PVP)", selectedMode == 0, Color.Cyan);
            DrawModeCard(g, startX + cardW + gap, y, cardW, cardH, "ARCADE MODE", "Player 1 vs Computer AI Opponent\n(VS CPU)", selectedMode == 1, Color.Crimson);

            using (Font promptF = new Font("Segoe UI", 12, FontStyle.Bold))
            {
                DrawTextCentered(g, "[ ARROW KEYS ] Choose Mode   |   [ ENTER ] Confirm", promptF, Brushes.Gold, w, h - 70);
            }
=======
            // Title (may anino)
            DrawFit(g, "Clash Arena", "Impact", 44, FontStyle.Italic, Color.OrangeRed, cx + 3, 60, w - 120, true);
            DrawFit(g, "Clash Arena", "Impact", 44, FontStyle.Italic, Color.Gold, cx, 57, w - 120, true);
            DrawFit(g, "SELECT BATTLE MODE", "Segoe UI", 14, FontStyle.Bold, Color.Cyan, cx, 128, w - 120, true);

            // Dalawang card, nakagitna bilang grupo
            int gap = 50;
            int cardW = Math.Min(400, (w - 120 - gap) / 2);
            int cardH = 260, y = 180;
            int startX = (int)((w - ((cardW * 2) + gap)) / 2f);

=======
            // Title (may anino)
            DrawFit(g, "Clash Arena", "Impact", 44, FontStyle.Italic, Color.OrangeRed, cx + 3, 60, w - 120, true);
            DrawFit(g, "Clash Arena", "Impact", 44, FontStyle.Italic, Color.Gold, cx, 57, w - 120, true);
            DrawFit(g, "SELECT BATTLE MODE", "Segoe UI", 14, FontStyle.Bold, Color.Cyan, cx, 128, w - 120, true);

            // Dalawang card, nakagitna bilang grupo
            int gap = 50;
            int cardW = Math.Min(400, (w - 120 - gap) / 2);
            int cardH = 260, y = 180;
            int startX = (int)((w - ((cardW * 2) + gap)) / 2f);

>>>>>>> d65bdb8cefa2138fe9b989a0160f4098ba193fc7
            DrawModeCard(g, startX, y, cardW, cardH, "2-PLAYER BATTLE", "Player 1 vs Player 2 (Local PVP)", selectedMode == 0, Color.Cyan);
            DrawModeCard(g, startX + cardW + gap, y, cardW, cardH, "ARCADE MODE (VS CPU)", "Player 1 vs Computer AI Opponent", selectedMode == 1, Color.Crimson);

            DrawFit(g, "[ ARROW KEYS ] Choose Mode   |   [ ENTER ] Confirm", "Segoe UI", 12, FontStyle.Bold, Color.Gold, cx, h - 70, w - 60, true);
<<<<<<< HEAD
>>>>>>> d65bdb8cefa2138fe9b989a0160f4098ba193fc7
=======
>>>>>>> d65bdb8cefa2138fe9b989a0160f4098ba193fc7
        }

        // One selectable card. The title, text and "selected" label are all centered inside the card
        // and the text wraps, so nothing runs past the edge.
        private void DrawModeCard(Graphics g, int x, int y, int w, int h, string title, string sub, bool isSelected, Color neon, string selectedText = "★ READY TO FIGHT ★")
        {
            using (Pen p = new Pen(isSelected ? neon : Color.FromArgb(50, 50, 65), isSelected ? 4 : 2)) g.DrawRectangle(p, x, y, w, h);
            using (LinearGradientBrush bg = new LinearGradientBrush(new Rectangle(x, y, w, h),
                isSelected ? Color.FromArgb(30, 25, 45) : Color.FromArgb(15, 15, 20),
                Color.FromArgb(10, 10, 15), LinearGradientMode.Vertical))
            {
                g.FillRectangle(bg, x + 2, y + 2, w - 4, h - 4);
            }

<<<<<<< HEAD
<<<<<<< HEAD
            const float pad = 16f;
            using (StringFormat center = new StringFormat { Alignment = StringAlignment.Center })
            using (StringFormat centerOneLine = new StringFormat { Alignment = StringAlignment.Center, FormatFlags = StringFormatFlags.NoWrap })
            {
                // Title: shrinks until it fits inside the card
                float size = 22f;
                Font tf = new Font("Impact", size, FontStyle.Italic);
                while (size > 12f && g.MeasureString(title, tf).Width > w - (pad * 2))
                {
                    tf.Dispose();
                    size -= 1f;
                    tf = new Font("Impact", size, FontStyle.Italic);
                }
                g.DrawString(title, tf, isSelected ? Brushes.White : Brushes.Gray, new RectangleF(x + pad, y + 36, w - (pad * 2), 44), centerOneLine);
                tf.Dispose();

                using (Font sf = new Font("Segoe UI", 10))
                {
                    g.DrawString(sub, sf, Brushes.Silver, new RectangleF(x + pad, y + 92, w - (pad * 2), h - 150), center);
                }

                if (isSelected)
                {
                    using (Font selF = new Font("Impact", 16))
                    using (SolidBrush neonBrush = new SolidBrush(neon))
                    {
                        g.DrawString(selectedText, selF, neonBrush, new RectangleF(x + pad, y + h - 56, w - (pad * 2), 30), centerOneLine);
                    }
                }
            }
        }

        private void DrawDifficultySelection(Graphics g)
=======
            float textW = w - 40;
            DrawFit(g, title, "Impact", 22, FontStyle.Italic, isSelected ? Color.White : Color.Gray, x + 20, y + 40, textW);
            DrawFit(g, sub, "Segoe UI", 10, FontStyle.Regular, Color.Silver, x + 20, y + 90, textW);

            if (isSelected)
            {
                DrawFit(g, "★ READY TO FIGHT ★", "Impact", 16, FontStyle.Regular, neon, x + (w / 2f), y + 180, textW, true);
            }
        }

        // ------------------------------------------------------------------
        // CHARACTER SELECT
        // ------------------------------------------------------------------
        private void DrawCharacterSelection(Graphics g)
>>>>>>> d65bdb8cefa2138fe9b989a0160f4098ba193fc7
        {
            int w = this.ClientSize.Width, h = this.ClientSize.Height;
            g.Clear(Color.FromArgb(10, 10, 15));
            float cx = w / 2f;

<<<<<<< HEAD
            using (Font titleF = new Font("Impact", 34, FontStyle.Italic))
            {
                DrawTextCentered(g, "CHOOSE CPU DIFFICULTY", titleF, Brushes.Gold, w, 45);
            }

            string[] names = { "EASY", "MEDIUM", "HARD" };
            string[] notes =
            {
                "Slow to react and rarely defends. Never uses Chrono-Shadow. Good for learning the controls.",
                "A balanced opponent that blocks, ducks and mixes up its attacks. Uses Chrono-Shadow now and then.",
                "Reacts fast, blocks and ducks smartly, dashes in often, chains long strings and drops Chrono-Shadows on you."
            };
            Color[] neon = { Color.LimeGreen, Color.Gold, Color.Crimson };

            int cardW = 270, cardH = 260, gap = 30, y = 140;
            int startX = (w - ((cardW * 3) + (gap * 2))) / 2;
            for (int i = 0; i < 3; i++)
            {
                DrawModeCard(g, startX + (i * (cardW + gap)), y, cardW, cardH, names[i], notes[i], (int)cpuDifficulty == i, neon[i], "★ SELECTED ★");
            }

            using (Font promptF = new Font("Segoe UI", 12, FontStyle.Bold))
            {
                DrawTextCentered(g, "[ ARROWS ] Choose Difficulty   |   [ ENTER ] Confirm   |   [ BACKSPACE ] Back", promptF, Brushes.Gold, w, h - 70);
=======
            float textW = w - 40;
            DrawFit(g, title, "Impact", 22, FontStyle.Italic, isSelected ? Color.White : Color.Gray, x + 20, y + 40, textW);
            DrawFit(g, sub, "Segoe UI", 10, FontStyle.Regular, Color.Silver, x + 20, y + 90, textW);

            if (isSelected)
            {
                DrawFit(g, "★ READY TO FIGHT ★", "Impact", 16, FontStyle.Regular, neon, x + (w / 2f), y + 180, textW, true);
>>>>>>> d65bdb8cefa2138fe9b989a0160f4098ba193fc7
            }
        }

        // ------------------------------------------------------------------
        // CHARACTER SELECT
        // ------------------------------------------------------------------
        private void DrawCharacterSelection(Graphics g)
        {
            int w = this.ClientSize.Width, h = this.ClientSize.Height;
            g.Clear(Color.FromArgb(10, 10, 15));
            float cx = w / 2f;

<<<<<<< HEAD
            using (Font titleF = new Font("Impact", 34, FontStyle.Italic))
            {
                string header = isVsCPU ? "CHOOSE YOUR FIGHTER (VS CPU)" : "CHARACTER SELECT (PVP)";
                DrawTextCentered(g, header, titleF, Brushes.Gold, w, 25);
            }

            int cardW = 210, cardH = 340, startY = 100, spacing = 240;
            int startX = (w - (((GameData.Roster.Length - 1) * spacing) + cardW)) / 2;

            using (Font nameF = new Font("Impact", 13))
            using (Font styleF = new Font("Segoe UI", 8))
            using (Font tagF = new Font("Impact", 11))
            using (StringFormat center = new StringFormat { Alignment = StringAlignment.Center, FormatFlags = StringFormatFlags.NoWrap, Trimming = StringTrimming.EllipsisCharacter })
            {
                for (int i = 0; i < GameData.Roster.Length; i++)
                {
                    int cx = startX + (i * spacing);
                    bool isP1 = p1CharIndex == i;
                    bool isP2 = p2CharIndex == i;

                    Color border = isP1 ? Color.Cyan : (isP2 ? Color.Crimson : Color.FromArgb(50, 50, 60));
                    using (Pen bp = new Pen(border, (isP1 || isP2) ? 4 : 2)) g.DrawRectangle(bp, cx, startY, cardW, cardH);

                    using (LinearGradientBrush cb = new LinearGradientBrush(new Rectangle(cx, startY, cardW, cardH),
                        Color.FromArgb(28, 28, 42), Color.FromArgb(10, 10, 15), LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(cb, cx + 2, startY + 2, cardW - 4, cardH - 4);
                    }

                    DrawArticulatedFighter(g, cx + 50, startY + 70, 0, true, GameData.Roster[i]);

                    using (SolidBrush b = new SolidBrush(Color.Black)) g.FillRectangle(b, cx + 2, startY + cardH - 75, cardW - 4, 73);
                    g.DrawString(GameData.Roster[i].Name, nameF, Brushes.White, new RectangleF(cx + 6, startY + cardH - 70, cardW - 12, 22), center);
                    g.DrawString(GameData.Roster[i].Style, styleF, Brushes.LightGray, new RectangleF(cx + 6, startY + cardH - 46, cardW - 12, 16), center);

                    if (isP1) g.DrawString(p1Locked ? "★ P1 LOCKED" : "► P1 SELECT", tagF, Brushes.Cyan, new RectangleF(cx, startY + 12, cardW, 18), center);
                    if (isP2 && p1Locked && !isVsCPU) g.DrawString("► P2 SELECT", tagF, Brushes.Crimson, new RectangleF(cx, startY + 30, cardW, 18), center);
                }
=======
            string header = isVsCPU ? "CHOOSE YOUR FIGHTER (VS CPU)" : "CHARACTER SELECT (PVP)";
            DrawFit(g, header, "Impact", 34, FontStyle.Italic, Color.Gold, cx, 25, w - 80, true);

            // Dynamic: kahit ilan ang fighters, kasya at nakagitna
            int n = GameData.Roster.Length;
            int margin = 40, gap = 12;
            int cardW = Math.Min(210, (w - (margin * 2) - (gap * (n - 1))) / n);
            int cardH = 340, startY = 100;
            int spacing = cardW + gap;
            int startX = (int)((w - ((cardW * n) + (gap * (n - 1)))) / 2f);

            for (int i = 0; i < n; i++)
            {
=======
            string header = isVsCPU ? "CHOOSE YOUR FIGHTER (VS CPU)" : "CHARACTER SELECT (PVP)";
            DrawFit(g, header, "Impact", 34, FontStyle.Italic, Color.Gold, cx, 25, w - 80, true);

            // Dynamic: kahit ilan ang fighters, kasya at nakagitna
            int n = GameData.Roster.Length;
            int margin = 40, gap = 12;
            int cardW = Math.Min(210, (w - (margin * 2) - (gap * (n - 1))) / n);
            int cardH = 340, startY = 100;
            int spacing = cardW + gap;
            int startX = (int)((w - ((cardW * n) + (gap * (n - 1)))) / 2f);

            for (int i = 0; i < n; i++)
            {
>>>>>>> d65bdb8cefa2138fe9b989a0160f4098ba193fc7
                int cardX = startX + (i * spacing);
                bool isP1 = p1CharIndex == i;
                bool isP2 = p2CharIndex == i;

                Color border = isP1 ? Color.Cyan : (isP2 ? Color.Crimson : Color.FromArgb(50, 50, 60));
                using (Pen bp = new Pen(border, (isP1 || isP2) ? 4 : 2)) g.DrawRectangle(bp, cardX, startY, cardW, cardH);

                using (LinearGradientBrush cb = new LinearGradientBrush(new Rectangle(cardX, startY, cardW, cardH),
                    Color.FromArgb(28, 28, 42), Color.FromArgb(10, 10, 15), LinearGradientMode.Vertical))
                {
                    g.FillRectangle(cb, cardX + 2, startY + 2, cardW - 4, cardH - 4);
                }

                // Fighter nakagitna sa card (ang gitna ng katawan ay x + 54)
                DrawArticulatedFighter(g, cardX + (cardW / 2f) - 54f, startY + 70, 0, true, GameData.Roster[i]);

                using (SolidBrush b = new SolidBrush(Color.Black)) g.FillRectangle(b, cardX + 2, startY + cardH - 75, cardW - 4, 73);

                float tx = cardX + 10, tw = cardW - 20;
                DrawFit(g, GameData.Roster[i].Name, "Impact", 13, FontStyle.Regular, Color.White, tx, startY + cardH - 70, tw);
                DrawFit(g, GameData.Roster[i].Style, "Segoe UI", 8, FontStyle.Regular, Color.LightGray, tx, startY + cardH - 48, tw);
                DrawFit(g, "SKILL: " + GameData.Roster[i].SpecialSkill, "Segoe UI", 7, FontStyle.Bold, GameData.Roster[i].AuraColor, tx, startY + cardH - 30, tw);

                if (isP1) DrawFit(g, p1Locked ? "★ P1 LOCKED" : "► P1 SELECT", "Impact", 11, FontStyle.Regular, Color.Cyan, cardX + 12, startY + 12, cardW - 24);
                if (isP2 && p1Locked && !isVsCPU) DrawFit(g, "► P2 SELECT", "Impact", 11, FontStyle.Regular, Color.Crimson, cardX + 12, startY + 30, cardW - 24);
<<<<<<< HEAD
>>>>>>> d65bdb8cefa2138fe9b989a0160f4098ba193fc7
=======
>>>>>>> d65bdb8cefa2138fe9b989a0160f4098ba193fc7
            }

            string prompt = isVsCPU
                ? "[ ARROWS ] Choose Character   |   [ ENTER ] Confirm and Choose Arena   |   [ BACKSPACE ] Back"
                : (!p1Locked ? "[ ARROWS ] Choose P1  |  [ ENTER ] Lock P1" : "[ ARROWS ] Choose P2  |  [ ENTER ] Choose Arena");
<<<<<<< HEAD
<<<<<<< HEAD
            using (Font promptF = new Font("Segoe UI", 12, FontStyle.Bold))
            {
                DrawTextCentered(g, prompt, promptF, Brushes.Gold, w, h - 50);
            }
=======
            DrawFit(g, prompt, "Segoe UI", 12, FontStyle.Bold, Color.Gold, cx, h - 50, w - 60, true);
>>>>>>> d65bdb8cefa2138fe9b989a0160f4098ba193fc7
=======
            DrawFit(g, prompt, "Segoe UI", 12, FontStyle.Bold, Color.Gold, cx, h - 50, w - 60, true);
>>>>>>> d65bdb8cefa2138fe9b989a0160f4098ba193fc7
        }

        private static float EaseOutCubic(float t)
        {
            float inv = 1f - t;
            return 1f - (inv * inv * inv);
        }

        private void ChangeArena(int direction)
        {
            arenaPrevIndex = selectedArenaIndex;
            arenaTransitionDir = direction;
            selectedArenaIndex = (selectedArenaIndex + direction + GameData.Arenas.Length) % GameData.Arenas.Length;
            arenaTransition = 0f;
            Sfx.Play(520, 40);
        }

        private void DrawArenaPreview(Graphics g, int arenaIndex, Rectangle box, float offsetX)
        {
            GraphicsState state = g.Save();
            g.SetClip(box);
            g.TranslateTransform(box.X + offsetX, box.Y);
            float scale = box.Width / 1024f;
            g.ScaleTransform(scale, scale);
            g.TranslateTransform(0, 20);

            DrawDynamicArenaScenery(g, 512f, arenaIndex);

            ArenaInfo arena = GameData.Arenas[arenaIndex];
            foreach (var m in ambientMotes)
            {
                if (arena.BackgroundFile != null) break;
                using (SolidBrush mote = new SolidBrush(Color.FromArgb(m.Alpha, arena.ParticleColor)))
                {
                    g.FillEllipse(mote, m.X, m.Y, m.Size, m.Size);
                }
            }

            g.Restore(state);
        }

        // ------------------------------------------------------------------
        // ARENA SELECT
        // ------------------------------------------------------------------
        private void DrawArenaSelection(Graphics g)
        {
            int w = this.ClientSize.Width, h = this.ClientSize.Height;
            g.Clear(Color.FromArgb(10, 10, 15));
            float cx = w / 2f;

            using Font nameF = new Font("Impact", 16);
            using Font cardF = new Font("Impact", 11);
            using Font subF = new Font("Segoe UI", 9);

            DrawFit(g, "SELECT BATTLE STAGE", "Impact", 30, FontStyle.Italic, Color.Cyan, cx, 12, w - 80, true);

            ArenaInfo current = GameData.Arenas[selectedArenaIndex];

            int boxW = Math.Min(520, w - 80);
            Rectangle box = new Rectangle((w - boxW) / 2, 64, boxW, (int)(boxW * 315f / 520f));
            int glowAlpha = 130;
            using (Pen glow = new Pen(Color.FromArgb(glowAlpha, current.RingNeon), 10))
            {
                g.DrawRectangle(glow, box.X - 3, box.Y - 3, box.Width + 6, box.Height + 6);
            }

            if (arenaTransition < 1f)
            {
                float eased = EaseOutCubic(arenaTransition);
                DrawArenaPreview(g, arenaPrevIndex, box, -arenaTransitionDir * eased * box.Width);
                DrawArenaPreview(g, selectedArenaIndex, box, arenaTransitionDir * (1f - eased) * box.Width);
            }
            else
            {
                DrawArenaPreview(g, selectedArenaIndex, box, 0f);
            }

            Rectangle infoBar = new Rectangle(box.X, box.Bottom - 50, box.Width, 50);
            using (SolidBrush barBrush = new SolidBrush(Color.FromArgb(190, 0, 0, 0)))
            using (SolidBrush nameBrush = new SolidBrush(current.RingNeon))
            {
                g.FillRectangle(barBrush, infoBar);
                g.DrawString(current.Name, nameF, nameBrush, infoBar.X + 12, infoBar.Y + 4);
                g.DrawString(current.Subtitle, subF, Brushes.LightGray, infoBar.X + 12, infoBar.Y + 29);
            }
            using (Pen boxBorder = new Pen(current.RingNeon, 3))
            {
                g.DrawRectangle(boxBorder, box);
            }

            // Dynamic: kasya kahit ilan ang stages, nakagitna
            int n = GameData.Arenas.Length;
            int sideMargin = 30;
            int cardW = Math.Min(210, (w - (sideMargin * 2) - (10 * (n - 1))) / n);
            int cardH = 110, baseY = box.Bottom + 30;
            const float hoverLift = 16f;
            int gap = n > 1 ? Math.Min(50, (w - (sideMargin * 2) - (cardW * n)) / (n - 1)) : 0;
            int spacing = cardW + gap;
            int startX = (int)((w - ((cardW * n) + (gap * (n - 1)))) / 2f);

            for (int i = 0; i < n; i++)
            {
                ArenaInfo arena = GameData.Arenas[i];
                float near = Math.Max(0f, 1f - Math.Abs(i - arenaSlide));
                RectangleF card = new RectangleF(startX + (i * spacing), baseY - (near * hoverLift), cardW, cardH);

                Bitmap? thumb = ArenaImages.Get(arena);
                if (thumb != null)
                {
                    RectangleF art = new RectangleF(card.X + 2, card.Y + 2, card.Width - 4, card.Height - 34);
                    float srcH = 1024f * art.Height / art.Width;
                    g.DrawImage(thumb, art, new RectangleF(0, 150, 1024, srcH), GraphicsUnit.Pixel);
                }

                using (SolidBrush nameBar = new SolidBrush(Color.Black))
                {
                    g.FillRectangle(nameBar, card.X + 2, card.Bottom - 32, card.Width - 4, 30);
                }
                DrawFit(g, arena.Name, "Impact", 11, FontStyle.Regular, i == selectedArenaIndex ? Color.White : Color.Gray,
                        card.X + 8, card.Bottom - 26, card.Width - 16);

                bool highlighted = near > 0.5f;
                using (Pen border = new Pen(highlighted ? arena.RingNeon : Color.FromArgb(50, 50, 65), highlighted ? 4 : 2))
                {
                    g.DrawRectangle(border, card.X, card.Y, card.Width, card.Height);
                }
            }

            float frameX = startX + (arenaSlide * spacing);
            using (Pen frame = new Pen(current.RingNeon, 3))
            {
                g.DrawRectangle(frame, frameX - 8, baseY - hoverLift - 8, cardW + 16, cardH + 16);
            }

            DrawFit(g, "[ ARROWS ] Choose Stage  |  [ ENTER ] START MATCH  |  [ BACKSPACE ] Back", "Segoe UI", 12, FontStyle.Bold, Color.Gold, cx, h - 48, w - 60, true);
        }
    }
}