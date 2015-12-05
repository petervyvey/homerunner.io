
using System;

namespace HomeRunner.Domain.Service.Platform
{
	/// <summary>
	/// Interface defining the exposed properties of a task domain object.
	/// </summary>
    public interface ITaskActivity
    {
        /// <summary>
        /// The entity identifier.
        /// </summary>
        Guid Id { get; set; }

		/// <summary>
		/// The task description.
		/// </summary>
		/// <value>The description.</value>
        string Description { get; }

		/// <summary>
		/// The person the task is assigned to.
		/// </summary>
		/// <value>The assigned to.</value>
        string AssignedTo { get; }

		/// <summary>
		/// TRUE if the task is claimed, FALSE otherwise.
		/// </summary>
		/// <value><c>true</c> if this instance is claimed; otherwise, <c>false</c>.</value>
        bool IsClaimed { get; }

        bool IsCompleted { get; }
    }
}
