using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Utilities.Math
{
    public static class Prime
    {
        //TODO aggiungere descrizione
        public static List<int> PrimeFactors(int a)
        {
            List<int> retval = new List<int>();
            for (int b = 2; a > 1; b++)
            {
                while (a % b == 0)
                {
                    a /= b;
                    retval.Add(b);
                }
            }
            return retval;
        }

        // Calcolo del massimo comune divisore usando l'algoritmo euclideo
        public static int GCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
        
        // Calcolo del minimo comune multiplo
        public static int LCM(int a, int b)
        {
            return System.Math.Abs(a * b) / GCD(a, b);
        }
        // Metodo per calcolare il LCM di una lista di numeri
        public static int LCMOfList(List<int> numbers)
        {
            if (numbers == null || numbers.Count == 0)
                throw new ArgumentException("La lista non può essere vuota o nulla.");

            return numbers.Aggregate(LCM); // Riduci la lista calcolando LCM progressivi
        }
        
/*Il Minimo Comune Multiplo (MCM) di due numeri 𝑎 e 𝑏 è legato al loro Massimo Comune Divisore (MCD) tramite la seguente relazione matematica:
MCM(𝑎,𝑏)=∣𝑎⋅𝑏∣/MCD(𝑎,𝑏)
               
Perché questa relazione è vera? La spiegazione si basa sulle proprietà fondamentali dei divisori e della fattorizzazione in numeri primi:
        
    Fattorizzazione dei numeri: Ogni numero 𝑎 e 𝑏 può essere scritto come prodotto dei suoi fattori primi:
        𝑎 =𝑝1^𝑒1⋅𝑝2^𝑒2⋅…, 𝑏=𝑝1^𝑓1⋅𝑝2^𝑓2⋅…, ​
        dove 𝑝𝑖 sono i fattori primi, e 𝑒𝑖,𝑓𝑖 sono gli esponenti dei fattori per 𝑎 e 𝑏.

    MCD e MCM nella fattorizzazione: 
        Il MCD prende i minimi esponenti comuni tra 𝑎 e 𝑏
            MCD(𝑎,𝑏)=𝑝1^min⁡(𝑒1,𝑓1)⋅𝑝2^min(𝑒2,𝑓2)⋅…
                
        Il MCM prende i massimi esponenti comuni tra 𝑎 e 𝑏:
            MCM(𝑎,𝑏)=𝑝1^max(𝑒1,𝑓1)⋅𝑝2^max(𝑒2,𝑓2)⋅…
                
    Relazione tra MCD e MCM: Il prodotto 𝑎⋅𝑏 contiene tutti i fattori primi sia di 𝑎 che di 𝑏, con la somma degli esponenti di ciascun fattore. 
        In termini di MCD e MCM: 𝑎⋅𝑏=MCD(𝑎,𝑏)⋅MCM(𝑎,𝑏).
        Questo avviene perché i fattori di MCD(𝑎,𝑏) e MCM(𝑎,𝑏) si compensano per formare 𝑎 e 𝑏.

                
    Isolamento del MCM: Dividendo entrambi i membri dell'equazione 𝑎⋅𝑏=MCD(𝑎,𝑏)⋅MCM(𝑎,𝑏) per MCD(𝑎,𝑏), otteniamo:
        MCM(𝑎,𝑏)=𝑎⋅𝑏/MCD(𝑎,𝑏)
            
    Esempio pratico. Supponiamo 𝑎=12 e 𝑏=18
        Fattori primi: 12=2^2⋅3^1, 18=2^1⋅3^2.
                
        MCD: Prendiamo i minimi esponenti:
        MCD(12,18)=2^min⁡(2,1)⋅3^min⁡(1,2)=2^1*3^1=6.

        MCM: Prendiamo i massimi esponenti:
        MCM(12,18)=2^max(2,1)⋅3^max(1,2)=2^2*3^2=36.

        Verifica della formula:
        MCM(12,18)=12⋅18/MCD(12,18)=216/6=36.

    Conclusione. La relazione tra MCM, MCD e il prodotto è una conseguenza della struttura dei divisori comuni e della fattorizzazione in numeri primi. Usarla è molto efficiente nei calcoli numerici!


Perché a⋅b=MCD(a,b)⋅MCM(a,b)?
    La relazione 𝑎⋅𝑏=MCD(𝑎,𝑏)⋅MCM(𝑎,𝑏) deriva dalla teoria dei numeri ed è una proprietà fondamentale dei divisori e multipli comuni. Vediamola passo per passo in dettaglio.

        1. Fattorizzazione in numeri primi
            Ogni numero intero può essere scomposto nella sua fattorizzazione unica in numeri primi. Ad esempio:
                𝑎=2^𝑒⋅3^𝑓⋅5^𝑔⋅… 𝑏=2^𝑒′⋅3^𝑓′⋅5^𝑔′⋅…
 
            In questa forma:
                𝑒,𝑓,𝑔 sono gli esponenti di 𝑎 per ciascun fattore primo.
                𝑒′,𝑓′,𝑔′ sono gli esponenti di 𝑏 per ciascun fattore primo.
            
            Esempio: se 𝑎=12=2^2⋅3^1 e 𝑏=18=2^1⋅3^2, allora:
                Fattori comuni: 2,3
                Esponenti di 2: 𝑒=2, 𝑒′=1.
                Esponenti di 3: 𝑓=1, 𝑓′=2.

        2. Definizione di MCD
            Il Massimo Comune Divisore (MCD) prende i minimi esponenti per ciascun fattore primo comune:
                MCD(𝑎,𝑏)=2^min⁡(𝑒,𝑒′)⋅3^min⁡(𝑓,𝑓′)⋅…
            
            Ad esempio, per 𝑎=12 e 𝑏=18:
                Per il fattore 2:min⁡(2,1)=1.
                Per il fattore 3:min⁡(1,2)=1.
                    MCD(12,18)=2^1⋅3^1=6.

        3. Definizione di MCM
            Il Minimo Comune Multiplo (MCM) prende i massimi esponenti per ciascun fattore primo:
                MCM(𝑎,𝑏)=2^max⁡(𝑒,𝑒′)⋅3^max⁡(𝑓,𝑓′)⋅…
            
            Ad esempio, per 𝑎=12 e 𝑏=18:
                Per il fattore 2:max⁡(2,1)=2.
                Per il fattore 3:max⁡(1,2)=2.
                    MCM(12,18)=2^2⋅3^2=36.

        4. Prodotto di 𝑎 e 𝑏
            Il prodotto 𝑎⋅𝑏 considera tutti gli esponenti di ciascun fattore primo:
                𝑎⋅𝑏=(2^𝑒⋅3^𝑓⋅5^𝑔⋅…)⋅(2^𝑒′⋅3^𝑓′⋅5^𝑔′⋅…).
            Se sommiamo gli esponenti per ciascun fattore primo:
                𝑎⋅𝑏=2^𝑒+𝑒′⋅3^𝑓+𝑓′⋅5^𝑔+𝑔′⋅…

        5. Perché 𝑎⋅𝑏=MCD(𝑎,𝑏)⋅MCM(𝑎,𝑏)?
            Confrontiamo le fattorizzazioni:
                MCD: prende il minimo esponente per ogni fattore primo.
                MCM: prende il massimo esponente per ogni fattore primo.
                Il prodotto tra MCD e MCM somma gli esponenti:
                    MCD(𝑎,𝑏)⋅MCM(𝑎,𝑏)=(2^min(𝑒,𝑒′)⋅3^min(𝑓,𝑓′)⋅…)⋅(2^max⁡(𝑒,𝑒′)⋅3^max⁡(𝑓,𝑓′)⋅…).
                
                Poiché min⁡(𝑒,𝑒′)+max⁡(𝑒,𝑒′)=𝑒+𝑒′ (lo stesso per ogni fattore primo), il prodotto restituisce:
                    MCD(𝑎,𝑏)⋅MCM(𝑎,𝑏)=2^𝑒+𝑒′⋅3^𝑓+𝑓′⋅…=𝑎⋅𝑏.

    Esempio numerico.
        Riprendiamo 𝑎=12 e 𝑏=18:
            MCD(12,18)=6.
            MCM(12,18)=36.
            𝑎⋅𝑏=12⋅18=216.
            MCD(12,18)⋅MCM(12,18)=6⋅36=216.

        La relazione è verificata!

    Conclusione
        La relazione 𝑎⋅𝑏=MCD(𝑎,𝑏)⋅MCM(𝑎,𝑏) funziona perché il MCD e il MCM partizionano il prodotto 𝑎⋅𝑏 tra:
            I fattori comuni (MCD).
            I fattori restanti (MCM).



12 e 18 hanno fattori primi in comune, Cosa accade in altri casi?
    Vediamo cosa accade quando 𝑎 e 𝑏 hanno fattori primi non in comune.

    Caso di fattori primi diversi
        Supponiamo 𝑎=12 e 𝑏=35.

        𝑎=12=2^2⋅3^1
        𝑏=35=5^1⋅7^1
 
        Qui non ci sono fattori comuni tra 𝑎 e 𝑏: i fattori primi di 𝑎 (2 e 3) sono distinti dai fattori primi di 𝑏 (5 e 7).
    
        Calcolo di MCD
            Quando due numeri non condividono alcun fattore primo, il loro Massimo Comune Divisore (MCD) è semplicemente:
                MCD(𝑎,𝑏)=1.
        
        Calcolo di MCM
            Il Minimo Comune Multiplo (MCM) è il prodotto completo dei fattori primi di 𝑎 e 𝑏, cioè:
                MCM(𝑎,𝑏)=𝑎⋅𝑏=12⋅35=420.

        Verifica della relazione
            Secondo la formula 𝑎⋅𝑏=MCD(𝑎,𝑏)⋅MCM(𝑎,𝑏):   
                𝑎⋅𝑏=12⋅35=420,
                MCD(𝑎,𝑏)⋅MCM(𝑎,𝑏)=1⋅420=420.
            La relazione è verificata.


Cosa succede in generale con fattori primi diversi?
    Se 𝑎 e 𝑏 non hanno fattori primi in comune, allora:
        MCD(𝑎,𝑏)=1, MCM(𝑎,𝑏)=𝑎⋅𝑏.

    In questo caso, la relazione diventa banalmente:
        𝑎⋅𝑏=1⋅(𝑎⋅𝑏).


Caso misto: alcuni fattori in comune, altri diversi
    Ora vediamo un esempio più complesso, dove 𝑎 e 𝑏 condividono alcuni fattori primi, ma non tutti. 
    Supponiamo: 𝑎=60=2^2⋅3^1⋅5^1, 𝑏=45=3^2⋅5^1.

        Calcolo di MCD
            Il Massimo Comune Divisore (MCD) prende i minimi esponenti per i fattori comuni (qui 3 e 5):
                MCD(60,45)=3^min(1,2)⋅5^min(1,1)=3^1⋅5^1=15.

        Calcolo di MCM
            Il Minimo Comune Multiplo (MCM) prende i massimi esponenti di tutti i fattori (sia comuni che non comuni):
                MCM(60,45)=2^max(2,0)⋅3^max⁡(1,2)⋅5^max⁡(1,1)=2^2⋅3^2⋅5^1=180.

        Verifica della relazione. 
            Calcoliamo:
                𝑎⋅𝑏=60⋅45=2700, MCD(60,45)⋅MCM(60,45)=15⋅180=2700.

        La relazione è verificata anche in questo caso!

    Conclusione
        Quando i due numeri:
            Non condividono fattori primi: 
                MCD(𝑎,𝑏)=1, e MCM(𝑎,𝑏)=𝑎⋅𝑏.
            Condividono alcuni fattori primi: 
                MCD(𝑎,𝑏) e MCM(𝑎,𝑏) partizionano il prodotto 𝑎⋅𝑏 tra i fattori comuni e quelli restanti.

        La relazione 𝑎⋅𝑏=MCD(𝑎,𝑏)⋅MCM(𝑎,𝑏) vale sempre, indipendentemente dal numero e dalla natura dei fattori comuni o distinti.*/
    }
}
