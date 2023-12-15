using ProjectManagementTool.Logic.Services;
using ProjectManagementTool.Models;
using Task = ProjectManagementTool.Models.Task;

namespace UnitTestProject2
{
    public class UnitTestContainer
    {
        private Project testProject = new Project
        {
            guid = Guid.Parse("d4e92bea-72b2-4fc7-bf7a-af732c10dc6f"),
            tasks = new List<Task>(),
            assignees = new List<Employee>(),
            goal = new Goal(),
            description = "Test description.",
            isNew = true,
            title = "TestTitle"
        };

        [Fact]
        public void ValidateCorrectInput()
        {
            // Create starting object
            // no changes to standard object.

            // Create expected outcome
            Project expectedProject = testProject;

            // Run Actual Method
            Project actualProject = ProjectValidator.Validate(testProject);

            // Check if outcome is expected
            Assert.Same(expectedProject, actualProject);
        }

        [Fact]
        public void ValidateNoAssignees()
        {
            // Create starting object
            Project test = testProject;
            test.assignees = new List<Employee>();

            // Create expected outcome
            Project expectedProject = testProject;
            expectedProject.description += "No Assignees Specified. ";
            expectedProject.assignees.Add(new Employee
            {
                email = "admin@adminsEmailService.com",
                guid = Guid.Parse("81cc02a7-d5de-4365-8213-b67affa6f337"),
                name = "Admin The Almighty"
            });

            // Run Actual Method
            Project actualProject = ProjectValidator.Validate(test);

            // Check if outcome is expected
            Assert.Same(expectedProject, actualProject);
        }

        [Fact]
        public void ValidateNoTasks()
        {
            // Create starting object
            Project test = testProject;
            test.tasks = new List<Task>();

            // Create expected outcome
            Project expectedProject = testProject;
            expectedProject.description += "No Tasks Specified. ";

            // Run Actual Method
            Project actualProject = ProjectValidator.Validate(test);

            // Check if outcome is expected
            Assert.Same(expectedProject, actualProject);
        }

        [Fact]
        public void ValidateNoGoal()
        {
            Project test = testProject;
            test.goal = null;

            // Create expected outcome
            Project expectedProject = testProject;
            expectedProject.goal = new Goal
            {
                completionPercentage = 0,
                guid = Guid.Empty,
                projectGuid = expectedProject.guid,
                tasks = null
            };

            // Run Actual Method
            Project actualProject = ProjectValidator.Validate(test);

            // Check if outcome is expected
            Assert.Same(expectedProject, actualProject);
        }

        [Fact]
        public void ValidateNoDescription()
        {
            Project test = testProject;
            test.description = null;

            // Create expected outcome
            Project expectedProject = testProject;
            expectedProject.description += "Description Empty. ";

            // Run Actual Method
            Project actualProject = ProjectValidator.Validate(test);

            // Check if outcome is expected
            Assert.Same(expectedProject, actualProject);
        }

        // There is a redundant test below, this is the case because the method im testing here has a validation check for wether the guid is passed as null
        // this can obviously not happen so the test fails on grounds of guid not being nullable
        [Fact]
        public void ValidateNoGuid()
        {
            Project test = testProject;
            test.guid = null!;

            // Create expected outcome
            Project expectedProject = new Project
            {
                goal = new Goal
                {
                    completionPercentage = 0,
                    guid = Guid.Empty,
                    projectGuid = Guid.Empty,
                    tasks = null
                },
                guid = Guid.Empty,
                description = "This is an invalid project, thus you shouldnt be seeing this.",
                assignees = null,
                tasks = null,
                isNew = false,
                title = "The undefined: Special edition"
            };

            // Run Actual Method
            Project actualProject = ProjectValidator.Validate(test);

            // Check if outcome is expected
            Assert.Same(expectedProject, actualProject);

        }
    }
}

