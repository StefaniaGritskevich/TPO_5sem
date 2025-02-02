using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using BookShop;
using Xunit.Abstractions;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
namespace BookShopTests
{
    public class BookShopTests
    {

        private readonly Mock<IPublicationRepository> _mockRepository;
        private readonly Cart _cart;
        private readonly List<Publication> _testPublications;
        private  Store _store;


        public BookShopTests()
        {
            
            _mockRepository = new Mock<IPublicationRepository>();
            _cart = new Cart();
            _store = new Store();

            _testPublications = new List<Publication>
            {
               new Book("Test Book 1", "Author 1", 10m,"Genre 2","123456",456),
               new Book("Test Book 2", "Author 2", 20m,"Genre 2","123457",153),
               new Magazine("Test Magazine 1", "Author 3", 5m,"Genre 3",345,"Weekly"),
               new Newspaper("Test Newspaper 1", "Author 4", 2m,"Genre 1","05.11.2024","BSTUPubl")
            };
           

        }



        [Fact]
        public void Store_SearchByAuthor_ShouldReturnCorrectResults()
        {
            _mockRepository.Setup(rep => rep.GetAllPublications()).Returns(_testPublications);
            var bookstore = new Store(_mockRepository.Object);
            var expectedAuthor = "Author 2";

            var res = bookstore.SearchByAuthor(expectedAuthor);

            Assert.Single(res);
            Assert.Equal(expectedAuthor, res.First().Author);
            Assert.Equal("Test Book 2", res.First().Title);
            _mockRepository.Verify(repo => repo.GetAllPublications(), Times.Once);

        }


        [Fact]
        public void Cart__CalculateTotal_ShouldReturnCorrectSum()
        {
            var testcart = new Cart();
            var expectedSum = 37m;
           foreach (var item in _testPublications)
            {
                testcart.AddItem(item);
            }
          var actualTotal = testcart.CalculateTotal();

            Assert.Equal(expectedSum, actualTotal);
            Assert.Equal(4, testcart.GetItemCount());

        }

        [Fact]
        public void Store_SearchByGenre_ShouldReturnCorrectResults()
        {
            _mockRepository.Setup(repo => repo.GetAllPublications()).Returns(_testPublications);
            var bookstore = new Store(_mockRepository.Object);
            var expectedGenre = "Genre 4";

            var results = bookstore.SearchByGenre(expectedGenre);
            
            Assert.Equal(2, results.Count);
            Assert.All(results, publication => Assert.Contains(expectedGenre, publication.Genre));
            _mockRepository.Verify(repo => repo.GetAllPublications(), Times.Once);
        }

    }






}