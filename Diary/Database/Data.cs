using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Database
{
    public class Data
    {
        private static PlannerContext _context;
        public static PlannerContext Context()
        {
            string asd;
            if(_context == null)
                _context = new PlannerContext();
            return _context;
        }
    }
}
