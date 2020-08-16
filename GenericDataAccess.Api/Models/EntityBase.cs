using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    internal class EntityBase
    {
        [Key] public Guid Id { get; set; }
    }
}