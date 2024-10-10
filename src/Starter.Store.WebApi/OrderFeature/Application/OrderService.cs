//namespace Starter.Store.WebApi.OrderFeature.Application;

//public class OrderService
//{
//    private readonly IUserService _userService;
//    private readonly IOrderRepository _orderRepository;

//    public OrderService(IUserService userService, IOrderRepository orderRepository)
//    {
//        _userService = userService;
//        _orderRepository = orderRepository;
//    }

//    public async Task<Order> CreateOrder(OrderDto orderDto)
//    {
//        // Check if the user exists and has enough balance
//        var userExists = await _userService.UserExists(orderDto.UserId);
//        if (!userExists)
//        {
//            throw new Exception("User does not exist");
//        }

//        var hasSufficientFunds = await _userService.HasSufficientFunds(orderDto.UserId, orderDto.TotalAmount);
//        if (!hasSufficientFunds)
//        {
//            throw new Exception("Insufficient funds");
//        }

//        // Create the order
//        var order = new Order(orderDto);
//        await _orderRepository.AddAsync(order);

//        return order;
//    }
//}
