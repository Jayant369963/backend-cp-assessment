using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TodoList.IntegrationTests;
using Xunit;

namespace TodoList.Application.Command;

public class CreateTodoItemCommandTest : IClassFixture<BaseTestFixture>
{
    private readonly BaseTestFixture _fixture;

    public CreateTodoItemCommandTest(BaseTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task ShouldCreateNewTodoItem()
    {
        // Arrange 
        var command = new CreateTodoItemCommand
        {
            Description = Guid.NewGuid().ToString(),
        };

        // Act
        var result = await _fixture.SendAsync(command);

        // Assert
        Assert.Equal(result.Description, command.Description);
    }


    [Fact]
    public async Task ShouldNotCreateTodoItemWhenSameTodoDescriptionExists()
    {
        // Arrange 
        var command = new CreateTodoItemCommand
        {
            Description = "This is test todo"
        };

        // Act
        var result = await _fixture.SendAsync(command);

        // Assert
        await FluentActions.Invoking(() => _fixture.SendAsync(command)).Should().ThrowAsync<InvalidOperationException>();
    }
}