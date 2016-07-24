using Grayscale.TileToPng.n___100_cmdline_;

namespace Grayscale.TileToPng.n100____cmdline_
{
    public class MarginImpl : Margin
    {
        public MarginImpl()
        {

        }

        public MarginImpl(
            int north,
            int east,
            int south,
            int west
            )
        {
            this.m_north_ = north;
            this.m_east_ = east;
            this.m_south_ = south;
            this.m_west_ = west;
        }

        private int m_north_;
        public int North
        {
            get { return this.m_north_; }
            set { this.m_north_ = value; }
        }

        private int m_east_;
        public int East
        {
            get { return this.m_east_; }
            set { this.m_east_ = value; }
        }

        private int m_south_;
        public int South
        {
            get { return this.m_south_; }
            set { this.m_south_ = value; }
        }

        private int m_west_;
        public int West
        {
            get { return this.m_west_; }
            set { this.m_west_ = value; }
        }

        public override string ToString()
        {
            return this.North + ", " + this.East + ", " + this.South + ", " + this.West;
        }

    }
}
