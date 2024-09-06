using System.Collections.Generic;

namespace STROOP.Forms
{
    public static class FormManager
    {
        private static readonly List<IUpdatableForm> Forms = new List<IUpdatableForm>();

        public static void AddForm(IUpdatableForm form)
        {
            Forms.Add(form);
        }

        public static void RemoveForm(IUpdatableForm form)
        {
            Forms.Remove(form);
        }

        public static void Update()
        {
            foreach (var form in Forms)
            {
                form.UpdateForm();
            }
        }
    }
}
