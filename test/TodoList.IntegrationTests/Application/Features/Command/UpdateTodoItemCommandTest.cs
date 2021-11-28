using FluentAssertions;
using System;
using System.Threading.Tasks;
using TodoList.IntegrationTests;
using Xunit;

namespace TodoList.Application.Command;

public class UpdateTodoItemCommandTest : IClassFixture<BaseTestFixture>
{
    private readonly BaseTestFixture _fixture;

    public UpdateTodoItemCommandTest(BaseTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task ShouldUpdateAnExistingTodoItem()
    {
        // Arrange 
        var command = new CreateTodoItemCommand
        {
            Description = Guid.NewGuid().ToString(),
        };
        var todoItem = await _fixture.SendAsync(command);

        var updateCommand = new UpdateTodoItemCommand
        {
            Id = todoItem.Id,
            Description = Guid.NewGuid().ToString(),
            IsCompleted = !todoItem.IsCompleted
        };
        
        // Act
        var result = await _fixture.SendAsync(updateCommand);

        // Assert
        result.Should().BeTrue();
    }


    [Fact]
    public async Task ShouldNotUpdateTodoItemWhenTodoItemDoesnotExist()
    {
        // Arrange 
        var command = new UpdateTodoItemCommand()
        {
            Id = Guid.NewGuid(),
            Description = "This is test todo item",
            IsCompleted = false
        };

        // Act
        var result = await _fixture.SendAsync(command);

        // Assert
        result.Should().BeFalse();
    }
}