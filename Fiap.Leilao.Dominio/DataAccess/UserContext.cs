using Fiap.Leilao.Dominio.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiap.Leilao.Dominio.DataAccess
{
    public class UserContext : IdentityDbContext<User>
    {
    }
}
