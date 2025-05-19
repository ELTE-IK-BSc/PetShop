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

            //�j partner
            shop.NewPartner(partner);
            Assert.ThrowsException<PartnerAlreadyExistException>(() => shop.NewPartner(partner));

            //v�s�rl�s
            shop.Buy(partner, hamster);
            Assert.IsTrue(shop.pets.Count == 1);
            Assert.IsTrue(shop.partners.Count == 1);

            //Kiv�tel a olyan �llat v�tel�re ami m�r van a keresked�snek
            Assert.ThrowsException<PetOwnedByShopException>(() => shop.Buy(partner, hamster));

            //elad�s 
            shop.Sell(partner, hamster);
            Assert.IsTrue(shop.pets.Count == 0);
            Assert.IsTrue(shop.partners.Count == 1);
            //Kiv�tel a olyan �llat elad�s�ra ami nincs a keresked�snek
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


            //Nincs pinty a keresked�sben
            Assert.IsFalse(shop.FinchWithColore("yellow"));

            //Nincs megfelel� sz�n� pinty
            shop.Buy(partner, finch1);
            Assert.IsFalse(shop.FinchWithColore("yellow"));

            //Volt de m�r el lett adva
            shop.Buy(partner, finch2);
            shop.Sell(partner, finch2);
            Assert.IsFalse(shop.FinchWithColore("yellow"));

            //Van megfelel� sz�n� pinty
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


            //Nem v�s�rolt a Keresked�s h�rcs�g�t
            Assert.AreEqual(0,shop.NumberofHamsters());

            //Nincs a k�szletben pillanatnyilag h�rcs�g
            shop.Buy(partner, hamster1);
            shop.Sell(partner, hamster1);
            Assert.AreEqual(0, shop.NumberofHamsters());

            //Van a k�szletben pillanatnyilag pontosan 1 h�rcs�g
            shop.Buy(partner, hamster2);
            Assert.AreEqual(1, shop.NumberofHamsters());

            //Van a k�szletben pillanatnyilag t�bb mint h�rcs�g
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


            //Nincs tarantulla a keresked�sben
            Assert.IsFalse(shop.MaxValueTarantula(ref pet));

            //Van 1 tarantulla a keresked�sben
            shop.Buy(partner, tarantula1);
            Assert.IsTrue(shop.MaxValueTarantula(ref pet));
            Assert.AreEqual("T1", pet.id);
            Assert.AreEqual(30, pet.value);

            //Van t�bb tarantulla a keresked�sben
            shop.Buy(partner, tarantula2);
            Assert.IsTrue(shop.MaxValueTarantula(ref pet));
            Assert.AreEqual("T2", pet.id);
            Assert.AreEqual(50, pet.value);

            //Van t�bb tarantulla a keresked�sben �s van olyan akiknek azonos az eszmei �rt�ke
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

            //Nem l�tez� partner
            Assert.AreEqual(0, shop.NumberofInvoicesWith(partner));
            
            //L�tez� partner 0 sz�mla
            shop.NewPartner(partner);
            Assert.AreEqual(0, shop.NumberofInvoicesWith(partner));

            //L�tez� partner 1 sz�mla
            shop.Buy(partner,hamster1);
            Assert.AreEqual(1, shop.NumberofInvoicesWith(partner));

            //L�tez� partner t�bb sz�mla
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

            //Feln� a h�rcs�g
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