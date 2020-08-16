using System;
using System.ComponentModel.DataAnnotations;

namespace Cli.Models
{
    internal class EntityBase
    {
        [Key] public Guid Id { get; set; }
    }
}