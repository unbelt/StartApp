namespace App.Data.Models
{
    using System.Collections.Generic;

    public class Category : BaseModel<int>
    {
        public Category()
        {
            this.Entities = new HashSet<Entity>();
        }

        public string Name { get; set; }

        public virtual ICollection<Entity> Entities { get; set; }
    }
}
