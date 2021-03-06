﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataPersistent;

namespace ControlePecuarista.src.Controls
{
    public partial class UnidadeAnimalUserControl : UserControl
    {
        private int currentID;
        private UnidadeAnimal currentUnidadeAnimal;
        private UnidadeAnimalDAO currentUnidadeAnimalDao;

        public UnidadeAnimalUserControl(string id = null)
        {
            InitializeComponent();
            currentID = -1;
            currentUnidadeAnimalDao = new UnidadeAnimalDAO(MainWindow.currentPath);

            if (id != null)
            {
                currentID = int.Parse(id);
                currentUnidadeAnimal = currentUnidadeAnimalDao.selectById(currentID);
                nomeTextBox.Text = currentUnidadeAnimal.nome;
                uaEntradaTextBox.Text = currentUnidadeAnimal.uaEntrada.ToString();
                uaSaidaTextBox.Text = currentUnidadeAnimal.uaSaida.ToString();
                dataEntradaDatePicker.Value = DateTime.FromFileTimeUtc(currentUnidadeAnimal.dataEntrada);
                dataSaidaDatePicker.Value = DateTime.FromFileTimeUtc(currentUnidadeAnimal.dataSaida);
                racaTextBox.Text = currentUnidadeAnimal.raca;
                valorUaTextBox.Text = currentUnidadeAnimal.valor.ToString();

            }


        }

        private void button1_Click(object sender, EventArgs e) {

            if (currentID != -1)
            {
                currentUnidadeAnimal.nome = nomeTextBox.Text;
                currentUnidadeAnimal.dataEntrada = dataEntradaDatePicker.Value.ToFileTimeUtc();
                currentUnidadeAnimal.dataSaida = dataSaidaDatePicker.Value.ToFileTimeUtc();
                currentUnidadeAnimal.uaEntrada = float.Parse(uaEntradaTextBox.Text);
                currentUnidadeAnimal.uaSaida = float.Parse(uaSaidaTextBox.Text);
                currentUnidadeAnimal.valor = float.Parse(valorUaTextBox.Text);
                currentUnidadeAnimal.raca = racaTextBox.Text;
                currentUnidadeAnimalDao.update(currentUnidadeAnimal);
            }
            else
            {
                currentUnidadeAnimal = new UnidadeAnimal(
                    nomeTextBox.Text,
                    float.Parse(uaEntradaTextBox.Text),
                    float.Parse(uaSaidaTextBox.Text),
                    dataEntradaDatePicker.Value.ToFileTimeUtc(),
                    dataSaidaDatePicker.Value.ToFileTimeUtc(),
                    racaTextBox.Text,
                    float.Parse(valorUaTextBox.Text)
                );
                currentUnidadeAnimalDao.insert(currentUnidadeAnimal);


            }
            MainWindow.updateTreeNodesAction();
            MessageBox.Show(this, "Unidade Animal adicionada com sucesso.");
            Dispose();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DatalheGastosWindow windows = new DatalheGastosWindow(currentID);
            windows.Show();

        }
    }
}
