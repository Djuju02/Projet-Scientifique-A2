using System;
using System.IO;


namespace TD_2
{//TD2_SALEH_Julien

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

            for (int i = 0; i < GetHauteur; i++)
            {
                for (int j = 0; j < GetLargeur; j++)
                {
                    this.matrice_image_RGB[i, j] = new Pixel(myfile[compteur], myfile[compteur + 1], myfile[compteur + 2]);
                    compteur += 3;
                }
            }


            //Emplacement 54

            /*
            Console.WriteLine("\n********************HEADER********************");
            for (int i = 0; i < 14; i++)
                Console.Write(myfile[i] + " ");
            //Métadonnées de l'image
            Console.WriteLine();
            Console.WriteLine("\n********************HEADER INFO********************");
            for (int i = 14; i < 54; i++)
                Console.Write(myfile[i] + " ");
            */

            //L'image elle-même

            /*
            Console.WriteLine("\n IMAGE");
            for (int i = 54; i < myfile.Length; i += GetLargeur)
            {
                for (int j = i; j < i + GetLargeur; j++)
                {
                    Console.Write("{0:D3} ", myfile[j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            */
        }

        /*
        //Si jamais on veut copier l'image;
        public MyImage(MyImage image_1)
        {
            this.image = image_1;
        }
        */


        public string GetTypeDeFichier
        {
            get { return this.typedefichier; }
            set { this.typedefichier = value; }
        }
        public int GetTaille_fichier
        {
            get { return this.taille_fichier; }
            set { this.taille_fichier = value; }
        }
        public int GetTaille_offset
        {
            get { return this.taille_offset; }
            set { this.taille_offset = value; }
        }
        public int GetLargeur
        {
            get { return this.largeur; }
            set { this.largeur = value; }
        }
        public int GetHauteur
        {
            get { return this.hauteur; }
            set { this.hauteur = value; }
        }
        public int GetNb_bits_couleur
        {
            get { return this.nb_bits_couleur; }
            set { this.nb_bits_couleur = value; }
        }
        public Pixel[,] GetMatrice_image_RGB
        {
            get { return this.matrice_image_RGB; }
            set { this.matrice_image_RGB = value; }
        }

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
        public void From_Image_To_File(string myfile)
        {
            int add = 0;
            //Modulo 4
            if (4 - (GetLargeur % 4) == 1)
            {
                add += 3;
            }
            if (4 - (GetLargeur % 4) == 2)
            {
                add += 2;
            }
            if (4 - (GetLargeur % 4) == 3)
            {
                add += 1;
            }


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

        public void Informations()
        {
            Console.WriteLine("La type de fichier est " + GetTypeDeFichier);
            Console.WriteLine("La taille du fichier est de " + GetTaille_fichier + " octets");
            Console.WriteLine("L'offset à partir duquel commence les données brutes de l'image est " + GetTaille_offset);
            Console.WriteLine("La résolution verticale de l'image est de " + GetHauteur + " pixels");
            Console.WriteLine("La résolution horizontale de l'image est de " + GetLargeur + " pixels");
            Console.WriteLine("Le nombre de bits par couleur est de " + GetNb_bits_couleur + " bits");
        }

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

            Pixel[,] new_matrix = new Pixel[new_hauteur, new_largeur];
            Pixel p = new Pixel(249, 66, 158); //PRANKED
            int[] new_center = { new_matrix.GetLength(0) / 2, new_matrix.GetLength(1) / 2 };
            for (int i = 0; i < new_matrix.GetLength(0); i++)
            {
                for (int j = 0; j < new_matrix.GetLength(1); j++)
                {
                    double rayon = Math.Sqrt(Math.Pow(i - new_center[0], 2) + Math.Pow(j - new_center[1], 2));
                    double theta = Math.Atan2(j - new_center[1], i - new_center[0]) - angle;
                    int new_x = (int)(center[0] + rayon * Math.Cos(theta));
                    int new_y = (int)(center[1] + rayon * Math.Sin(theta));
                    if (new_x >= 0 && new_x < GetMatrice_image_RGB.GetLength(0) && new_y >= 0 && new_y < GetMatrice_image_RGB.GetLength(1))
                    {
                        new_matrix[i, j] = GetMatrice_image_RGB[new_x, new_y];
                    }
                    else
                    {
                        new_matrix[i, j] = p;
                    }

                }
            }

            GetHauteur = new_hauteur;
            GetLargeur = new_largeur;
            GetMatrice_image_RGB = new_matrix;
            GetTaille_fichier = GetHauteur * GetLargeur * 3;

            From_Image_To_File(filename);
        }

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

    }
}
