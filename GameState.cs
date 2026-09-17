namespace BuildRenderer {
	public class GameState {
		public GameState() {
			renderer = new Renderer(player, level);

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
