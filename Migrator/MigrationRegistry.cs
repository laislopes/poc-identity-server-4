using System;
using System.Collections.Generic;
using System.Text;

namespace Migrator
{
    using FluentMigrator;
    using global::Migrator.Base;

    namespace Migrator
    {
        [Migration(0)]
        public class Initial_Migration : StartMigration { }

        [Migration(202003031141)]
        public class Mig_202003031141_CreateTableProfiles : BaseMigration { }

        [Migration(202003031145)]
        public class Mig_202003031145_CreateTableEvents : BaseMigration { }

        [Migration(202003031147)]
        public class Mig_202003031147_CreateTableProfiles_Events : BaseMigration { }

    }

}
