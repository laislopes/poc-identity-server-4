using FluentMigrator;
using System;

namespace Migrator.Base
{
    public class StartMigration : Migration
    {
        public override void Down() => Console.WriteLine("Initial migration");
        

        public override void Up() => Console.WriteLine("Initial migration");
        
    }
}
