using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchMvc.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO Products (Name,Description,Price,Stock,Image,CategoryId) " +
            "VALUES('Caderno espiral','Caderno espiral 100 folhas',7.45,5,'caderno1.jpg',1)");

            mb.Sql("INSERT INTO Products (Name,Description,Price,Stock,Image,CategoryId) " +
            "VALUES('Caderno espiral2','Caderno espiral2 100 folhas',7.45,5,'caderno2.jpg',1)");

            mb.Sql("INSERT INTO Products (Name,Description,Price,Stock,Image,CategoryId) " +
            "VALUES('Caderno espiral3','Caderno espiral3 100 folhas',7.45,5,'caderno3.jpg',1)");

            mb.Sql("INSERT INTO Products (Name,Description,Price,Stock,Image,CategoryId) " +
            "VALUES('Caderno espiral4','Caderno espiral4 100 folhas',7.45,5,'caderno4.jpg',1)");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM Products");
        }
    }
}
