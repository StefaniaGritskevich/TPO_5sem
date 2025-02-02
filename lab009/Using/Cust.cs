using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShop;
using BookShop.BookShop;

namespace Using
{
    public class Cust
    {

        static void Main(string[] args)
        {
            IPublicationRepository repository = new PublicationRepository();
            Store store = new Store(repository);
            store.ShowInventory();
            List<Publication> mybooks = store.SearchByAuthor("Джордж Оруэлл");


            Cart cart = new Cart();
            foreach (Publication item in mybooks)
            {
                cart.AddItem(item);

            }

            cart.ShowCart();
        }
    }
}
