using App.Server.Common.Mapping;

namespace App.Server.DataTransferModels.User
{
    public class UserResponseModel : IMapFrom<Data.Models.User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
    }
}
