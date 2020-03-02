using FluentMigrator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Migrator.Base
{
    public class BaseMigration : Migration
    {
        const string SCRIPT_PATH = "sql-scripts";

        protected string GenerateScriptName(string suffix)
        {
            return Path.Combine(AppContext.BaseDirectory, SCRIPT_PATH, $"{GetType().Name}{suffix}.sql");
        }

        public override void Up()
        {
            Execute.Script(GenerateScriptName("_UP"));
        }
        public override void Down()
        {
            Execute.Script(GenerateScriptName("_DOWN"));
        }
    }
}
