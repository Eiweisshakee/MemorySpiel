using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemorieSpiel
{
    public partial class Spielfeld : Form
    {
        // Die Variable firstClicked merkt sich das erste Feld das ein Spieler anklickt
        Label firstClicked = null;

        // Die Variable secondClicked merkt sich das zweite Feld das ein Spieler anklickt
        Label secondClicked = null;
        
        //Random hilft dabei die Bilder zufällig anzuordnen
        Random random = new Random();

        //ToDo: Fülle die Liste mit allen Symbolen auf dem Spielfeld
        List<string> bilderListe = new List<string>()
        {
            "!","!","N","N",",",",","k","k",
            "b","b","v","v","w","w","z","z"
        };

        //Konstruktor, die Methoden werden beim Start der Klasse aufgerufen, wenn Sie hier aufgerufen werden
        public Spielfeld()
        {
            InitializeComponent();
            OrdneBilderZuRechteckenZu();
        }

        //ToDo: 
        //Ordne die Bilder den einzelnen Rechtecken auf dem Spielfeld zu
        // Das TableLayoutPanel hat 16 labels und die Bilderliste hat 16 Symbole.
        // Ordne jedem Rechteck/Label in dem RechteckTabelle Layout ein Symbol
        // aus der BilderListe zu. Nutze dazu Random.

        private void OrdneBilderZuRechteckenZu()
        {
            Label label;
            int randomZahl;

            for(int i = 0; i < rechteckTabelle.Controls.Count; i++)
            {
                if(rechteckTabelle.Controls[i] is Label)
                
                    label = (Label)rechteckTabelle.Controls[i];
                else
                continue;

                randomZahl = random.Next(0, bilderListe.Count);
                label.Text = bilderListe[randomZahl];

                bilderListe.RemoveAt(randomZahl);

            }

        }

        //ToDo: 
        //Methode die beim Click auf jedes Label in der Tabelle ausgeführt wird.
        //Prüfe, ob der zaehler enabled ist, wenn ja dann gehe zurück
        //Untersuche, ob das clickedLabel nicht null ist
        //Schaue ob die ForeColor vom clickedLabel schwarz ist, dann gehe zurück
        //Prüfe ob firstClicked null ist, wenn ja, dann hat der Spieler das erste Bild angeklickt und wir merken
        //uns das Bild und gehen zurück
        //Wenn firstClicked schon gesetzt ist, dann merken wir uns das Bild in secondclicked
        //Prüfe auf einen Gewinn.
        //Prüfe, ob secondClicked==firstClicked
        //Starte den zaehler

        private void label_Click(object sender, EventArgs e)
        {
            //Die Methode übergibt das gerade angeklickte Label als Object sender
            Label clickedLabel = sender as Label;

            if (firstClicked != null && secondClicked != null)
                return;

            if (clickedLabel == null)
                return;

            if (clickedLabel.ForeColor == Color.Black)
                return;

            if (firstClicked == null)
            {
                firstClicked = clickedLabel;
                firstClicked.ForeColor = Color.Black;
                return;
            }

            secondClicked = clickedLabel;
            secondClicked.ForeColor = Color.Black;

            PruefeAufGewinner();

            if (firstClicked.Text == secondClicked.Text)
            {
                firstClicked = null;
                secondClicked = null;
            }
            else
            zaehler.Start();
        }

        //ToDo: 
        //Beende den aktuellen zaehler.
        //Verstecke beide sichtbaren Bilder
        //Setze die Variablen firstClicked und SecondClicked zurück
        private void zaehler_tick(object sender, EventArgs e)
        {
            zaehler.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            firstClicked = null;
            secondClicked = null;

        }

        //ToDo: 
        //Untersuche alle Labels auf dem rechteckTabelle-Layout
        //Prüfe, ob die Bilder sichtbar sind und mit einem anderen Bild matchen
        //Vergleiche dafür die Vordergrund und die Hintergrundfarbe von einem Label, ob diese gleich sind
        //Nutze hier eine foreach Schleife
        //Wenn die foreach-Schleife beendet wird, dann soll eine MessageBox den Spieler auf seinen Gewinn hinweisen.
        private void PruefeAufGewinner()
        {
            Label label;
            for(int i = 0; i < rechteckTabelle.Controls.Count; i++)
            {
                label = rechteckTabelle.Controls[i] as Label;
                if (label != null && label.ForeColor == Color.Black)
                    return;


            }
            MessageBox.Show("GEWONNEN!");
        }

    }
}
