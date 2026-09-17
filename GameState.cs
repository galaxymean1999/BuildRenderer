using Microsoft.Xna.Framework.Graphics;

namespace BuildRenderer {
	public class GameState {
		public GameState(GraphicsDevice g) {
			renderer = new Renderer(player, level, g);

			level.LoadLevel(currentLevel);
		}

		public Renderer renderer;

		public Player player = new Player();

		public Level level = new Level();

		public int currentLevel = 0;

		public void Update() {
			player.Update();
		}
	}
}
