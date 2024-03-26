using AutoMapper;
using Azure;
using Microsoft.VisualBasic;
using Repository.Models;
using Repository.UnitOfWork;
using Services.Extensions;
using Shared.ModelsDto;
using Shared.Services;

namespace Services.Services;

public class OrderProductService : IOrderProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    // Define an operation to add the quantity
    private int AddOperation(int currentQuantity, int changeBy) => currentQuantity + changeBy;

    // Define an operation to subtract the quantity
    private int SubtractOperation(int currentQuantity, int changeBy) => currentQuantity - changeBy;


    public OrderProductService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    //#region OldCode
    //public async Task<OrderProductDto> CreateOrderProductAsync(IEnumerable<CreateOrderProductDto> orderProducts)
    //{
    //    var productTasks = orderProducts.Select(async op =>
    //    {
    //        var productDto = await UpdateProductAsync(op);
    //        return productDto;
    //    });

    //    var products = await Task.WhenAll(productTasks);


    //    var order = await _unitOfWork.Orders.CreateAsync(new Order { CreateAt = DateTimeOffset.UtcNow, Amount = CalculateAmount(products) });

    //    CreateOrderProduct(order.Id, orderProducts);

    //    return new OrderProductDto
    //    {
    //        OrderId = order.Id,
    //        Amount = order.Amount,
    //        CreatedAt = order.CreateAt,
    //        Products = products
    //    };
    //}
    //private void CreateOrderProduct(int orderID, IEnumerable<CreateOrderProductDto> orderProducts)
    //{
    //    orderProducts.ForEach(async orderProduct =>
    //    {
    //        await _unitOfWork.OrderProducts.CreateAsync(new OrderProduct
    //        {
    //            ProductId = orderProduct.ProductId,
    //            OrderId = orderID,
    //            Quantity = orderProduct.Quantity
    //        });
    //    });
    //}

    //private async Task<ProductDto> UpdateProductAsync(CreateOrderProductDto orderProduct)
    //{
    //    // Verify product quantity availability
    //    var availableProduct = await GetAvailableProduct(orderProduct.ProductId);
    //    if (availableProduct.Quantity < orderProduct.Quantity)
    //    {
    //        throw new InvalidOperationException("Insufficient product quantity available.");
    //    }

    //    var changedProduct = ChangeProductQuantity(availableProduct, orderProduct.Quantity, SubtractOperation);

    //    _unitOfWork.Products.UpdateAsync(changedProduct);

    //    return _mapper.Map<ProductDto>(changedProduct);

    //}


    //private async Task<Product> GetAvailableProduct(int productId)
    //{
    //    // Implementation of logic to retrieve available quantity of a product from inventory or wherever it's stored
    //    // For demonstration purpose, let's assume it's stored in a ProductRepository
    //    var product = await _unitOfWork.Products.GetAsync(product => product.Id.Equals(productId));
    //    if (product == null)
    //    {
    //        throw new InvalidOperationException($"Product with ID {productId} not found.");
    //    }
    //    return product;
    //}

    //private Product ChangeProductQuantity(Product product, int quantity, Func<int, int, int> operation)
    //{
    //    int newQuantity = operation(product.Quantity, quantity);
    //    product.Quantity = newQuantity;
    //    return product;
    //}

    //private double CalculateAmount(IEnumerable<ProductDto> products)
    //{
    //    double totalAmount = 0;

    //    foreach (var product in products)
    //    {
    //        totalAmount += product.ProductPrice * product.ProductQuantity;
    //    }

    //    return totalAmount;
    //}
    //#endregion


    #region NewCode
    public async Task<IReadOnlyCollection<OrderProductDto>> GetAllOrderProductsAsync()
    {
        var orderProducts = await _unitOfWork.OrderProducts.GetAllAsync();


        var orderProductDtos = new List<OrderProductDto>();

        foreach (var orderProduct in orderProducts)
        {
            var product = orderProduct.Product;

            var orderProductDto = new OrderProductDto
            {
                OrderId = orderProduct.OrderId,
                CreatedAt = orderProduct.Order.CreateAt, 
                Amount = orderProduct.Order.Amount, 
                Products = orderProducts.Where(op => op.OrderId == orderProduct.OrderId)
                                        .Select(op => _mapper.Map<ProductDto> (op.Product))
                                        .ToList()
            };

            orderProductDtos.Add(orderProductDto);
        }

        return orderProductDtos;

    }
    public async Task<OrderProductDto> UpdateOrderProductAsync(int orderId, IEnumerable<CreateOrderProductDto> updatedOrderProducts)
    {
        // Retrieve existing order products for the specified order ID
        var existingOrderProducts = await _unitOfWork.OrderProducts.GetAllAsync(op => op.OrderId == orderId);

        // Validate that existing order products exist for the specified order ID
        if (existingOrderProducts == null || !existingOrderProducts.Any())
        {
            throw new InvalidOperationException($"No existing order products found for order with ID {orderId}.");
        }

        var updatedProducts = new List<ProductDto>();

        // Iterate through each updated order product
        foreach (var updatedOrderProduct in updatedOrderProducts)
        {
            // Find the corresponding existing order product
            var existingOrderProduct = existingOrderProducts.FirstOrDefault(op => op.ProductId == updatedOrderProduct.ProductId);

            // Ensure the existing order product is found
            if (existingOrderProduct == null)
            {
                throw new InvalidOperationException($"No existing order product found for product with ID {updatedOrderProduct.ProductId} and order ID {orderId}.");
            }

            // Retrieve the corresponding product for the existing order product
            var product = existingOrderProduct.Product;

            // Calculate the difference in quantity between the updated order product and the existing order product
            var quantityDifference = updatedOrderProduct.Quantity - existingOrderProduct.Quantity;

            // Update the product quantity by adding the quantity difference
            var updatedProduct = await UpdateProductAsync(product, quantityDifference, SubtractOperation);
            updatedProducts.Add(updatedProduct);

            // Update the order product with the updated quantity
            existingOrderProduct.Quantity = updatedOrderProduct.Quantity;

            // Save changes to the order product
            await _unitOfWork.OrderProducts.UpdateAsync(existingOrderProduct);
        }

        // Calculate the total amount for the updated order
        var updatedOrder = existingOrderProducts.First().Order;
        updatedOrder.Amount = CalculateAmount(updatedProducts, updatedOrderProducts);

        // Save changes to the order and associated products
        await _unitOfWork.CompleteAsync();

        // Map the updated order and products to an OrderProductDto
        return MapToOrderProductDto(updatedOrder, updatedProducts);
    }

    public async Task<OrderProductDto> CreateOrderProductAsync(IEnumerable<CreateOrderProductDto> orderProducts)
    {
        var products = await CollectProductsAsync(orderProducts, SubtractOperation);
        var order = await CreateOrderAsync(products, orderProducts);
        await CreateOrderProductsAsync(order.Id, orderProducts);

        await _unitOfWork.CompleteAsync();

        return MapToOrderProductDto(order, products);
    }

    private async Task<IEnumerable<ProductDto>> CollectProductsAsync(IEnumerable<CreateOrderProductDto> orderProducts, Func<int, int, int> operation)
    {
        var productDtos = new List<ProductDto>();

        foreach (var orderProduct in orderProducts)
        {
            var productDto = await UpdateProductAsync(orderProduct, operation);
            productDtos.Add(productDto);
        }

        return productDtos;
    }

    private async Task<Order> CreateOrderAsync(IEnumerable<ProductDto> products, IEnumerable<CreateOrderProductDto> createProducts)
    {
        var amount = CalculateAmount(products, createProducts);
        var order = await _unitOfWork.Orders.CreateAsync(new Order { CreateAt = DateTimeOffset.UtcNow, Amount = amount });
        return order;
    }

    private async Task CreateOrderProductsAsync(int orderId, IEnumerable<CreateOrderProductDto> orderProducts)
    {
        orderProducts.ForEach(async orderProduct =>
        {
            await _unitOfWork.OrderProducts.CreateAsync(new OrderProduct
            {
                ProductId = orderProduct.ProductId,
                OrderId = orderId,
                Quantity = orderProduct.Quantity
            });
        });
    }

    private async Task<ProductDto> UpdateProductAsync(CreateOrderProductDto orderProduct, Func<int, int, int> operation)
    {
        var availableProduct = await GetAvailableProduct(orderProduct.ProductId);
        return await VerifyAndUpdateProductAsync(availableProduct, orderProduct.Quantity, operation);
    }

    private async Task<ProductDto> UpdateProductAsync(Product availableProduct, int quantity, Func<int, int, int> operation)
    {
        var changedProduct = ChangeProductQuantity(availableProduct, quantity, operation);
        await _unitOfWork.Products.UpdateAsync(changedProduct);
        var mappedProduct = _mapper.Map<ProductDto>(changedProduct);
        return mappedProduct;
    }

    private async Task<Product> GetAvailableProduct(int productId)
    {
        var product = await _unitOfWork.Products.GetAsync(product => product.Id == productId);
        if (product == null)
        {
            throw new InvalidOperationException($"Product with ID {productId} not found.");
        }
        return product;
    }

    private Product ChangeProductQuantity(Product product, int quantity, Func<int, int, int> operation)
    {
        int newQuantity = operation(product.Quantity, quantity);
        product.Quantity = newQuantity;
        return product;
    }

    private async Task<ProductDto> VerifyAndUpdateProductAsync(Product product, int quantity, Func<int, int, int> operation)
    {
        if (product.Quantity < quantity)
        {
            throw new InvalidOperationException("Insufficient product quantity available.");
        }
        return await UpdateProductAsync(product, quantity, operation);
    }

    private double CalculateAmount(IEnumerable<ProductDto> products, IEnumerable<CreateOrderProductDto> createProducts)
    {
        double amount = 0;
        products.ForEach(product => { 
            var price = product.ProductPrice;
            var quantity = createProducts.FirstOrDefault(p => p.ProductId == product.ProductID).Quantity;

            amount += price * quantity;
        });
        return amount;
    }

    private OrderProductDto MapToOrderProductDto(Order order, IEnumerable<ProductDto> products)
    {
        return new OrderProductDto
        {
            OrderId = order.Id,
            Amount = order.Amount,
            CreatedAt = order.CreateAt,
            Products = products.ToList()
        };
    }
    #endregion
}


