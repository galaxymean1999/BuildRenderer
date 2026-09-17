using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BuildRenderer {
	public class Renderer {
		public Renderer(Player player, Level level) {
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
		}

		public int screenWidth = 800;
		public int screenHeight = 600;

		private Player player;

		private Level level;

		private float[] upperClip;
		private float[] lowerClip;
		private float[] zBuffer;

		public float focalLength;

		const float farPlane = 128.0f;
		const float nearPlane = 0.01f;

		public void Render(SpriteBatch sb) {

		}

		private float FindRelativeAngle(Vector2 wallPointPos) {
			float dx = wallPointPos.X - player.position.X;
			float dy = wallPointPos.Y - player.position.Y;

			return NormaliseAngle(MathF.Atan2(dy, dx) - player.heading);
		}

		private Vector2 FindRelativePos(Vector2 wallPointPos) {
			Vector2 relPos = new Vector2();

			float relativeAngle = FindRelativeAngle(wallPointPos);

			float angleToRelCords = MathF.PI - relativeAngle;

			float distance = MathF.Sqrt(wallPointPos.X - player.position.X + wallPointPos.Y - player.position.Y);

			relPos.Y = MathF.Sin(angleToRelCords) * distance;

			relPos.X = relPos.Y / MathF.Tan(angleToRelCords);

			return relPos;
		}

		public static float NormaliseAngle(float angle) {
			if (angle > MathF.PI) {
				return angle - MathF.PI;
			}
			else if (angle < -MathF.PI) {
				return angle + MathF.PI;
			}
			else {
				return angle;
			}
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
					int screenX1 = (int)(screenWidth / 2 + MathF.Tan(relAngle1) * focalLength);
					int screenX2 = (int)(screenWidth / 2 + MathF.Tan(relAngle1) * focalLength);
				}
				else {
					
				}
			}
		}
	}
}
