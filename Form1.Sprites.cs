using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Gui
{
    /// <summary>
    /// Sprite fighters: one Player per fighter keeps the animation state, and DrawSpriteFighter draws it.
    /// The game engine (Form1.Battle.cs) still does all the moving, hitting and AI.
    /// </summary>
    public partial class Form1 : Form
    {
        private const float SpriteScale = 3f;            // each sprite pixel becomes 3x3 screen pixels
        private const float SpriteFootOffset = 184f;     // feet sit this far below the fighter's y (same as the old shape fighters)

        // Tinatayang taas ng nakikitang katawan ng sprite (mula paa hanggang ulo), sa screen pixels.
        // Dito nakabase ang pwesto ng name tag. Kung mataas pa/mababa ang tag sa ulo, baguhin ito.
        private const float SpriteVisibleHeight = 110f;

        // Shows a message when the game starts, saying whether the sprite picture was found.
        // Naka-false na dahil gumagana na ang sprites. Gawing true ulit kung kailangan mag-check.
        private const bool ShowSpriteCheck = false;

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (!ShowSpriteCheck) return;

            bool renderingUpdated = typeof(Form1)
                .GetMethod("DrawArticulatedFighter", BindingFlags.Instance | BindingFlags.NonPublic)?
                .GetParameters().Any(p => p.Name == "sprite") ?? false;

            MessageBox.Show(this,
                "Sprite picture: " + FighterSprites.Status +
                "\nForm1.Rendering.cs is the new version: " + (renderingUpdated ? "yes" : "NO - replace it with the new file"),
                "Sprite check");
        }

        private readonly Player p1Sprite = new Player();
        private readonly Player p2Sprite = new Player();

        // Called once per game tick (see EngineLoop_Tick in Form1.cs)
        private void UpdateFighterSprites()
        {
<<<<<<< HEAD
            p1Sprite.FighterIndex = p1CharIndex;
            p2Sprite.FighterIndex = p2CharIndex;
            p1Sprite.SyncFromGame(p1X, p1VelX, p1VelY, p1Y >= GroundLevel, p1Action, p1ActionTimer, false);
            p2Sprite.SyncFromGame(p2X, p2VelX, p2VelY, p2Y >= GroundLevel, p2Action, p2ActionTimer, true);
=======
            // GINAMIT ANG p1FacingRight AT p2FacingRight PARA SA DYNAMIC BOTH-SIDE FACING LOGIC
            p1Sprite.SyncFromGame(p1X, p1VelX, p1VelY, p1Y >= GroundLevel, p1Action, p1ActionTimer, p1FacingRight);
            p2Sprite.SyncFromGame(p2X, p2VelX, p2VelY, p2Y >= GroundLevel, p2Action, p2ActionTimer, p2FacingRight);
        }

        // Tamang text ng name tag base sa TUNAY na player (hindi sa direksyon ng pagharap)
        private string GetFighterTag(FighterInfo fighter, Player? sprite)
        {
            if (sprite != null && ReferenceEquals(sprite, p2Sprite) && isVsCPU) return "CPU: " + fighter.Name;
            return fighter.Name;
        }

        // Kulay ng tag: P1 = cyan, P2/CPU = pula-orange (kapareho ng HUD), preview = puti
        private Color GetFighterTagColor(Player? sprite)
        {
            if (sprite == null) return Color.White;
            return ReferenceEquals(sprite, p1Sprite) ? Color.Cyan : Color.OrangeRed;
        }

        // Nagdo-draw ng name tag na nakagitna sa ulo. Kapag magkadikit ang dalawang fighter,
        // lumalayo ang tag ng isa't isa para hindi magsapawan.
        private void DrawFighterNameTag(Graphics g, string tag, Color color, float centerX, float tagY, Player? sprite)
        {
            using (Font nameFont = new Font("Impact", 11, FontStyle.Italic))
            {
                StringFormat fmt = StringFormat.GenericTypographic;
                float tw = g.MeasureString(tag, nameFont, int.MaxValue, fmt).Width;

                float drawCenter = centerX;
                if (sprite != null)
                {
                    float otherCenter = ReferenceEquals(sprite, p1Sprite) ? p2X + 54f : p1X + 54f;
                    float dx = centerX - otherCenter;
                    float needed = tw + 12f;                       // layo na kailangan ng dalawang tag
                    if (Math.Abs(dx) < needed)
                    {
                        float dir = dx >= 0f ? 1f : -1f;
                        drawCenter += dir * ((needed - Math.Abs(dx)) / 2f);   // tulakan palayo
                    }
                }

                float tx = drawCenter - (tw / 2f);
                using (SolidBrush shadow = new SolidBrush(Color.FromArgb(200, 0, 0, 0)))
                using (SolidBrush main = new SolidBrush(color))
                {
                    g.DrawString(tag, nameFont, shadow, tx + 1.5f, tagY + 1.5f, fmt);
                    g.DrawString(tag, nameFont, main, tx, tagY, fmt);
                }
            }
>>>>>>> d65bdb8cefa2138fe9b989a0160f4098ba193fc7
        }

        // Replaces the shape-drawn fighter whenever the sprite picture is available.
        // sprite = null draws the standing pose (character select).
        private void DrawSpriteFighter(Graphics g, float x, float y, int action, bool faceRight, FighterInfo fighter, Player? sprite)
        {
            float cy = y;
            float footY = cy + SpriteFootOffset;
            float centerX = x + 54f;

            // Simple ground shadow
            float shadowScale = Math.Max(0.4f, 1f - ((GroundLevel - y) / 250f));
            using (SolidBrush shadow = new SolidBrush(Color.FromArgb(140, 0, 0, 0)))
            {
                g.FillEllipse(shadow, x + 15, GroundLevel + 160, 90 * shadowScale, 20 * shadowScale);
            }

            // Elder's Hyper Armor: a golden glow while an attack cannot be interrupted
            if (sprite != null && sprite.HasHyperArmor(action))
            {
                using (GraphicsPath auraPath = new GraphicsPath())
                {
                    auraPath.AddEllipse(x - 25, cy - 25, 170, 230);
                    using (PathGradientBrush glow = new PathGradientBrush(auraPath))
                    {
                        glow.CenterColor = Color.FromArgb(150, Color.Gold);
                        glow.SurroundColors = new Color[] { Color.Transparent };
                        g.FillPath(glow, auraPath);
                    }
                }
            }

            string animation = sprite?.CurrentAnimation ?? "Idle";
            int frame = sprite?.Frame ?? 0;
            bool hurt = sprite?.Hurt ?? false;
            FighterSprites.Draw(g, Array.IndexOf(GameData.Roster, fighter), animation, frame, centerX, footY, SpriteScale, faceRight, hurt, sprite?.CurrentAge ?? AgeStage.Youth);

<<<<<<< HEAD
            // No name tag during the round intro and the K.O. screen: the big announcement text sits right there
            if (currentState == GameState.FightIntro || currentState == GameState.KO) return;

            bool isCpu = isVsCPU && sprite != null && ReferenceEquals(sprite, p2Sprite);
            using (Font nameFont = new Font("Impact", 11, FontStyle.Italic))
            {
                string tag = isCpu ? "CPU: " + fighter.Name : fighter.Name;
                float tagWidth = g.MeasureString(tag, nameFont).Width;
                g.DrawString(tag, nameFont, Brushes.White, centerX - (tagWidth / 2f), footY - (FighterSprites.CellH * SpriteScale) - 22f);   // centered over the fighter
            }
=======
            // Name tag: nakagitna sa ulo ng fighter, tamang P1 / CPU label
            float tagY = footY - SpriteVisibleHeight - 20f;
            DrawFighterNameTag(g, GetFighterTag(fighter, sprite), GetFighterTagColor(sprite), centerX, tagY, sprite);
>>>>>>> d65bdb8cefa2138fe9b989a0160f4098ba193fc7
        }
    }
}