namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulatingGenre : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (Name) VALUES ('Action'), ('Comedy'), ('Drama'), ('Adventure'), ('Fantasy')");
        }
        
        public override void Down()
        {
        }
    }
}
