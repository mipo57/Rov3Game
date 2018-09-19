using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunicationDataInterpret : MonoBehaviour {

    public ROVModel boat;

	public void DataInterpretation(int [] arrayOfVar)
    {
        /*
        PredkoscZadanaX, PredkoscZadanaY, PredkoscZadanaZ,
        PredkoscKatowaZadanaX, PredkoscKatowaZadanaY, PredkoscKatowaZadanaZ,
        ObecnaPredkoscZadanaX, ObecnaPredkoscZadanaY, ObecnaPredkoscZadanaZ,
        ObecnaPredkoscKatowaZadanaX, ObecnaPredkoscKatowaZadanaY, ObecnaPredkoscKatowaZadanaZ,
        ObecnePolozenieKatoweX,ObecnePolozenieKotoweY,ObecnePolozenieKatoweZ,

        6 stopni swobody manipulatora + chwytak
        */
        const int indexOf_PredkoscZadanaX = 0;
        const int indexOf_PredkoscKatowaZadana = 3;

        boat.setVelocity(new Vector3(arrayOfVar[2]/10, arrayOfVar[0]/10, arrayOfVar[1]/10));
        boat.setAngularVelocityYaxis( arrayOfVar[indexOf_PredkoscKatowaZadana]/50);

    }

    public int[] dataFormBoat()
    {
        /*
        czyPrzeciek,Cisnienie,Temperatura
        ObecnaPredkoscZadanaX, ObecnaPredkoscZadanaY, ObecnaPredkoscZadanaZ,
        ObecnaPredkoscKatowaZadanaX, ObecnaPredkoscKatowaZadanaY, ObecnaPredkoscKatowaZadanaZ,
        ObecnePolozenieKatoweX, ObecnePolozenieKotoweY, ObecnePolozenieKatoweZ,
        */

        int[] data = new int[12];
        data[0] = 0; // isLeak
        data[1] = (int)(boat.getDepth() * 998 * 9.81); // h* ro * g //pressure
        data[2] = 25; // temperature
        
        data[3] = (int)( boat.getLinearVelocity().x);//v X
        data[4] = (int)(boat.getLinearVelocity().y);//v y
        data[5] = (int)(boat.getLinearVelocity().z);//v 

        data[6] = (int)(boat.getAngularVelocity().x);//v X
        data[7] = (int)(boat.getAngularVelocity().y);//v y
        data[8] = (int)(boat.getAngularVelocity().z);//v z

        data[9] = (int)(boat.getAngularPosition().x);//v X
        data[10] = (int)(boat.getAngularPosition().y);//v y
        data[11] = (int)(boat.getAngularPosition().z);//v z

        return data;
    }
    public int get_depth()
    {
        return (int)(boat.getDepth());
    }

    public int get_rotation()
    {
        return 10*(int)boat.getAngularPosition().y;
    }
}
