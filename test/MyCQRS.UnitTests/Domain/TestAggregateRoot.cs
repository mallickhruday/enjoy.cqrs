﻿using System;
using MyCQRS.EventStore;
using MyCQRS.UnitTests.Domain.Events;

namespace MyCQRS.UnitTests.Domain
{
    public class TestAggregateRoot : Aggregate
    {
        public string Name { get; private set; }

        public TestAggregateRoot()
        {
            On<SomeEvent>(x =>  { Name = x.Name; });
            On<TestAggregateCreatedEvent>(x => { Id = x.AggregateId; });
        }

        private TestAggregateRoot(Guid newGuid) : this()
        {
            Raise(new TestAggregateCreatedEvent(newGuid));
        }

        public static TestAggregateRoot Create()
        {
            return new TestAggregateRoot(Guid.NewGuid());
        }
        
        public void DoSomething(string name)
        {
            Raise(new SomeEvent(name));
        }

        public void DoSomethingWithoutEvent()
        {
            Raise(new NotRegisteredEvent());
        }
    }
}