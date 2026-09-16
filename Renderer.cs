using Microsoft.Xna.Framework.Graphics;

namespace BuildRenderer {
	public class Renderer {
		public Renderer(Player player, Level level) {
			this.player = player;
			this.level = level;
		}

		private Player player;

		private Level level;

		public void Render(SpriteBatch sb) {

		}

		private void DrawSector(int id, SpriteBatch sb) {

		}
	}
}
