using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSqlPad
{
    public class Template
    {
        public class Person
        {
            public string username { get; set; }
            public int age { get; set; }
        }
        public IEnumerable sql(IFreeSql db)
        {
            return db.Select<Person>().Where(x => x.username.Contains("li")).ToList();
        }
    }
}
