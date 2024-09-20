﻿using App.Domain.Entities.Common;

namespace App.Domain.Entities;
public class Product:BaseEntity<int>,IAuditEntity
{
    //public int Id { get; set; }
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    public int Stock { get; set; }

    public int CategoryId { get; set; }
    public Category Category { get; set; } = default!; // Burası null olamaz.
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
}