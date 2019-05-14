﻿using GraphQL.Types;

namespace Orders.GraprhQlTypes.Order
{
    public class OrderStatusesEnum : EnumerationGraphType
    {
        public OrderStatusesEnum()
        {
            Name = "OrderStatuses";
            AddValue("CREATED", "Order was created", 2);
            AddValue("PROCESSING", "Order is being processed", 4);
            AddValue("COMPLETED", "Order is completed", 8);
            AddValue("CANCALLED", "Order was cancalled", 16);
            AddValue("CLOSED", "Order was closed", 32);
        }
    }
}