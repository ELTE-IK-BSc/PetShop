using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beadando
{
    abstract public class Age
    {
        
        abstract public double Multiplicator(Hamster hamster);

        abstract public double Multiplicator(Finch finch);

        abstract public double Multiplicator(Tarantula tarantula);
    }
    public class Young : Age
    {
        private static Young instance = null;
        public static Young Instance()
        {
            if (instance == null)
            {
                instance = new Young();
            }
            return instance;
        }
        public Young() {}
        public override double Multiplicator(Hamster hamster)
        {
            return 2.0;
        }
        public override double Multiplicator(Finch finch)
        {
            return 1.0;
        }
        public override double Multiplicator(Tarantula tarantula)
        {
            return 3.0;
        }
    }
    public class Adoult : Age
    {
        private static Adoult instance = null;
        public static Adoult Instance()
        {
            if (instance == null)
            {
                instance = new Adoult();
            }
            return instance;
        }
        public Adoult() {}
        public override double Multiplicator(Hamster hamster)
        {
            return 1.0;
        }
        public override double Multiplicator(Finch finch)
        {
            return 3.0;
        }
        public override double Multiplicator(Tarantula tarantula)
        {
            return 2.0;
        }
    }
}
