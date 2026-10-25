using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Gui
{
    /// <summary>
<<<<<<< HEAD
    /// In-match HUD: health bars, age tags, round wins, Chrono-Shadow meters, combo text, and announcements.
=======
    /// In-match HUD: health bars, heat gauge, skill bar, combo text, and announcements.
    ///
    /// LAYOUT (bawat player, mula taas pababa):
    ///   [ health bar ]
    ///   [ PANGALAN ............ HEAT ]   <- iisang row, magkabilang gilid
    ///   [ heat bar ]
    ///   (labas ng glass panel)
    ///   [F] skill hints
    ///   COMBO CHARGE
    ///   HITS COMBO!
>>>>>>> d65bdb8cefa2138fe9b989a0160f4098ba193fc7
    /// </summary>
    public partial class Form1 : Form
    {
        private const int HudMargin = 50;
        private const int HudTopY = 32;
        private const int HudBarH = 26;
        private const int HudCenterGap = 170;   // espasyo sa gitna para sa timer at round pips

        // Lapad ng health bar: kasya kahit maliit ang window
        private int HudBarW()
        {
            return Math.Min(380, (this.ClientSize.Width - (HudMargin * 2) - HudCenterGap) / 2);
        }

        // Text na SIGURADONG kasya. align: 0 = kaliwa, 1 = gitna, 2 = kanan (x ang anchor)
        private void DrawFitAligned(Graphics g, string text, string family, float size, FontStyle style,
                                    Color color, float x, float y, float maxWidth, int align)
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
                float dx = align == 0 ? x : (align == 1 ? x - (tw / 2f) : x - tw);
                g.DrawString(text, f, b, dx, y, fmt);
            }
        }

        private void DrawModernHUD(Graphics g)
        {
            int w = this.ClientSize.Width;
            int barW = HudBarW();
            int barH = HudBarH, topY = HudTopY;
            int heatH = 6;

<<<<<<< HEAD
            DrawFighterPanel(g, 1, 50, barW, barH, topY);
            DrawFighterPanel(g, 2, w - 50 - barW, barW, barH, topY);

            // Round timer
=======
            int nameY = topY + barH + 3;        // row ng pangalan + HEAT label
            int heatY = topY + barH + 29;       // heat bar (may espasyo sa ilalim ng pangalan)
            int panelH = barH + 44;             // sakop ng glass panel hanggang sa ilalim ng heat bar

            int p1BarX = HudMargin;
            int p2BarX = w - HudMargin - barW;

            // Max HP bawat fighter (Youth 100 / Veteran 85 / Elder 60 + skin)
            int p1Max = Math.Max(1, p1MaxHealth);
            int p2Max = Math.Max(1, p2MaxHealth);

            // ================= PLAYER 1 =================
            using (SolidBrush glass = new SolidBrush(Color.FromArgb(160, 20, 20, 30)))
                g.FillRectangle(glass, p1BarX - 6, topY - 6, barW + 12, panelH);
            using (Pen frame = new Pen(Color.FromArgb(80, 255, 255, 255), 1))
                g.DrawRectangle(frame, p1BarX - 6, topY - 6, barW + 12, panelH);

            int p1RedW = (int)(barW * Math.Clamp((float)p1RedHealth / p1Max, 0f, 1f));
            g.FillRectangle(Brushes.DarkRed, p1BarX + (barW - p1RedW), topY, p1RedW, barH);

            int p1CurW = (int)(barW * Math.Clamp((float)p1Health / p1Max, 0f, 1f));
            if (p1CurW > 0)
            {
                using (LinearGradientBrush gb = new LinearGradientBrush(new Rectangle(p1BarX, topY, barW, barH), Color.Gold, Color.OrangeRed, LinearGradientMode.Horizontal))
                    g.FillRectangle(gb, p1BarX + (barW - p1CurW), topY, p1CurW, barH);
            }
            using (Pen outline = new Pen(Color.White, 2))
                g.DrawRectangle(outline, p1BarX, topY, barW, barH);

            // Row: pangalan (kaliwa) ........ HEAT (kanan)
            DrawFitAligned(g, GameData.Roster[p1CharIndex].Name, "Impact", 15, FontStyle.Italic, Color.White,
                           p1BarX, nameY, barW - 80, 0);
            DrawFitAligned(g, "HEAT", "Segoe UI", 8, FontStyle.Bold, Color.Cyan,
                           p1BarX + barW, nameY + 5, 70, 2);

            g.FillRectangle(Brushes.Black, p1BarX, heatY, barW, heatH);
            int p1HeatW = (int)(barW * Math.Clamp((float)p1Heat / MaxHeat, 0f, 1f));
            using (SolidBrush hb = new SolidBrush(p1Heat >= 50 ? Color.Cyan : Color.DeepSkyBlue))
                g.FillRectangle(hb, p1BarX + (barW - p1HeatW), heatY, p1HeatW, heatH);

            // ================= TIMER (gitna) =================
>>>>>>> d65bdb8cefa2138fe9b989a0160f4098ba193fc7
            string t = roundTimer.ToString();
            using (Font tf = new Font("Impact", 36, FontStyle.Bold))
            {
                SizeF sz = g.MeasureString(t, tf);
                g.DrawString(t, tf, Brushes.Yellow, (w / 2f) - (sz.Width / 2f), topY - 10);
            }

<<<<<<< HEAD
            // Round number and round wins (two pips each, filled once a round is won)
            using (Font rf = new Font("Segoe UI", 8, FontStyle.Bold))
            {
                string roundLabel = RoundLabel();
                SizeF rs = g.MeasureString(roundLabel, rf);
                g.DrawString(roundLabel, rf, Brushes.Silver, (w / 2f) - (rs.Width / 2f), topY + 44);
            }
            for (int i = 0; i < GameConfig.RoundsToWin; i++)
=======
            // ================= PLAYER 2 =================
            using (SolidBrush glass = new SolidBrush(Color.FromArgb(160, 20, 20, 30)))
                g.FillRectangle(glass, p2BarX - 6, topY - 6, barW + 12, panelH);
            using (Pen frame = new Pen(Color.FromArgb(80, 255, 255, 255), 1))
                g.DrawRectangle(frame, p2BarX - 6, topY - 6, barW + 12, panelH);

            int p2RedW = (int)(barW * Math.Clamp((float)p2RedHealth / p2Max, 0f, 1f));
            g.FillRectangle(Brushes.DarkRed, p2BarX, topY, p2RedW, barH);

            int p2CurW = (int)(barW * Math.Clamp((float)p2Health / p2Max, 0f, 1f));
            if (p2CurW > 0)
>>>>>>> d65bdb8cefa2138fe9b989a0160f4098ba193fc7
            {
                DrawWinPip(g, (w / 2f) - 38 - (i * 20), topY + 66, i < p1RoundWins, Color.Cyan);
                DrawWinPip(g, (w / 2f) + 24 + (i * 20), topY + 66, i < p2RoundWins, Color.OrangeRed);
            }
<<<<<<< HEAD
=======
            using (Pen outline = new Pen(Color.White, 2))
                g.DrawRectangle(outline, p2BarX, topY, barW, barH);

            // Row (naka-mirror): HEAT (kaliwa) ........ pangalan (kanan)
            string p2DisplayName = isVsCPU ? $"CPU ({GameData.Roster[p2CharIndex].Name})" : GameData.Roster[p2CharIndex].Name;
            DrawFitAligned(g, p2DisplayName, "Impact", 15, FontStyle.Italic, Color.White,
                           p2BarX + barW, nameY, barW - 80, 2);
            DrawFitAligned(g, "HEAT", "Segoe UI", 8, FontStyle.Bold, Color.OrangeRed,
                           p2BarX, nameY + 5, 70, 0);

            g.FillRectangle(Brushes.Black, p2BarX, heatY, barW, heatH);
            int p2HeatW = (int)(barW * Math.Clamp((float)p2Heat / MaxHeat, 0f, 1f));
            using (SolidBrush hb = new SolidBrush(p2Heat >= 50 ? Color.OrangeRed : Color.DarkOrange))
                g.FillRectangle(hb, p2BarX + (barW - p2HeatW), heatY, p2HeatW, heatH);

            // ================= SKILL HINTS (labas ng panel) =================
            DrawLoadoutHud(g);
>>>>>>> d65bdb8cefa2138fe9b989a0160f4098ba193fc7

            // ================= COMBO TEXT (sa ilalim ng skill hints) =================
            int comboY = HudTopY + panelH + 38;
            if (p1ComboHits > 1)
            {
                string txt = $"{p1ComboHits} HITS COMBO!";
                DrawFitAligned(g, txt, "Impact", 28, FontStyle.Italic, Color.Red, p1BarX + 2, comboY + 2, barW, 0);
                DrawFitAligned(g, txt, "Impact", 28, FontStyle.Italic, Color.Gold, p1BarX, comboY, barW, 0);
            }
            if (p2ComboHits > 1)
            {
                string txt = $"{p2ComboHits} HITS COMBO!";
                DrawFitAligned(g, txt, "Impact", 28, FontStyle.Italic, Color.Red, p2BarX + barW + 2, comboY + 2, barW, 2);
                DrawFitAligned(g, txt, "Impact", 28, FontStyle.Italic, Color.Gold, p2BarX + barW, comboY, barW, 2);
            }

<<<<<<< HEAD
            using (Font gf = new Font("Segoe UI", 8, FontStyle.Bold))
            {
                string helpControls = isVsCPU
                    ? "[A/D] Move (tap twice = Dash)  [W] Jump  [S] Duck  [Q/E/R] Attack  [S+Q/E] Down Atk  [W+Q/E] Jump Atk  [X] Shadow (mid-attack)  [T] Block"
                    : "P1: [A/D/W/S] Move  [Q/E/R] Attack  [X] Shadow  [T] Block   |   P2: [J/L/I/K] Move  [U/O/P] Attack  [M] Shadow  [Y] Block";
                g.DrawString(helpControls, gf, Brushes.Silver, 20, this.ClientSize.Height - 26);
            }
=======
            // ================= CONTROLS HELP (ibaba, may dark strip para mabasa) =================
            string helpControls = isVsCPU
                ? "P1: [A/D] Move  [W] Jump  [F/G/T] Skill Slots 1-3    |    Opponent: Smart CPU AI"
                : "P1: [A/D/W] Move/Jump  [F/G/T] Skills   |   P2: [J/L/I] Move/Jump  [K/O/P] Skills";
            int stripH = 22;
            int stripY = this.ClientSize.Height - stripH - 8;
            using (SolidBrush strip = new SolidBrush(Color.FromArgb(130, 0, 0, 0)))
                g.FillRectangle(strip, 0, stripY, w, stripH);
            DrawFitAligned(g, helpControls, "Segoe UI", 9, FontStyle.Bold, Color.Silver, w / 2f, stripY + 3, w - 60, 1);
>>>>>>> d65bdb8cefa2138fe9b989a0160f4098ba193fc7
        }

        private static void DrawWinPip(Graphics g, float x, float y, bool won, Color color)
        {
<<<<<<< HEAD
            RectangleF r = new RectangleF(x, y, 14, 14);
            if (won)
            {
                using (SolidBrush fill = new SolidBrush(color)) g.FillEllipse(fill, r);
=======
            int barW = HudBarW();
            int p1BarX = HudMargin, p2BarX = this.ClientSize.Width - HudMargin - barW;
            int panelBottom = HudTopY - 6 + (HudBarH + 44);
            int skillY = panelBottom + 6;

            string p1Text = BuildSkillBarText(p1Loadout, new[] { "F", "G", "T" });
            string p2Text = BuildSkillBarText(p2Loadout, isVsCPU ? null : new[] { "K", "O", "P" });

            DrawFitAligned(g, p1Text, "Segoe UI", 8, FontStyle.Bold, Color.Cyan, p1BarX, skillY, barW, 0);
            DrawFitAligned(g, p2Text, "Segoe UI", 8, FontStyle.Bold, Color.OrangeRed, p2BarX + barW, skillY, barW, 2);

            if (p1Loadout.ComboCharge > 0)
            {
                DrawFitAligned(g, $"COMBO CHARGE x{p1Loadout.ComboCharge}", "Segoe UI", 8, FontStyle.Bold, Color.Gold,
                               p1BarX, skillY + 16, barW, 0);
>>>>>>> d65bdb8cefa2138fe9b989a0160f4098ba193fc7
            }
            using (Pen outline = new Pen(won ? Color.White : Color.Gray, 2)) g.DrawEllipse(outline, r);
        }

        // Health bar, age tag and Chrono-Shadow meter for one fighter. Player 1 is on the left, player 2 on the right.
        private void DrawFighterPanel(Graphics g, int who, int barX, int barW, int barH, int topY)
        {
            bool left = who == 1;
            Player sp = SpriteOf(who);
            int health = left ? p1Health : p2Health;
            int redHealth = left ? p1RedHealth : p2RedHealth;

            using (SolidBrush glass = new SolidBrush(Color.FromArgb(160, 20, 20, 30)))
            {
<<<<<<< HEAD
                g.FillRectangle(glass, barX - 6, topY - 6, barW + 12, barH + 66);
            }
            using (Pen frame = new Pen(Color.FromArgb(80, 255, 255, 255), 1))
            {
                g.DrawRectangle(frame, barX - 6, topY - 6, barW + 12, barH + 66);
            }

            // The bars fill toward the center. A fighter who has aged has a smaller health pool, so the
            // outer part of the bar is greyed out: that is the max HP lost to The Mortal Toll.
            int redW = (int)(barW * ((float)redHealth / MaxHealth));
            int curW = (int)(barW * ((float)health / MaxHealth));
            int maxW = (int)(barW * (sp.MaxHealth / MaxHealth));
            int lostW = barW - maxW;

            int redX = left ? barX + (barW - redW) : barX;
            g.FillRectangle(Brushes.DarkRed, redX, topY, redW, barH);

            if (curW > 0)
            {
                int curX = left ? barX + (barW - curW) : barX;
                Color c1 = left ? Color.Gold : Color.OrangeRed;
                Color c2 = left ? Color.OrangeRed : Color.Gold;
                using (LinearGradientBrush gb = new LinearGradientBrush(new Rectangle(barX, topY, barW, barH), c1, c2, LinearGradientMode.Horizontal))
                {
                    g.FillRectangle(gb, curX, topY, curW, barH);
                }
            }

            if (lostW > 0)
            {
                int lostX = left ? barX : barX + barW - lostW;
                using (SolidBrush lost = new SolidBrush(Color.FromArgb(200, 25, 25, 35)))
                using (HatchBrush hatch = new HatchBrush(HatchStyle.WideUpwardDiagonal, Color.FromArgb(90, 120, 120, 140), Color.FromArgb(200, 25, 25, 35)))
                {
                    g.FillRectangle(hatch, lostX, topY, lostW, barH);
                }
            }
            using (Pen border = new Pen(Color.White, 2)) g.DrawRectangle(border, barX, topY, barW, barH);

            // Name and age
            string name = FighterDisplayName(who);
            string age = CombatRules.AgeName(sp.CurrentAge);
            Color ageColor = sp.CurrentAge == AgeStage.Youth ? Color.LimeGreen : (sp.CurrentAge == AgeStage.Veteran ? Color.Orange : Color.Crimson);
            using (Font nf = new Font("Impact", 14, FontStyle.Italic))
            using (Font af = new Font("Impact", 11))
            using (SolidBrush ageBrush = new SolidBrush(ageColor))
            {
                SizeF ns = g.MeasureString(name, nf);
                SizeF ags = g.MeasureString(age, af);
                if (left)
                {
                    g.DrawString(name, nf, Brushes.White, barX, topY + barH + 2);
                    g.DrawString(age, af, ageBrush, barX + ns.Width, topY + barH + 5);
                }
                else
                {
                    g.DrawString(name, nf, Brushes.White, barX + barW - ns.Width, topY + barH + 2);
                    g.DrawString(age, af, ageBrush, barX + barW - ns.Width - ags.Width, topY + barH + 5);
                }
            }

            // Chrono-Shadow meter. The notch marks how much one phantom costs.
            int meterY = topY + barH + 24;
            int meterH = 16;
            int charges = (int)(sp.ShadowMeter / GameConfig.ShadowCost);
            bool ready = charges > 0;

            using (SolidBrush back = new SolidBrush(Color.FromArgb(255, 10, 20, 40)))
            {
                g.FillRectangle(back, barX, meterY, barW, meterH);
            }

            int meterW = (int)(barW * (sp.ShadowMeter / GameConfig.MaxShadowMeter));
            if (meterW > 0)
            {
                int meterX = left ? barX + (barW - meterW) : barX;
                Color m1 = ready ? Color.DeepSkyBlue : Color.SteelBlue;
                Color m2 = ready ? Color.White : Color.LightSteelBlue;
                using (LinearGradientBrush mb = new LinearGradientBrush(new Rectangle(meterX, meterY, Math.Max(1, meterW), meterH), m1, m2, LinearGradientMode.Vertical))
                {
                    g.FillRectangle(mb, meterX, meterY, meterW, meterH);
                }
            }

            int costW = (int)(barW * (GameConfig.ShadowCost / GameConfig.MaxShadowMeter));
            int notchX = left ? barX + barW - costW : barX + costW;
            g.DrawLine(Pens.White, notchX, meterY - 3, notchX, meterY + meterH + 3);
            using (Pen meterBorder = new Pen(ready ? Color.DeepSkyBlue : Color.LightSteelBlue, 2))
            {
                g.DrawRectangle(meterBorder, barX, meterY, barW, meterH);
            }

            using (Font lf = new Font("Segoe UI", 9, FontStyle.Bold))
            {
                string label = ready ? $"CHRONO-SHADOW  x{charges}  READY" : "CHRONO-SHADOW";
                SizeF ls = g.MeasureString(label, lf);
                float lx = left ? barX : barX + barW - ls.Width;
                float ly = meterY + meterH + 3;
                Brush main = ready ? Brushes.DeepSkyBlue : Brushes.White;
                g.DrawString(label, lf, Brushes.Black, lx + 1, ly + 1);   // outline so it reads on any background
                g.DrawString(label, lf, main, lx, ly);
=======
                DrawFitAligned(g, $"COMBO CHARGE x{p2Loadout.ComboCharge}", "Segoe UI", 8, FontStyle.Bold, Color.Gold,
                               p2BarX + barW, skillY + 16, barW, 2);
>>>>>>> d65bdb8cefa2138fe9b989a0160f4098ba193fc7
            }
        }

        private void DrawTextCentered(Graphics g, string text, Font font, Brush brush, int width, float y)
        {
            SizeF size = g.MeasureString(text, font);
            g.DrawString(text, font, brush, (width / 2f) - (size.Width / 2f), y);
        }

        // Small PAUSE button at the top of the screen, and the dimmed pause menu
        private void DrawPauseUi(Graphics g, int w, int h)
        {
            Rectangle b = PauseButtonRect;
            using (SolidBrush back = new SolidBrush(Color.FromArgb(170, 15, 15, 25)))
            using (Pen edge = new Pen(Color.FromArgb(200, 255, 255, 255), 1))
            using (Font f = new Font("Segoe UI", 8, FontStyle.Bold))
            {
                g.FillRectangle(back, b);
                g.DrawRectangle(edge, b);
                DrawTextCentered(g, paused ? "RESUME" : "II PAUSE", f, Brushes.White, w, b.Y + 3);
            }

            if (!paused) return;

            using (SolidBrush dim = new SolidBrush(Color.FromArgb(170, 0, 0, 0)))
            {
                g.FillRectangle(dim, 0, 0, w, h);
            }
            using (Font big = new Font("Impact", 54, FontStyle.Italic))
            using (Font small = new Font("Segoe UI", 14, FontStyle.Bold))
            {
                DrawTextCentered(g, "PAUSED", big, Brushes.Gold, w, 190);
                DrawTextCentered(g, "[ ESC ] or [ ENTER ]  Resume", small, Brushes.White, w, 300);
                DrawTextCentered(g, "[ R ]  Quit to menu", small, Brushes.LightGray, w, 332);
            }
        }

        private void DrawAnnouncement(Graphics g, string text, Color color)
        {
            int w = this.ClientSize.Width;
            using (Font af = new Font("Impact", 60, FontStyle.Italic))
<<<<<<< HEAD
            using (SolidBrush brush = new SolidBrush(color))
            {
                SizeF sz = g.MeasureString(text, af);
                g.DrawString(text, af, Brushes.DarkRed, (w / 2f) - (sz.Width / 2f) + 4, 184);
                g.DrawString(text, af, brush, (w / 2f) - (sz.Width / 2f), 180);
=======
            using (SolidBrush shadow = new SolidBrush(Color.DarkRed))
            using (SolidBrush main = new SolidBrush(color))
            {
                SizeF sz = g.MeasureString(text, af);
                g.DrawString(text, af, shadow, (w / 2f) - (sz.Width / 2f) + 4, 184);
                g.DrawString(text, af, main, (w / 2f) - (sz.Width / 2f), 180);
>>>>>>> d65bdb8cefa2138fe9b989a0160f4098ba193fc7
            }
        }
    }
}