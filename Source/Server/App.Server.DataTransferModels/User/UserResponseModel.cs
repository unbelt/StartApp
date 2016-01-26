namespace App.Server.DataTransferModels.User
{
    using App.Server.Common.Mapping;

    public class UserResponseModel : IMapFrom<Data.Models.User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
    }
}
