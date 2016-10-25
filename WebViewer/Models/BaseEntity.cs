using System;
using System.ComponentModel.DataAnnotations;

namespace WebViewer.Models
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}