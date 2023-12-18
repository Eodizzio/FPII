using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Dominio
{
    public class FormaPago
    {
        public int Id { get; set; }
        public string formaPago { get; set; }


        public FormaPago()
        {
            Id = 0;
            formaPago = string.Empty;
        }
        public FormaPago(int id, string fp)
        {
            this.Id = id;
            this.formaPago = fp;
        }

        public override string ToString()
        {
            return formaPago;
        }
    }
}
