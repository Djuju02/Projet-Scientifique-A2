using System;

namespace TD_2
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ConsoleKeyInfo cki;
            do
            {
                int mémoire = 1;

                Console.WriteLine("Voici la liste d'image disponible :" +
                "\n1 - Coco" +
                "\n2 - Lac" +
                "\n3 - Test" +
                "\n4 - Léna\n");

                Console.Write("Veuillez saisir le numéro de l'image que vous souhaitez traiter -> ");
                int lecture = Convert.ToInt32(Console.ReadLine());

                if(lecture <= 0 || lecture > 4)
                {
                    while(lecture <= 0 || lecture > 4)
                    {
                        Console.WriteLine("\n***** Valeur erronée ! *****");
                        Console.Write("\nVeuillez saisir un numéro valide entre 1 et 4 -> ");
                        lecture = Convert.ToInt32(Console.ReadLine());
                    }
                }

                Console.Clear();
                while (mémoire == 1)
                {
                    MyImage image = null;
                    switch (lecture)
                    {
                        case 1:
                            image = new MyImage("./Images références/coco.bmp");
                            Console.WriteLine("Vous avez séléctionné l'image Coco");
                            break;

                        case 2:
                            image = new MyImage("./Images références/lac.bmp");
                            Console.WriteLine("Vous avez séléctionné l'image Lac");
                            break;

                        case 3:
                            image = new MyImage("./Images références/Test.bmp");
                            Console.WriteLine("Vous avez séléctionné l'image Test");
                            break;

                        case 4:
                            image = new MyImage("./Images références/lena.bmp");
                            Console.WriteLine("Vous avez séléctionné l'image Léna");
                            break;
                    }
                    Console.WriteLine("Que souhaitez-vous faire avec cette image ?");
                    Console.WriteLine("\nVoici la liste d'actions réalisables :" +
                        "\n1 - Récupérer ses informations" +
                        "\n2 - Appliquer un filtre noir et blanc" +
                        "\n3 - Appliquer un filtre nuance de gris" +
                        "\n4 - Appliquer une rotation de l'image" +
                        "\n5 - Appliquer un effet de rétrécissement ou d'agrandissement" +
                        "\n6 - Appliquer un effet miroir" +
                        "\n7 - Détection de contours" +
                        "\n8 - Renforcement des bords" +
                        "\n9 - Flou" +
                        "\n10 - Repoussage" +
                        "\n11 - Création d'une image décrivant une fractale" +
                        "\n12 - Création d'un histogramme de l'image" +
                        "\n13 - Coder une image dans une image" +
                        "\n14 - Décoder une image dans une image" +
                        "\n15 - Générer un QR Code" +
                        "\n");
                    Console.Write("Veuillez saisir le numéro de l'action désirée -> ");
                    int lecture_action = Convert.ToInt32(Console.ReadLine());

                    if (lecture_action <= 0 || lecture_action > 15)
                    {
                        while (lecture_action <= 0 || lecture_action > 15)
                        {
                            Console.WriteLine("\n********** Valeur erronée ! **********");
                            Console.Write("\nVeuillez saisir un numéro valide entre 1 et 15 -> ");
                            lecture_action = Convert.ToInt32(Console.ReadLine());
                        }
                    }

                    Console.Clear();

                    switch (lecture_action)
                    {

                        case 1:
                            image.Informations();
                            break;

                        case 2:
                            image.Blanc_noir();
                            break;

                        case 3:
                            image.Nuance_Gris();
                            break;

                        case 4:
                            Console.Write("Veuillez saisir l'angle de rotation (sens horaire) que vous souhaitez appliquer à votre image -> ");
                            double val = Convert.ToDouble(Console.ReadLine());
                            image.Rotation(val);
                            break;

                        case 5:
                            Console.Write("Veuillez saisir le pourcentage pour agrandir ou réduire votre image -> ");
                            double pourcentage = Convert.ToDouble(Console.ReadLine());
                            image.AgrandissementRetrecissement(pourcentage);
                            break;

                        case 6:
                            image.Miroir();
                            break;

                        case 7:
                            image.Detection_de_contour();
                            break;

                        case 8:
                            image.Renforcement_des_bords();
                            break;

                        case 9:
                            image.Flou();
                            break;

                        case 10:
                            image.Repoussage();
                            break;

                        case 11:
                            image.Fractale();
                            break;

                        case 12:
                            image.Histogramme();
                            break;

                        case 13:
                            image.Codeur();
                            break;

                        case 14:
                            image.Decodeur();
                            break;

                        case 15:
                            Console.WriteLine("Veuillez saisir le lien à convertir -->");
                            string mot = (string)Console.ReadLine();
                            image.QR_Code(mot);
                            break;
                    }
                    Console.Clear();
                    if (lecture_action != 1) Console.WriteLine("Enregistrement effectué !");
                    Console.Write("\nAppuyez pour continuer");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("Souhaitez-vous effectuer d'autres actions sur cette image ?");
                    Console.WriteLine("1 - Oui\n2 - Non\n");
                    Console.Write("Veuillez saisir votre choix -> ");

                    int choix = Convert.ToInt32(Console.ReadLine());
                    if (choix <= 0 || choix > 2)
                    {
                        while (choix <= 0 || choix > 2)
                        {
                            Console.WriteLine("\n********** Valeur erronée ! **********");
                            Console.Write("\nVeuillez saisir un numéro valide entre 1 et 2 -> ");
                            choix = Convert.ToInt32(Console.ReadLine());
                        }
                    }
                    if (choix == 2) mémoire = 0;
                    Console.Clear();
                }

                Console.WriteLine("\nTapez <ECHAP> pour sortir ou <ENTRER> pour continuer");
                cki = Console.ReadKey();
                Console.Clear();
            } while (cki.Key != ConsoleKey.Escape);

            Console.WriteLine("BBonne continuation !");
            Console.ReadKey();
        }
    }
}
