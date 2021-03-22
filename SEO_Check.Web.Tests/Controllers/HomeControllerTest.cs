using AutoMapper;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using SEO_Check.Domain;
using SEO_Check.Persistance;
using SEO_Check.Web.Controllers;
using SEO_Check.Web.Infrastructure;
using SEO_Check.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SEO_Check.Web.Tests.Controllers
{
	public class HomeControllerTest
	{
		private ILogger<HomeController> _logger;
		private IUnitOfWork _unitOfWork;
		private IMapper _mapper;
		private IMemoryCache _cache;
		SearchEngineIndexModel model;
		private DbContextOptions<ApplicationDbContext> _options;
		private IConfigurationRoot _configuration;
		private string[] selectedIds;
		[Test]
		public void GivenBasicInputs_GetIndexModel_SearchEngineListIsNotNull()
		{
			//Arrange
			HomeController sut = new HomeController(_logger, _unitOfWork, _mapper, _cache);
			//Act
			model = sut.GetIndexModel();
			//Assert
			Assert.NotNull(model.listSearchEngines);
		}

		[Test]
		public void GivenBasicInputs_GetIndexModel_SearchEngineListContainsGoogle()
		{
			//Arrange
			HomeController sut = new HomeController(_logger, _unitOfWork, _mapper, _cache);
			//Act
			model = sut.GetIndexModel();
			//Assert
			Assert.That(model.listSearchEngines.Select(g => g.SearchEngineName).Contains("Google"));
		}

		[Test]
		public void GivenBasicInputs_CheckSeoResults_ThrowNullRefException()
		{
			//Arrange
			HomeController sut = new HomeController(_logger, _unitOfWork, _mapper, _cache);
			var model = sut.GetIndexModel();
			selectedIds = new string[1];
			selectedIds[0] = model.listSearchEngines[0].Id.ToString();
			model.searchParams = null;
			//Act

			//Assert
			var ex = Assert.Throws<NullReferenceException>(() => sut.CheckSeoResults(selectedIds, model));

			Assert.That(ex.Message, Is.EqualTo("Object reference not set to an instance of an object."));

		}

		[Test]
		public void GivenBasicInputs_CheckSeoResults_DoesNotThrowNullRefException()
		{
			//Arrange
			HomeController sut = new HomeController(_logger, _unitOfWork, _mapper, _cache);
			var model = sut.GetIndexModel();
			selectedIds = new string[1];
			selectedIds[0] = model.listSearchEngines[0].Id.ToString();

			model.searchParams = new SearchParamsModel();
			model.searchParams.Keyword = "e-settlement";
			model.searchParams.TargetUrl = "https://www.sympli.com.au";
			//Act

			//Assert
			Assert.DoesNotThrow(() => sut.CheckSeoResults(selectedIds, model));

		}

		[SetUp]
		public void Setup()
		{
			_logger = A.Fake<ILogger<HomeController>>();
			
			_cache = A.Fake<IMemoryCache>();

			var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

			_configuration = builder.Build();

			_options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(_configuration.GetConnectionString("DefaultConnection")).Options;

			_unitOfWork = new UnitOfWork(_options);

			var mappingConfig = new MapperConfiguration(mc =>
			{
				mc.AddProfile(new AutoMapperConfiguration());
			});
			IMapper mapper = mappingConfig.CreateMapper();
			_mapper = mapper;

		}
	}
}
