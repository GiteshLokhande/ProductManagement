using AutoMapper;
using FluentAssertions;
using Moq;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Mappings;
using ProductManagement.Application.Services;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Exceptions;
using Xunit;

namespace ProductManagement.Application.Tests.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<ICurrentUserService> _currentUserServiceMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly IMapper _mapper;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _currentUserServiceMock = new Mock<ICurrentUserService>();
            _currentUserServiceMock.Setup(x => x.UserName).Returns("TestUser");

            _currentUserServiceMock.Setup(x => x.UserId).Returns("123");

            _currentUserServiceMock.Setup(x => x.Email).Returns("test@test.com");
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _productRepositoryMock = new Mock<IProductRepository>();

            _unitOfWorkMock.Setup(x => x.Products).Returns(_productRepositoryMock.Object);

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _productService = new ProductService(_unitOfWorkMock.Object, _mapper, _currentUserServiceMock.Object);
        }

        [Fact]
        public async Task CreateAsync_Should_Create_Product_When_Request_Is_Valid()
        {
            // Arrange
            var dto = new CreateProductDto
            {
                ProductName = "Laptop"
            };

            _unitOfWorkMock
                .Setup(x => x.SaveChangesAsync())
                .ReturnsAsync(1);

            // Act
            var result = await _productService.CreateAsync(dto);

            // Assert
            result.Should().NotBeNull();
            result.ProductName.Should().Be("Laptop");

            _productRepositoryMock.Verify(
                x => x.AddAsync(It.Is<Product>(p =>
                    p.ProductName == "Laptop" &&
                    p.CreatedBy == "TestUser")),
                Times.Once);

            _unitOfWorkMock.Verify(
                x => x.SaveChangesAsync(),
                Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Product_When_Product_Exists()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                ProductName = "Laptop",
                CreatedBy = "TestUser",
                CreatedOn = DateTime.UtcNow
            };

            _productRepositoryMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(product);

            // Act
            var result = await _productService.GetByIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(1);
            result.ProductName.Should().Be("Laptop");

            _productRepositoryMock.Verify(
                x => x.GetByIdAsync(1),
                Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Throw_NotFoundException_When_Product_Does_Not_Exist()
        {
            // Arrange
            _productRepositoryMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync((Product?)null);

            // Act
            Func<Task> act = async () => await _productService.GetByIdAsync(1);

            // Assert
            await act.Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage("Product with Id 1 was not found.");

            _productRepositoryMock.Verify(
                x => x.GetByIdAsync(1),
                Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_All_Products()
        {
            // Arrange
            var products = new List<Product>
    {
        new Product
        {
            Id = 1,
            ProductName = "Laptop",
            CreatedBy = "TestUser",
            CreatedOn = DateTime.UtcNow
        },
        new Product
        {
            Id = 2,
            ProductName = "Mouse",
            CreatedBy = "TestUser",
            CreatedOn = DateTime.UtcNow
        }
    };

            _productRepositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(products);

            // Act
            var result = await _productService.GetAllAsync();

            // Assert
            result.Should().HaveCount(2);

            result.Select(x => x.ProductName)
                  .Should()
                  .Contain(new[] { "Laptop", "Mouse" });

            _productRepositoryMock.Verify(
                x => x.GetAllAsync(),
                Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_Should_Update_Product_When_Product_Exists()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                ProductName = "Old Product",
                CreatedBy = "TestUser",
                CreatedOn = DateTime.UtcNow
            };

            var dto = new UpdateProductDto
            {
                ProductName = "New Product"
            };

            _productRepositoryMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(product);

            _unitOfWorkMock
                .Setup(x => x.SaveChangesAsync())
                .ReturnsAsync(1);

            // Act
            await _productService.UpdateAsync(1, dto);

            // Assert
            product.ProductName.Should().Be("New Product");

            _productRepositoryMock.Verify(
                x => x.Update(It.Is<Product>(p =>
                    p.ProductName == "New Product")),
                Times.Once);

            _unitOfWorkMock.Verify(
                x => x.SaveChangesAsync(),
                Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_Should_Throw_NotFoundException_When_Product_Does_Not_Exist()
        {
            // Arrange
            var dto = new UpdateProductDto
            {
                ProductName = "New Product"
            };

            _productRepositoryMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync((Product?)null);

            // Act
            Func<Task> act = async () => await _productService.UpdateAsync(1, dto);

            // Assert
            await act.Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage("Product with Id 1 was not found.");

            _productRepositoryMock.Verify(
                x => x.GetByIdAsync(1),
                Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_Should_Delete_Product_When_Product_Exists()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                ProductName = "Laptop",
                CreatedBy = "TestUser",
                CreatedOn = DateTime.UtcNow
            };

            _productRepositoryMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(product);

            _unitOfWorkMock
                .Setup(x => x.SaveChangesAsync())
                .ReturnsAsync(1);

            // Act
            await _productService.DeleteAsync(1);

            // Assert
            _productRepositoryMock.Verify(
                x => x.Delete(product),
                Times.Once);

            _unitOfWorkMock.Verify(
                x => x.SaveChangesAsync(),
                Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_Should_Throw_NotFoundException_When_Product_Does_Not_Exist()
        {
            // Arrange
            _productRepositoryMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync((Product?)null);

            // Act
            Func<Task> act = async () => await _productService.DeleteAsync(1);

            // Assert
            await act.Should()
                .ThrowAsync<NotFoundException>()
                .WithMessage("Product with Id 1 was not found.");

            _productRepositoryMock.Verify(
                x => x.GetByIdAsync(1),
                Times.Once);

            _productRepositoryMock.Verify(
                x => x.Delete(It.IsAny<Product>()),
                Times.Never);

            _unitOfWorkMock.Verify(
                x => x.SaveChangesAsync(),
                Times.Never);
        }
    }
}