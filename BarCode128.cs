// Original code VBA by
// Auteur:    Joffrey VERDIER
// Date :     08/2006
// Légal:     OpenSource © 2007 AVRANCHES
//
//modified and translate to C# by:
//Leonardo N M 
// date: 02/2021
// 
// to use this code you need to install a barcode font 128 (.ttf)
// 
using System;


public class BarCode128
{
    public string Encodes(string chaine) //string is the 'string' to be converted to barcode
    {
        int ind = 1;
        int checksum = 0;
        int mini;
        int dummy;
        bool tableB;
        string code128;
        int longueur;

        code128 = "";
        longueur = chaine.Length;

        if (longueur == 0)
        {
            //Console.WriteLine(@"\n chaine vide");
            //MessageBox.Show(" chaine vide");
        }
        else
        {
            for (ind = 0; ind <= longueur - 1; ind++)
            {
                if ((int)System.Convert.ToChar(chaine[ind]) < 32 | (int)System.Convert.ToChar(chaine[ind]) > 126)
                {
                }

            }
        }

        tableB = true;
        ind = 0;

        while (ind < longueur)
        {
            if (tableB == true)
            {
                if (ind == 0 | (ind + 3) == longueur - 1)
                {
                    mini = 4;
                }
                else
                {
                    mini = 6;
                }

                mini = mini - 1;

                if ((ind + mini) <= (longueur - 1))
                {
                    while (mini >= 0)
                    {
                        if ((int)System.Convert.ToChar(chaine[ind + mini]) < 48 | (int)System.Convert.ToChar(chaine[ind + mini]) > 57)
                        {
                            break;
                        }
                        mini = mini - 1;
                    }
                }

                if (mini < 0)
                {
                    if (ind == 0)
                    {
                        code128 = char.ToString(Convert.ToChar(205));
                    }
                    else
                    {
                        code128 = code128 + char.ToString(Convert.ToChar(199));
                    }

                    tableB = false;

                }
                else if (ind == 0)
                {
                    code128 = char.ToString(Convert.ToChar(204));
                }

            }

            if (tableB == false)
            {
                mini = 2;
                mini = mini - 1;
                if ((ind + mini) < longueur)
                {
                    while (mini >= 0)
                    {
                        if ((int)System.Convert.ToChar(chaine[ind + mini]) < 48 | (int)System.Convert.ToChar(chaine[ind]) > 57)
                        {
                            break;
                        }

                        mini = mini - 1;
                    }
                }

                if (mini < 0)
                {
                    dummy = int.Parse(chaine.Substring(ind, 2));
                    if (dummy < 95)
                    {
                        dummy = dummy + 32;
                    }
                    else
                    {
                        dummy = dummy + 100;
                    }

                    code128 = code128 + Convert.ToChar(dummy);
                    ind = ind + 2;
                }
                else
                {
                    code128 = code128 + Convert.ToChar(200).ToString();
                    tableB = true;
                }
            }


            if (tableB == true)
            {
                code128 = code128 + chaine[ind];
                ind = ind + 1;
            }
        }

        for (ind = 0; ind <= code128.Length - 1; ind++)
        {
            dummy = (int)System.Convert.ToChar(code128[ind]);
            if (dummy < 127)
            {
                dummy = dummy - 32;
            }
            else
            {
                dummy = dummy - 100;
            }

            if (ind == 0)
            {
                checksum = dummy;
            }

            checksum = (checksum + (ind) * dummy) % 103;
        }

        if (checksum < 95)
        {
            checksum = checksum + 32;
        }
        else
        {
            checksum = checksum + 100;
        }

        code128 = code128 + char.ToString(Convert.ToChar(checksum)) + char.ToString(Convert.ToChar(206));

        code128 = code128.Replace(' ', 'Â');

        return code128;
    }
}