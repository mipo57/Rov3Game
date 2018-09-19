using UnityEngine;

public class Task1_Bramka : MonoBehaviour {

    private bool bramka_szary = false;
    private bool bramka_czerwony = false;
    private bool zakazany = false;

    private int podBramka = 0;

    public int Score = 0;

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "bramka_szary":
                bramka_szary = true;
                podBramka++;
                break;
            case "bramka_czerwony":
                bramka_czerwony = true;
                podBramka++;
                break;
            case "poza_bramka":
                zakazany = true;
                podBramka++;
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        podBramka--;
        if(podBramka == 0)
        {
            if (zakazany)
                Score = -9000;
            else
            {
                switch (bramka_szary ^ bramka_czerwony)
                {
                    case true:
                        Score += 150;
                        goto case false;
                    case false:
                        Score += 100;
                        break;
                }
            }
        }
    }
}
