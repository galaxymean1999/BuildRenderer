using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace BuildRenderer {
	public class Player {
		public Player() {

		}

		public Vector2 position = new Vector2(0, 0);

		public float eyeLevel = 0.5f;

		public float heading = 0;

		public float FOV = MathF.PI / 3;

		public int currentSectorID = 0;

		public void Update() {
			if (Keyboard.GetState().IsKeyDown(Keys.D)) {
				heading += 0.02f;
			}
			if (Keyboard.GetState().IsKeyDown(Keys.A)) {
				heading -= 0.02f;
			}
			if (Keyboard.GetState().IsKeyDown(Keys.W)) {
				position += new Vector2(MathF.Cos(heading) / 20, MathF.Sin(heading) / 20);
			}
			if (Keyboard.GetState().IsKeyDown(Keys.S)) {
				position -= new Vector2(MathF.Cos(heading) / 20, MathF.Sin(heading) / 20);
			}

			heading = Renderer.NormaliseAngle(heading);
		}
	}
}
