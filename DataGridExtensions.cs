using System;
using System.Text;
using System.Windows.Forms;

public static class DataGridViewExtensions
{
    public static void CopyDataGridViewToClipboard(DataGridView dataGridView)
    {
        // Проверяем наличие данных в DataGridView
        if (dataGridView.Rows.Count == 0)
        {
            return;
        }

        // Создаем StringBuilder для построения строки, которая будет скопирована в буфер обмена
        StringBuilder clipboardText = new StringBuilder();

        // Добавляем названия столбцов
        for (int i = 0; i < dataGridView.Columns.Count; i++)
        {
            if (dataGridView.Columns[i].Visible)
            {
                clipboardText.Append(dataGridView.Columns[i].HeaderText + "\t");
            }
        }
        clipboardText.AppendLine();

        // Добавляем данные из строк
        foreach (DataGridViewRow row in dataGridView.Rows)
        {
            if (!row.IsNewRow)
            {
                for (int i = 0; i < dataGridView.Columns.Count; i++)
                {
                    if (dataGridView.Columns[i].Visible)
                    {
                        clipboardText.Append(row.Cells[i].Value?.ToString() + "\t");
                    }
                }
                clipboardText.AppendLine();
            }
        }

        // Копируем текст в буфер обмена
        Clipboard.SetText(clipboardText.ToString());
    }
}
