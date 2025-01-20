using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NunitTest
{
    public class TaskReportingTests
    {
        [Test]
        public void SearchClients_ByAttributes_ReturnsCorrectResults()
        {
            // Arrange
            var clientManager = new ClientManager();
            clientManager.AddClient(new Client { Name = "Tech Corp", City = "New York", ContactName = "John Doe" });
            clientManager.AddClient(new Client { Name = "Health Inc", City = "Los Angeles", ContactName = "Jane Smith" });
            clientManager.AddClient(new Client { Name = "EduTech", City = "New York", ContactName = "Emily Johnson" });

            // Act
            var resultsByName = clientManager.SearchClients("Tech");
            var resultsByCity = clientManager.SearchClients(city: "New York");
            var resultsByContactName = clientManager.SearchClients(contactName: "Jane");

            // Assert
            Assert.That(resultsByName.Count, Is.EqualTo(2));
            Assert.That(resultsByCity.Count, Is.EqualTo(2));
            Assert.That(resultsByContactName.Count, Is.EqualTo(1));
            Assert.That(resultsByContactName.First().Name, Is.EqualTo("Health Inc"));
        }

        [Test]
        public void GenerateReport_ReturnsCorrectTaskStatistics()
        {
            // Arrange
            var taskManager = new TaskManager();
            var employee = new Employee { Name = "Alice", Role = EmployeeRole.Employee };

            taskManager.AddTask(new Task
            {
                Assignee = employee,
                CreatedDate = DateTime.Now.AddDays(-10),
                DueDate = DateTime.Now.AddDays(-5),
                CompletedDate = DateTime.Now.AddDays(-6) // Completed on time
            });

            taskManager.AddTask(new Task
            {
                Assignee = employee,
                CreatedDate = DateTime.Now.AddDays(-20),
                DueDate = DateTime.Now.AddDays(-10),
                CompletedDate = DateTime.Now.AddDays(-8) // Completed late
            });

            taskManager.AddTask(new Task
            {
                Assignee = employee,
                CreatedDate = DateTime.Now.AddDays(-15),
                DueDate = DateTime.Now.AddDays(-5),
                CompletedDate = null // Overdue and not completed
            });

            taskManager.AddTask(new Task
            {
                Assignee = employee,
                CreatedDate = DateTime.Now.AddDays(-5),
                DueDate = DateTime.Now.AddDays(5),
                CompletedDate = null // Not overdue, not completed
            });

            // Act
            var report = taskManager.GenerateReport(employee, DateTime.Now.AddDays(-30), DateTime.Now);

            // Assert
            Assert.That(report.TotalTasks, Is.EqualTo(4));
            Assert.That(report.CompletedOnTime, Is.EqualTo(1));
            Assert.That(report.CompletedLate, Is.EqualTo(1));
            Assert.That(report.OverdueNotCompleted, Is.EqualTo(1));
            Assert.That(report.NotOverdueNotCompleted, Is.EqualTo(1));
        }
    }

}
