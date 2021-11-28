using FluentAssertions;
using System;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Application.Features.Query;
using TodoList.Application.Query;
using TodoList.IntegrationTests;
using Xunit;

namespace TodoList.Application.Command;

public class GetTodoItemByIdQueryTest : IClassFixture<BaseTestFixture>
{
    private readonly BaseTestFixture _fixture;

    public GetTodoItemByIdQueryTest(BaseTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task ShouldReturnAllExistingTodoItems()
    {
        // Arrange 
        var command = new CreateTodoItemCommand
        {
            Description = Guid.NewGuid().ToString(),
        };
        var todoItem = await _fixture.SendAsync(command);

        // Act
        var result = await _fixture.SendAsync(new GetTodoItemByIdQuery() { Id = todoItem.Id});

        // Assert
        result.Id.Should().Be(todoItem.Id);
        result.Description.Should().Be(todoItem.Description);
        result.IsCompleted.Should().Be(todoItem.IsCompleted);
    }
}