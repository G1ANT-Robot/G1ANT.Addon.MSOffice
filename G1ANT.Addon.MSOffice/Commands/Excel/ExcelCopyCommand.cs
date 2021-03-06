﻿/**
*    Copyright(C) G1ANT Ltd, All rights reserved
*    Solution G1ANT.Addon, Project G1ANT.Addon.MSOffice
*    www.g1ant.com
*
*    Licensed under the G1ANT license.
*    See License.txt file in the project root for full license information.
*
*/

using G1ANT.Language;
using System;

namespace G1ANT.Addon.MSOffice
{
    [Command(Name = "excel.copy", Tooltip = "This command copies content of the currently selected cells to the clipboard")]
    public class ExcelCopyCommand : Command
    {
        public ExcelCopyCommand(AbstractScripter scripter) : base(scripter){}

        public void Execute(CommandArguments arguments)
        {
            try
            {
                ExcelManager.CurrentExcel.CopySelectedCellsToClipboard();
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error occurred while copying content of the currently selected cells. Message: '{ex.Message}'", ex);
            }
        }
    }
}
