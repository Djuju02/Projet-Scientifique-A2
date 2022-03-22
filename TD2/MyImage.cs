using System;
using System.IO;


namespace TD_2
{
    class MyImage
    {
        string filename;
        string typedefichier;
        int taille_fichier;
        int taille_offset;
        int largeur;
        int hauteur;
        int nb_bits_couleur;
        Pixel[,] matrice_image_RGB;

        //Pour charger un fichier et le transformer en instance de la classe MyImage
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename_1"></param>
        public MyImage(string filename_1)
        {
            byte[] myfile = File.ReadAllBytes(filename_1);
            this.filename = filename_1;

            if (myfile[0] == 66f && myfile[1] == 77f) this.typedefichier = "Bitmap";
            else this.typedefichier = "error";

            this.taille_fichier = Convert_Endian_To_Int(myfile, 2, 3, 4, 5);
            this.taille_offset = Convert_Endian_To_Int(myfile, 10, 11, 12, 13);
            this.largeur = Convert_Endian_To_Int(myfile, 18, 19, 20, 21);
            this.hauteur = Convert_Endian_To_Int(myfile, 22, 23, 24, 25);
            this.nb_bits_couleur = Convert_Endian_To_Int(myfile, 28, 29, -1, -1);

            this.matrice_image_RGB = new Pixel[GetHauteur, GetLargeur];

            int compteur = GetTaille_offset;

            //Modulo 4
            int vide = 0;

            if (4 - (GetLargeur % 4) == 1) vide += 3;

            if (4 - (GetLargeur % 4) == 2) vide += 2;

            if (4 - (GetLargeur % 4) == 3) vide += 1;

            for (int i = 0; i < GetHauteur; i++)
            {
                for (int j = 0; j < GetLargeur; j++)
                {
                    this.matrice_image_RGB[i, j] = new Pixel(myfile[compteur], myfile[compteur + 1], myfile[compteur + 2]);
                    compteur += 3;
                }
                compteur += vide;
            }
        }

        /*
        //Si jamais on veut copier l'image;
        public MyImage(MyImage image_1)
        {
            this.image = image_1;
        }
        */

        /// <summary>
        /// 
        /// </summary>
        public string GetTypeDeFichier
        {
            get { return this.typedefichier; }
            set { this.typedefichier = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int GetTaille_fichier
        {
            get { return this.taille_fichier; }
            set { this.taille_fichier = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int GetTaille_offset
        {
            get { return this.taille_offset; }
            set { this.taille_offset = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int GetLargeur
        {
            get { return this.largeur; }
            set { this.largeur = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int GetHauteur
        {
            get { return this.hauteur; }
            set { this.hauteur = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int GetNb_bits_couleur
        {
            get { return this.nb_bits_couleur; }
            set { this.nb_bits_couleur = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Pixel[,] GetMatrice_image_RGB
        {
            get { return this.matrice_image_RGB; }
            set { this.matrice_image_RGB = value; }
        }
        /// <summary>
        /// Convert endian to int
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="val1"></param>
        /// <param name="val2"></param>
        /// <param name="val3"></param>
        /// <param name="val4"></param>
        /// <returns></returns>
        public int Convert_Endian_To_Int(byte[] tab, int val1, int val2, int val3, int val4)
        {
            int convertion = 0;

            if (val3 != -1 && val4 != -1)
            {
                byte[] tabconversion = { tab[val1], tab[val2], tab[val3], tab[val4] };
                for (int i = 0; i <= 3; i++)
                {
                    convertion += tabconversion[i] * Convert.ToInt32(Math.Pow(256, i));
                }
            }
            else
            {
                byte[] tabconversion = { tab[val1], tab[val2] };
                for (int i = 0; i <= 1; i++)
                {
                    convertion += tabconversion[i] * Convert.ToInt32(Math.Pow(256, i));
                }
            }

            return convertion;
        }

        //2000 = 0*256^3 + 0*256^2 + 7*256^1+ 208*256^0
        /// <summary>
        /// Convertir int to endian
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public byte[] Convertir_Int_To_Endian(int val)
        {
            byte[] conversion = new byte[4];
            int test;
            for (int i = 3; i >= 0; i--)
            {
                test = Convert.ToInt32(Math.Pow(256, i));
                while (val - test >= 0 && test <= val)
                {
                    val -= test;
                    conversion[i] += 1;
                }
            }
            return conversion;
        }

        //pour sauvegarder le fichier
        /// <summary>
        /// From image to file
        /// </summary>
        /// <param name="myfile"></param>
        public void From_Image_To_File(string myfile)
        {
            //Modulo 4
            int add = 0;

            if (4 - (GetLargeur % 4) == 1) add += 3;

            if (4 - (GetLargeur % 4) == 2) add += 2;

            if (4 - (GetLargeur % 4) == 3) add += 1;

            byte[] fileorigin = File.ReadAllBytes(myfile);
            byte[] filefinal = new byte[GetTaille_offset + GetMatrice_image_RGB.Length * 3 + add * GetHauteur];

            //Type de fichier 0/1
            filefinal[0] = 66;
            filefinal[1] = 77;

            //Taille de fichier 2/3/4/5
            byte[] ConvTaille_fichier = Convertir_Int_To_Endian(GetTaille_fichier);
            int compteur1 = 0;
            for (int i = 2; i <= 5; i++)
            {
                filefinal[i] = ConvTaille_fichier[compteur1];

                compteur1++;
            }

            //Cases 6/7/8/9
            for (int i = 6; i <= 9; i++)
            {
                filefinal[i] = fileorigin[i];
            }

            //Offset à partir duquel commence les données brutes de l'image 10/11/12/13
            byte[] ConvTaille_offset = Convertir_Int_To_Endian(GetTaille_offset);
            int compteur2 = 0;
            for (int i = 10; i <= 13; i++)
            {
                filefinal[i] = ConvTaille_offset[compteur2];
                compteur2++;
            }

            //Cases 14/15/16/17
            for (int i = 14; i <= 17; i++)
            {
                filefinal[i] = fileorigin[i];
            }

            //résolution horizontale 18/19/20/21
            byte[] ConvLargeur = Convertir_Int_To_Endian(GetLargeur);
            int compteur3 = 0;
            for (int i = 18; i <= 21; i++)
            {
                filefinal[i] = ConvLargeur[compteur3];
                compteur3++;
            }

            //Résolution verticale 22/23/24/25
            byte[] ConvHauteur = Convertir_Int_To_Endian(GetHauteur);
            int compteur4 = 0;
            for (int i = 22; i <= 25; i++)
            {
                filefinal[i] = ConvHauteur[compteur4];
                compteur4++;
            }

            //Cases 26/27
            for (int i = 26; i <= 27; i++)
            {
                filefinal[i] = fileorigin[i];
            }

            //Nombre de Bits par couleur 28/29
            byte[] ConvNb_bits_couleurs = Convertir_Int_To_Endian(GetNb_bits_couleur);
            int compteur5 = 0;
            for (int i = 28; i <= 29; i++)
            {
                filefinal[i] = ConvNb_bits_couleurs[compteur5];
                compteur5++;
            }

            //Cases 30 à 53
            for (int i = 30; i <= 53; i++)
            {

                filefinal[i] = fileorigin[i];
            }

            //Ligne 54 
            int compteur = GetTaille_offset;

            for (int i = 0; i < GetHauteur; i++)
            {
                for (int j = 0; j < GetLargeur; j++)
                {
                    filefinal[compteur] = GetMatrice_image_RGB[i, j].GetRouge;
                    filefinal[compteur + 1] = GetMatrice_image_RGB[i, j].GetVert;
                    filefinal[compteur + 2] = GetMatrice_image_RGB[i, j].GetBleu;
                    compteur += 3;
                }
                for (int j = GetLargeur; j < GetLargeur + add; j++)
                {
                    filefinal[compteur] = 0;
                    compteur++;
                }
            }

            File.WriteAllBytes("./Images enregistrée/SortieTest.bmp", filefinal);
        }
        /// <summary>
        /// 
        /// </summary>
        public void Informations()
        {
            Console.WriteLine("La type de fichier est " + GetTypeDeFichier);
            Console.WriteLine("La taille du fichier est de " + GetTaille_fichier + " octets");
            Console.WriteLine("L'offset à partir duquel commence les données brutes de l'image est " + GetTaille_offset);
            Console.WriteLine("La résolution verticale de l'image est de " + GetHauteur + " pixels");
            Console.WriteLine("La résolution horizontale de l'image est de " + GetLargeur + " pixels");
            Console.WriteLine("Le nombre de bits par couleur est de " + GetNb_bits_couleur + " bits");
            Console.ReadKey();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Nuance_Gris()
        {
            for (int i = 0; i < GetHauteur; i++)
            {
                for (int j = 0; j < GetLargeur; j++)
                {
                    byte r = GetMatrice_image_RGB[i, j].GetRouge;
                    byte g = GetMatrice_image_RGB[i, j].GetVert;
                    byte b = GetMatrice_image_RGB[i, j].GetBleu;
                    int moyenne = (r + g + b) / 3;
                    GetMatrice_image_RGB[i, j].GetRouge = Convert.ToByte(moyenne);
                    GetMatrice_image_RGB[i, j].GetVert = Convert.ToByte(moyenne);
                    GetMatrice_image_RGB[i, j].GetBleu = Convert.ToByte(moyenne);

                }
            }
            From_Image_To_File(filename);
        }
        /// <summary>
        /// 
        /// </summary>
        public void Blanc_noir()
        {
            for (int i = 0; i < GetHauteur; i++)
            {
                for (int j = 0; j < GetLargeur; j++)
                {
                    int r = GetMatrice_image_RGB[i, j].GetRouge;
                    int g = GetMatrice_image_RGB[i, j].GetVert;
                    int b = GetMatrice_image_RGB[i, j].GetBleu;

                    int moyenne = (r + g + b) / 3;

                    if (moyenne < 255 / 2)
                    {
                        GetMatrice_image_RGB[i, j].GetRouge = 0;
                        GetMatrice_image_RGB[i, j].GetVert = 0;
                        GetMatrice_image_RGB[i, j].GetBleu = 0;
                    }
                    else
                    {
                        GetMatrice_image_RGB[i, j].GetRouge = 255;
                        GetMatrice_image_RGB[i, j].GetVert = 255;
                        GetMatrice_image_RGB[i, j].GetBleu = 255;
                    }
                }
            }
            From_Image_To_File(filename);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pourcentage"></param>
        public void AgrandissementRetrecissement(double pourcentage)
        {
            int newhauteur = Convert.ToInt32(GetHauteur * pourcentage);
            int newlargeur = Convert.ToInt32(GetLargeur * pourcentage);

            Pixel[,] newmatrice_image_RGB = new Pixel[newhauteur, newlargeur];

            for (int i = 0; i < newmatrice_image_RGB.GetLength(0); i++)
            {
                for (int j = 0; j < newmatrice_image_RGB.GetLength(1); j++)
                {
                    int div1 = Convert.ToInt32((i * GetMatrice_image_RGB.GetLength(0)) / newmatrice_image_RGB.GetLength(0));
                    int div2 = Convert.ToInt32((j * GetMatrice_image_RGB.GetLength(1)) / newmatrice_image_RGB.GetLength(1));

                    newmatrice_image_RGB[i, j] = new Pixel(GetMatrice_image_RGB[div1, div2].GetRouge, GetMatrice_image_RGB[div1, div2].GetVert, GetMatrice_image_RGB[div1, div2].GetBleu);
                }
            }

            GetMatrice_image_RGB = newmatrice_image_RGB;
            GetHauteur = GetMatrice_image_RGB.GetLength(0);
            GetLargeur = GetMatrice_image_RGB.GetLength(1);
            GetTaille_fichier = GetHauteur * GetLargeur * 3;

            From_Image_To_File(filename);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="angle"></param>
        public void Rotation(double angle)
        {
            angle = (angle / 180.0) * Math.PI;
            int[] center = { GetMatrice_image_RGB.GetLength(0) / 2, GetMatrice_image_RGB.GetLength(1) / 2 };
            int new_hauteur = GetMatrice_image_RGB.GetLength(0) / 2;
            int new_largeur = GetMatrice_image_RGB.GetLength(1) / 2;
            int x_max = 0;
            int x_min = 0;
            int y_max = 0;
            int y_min = 0;
            for (int i = 0; i < GetMatrice_image_RGB.GetLength(0); i += GetMatrice_image_RGB.GetLength(0) - 1)
            {
                for (int j = 0; j < GetMatrice_image_RGB.GetLength(1); j += GetMatrice_image_RGB.GetLength(1) - 1)
                {
                    double r = Math.Sqrt(Math.Pow(i - center[0], 2) + Math.Pow(j - center[1], 2));
                    double theta = Math.Atan2(i - center[0], j - center[1]) + angle;

                    x_max = (x_max < r * Math.Cos(theta)) ? (int)(r * Math.Cos(theta)) : x_max;
                    x_min = (x_min > r * Math.Cos(theta)) ? (int)(r * Math.Cos(theta)) : x_min;

                    y_max = (y_max < r * Math.Sin(theta)) ? (int)(r * Math.Sin(theta)) : y_max;
                    y_min = (y_min > r * Math.Sin(theta)) ? (int)(r * Math.Sin(theta)) : y_min;
                }
            }
            new_hauteur = y_max - y_min + 1;
            new_largeur = x_max - x_min + 1;

            Pixel[,] newmatrice_image_RGB = new Pixel[new_hauteur, new_largeur];
            Pixel p = new Pixel(0, 0, 0);
            int[] new_center = { newmatrice_image_RGB.GetLength(0) / 2, newmatrice_image_RGB.GetLength(1) / 2 };
            for (int i = 0; i < newmatrice_image_RGB.GetLength(0); i++)
            {
                for (int j = 0; j < newmatrice_image_RGB.GetLength(1); j++)
                {
                    double rayon = Math.Sqrt(Math.Pow(i - new_center[0], 2) + Math.Pow(j - new_center[1], 2));
                    double theta = Math.Atan2(j - new_center[1], i - new_center[0]) - angle;

                    int new_x = (int)(center[0] + rayon * Math.Cos(theta));
                    int new_y = (int)(center[1] + rayon * Math.Sin(theta));

                    if (new_x >= 0 && new_x < GetMatrice_image_RGB.GetLength(0) && new_y >= 0 && new_y < GetMatrice_image_RGB.GetLength(1)) newmatrice_image_RGB[i, j] = GetMatrice_image_RGB[new_x, new_y];
                    else newmatrice_image_RGB[i, j] = p;
                }
            }
            
            GetHauteur = new_hauteur;
            GetLargeur = new_largeur;
            GetMatrice_image_RGB = newmatrice_image_RGB;
            GetTaille_fichier = GetHauteur * GetLargeur * 3;

            From_Image_To_File(filename);
        }
        /// <summary>
        /// 
        /// </summary>
        public void Miroir()
        {
            Pixel[,] newmatrice_image_RGB = new Pixel[GetHauteur, GetLargeur];
            for (int i = 0; i < GetHauteur; i++)
            {
                for (int j = 0; j < GetLargeur; j++)
                {
                    newmatrice_image_RGB[i, j] = GetMatrice_image_RGB[i, GetLargeur - j - 1];
                }
            }
            this.GetMatrice_image_RGB = newmatrice_image_RGB;
            From_Image_To_File(filename);
        }

        /// <summary>
        /// Detection de contour
        /// </summary>
        public void Detection_de_contour()
        {
            //int[,] noyau_contours = { { 1, 0, -1 },{ 0, 0, 0 },{ -1, 0, 1 } };
            //int[,] noyau_contours = { { 0, 1, 0 }, { 1, -4, 1 }, { 0, 1, 0 } };
            int[,] noyau_contours = { { -1, -1, -1 }, { -1, 8, -1 }, { -1, -1, -1 } };

            int div = 1;

            Convolution(noyau_contours, div);
        }

        /// <summary>
        /// Renforcement des bords
        /// </summary>
        public void Renforcement_des_bords()
        {
            int[,] noyau_bords = { { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } };
            int div = 1;

            Convolution(noyau_bords, div);
        }

        /// <summary>
        /// Flou
        /// </summary>
        public void Flou()
        {
            int[,] noyau_flou = { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
            //int[,] noyau_flou = { { 1, 2, 1 }, { 2, 4, 2 }, { 1, 2, 1 } };

            int div = 9;
            //int div = 16;
            Convolution(noyau_flou, div);
        }

        /// <summary>
        /// Repoussage
        /// </summary>
        public void Repoussage()
        {
            int[,] noyau_repoussage = { { -2, -1, 0 }, { -1, 1, 1 }, { 0, 1, 2 } };

            int div = 1;

            Convolution(noyau_repoussage, div);
        }

        /// <summary>
        /// Convolution
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="div"></param>
        public void Convolution(int[,] mat, int div)
        {
            Pixel[,] newmatrice_image_RGB = new Pixel[GetHauteur, GetLargeur];
            Pixel p = new Pixel(0, 0, 0);

            int moyenneR;
            int moyenneG;
            int moyenneB;
            int compteurColonne = 0;
            int compteurLigne = 0;

            for (int i = 0; i < GetHauteur; i++)
            {
                for (int j = 0; j < GetLargeur; j++)
                {
                    moyenneR = 0;
                    moyenneG = 0;
                    moyenneB = 0;
                    int c = 0;
                    int d = 0;

                    for (int a = i - 1; a <= i + 1; a++)
                    {
                        for (int b = j - 1; b <= j + 1; b++)
                        {
                            if (i > 0 && i < GetHauteur - 1 && j > 0 && j < GetLargeur - 1)
                            {
                                c = a;
                                d = b;
                            }
                            if (a == -1) c = GetHauteur - 1;
                            if (a == GetHauteur) c = 0;
                            if (b == -1) d = GetLargeur - 1;
                            if (b == GetLargeur) d = 0;

                            moyenneR += GetMatrice_image_RGB[c, d].GetRouge * mat[compteurLigne, compteurColonne];
                            moyenneG += GetMatrice_image_RGB[c, d].GetVert * mat[compteurLigne, compteurColonne];
                            moyenneB += GetMatrice_image_RGB[c, d].GetBleu * mat[compteurLigne, compteurColonne];

                            compteurColonne++;
                        }
                        compteurColonne = 0;
                        compteurLigne++;
                    }
                    compteurLigne = 0;

                    moyenneR /= div;
                    moyenneG /= div;
                    moyenneB /= div;

                    if (moyenneR < 0) moyenneR = 0;
                    if (moyenneG < 0) moyenneG = 0;
                    if (moyenneB < 0) moyenneB = 0;
                    if (moyenneR > 255) moyenneR = 255;
                    if (moyenneG > 255) moyenneG = 255;
                    if (moyenneB > 255) moyenneB = 255;

                    newmatrice_image_RGB[i, j] = new Pixel((byte)moyenneR, (byte)moyenneG, (byte)moyenneB);
                }
            }
            GetMatrice_image_RGB = newmatrice_image_RGB;
            From_Image_To_File(filename);
        }

        /// <summary>
        /// Fractale
        /// </summary>
        public void Fractale()
        {
            GetHauteur = 3000;
            GetLargeur = 3000;
            int iteration_max = 50;
            double c_r = 0;
            double c_i = 0;
            double z_r = 0;
            double z_i = 0;
            int k = 0;
            double tmp = 0;

            Pixel[,] fractale = new Pixel[GetHauteur, GetLargeur];

            for (int i = 0; i < GetHauteur; i++)
            {
                for (int j = 0; j < GetLargeur; j++)
                {
                    c_r = (double)(i - (GetHauteur) / 2) / (double)(GetHauteur / 3);
                    c_i = (double)(j - (GetLargeur) / 2) / (double)(GetLargeur / 3);
                    z_r = 0;
                    z_i = 0;
                    k = 0;

                    while (k < iteration_max && z_r * z_r + z_i * z_i < 4)
                    {
                        tmp = z_r;
                        z_r = z_r * z_r - z_i * z_i + c_r;
                        z_i = 2 * z_i * tmp + c_i;
                        k++;
                    }
                    if (k == iteration_max)
                    {

                        fractale[i, j] = new Pixel(0, 0, 0);
                    }
                    else
                    {
                        fractale[i, j] = new Pixel((byte)(k * 255 / iteration_max), 0, (byte)(k * 255 / iteration_max));
                        n
                    }
                }
            }
            GetMatrice_image_RGB = fractale;
            GetTaille_fichier = GetHauteur * GetLargeur * 3;
            From_Image_To_File(filename);
        }
        /// <summary>
        /// Histogramme
        /// </summary>
        public void Histogramme()
        {
            Console.WriteLine("Il existe deux types d'histogramme :");
            Console.WriteLine("1 - RGB\n2 - Noir et blanc\n");
            Console.Write("Veuillez séléctionner le type d'histogramme souhaiter -> ");

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
            Console.Clear();
            switch (choix)
            {
                case 1:
                    Console.WriteLine("Histogramme RGB");

                    int[] tab_r = new int[256];
                    int[] tab_g = new int[256];
                    int[] tab_b = new int[256];
                    
                    Pixel[,] histo = new Pixel[100, 3 * 256 + 20];
                    Pixel p = new Pixel(0, 0, 0);
                    Pixel r = new Pixel(255, 0, 0);
                    Pixel g = new Pixel(0, 255, 0);
                    Pixel b = new Pixel(0, 0, 255);
                    for (int i = 0; i < histo.GetLength(0); i++)
                    {
                        for (int j = 0; j < histo.GetLength(1); j++)
                        {
                            histo[i, j] = p;
                        }
                    }

                    for (int i = 0; i<GetMatrice_image_RGB.GetLength(0); i++)
                    {
                        for (int j = 0; j < GetMatrice_image_RGB.GetLength(1); j++)
                        {
                            tab_r[GetMatrice_image_RGB[i, j].GetRouge]++;
                            tab_g[GetMatrice_image_RGB[i, j].GetVert]++;
                            tab_b[GetMatrice_image_RGB[i, j].GetBleu]++;
                        }
                    }
                    
                    int maxr = 0;
                    int maxg = 0;
                    int maxb = 0;

                    for (int j = 0; j <= 255; j++)
                    {
                        if (tab_r[j]> maxr) maxr = tab_r[j];
                        if (tab_g[j] > maxg) maxg = tab_g[j];
                        if (tab_b[j] > maxb) maxb = tab_b[j];
                    }

                    for (int j = 0; j <= 255; j++)
                    {
                        tab_r[j] = (tab_r[j] * 100/ maxr) ;
                        tab_g[j] = (tab_g[j] * 100/ maxg) ;
                        tab_b[j] = (tab_b[j] * 100/ maxb) ;
                    }

                    for (int i = 0; i <= 255; i++)
                    {
                        for (int j = 0; j <= tab_r[i] && j <= 99; j++)
                        {
                            histo[j, i] = r;
                        }

                        for (int j = 0; j <= tab_g[i] && j <= 99; j++)
                        {
                            histo[j, i + 256 + 10] = g;
                        }

                        for (int j = 0; j <= tab_b[i] && j <= 99; j++)
                        {
                            histo[j, i + 256*2 + 10*2] = b;
                        }
                    }
                    
                    GetHauteur = histo.GetLength(0);
                    GetLargeur = histo.GetLength(1);
                    GetMatrice_image_RGB = histo;
                    GetTaille_fichier = GetHauteur * GetLargeur * 3;
                    From_Image_To_File(filename);
                    break;

                case 2:
                    Console.WriteLine("Histogramme noir et blanc");

                    int[] tab_gris = new int[256];

                    Pixel[,] histo2 = new Pixel[100, 256];
                    Pixel p2 = new Pixel(0, 0, 0);
                    Pixel gris = new Pixel(172, 172, 172);

                    for (int i = 0; i < histo2.GetLength(0); i++)
                    {
                        for (int j = 0; j < histo2.GetLength(1); j++)
                        {
                            histo2[i, j] = p2;
                        }
                    }

                    for (int i = 0; i < GetMatrice_image_RGB.GetLength(0); i++)
                    {
                        for (int j = 0; j < GetMatrice_image_RGB.GetLength(1); j++)
                        {
                            tab_gris[(GetMatrice_image_RGB[i, j].GetRouge+GetMatrice_image_RGB[i, j].GetVert+GetMatrice_image_RGB[i, j].GetBleu)/3]++;
                        }
                    }

                    int maxgris = 0;

                    for (int j = 0; j <= 255; j++)
                    {
                        if (tab_gris[j] > maxgris) maxgris = tab_gris[j];
                    }

                    for (int j = 0; j <= 255; j++)
                    {
                        tab_gris[j] = (tab_gris[j] * 100 / maxgris);
                    }

                    for (int i = 0; i <= 255; i++)
                    {
                        for (int j = 0; j <= tab_gris[i] && j <= 99; j++)
                        {
                            histo2[j, i] = gris;
                        }
                    }

                    GetHauteur = histo2.GetLength(0);
                    GetLargeur = histo2.GetLength(1);
                    GetMatrice_image_RGB = histo2;
                    GetTaille_fichier = GetHauteur * GetLargeur * 3;
                    From_Image_To_File(filename);
                    break;
            }
        }

        //----------Codeur----------
        public void Codeur()
        {

        }
        public void Decodeur()
        {

        }




        public void HiddenPic(Pixel[,] image2)
        {
            Pixel[,] image_retour = new Pixel[GetMatrice_image_RGB.GetLength(0), GetMatrice_image_RGB.GetLength(1)];
            for (int i = 0; i < image_retour.GetLength(0); i++)
            {
                for (int n = 0; n < image_retour.GetLength(1); n++)
                {
                    image_retour[i, n].GetRouge = (byte)Convertir_Binairy_To_Int(Concaténer_tableaux(Convertir_Int_To_Binairy((int)this.GetMatrice_image_RGB[i, n].GetRouge), Convertir_Int_To_Binairy((int)image2[i, n].GetRouge)));
                    image_retour[i, n].GetVert = (byte)Convertir_Binairy_To_Int(Concaténer_tableaux(Convertir_Int_To_Binairy((int)this.GetMatrice_image_RGB[i, n].GetVert), Convertir_Int_To_Binairy((int)image2[i, n].GetVert)));
                    image_retour[i, n].GetBleu = (byte)Convertir_Binairy_To_Int(Concaténer_tableaux(Convertir_Int_To_Binairy((int)this.GetMatrice_image_RGB[i, n].GetBleu), Convertir_Int_To_Binairy((int)image2[i, n].GetBleu)));
                }
            }
            this.GetMatrice_image_RGB = image_retour;
        }

        public void Retrouver_image()
        {
            Pixel[,] image_retour = new Pixel[GetMatrice_image_RGB.GetLength(0), GetMatrice_image_RGB.GetLength(1)];
            for (int i = 0; i < image_retour.GetLength(0); i++)
            {
                for (int n = 0; n < image_retour.GetLength(1); n++)
                {
                    image_retour[i, n].GetRouge = (byte)Convertir_Binairy_To_Int(Recuperer_tableau(Convertir_Int_To_Binairy((int)this.GetMatrice_image_RGB[i, n].GetRouge)));
                    image_retour[i, n].GetVert = (byte)Convertir_Binairy_To_Int(Recuperer_tableau(Convertir_Int_To_Binairy((int)this.GetMatrice_image_RGB[i, n].GetVert)));
                    image_retour[i, n].GetBleu = (byte)Convertir_Binairy_To_Int(Recuperer_tableau(Convertir_Int_To_Binairy((int)this.GetMatrice_image_RGB[i, n].GetBleu)));
                }
            }
            this.GetMatrice_image_RGB = image_retour;
        }

        public int Convertir_Binairy_To_Int(int[] tab)
        {
            int s = 0;
            for (int n = tab.Length - 1; n >= 0; n--)
            {
                s += tab[tab.Length - 1 - n] * Convert.ToInt32(Math.Pow(2, n));
            }
            return s;
        }

        public int[] Convertir_Int_To_Binairy(int entier)
        {
            int p = 0;
            while ((entier / Convert.ToInt64(Math.Pow(2, p)) >= 1))
            {
                p++;
            }
            p--;
            int[] retour = new int[8];
            int[] temp = new int[8];
            for (int i = 0; i < retour.Length; i++)
            {
                temp[i] = 0;
            }
            for (int i = p; i >= 0; i--)
            {
                temp[i] = Convert.ToByte(entier / Convert.ToInt64(Math.Pow(2, p)));
                entier -= temp[i] * Convert.ToInt32(Math.Pow(2, p));
                p--;
            }
            for (int i = 0; i < retour.Length; i++)
            {
                retour[i] = temp[retour.Length - i - 1];
            }
            return retour;
        }

        public int[] Concaténer_tableaux(int[] tab1, int[] tab2)
        {
            int[] retour = new int[8];
            for (int i = 0; i < 4; i++)
            {
                retour[i] = tab1[i];
            }
            for (int i = 4; i < 8; i++)
            {
                retour[i] = tab2[i - 4];
            }
            return retour;
        }

        public int[] Recuperer_tableau(int[] tab)
        {
            int[] retour = new int[8];
            for (int i = 0; i < retour.Length; i++)
            {
                retour[i] = 0;
            }
            for (int i = 0; i < 4; i++)
            {
                retour[i] = tab[4 + i];
            }
            return retour;
        }

        public void QR_Code(string mot)
        {
            mot = mot.ToUpper();
            string[] tab = new string[mot.Length];

            if (mot.Length%2 == 0)
            {
                for (int i = 0; i < mot.Length; i += 2)
                {
                    int a = mot[i] ;
                    int b = mot[i + 1];

                    ConvertionHexa(a, b);
                    Console.WriteLine((int)a+" "+(int)b);
                }
            }
            else
            {
                for (int i = 0; i < mot.Length-1; i += 2)
                {
                    int a = mot[i];
                    int b = mot[i + 1];
                    ConvertionHexa(a, b);
                    Console.WriteLine((int)a +" " +(int)b);
                }
                ConvertionHexa(-1, (int)mot[mot.Length - 1]);
                Console.WriteLine("00 " + (int)mot[mot.Length-1]);
            }
            Console.ReadKey();
        }

        public int ConvertionHexa(int a, int b)
        {

            return 0;
        }


        /*
        public string Convert_Char_To_Binaire(char val1, char val2)
        {
            char[] tab = { val1, val2 };
            string tabr;
            tabr = getBinaire(val1) +" "+getBinaire(val2);
            return tabr;
        }

        public string getBinaire(char lettre)
        {
            string binaire = "";
            string retbinaire = "";
            for (int i = 0; i < 8; i++)
            {
                if ((lettre & 1) == 1) binaire += "1";
                else binaire += "0";
                lettre >>= 1;
            }

            for (int i = 0; i < 8; i++)
            {
                retbinaire += binaire[binaire.Length-1-i];
            }

            return retbinaire;
        }
        */
    }
}

