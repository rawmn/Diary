using System;
using System.Collections.Generic;

namespace Diary.Database
{
    public partial class Task
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int UserId { get; set; }
        public int DayId { get; set; }

        public bool Status { get; set; }

        public virtual Day Day { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
