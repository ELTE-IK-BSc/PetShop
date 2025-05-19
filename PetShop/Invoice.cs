using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beadando
{
    public class Invoice
    {
        public Pet pet;
        public DateTime date;
        public double price;
        public Partner partner;
        public PetShop shop;
        public bool isBuy;
        public bool isSell;
        public Invoice(Pet pe, DateTime d, double pr, Partner pa, PetShop s, bool isb, bool iss)
        {
            pet = pe;
            date = d;
            price = pr;
            partner = pa;
            shop = s;
            isBuy = isb;
            isSell = iss;
        }
        
    }
}
