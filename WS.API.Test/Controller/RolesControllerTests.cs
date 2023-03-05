using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WS.API.Controllers;
using WS.API.DTO.Role;
using WS.API.Extensions;
using WS.API.Models;
using WS.API.Service;
using WS.API.Service.Implements;
using Xunit;

namespace WS.API.Test.Controller
{
    public class RolesControllerTests
    {
        private readonly IRoleService _service;
        private readonly IMapper _mapper;
        private readonly RolesController _controller;
        public RolesControllerTests()
        {
            _service = A.Fake<IRoleService>();
        }

        [Fact]
        public async Task RolesController_GetRoleList()
        {
            //Arrange
            var roleList = new List<RoleResponse>();
            var roles = A.Fake<List<Role>>();
            var roleController = new RolesController(_service);
            //A.CallTo(() => _service.GetRolesAsync()).Returns(Task.FromResult(roles));
            A.CallTo(_service)
                .Where(call => call.Method.Name == "Completed")
                .WithReturnType<Task<List<Role>>>()
                .Returns(roles);
            //Act
            var result = await roleController.GetRolesAsync();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<RoleResponse>>();
        }

        [Fact]
        public async Task RolesControllerTest_CreateRole()
        {
            //Arrange
            var request = A.Fake<RoleRequest>();
            request.NameRole = "Vi";
                
            var roleController = new RolesController(_service);
            //A.CallTo(() => _service.GetRolesAsync()).Returns(Task.FromResult(roles));
          
            //Act
            var result = await roleController.CreateRole(request);

            //Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<OkObjectResult>();
        }
    }
}
