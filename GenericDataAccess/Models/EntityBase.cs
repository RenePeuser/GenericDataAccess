using System;
using System.ComponentModel.DataAnnotations;

namespace GenericDataAccess.Models
{
    internal class EntityBase
    {
        [Key]
        public Guid Id { get; set; }
    }
}