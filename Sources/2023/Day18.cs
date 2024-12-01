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
    public class Day18 : Solver, IDay
    {
        public static String[] delimiters = { "\r\n", " " };
        public static String[] delimiter_space = { " " };
        public static String[] delimiter_line = { "\r\n" };
        public static String[] delimiter_equals = { "=" };
        public static String[] delimiter_signs = { "=", "-" };
        public static String[] delimiter_parentesi = { "(", ")", "," };
        public static String[] delimiter_comma = { "," };
        public string DigPlan_string = "";
        public string[,] ExploredTile;
        char[,] Tile;
        public int n;
        public void Part1(object input, bool test, ref object solution)
        {
            test = true;

            if (!test)
            {
                DigPlan_string = "L 4 (#6f81b2)\r\nD 5 (#295ca1)\r\nL 2 (#51f302)\r\nD 6 (#295ca3)\r\nL 11 (#604112)\r\nU 4 (#711a63)\r\nL 2 (#113be0)\r\nU 7 (#6d3273)\r\nL 7 (#2180d0)\r\nU 2 (#51c613)\r\nL 10 (#0e0770)\r\nU 2 (#10e181)\r\nL 3 (#485450)\r\nU 10 (#83df31)\r\nL 3 (#1ea290)\r\nU 4 (#235d31)\r\nL 2 (#09ca50)\r\nU 6 (#06daa1)\r\nL 6 (#703070)\r\nU 2 (#3c56a3)\r\nL 3 (#20af60)\r\nU 4 (#152273)\r\nL 6 (#880cb2)\r\nU 5 (#451b63)\r\nL 3 (#880cb0)\r\nU 5 (#48c2b3)\r\nR 4 (#397f70)\r\nU 4 (#069351)\r\nR 4 (#0fe520)\r\nU 10 (#7f2111)\r\nR 4 (#0fe522)\r\nD 8 (#1d4c21)\r\nR 2 (#4a1650)\r\nD 6 (#3966e3)\r\nR 5 (#00cba0)\r\nU 7 (#212d93)\r\nR 8 (#273670)\r\nU 3 (#6c6c51)\r\nR 7 (#1bae00)\r\nU 9 (#1053e1)\r\nR 5 (#4d2030)\r\nU 2 (#7cc033)\r\nR 6 (#1659d0)\r\nU 4 (#447301)\r\nR 5 (#611880)\r\nU 11 (#25c2b1)\r\nR 5 (#0ed680)\r\nU 5 (#7b67d1)\r\nR 2 (#0ed682)\r\nU 7 (#092ab1)\r\nR 5 (#4eb230)\r\nU 3 (#559ba3)\r\nL 5 (#103b62)\r\nU 7 (#40fb43)\r\nR 5 (#103b60)\r\nU 9 (#583153)\r\nR 4 (#4cc440)\r\nD 6 (#4c2dd3)\r\nR 7 (#02e430)\r\nU 7 (#2545d3)\r\nR 3 (#5edcf0)\r\nU 3 (#79e7f3)\r\nR 5 (#10ccc0)\r\nD 10 (#6d8b11)\r\nR 3 (#3f7ce0)\r\nD 3 (#240201)\r\nR 6 (#23cec0)\r\nD 5 (#59ce81)\r\nL 6 (#370b30)\r\nD 4 (#104291)\r\nL 9 (#320380)\r\nU 4 (#458441)\r\nL 6 (#9263f2)\r\nD 6 (#190861)\r\nL 3 (#9263f0)\r\nD 6 (#444301)\r\nR 5 (#289070)\r\nD 3 (#096fd1)\r\nR 9 (#79ee30)\r\nD 6 (#22e183)\r\nL 9 (#08b990)\r\nD 6 (#895df3)\r\nR 5 (#164c60)\r\nD 4 (#143041)\r\nR 4 (#4d7220)\r\nD 5 (#7c1e61)\r\nR 5 (#1ad4f0)\r\nU 5 (#0fc2e1)\r\nR 6 (#684712)\r\nD 8 (#012521)\r\nR 3 (#183810)\r\nD 6 (#387cf1)\r\nL 4 (#070d90)\r\nD 6 (#1cdc61)\r\nL 6 (#3345b0)\r\nU 7 (#46fa61)\r\nL 4 (#0c1060)\r\nD 7 (#38a731)\r\nL 5 (#88f420)\r\nU 6 (#2aae11)\r\nL 3 (#4aee92)\r\nU 4 (#2f2bc1)\r\nL 5 (#2dd012)\r\nD 4 (#2f2bc3)\r\nL 3 (#4f8b92)\r\nD 6 (#0637f1)\r\nL 8 (#3fa330)\r\nD 9 (#433fa1)\r\nL 6 (#4ecdf0)\r\nD 8 (#1e3aa1)\r\nL 6 (#847120)\r\nD 7 (#4673c3)\r\nR 4 (#014b10)\r\nD 7 (#836133)\r\nR 8 (#259bd0)\r\nD 4 (#155de3)\r\nR 7 (#685880)\r\nD 4 (#0c3403)\r\nR 6 (#45ca80)\r\nU 6 (#5d3e63)\r\nR 6 (#3314b2)\r\nU 5 (#210823)\r\nR 7 (#3c6db2)\r\nU 4 (#65ddf3)\r\nR 8 (#6f8260)\r\nD 5 (#42dde3)\r\nR 4 (#037972)\r\nD 8 (#24cd91)\r\nR 4 (#5c2e72)\r\nU 3 (#24cd93)\r\nR 4 (#4e7b22)\r\nU 9 (#066b33)\r\nR 2 (#040360)\r\nU 2 (#3d4c13)\r\nR 5 (#8465c0)\r\nU 5 (#2c64a3)\r\nR 2 (#14ad02)\r\nU 6 (#356233)\r\nL 5 (#4ca262)\r\nU 6 (#4acd83)\r\nR 5 (#202ce2)\r\nU 10 (#4579b3)\r\nR 3 (#3fe2f2)\r\nD 5 (#205603)\r\nR 4 (#3319b2)\r\nD 4 (#692d23)\r\nR 5 (#2c7bd2)\r\nD 5 (#68d661)\r\nL 5 (#3742b2)\r\nD 5 (#0fe0c1)\r\nR 5 (#452892)\r\nD 8 (#78b723)\r\nR 3 (#37a0d2)\r\nU 14 (#3768e3)\r\nR 4 (#270100)\r\nD 2 (#38ff13)\r\nR 6 (#3dcc72)\r\nD 3 (#2e1723)\r\nL 6 (#3dcc70)\r\nD 6 (#2cdba3)\r\nR 8 (#4cdfd0)\r\nD 5 (#17ca31)\r\nL 8 (#419790)\r\nD 5 (#035321)\r\nR 6 (#397212)\r\nD 5 (#914b21)\r\nR 7 (#397210)\r\nU 3 (#14f231)\r\nR 5 (#5a4a50)\r\nU 2 (#34d9c3)\r\nR 9 (#320cf2)\r\nU 6 (#626173)\r\nL 6 (#320cf0)\r\nU 2 (#2a1f73)\r\nL 8 (#3a33c0)\r\nU 5 (#2ba8e1)\r\nR 2 (#68d670)\r\nU 8 (#528601)\r\nR 5 (#0ae4b0)\r\nU 2 (#15c2f1)\r\nR 5 (#02a230)\r\nU 10 (#6338d3)\r\nR 6 (#078062)\r\nD 4 (#72c223)\r\nR 2 (#5846a2)\r\nD 5 (#39cd03)\r\nR 11 (#464952)\r\nU 3 (#660903)\r\nR 7 (#34bd02)\r\nU 9 (#2a59c3)\r\nR 7 (#751042)\r\nU 8 (#562a93)\r\nR 7 (#0a81a2)\r\nU 4 (#4d15f3)\r\nR 6 (#67cc92)\r\nU 6 (#4d15f1)\r\nL 3 (#723672)\r\nU 6 (#0566d1)\r\nL 6 (#6cf2f2)\r\nU 2 (#582a11)\r\nL 4 (#74a212)\r\nU 11 (#103141)\r\nR 6 (#287b52)\r\nU 4 (#84f181)\r\nR 7 (#2eb650)\r\nD 8 (#903541)\r\nR 8 (#2eb652)\r\nD 3 (#103391)\r\nR 3 (#157392)\r\nD 3 (#0789b3)\r\nR 7 (#1d07a2)\r\nD 6 (#61e393)\r\nR 4 (#7881d2)\r\nD 2 (#3f5893)\r\nR 7 (#286722)\r\nD 6 (#381f83)\r\nR 8 (#4a7da2)\r\nD 2 (#463143)\r\nR 4 (#0b4032)\r\nD 7 (#7e9143)\r\nL 6 (#355d82)\r\nD 2 (#02aae3)\r\nL 4 (#11bbe2)\r\nU 4 (#67fa03)\r\nL 10 (#11bbe0)\r\nD 4 (#3e2733)\r\nL 5 (#407c92)\r\nD 2 (#04db23)\r\nL 5 (#386842)\r\nD 8 (#4d5491)\r\nR 6 (#4e3752)\r\nD 4 (#4d5493)\r\nR 6 (#409f12)\r\nU 7 (#873c01)\r\nR 7 (#4d0672)\r\nU 2 (#873c03)\r\nR 3 (#238232)\r\nD 5 (#2ec8d3)\r\nL 6 (#1a84c0)\r\nD 6 (#4e0e23)\r\nR 6 (#207ac0)\r\nD 3 (#234f73)\r\nR 6 (#257662)\r\nD 5 (#665b03)\r\nL 9 (#257660)\r\nD 3 (#1154a3)\r\nL 3 (#1a2570)\r\nD 7 (#08f133)\r\nR 6 (#7489d0)\r\nD 11 (#0d29d1)\r\nR 4 (#3b6d72)\r\nU 5 (#5ad2a1)\r\nR 8 (#4f1132)\r\nU 5 (#021471)\r\nR 4 (#12f0e0)\r\nU 2 (#1b0d21)\r\nR 4 (#4a05e0)\r\nU 8 (#4d8d53)\r\nR 4 (#69d810)\r\nU 3 (#4d8d51)\r\nR 5 (#168f40)\r\nU 3 (#3b30b1)\r\nR 8 (#5dc4b2)\r\nU 8 (#289f01)\r\nL 10 (#7f9962)\r\nU 7 (#04d161)\r\nL 4 (#8a7ea0)\r\nU 4 (#043f51)\r\nL 3 (#7d8e00)\r\nU 3 (#1542d3)\r\nR 8 (#13a2b0)\r\nU 9 (#50e003)\r\nL 6 (#105580)\r\nU 4 (#159251)\r\nL 11 (#536490)\r\nU 3 (#69cae1)\r\nR 10 (#1d0b70)\r\nU 2 (#27dc61)\r\nR 3 (#2e41c0)\r\nU 6 (#01a1a1)\r\nR 4 (#8dfc90)\r\nU 7 (#2c9351)\r\nL 8 (#166b42)\r\nU 3 (#6e2221)\r\nL 4 (#5b1e42)\r\nU 7 (#6e2223)\r\nL 5 (#3232c2)\r\nU 4 (#520c71)\r\nR 9 (#2d1100)\r\nU 9 (#45aaa1)\r\nR 8 (#76ab40)\r\nD 8 (#2ca591)\r\nR 8 (#8dfc92)\r\nD 7 (#21f501)\r\nR 6 (#49ca60)\r\nD 8 (#887f81)\r\nR 9 (#48c220)\r\nD 3 (#887f83)\r\nL 9 (#1d4540)\r\nD 7 (#64f8f3)\r\nR 6 (#29f2f0)\r\nD 3 (#3672b3)\r\nR 4 (#5b6a30)\r\nD 9 (#47a913)\r\nR 3 (#5b6a32)\r\nD 4 (#1c77c3)\r\nR 4 (#29f2f2)\r\nU 13 (#14fa23)\r\nR 5 (#56ead0)\r\nD 5 (#258123)\r\nR 11 (#6e74d0)\r\nD 5 (#258143)\r\nR 2 (#1127e0)\r\nD 4 (#536d63)\r\nR 4 (#7f9cb2)\r\nD 2 (#08c9d3)\r\nR 12 (#11c220)\r\nD 4 (#0d4613)\r\nR 4 (#1117c0)\r\nU 6 (#63d6e3)\r\nR 7 (#079410)\r\nD 6 (#140c73)\r\nR 4 (#518310)\r\nD 2 (#2f5813)\r\nR 8 (#3d38b0)\r\nD 8 (#33bca3)\r\nR 4 (#2fc890)\r\nD 3 (#199091)\r\nR 4 (#3c9a40)\r\nD 6 (#199093)\r\nL 12 (#671c60)\r\nD 6 (#6314b1)\r\nL 3 (#0ddda0)\r\nD 6 (#2625c1)\r\nL 8 (#67eb80)\r\nD 4 (#5f03a1)\r\nL 4 (#402810)\r\nD 6 (#6622d1)\r\nL 6 (#3c8210)\r\nD 5 (#1f1501)\r\nL 4 (#386720)\r\nU 4 (#5e9f81)\r\nL 3 (#4658b0)\r\nU 7 (#50aee1)\r\nL 6 (#3b36b0)\r\nU 4 (#2d3bf1)\r\nL 6 (#485710)\r\nD 7 (#5b0341)\r\nL 2 (#826220)\r\nD 5 (#299251)\r\nL 7 (#6d7080)\r\nD 4 (#20d6f1)\r\nL 7 (#547000)\r\nD 4 (#7ded11)\r\nR 9 (#19bc70)\r\nD 3 (#119381)\r\nR 5 (#28b900)\r\nD 6 (#2a6a61)\r\nL 6 (#11d062)\r\nD 6 (#6de691)\r\nL 8 (#11d060)\r\nU 4 (#20cb01)\r\nL 3 (#44ffd0)\r\nU 12 (#5555e3)\r\nL 5 (#567c32)\r\nU 2 (#24a913)\r\nL 3 (#567c30)\r\nD 5 (#3f1d03)\r\nL 8 (#6cc840)\r\nD 2 (#4f6121)\r\nL 4 (#427f10)\r\nD 5 (#662b11)\r\nR 12 (#274300)\r\nD 3 (#4d3db3)\r\nL 5 (#8269d0)\r\nD 3 (#0a29d3)\r\nL 4 (#8269d2)\r\nD 4 (#4cad33)\r\nR 9 (#5897b0)\r\nD 7 (#07e1b1)\r\nR 3 (#3b3d00)\r\nD 4 (#502e91)\r\nR 8 (#2afc70)\r\nD 5 (#4c0471)\r\nL 4 (#0abae0)\r\nD 2 (#7d3af1)\r\nL 9 (#085c40)\r\nD 3 (#294e31)\r\nL 3 (#5af032)\r\nD 6 (#15b561)\r\nR 6 (#3d5552)\r\nD 4 (#538691)\r\nR 5 (#1ef0e2)\r\nU 4 (#4ad471)\r\nR 5 (#365c92)\r\nD 3 (#0b4651)\r\nR 3 (#165bf2)\r\nD 6 (#7969a1)\r\nR 5 (#258320)\r\nU 6 (#3eac11)\r\nR 4 (#7f1160)\r\nU 6 (#3bbb51)\r\nR 2 (#2d4ac0)\r\nU 8 (#7a6763)\r\nR 8 (#320fa0)\r\nU 5 (#497d21)\r\nR 3 (#4affb0)\r\nU 11 (#60bfa1)\r\nR 7 (#4affb2)\r\nD 4 (#058041)\r\nR 9 (#085c42)\r\nD 6 (#7b9b61)\r\nR 7 (#1b6382)\r\nD 7 (#69d101)\r\nR 8 (#08bdd2)\r\nD 3 (#7923f1)\r\nL 5 (#0c8052)\r\nD 12 (#047191)\r\nL 6 (#217442)\r\nD 2 (#12c433)\r\nL 8 (#6a7582)\r\nD 4 (#5c3823)\r\nL 8 (#5fd572)\r\nD 2 (#1b1723)\r\nL 6 (#5fd570)\r\nD 4 (#5d5313)\r\nL 3 (#3443a2)\r\nD 6 (#3865d1)\r\nL 8 (#5d30a2)\r\nD 5 (#528a23)\r\nR 9 (#64c9c2)\r\nD 2 (#7b78e3)\r\nR 6 (#31c952)\r\nU 12 (#476ac3)\r\nR 6 (#57aeb2)\r\nD 7 (#537b03)\r\nR 2 (#1447f2)\r\nD 5 (#582521)\r\nR 5 (#12d112)\r\nD 7 (#4424c1)\r\nL 9 (#5c3792)\r\nD 7 (#53d2c1)\r\nL 3 (#5c3790)\r\nD 10 (#519051)\r\nL 7 (#218392)\r\nU 10 (#273bd1)\r\nL 6 (#2d6cd2)\r\nD 5 (#4ec1d3)\r\nL 3 (#8492b2)\r\nD 5 (#4ec1d1)\r\nL 3 (#171a22)\r\nD 3 (#0002d1)\r\nL 8 (#634852)\r\nU 2 (#0ee1d1)\r\nL 6 (#3d2eb2)\r\nU 3 (#04ea01)\r\nR 4 (#607f20)\r\nU 9 (#7e4251)\r\nR 2 (#607f22)\r\nU 4 (#0fa471)\r\nR 3 (#2d0682)\r\nU 12 (#4fbb41)\r\nL 3 (#005e72)\r\nU 6 (#4c5b61)\r\nL 6 (#7f5b72)\r\nU 10 (#33e641)\r\nL 7 (#4240a2)\r\nD 3 (#3c80b1)\r\nL 4 (#24d372)\r\nD 9 (#3298d1)\r\nL 6 (#055d42)\r\nD 4 (#41a9c3)\r\nL 6 (#4ace92)\r\nD 3 (#41a9c1)\r\nL 5 (#49d532)\r\nD 8 (#2d69e1)\r\nL 5 (#11cb42)\r\nD 9 (#6343c1)\r\nL 5 (#5aec92)\r\nU 8 (#727291)\r\nL 7 (#575b02)\r\nU 8 (#2d9493)\r\nL 5 (#29b0e2)\r\nU 5 (#773de3)\r\nL 6 (#448ef2)\r\nU 4 (#30b9c3)\r\nL 6 (#453232)\r\nU 4 (#48bc11)\r\nL 2 (#3e79d2)\r\nU 11 (#7ccff1)\r\nL 3 (#5516d2)\r\nD 7 (#3596c1)\r\nL 8 (#0a44d2)\r\nD 2 (#363c41)\r\nL 6 (#83a172)\r\nD 4 (#23f181)\r\nL 11 (#8de640)\r\nD 4 (#312f31)\r\nL 6 (#7ace72)\r\nD 4 (#2dc461)\r\nL 6 (#1a77a2)\r\nD 7 (#215151)\r\nL 6 (#8a6f80)\r\nD 3 (#41f301)\r\nR 3 (#0c6ef0)\r\nD 7 (#41f303)\r\nR 9 (#4b0960)\r\nD 6 (#5ab701)\r\nR 12 (#46eee0)\r\nD 5 (#0cf881)\r\nR 5 (#4175a2)\r\nD 8 (#08d7b1)\r\nL 6 (#576f32)\r\nU 4 (#5b2e81)\r\nL 3 (#1609b2)\r\nD 4 (#548b91)\r\nL 7 (#653f72)\r\nD 3 (#503443)\r\nR 5 (#42aae2)\r\nD 5 (#5f85d3)\r\nR 5 (#0efc42)\r\nD 3 (#023203)\r\nR 7 (#4ce242)\r\nD 3 (#914443)\r\nR 5 (#379b32)\r\nD 8 (#914441)\r\nL 7 (#23df42)\r\nD 3 (#5f0713)\r\nL 10 (#181532)\r\nD 3 (#0caa31)\r\nL 5 (#25dc72)\r\nD 7 (#1f7ef3)\r\nL 6 (#43f6d2)\r\nD 3 (#1f7ef1)\r\nL 10 (#39ee62)\r\nU 6 (#0caa33)\r\nL 3 (#729962)\r\nU 8 (#08e3b3)\r\nL 9 (#4abe22)\r\nU 6 (#11eca3)\r\nL 9 (#251700)\r\nU 4 (#677653)\r\nL 3 (#75e930)\r\nU 4 (#5a9583)\r\nL 6 (#64e040)\r\nU 7 (#0d2d03)\r\nR 9 (#723e00)\r\nU 9 (#44b093)\r\nL 6 (#070fe0)\r\nU 2 (#42d003)\r\nL 12 (#5dfdd0)\r\nU 6 (#16e5d3)\r\nL 4 (#4a5ee0)\r\nU 4 (#5c7403)\r\nL 5 (#623452)\r\nU 9 (#653f33)\r\nL 7 (#0983c2)\r\nU 10 (#2b6293)\r\nR 5 (#1eba12)\r\nU 12 (#3f8d01)\r\nR 3 (#516e32)\r\nD 9 (#5114c1)\r\nR 8 (#257082)\r\nD 3 (#3c6e33)\r\nR 7 (#541502)\r\nU 6 (#502401)\r\nR 5 (#3d0322)\r\nU 6 (#4eec41)\r\nR 6 (#74a502)\r\nU 8 (#3a4fc3)\r\nL 3 (#29c2f2)\r\nU 3 (#572513)\r\nL 11 (#780f92)\r\nU 4 (#0d9b73)\r\nL 2 (#321ee2)\r\nU 3 (#6d7933)\r\nL 5 (#614562)\r\nU 6 (#4bdbd3)\r\nL 12 (#614560)\r\nU 3 (#3facd3)\r\nL 4 (#4c7bd0)\r\nD 8 (#58d343)\r\nL 4 (#7d48b0)\r\nD 4 (#05c741)\r\nL 4 (#2d8730)\r\nU 4 (#6964f1)\r\nL 4 (#68bdd0)\r\nU 8 (#780801)\r\nL 8 (#3bbd00)\r\nU 4 (#0296e1)\r\nL 6 (#0d0280)\r\nU 3 (#3094e3)\r\nL 8 (#2581a0)\r\nU 8 (#6b7e33)\r\nL 5 (#2581a2)\r\nU 3 (#4db803)\r\nL 7 (#30e580)\r\nU 5 (#4f8f73)\r\nL 2 (#6ec3c2)\r\nU 8 (#340063)\r\nL 4 (#1428b2)\r\nU 3 (#4810c3)";
            }
            else
            {
                DigPlan_string = @"R 6 (#70c710)
D 5 (#0dc571)
L 2 (#5713f0)
D 2 (#d2c081)
R 2 (#59c680)
D 2 (#411b91)
L 5 (#8ceee2)
U 2 (#caa173)
L 1 (#1b58a2)
U 2 (#caa171)
R 2 (#7807d2)
U 3 (#a77fa3)
L 2 (#015232)
U 2 (#7a21e3)";
            }


            prova p1 = new prova("primo");
            prova p2 = new prova("secondo");
            prova p3 = new prova("terzo");
            prova p4 = new prova("quarto");
            List<prova> pp = new List<prova>();
            pp.Add(p1); pp.Add(p2); pp.Add(p3); pp.Add(p4);
            Console.WriteLine($"Tutti: ");
            foreach (var item in pp)
            {
                Console.WriteLine($"{item.name}");
            }

            p1.f_1 = true;
            p3.f_1 = true;
            Console.WriteLine($"Pari: ");
            foreach (var item in pp.Where(f => f.f_1))
            {
                Console.WriteLine($"{item.name}");
            }

            Console.WriteLine($"Dispari: ");
            foreach (var item in pp.Where(f => !f.f_1))
            {
                Console.WriteLine($"{item.name}");
                item.f_1 = true;
            }



            var DigPlan_array = DigPlan_string.Split(delimiter_line, StringSplitOptions.None);
            n = DigPlan_array.Length;
            int np = n;//int.Parse(Console.ReadLine());
            double[] x = new double[np];
            double[] y = new double[np];

            Position You = new Position() { x = 0, y = 0 };
            x[0] = 0;
            y[0] = 0;

            for (int i = 1; i < np; i++)
            {
                DirectionEnum d = ConvertLetterToDirection(DigPlan_array[i - 1].Split(delimiter_space, StringSplitOptions.None)[0]);
                int meters = int.Parse(DigPlan_array[i - 1].Split(delimiter_space, StringSplitOptions.None)[1]);

                switch (d)
                {
                    case DirectionEnum.Up:
                        You.y -= meters;
                        break;
                    case DirectionEnum.Right:
                        You.x += meters;
                        break;
                    case DirectionEnum.Down:
                        You.y += meters;
                        break;
                    case DirectionEnum.Left:
                        You.x -= meters;
                        break;
                }
                //Console.WriteLine($"Введите координаты {i + 1}-й вершины (x, y):");

                //string[] coordinates = Console.ReadLine().Split(' ');
                //x[i] = double.Parse(coordinates[0]);
                //y[i] = double.Parse(coordinates[1]);
                x[i] = You.x;
                y[i] = You.y;

            }
            double area = CalculatePolygonArea(x, y);

            Tile = new char[n, n];
            ExploredTile = new string[n, n];
            for (int r = 0; r < DigPlan_array.Length; r++)
            {
                //                DirectionEnum d = ConvertLetterToDirection(DigPlan_array[r].Split(delimiter_space, StringSplitOptions.None)[0]);
                //              int meters = int.Parse(DigPlan_array[r].Split(delimiter_space, StringSplitOptions.None)[1]);

                /*              switch (d)
                              {
                                  case DirectionEnum.Up:

                                      break;
                                  case DirectionEnum.Up:
                                      break;
                                  case DirectionEnum.Up:
                                      break;
                                  case DirectionEnum.Up:
                                      break;
                              }
                  */            //for (int c = 0; c < DigPlan_array[r].Length; c++)
                                //{
                                //   Tile[c, r] = DigPlan_array[r][c];
                                //  ExploredTile[c, r] = "";
                                // }
            }
        }
        public enum DirectionEnum
        {
            Up,
            Right,
            Down,
            Left
        }

        public string ConvertDirectionToLetter(DirectionEnum de)
        {
            switch (de)
            {
                case DirectionEnum.Up: return "U";
                case DirectionEnum.Right: return "R";
                case DirectionEnum.Down: return "D";
                case DirectionEnum.Left: return "L";
            }
            return "";
        }
        public DirectionEnum ConvertLetterToDirection(string l)
        {
            switch (l)
            {
                case "U": return DirectionEnum.Up;
                case "R": return DirectionEnum.Right;
                case "D": return DirectionEnum.Down;
                case "L": return DirectionEnum.Left;
            }
            return DirectionEnum.Right;
        }
        public class prova
        {
            public string name;
            public bool f_1;
            public bool f_2;
            public prova(string n)
            {
                name = n;
                f_1 = false;
                f_2 = false;
            }
        }
        static double CalculatePolygonArea(double[] x, double[] y)
        {
            double area = 0;

            for (int i = 0; i < x.Length; i++)
            {
                int j = (i + 1) % x.Length;
                area += x[i] * y[j] - x[j] * y[i];
            }

            return Math.Abs(area / 2);
        }
        public class Position
        {
            public double x { get; set; }
            public double y { get; set; }
        }
        public void Part2(object input, bool test, ref object solution)
        {

        }

    }
}
