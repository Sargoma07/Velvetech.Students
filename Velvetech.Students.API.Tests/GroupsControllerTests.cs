using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using Velvetech.Students.API.Controllers;
using Velvetech.Students.Domain.Entities;
using Velvetech.Students.Domain.Model;
using Velvetech.Students.Domain.Repositories;

namespace Velvetech.Students.API.Tests
{
    public class GroupsControllerTests
    {
        public static GroupDto GetGroupDto()
        {
            var group = new Fixture()
                .Build<GroupDto>()
                .Create();

            return group;
        }

        public static IGroupRepository MakeFakeGroupRepository()
        {
            var fake = Substitute.For<IGroupRepository>();
            fake.Find(Arg.Any<long>()).Returns(new Group(1, new Fixture().Create<string>()));
            return fake;
        }

        public static IStudentRepository MakeFakeStudentRepository()
        {
            return Substitute.For<IStudentRepository>();
        }

        public static IMapper MakeFakeMapper()
        {
            return Substitute.For<IMapper>();
        }

        public static GroupsController MakeGroupsController()
        {
            return new GroupsController(MakeFakeGroupRepository(), MakeFakeStudentRepository(), MakeFakeMapper());
        }

        [Test]
        public void GetAll_ValidRequest_CallGetAllWithFilter()
        {
            //Arrange 
            var mockGroupRepository = MakeFakeGroupRepository();
            var controller = new GroupsController(mockGroupRepository, MakeFakeStudentRepository(), MakeFakeMapper());

            //Act 
            controller.GetAll("name");

            //Assert
            mockGroupRepository.Received().GetAll(Arg.Any<Expression<Func<Group, bool>>>());
        }

        [TestCase(null)]
        [TestCase("")]
        public void GetAll_EmptyOrNullRequest_CallGetAll(string name)
        {
            //Arrange 
            var mockGroupRepository = MakeFakeGroupRepository();
            var controller = new GroupsController(mockGroupRepository, MakeFakeStudentRepository(), MakeFakeMapper());

            //Act 
            controller.GetAll(name);

            //Assert 
            mockGroupRepository.Received().GetAll();
        }

        [Test]
        public async Task Create_ValidRequest_ReturnsSuccess()
        {
            //Arrange 
            var controller = MakeGroupsController();

            //Act 
            var result = await controller.Create(GetGroupDto()) as ObjectResult;

            //Assert
            Assert.AreEqual(201, result?.StatusCode);
        }

        [Test]
        public async Task Update_ValidRequest_ReturnsSuccess()
        {
            //Arrange 
            var controller = MakeGroupsController();
            var request = GetGroupDto();

            //Act 
            var result = await controller.Update(request.Id, request) as StatusCodeResult;

            //Assert
            Assert.AreEqual(204, result?.StatusCode);
        }

        // TODO: NullReferenceException падает из-за ProblemDetails  
        // [Test]
        // public async Task Update_DifferentId_ReturnsError()
        // {
        //     //Arrange 
        //     var controller = MakeGroupsController();
        //     var request = GetGroupDto();
        //
        //     //Act 
        //     var result = await controller.Update(10,request) as StatusCodeResult ; //
        //
        //     //Assert
        //     Assert.AreEqual(204,result?.StatusCode);
        // }

        [Test]
        public async Task Delete_ValidRequest_ReturnsSuccess()
        {
            //Arrange 
            var controller = MakeGroupsController();

            //Act 
            var result = await controller.Delete(1) as StatusCodeResult;

            //Assert
            Assert.AreEqual(204, result?.StatusCode);
        }
    }
}