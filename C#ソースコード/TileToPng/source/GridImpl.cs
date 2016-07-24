using System.Drawing;

namespace Grayscale.TileToPng
{
    public class GridImpl : Grid
    {
        public GridImpl(
            float ox,
            float oy,
            float cellW,
            float cellH
            )
        {
            this.m_ox_ = ox;
            this.m_oy_ = oy;
            this.m_cellW_ = cellW;
            this.m_cellH_ = cellH;
        }

        private float m_ox_;
        public float Ox
        {
            get { return this.m_ox_; }
            set { this.m_ox_ = value; }
        }

        private float m_oy_;
        public float Oy
        {
            get { return this.m_oy_; }
            set { this.m_oy_ = value; }
        }

        private float m_cellW_;
        public float CellW
        {
            get { return this.m_cellW_; }
            set { this.m_cellW_ = value; }
        }

        private float m_cellH_;
        public float CellH
        {
            get { return this.m_cellH_; }
            set { this.m_cellH_ = value; }
        }

        private PointF m_next_;
        public PointF Next
        {
            get { return this.m_next_; }
            set { this.m_next_ = value; }
        }

        public int Cols
        {
            get { return (int)((this.Next.X - this.Ox) / this.CellW); }
        }

        public int Rows
        {
            get { return (int)((this.Next.Y - this.Oy) / this.CellH); }
        }
    }
}
