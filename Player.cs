using Microsoft.Xna.Framework;
using System;

namespace BuildRenderer {
	public class Player {
		public Player() {

		}

		public Vector2 position = new Vector2(0, 0);
		public float heading = 0;

		public float FOV = MathF.PI / 3;

		public int currentSectorID = 0;
	}
}
