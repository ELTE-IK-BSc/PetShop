using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Beadando
{
    abstract public class Pet
    {
        public string id;
        public string color;
        public int value;
        protected bool young;
        private List<Invoice> invoices = new List<Invoice>();
        public void Transaction(Invoice invoice)
        {
            invoices.Add(invoice);
        }
        public int GetNumberofInvoices()
        {
            return invoices.Count();
        }
        public Pet(string i, string c, int v, bool y)
        {
            id = i;
            color = c;
            value = v;
            young = y;
        }

        public void Grow()
        {
            young = false;
        }

        abstract public double Price();


        public double AvgPrice()
        {
            double avgPrice = 0;
            int count = 0;
            foreach (Invoice invoice in invoices)
            {
                count += 1;
                avgPrice += invoice.price;

            }
            avgPrice = avgPrice / count;
            return avgPrice;
        }

        public virtual bool isHamster()
        {
            return false;
        }
        public virtual bool isFinch()
        {
            return false;
        }
        public virtual bool isTarantula()
        {
            return false;
        }
    }
    public class Hamster : Pet
    {
        public Hamster(string i, string c, int v, bool y) : base(i, c, v, y) { }

        public override bool isHamster()
        {
            return true;
        }


        public override double Price()
        {
            if (young)
            {
                Young mp = new Young();
                return value * mp.Multiplicator(this);
            }
            else
            {
                Adoult mp = new Adoult();
                return value * mp.Multiplicator(this);
            }
        }
    }
    public class Finch : Pet
    {
        public Finch(string i, string c, int v, bool y) : base(i, c, v, y) { }
        public override bool isFinch()
        {
            return true;
        }

        public override double Price()
        {
            if (young)
            {
                Young mp = new Young();
                return value * mp.Multiplicator(this);
            }
            else
            {
                Adoult mp = new Adoult();
                return value * mp.Multiplicator(this);
            }
        }
    }
    public class Tarantula : Pet
    {
        public Tarantula(string i, string c, int v, bool y) : base(i, c, v, y) { }
        public override bool isTarantula()
        {
            return true;
        }

        public override double Price()
        {
            if (young)
            {
                Young mp = new Young();
                return value * mp.Multiplicator(this);
            }
            else
            {
                Adoult mp = new Adoult();
                return value * mp.Multiplicator(this);
            }
        }
    }
}