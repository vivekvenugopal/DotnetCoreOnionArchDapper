using System.Data;
using Dapper;
using Demo.DataAccess;
using Demo.DTO;
using Moq;
using Xunit;

namespace Demo.UnitTest.Service
{
    public class ProductRepositoryTests
    {
        private readonly Mock<IDbConnection> _mockDbConnection;

        private readonly ProductRepository _productRepository;

        public ProductRepositoryTests()
        {
            _mockDbConnection = new Mock<IDbConnection>();

            _productRepository = new ProductRepository(_mockDbConnection.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 100 },
                new Product { Id = 2, Name = "Product 2", Price = 200 }
            };
            _mockDbConnection
                .Setup(db => db.QueryAsync<Product>("SELECT * FROM Products", null, null, null, null))
                .ReturnsAsync(products);

            // Act
            var result = await _productRepository.GetAllAsync();

            // Assert
            Assert.NotNull(result);

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Product 1", Price = 100 };
            _mockDbConnection
                .Setup(db => db.QuerySingleOrDefaultAsync<Product>(
                    "SELECT * FROM Products WHERE Id = @Id",
                    It.IsAny<object>(), null, null, null))
                .ReturnsAsync(product);

            // Act
            var result = await _productRepository.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(product.Id, result.Id);
        }

        [Fact]
        public async Task AddAsync_ShouldInsertProduct()
        {
            // Arrange
            var product = new Product { Name = "New Product", Price = 150 };
            _mockDbConnection
                .Setup(db => db.ExecuteAsync(
                    "INSERT INTO Products (Name, Price) VALUES (@Name, @Price)",
                    It.IsAny<object>(), null, null, null))
                .ReturnsAsync(1);

            // Act
            await _productRepository.AddAsync(product);

            // Assert
            _mockDbConnection.Verify(db => db.ExecuteAsync(
                "INSERT INTO Products (Name, Price) VALUES (@Name, @Price)",
                It.IsAny<object>(), null, null, null), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateProduct()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Updated Product", Price = 200 };
            _mockDbConnection
                .Setup(db => db.ExecuteAsync(
                    "UPDATE Products SET Name = @Name, Price = @Price WHERE Id = @Id",
                    It.IsAny<object>(), null, null, null))
                .ReturnsAsync(1);

            // Act
            await _productRepository.UpdateAsync(product);

            // Assert
            _mockDbConnection.Verify(db => db.ExecuteAsync(
                "UPDATE Products SET Name = @Name, Price = @Price WHERE Id = @Id",
                It.IsAny<object>(), null, null, null), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteProduct()
        {
            // Arrange
            var productId = 1;
            _mockDbConnection
                .Setup(db => db.ExecuteAsync(
                    "DELETE FROM Products WHERE Id = @Id",
                    It.IsAny<object>(), null, null, null))
                .ReturnsAsync(1);

            // Act
            await _productRepository.DeleteAsync(productId);

            // Assert
            _mockDbConnection.Verify(db => db.ExecuteAsync(
                "DELETE FROM Products WHERE Id = @Id",
                It.IsAny<object>(), null, null, null), Times.Once);
        }
    }
}
