﻿namespace GroceryPalAPI.Models
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid LocalId { get; set; }
    }
}
