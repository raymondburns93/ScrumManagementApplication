using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrumManagementApp.DAL.Repository;
using ScrumManagementApp.EntityModels.Models;

namespace ScrumManagementApp.Business
{
    /// <summary>
    /// Author: Raymond Burns
    /// Description: Business logic for Product Backlog. Interacts with DAL and performs any required server side validation
    /// </summary>
    public class ProductBacklogLogic
    {

        private IProductBacklogRepository _productBacklogRepository;
        private IUserStoryRepository _storyRepository;
       // private AssignedUserStoryRepository _assignedStoryRepository;

        /// <summary>
        /// Constructor. Instantiates product backlog and user story repositories.
        /// </summary>
        public ProductBacklogLogic()
        {
            _productBacklogRepository = new ProductBacklogRepository();
            _storyRepository = new UserStoryRepository();
            //_assignedStoryRepository = new AssignedUserStoryRepository();
        }

        //lmc added for unit tests
        public ProductBacklogLogic(IProductBacklogRepository productBacklogRepository)
        {
            this._productBacklogRepository = productBacklogRepository;
        }

        //lmc added for unit tests
        public ProductBacklogLogic(IUserStoryRepository storyRepository)
        {
            this._storyRepository = storyRepository;
        }

        /// <summary>
        /// Returns the product backlog for the given project 
        /// </summary>
        /// <param name="project_Id">Primary key of project</param>
        /// <returns></returns>
        public ProductBacklog GetProductBacklog(int project_Id)
        {
            return _productBacklogRepository.GetSingle(b => b.ProjectId == project_Id, us => us.Stories);
           
        }

        /// <summary>
        /// Returns a list of UserStory entities based on the projectId provided
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public IList<UserStory> GetProductBacklogUserStories(int projectId)
        {
            return _storyRepository.GetList(us => us.productBacklog.ProjectId == projectId,p => p.productBacklog);
        }

        /*
        /// <summary>
        /// Assigns a user story to the product backlog for the given project.
        /// User story MUST already exist in the database.
        /// </summary>
        /// <param name="project_Id">Primary key of project</param>
        /// <param name="story">UserStory object</param>
        public void AssignToBacklog(int project_Id , UserStory story)
        {
            // Get product backlog for project
           // Backlog backlog = this.GetProductBacklog(project_Id);

            //if(backlog != null && story.UserStoryID > 0)
            {
                // Data seems correct. Assign to backlog via AssignedUserStory table
                AssignedUserStory assignment = new AssignedUserStory();
                //assignment.BacklogID = backlog.BacklogID;
                assignment.UserStoryID = story.UserStoryID;
               // _assignedStoryRepository.Add(assignment);
            }


            //Backlog backlog = this.GetProductBacklog(project_Id);
            //userstoryrepository.add(story)


        }
        */

        /// <summary>
        /// Assigns a user story to a product backlog based in the provided project id.
        /// </summary>
        /// <param name="project_Id"></param>
        /// <param name="story"></param>
        public void AddUserStory(int project_Id, UserStory story)
        {
            ProductBacklog productBacklog = this.GetProductBacklog(project_Id);
            story.ProductBacklogId = productBacklog.ProductBacklogId;
            _storyRepository.Add(story);
        }

        /// <summary>
        /// Removes a user story (de-assigns) from a product backlog.
        /// </summary>
        /// <param name="project_Id"></param>
        /// <param name="story_Id"></param>
        public void DeleteUserStory(int project_Id, int story_Id)
        {
         
            UserStory userStory = _storyRepository.GetSingle(us => us.UserStoryID == story_Id,null);

            _storyRepository.Remove(userStory);

        }

        /// <summary>
        /// Updates a user story contained within a project.
        /// </summary>
        /// <param name="project_Id"></param>
        /// <param name="story"></param>
        public void EditUserStory(int project_Id,UserStory story)
        {

            _storyRepository.Update(story);

        }

        /// <summary>
        /// Update the 'priority' property of a user story. Does not check for duplicate priorities.
        /// </summary>
        /// <param name="project_Id"></param>
        /// <param name="story_Id"></param>
        /// <param name="newPriority"></param>
        public void ReprioritiseUserStory(int project_Id, int story_Id, int newPriority)
        {
            UserStory userStory = _storyRepository.GetSingle(us => us.UserStoryID == story_Id);
             userStory.Priority = newPriority;
            _storyRepository.Update(userStory);
        }


    }
}
