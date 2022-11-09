using AutoMapper;
using Beca.BookInfo.API.Controllers;
using Beca.BookInfo.API.Entities;
using Beca.BookInfo.API.Models;
using Beca.BookInfo.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Beca.BookInfo.Tests
{
    public class BookControllerTests
    {
        public int ExistingBookId = 1;

        [Fact]

        public async Task GetBook_GetAction_MustReturnOk()                                                  
        {
            // This unitary tests tries to assert if the method GetBook will provide a Ok Action Result

            // Arrange
                // Creating one class through mocking the desired interface
            var _bookInfoRepositoryMock = new Mock<IBookInfoRepository>();
                //Creating the book
            var bookMock = _bookInfoRepositoryMock
                .Setup(p => p.GetBookAsync(ExistingBookId, false))
                .ReturnsAsync(new Book("Alicia en el país de las maravillas"));
                // Mocking the Automapper which is neccesary for the method BookController
            var mapperMock = new Mock<IMapper>();
            mapperMock
                .Setup(p => p.Map<Book, BookWithoutChaptersDto>(It.IsAny<Book>()))
                .Returns(new BookWithoutChaptersDto());
                // Creating the bookController with the mocked dependencies 
            var bookController = new BooksController(_bookInfoRepositoryMock.Object, mapperMock.Object);

            // Act
                // Calling GetBooks method which belongs to the mocked class bookController. This method will call GetBookAsync method which returns a book of the class Entitites.Book.
                // The father method will return a Ok status and will inmediatly execute the mocked automapping
            var result = bookController.GetBooks(ExistingBookId, false);
                // result should be a OkObjectResult type with 200 status.
            Assert.IsType<OkObjectResult>(result.Result);

        }

        [Fact]

        public async Task GetChaptersOfABook_GetAction_MustReturnOk()
        {
            // Arrange
            var _bookInfoRepositoryMock = new Mock<IBookInfoRepository>();
            //Creating the book
            var bookMock = _bookInfoRepositoryMock
                .Setup(p => p.GetBookAsync(ExistingBookId, true))
                .ReturnsAsync(new Book("Alicia en el país de las maravillas"));
            
            var mapperMock = new Mock<IMapper>();
            mapperMock
                .Setup(p => p.Map<Book, BookDto>(It.IsAny<Book>()))
                .Returns(new BookDto());
            // Creating the bookController with the mocked dependencies 
            var bookController = new BooksController(_bookInfoRepositoryMock.Object, mapperMock.Object);

            // Act
            // Calling GetBooks method which belongs to the mocked class bookController. This method will call GetBookAsync method which returns a book of the class Entitites.Book.
            // The father method will return a Ok status and will inmediatly execute the mocked automapping
            var result = bookController.GetBooks(ExistingBookId, true);
            // result should be a OkObjectResult type with 200 status.
            Assert.IsType<OkObjectResult>(result.Result);

        }


    }
}
