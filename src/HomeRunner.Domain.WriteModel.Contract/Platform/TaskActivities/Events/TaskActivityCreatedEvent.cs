
using HomeRunner.Domain.WriteModel.Platform.TaskActivities.Commands;
using HomeRunner.Foundation.Cqrs;
using System;

namespace HomeRunner.Domain.WriteModel.Platform.TaskActivities.Events
{
	public class TaskActivityCreatedEvent
		: DomainEvent
	{
		public TaskActivityCreatedEvent(CreateTaskActivityCommand command)
			: base(command.Id)
		{
			this.TaskId = Guid.NewGuid ();
			this.Description = command.Description;
		}

		public Guid TaskId { get; set; }

		public string Description { get; set; }
	}
}

