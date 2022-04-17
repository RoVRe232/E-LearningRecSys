using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Domain.Entities.Tokens
{
    public class JwtToken
    {
        [Key]
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
