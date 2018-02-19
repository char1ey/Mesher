using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Mesher.GraphicsCore.Text;
using SharpFont;

namespace Mesher
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			
		}

	/*	protected override void OnPaint(PaintEventArgs e)
		{
			var lib = new Library();
			
			Face face = new Face(lib, "C:\\Users\\Charley\\Desktop\\Mesher\\Mesher\\bin\\Debug\\timesi.ttf");
			face.LoadGlyph(39, LoadFlags.NoBitmap | LoadFlags.NoHinting | LoadFlags.NoAutohint | LoadFlags.NoScale | LoadFlags.LinearDesign | LoadFlags.IgnoreTransform, LoadTarget.Normal);

			var arcs = new GlyphGeometryDecomposer(face.Glyph.Outline);

			for (var i = 1; i < arcs.Endpoints.Count; i++)
			{
				if (!double.IsPositiveInfinity(arcs.Endpoints[i].D))
				{
					e.Graphics.DrawLine(new Pen(Color.Red, 1), (float)arcs.Endpoints[i - 1].P.X / 2 + 100, 
						(float)arcs.Endpoints[i - 1].P.Y / 2 + 100, 
						(float)arcs.Endpoints[i].P.X / 2 + 100,
						(float)arcs.Endpoints[i].P.Y / 2 + 100);

					
				}e.Graphics.DrawRectangle(new Pen(Color.Blue, 3), new Rectangle((int)((float)arcs.Endpoints[i].P.X / 2 + 100),
						(int)((float)arcs.Endpoints[i].P.Y / 2 + 100), 3, 3));
			}

			base.OnPaint(e);
		}*/
	}
}
