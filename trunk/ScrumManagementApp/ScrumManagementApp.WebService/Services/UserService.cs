using System;
using System.Collections.Generic;
using ScrumManagementApp.Business;
using ScrumManagementApp.Common.DTOs;
using ScrumManagementApp.EntityModels.Models;
using ScrumManagementApp.WebService.Common;
using ScrumManagementApp.WebService.Interfaces;

namespace ScrumManagementApp.WebService.Services
{
    public class UserService : IUserService
    {
        readonly UserLogic userLogic = new UserLogic();

        public UserDTO CreateUser(UserDTO userDto)
        {
            User user = new User
            {
                Email = userDto.Email,
                Password = userDto.Password,
                Alias = userDto.Alias,
                Firstname = userDto.Firstname,
                Lastname = userDto.Lastname,
                Skillset = userDto.SkillSet,
                IsProductOwner = userDto.IsProductOwner,
                IsScrumMaster = userDto.IsScrumMaster,
                IsDeveloper = userDto.IsDeveloper
            };
            userLogic.AddUser(user);

            return userDto;

        }

        public List<UserDTO> GetAllUsers()
        {
            IList<User> users = userLogic.GetAllUsers();
            List<UserDTO> userDTOs = new List<UserDTO>();
            
            if(users != null)
            {
                foreach (User u in users)
                {
                    userDTOs.Add(EntityTranslations.Translate_User_Entity_To_DTO(u));
                }
            }

            users = null;

            return userDTOs;

        }

        public UserDTO GetUser(int userId)
        {
            var user = userLogic.GetUser(userId);
            UserDTO userDTO = null;
            if(user != null)
            {
                EntityTranslations.Translate_User_Entity_To_DTO(user);
            }

            return userDTO;            
        }

        
        public UserDTO GetUserByEmail(String pEmail, bool pReturnProductOwner, bool pReturnScrumMaster)
        {
            var user = userLogic.GetUserByEmail(pEmail, pReturnProductOwner, pReturnScrumMaster);
            UserDTO userDTO = null;
            
            if (user != null)
            {
                userDTO = EntityTranslations.Translate_User_Entity_To_DTO(user);
            }

            return userDTO;     
        }
        

        /// <summary>
        /// Checks if username/email exists in the system
        /// </summary>
        /// <param name="username"></param>
        /// <returns>true if the username exists or false if it doesn't</returns>
        public bool UsernameExists(string username)
        {
            User user = userLogic.GetUserByUsername(username);

            if (user != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Searches DB for specified email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Returns the UserDTO associated to the input email if it exists, else null</returns>
        public UserDTO GetUserByEmail(String email)
        {
            User user = userLogic.GetUserByUsername(email);

            if (user != null)
            {
                return EntityTranslations.Translate_User_Entity_To_DTO(user);
            }
            return null;
        }

        /// <summary>
        /// Checks if username exists in the system and if the password specified matches that stored on the system
        /// </summary>
        /// <param name="loginDetails"></param>
        /// <returns>Returns a UserDTO if the user exists and the username && password match.  Null if not</returns>
        public UserDTO ValidateLoginDetails(CondensedUserDTO loginDetails)
        {      

            User user = userLogic.LogInUser(loginDetails.Email, loginDetails.Password);

            if (user != null)
            {
                return EntityTranslations.Translate_User_Entity_To_DTO(user);
            }
            return null;
        }

        public List<UserDTO> GetUsersByProjectId(int pProjectId)
        {
            IList<User> users = userLogic.GetUsersByProjectId(pProjectId);

            List<UserDTO> userDTOs = new List<UserDTO>();

            if(users != null)
            {
                foreach (User u in users)
                {
                    userDTOs.Add(EntityTranslations.Translate_User_Entity_To_DTO(u));
                }

                users = null;
            }
            
            return userDTOs;
        }

        public List<UserDTO> FindUsersBySkillset(String Skillset)
        {
            List<User> entities = userLogic.FindUsersBySkillset(Skillset);
            List<UserDTO> dtos = new List<UserDTO>();

            foreach(User u in entities)
            {
                dtos.Add(EntityTranslations.Translate_User_Entity_To_DTO(u));
            }
            return dtos;
        }

    }

}
