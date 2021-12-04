using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using System.Xml.Linq;


public class Dictionnaire : MonoBehaviour
{

    Dico dictionnaire;
    private void Start()
    {
        dictionnaire = new Dico("Assets/XML/xml");
        lireDictionnaireDOM(dictionnaire.getCheminFichierDico(), "/dico.xml");
    }

    void lireDictionnaireDOM(string path, string filename)
    {
        Debug.Log("Start of lireDictionnaire");
        XDocument doc = XDocument.Load(path + filename);
        var ns = XNamespace.Get("http://myGame/tux");
        Debug.Log(doc.DescendantNodes().ToString());
        foreach (XElement item in doc.DescendantNodes())
        {
            Debug.Log("Iteration");
            dictionnaire.ajouteMotADico(System.Int32.Parse(item.Attribute("niveau").Value), item.Value.Trim());
        }

    }
}
public class Dico
{
    private List<string> listeNiveau1;
    private List<string> listeNiveau2;
    private List<string> listeNiveau3;
    private List<string> listeNiveau4;
    private List<string> listeNiveau5;
    private string cheminFichierDico;

    public Dico(string chemin)
    {
        this.cheminFichierDico = chemin;
        listeNiveau1 = new List<string>(5);
        listeNiveau2 = new List<string>(5);
        listeNiveau3 = new List<string>(5);
        listeNiveau4 = new List<string>(5);
        listeNiveau5 = new List<string>(5);
    }

    public string getMotDepuisListeNiveaux(int niveau)
    {
        switch (verifieNiveau(niveau))
        {
            case 1:
                return getMotDepuisListe(listeNiveau1);
            case 2:
                return getMotDepuisListe(listeNiveau2);
            case 3:
                return getMotDepuisListe(listeNiveau3);
            case 4:
                return getMotDepuisListe(listeNiveau4);
            case 5:
                return getMotDepuisListe(listeNiveau5);
            default:
                Debug.Log("Entrez le niveau entre 1 et 5!");
                return null;
        }

    }

    public void ajouteMotADico(int niveau, string mot)
    {
        switch (verifieNiveau(niveau))
        {
            case 1:
                listeNiveau1.Add(mot);
                break;
            case 2:
                listeNiveau2.Add(mot);
                break;
            case 3:
                listeNiveau3.Add(mot);
                break;
            case 4:
                listeNiveau4.Add(mot);
                break;
            case 5:
                listeNiveau5.Add(mot);
                break;
            default:
                Debug.Log("Ce n'est pas le bon niveau!");
                break;
        }
    }

    public string getCheminFichierDico()
    {
        return this.cheminFichierDico;
    }

    private int verifieNiveau(int niveau)
    {
        if (niveau > 0 && niveau < 6)
        {
            return niveau;
        }
        return 1;
    }

    private string getMotDepuisListe(List<string> list)
    {
        var rnd = Random.Range(0, list.Count - 1);
        return list[1];
    }



}
