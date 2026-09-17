using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BuildRenderer {
	public class Renderer {
		public Renderer(Player player, Level level, GraphicsDevice g) {
			this.player = player;
			this.level = level;

			upperClip = new float[screenWidth];
			lowerClip = new float[screenWidth];
			zBuffer = new float[screenWidth];

			for (int i = 0; i < screenWidth; i++) {
				upperClip[i] = screenHeight;
				lowerClip[i] = 0;
				zBuffer[i] = farPlane;
			}

			focalLength = screenWidth / (2 * MathF.Tan(player.FOV / 2));

			blank = new Texture2D(g, 1, 1);
			blank.SetData(new Color[] { Color.White });
		}

		public int screenWidth = 800;
		public int screenHeight = 600;

		private Player player;

		private Level level;

		private Texture2D blank;

		private float[] upperClip;
		private float[] lowerClip;
		private float[] zBuffer;

		public float focalLength;

		const float farPlane = 128.0f;
		const float nearPlane = 0.01f;

		public void Render(SpriteBatch sb) {
			DrawSector(player.currentSectorID, sb);
		}

		private float FindRelativeAngle(Vector2 wallPointPos) {
			float dx = wallPointPos.X - player.position.X;
			float dy = wallPointPos.Y - player.position.Y;

			return NormaliseAngle(MathF.Atan2(dy, dx) - player.heading);
		}

		private Vector2 FindRelativePos(Vector2 wallPointPos) {
			/*Vector2 relPos = new Vector2();

			float relativeAngle = FindRelativeAngle(wallPointPos);

			float angleToRelCords = MathF.PI - relativeAngle;

			float distance = MathF.Sqrt(MathF.Pow(wallPointPos.X - player.position.X, 2) + MathF.Pow(wallPointPos.Y - player.position.Y, 2));

			relPos.Y = MathF.Sin(angleToRelCords) * distance;

			relPos.X = relPos.Y / MathF.Tan(angleToRelCords);

			return relPos;*/

			float dx = wallPointPos.X - player.position.X;
			float dy = wallPointPos.Y - player.position.Y;

			float sin = MathF.Sin(player.heading);
			float cos = MathF.Cos(player.heading);

			return new Vector2(
				dy * cos - dx * sin,
				dy * sin + dx * cos
				);
		}

		private float GetDistance(Vector2 pos1, Vector2 pos2) {
			return MathF.Sqrt(MathF.Pow(pos1.X - pos2.X, 2) + MathF.Pow(pos1.Y - pos2.Y, 2));
		}

		public static float NormaliseAngle(float angle) {
			return MathF.Atan2(MathF.Sin(angle), MathF.Cos(angle));
		}

		private void DrawSector(int id, SpriteBatch sb) {
			foreach (Wall w in level.sectors[id].walls) {

				Vector2 relPos1 = FindRelativePos(w.pos1);
				Vector2 relPos2 = FindRelativePos(w.pos2);

				float relAngle1 = FindRelativeAngle(w.pos1);
				float relAngle2 = FindRelativeAngle(w.pos2);

				if (relPos1.Y < nearPlane && relPos2.Y < nearPlane) {
					continue;
				}

				if (w.portal == -1) {
					int screenX1 = (int)(screenWidth / 2 + (relPos1.X / relPos1.Y) * focalLength);
					int screenX2 = (int)(screenWidth / 2 + (relPos2.X / relPos2.Y) * focalLength);

					int height1 = (int)(screenHeight / relPos1.Y);
					int height2 = (int)(screenHeight / relPos2.Y);

					if (screenX1 > screenX2) {
						(screenX1, screenX2) = (screenX2, screenX1);

						(relPos1, relPos2) = (relPos2, relPos1);

						(height1, height2) = (height2, height1);
					}

					float dHeight = height2 - height1;
					float heightStep = dHeight / (float)((screenX2 - screenX1) == 0 ? 1 : screenX2 - screenX1);
					float height = height1;

					for (int x = screenX1; x <= screenX2; x++) {
						if (x >= 0 && x < screenWidth) {
							sb.Draw(blank, new Rectangle(x, screenHeight / 2 - (int)height / 2, 1, (int)height), Color.White);
						}

						height += heightStep;
					}

					/*sb.Draw(blank, new Rectangle(screenX1, screenHeight / 2 - height1 / 2, 1, height1), Color.White);
					sb.Draw(blank, new Rectangle(screenX2, screenHeight / 2 - height2 / 2, 1, height2), Color.White);*/
				}
				else {
					//DrawSector(w.portal, sb);
				}
			}
		}
	}
}
