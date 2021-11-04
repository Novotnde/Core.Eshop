
using Core.Contracts.Models;
using Core.DataAccess.ContractsDAL;
using Core.DataAccess.ModelDAL;
using Moq;
using NUnit.Framework;

namespace Core.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(x => x.GetProductByIdAsync(42)).ReturnsAsync(new ProductDAL());
        }
    }
}
