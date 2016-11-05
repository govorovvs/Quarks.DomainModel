using System;
using System.Collections.Generic;
using System.Linq;
using Quarks.DomainModel.Events;

namespace Quarks.DomainModel.Tests.Domain.Repositories
{
    public class ShipEventRepository
    {
        public List<IEntityEvent> Events { get; set; }= new List<IEntityEvent>();

        public IEnumerable<IEntityEvent> Find(DateTime time)
        {
            return Events.Where(x => x.Occurred <= time).OrderBy(x => x.Occurred);
        }

        public void Save(IEnumerable<IEntityEvent> events)
        {
            Events.AddRange(events);
        }
    }
}