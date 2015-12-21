using System;
using System.Collections.Generic;
using System.ServiceModel;
using ScrumManagementApp.Common.DTOs;

namespace ScrumManagementApp.WebService.Interfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IUserService
    {
        // authentication and registration
        [OperationContract]
        UserDTO CreateUser(UserDTO user);

        [OperationContract]
        List<UserDTO> GetAllUsers();

        [OperationContract]
        UserDTO GetUser(int userId);

        [OperationContract]
        UserDTO GetUserByEmail(String pEmail, bool pReturnProductOwner, bool pReturnScrumMaster);

        [OperationContract]
        bool UsernameExists(String username);

        [OperationContract]
        UserDTO ValidateLoginDetails(CondensedUserDTO user);

        [OperationContract]
        List<UserDTO> GetUsersByProjectId(int pProjectId);

        [OperationContract]
        List<UserDTO> FindUsersBySkillset(String Skillset);
    }

}
