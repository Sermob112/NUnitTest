using ClassLibraryForTests;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace NunitTests
{
    public class TaskManagementTests
    {
        [Test]
        public void Employee_CanOnlyAssignTasksToSelf()
        {
            
            var taskManager = new TaskManager();

            var employee = new Employee { Name = "Alice", Role = EmployeeRole.Employee };
            var manager = new Employee { Name = "Bob", Role = EmployeeRole.Manager };
            var task = new ClassLibraryForTests.Task
            {
                Description = "Call client",
                Priority = TaskPriority.Medium,
                ContactPerson = new Contact { Name = "John Doe", Phone = "+123456789" },
                CreatedDate = DateTime.Now
            };

            
            Assert.DoesNotThrow(() => taskManager.AssignTask(task, employee, employee));

       
            Assert.Throws<InvalidOperationException>(() => taskManager.AssignTask(task, employee, manager));

            
            Assert.DoesNotThrow(() => taskManager.AssignTask(task, manager, manager));
            Assert.DoesNotThrow(() => taskManager.AssignTask(task, manager, employee));
        }

        [Test]
        public void CompletedTask_CannotBeModified()
        {
            // Arrange
            var taskManager = new TaskManager();
            var employee = new Employee { Name = "Alice", Role = EmployeeRole.Employee };
            var task = new ClassLibraryForTests.Task
            {
                Description = "Send email",
                Priority = TaskPriority.High,
                CreatedDate = DateTime.Now,
                ContactPerson = new Contact { Name = "Jane Smith", Phone = "+987654321" }
            };

           
            taskManager.AssignTask(task, employee, employee);
            taskManager.MarkTaskAsCompleted(task, employee);

            
            Assert.Throws<InvalidOperationException>(() => taskManager.UpdateTask(task, t => t.Description = "Updated email"));
        }

        [Test]
        public void Task_IsDeletedAfterOneYearOfCompletion()
        {
            
            var taskManager = new TaskManager();
            var employee = new Employee { Name = "Alice", Role = EmployeeRole.Employee };
            var task = new ClassLibraryForTests.Task
            {
                Description = "Setup meeting",
                Priority = TaskPriority.Low,
                CreatedDate = DateTime.Now,
                CompletedDate = DateTime.Now.AddYears(-1).AddSeconds(-1),
                ContactPerson = new Contact { Name = "Jane Smith", Phone = "+987654321" }
            };  

            taskManager.AssignTask(task, employee, employee);
            taskManager.MarkTaskAsCompleted(task, employee);

            
            taskManager.RemoveExpiredTasks();

            
            Assert.That(taskManager.Tasks, Does.Not.Contain(task), "Tasks older than 12 months should be removed.");
        }
    }
}
