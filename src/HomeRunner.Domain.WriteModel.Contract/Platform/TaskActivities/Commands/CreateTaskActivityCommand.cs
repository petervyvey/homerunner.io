
using HomeRunner.Foundation.Cqrs;
using System;

namespace HomeRunner.Domain.WriteModel.Platform.TaskActivities.Commands
{
	public class CreateTaskActivityCommand
		: Command
	{
		public CreateTaskActivityCommand()
			: base() { }

		public CreateTaskActivityCommand(string description)
			: this()
		{
			this.Description = description;
		}

		public string Description { get; set; }
	}
}

