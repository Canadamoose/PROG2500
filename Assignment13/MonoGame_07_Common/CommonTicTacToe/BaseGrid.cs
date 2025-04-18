using Microsoft.Xna.Framework;

namespace CommonTicTacToe
{
    public class BaseGrid
    {
        public GridVal[,,] Cells;
        public Vector3 Cursor;
        public readonly Vector3 Dimensions;

        public BaseGrid(int x, int y, int z = 1)
        {
            Dimensions = new(x, y, z);
            Cells = new GridVal[x, y, z];
            Reset(GridVal.Dot);
        }

        public void CycleCell(GridVal newValue)
        {
            var x = (int)Cursor.X;
            var y = (int)Cursor.Y;
            var z = (int)Cursor.Z;

            if (x >= 0 && x < Dimensions.X &&
                y >= 0 && y < Dimensions.Y &&
                z >= 0 && z < Dimensions.Z)
            {
                Cells[x, y, z] = newValue;
            }
        }

        public void Reset(GridVal value)
        {
            for (int x = 0; x < Dimensions.X; x++)
                for (int y = 0; y < Dimensions.Y; y++)
                    for (int z = 0; z < Dimensions.Z; z++)
                        Cells[x, y, z] = value;
        }
    }
}