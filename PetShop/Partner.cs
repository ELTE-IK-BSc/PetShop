using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beadando
{
    public class Partner
    {
        private List<Invoice> invoices = new List<Invoice>();
        public string Name;
        public Partner(string n)
        {
            Name = n;
        }

        public void Transaction(Invoice invoice)
        {
            invoices.Add(invoice);
        }
        public int GetNumberofInvoices()
        {
            return invoices.Count();
        }
    }
}
