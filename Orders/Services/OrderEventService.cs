using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using Orders.Models;
using Orders.Schema;

namespace Orders.Services
{
	public interface IOrderEventService
	{
		ConcurrentStack<OrderEvent> AllEvents { get; }

		void AddError(Exception ex);

		OrderEvent AddEvent(OrderEvent orderEvent);

		IObservable<OrderEvent> EventStream();
	}


	public class OrderEventService : IOrderEventService
	{
		private readonly ISubject<OrderEvent> _eventStream = new ReplaySubject<OrderEvent>(1);

		public ConcurrentStack<OrderEvent> AllEvents { get; }

		public OrderEventService()
		{
			AllEvents = new ConcurrentStack<OrderEvent>();
		}


		public void AddError(Exception ex)
		{
			_eventStream.OnError(ex);
		}

		public OrderEvent AddEvent(OrderEvent orderEvent)
		{
			AllEvents.Push(orderEvent);
			_eventStream.OnNext(orderEvent);
			return orderEvent;
		}

		public IObservable<OrderEvent> EventStream()
		{
			return _eventStream.AsObservable();
		}
	}
}
