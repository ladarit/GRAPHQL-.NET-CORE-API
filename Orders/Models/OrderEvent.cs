using System;

namespace Orders.Models
{
	public class OrderEvent
	{
		public OrderEvent(string orderId, string name, OrderStatuses status, DateTime timeStamp)
		{
			Id = Guid.NewGuid().ToString();
			OrderId = orderId;
			Name = name;
			Status = status;
			TimeStamp = timeStamp;
		}

		public string Id { get; set; }

		public string OrderId { get; set; }

		public string Name { get; set; }

		public OrderStatuses Status { get; set; }

		public DateTime TimeStamp { get; set; }
	}
}
