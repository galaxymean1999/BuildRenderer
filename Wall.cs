using Microsoft.Xna.Framework;

namespace BuildRenderer {
	public class Wall {
		public Wall(Vector2 pos1, Vector2 pos2, int portal) {
			this.pos1 = pos1; this.pos2 = pos2; this.portal = portal;
		}

		public Vector2 pos1;
		public Vector2 pos2;

		//public int textureID;
		public int portal = -1;
	}
}
