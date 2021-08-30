using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EUROMONITOR.Model.Configuration
{
    // Seed
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData
            (
                new Book
                {
                    BookId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    Name = "An Introduction to Design Patterns in C++ with Qt 5",
                    Text = "This book is designed to teach design pattern",
                    Price = 120
                },
                new Book
                {
                    BookId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                    Name = "Pro ASP.NET Core MVC",
                    Text = "Best seller book to learn ASP.Net Core from scratch",
                    Price = 90
                },
                new Book
                {
                    BookId = new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"),
                    Name = "Applying Domain Driven Design and Patterns with Examples in C# and .NET",
                    Text = "Learn DDD from Beginer to Pro",
                    Price = 240
                }
            );
        }
    }
}
