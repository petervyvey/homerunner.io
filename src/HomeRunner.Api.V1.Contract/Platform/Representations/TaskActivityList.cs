﻿
using HalJsonNet.Configuration;
using HalJsonNet.Configuration.Attributes;
using HalJsonNet.Configuration.Interfaces;
using System;
using System.Collections.Generic;

namespace HomeRunner.Api.V1.Platform.Representations
{
	public class TaskActivityList
		: IHaveHalJsonLinks
	{
		public TaskActivityList (IEnumerable<TaskActivity> taskActivities)
		{
			this.TaskActivities = taskActivities;
		}

		[HalJsonEmbedded("taskActivities")]
		public IEnumerable<TaskActivity> TaskActivities { get; set;}


		public IDictionary<string, Link> GetLinks ()
		{
			var tenant = "tenant";
			return new Dictionary<string, Link> { { "self", string.Format("/v1/{0}/taskactivity", tenant) } };
		}
	}
}

