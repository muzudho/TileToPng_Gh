using System.Drawing;

namespace Grayscale.TileToPng
{
    public interface Grid
    {

        float Ox { get; set; }
        float Oy { get; set; }
        float CellW { get; set; }
        float CellH { get; set; }
        PointF Next { get; set; }

        int Cols { get; }
        int Rows { get; }

    }
}
