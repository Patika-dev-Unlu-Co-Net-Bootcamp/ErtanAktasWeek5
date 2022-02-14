using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace hafta1WebApi
{
    public class Tasklar
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string Status { get;  set; }
        

        public DateTime Date { get; set; }

    }
}
