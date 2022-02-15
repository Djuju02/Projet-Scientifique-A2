using System;

namespace TD_2
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo cki;
            do
            {
                Console.WriteLine("Voici la liste d'image disponible :" +
                    "\n1 - Test" +
                    "\n2 - Coco" +
                    "\n3 - Love" +
                    "\n4 - Lac\n");
                Console.Write("Veuillez saisir le numéro de l'image que vous souhaitez traiter -> ");
                int lecture = Convert.ToInt32(Console.ReadLine());

                Console.Clear();

                switch (lecture)
                {
                    case 1:
                        int mémoire1 = 1;                    
                        while (mémoire1 == 1)
                        {
                            MyImage image1 = new MyImage("test.bmp");

                            Console.WriteLine("Que souhaitez-vous faire avec cette image ?");
                            Console.WriteLine("\nVoici la liste d'actions réalisables :" +
                                "\n1 - Récupérer ses informations" +
                                "\n2 - Appliquer un filtre noir et blanc" +
                                "\n3 - Appliquer un filtre nuance de gris" +
                                "\n4 - Appliquer une rotation de l'image" +
                                "\n5 - Appliquer un effet de rétrécissement ou d'agrandissement" +
                                "\n6 - Appliquer un effet miroir\n");
                            Console.Write("Veuillez saisir le numéro de l'action désirée -> ");
                            int lecture1 = Convert.ToInt32(Console.ReadLine());
                            Console.Clear();

                            switch (lecture1)
                            {

                                case 1:
                                    image1.Informations();
                                    break;

                                case 2:
                                    image1.Blanc_noir();
                                    break;

                                case 3:
                                    image1.Nuance_Gris();
                                    break;

                                case 4:
                                    int val = Convert.ToInt32(Console.ReadLine());
                                    image1.Rotation(val);
                                    break;

                                case 5:
                                    int pourcentage = Convert.ToInt32(Console.ReadLine());
                                    image1.AgrandissementRetrecissement(pourcentage);
                                    break;

                                case 6:
                                    int valeur = Convert.ToInt32(Console.ReadLine());
                                    image1.Miroir(valeur);
                                    break;
                            }
                            Console.WriteLine("\nAppuyez pour continuer");
                            Console.ReadKey();
                            Console.Clear();
                            Console.WriteLine("Souhaitez-vous effectuer d'autres actions sur cette image ?");
                            Console.WriteLine("1 - Oui\n2 - Non\n");
                            Console.Write("Veuillez saisir votre choix -> ");
                            if (Convert.ToInt32(Console.ReadLine()) == 2) mémoire1 = 0;
                            Console.Clear();
                        }
                        break;

                    case 2:
                        int mémoire2 = 1;
                        while (mémoire2 == 1)
                        {
                            MyImage image2 = new MyImage("coco.bmp");

                            Console.WriteLine("Que souhaitez-vous faire avec cette image ?");
                            Console.WriteLine("\nVoici la liste d'actions réalisables :" +
                                "\n1 - Récupérer ses informations" +
                                "\n2 - Appliquer un filtre noir et blanc" +
                                "\n3 - Appliquer un filtre nuance de gris" +
                                "\n4 - Appliquer une rotation de l'image" +
                                "\n5 - Appliquer un effet de rétrécissement ou d'agrandissement" +
                                "\n6 - Appliquer un effet miroir\n");
                            Console.Write("Veuillez saisir le numéro de l'action désirée -> ");
                            int lecture2 = Convert.ToInt32(Console.ReadLine());
                            Console.Clear();

                            switch (lecture2)
                            {

                                case 1:
                                    image2.Informations();
                                    break;

                                case 2:
                                    image2.Blanc_noir();
                                    break;

                                case 3:
                                    image2.Nuance_Gris();
                                    break;

                                case 4:
                                    int val = Convert.ToInt32(Console.ReadLine());
                                    image2.Rotation(val);
                                    break;

                                case 5:
                                    int pourcentage = Convert.ToInt32(Console.ReadLine());
                                    image2.AgrandissementRetrecissement(pourcentage);
                                    break;

                                case 6:
                                    int valeur = Convert.ToInt32(Console.ReadLine());
                                    image2.Miroir(valeur);
                                    break;
                            }
                            Console.WriteLine("\nAppuyez pour continuer");
                            Console.ReadKey();
                            Console.Clear();
                            Console.WriteLine("Souhaitez-vous effectuer d'autres actions sur cette image ?");
                            Console.WriteLine("1 - Oui\n2 - Non\n");
                            Console.Write("Veuillez saisir votre choix -> ");
                            if (Convert.ToInt32(Console.ReadLine()) == 2) mémoire2 = 0;
                            Console.Clear();
                        }
                        break;

                    case 3:
                        int mémoire3 = 1;
                        while (mémoire3 == 1)
                        {
                            MyImage image3 = new MyImage("love.bmp");

                            Console.WriteLine("Que souhaitez-vous faire avec cette image ?");
                            Console.WriteLine("\nVoici la liste d'actions réalisables :" +
                                "\n1 - Récupérer ses informations" +
                                "\n2 - Appliquer un filtre noir et blanc" +
                                "\n3 - Appliquer un filtre nuance de gris" +
                                "\n4 - Appliquer une rotation de l'image" +
                                "\n5 - Appliquer un effet de rétrécissement ou d'agrandissement" +
                                "\n6 - Appliquer un effet miroir\n");
                            Console.Write("Veuillez saisir le numéro de l'action désirée -> ");
                            int lecture3 = Convert.ToInt32(Console.ReadLine());
                            Console.Clear();

                            switch (lecture3)
                            {

                                case 1:
                                    image3.Informations();
                                    break;

                                case 2:
                                    image3.Blanc_noir();
                                    break;

                                case 3:
                                    image3.Nuance_Gris();
                                    break;

                                case 4:
                                    int val = Convert.ToInt32(Console.ReadLine());
                                    image3.Rotation(val);
                                    break;

                                case 5:
                                    int pourcentage = Convert.ToInt32(Console.ReadLine());
                                    image3.AgrandissementRetrecissement(pourcentage);
                                    break;

                                case 6:
                                    int valeur = Convert.ToInt32(Console.ReadLine());
                                    image3.Miroir(valeur);
                                    break;
                            }
                            Console.WriteLine("\nAppuyez pour continuer");
                            Console.ReadKey();
                            Console.Clear();
                            Console.WriteLine("Souhaitez-vous effectuer d'autres actions sur cette image ?");
                            Console.WriteLine("1 - Oui\n2 - Non\n");
                            Console.Write("Veuillez saisir votre choix -> ");
                            if (Convert.ToInt32(Console.ReadLine()) == 2) mémoire3 = 0;
                            Console.Clear();
                        }
                        break;

                    case 4:
                        int mémoire4 = 1;
                        while (mémoire4 == 1)
                        {
                            MyImage image4 = new MyImage("lac.bmp");

                            Console.WriteLine("Que souhaitez-vous faire avec cette image ?");
                            Console.WriteLine("\nVoici la liste d'actions réalisables :" +
                                "\n1 - Récupérer ses informations" +
                                "\n2 - Appliquer un filtre noir et blanc" +
                                "\n3 - Appliquer un filtre nuance de gris" +
                                "\n4 - Appliquer une rotation de l'image" +
                                "\n5 - Appliquer un effet de rétrécissement ou d'agrandissement" +
                                "\n6 - Appliquer un effet miroir\n");
                            Console.Write("Veuillez saisir le numéro de l'action désirée -> ");
                            int lecture4 = Convert.ToInt32(Console.ReadLine());
                            Console.Clear();

                            switch (lecture4)
                            {

                                case 1:
                                    image4.Informations();
                                    break;

                                case 2:
                                    image4.Blanc_noir();
                                    break;

                                case 3:
                                    image4.Nuance_Gris();
                                    break;

                                case 4:
                                    int val = Convert.ToInt32(Console.ReadLine());
                                    image4.Rotation(val);
                                    break;

                                case 5:
                                    int pourcentage = Convert.ToInt32(Console.ReadLine());
                                    image4.AgrandissementRetrecissement(pourcentage);
                                    break;

                                case 6:
                                    int valeur = Convert.ToInt32(Console.ReadLine());
                                    image4.Miroir();
                                    break;
                            }
                            Console.WriteLine("\nAppuyez pour continuer");
                            Console.ReadKey();
                            Console.Clear();
                            Console.WriteLine("Souhaitez-vous effectuer d'autres actions sur cette image ?");
                            Console.WriteLine("1 - Oui\n2 - Non\n");
                            Console.Write("Veuillez saisir votre choix -> ");
                            if (Convert.ToInt32(Console.ReadLine()) == 2) mémoire4 = 0;
                            Console.Clear();
                        }
                        break;
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
