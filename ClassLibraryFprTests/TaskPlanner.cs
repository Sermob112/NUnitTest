using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryForTests
{
    public class TaskManager
    {
        public List<Task> Tasks { get; } = new();
        public void AddTask(Task task) => Tasks.Add(task);
        public void AssignTask(Task task, Employee author, Employee assignee)
        {
            if (author.Role == EmployeeRole.Employee && author != assignee)
            {
                throw new InvalidOperationException("Employees can only assign tasks to themselves.");
            }

            task.Author = author;
            task.Assignee = assignee;
            Tasks.Add(task);
        }

        public void MarkTaskAsCompleted(Task task, Employee actor)
        {
            if (actor != task.Author && actor != task.Assignee)
            {
                throw new InvalidOperationException("Only the author or assignee can mark a task as completed.");
            }

            task.CompletedDate =  DateTime.Now.AddYears(-1);
        }

        public void UpdateTask(Task task, Action<Task> updateAction)
        {
            if (task.CompletedDate != null)
            {
                throw new InvalidOperationException("Completed tasks cannot be modified.");
            }

            updateAction(task);
        }
        public TaskReport GenerateReport(Employee employee, DateTime startDate, DateTime endDate)
        {
            var tasksForEmployee = Tasks.Where(t => t.Assignee == employee && t.CreatedDate >= startDate && t.CreatedDate <= endDate);

            var completedOnTime = tasksForEmployee.Count(t => t.CompletedDate != null && t.CompletedDate <= t.DueDate);
            var completedLate = tasksForEmployee.Count(t => t.CompletedDate != null && t.CompletedDate > t.DueDate);
            var overdueNotCompleted = tasksForEmployee.Count(t => t.CompletedDate == null && t.DueDate < DateTime.Now);
            var notOverdueNotCompleted = tasksForEmployee.Count(t => t.CompletedDate == null && t.DueDate >= DateTime.Now);

            return new TaskReport
            {
                TotalTasks = tasksForEmployee.Count(),
                CompletedOnTime = completedOnTime,
                CompletedLate = completedLate,
                OverdueNotCompleted = overdueNotCompleted,
                NotOverdueNotCompleted = notOverdueNotCompleted
            };
        }
        public void RemoveExpiredTasks()
        {
            var expirationDate = DateTime.Now.AddMonths(-12);
            Console.WriteLine($"Expiration date: {expirationDate}");

            foreach (var t in Tasks)
            {
                Console.WriteLine($"Checking task: {t.Description}, CompletedDate: {t.CompletedDate}");
            }

            Tasks.RemoveAll(t => t.CompletedDate <= expirationDate);

            Console.WriteLine($"Remaining tasks count: {Tasks.Count}");
        }
    }


    public class TaskReport
    {
        public int TotalTasks { get; set; }
        public int CompletedOnTime { get; set; }
        public int CompletedLate { get; set; }
        public int OverdueNotCompleted { get; set; }
        public int NotOverdueNotCompleted { get; set; }
    }
    public class Contact
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    public class Employee
    {
        public string Name { get; set; }
        public EmployeeRole Role { get; set; }
    }

    public enum EmployeeRole
    {
        Employee,
        Manager
    }

    public enum TaskPriority
    {
        Low,
        Medium,
        High
    }
}
