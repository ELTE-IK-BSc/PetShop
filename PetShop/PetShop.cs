using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Beadando
{
    public class PetNotOwnedByShopException : Exception { }
    public class PetOwnedByShopException : Exception { }
    public class PartnerAlreadyExistException : Exception { }
    public class PetShop
    {
        
        public List<Pet> pets = new List<Pet>();
        public List<Invoice> invoices = new List<Invoice>();
        public List<Partner> partners = new List<Partner>();

        public PetShop() { }
        public void Populating(string fname)
        {
            StreamReader sr = new StreamReader(fname);
            int.TryParse(sr.ReadLine(), out int n);
            for (int i = 0; i < n; i++)
            {
                int.TryParse(sr.ReadLine(), out int b);
                for (int j = 0; j < b; j++)
                {
                    string[] row = sr.ReadLine().Split('\t');
                    Partner part = null;
                    foreach (Partner partner in partners)
                    {
                        if (partner.Name == row[0])
                        {
                            part = partner;
                            break;
                        }
                    }
                    if (part == null)
                    {
                         part = new Partner(row[0]);
                    }
                    
                    Pet? pet = null;

                    switch (row[1])
                    {
                        case "Hamster":
                            pet = new Hamster(row[2], row[3], int.Parse(row[4]), bool.Parse(row[5]));
                            break;

                        case "Finch":
                            pet = new Finch(row[2], row[3], int.Parse(row[4]), bool.Parse(row[5]));
                            break;

                        case "Tarantula":
                            pet = new Tarantula(row[2], row[3], int.Parse(row[4]), bool.Parse(row[5]));
                            break;
                        default:
                            break;
                    }   
                    Buy(part, pet);
                }

                int.TryParse(sr.ReadLine(), out int s);
                for (int j = 0; j < s; j++)
                {
                    string[] row = sr.ReadLine().Split('\t');
                    Partner part = null;
                    foreach (Partner partner in partners)
                    {
                        if (partner.Name == row[0])
                        {
                            part = partner;
                            break;
                        }
                    }
                    if (part == null)
                    {
                        part = new Partner(row[0]);
                    }
                    Pet pet = null;
                    foreach (Pet p in pets)
                    {
                        if (p.id == row[2])
                        {
                            pet = p;
                            break;
                        }
                    }
                    Sell(part, pet);
                }
            }
            sr.Close();
        }

        public void NewPartner(Partner partner)
        {            
            if (!partners.Contains(partner))
            {
                partners.Add(partner);
            }
            else
            {
                throw new PartnerAlreadyExistException();
            }
        }

        public void Buy(Partner partner, Pet pet)
        {
            if (!pets.Contains(pet))
            {
                if (!partners.Contains(partner))
                {
                    NewPartner(partner);
                }
                Invoice invoice = new Invoice(pet, DateTime.Now, pet.Price(), partner, this, true, false);
                partner.Transaction(invoice);
                pet.Transaction(invoice);
                invoices.Add(invoice);
                pets.Add(pet);
            }
            else
            { 
                throw new PetOwnedByShopException();
            }

        }
        public void Sell(Partner partner, Pet pet) // ide bevezethető egy paraméter amivel kialakítható a megfelelő árrés.
        {
            if (pets.Contains(pet))
            {
                if (!partners.Contains(partner))
                {
                    NewPartner(partner);
                }
                Invoice invoice = new Invoice(pet, DateTime.Now, pet.Price(), partner, this, false, true);// és itt szorozhatjuk a pet.Price()-t azzal az értékkel, amelyet a paraméterként megadtunk, ez megadja milyen mértékű nyereséget szeretnénk elérni az adott állat eladásán.
                partner.Transaction(invoice);
                pet.Transaction(invoice);
                invoices.Add(invoice);
                pets.Remove(pet);
            }
            else
            {
                throw new PetNotOwnedByShopException();
            }
            
        }
        public bool FinchWithColore(string color) 
        {
            foreach (Pet pet in pets)
            {
                if (pet.isFinch() && (pet.color == color))
                {
                    return true;
                }
            }
            return false;
        }

        public int NumberofHamsters()
        {
            int count = 0;

            foreach (Pet pet in pets) 
            {
                if (pet.isHamster())
                {
                    count++;
                }
            }
            return count;
        }
        public bool MaxValueTarantula(ref Pet? MaxTarantula) {
            bool l = false;
            foreach (Pet pet in pets)
            {
                if (!l && pet.isTarantula())
                {
                    l = true;
                    MaxTarantula = pet;
                }
                else
                {
                    if (pet.isTarantula() && pet.value > MaxTarantula.value)
                    {
                        MaxTarantula = pet;
                    }
                }
            }
                return l;
        }
        public double Profit() 
        {
            double profit = 0;
            foreach (Invoice invoice in invoices)
            {
                if (invoice.isBuy)
                {
                    profit -= invoice.price;
                }
                else if (invoice.isSell)
                {
                    profit += invoice.price;
                }
            }
            return profit;
        }

        public int NumberofInvoicesWith(Partner partner) {

            foreach (Partner p in partners)
            {
                if (p.Name == partner.Name)
                {
                    return p.GetNumberofInvoices();
                }
            }
            return 0;
        }

    }
}
