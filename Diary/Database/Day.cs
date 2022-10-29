using System;
using System.Collections.Generic;

namespace Diary.Database
{
    public partial class Day
    {
        public Day()
        {
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }
        public DateTime? Date { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
