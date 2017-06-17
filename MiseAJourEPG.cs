using System;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;

namespace ConsoleApplication1
{
    class Program
    {
        static string DossierUtilisateur = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        static string DossierEPG = Path.Combine(DossierUtilisateur, "Documents", "EPG Kodi");
        static string FichierZip = Path.Combine(DossierEPG, "complet.zip");
        static string FichierXML = Path.Combine(DossierEPG, "complet.xml");
        static string adresse = "http://xmltv.dtdns.net/download/complet.zip";
        static bool TraceLigne = true;

        static void Main(string[] args)
        {
            Console.WriteLine("Lancement de la mise à jour de l'EPG");
            if (!Directory.Exists(DossierEPG))
            {
                Console.WriteLine("Création du Dossier EPG dans le Dossier 'Documents'");
                Directory.CreateDirectory(DossierEPG);
            }
            if (File.Exists(FichierZip))
            {
                Console.WriteLine("Effacement du fichier Zip précédent");
                File.Delete(FichierZip);
            }
            if (File.Exists(FichierXML))
            {
                Console.WriteLine("Effacement du fichier XML précédent");
                File.Delete(FichierXML);
            }
            Console.WriteLine("Lancement du téléchargement du nouveau fichier Zip en cours...");
            TelechagementEnFond(adresse);
            Thread.Sleep(60000);
        }

        public static void TelechagementEnFond(string address)
        {
            Uri uri = new Uri(address);
            WebClient MonClientWeb = new WebClient();
            // Specify that the DownloadFileCallback method gets called
            // when the download completes.
            MonClientWeb.DownloadFileCompleted += new AsyncCompletedEventHandler (TelechagementTermine);
            // Specify a avancement notification handler.
            MonClientWeb.DownloadProgressChanged += new DownloadProgressChangedEventHandler (TelechagementEnCours);
            MonClientWeb.DownloadFileAsync(uri, FichierZip);
        }

        private static void TelechagementEnCours(object sender, DownloadProgressChangedEventArgs e)
        {
            try
            {
                if ( TraceLigne == true )
                { 
                    DessinerBarreDeavancementionText(e.ProgressPercentage, 100);
                }
            }
            catch (Exception ex) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Echec du téléchargement : " + ex.Message); }
        }
        private static void DessinerBarreDeavancementionText(int avancement, int tot)
        {
            //draw empty avancement bar
            TraceLigne = false;
            Console.CursorLeft = 0;
            Console.Write("["); //start
            Console.CursorLeft = 32;
            Console.Write("]"); //end
            Console.CursorLeft = 1;
            float RepresentationPourcentageSurTrente = 30.0f / tot;

            //draw filled part
            int position = 1;
            for (int i = 0; i < RepresentationPourcentageSurTrente * avancement; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.CursorLeft = position++;
                Console.Write("#");
            }

            //draw unfilled part
            for (int i = position; i <= 31; i++)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.CursorLeft = position++;
                Console.Write("-");
            }

            //draw totals
            Console.CursorLeft = 35;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(avancement.ToString() + "%" + " téléchargé(s) sur " + tot.ToString() + "%" + "                      "); //blanks at the end remove any excess
            TraceLigne = true;
        }

        private static void TelechagementTermine(object sender, AsyncCompletedEventArgs e)
        {
            Thread.Sleep(500);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("");
            Console.WriteLine("Téléchargement terminé");
            Console.WriteLine("Extraction du fichier Zip en cours...");
            ZipFile.ExtractToDirectory(FichierZip, DossierEPG);
            Console.WriteLine("Finit");
            Thread.Sleep(500);
            Environment.Exit(0);
        }
    }
}