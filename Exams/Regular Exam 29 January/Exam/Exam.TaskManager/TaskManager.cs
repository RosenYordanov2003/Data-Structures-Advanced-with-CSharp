using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.TaskManager
{
    public class TaskManager : ITaskManager
    {
        private readonly LinkedList<Task> pendingTasks;
        private readonly Dictionary<string, Task> allTasks;
        private readonly HashSet<Task> executedTasks;

        public TaskManager()
        {
            pendingTasks = new LinkedList<Task>();
            allTasks = new Dictionary<string, Task>();
            executedTasks = new HashSet<Task>();
        }
        public void AddTask(Task task)
        {
            pendingTasks.AddLast(task);
            allTasks.Add(task.Id, task);
        }

        public bool Contains(Task task)
        {
            return allTasks.ContainsKey(task.Id);
        }

        public void DeleteTask(string taskId)
        {
            if (!allTasks.ContainsKey(taskId))
            {
                throw new ArgumentException();
            }
            Task taskToRemove = allTasks[taskId];
            allTasks.Remove(taskId);
            if (executedTasks.Contains(taskToRemove))
            {
                executedTasks.Remove(taskToRemove);
            }
            else
            {
                pendingTasks.Remove(taskToRemove);
            }
        }

        public Task ExecuteTask()
        {
            if (pendingTasks.Count == 0)
            {
                throw new ArgumentException();
            }
            Task task = pendingTasks.First.Value;
            pendingTasks.RemoveFirst();

            executedTasks.Add(task);

            return task;
        }

        public IEnumerable<Task> GetAllTasksOrderedByEETThenByName()
        {
            return allTasks.Select(kvp => kvp.Value).OrderByDescending(t => t.EstimatedExecutionTime).ThenBy(t => t.Name);
        }

        public IEnumerable<Task> GetDomainTasks(string domain)
        {
            IEnumerable<Task> tasks = pendingTasks.Where(t => t.Domain == domain);

            if (tasks.Count() == 0)
            {
                throw new ArgumentException();
            }
            return tasks;
        }

        public Task GetTask(string taskId)
        {
            if (!allTasks.ContainsKey(taskId))
            {
                throw new ArgumentException();
            }
            return allTasks[taskId];
        }

        public IEnumerable<Task> GetTasksInEETRange(int lowerBound, int upperBound)
        {
            return pendingTasks.Where(t => t.EstimatedExecutionTime >= lowerBound && t.EstimatedExecutionTime <= upperBound);
        }

        public void RescheduleTask(string taskId)
        {
            if (!allTasks.ContainsKey(taskId))
            {
                throw new ArgumentException();
            }
            Task task = allTasks[taskId];
            executedTasks.Remove(task);
            pendingTasks.AddLast(task);
        }

        public int Size() => allTasks.Count;
    }
}
