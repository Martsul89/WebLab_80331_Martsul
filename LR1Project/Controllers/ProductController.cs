using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebLab.DAL.Entities;
using LR1Project.Models;
using LR1Project.Extensions;

namespace LR1Project.Controllers
{
    public class ProductController : Controller
    {
        public List<Flower> _flowers;
        List<FlowerGroup> _flowerGroups;
        int _pageSize;

        public ProductController()
        {
            _pageSize = 3;
            SetupData();
        }
        [Route("Catalog")]
        [Route("Catalog/Page_{pageNo}")]
        public IActionResult Index(int? group, int pageNo = 1)
        {
            var flowersFiltered = _flowers
            .Where(d => !group.HasValue || d.FlowerGroupId == group.Value);
            // Поместить список групп во ViewData
            ViewData["Groups"] = _flowerGroups;
            // Получить id текущей группы и поместить в TempData
            var currentGroup = group.HasValue
            ? group.Value
            : 0;
            ViewData["CurrentGroup"] = currentGroup;
            //if (Request.Headers["x-requested-with"] == "XMLHttpRequest")
            if (Request.IsAjaxRequest())
                return PartialView("_ListPartial", ListViewModel<Flower>.GetModel(flowersFiltered, pageNo, _pageSize));
            return View(ListViewModel<Flower>.GetModel(flowersFiltered,
            pageNo, _pageSize));
        }

        /// <summary>
        /// Инициализация списков
        /// </summary>
        private void SetupData()
        {
            _flowerGroups = new List<FlowerGroup>
                {
                new FlowerGroup {FlowerGroupId=1, GroupName="Амариллисовые"},
                new FlowerGroup {FlowerGroupId=2, GroupName="Астровые"},
                new FlowerGroup {FlowerGroupId=3, GroupName="Ирисовые"},
                new FlowerGroup {FlowerGroupId=4, GroupName="Лилейные"},
                new FlowerGroup {FlowerGroupId=5, GroupName="Лютиковые"},
                new FlowerGroup {FlowerGroupId=6, GroupName="Пионовые"},
                new FlowerGroup {FlowerGroupId=7, GroupName="Орхидные"},
                new FlowerGroup {FlowerGroupId=8, GroupName="Яснотковые"},
                };
            _flowers = new List<Flower>
            {
                new Flower { FlowerId = 1, FlowerName = "Амариллис", FlowerDescription = "Цветущее луковичное растение. Место происхождения: Южная Африка", FlowerPrice = 35, FlowerGroupId = 1, FlowerImage = "amaryllis.jpg" },
                new Flower { FlowerId = 2, FlowerName = "Анемон", FlowerDescription = "маковидные и ромашковидные цветы. Место происхождения: Южная Европа, Центральная Азия", FlowerPrice = 18, FlowerGroupId = 5, FlowerImage = "anemone.jpg" },
                new Flower { FlowerId = 3, FlowerName = "Крокус (Шафран)", FlowerDescription = "Место происхождения: Индия", FlowerPrice = 25, FlowerGroupId = 3, FlowerImage = "Crocus.jpg" },
                new Flower { FlowerId = 4, FlowerName = "Лаванда", FlowerDescription = "Место происхождения: Средиземноморье", FlowerPrice = 18, FlowerGroupId = 8, FlowerImage = "Lavandula.jpg" },
                new Flower { FlowerId = 5, FlowerName = "Нарцисс", FlowerDescription = "Место происхождения: Центральная и Западная Европа", FlowerPrice = 19, FlowerGroupId = 1, FlowerImage = "narcissus.jpg" },
                new Flower { FlowerId = 6, FlowerName = "Орхидея Цимбидиум", FlowerDescription = "Место происхождения: Юго-Восточная Азия", FlowerPrice = 38, FlowerGroupId = 7, FlowerImage = "Cymbidium.jpg" },
                new Flower { FlowerId = 7, FlowerName = "Пион", FlowerDescription = "Место происхождения: Сибирь, Западный Китай, Монголия", FlowerPrice = 34, FlowerGroupId = 6, FlowerImage = "Paeonia.jpg" },
                new Flower { FlowerId = 8, FlowerName = "Тюльпан", FlowerDescription = "Многолетние луковичные растения. Место происхождения: Казахстан", FlowerPrice = 12, FlowerGroupId = 4, FlowerImage = "Tulipa.jpg" },
                new Flower { FlowerId = 9, FlowerName = "Хризантема кустовая", FlowerDescription = "Место происхождения: Азия", FlowerPrice = 15, FlowerGroupId = 2, FlowerImage = "Chrysanthemum.jpg" }
            };
        }
    }
}
