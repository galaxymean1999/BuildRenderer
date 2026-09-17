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
			gs = new GameState(GraphicsDevice);

			g.PreferredBackBufferWidth = gs.renderer.screenWidth;
			g.PreferredBackBufferHeight = gs.renderer.screenHeight;

			base.Initialize();
		}

		protected override void LoadContent() {
			sb = new SpriteBatch(GraphicsDevice);
		}

		protected override void Update(GameTime gameTime) {
			if (Keyboard.GetState().IsKeyDown(Keys.Escape)) {
				Exit();
			}

			gs.Update();

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime) {
			GraphicsDevice.Clear(Color.Black);
			sb.Begin();

			gs.renderer.Render(sb);

			sb.End();
			base.Draw(gameTime);
		}
	}
}
