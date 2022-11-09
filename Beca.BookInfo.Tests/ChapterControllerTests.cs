using AutoMapper;
using Beca.BookInfo.API.Controllers;
using Beca.BookInfo.API.Entities;
using Beca.BookInfo.API.Models;
using Beca.BookInfo.API.Services;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beca.BookInfo.Tests
{
    public class ChapterControllerTests
    {
        public int ExistingBookId = 1;

        [Fact]

        public async Task GetChapter_GetAction_MustReturnOk()
        {
            // Para hacer el mock del controlador tenemos que hacer el mock de las dependencias ILogger, IBookInfoRepository y IMapper
            
            var _bookInfoRepositoryMock = new Mock<IBookInfoRepository>();

            // Hacemos el setup de los metodos internos para infiltrarnos en el escnario deseado

            var existBookMock = _bookInfoRepositoryMock
                .Setup(p => p.BookExistsAsync(ExistingBookId))
                .ReturnsAsync(true);

            var chapterMock = _bookInfoRepositoryMock
                .Setup(p => p.GetChapterForBookAsync(ExistingBookId, 1))
                .ReturnsAsync(new Chapter("El acceso a la madriguera"));      


            var mapperMock = new Mock<IMapper>();
            mapperMock
                .Setup(p => p.Map<Chapter, ChapterDto>(It.IsAny<Chapter>()))
                .Returns(new ChapterDto());

            var loggerMock = new Mock<ILogger<ChaptersController>>();

            loggerMock.Setup(x => x.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()))
                .Callback(new InvocationAction(invocation =>
                {
                    var logLevel = (LogLevel)invocation.Arguments[0];
                    var eventId = (EventId)invocation.Arguments[1];  
                    var state = invocation.Arguments[2];
                    var exception = (Exception)invocation.Arguments[3];
                    var formatter = invocation.Arguments[4];

                    var invokeMethod = formatter.GetType().GetMethod("Invoke");
                    var logMessage = (string)invokeMethod?.Invoke(formatter, new[] { state, exception });
                }));


            var chaptersControllers = new ChaptersController(loggerMock.Object,_bookInfoRepositoryMock.Object, mapperMock.Object);

            // Act
            
            var result = chaptersControllers.GetChapter(ExistingBookId, 1);
            // result is an ActionResult ChapterDto class
            Assert.IsType<ActionResult<ChapterDto>>(result.Result);
            // result should be a OkObjectResult type with 200 status.
            Assert.IsType<OkObjectResult>(result.Result.Result);
           

        }
    }
}
