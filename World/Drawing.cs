using Agent;
using Auxiliary;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace World
{
    public class DrawingFlags
    {
        public bool DrawSheepSight;
        public bool DrawShepherdsSight;
        public bool DrawSheepPath;
        public bool DrawShepherdsPath;
        public bool DrawCenterOfSheep;
    }

    class Drawing
    {
        private readonly int sizeX;
        private readonly int sizeY;
        private readonly int backgroundOffsetX;
        private readonly int backgroundOffsetY;
        private int OffsetX { get { return backgroundOffsetX + 100; } }
        private int OffsetY { get { return backgroundOffsetY + 100; } }

        private readonly int numberOfSeenShepherds;
        private readonly int numberOfSeenSheep;

        private readonly Color backgroundColor = Color.Black;
        private readonly Color sheepColor = Color.Blue;
        private readonly Color shepherdColor = Color.Red;
        private readonly Color sheepSightColor = Color.DarkBlue;
        private readonly Color shepherdSheepSightColor = Color.DodgerBlue;
        private readonly Color shepherdShepherdSightColor = Color.IndianRed;
        private readonly Color sheepPathColor = Color.DodgerBlue;
        private readonly Color shepherdPathColor = Color.OrangeRed;
        private readonly Color centreOfSheepColor = Color.White;

        private readonly IEnumerable<IMobileAgent> sheep;
        private readonly IEnumerable<IMobileAgent> shepherds;

        public Drawing(int offsetX, int offsetY, int sizeX, int sizeY, IEnumerable<IMobileAgent> sheep, IEnumerable<IMobileAgent> shepherds, int numberOfSeenSheep, int numberOfSeenShepherds)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            this.backgroundOffsetX = offsetX;
            this.backgroundOffsetY = offsetY;

            this.sheep = sheep;
            this.shepherds = shepherds;

            this.numberOfSeenSheep = numberOfSeenSheep;
            this.numberOfSeenShepherds = numberOfSeenShepherds;
        }

        public void Draw(Graphics gfx, DrawingFlags flags)
        {
            DrawBackground(gfx);

            if (flags.DrawSheepSight)
                DrawSheepSight(gfx);

            if (flags.DrawSheepPath)
                DrawPath(gfx, sheep, sheepPathColor);

            if (flags.DrawShepherdsPath)
                DrawPath(gfx, shepherds, shepherdPathColor);

            if(flags.DrawShepherdsSight)
                DrawShepherdsSight(gfx);

            DrawAgents(gfx, sheep, sheepColor);
            DrawAgents(gfx, shepherds, shepherdColor);

            if (flags.DrawCenterOfSheep)
                DrawCentreOfSheep(gfx);
        }

        private void DrawBackground(Graphics gfx)
        {
            gfx.FillRectangle(new SolidBrush(backgroundColor), backgroundOffsetX, backgroundOffsetY, sizeX, sizeY);
        }

        private void DrawSheepSight(Graphics gfx)
        {
            foreach (var s in sheep)
                gfx.FillEllipse(new SolidBrush(Color.DarkBlue), new Rectangle(OffsetX + (int)s.Position.X - 50, OffsetY + (int)s.Position.Y - 50, 100, 100));
        }
        
        private void DrawPath(Graphics gfx, IEnumerable<IMobileAgent> mobiles, Color color)
        {
            foreach (var m in mobiles)
            {
                for (int i = 1; i < m.Path.Count; i++)
                    gfx.DrawLine(new Pen(color), new Point(OffsetX + (int)m.Path[i - 1].X, OffsetY + (int)m.Path[i - 1].Y), new Point(OffsetX + (int)m.Path[i].X, OffsetY + (int)m.Path[i].Y));
            }
        }

        private void DrawShepherdsSight(Graphics gfx)
        {
            foreach(var s in shepherds)
            {
                DrawSight(gfx, s, Finder.FindClosestAgents(sheep, s, numberOfSeenSheep), shepherdSheepSightColor);
                DrawSight(gfx, s, Finder.FindClosestAgents(shepherds, s, numberOfSeenShepherds), shepherdShepherdSightColor);
            }
        }

        private void DrawSight(Graphics gfx, IHasPosition center, IEnumerable<IHasPosition> close, Color color)
        {
            foreach (var c in close)
            {
                gfx.DrawLine(new Pen(color), new Point(OffsetX + (int)center.Position.X, OffsetY + (int)center.Position.Y), new Point(OffsetX + (int)c.Position.X, OffsetY + (int)c.Position.Y));
            }
        }

        private void DrawAgents(Graphics gfx, IEnumerable<IHasPosition> agents, Color color)
        {
            foreach(var a in agents)
                gfx.FillEllipse(new SolidBrush(color), new Rectangle((int)a.Position.X - 4 + OffsetX, (int)a.Position.Y - 4 + OffsetY, 8, 8));
        }

        private void DrawCentreOfSheep(Graphics gfx)
        {
            Position center = sheep.Select(x => x.Position).Center();
            gfx.FillEllipse(new SolidBrush(centreOfSheepColor), new Rectangle(OffsetX + (int)center.X - 2, OffsetY + (int)center.Y - 2, 4, 4));
        }
    }
}
