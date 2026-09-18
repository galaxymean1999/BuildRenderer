using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Windows.Forms;

namespace BuildRenderer {
	public static class Renderer {
		public static void Init(Player player, Level level, GraphicsDevice g) {
			Renderer.player = player;
			Renderer.level = level;

			upperClip = new float[screenWidth];
			lowerClip = new float[screenWidth];
			zBuffer = new float[screenWidth];

			InitClippings();

			focalLength = screenWidth / (2 * MathF.Tan(player.FOV / 2));

			blank = new Texture2D(g, 1, 1);
			blank.SetData(new Color[] { Color.White });
		}

		public static int screenWidth = 800;
		public static int screenHeight = 600;

		private static Player player;

		private static Level level;

		private static Texture2D blank;

		private static float[] upperClip;
		private static float[] lowerClip;
		private static float[] zBuffer;

		public static float focalLength;

		const float farPlane = 128.0f;
		const float nearPlane = 0.01f;

		public static void Render(SpriteBatch sb) {
			InitClippings();

			DrawSector(player.currentSectorID, sb);
		}

		private static void InitClippings() {
			for (int i = 0; i < screenWidth; i++) {
				upperClip[i] = screenHeight;
				lowerClip[i] = 0;
				zBuffer[i] = farPlane;
			}
		}

		private static float FindRelativeAngle(Vector2 wallPointPos) {
			float dx = wallPointPos.X - player.position.X;
			float dy = wallPointPos.Y - player.position.Y;

			return NormaliseAngle(MathF.Atan2(dy, dx) - player.heading);
		}

		private static Vector2 FindRelativePos(Vector2 wallPointPos) {
			float dx = wallPointPos.X - player.position.X;
			float dy = wallPointPos.Y - player.position.Y;

			float sin = MathF.Sin(player.heading);
			float cos = MathF.Cos(player.heading);

			// matrix rotation to rotate the world position to the player heading
			return new Vector2(
				dy * cos - dx * sin,
				dy * sin + dx * cos
				);
		}

		public static float NormaliseAngle(float angle) {
			return MathF.Atan2(MathF.Sin(angle), MathF.Cos(angle));
		}

		private static void DrawSector(int id, SpriteBatch sb) {
			foreach (Wall w in level.sectors[id].walls) {

				Vector2 relPos1 = FindRelativePos(w.pos1);
				Vector2 relPos2 = FindRelativePos(w.pos2);

				float relAngle1 = FindRelativeAngle(w.pos1);
				float relAngle2 = FindRelativeAngle(w.pos2);

				if (relPos1.Y < nearPlane && relPos2.Y < nearPlane) {
					continue;
				}

				// CLIPPING
				if (relPos1.Y < nearPlane) {
					float t = (nearPlane - relPos1.Y) / (relPos2.Y - relPos1.Y);

					relPos1.X = relPos1.X + t * (relPos2.X - relPos1.X);
					relPos1.Y = nearPlane;
				}
				else if (relPos2.Y < nearPlane) {
					float t = (nearPlane - relPos2.Y) / (relPos1.Y - relPos2.Y);

					relPos2.X = relPos2.X + t * (relPos2.X - relPos2.X);
					relPos2.Y = nearPlane;
				}

				// If the Wall is SOLID
				if (w.portal == -1) {
					int screenX1 = (int)(screenWidth / 2 + (relPos1.X / relPos1.Y) * focalLength);
					int screenX2 = (int)(screenWidth / 2 + (relPos2.X / relPos2.Y) * focalLength);

					int height1 = (int)(screenHeight / relPos1.Y);
					int height2 = (int)(screenHeight / relPos2.Y);

					// Swap if not ordered
					if (screenX1 > screenX2) {
						(screenX1, screenX2) = (screenX2, screenX1);

						(relPos1, relPos2) = (relPos2, relPos1);

						(height1, height2) = (height2, height1);
					}

					// Calculating for height
					float dHeight = height2 - height1;
					float heightStep = dHeight / (float)((screenX2 - screenX1) == 0 ? 1 : screenX2 - screenX1);
					float height = height1;

					// Draw the wall
					for (int x = screenX1; x <= screenX2; x++) {
						if (x >= 0 && x < screenWidth) {
							lowerClip[x] = screenHeight / 2 - (int)height / 2 + height;
							upperClip[x] = screenHeight / 2 - (int)height / 2;
							sb.Draw(blank, new Rectangle(x, screenHeight / 2 - (int)height / 2, 1, (int)height), Color.White);
						}

						height += heightStep;
					}

					// temporary draw wall bounds
					sb.Draw(blank, new Rectangle(screenX1, screenHeight / 2 - height1 / 2, 1, height1), Color.Red);
					sb.Draw(blank, new Rectangle(screenX2, screenHeight / 2 - height2 / 2, 1, height2), Color.Red);
				}
				else {
					//DrawSector(w.portal, sb);
				}
			}
		}
	}
}
