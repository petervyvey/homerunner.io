
using FluentNHibernate.Mapping;

namespace HomeRunner.Domain.Data.Platform.Map
{
    public class TaskActivityMap : ClassMap<Platform.TaskActivity>
    {
        public TaskActivityMap()
        {
            this.Table("Task");

            this.Id(e => e.Id).GeneratedBy.Assigned();

            this.Map(e => e.Description);
            this.Map(e => e.IsClaimed);
            this.Map(e => e.IsCompleted);
            this.Map(e => e.AssignedTo);
            this.Map(e => e.UpdateTime);
        }
    }
}
