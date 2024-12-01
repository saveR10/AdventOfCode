using AOC;
using AOC.DataStructures.Clustering;
using AOC.Documents.LINQ;
using AOC.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace AOC2023
{
    //DA SISTEMARE
    public class Day5 : Solver, IDay
    {
        String[] result;
        string string_seeds = "432563865 39236501 1476854973 326201032 1004521373 221995697 2457503679 46909145 603710475 11439698 1242281714 12935671 2569215463 456738587 3859706369 129955069 3210146725 618372750 601583464 1413192";
        string seed_to_soil_map = "2824905526 2969131334 898611144\r\n0 322319732 9776277\r\n379216444 692683038 140400417\r\n3723516670 1559827635 9493936\r\n637824014 332096009 211964909\r\n929691047 1569321571 35824014\r\n965515061 1605145585 1281263183\r\n621118546 833083455 16705468\r\n9776277 0 322319732\r\n332096009 544060918 47120435\r\n3733010606 1319171816 134731872\r\n2329500810 1453903688 72388062\r\n2401888872 1526291750 33535885\r\n2246778244 2886408768 82722566\r\n2435424757 929691047 389480769\r\n519616861 591181353 101501685";
        string seil_to_fertilizer_map = "2819195624 2690204780 252557843\r\n1098298904 1339121422 10546957\r\n499510245 852292683 97183057\r\n4225167944 2372810194 69799352\r\n887408376 1808006977 56225538\r\n3071753467 4058417302 4632491\r\n2455749676 3452467754 363445948\r\n2243979338 3846646964 211770338\r\n3076385958 4063049793 231917503\r\n3860856553 2269547568 103262626\r\n943633914 21067281 39980869\r\n1388096097 1584043708 223963269\r\n3493291351 2442609546 247595234\r\n1801722612 949475740 41203502\r\n264653767 678191385 102026845\r\n3740886585 2942762623 89236706\r\n1152866424 310132079 235229673\r\n3830123291 3815913702 30733262\r\n249083929 1568473870 15569838\r\n1612059366 1506703078 61770792\r\n366680612 545361752 132829633\r\n1108845861 1992124969 44020563\r\n596693302 1048406348 290715074\r\n3467723121 2243979338 25568230\r\n1879110833 1349668379 157034699\r\n1858043552 0 21067281\r\n3308303461 3293048094 159419660\r\n3964119179 3031999329 261048765\r\n983614783 780218230 72074453\r\n1055689236 1005796680 42609668\r\n1673830158 1864232515 127892454\r\n1842926114 990679242 15117438\r\n0 61048150 249083929";
        string fertilizer_to_water_map = "0 434502471 470583313\r\n1739362496 1919893972 48874906\r\n2735409723 1968768878 16148586\r\n3324522082 1858151799 61742173\r\n4137416965 1984917464 25502361\r\n470583313 2971682591 186824423\r\n2751558309 2532295557 111781313\r\n3664705516 1340674299 435773845\r\n1851374390 3898529330 80666020\r\n857422234 2010419825 207606184\r\n1788237402 2418040507 63136988\r\n1065028418 0 434502471\r\n1499530889 2481177495 4578648\r\n4162919326 2916167530 55515061\r\n1670138099 3158507014 69224397\r\n3386264255 905085784 278441261\r\n2496170686 3979195350 239239037\r\n4100479361 2879229926 36937604\r\n1504109537 3227731411 166028562\r\n657407736 2218026009 200014498\r\n2909879036 3393759973 257495792\r\n1932040410 2644076870 235153056\r\n2167193466 3651255765 247273565\r\n2863339622 2485756143 46539414\r\n3167374828 1183527045 157147254\r\n2414467031 1776448144 81703655";
        string water_to_light_map = "894548549 593866955 6252040\r\n3168871398 327816880 11668092\r\n3549766643 2935057349 16258370\r\n1070236274 1304104659 106353135\r\n900800589 723222576 35881223\r\n2175309976 2744985745 82677694\r\n3969615926 3819543751 36919401\r\n4086160816 4015453657 208806480\r\n0 360262147 233604808\r\n3566025013 1514177176 87956572\r\n3440195928 1874477761 44575925\r\n1851853101 948479332 113304167\r\n2969520516 3389635913 199350882\r\n1504406289 1061783499 137386237\r\n623007058 1630426233 244051528\r\n1749186436 620555911 102666665\r\n2154532801 339484972 20777175\r\n1965157268 759103799 189375533\r\n4007153882 3758595179 60948572\r\n3180539490 2951315719 231363953\r\n233604808 1471149229 43027947\r\n276632755 1919053686 114911374\r\n1176589409 0 327816880\r\n867058586 1410457794 27489963\r\n391544129 1199169736 4581933\r\n603082303 600118995 19924755\r\n4068102454 4276290379 18058362\r\n3810625421 3856463152 158990505\r\n2969008355 620043750 512161\r\n3758595179 4224260137 52030242\r\n396126062 3182679672 206956241\r\n4006535327 4294348741 618555\r\n3484771853 3588986795 64994790\r\n2257987670 2033965060 711020685\r\n1641792526 2827663439 107393910\r\n3411903443 1602133748 28292485\r\n1037034802 1437947757 33201472\r\n936681812 1203751669 100352990";
        string light_to_temperature_map = "1726863959 864157287 834947717\r\n263199301 190436173 53620398\r\n1393417259 1699105004 333446700\r\n2783912856 244056571 155961192\r\n2939874048 400017763 299945457\r\n671449939 2517852185 721967320\r\n2561811676 2295751005 222101180\r\n0 2032551704 263199301\r\n481013766 0 190436173\r\n316819699 699963220 164194067";
        string temperature_to_humidity_map = "603287260 3766826980 8741130\r\n572607531 3684982838 30679729\r\n2084038135 1101548002 100083930\r\n655933651 3228345771 56278566\r\n1881393627 553997241 168332584\r\n553997241 2882185871 18610290\r\n627184746 1072799097 28748905\r\n612028390 3397056204 15156356\r\n1693489030 1430646491 187904597\r\n3039118107 1734352525 2023479\r\n220345266 0 43042720\r\n840454312 3775568110 147781659\r\n2184122065 3715662567 51164413\r\n317040325 171240045 2422893\r\n3245373536 4158663426 136303870\r\n145773749 385599932 74571517\r\n0 43042720 128197325\r\n1490020808 4094893831 63769595\r\n319463218 173662938 140708231\r\n712212217 1821839294 128242095\r\n128197325 314371169 17576424\r\n2474405575 3923349769 171544062\r\n2352890439 3105028111 121515136\r\n3467140696 3318936261 78119943\r\n2235286478 3226543247 1802524\r\n4084196651 722329825 210770645\r\n1553790403 933100470 139698627\r\n1261006249 1201631932 229014559\r\n263387986 331947593 53652339\r\n3137034338 2900796161 108339198\r\n988235971 3412212560 272770278\r\n2237089002 1618551088 115801437\r\n3381677406 1736376004 85463290\r\n3545260639 1950081389 538936012\r\n2049726211 3284624337 34311924\r\n2645949637 2489017401 393168470\r\n3041141586 3009135359 95892752";
        string humidity_to_location_map = "596652260 530461632 95173962\r\n3845096173 1731990943 158117085\r\n2243878974 1890108028 393769632\r\n0 625635594 63651375\r\n1920725725 753532949 155684321\r\n63651375 329652856 200808776\r\n264460151 0 60175490\r\n1381444473 2283877660 346420873\r\n4003213258 3594530530 47694548\r\n548036821 60175490 46076186\r\n4100105147 1678246429 53744514\r\n2637648606 3642225078 292412911\r\n324635641 106251676 223401180\r\n4050907806 3545333189 49197341\r\n4153849661 909217270 141117635\r\n3455132509 3477799963 67533226\r\n594113007 689286969 2539253\r\n2930061517 2630298533 525070992\r\n753532949 1050334905 627911524\r\n3522665735 3155369525 322430438\r\n2076410046 3934637989 167468928\r\n1727865346 4102106917 192860379";
        //string string_seeds = "79 14 55 13";
        //string seed_to_soil_map = "50 98 2\r\n52 50 48";
        //string seil_to_fertilizer_map = "0 15 37\r\n37 52 2\r\n39 0 15";
        //string fertilizer_to_water_map = "49 53 8\r\n0 11 42\r\n42 0 7\r\n57 7 4";
        //string water_to_light_map = "88 18 7\r\n18 25 70";
        //string light_to_temperature_map = "45 77 23\r\n81 45 19\r\n68 64 13";
        //string temperature_to_humidity_map = "0 69 1\r\n1 0 69";
        //string humidity_to_location_map = "60 56 37\r\n56 93 4";

        //String[] delimiters = { "\r\n", " " };
        //String[] delimiter_space = { " " };
        //String[] delimiters_line = { "\r\n" };
        public static String[] delimiters = { "\r\n", " " };
        public static String[] delimiter_space = { " " };
        public static String[] delimiters_line = { "\r\n" };
        
        List<long> seeds = new List<long>();
        List<long> soils = new List<long>();
        List<long> fertilizers = new List<long>();
        List<long> waters = new List<long>();
        List<long> lights = new List<long>();
        List<long> temperatures = new List<long>();
        List<long> humidities = new List<long>();
        List<long> locations = new List<long>();
        public void Part1(object input, bool test, ref object solution)
        {
            PopulateList(seeds, string_seeds);
            Transform(seeds, soils, seed_to_soil_map);
            Transform(soils, fertilizers, seil_to_fertilizer_map);
            Transform(fertilizers, waters, fertilizer_to_water_map);
            Transform(waters, lights, water_to_light_map);
            Transform(lights, temperatures, light_to_temperature_map);
            Transform(temperatures, humidities, temperature_to_humidity_map);
            Transform(humidities, locations, humidity_to_location_map);
            long min = locations.Min();
        }
        public void PopulateList(List<long> list, string string_list)
        {
            foreach (string item in string_list.Split(delimiters, StringSplitOptions.None))
            {
                list.Add(long.Parse(item));
            }
        }
        public  void Transform(List<long> list_source, List<long> list_destination, string string_list)
        {
            foreach (long item_source in list_source)
            {
                bool trovato = false;
                foreach (string line in string_list.Split(delimiters_line, StringSplitOptions.None))
                {
                    long source = long.Parse(line.Split(delimiter_space, StringSplitOptions.None)[1]);
                    long dest = long.Parse(line.Split(delimiter_space, StringSplitOptions.None)[0]);
                    long range = long.Parse(line.Split(delimiter_space, StringSplitOptions.None)[2]);

                    if (item_source >= source && item_source <= source + range - 1)
                    {
                        list_destination.Add(dest + (item_source - source));
                        trovato = true;
                        break;
                    }
                }
                if (!trovato)
                {
                    list_destination.Add(item_source);
                }

            }


        }

        public void Part2(object input, bool test, ref object solution)
        {
            List<GenericResource> Resources = new List<GenericResource>();

            test = false;

            string string_seeds;
            string seed_to_soil_map;
            string soil_to_fertilizer_map;
            string fertilizer_to_water_map;
            string water_to_light_map;
            string light_to_temperature_map;
            string temperature_to_humidity_map;
            string humidity_to_location_map;

            if (!test)
            {
                string_seeds = "432563865 39236501 1476854973 326201032 1004521373 221995697 2457503679 46909145 603710475 11439698 1242281714 12935671 2569215463 456738587 3859706369 129955069 3210146725 618372750 601583464 1413192";
                seed_to_soil_map = "2824905526 2969131334 898611144\r\n0 322319732 9776277\r\n379216444 692683038 140400417\r\n3723516670 1559827635 9493936\r\n637824014 332096009 211964909\r\n929691047 1569321571 35824014\r\n965515061 1605145585 1281263183\r\n621118546 833083455 16705468\r\n9776277 0 322319732\r\n332096009 544060918 47120435\r\n3733010606 1319171816 134731872\r\n2329500810 1453903688 72388062\r\n2401888872 1526291750 33535885\r\n2246778244 2886408768 82722566\r\n2435424757 929691047 389480769\r\n519616861 591181353 101501685";
                soil_to_fertilizer_map = "2819195624 2690204780 252557843\r\n1098298904 1339121422 10546957\r\n499510245 852292683 97183057\r\n4225167944 2372810194 69799352\r\n887408376 1808006977 56225538\r\n3071753467 4058417302 4632491\r\n2455749676 3452467754 363445948\r\n2243979338 3846646964 211770338\r\n3076385958 4063049793 231917503\r\n3860856553 2269547568 103262626\r\n943633914 21067281 39980869\r\n1388096097 1584043708 223963269\r\n3493291351 2442609546 247595234\r\n1801722612 949475740 41203502\r\n264653767 678191385 102026845\r\n3740886585 2942762623 89236706\r\n1152866424 310132079 235229673\r\n3830123291 3815913702 30733262\r\n249083929 1568473870 15569838\r\n1612059366 1506703078 61770792\r\n366680612 545361752 132829633\r\n1108845861 1992124969 44020563\r\n596693302 1048406348 290715074\r\n3467723121 2243979338 25568230\r\n1879110833 1349668379 157034699\r\n1858043552 0 21067281\r\n3308303461 3293048094 159419660\r\n3964119179 3031999329 261048765\r\n983614783 780218230 72074453\r\n1055689236 1005796680 42609668\r\n1673830158 1864232515 127892454\r\n1842926114 990679242 15117438\r\n0 61048150 249083929";
                fertilizer_to_water_map = "0 434502471 470583313\r\n1739362496 1919893972 48874906\r\n2735409723 1968768878 16148586\r\n3324522082 1858151799 61742173\r\n4137416965 1984917464 25502361\r\n470583313 2971682591 186824423\r\n2751558309 2532295557 111781313\r\n3664705516 1340674299 435773845\r\n1851374390 3898529330 80666020\r\n857422234 2010419825 207606184\r\n1788237402 2418040507 63136988\r\n1065028418 0 434502471\r\n1499530889 2481177495 4578648\r\n4162919326 2916167530 55515061\r\n1670138099 3158507014 69224397\r\n3386264255 905085784 278441261\r\n2496170686 3979195350 239239037\r\n4100479361 2879229926 36937604\r\n1504109537 3227731411 166028562\r\n657407736 2218026009 200014498\r\n2909879036 3393759973 257495792\r\n1932040410 2644076870 235153056\r\n2167193466 3651255765 247273565\r\n2863339622 2485756143 46539414\r\n3167374828 1183527045 157147254\r\n2414467031 1776448144 81703655";
                water_to_light_map = "894548549 593866955 6252040\r\n3168871398 327816880 11668092\r\n3549766643 2935057349 16258370\r\n1070236274 1304104659 106353135\r\n900800589 723222576 35881223\r\n2175309976 2744985745 82677694\r\n3969615926 3819543751 36919401\r\n4086160816 4015453657 208806480\r\n0 360262147 233604808\r\n3566025013 1514177176 87956572\r\n3440195928 1874477761 44575925\r\n1851853101 948479332 113304167\r\n2969520516 3389635913 199350882\r\n1504406289 1061783499 137386237\r\n623007058 1630426233 244051528\r\n1749186436 620555911 102666665\r\n2154532801 339484972 20777175\r\n1965157268 759103799 189375533\r\n4007153882 3758595179 60948572\r\n3180539490 2951315719 231363953\r\n233604808 1471149229 43027947\r\n276632755 1919053686 114911374\r\n1176589409 0 327816880\r\n867058586 1410457794 27489963\r\n391544129 1199169736 4581933\r\n603082303 600118995 19924755\r\n4068102454 4276290379 18058362\r\n3810625421 3856463152 158990505\r\n2969008355 620043750 512161\r\n3758595179 4224260137 52030242\r\n396126062 3182679672 206956241\r\n4006535327 4294348741 618555\r\n3484771853 3588986795 64994790\r\n2257987670 2033965060 711020685\r\n1641792526 2827663439 107393910\r\n3411903443 1602133748 28292485\r\n1037034802 1437947757 33201472\r\n936681812 1203751669 100352990";
                light_to_temperature_map = "1726863959 864157287 834947717\r\n263199301 190436173 53620398\r\n1393417259 1699105004 333446700\r\n2783912856 244056571 155961192\r\n2939874048 400017763 299945457\r\n671449939 2517852185 721967320\r\n2561811676 2295751005 222101180\r\n0 2032551704 263199301\r\n481013766 0 190436173\r\n316819699 699963220 164194067";
                temperature_to_humidity_map = "603287260 3766826980 8741130\r\n572607531 3684982838 30679729\r\n2084038135 1101548002 100083930\r\n655933651 3228345771 56278566\r\n1881393627 553997241 168332584\r\n553997241 2882185871 18610290\r\n627184746 1072799097 28748905\r\n612028390 3397056204 15156356\r\n1693489030 1430646491 187904597\r\n3039118107 1734352525 2023479\r\n220345266 0 43042720\r\n840454312 3775568110 147781659\r\n2184122065 3715662567 51164413\r\n317040325 171240045 2422893\r\n3245373536 4158663426 136303870\r\n145773749 385599932 74571517\r\n0 43042720 128197325\r\n1490020808 4094893831 63769595\r\n319463218 173662938 140708231\r\n712212217 1821839294 128242095\r\n128197325 314371169 17576424\r\n2474405575 3923349769 171544062\r\n2352890439 3105028111 121515136\r\n3467140696 3318936261 78119943\r\n2235286478 3226543247 1802524\r\n4084196651 722329825 210770645\r\n1553790403 933100470 139698627\r\n1261006249 1201631932 229014559\r\n263387986 331947593 53652339\r\n3137034338 2900796161 108339198\r\n988235971 3412212560 272770278\r\n2237089002 1618551088 115801437\r\n3381677406 1736376004 85463290\r\n3545260639 1950081389 538936012\r\n2049726211 3284624337 34311924\r\n2645949637 2489017401 393168470\r\n3041141586 3009135359 95892752";
                humidity_to_location_map = "596652260 530461632 95173962\r\n3845096173 1731990943 158117085\r\n2243878974 1890108028 393769632\r\n0 625635594 63651375\r\n1920725725 753532949 155684321\r\n63651375 329652856 200808776\r\n264460151 0 60175490\r\n1381444473 2283877660 346420873\r\n4003213258 3594530530 47694548\r\n548036821 60175490 46076186\r\n4100105147 1678246429 53744514\r\n2637648606 3642225078 292412911\r\n324635641 106251676 223401180\r\n4050907806 3545333189 49197341\r\n4153849661 909217270 141117635\r\n3455132509 3477799963 67533226\r\n594113007 689286969 2539253\r\n2930061517 2630298533 525070992\r\n753532949 1050334905 627911524\r\n3522665735 3155369525 322430438\r\n2076410046 3934637989 167468928\r\n1727865346 4102106917 192860379";
            }
            else
            {
                string_seeds = "79 14 55 13";
                //seed_to_soil_map = "50 98 2\r\n52 50 48";
                seed_to_soil_map = "50 82 3\r\n52 50 48";
                soil_to_fertilizer_map = "0 15 37\r\n37 52 2\r\n39 0 15";
                fertilizer_to_water_map = "49 53 8\r\n0 11 42\r\n42 0 7\r\n57 7 4";
                water_to_light_map = "88 18 7\r\n18 25 70";
                light_to_temperature_map = "45 77 23\r\n81 45 19\r\n68 64 13";
                temperature_to_humidity_map = "0 69 1\r\n1 0 69";
                humidity_to_location_map = "60 56 37\r\n56 93 4";
            }


            for (int i = 0; i < string_seeds.Split(delimiter_space, StringSplitOptions.None).Count(); i += 2)
            {
                GenericResource genericResource = new GenericResource(BigInteger.Parse(string_seeds.Split(delimiter_space, StringSplitOptions.None)[i]), BigInteger.Parse(string_seeds.Split(delimiter_space, StringSplitOptions.None)[i + 1]));
                Resources.Add(genericResource);

            }
            BigInteger min = 999999999999999999;


            for (int i = 0; i < Resources.Count(); i++)
            {
                TransformB(Resources[i], seed_to_soil_map, Resources);
            }
            for (int i = 0; i < Resources.Count(); i++)
            {
                TransformB(Resources[i], soil_to_fertilizer_map, Resources);
            }
            for (int i = 0; i < Resources.Count(); i++)
            {
                TransformB(Resources[i], fertilizer_to_water_map, Resources);
            }
            for (int i = 0; i < Resources.Count(); i++)
            {
                TransformB(Resources[i], water_to_light_map, Resources);
            }
            for (int i = 0; i < Resources.Count(); i++)
            {
                TransformB(Resources[i], light_to_temperature_map, Resources);
            }
            for (int i = 0; i < Resources.Count(); i++)
            {
                TransformB(Resources[i], temperature_to_humidity_map, Resources);
            }
            for (int i = 0; i < Resources.Count(); i++)
            {
                TransformB(Resources[i], humidity_to_location_map, Resources);
            }

            foreach (GenericResource res in Resources)
            {
                if (res.start < min) min = res.start;
            }
            //if (Resources[i].start < min) min = Resources[i].start;
            //  Resources.RemoveAt(i);

        }
        public void TransformB(GenericResource Resource, string transform_rule, List<GenericResource> Resources)
        {
            bool trovato = false;
            for (int i = 0; i < transform_rule.Split(delimiters_line, StringSplitOptions.None).Count(); i++)
            {
                if (!trovato)
                {
                    typeOverlap to = VerifyOverlappingB(Resource, transform_rule.Split(delimiters_line, StringSplitOptions.None)[i]);
                    switch (to)
                    {
                        case typeOverlap.None:
                            break;
                        case typeOverlap.Partial:
                            Resource.PartialOverlap(Resource, transform_rule.Split(delimiters_line, StringSplitOptions.None)[i], Resources);
                            trovato = true;
                            break;
                        case typeOverlap.Total:
                            Resource.TotaleOverlap(Resource, transform_rule.Split(delimiters_line, StringSplitOptions.None)[i]);
                            trovato = true;
                            break;
                        case typeOverlap.OverPartial:
                            Resource.OverPartialOverlap(Resource, transform_rule.Split(delimiters_line, StringSplitOptions.None)[i], Resources);
                            trovato = true;
                            break;
                    }
                }
            }
        }
        public typeOverlap VerifyOverlappingB(GenericResource genericResource, string transform_rule)
        {
            typeOverlap ret;
            BigInteger start_rule = ConvertNumberB(transform_rule, 1);
            BigInteger end_rule = ConvertNumberB(transform_rule, 1) + ConvertNumberB(transform_rule, 2) - 1;
            if (genericResource.end < start_rule || genericResource.start > end_rule) ret = typeOverlap.None;
            else if (genericResource.start >= start_rule && genericResource.end <= end_rule) ret = typeOverlap.Total;
            else if (genericResource.start < start_rule && genericResource.end > end_rule) ret = typeOverlap.OverPartial;
            else ret = typeOverlap.Partial;
            return ret;
        }
        public enum typeOverlap
        {
            None,
            Partial,
            OverPartial,
            Total
        }
        public static BigInteger ConvertNumberB(string transform_rule, int i)
        {
            return BigInteger.Parse(transform_rule.Split(delimiter_space, StringSplitOptions.None)[i]);
        }
        public class GenericResource
        {
            public GenericResource(BigInteger start, BigInteger range)
            {
                this.start = start;
                this.end = start + range - 1;
                this.range = range;
            }
            public void OverPartialOverlap(GenericResource genericResource, string transform_rule, List<GenericResource> Resources)
            {
                BigInteger start_rule = BigInteger.Parse(transform_rule.Split(delimiter_space, StringSplitOptions.None)[1]);
                BigInteger end_rule = BigInteger.Parse(transform_rule.Split(delimiter_space, StringSplitOptions.None)[1]) + BigInteger.Parse(transform_rule.Split(delimiter_space, StringSplitOptions.None)[2]) - 1;
                BigInteger start_destination = ConvertNumberB(transform_rule, 0);
                BigInteger range = ConvertNumberB(transform_rule, 2);
                Resources.Add(new GenericResource(genericResource.start, start_rule - genericResource.start));
                Resources.Add(new GenericResource(end_rule + 1, genericResource.end - end_rule));
                genericResource.start = start_destination;
                genericResource.end = start_destination + range - 1;
                genericResource.range = genericResource.end - genericResource.start + 1;
            }

            public void PartialOverlap(GenericResource genericResource, string transform_rule, List<GenericResource> Resources)
            {
                BigInteger start_rule = BigInteger.Parse(transform_rule.Split(delimiter_space, StringSplitOptions.None)[1]);
                BigInteger end_rule = BigInteger.Parse(transform_rule.Split(delimiter_space, StringSplitOptions.None)[1]) + BigInteger.Parse(transform_rule.Split(delimiter_space, StringSplitOptions.None)[2]) - 1;
                BigInteger start_destination = ConvertNumberB(transform_rule, 0);
                BigInteger range = ConvertNumberB(transform_rule, 2);
                Console.WriteLine($"Regola di trasformazione: {start_destination} {start_rule} {range}         GenericResource: {genericResource.start} {genericResource.end} {genericResource.range}");
                if (genericResource.start <= start_rule && genericResource.end >= start_rule && genericResource.end <= end_rule)
                {
                    Console.WriteLine("Primo if");
                    Resources.Add(new GenericResource(genericResource.start, start_rule - genericResource.start)); //OK
                    genericResource.start = start_destination;//OK
                    genericResource.end = start_destination + genericResource.end - start_rule;//OK
                    genericResource.range = genericResource.end - genericResource.start + 1;//OK
                    Console.WriteLine($"{genericResource.start} - {genericResource.end} - {genericResource.range}");
                }
                else
                {
                    Console.WriteLine("Secondo if");
                    Resources.Add(new GenericResource(end_rule + 1, genericResource.end - end_rule));
                    genericResource.end = start_destination + end_rule - start_rule;
                    genericResource.start = genericResource.start - start_rule + start_destination;
                    genericResource.range = genericResource.end - genericResource.start + 1;
                    Console.WriteLine($"{genericResource.start} - {genericResource.end} - {genericResource.range}");
                }

            }
            public void TotaleOverlap(GenericResource genericResource, string transform_rule)
            {
                BigInteger start_rule = ConvertNumberB(transform_rule, 1);
                BigInteger end_rule = ConvertNumberB(transform_rule, 1) + ConvertNumberB(transform_rule, 2) - 1;
                BigInteger start_destination = ConvertNumberB(transform_rule, 0);
                genericResource.start = genericResource.start - start_rule + start_destination;
                genericResource.end = genericResource.end - start_rule + start_destination;
            }

            public BigInteger start { get; set; }
            public BigInteger end { get; set; }

            public BigInteger range { get; set; }

        }

    }
}
