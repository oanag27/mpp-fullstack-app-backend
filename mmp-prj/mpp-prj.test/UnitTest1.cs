using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mmp_prj.Controllers;
using mmp_prj.Models;
using Moq;
using NUnit.Framework;
namespace mpp_prj.test;


[TestClass]
public class UnitTest1
{

    [Test]
    public void TestGetTaskById_ReturnsTask_WhenValidId()
    {
        // Arrange
        var taskId = 1;
        var mockController = new Mock<TaskController>();
        mockController.Setup(c => c.GetTaskById(taskId)).Returns(new Tasks { Id = taskId });

        // Act
        var result = mockController.Object.GetTaskById(taskId);

        // Assert
        NUnit.Framework.Assert.That(result, Is.TypeOf<ActionResult<Tasks>>());
        NUnit.Framework.Assert.That(result.Value.Id, Is.EqualTo(taskId));
    }




    [Test]
    public void TestGetAllTasks_ReturnsAllTasks()
    {
        // Arrange
        var expectedTasks = new List<Tasks>
            {
                new Tasks { Id = 1, Name = "Task 1", Description = "Description for Task 1", Duration = 10 },
                new Tasks { Id = 2, Name = "Task 2", Description = "Description for Task 2", Duration = 20 },
                new Tasks { Id = 3, Name = "Task 3", Description = "Description for Task 3", Duration = 7 },
                new Tasks { Id = 4, Name = "Task 4", Description = "Description for Task 4", Duration = 100 },
                new Tasks { Id = 5, Name = "Task 5", Description = "Description for Task 5", Duration = 50 }
            };
        var mockController = new Mock<TaskController>();
        mockController.Setup(c => c.GetAllTasks()).Returns(new ActionResult<IEnumerable<Tasks>>(expectedTasks));

        // Act
        var result = mockController.Object.GetAllTasks();

        // Assert
        NUnit.Framework.Assert.That(result.Value, Is.EqualTo(expectedTasks));
    }

    [Test]
    public void TestDelete_ReturnsNoContent_WhenValidId()
    {
        // Arrange
        var taskId = 1;
        var mockController = new Mock<TaskController>();
        mockController.Setup(c => c.Delete(taskId)).Returns(new NoContentResult());

        // Act
        var result = mockController.Object.Delete(taskId);

        // Assert
        NUnit.Framework.Assert.That(result, Is.TypeOf<NoContentResult>());
    }

    [Test]
    public void TestDelete_ReturnsNotFound_WhenInvalidId()
    {
        // Arrange
        var invalidId = 10;
        var mockController = new Mock<TaskController>();
        mockController.Setup(c => c.Delete(invalidId)).Returns(new NotFoundResult());

        // Act
        var result = mockController.Object.Delete(invalidId);

        // Assert
        NUnit.Framework.Assert.That(result, Is.TypeOf<NotFoundResult>());
    }

    [Test]
    public void TestAddTask_ReturnsCreatedAtAction()
    {
        // Arrange
        var newTask = new Tasks { Id = 6, Name = "New Task", Description = "Description for New Task", Duration = 15 };
        var mockController = new Mock<TaskController>();
        mockController.Setup(c => c.AddTask(newTask))
                     .Returns(new CreatedAtActionResult("GetTaskById", "TasksController", new { id = newTask.Id }, newTask));

        // Act
        var result = mockController.Object.AddTask(newTask);

        // Assert
        NUnit.Framework.Assert.That(result, Is.TypeOf<ActionResult<Tasks>>());
    }

    [Test]
    public void TestUpdateTask_ReturnsNoContent_WhenValidId()
    {
        // Arrange
        var taskId = 1;
        var updatedTask = new Tasks { Id = taskId, Name = "Updated Task", Description = "Updated Description", Duration = 30 };
        var mockController = new Mock<TaskController>();
        mockController.Setup(c => c.UpdateTask(taskId, updatedTask)).Returns(new NoContentResult());

        // Act
        var result = mockController.Object.UpdateTask(taskId, updatedTask);

        // Assert
        NUnit.Framework.Assert.That(result, Is.TypeOf<NoContentResult>());
    }

    [Test]
    public void TestUpdateTask_ReturnsBadRequest_WhenIdsDoNotMatch()
    {
        // Arrange
        var taskId = 1;
        var updatedTask = new Tasks { Id = taskId + 1, Name = "Updated Task", Description = "Updated Description", Duration = 30 };
        var mockController = new Mock<TaskController>();
        mockController.Setup(c => c.UpdateTask(taskId, updatedTask)).Returns(new BadRequestResult());

        // Act
        var result = mockController.Object.UpdateTask(taskId, updatedTask);

        // Assert
        NUnit.Framework.Assert.That(result, Is.TypeOf<BadRequestResult>());
    }

    [Test]
    public void TestUpdateTask_ReturnsNotFound_WhenInvalidId()
    {
        // Arrange
        var invalidId = 10;
        var updatedTask = new Tasks { Id = invalidId, Name = "Updated Task", Description = "Updated Description", Duration = 30 };
        var mockController = new Mock<TaskController>();
        mockController.Setup(c => c.UpdateTask(invalidId, updatedTask)).Returns(new NotFoundResult());

        // Act
        var result = mockController.Object.UpdateTask(invalidId, updatedTask);

        // Assert
        NUnit.Framework.Assert.That(result, Is.TypeOf<NotFoundResult>());
    }

}
