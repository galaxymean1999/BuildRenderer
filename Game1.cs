using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BuildRenderer {
	public class BuildRenderer : Game {
		private GraphicsDeviceManager g;
		private SpriteBatch sb;

		public BuildRenderer() {
			g = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}
		
		private GameState gs;

		protected override void Initialize() {
			gs = new GameState();

			base.Initialize();
		}

		protected override void LoadContent() {
			sb = new SpriteBatch(GraphicsDevice);
		}

		protected override void Update(GameTime gameTime) {
			if (Keyboard.GetState().IsKeyDown(Keys.Escape)) {
				Exit();
			}

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.Black);
			sb.Begin();

			sb.End();
			base.Draw(gameTime);
		}
	}
}
