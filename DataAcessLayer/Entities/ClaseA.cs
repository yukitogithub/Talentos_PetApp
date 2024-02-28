using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class ClaseA
    {
        public int Id { get; set; }
        public string Data { get; set; }
        public List<ClaseB> ObjetosB { get; set; }
    }

    //var objetoA = new ClaseA();
    //objetoA.Id = 1;
    //objetoA.Data = "Data";
    //objetoA.ObjetoB = new ClaseB();
}
