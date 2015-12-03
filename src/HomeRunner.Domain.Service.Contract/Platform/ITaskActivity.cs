
using System;

namespace HomeRunner.Domain.Service.Platform
{
    public interface ITaskActivity
    {
        /// <summary>
        /// The entity identifier.
        /// </summary>
        Guid Id { get; set; }

        string Description { get; }

        string AssignedTo { get; }

        bool IsClaimed { get; }

        bool IsCompleted { get; }
    }
}
