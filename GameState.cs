using Microsoft.Xna.Framework.Graphics;

namespace BuildRenderer {
	public class GameState {
		public GameState(GraphicsDevice g) {
			Renderer.Init(player, level, g);

			level.LoadLevel(currentLevel);
		}

		public Player player = new Player();

		public Level level = new Level();

		public int currentLevel = 0;

		public void Update() {
			player.Update();
		}
	}
}
