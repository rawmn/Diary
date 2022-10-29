using System;
using System.Collections.Generic;

namespace Diary.Database
{
    public partial class User
    {
        public User()
        {
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Fio { get; set; } = null!;

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
