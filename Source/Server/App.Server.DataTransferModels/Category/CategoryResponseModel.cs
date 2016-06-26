namespace App.Server.DataTransferModels.Category
{
    using App.Data.Models;
    using App.Server.Common.Mapping;

    public class CategoryResponseModel : BaseModel<int>, IMapFrom<Category>
    {
        public string Name { get; set; }
    }
}
