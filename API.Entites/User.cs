using System;
using System.Collections.Generic;
using System.Text;

namespace API.Entites
{
   public  class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public string Token { get; set; }
    }
}
