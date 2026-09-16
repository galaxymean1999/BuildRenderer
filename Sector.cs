using System.Collections.Generic;

namespace BuildRenderer {
	public  class Sector {
		public int floorHeight;
		public int ceilingHeight;
		public int id;
		public List<Wall> walls = new List<Wall>();
	}
}
