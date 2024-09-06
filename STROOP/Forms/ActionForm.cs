using STROOP.Structs;
using STROOP.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace STROOP.Forms
{
    public partial class ActionForm : Form
    {
        public ActionForm()
        {
            InitializeComponent();

            var actions = TableConfig.MarioActions.GetActionList();
            foreach (var paramList in actions.Select(GetRowParams))
            {
                dataGridViewActions.Rows.Add(paramList.ToArray());
            }
        }

        private static List<object> GetRowParams(uint action)
        {
            var name = TableConfig.MarioActions.GetActionName(action);
            var group = TableConfig.MarioActions.GetGroup(action);
            var groupName = TableConfig.MarioActions.GetGroupName(action);
            var id = TableConfig.MarioActions.GetId(action);
            var actionBits = Enumerable.Range(9, 23).ToList()
                .ConvertAll(bit => (object)GetBit(action, bit));

            var paramList = new List<object>
            {
                name,
                HexUtilities.FormatValue(action, 8),
                HexUtilities.FormatValue(group, 3),
                groupName,
                HexUtilities.FormatValue(id, 3)
            };
            paramList.AddRange(actionBits);
            return paramList;
        }

        private static bool GetBit(uint action, int bit)
        {
            var value = action & (1 << bit);
            return value != 0;
        }
    }
}
