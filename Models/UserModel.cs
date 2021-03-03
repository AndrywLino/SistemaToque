using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaToque.Models
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
        public string MessageUser { get; set; }
        public string MessageSenha { get; set; }
    }
}