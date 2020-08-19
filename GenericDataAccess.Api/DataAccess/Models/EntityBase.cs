using System;
using System.ComponentModel.DataAnnotations;

namespace Api.DataAccess.Models
{
    internal class EntityBase
    {
        [Key] public Guid Id { get; set; }
    }
}