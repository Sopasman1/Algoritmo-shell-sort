using System.Drawing;
using System.Windows.Forms;
namespace Algoritmo_shell_sort
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Configuración inicial del DataGridView
            dataGridView1.ColumnCount = 10;  // Vamos a mostrar el arreglo completo en una fila de 10 columnas
            dataGridView1.RowCount = 1;      // Solo una fila para los números

            // Configurar el DataGridView para que no pueda editarse
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;

            // Asegurar que se muestre correctamente el contenido
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            try
            {
                // Obtener los números ingresados por el usuario
                string input = txtInput.Text;
                int[] numbers = input.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                     .Select(int.Parse)
                                     .ToArray();

                // Desordenar el arreglo antes de ordenar
                ShuffleArray(numbers);

                // Mostrar el arreglo desordenado
                ShowArrayState(numbers);

                // Aplicar Shell Sort y mostrar cada paso
                ShellSort(numbers);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Algoritmo de Shell Sort con visualización
        private void ShellSort(int[] arr)
        {
            int n = arr.Length;

            // Inicializar el intervalo
            for (int gap = n / 2; gap > 0; gap /= 2)
            {
                for (int i = gap; i < n; i++)
                {
                    int temp = arr[i];
                    int j;

                    // Realizar la comparación y reordenamiento
                    for (j = i; j >= gap && arr[j - gap] > temp; j -= gap)
                    {
                        arr[j] = arr[j - gap];

                        // Mostrar el estado actual con los cambios
                        ShowArrayState(arr, j, j - gap); // Resaltamos los elementos intercambiados
                    }

                    arr[j] = temp;

                    // Mostrar el estado después de la inserción
                    ShowArrayState(arr, j, i);
                }
            }

        }

        // Función para mostrar el estado del arreglo completo en el DataGridView
        private void ShowArrayState(int[] arr, int? index1 = null, int? index2 = null)
        {
            // Agregar el arreglo como una nueva fila (acumulando los cambios)
            DataGridViewRow row = new DataGridViewRow();
            foreach (int number in arr)
            {
                DataGridViewCell cell = new DataGridViewTextBoxCell
                {
                    Value = number
                };
                row.Cells.Add(cell);
            }
            dataGridView1.Rows.Add(row);

            // Resaltar los intercambios en la fila
            if (index1.HasValue && index2.HasValue)
            {
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[index1.Value].Style.BackColor = Color.Yellow;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[index1.Value].Style.ForeColor = Color.Red;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[index2.Value].Style.BackColor = Color.Yellow;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[index2.Value].Style.ForeColor = Color.Red;
            }

            // Actualizar la interfaz
            dataGridView1.Refresh();
            System.Threading.Thread.Sleep(500); // Pausa para visualizar el cambio
        }

        // Función para desordenar el arreglo (Algoritmo Fisher-Yates)
        private void ShuffleArray(int[] arr)
        {
            Random rand = new Random();
            int n = arr.Length;

            // Recorremos el arreglo desde el final hasta el principio
            for (int i = n - 1; i > 0; i--)
            {
                // Elegimos un índice aleatorio entre 0 y i
                int j = rand.Next(0, i + 1);

                // Intercambiamos arr[i] con arr[j]
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }

    }
}

