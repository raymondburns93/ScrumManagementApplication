using ScrumManagementApp.DAL.Repository;
using System;
using System.Collections.Generic;
using ScrumManagementApp.EntityModels.Models;

namespace ScrumManagementApp.Business
{
    public class UserLogic
    {
        private readonly IUserRepository userRepository;

        /// <summary>
        /// Constructor. Instantiates user repository.
        /// </summary>
        public UserLogic()
        {
            userRepository = new UserRepository();
        }

        /// <summary>
        /// Constructor. Used for unit testing.
        /// </summary>
        /// <param name="userRepository"></param>
        public UserLogic(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Returns a list of all known users.
        /// </summary>
        /// <returns></returns>
        public IList<User> GetAllUsers()
        {
            return userRepository.GetAll(null);
        }

        /// <summary>
        /// Returns a user entity based on their email address.
        /// </summary>
        /// <param name="pEmail">user's email address</param>
        /// <param name="pReturnProductOwner">Is the user marked as a product owner?</param>
        /// <param name="pReturnScrumMaster">Is the user marked as a scrum master?</param>
        /// <returns></returns>
        public User GetUserByEmail(String pEmail, bool pReturnProductOwner, bool pReturnScrumMaster)
        {
            if (pReturnProductOwner)
            {
                return userRepository.GetSingle(
                    u => u.Email.Equals(pEmail)
                        && u.IsProductOwner.Equals(pReturnProductOwner), null);
            }
            else
            {
                return userRepository.GetSingle(
                    u => u.Email.Equals(pEmail)
                        && u.IsScrumMaster.Equals(pReturnScrumMaster), null);
            }

        }

        /// <summary>
        /// Returns a user entity based on 'username' which in this case is their email.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public User GetUserByUsername(String username)
        {
            return userRepository.GetSingle(u => u.Email.Equals(username), null);
        }

        /// <summary>
        /// Returns a user entity based on the given user id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User GetUser(int userId)
        {

            return userRepository.GetSingle(u => u.UserId.Equals(userId), null);
        }

        /// <summary>
        /// Author: Andrew Moyes.
        /// Gets a list of users based on the given Skillset parameter and who are developers.
        /// </summary>
        /// <param name="Skillset">Comma-separated list of desired skills (concatenated as one string)</param>
        /// <returns>List of User entities</returns>
        public List<User> FindUsersBySkillset(String Skillset)
        {
            List<User> FoundUsers = new List<User>();
            // Split skills into individual items
            String[] skills = Skillset.Split(',');
            // Trim each skill and find developers who match
            for (var i = 0; i < skills.Length; i++)
            {
                skills[i] = skills[i].Trim();
                IList<User> TempUsers = userRepository.GetList(u => u.IsDeveloper && u.Skillset.Contains(skills[i]));
                foreach (User u in TempUsers)
                {
                    if (!FoundUsers.Contains(u))
                    {
                        FoundUsers.Add(u);
                    }
                }
            }
            return FoundUsers;
        }

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(User user)
        {
            //check does user already exist
            if (GetUserByUsername(user.Email) == null)
            {
                userRepository.Add(user);
            }

        }

        /// <summary>
        /// Update an existing user.
        /// </summary>
        /// <param name="user"></param>
        public void UpdateUser(User user)
        {
            /* Validation and error handling omitted */
            userRepository.Update(user);
        }

        /// <summary>
        /// Remove an existing user.
        /// </summary>
        /// <param name="user"></param>
        public void RemoveUser(User user)
        {
            /* Validation and error handling omitted */
            userRepository.Remove(user);
        }

        /// <summary>
        /// Authenticates a user. Data is sent and stored in plain text. If the user authenticates successfully then their full user entity is returned; otherwise null is returned.
        /// </summary>
        /// <param name="username">as email address</param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User LogInUser(String username, String password)
        {
            User user = GetUserByUsername(username);

            //user exists
            if (user != null)
            {
                //password matches
                if (password.Equals(user.Password))
                {
                    return user;
                }
            }
            //user doesn't exist / password doesn't match
            return null;
        }

        /// <summary>
        /// Returns a list of users who are involved in the given project.
        /// </summary>
        /// <param name="pProjectId"></param>
        /// <returns></returns>
        public IList<User> GetUsersByProjectId(int pProjectId)
        {
            IUserProjectRepository userProjectRepo = new UserProjectRepository();
            IList<UserProject> userProjects = userProjectRepo.GetAll(u => u.ProjectId.Equals(pProjectId));

            IList<User> toReturn = new List<User>();
            foreach (var u in userProjects)
            {
                toReturn.Add(u.user);
            }

            return toReturn;
        }
    }
}
