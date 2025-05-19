using Beadando;
using System;

namespace Beadando
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Instantiating()
        {
            PetShop shop = new PetShop();
            Assert.IsNotNull(shop);

            shop.Populating("input.txt");
            Assert.IsTrue(shop.pets.Count != 0);
            Assert.IsTrue(shop.partners.Count != 0);
            Assert.IsTrue(shop.invoices.Count != 0);
        }

        [TestMethod]
        public void Trade()
        {
            PetShop shop = new PetShop();
            Partner partner = new Partner("Partner");
            Hamster hamster = new Hamster("H1", "brown", 50, false);            
            Assert.IsTrue(shop.pets.Count== 0);
            Assert.IsTrue(shop.partners.Count == 0);

            //új partner
            shop.NewPartner(partner);
            Assert.ThrowsException<PartnerAlreadyExistException>(() => shop.NewPartner(partner));

            //vásárlás
            shop.Buy(partner, hamster);
            Assert.IsTrue(shop.pets.Count == 1);
            Assert.IsTrue(shop.partners.Count == 1);

            //Kivétel a olyan állat vételére ami már van a kereskedésnek
            Assert.ThrowsException<PetOwnedByShopException>(() => shop.Buy(partner, hamster));

            //eladás 
            shop.Sell(partner, hamster);
            Assert.IsTrue(shop.pets.Count == 0);
            Assert.IsTrue(shop.partners.Count == 1);
            //Kivétel a olyan állat eladására ami nincs a kereskedésnek
            Assert.ThrowsException<PetNotOwnedByShopException>(() => shop.Sell(partner, hamster));

            //Profit
            Assert.AreEqual(0,shop.Profit());
            
            Assert.AreEqual(2,hamster.GetNumberofInvoices());
        }

        [TestMethod]
        public void FinchWithColor()
        {
            PetShop shop = new PetShop();
            Partner partner = new Partner("Partner");
            Finch finch1 = new Finch("F1", "red", 50, false);
            Finch finch2 = new Finch("F2", "yellow", 50, false);
            Hamster hamster = new Hamster("H1", "yellow", 50, false);
            shop.Buy(partner, hamster);


            //Nincs pinty a kereskedésben
            Assert.IsFalse(shop.FinchWithColore("yellow"));

            //Nincs megfelelõ színû pinty
            shop.Buy(partner, finch1);
            Assert.IsFalse(shop.FinchWithColore("yellow"));

            //Volt de már el lett adva
            shop.Buy(partner, finch2);
            shop.Sell(partner, finch2);
            Assert.IsFalse(shop.FinchWithColore("yellow"));

            //Van megfelelõ színû pinty
            shop.Buy(partner, finch2);
            Assert.IsTrue(shop.FinchWithColore("yellow"));
        }

        [TestMethod]
        public void NumberofHamsters()
        {
            PetShop shop = new PetShop();
            Partner partner = new Partner("Partner");
            Hamster hamster1 = new Hamster("H1", "black", 50, false);
            Hamster hamster2 = new Hamster("H2", "brown", 50, false);
            Hamster hamster3 = new Hamster("H3", "light-brown", 50, false);

            Finch finch = new Finch("F1", "red", 50, false);
            shop.Buy(partner, finch);
            Tarantula tarantula = new Tarantula("T1", "black", 50, false);
            shop.Buy(partner, tarantula);


            //Nem vásárolt a Kereskedés hörcsögöt
            Assert.AreEqual(0,shop.NumberofHamsters());

            //Nincs a készletben pillanatnyilag hörcsög
            shop.Buy(partner, hamster1);
            shop.Sell(partner, hamster1);
            Assert.AreEqual(0, shop.NumberofHamsters());

            //Van a készletben pillanatnyilag pontosan 1 hörcsög
            shop.Buy(partner, hamster2);
            Assert.AreEqual(1, shop.NumberofHamsters());

            //Van a készletben pillanatnyilag több mint hörcsög
            shop.Buy(partner, hamster1);
            shop.Buy(partner, hamster3);
            Assert.AreEqual(3, shop.NumberofHamsters());
        }               

        [TestMethod]
        public void MaxValueTarantula()
        {
            PetShop shop = new PetShop();
            Partner partner = new Partner("Partner");
            Hamster hamster1 = new Hamster("H1", "black", 50, false);
            Finch finch1 = new Finch("F1", "red", 50, false);
            Tarantula tarantula1 = new Tarantula("T1", "black", 30, false);
            Tarantula tarantula2 = new Tarantula("T2", "black", 50, false);

            shop.Buy(partner, hamster1);
            shop.Buy(partner, finch1);
            Pet? pet = null;


            //Nincs tarantulla a kereskedésben
            Assert.IsFalse(shop.MaxValueTarantula(ref pet));

            //Van 1 tarantulla a kereskedésben
            shop.Buy(partner, tarantula1);
            Assert.IsTrue(shop.MaxValueTarantula(ref pet));
            Assert.AreEqual("T1", pet.id);
            Assert.AreEqual(30, pet.value);

            //Van több tarantulla a kereskedésben
            shop.Buy(partner, tarantula2);
            Assert.IsTrue(shop.MaxValueTarantula(ref pet));
            Assert.AreEqual("T2", pet.id);
            Assert.AreEqual(50, pet.value);

            //Van több tarantulla a kereskedésben és van olyan akiknek azonos az eszmei értéke
            tarantula1.value = 50;
            Assert.IsTrue(shop.MaxValueTarantula(ref pet));
            Assert.AreEqual("T1", pet.id);
            Assert.AreEqual(50,pet.value);
        }

        [TestMethod]
        public void NumberofInvoicesWith() 
        {
            PetShop shop = new PetShop();
            Partner partner = new Partner("Partner");
            Hamster hamster1 = new Hamster("H1", "black", 50, false);

            //Nem létezõ partner
            Assert.AreEqual(0, shop.NumberofInvoicesWith(partner));
            
            //Létezõ partner 0 számla
            shop.NewPartner(partner);
            Assert.AreEqual(0, shop.NumberofInvoicesWith(partner));

            //Létezõ partner 1 számla
            shop.Buy(partner,hamster1);
            Assert.AreEqual(1, shop.NumberofInvoicesWith(partner));

            //Létezõ partner több számla
            shop.Sell(partner, hamster1);
            shop.Buy(partner, hamster1);
            Assert.AreEqual(3, shop.NumberofInvoicesWith(partner));
        }

        [TestMethod]
        public void AvgPrice()
        {
            PetShop shop = new PetShop();
            Partner partner = new Partner("Partner");
            Hamster hamster1 = new Hamster("H1", "black", 50, true);
            
            shop.Buy(partner, hamster1);
            Assert.AreEqual(100, hamster1.AvgPrice());
            shop.Sell(partner, hamster1);
            Assert.AreEqual(100, hamster1.AvgPrice());
            shop.Buy(partner, hamster1);
            Assert.AreEqual(100, hamster1.AvgPrice());

            //Felnõ a hörcsög
            hamster1.Grow();

            shop.Sell(partner, hamster1);      
            Assert.AreEqual(87.5,hamster1.AvgPrice());

            shop.Buy(partner,hamster1);         
            Assert.AreEqual(80, hamster1.AvgPrice());

            shop.Sell(partner, hamster1);     
            Assert.AreEqual(75, hamster1.AvgPrice());

        }
    }
}