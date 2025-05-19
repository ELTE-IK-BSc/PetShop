namespace Beadando
{
    public class Program
    {
        static void Main(string[] args)
        {
            PetShop BestShop = new PetShop();
            BestShop.Populating("input.txt");


            Console.WriteLine("a. Van-e egy kereskedésben adott színű pinty?");
            Console.Write("Add meg a színt: ");
            string color = Console.ReadLine();
            if (BestShop.FinchWithColore(color))
            {
                Console.WriteLine("Van");
            }
            else { Console.WriteLine("Nincs"); }
            Console.WriteLine("b. Hány hörcsöge van egy kereskedésnek?");
            Console.WriteLine(""+BestShop.NumberofHamsters());
            Console.WriteLine("c. Melyik a legnagyobb eszmei értékű tarantullája egy kereskedésnek?");
            Pet? pet = null;
            if (BestShop.MaxValueTarantula(ref pet))
            {
                Console.WriteLine(pet.value);
            }
            else { Console.WriteLine("nincs tar"); }
            Console.WriteLine("d. Hány szerződést kötött egy adott kereskedés egy adott partnerrel?");
            Console.Write("Add meg a partnert: ");
            string  part = Console.ReadLine();
            Partner  partner= new Partner(part);
            Console.WriteLine(BestShop.NumberofInvoicesWith(partner));
            Console.WriteLine("e. Mekkora egy kereskedésnek a nyeresége?");
            Console.WriteLine("" + BestShop.Profit());

            

        }
    }
}