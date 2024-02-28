using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class ClaseB
    {
        public int Id { get; set; }
        public string Data { get; set; }
        public List<ClaseA> ObjetosA { get; set; }
    }

    //var objetoB = new ClaseB();
    //objetoB.Id = 1;
    //objetoB.Data = "Data";
    //objetoB.ObjetoA = new ClaseA();
    
    //Relacion de 1 a 1
    //Ejemplo:
    //Un Cliente tiene una Direccion

    //Relacion de 1 a muchos
    //Ejemplo:
    //Un Cliente tiene muchas Ordenes
    //Una Provincia tiene muchas ciudades

    //Relacion de muchos a muchos
    //Ejemplo:
    //Una Orden tiene muchos Productos
    //Un Producto tiene muchas Ordenes

    //Libro en una biblioteca
    //Un libro tiene muchos autores
    //Un autor tiene muchos libros
}
