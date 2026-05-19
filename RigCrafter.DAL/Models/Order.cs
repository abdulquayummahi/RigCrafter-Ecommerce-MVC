using System;
using System.Collections.Generic;

namespace RigCrafter.DAL.Models;

public partial class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime OrderDate { get; set; }

    public string ShippingAddress { get; set; } = null!;

    public decimal TotalAmount { get; set; }

    public string OrderStatus { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual User User { get; set; } = null!;
}
