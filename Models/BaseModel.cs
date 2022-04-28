namespace GroceryPalAPI.Models
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; }
        public string LocalId { get; set; }

        public BaseModel(Guid? Id, string LocalId)
        {
            this.Id = Id ?? Guid.NewGuid();
            this.LocalId = LocalId;
        }
    }
}
