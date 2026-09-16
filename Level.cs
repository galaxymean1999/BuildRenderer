using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;

namespace BuildRenderer {
	public class Level {
		List<Sector> sectors = new List<Sector>();

		public void LoadLevel(int id) {
			sectors.Clear();

			LoadSectors();
		}

		private void LoadSectors() {
			// SsectorID;numOfWalls;floorHeight;ceilingHeight
			// pointAX;pointAY;pointBX;pointBY;portal

			if (File.Exists("Level/level")) {
				string[] level = File.ReadAllLines("Level/level");
				int sectorID = -1;
				int numOfWalls = 0;
				int sectorStartLine = -1;

				for (int i = 0; i < level.Length; i++) {
					string[] line = level[i].Split(';');

					if (level[i][0] == 'S') {
						sectorStartLine = i;

						sectorID = int.Parse(line[0].Remove(0, 1));
						numOfWalls = int.Parse(line[1]);
						int floorHeight = int.Parse(line[2]);
						int ceilingHeight = int.Parse(line[3]);

						sectors.Add(new Sector());

						sectors[sectorID].id = sectorID;
						sectors[sectorID].floorHeight = floorHeight;
						sectors[sectorID].ceilingHeight = ceilingHeight;
					}
					else if (i > sectorStartLine && i <= sectorStartLine + numOfWalls) {
						Vector2 pos1 = new Vector2(float.Parse(line[0]), float.Parse(line[1]));
						Vector2 pos2 = new Vector2(float.Parse(line[2]), float.Parse(line[3]));
						int portal = int.Parse(line[4]);

						sectors.Last().walls.Add(new Wall(pos1, pos2, portal));
					}
				}
			}
		}
	}
}
