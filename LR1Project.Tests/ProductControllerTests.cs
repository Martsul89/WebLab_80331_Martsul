using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using LR1Project.Controllers;
using WebLab.DAL.Entities;
using LR1Project.Models;
using Xunit;

namespace LR1Project.Tests
{
    public class ProductControllerTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void ControllerGetsProperPage(int page, int qty, int id)
        {
            // Arrange
            var controller = new ProductController();
            controller._flowers = GetFlowersList();
            // Act
            var result = controller.Index(pageNo: page, group: null) as ViewResult;
            var model = result?.Model as List<Flower>;
            // Assert
            Assert.NotNull(model);
            Assert.Equal(qty, model.Count);
            Assert.Equal(id, model[0].FlowerId);
        }
        /// <summary>
        /// Исходные данные для теста
        /// номер страницы, кол.объектов на выбранной странице и
        /// id первого объекта на странице
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<object[]> Data()
        {
            yield return new object[] { 1, 3, 1 };
            yield return new object[] { 2, 2, 4 };
        }
        /// <summary>
        /// Получение тестового списка объектов
        /// </summary>
        /// <returns></returns>
        private List<Flower> GetFlowersList()
        {
            return new List<Flower>
            {
            new Flower{ FlowerId=1, FlowerGroupId=1},
            new Flower{ FlowerId=2, FlowerGroupId=1},
            new Flower{ FlowerId=3, FlowerGroupId=2},
            new Flower{ FlowerId=4, FlowerGroupId=2},
            new Flower{ FlowerId=5, FlowerGroupId=3}
            };
        }

        [Theory]
        [MemberData(memberName: nameof(Data))]
        public void ListViewModelCountsPages(int page, int qty, int id)
        {
            // Act
            var model = ListViewModel<Flower>.GetModel(GetFlowersList(), page, 3);
            // Assert
            Assert.Equal(2, model.TotalPages);
        }
        [Theory]
        [MemberData(memberName: nameof(Data))]
        public void ListViewModelSelectsCorrectQty(int page, int qty, int id)
        {
            // Act
            var model = ListViewModel<Flower>.GetModel(GetFlowersList(), page, 3);
            // Assert
            Assert.Equal(qty, model.Count);
        }
        [Theory]
        [MemberData(memberName: nameof(Data))]
        public void ListViewModelHasCorrectData(int page, int qty, int id)
        {
            // Act
            var model = ListViewModel<Flower>.GetModel(GetFlowersList(), page, 3);
            // Assert
            Assert.Equal(id, model[0].FlowerId);
        }
        [Theory]
        [MemberData(memberName: nameof(Data))]
        public void ControllerSelectsGroup()
        {
            // arrange
            var controller = new ProductController();
            controller._flowers = GetFlowersList();
            // act
            var result = controller.Index(2) as ViewResult;
            var model = result.Model as List<Flower>;
            // assert
            Assert.Equal(2, model.Count);
            Assert.Equal(GetFlowersList()[1], model[2], Comparer<Flower>.GetComparer((d1, d2) => 
            {
                return d1.FlowerId == d2.FlowerId;
            }));
        }
    }

}
