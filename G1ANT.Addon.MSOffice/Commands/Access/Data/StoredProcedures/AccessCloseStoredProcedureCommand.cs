/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.MSOffice
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/

using G1ANT.Language;

namespace G1ANT.Addon.MSOffice.Commands.Access.Data.StoredProcedures
{
    [Command(Name = "access.closestoredprocedure", Tooltip = "This command closes and optionally saves an Access Stored Procedure")]
    public class AccesCloseStoredProcedureCommand : Command
    {
        public class Arguments : CommandArguments
        {
            [Argument(Tooltip = "Name of the stored procedure to close", Required = true)]
            public TextStructure Name { get; set; }

            [Argument(Tooltip = "Set to true to save changes, false to discard them, don't set any value to prompt user")]
            public BooleanStructure SaveChanges { get; set; }
        }

        public AccesCloseStoredProcedureCommand(AbstractScripter scripter) : base(scripter)
        { }

        public void Execute(Arguments arguments)
        {
            AccessManager.CurrentAccess.Close(Microsoft.Office.Interop.Access.AcObjectType.acStoredProcedure, arguments.Name.Value, arguments.SaveChanges?.Value);
        }
    }
}