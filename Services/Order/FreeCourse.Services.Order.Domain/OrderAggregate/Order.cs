using FreeCourse.Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    // EF Core Features
    // -- Owned Types
    // -- Shadow Property
    // -- Backing Field
    public class Order : Entity, IAggregateRoot
    {
        public DateTime CreatedDate { get; private set; }
        public Address Address  { get; private set; }
        public string BuyerId { get; private set; } //get ve set var, bu bir -property
        //public string OrderId { get; set; } OrderId kodlamada yok fakat EF onu databasede tanımlayacak -Shadow Property-

        private readonly List<OrderItem> _orderItems; //get ve set yok, bu bir field -backing field- EFCore dolduracak
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems; //itemler dıs dunyadan alınabilsin diye readonly ekledim, capsulledim

        public Order(string buyerId, Address address) //orderları sadece buyerId ve address alarak olusturuyorum
        {
            _orderItems = new List<OrderItem>();
            CreatedDate = DateTime.Now;
            BuyerId = buyerId;
            Address = address;
        }

        public Order()
        {
        }

        public void AddOrderItem(string productId, string productName, decimal price, string pictureUrl)  //Order'a item eklemek lazım, item private dolayısıyla
        {                                                                                                 //metot ile ekliyorum
            var existProduct = _orderItems.Any(x => x.ProductId == productId);
            if (!existProduct)
            {
                var newOrderItem = new OrderItem(productId, productName, pictureUrl, price);
                _orderItems.Add(newOrderItem);
            }
        }

        public decimal getTotalPrice => _orderItems.Sum(x => x.Price);
    }
}
