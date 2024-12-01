﻿using AOC;
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

    public class Day10 : Solver, IDay
    {
        public static String[] delimiters = { "\r\n", " " };
        public static String[] delimiter_space = { " " };
        public static String[] delimiter_line = { "\r\n" };
        public static String[] delimiter_equals = { "=" };
        public static String[] delimiter_parentesi = { "(", ")", "," };
        //public List<int[]> _YouAre = new List<int[]>();
        //public List<int[]> _NextYou = new List<int[]>();
        //public List<int[]> _VisitedPositions = new List<int[]>();
        public string Map = "";
        public int n;
        public Pipes pipes;

        public int nr;
        public int nc;
        bool[,] Inner;
        bool[,] Perimeter;
        public void Part1(object input, bool test, ref object solution)
        {
            test = false;

            if (!test)
            {
                Map = @"F--FJ77F-L7---J-F777-F7|77F|-J-F|77|F-L-J-77-L7FL77-7|----.F7-|-|.7-.-FL-FF-FF77.|.F-F-.F|.7.F.7-F-77.J7|----7LL7F|7|-F-J-777-77.F-7-FL77FF|
F7FJ.LF-7JJ||.JF-J-7F-FLJJ|F7JFL|LJ.|.|7LFF-J|J-L7LJFJF||.F.FFJFF7L7.L-JF7.FLL-FJ-J7F|L-7|F7.7-|.J7|F-7LJ|FFL7J-77||LF-JJ|L7JLLJLL--F7|.LFFJ
LL7|7FJFJ7L--LJ|.LF7--|JJL-.|7FF-7.LJ-LLJFLJL|.|.LJ||.7JF-7F|L-|J|L7F||LFJL|.7J|LJ.--77|.LLFF77||7F|JLF-77-|7F7LFJLJ-FJJ-7FLL7J.|7|7.L.FLF77
||FJ--.F-7FL7JJL7.|JL-LF777FFF7|-FJJ.L||--JF7JLL|777LL-.|FLL|.|J.|L|-L7.LFL7-JF7F77..L7F7JF7|L77JLFJJ.L7|LLJJJLFJFJJ.JJJ.L7.||FLFFF7F|FJ-||F
||JJF|-J7L|.LJ7..-J.|JL||F7F7JFF-7J.7|F7J-L|J-|-J7-7-|.FFL7LJ77|FJ7|JLJL--.||L|FFJ--LLJJFF|LJFJ77.||..F7J7FF|L7JF|J777|-|JJF|J7.L|.|FLF7FFJ|
|J|-|7JF77L-|JF-7J-7J-||LJLJL--7.|FF77|||..LF7-7L77F--L77L|FF|L77LFF-7JJFLFF7|F7-J-LF|7F-7L-7|7F7.F77F-7FL|77F7LFJ-JFLJFJJ-LJJ|L.|FF-7FL7JFJ
|LJJ||LL|77F|JLF7|FL-F7L-7F7F7FJF-7||-||77.F|.FL7|7LF7|L7|LF7JLJ|F-||JL-7|F|L-J|JJJ.FF-JFJF-JL7|L7||7L7L-7.FJ|J|LJ7.|.J|..FJ|L7.-7JLL||L-.JJ
|F|LF7.||-FJ||.L7-|.F|L--J|LJLJ-|FJ||L||F7F7777L|7F7FF7J|F7||7.FF|F|JL7J-F.L7F7|F7F7JL-7|-L-7FJ|FJ||F7L7FJ7|.|LL|FL--F-L7FF-|FJ-L|.-.L-7-FL7
L-F---|7.FLJJF||L...|L---7L7F7.FJL7||FJ||LJ|F77FFF7F7|L7FJ||L--7F-7-7-J7-|7LLJ|LJ|||JFL||F7F|L7|L-J|||-||F7F|J7FLJ-7-7-LF7LF|7JF-JL77LLL7.|J
J7F7JL|LFL-JJF77|-F-----7|FJ||7L-7||||FJ|F-J|L7F7||||L7|L7||F--JL7|-F7L7.|7FF7L7FJ||F7FJ||L7L7LJF-7||L-J||L-77L-J|-F-LF7.77L7J.|F7L-7-|JL|L|
.|L|FJ..LLL.FF-77F|F---7||L7|L-7FJLJ||L7|L-7|FJ|||LJL7|L7|LJL7J|FJL-J|J||LFFJL-JL-J|||L7||FJ7L-7|JLJL7F-JL7FJ-|J7LF77|||-L.LL|F.LJLL|7-7LL-J
-.|LJ||-F7|FJ.FF7.LJJF7|LJFJ|F-JL-7FJL7LJF-J||FJ|L--7|L7|L-7FJ.FL7F--JF7J.FJF7F---7LJL7|LJL-7-FJL7-F7|L---J|JF7LF-JLFF7||L-F--JF7|.LL77|JL-|
LLJ.LLJFJJ-7-FF||.J|J||L-7L7|L--7FJL-7L-7|F7|LJFJ7F-JL7||F-JL-77F|L---J|-FJFJLJLF-JF--J|F7F7L7|F7L7|||F----J.||7FFL7F7FJ7JFL||||LJ..L|7J|FF7
.||-.L|-|7.J7F-J|7--FJL-7L7||F--JL7F-JF-JLJLJF7L-7L--7|||L--7FJF7|F----J.|FJJLF7|F7|LF7||LJL7|LJL7||LJL7F--77||-F7.F7-JF7.-7L777.FF7F.L7--||
-|7.|||JLF7.FJF7L77.L--7L-JLJL7F--JL-7L--7F7FJL--JF7FJ||L7F-JL7|LJL-7F-77LJ7F7||LJ|L7|LJL7F-J|F--J||F--JL7FJFJ|FJL-J|7JFJ.L777JF7-J--JFJ7|.7
F77FFF---L7-L-JL7|77LF-JF7F7F-JL----7|F-7||LJF7F7.||L7||FJL-7FJ|F7F7LJFJF7|FJ|||F-JFJ|F--JL--JL-7FJ|L-7F7|L7|FJL7F--J---7L7FJ|FLL7.F|LLJ7L7|
|J|7.F7|.F-.|FF-J|F7LL--J||LJ7F-----J|L7|||F7|LJL7|L-JLJL-7FJL7||LJL--JFJL7L7|||L-7L7|L------7LFJL7|F-J|||FJ|L-7|L-7||-J7L7.FFJL|LJ-L7.|77L7
JF||7|F|7.|7|FL-7|||F7F--JL--7L--7F-7|FJ||||||F--JL----7F-JL-7||L--7LF7L-7|FJ||L--JFJ|F------JFJF7||L7L||||-L7FJ|F-JF7.|F-J7L|FF7-JJ-7--.|J|
F|LJ-|.|LFLJ|77FJLJLJ|L--7F--JF7-||FJLJFJ|||LJL-7F7F7F-J|F7F7|||F--JFJ|-FJLJFJL---7|||L-7F---7|FJ||L7L-JLJ|F-J|FJL7FJ|7F7.F|||FF|LLJ.||.LJJF
7J.F7J7---J.LLFJF---7|F7FJL--7|L7LJL-7FJFJ||F---J||||L-7||LJLJ||L--7L7L7L--7L7-F7FJL-JF-JL7F-JLJFJ|-L7F---JL7FJL7FJ|FJFJ|.F77JFFF.FFF7F-JL7J
F777.LF-JJ7F7LL7|F--J|||L7F--JL7L7F--JL7L7|||F7.FJ|||F7||L---7||F--J-L7|F7J|FJFJ|L-7F-JF7FJL7F7FJFJF-JL---7FJL7FJL-JL7L7|FJL77FJL.F7.7JJ.|J7
|F-J--7|.FJ-F7L|||F-7LJ|-|L7F7-L7|L--7FJFJ||LJL7L7||||||L7F--J||L--7F-J||L-JL-JFJF7|L7FJ|L-7|||L7||L7F----JL7FJ|F7F-7L-JLJF-J-J-J.||FJ|.F7.|
LLJ7FF-77|7-L|L||||-L-7L-JFJ|L-7|L7F7||.L7LJF--JF||||||L7|L--7LJF--JL-7|L----7FJ-|LJFJ|FJF-S|||FJ|F7||F-7.F7|L7||||FJF----JFF7J|FFLJJ7L-FJFJ
.FLF-LJ--LL7FF7LJ||F--JF-7|FJF-JL7|||||F7|F7|F7F7||||||FJL7F7|F-JF7F7FJ|LF7F7||F7|F7L7|L7L-7|||L7||||||FJFJLJFJ||LJL-JF7F7LF||-.JL|-L-7||||J
LF7L|.|LLJJLFJL-7LJL--7|.|||FJF7FJLJLJ|||LJ|||LJLJ||LJ||F7LJ||L7FJ|||L7L7|LJLJLJ|LJ|FJ|FJF-JLJL7|LJ||LJ|FJF-7L-JL---7FJLJL-7||..|7..F-J7-||.
F|7F-F---J.|L-7FJ-|JF7LJFJLJL-J|L---7FJ||F7|LJF7F7|L7FJLJL7FJ|FJ|FJ|L7|FJL----7FJ-FJ|||||L-7F7FJL7FJL7FJL7|-L-7F----JL7F7F-J|L7.-7..JJ.J.77-
-J||.|7|.F-FF7|L-7F-J|F-JF--7F-J-F--J|FJ||||F-JLJLJFJL--7J||FJL-JL7|FJ|L7-F7F-J|-FJFJFJ|F-7||||F-JL7J||F7|L--7|L-7LF7.LJ|L-7|FJ7JLF.|-||FL-7
|-F--F77-77FJ|L7FJL-7LJF7|F-JL--7L--7||FJ|LJL7F7-F7L--7FJFJ|L-7F7FJ|L7L7|FJ||F7L7L7L7L7LJFJLJ|||F-7|FJ||||F--JL--JFJL-7FJF7||L--7.|-J7LL7-|7
L||-FJ||LF-JFJFJ|F--JF-JLJL-7F-7|F--JLJ|7L-7FJ||FJL7F7|L7L7L--J|||FJFJL||L7|LJL7L7|FJ-|F-JF7J||LJFJ|L7||LJL7F7F--7|F--JL7|LJ|F--JF|||||-LFF7
F|7.JL-F-JF7L7L7|L---JJF----JL7LJL----7|F7||L-J||F-J||L7L7|F---J||L7L7FJ|L|L7F-JFJ|L7FJL--J|FJ|F7L7|L||L-7FJ|||F-J|L7-F7|L--JL----7J77FF-J|L
-JL7JFLL7FJL7L-J|F----7L-----7|F77F7F-J|||FJF--J||F7||LL7LJL---7LJJL7|L7|FJFJL-7L7|FJL-7F--JL7||L7|L7||F7||7||||F7|FJFJLJF-7F-----JJL|JL77FJ
.J.|-J..LJ7LL7F-JL-7F7L------JLJ|FJ|L-7||||FJF7FJ|||||F7L-7F---JF--7||J|||FJF7|L7|||F7FJL7F-7|||FJL7||LJ||L-J||LJLJL-JF7FJFJL-7.L-|F-7.LJJ.|
|FFJ7.FF7F7F-JL7-F7LJL--7F7F---7|L7|F7|LJLJL-J|L7||||LJ|F-J|F7F7L-7LJL7|||L7||F-J||||||F-JL7||LJ|F7|||F-J|F--J|F---7F7|LJ7L-7FJ7||FFJ.F7FJ--
-|7|F7FJLJLJF-7L7|L7F---J|||F--JL-JLJ|L7F-----JFJ||||F-JL-7LJ||L7FJF--J|||FJ||L-7LJ|||||F7FJ|L-7|||||||F7|L7JFJL--7|||L-7-F7||-F-LJL|7|-|-L|
.|F-JLJF----J-|FJ|FJL--7FJLJL7F-----7|-||F7F7F7L7|||||F7F7L7FJL7|L7|F7L||||-||F7L-7|||||||L7|F7|||||||||||FJFJF7F-JLJL7FJ|||||J|7J|.LJJ|||F|
FLL-7F7|F7F---JL7|L7F7FJ|F7F-J|F7F--J|FJ|||||||FJ|||||||||FJL-7||FJ|||FJ||L7||||F-J||||||L7|||LJ||||||||LJL-JFJLJF7F7JLJ-FJ|LJ||7--JJ|-7F||J
||7.LJLJ|LJF---7LJFJ||L-J||L--J||L--7||FJ|LJ|||L7||||LJLJ|L-7FJ|LJJ||||FJL7|||||L7FJ|||LJFJ||L-7||LJ|||||F7F-JLF-JLJL-7F7L7|F-7JJ|.LF|7.-L|-
F7-F-7F7L--JJF-JF-J7||F--JL--7FJL---J||L7|.FJ||FJ||||F7F-JF-JL7L7F-J|||L7FJ||LJ|FJ|FJ|L7JL7||F-J||F-J|||FJLJ.F7L7F----J|L-JLJFJJ-FL7L-F7J.|.
F77L7LJ|-F7F7L-7L7F7|||F7F--7|L7F---7||.|L7|FJ||FJ||LJ|L-7L--7L-JL-7|LJ7|L7|L-7||FJL7L7L-7|||L77LJ|F7|||L----JL-JL----7L7F-7FJF7JFJL7|||FF|J
|L--JF7L-JLJL-7|FJ||||LJ||F-JL7LJF7FJ||FJFJLJFJ|L-J|F7L7FL7F-JF----JL--7|FJL7FJ||L7FJ.|F7|LJL7L-7JLJ||LJJF-7F--7F7F7F7L7LJFJL-JL-7.L7F-|-FJJ
|F--7||F7F7F-7|||FJLJ|F7LJL7F7L7FJLJJLJL7L-7-L7|F--J||FJF-JL7FJF7F7F-7FJ||-FJL7||FJ||FJ||L-7FJF7L-7FJ|F--JFJL-7||||||L7L7FJF-7F--J7FJ|LL-|J.
LJF-J|||LJLJFJLJ|L-7FJ|L7F7LJL7LJF--7F7FJF7|F7LJL7F7||L7L7F7|L-JLJ||FJ|7|L7|F-JLJL7L7L7||F-JL7||F-J|FJL--7L---JLJ||LJFJFJ|FJ7|L-7J|-L-7J|F77
FFJF7|LJF7F-JF-7L--JL7|FJ|L7F7|F-JF-J||L-J|LJL--7|||||FJFJ|||F7F-7||L7L7L-J|L---7FJFJ7||||F7FJ||L7JLJF7F7L7F7F7LFJL-7L-JF|L-7L-7|F7J|LF77J-J
FL-J||F-JLJF7L7L7F--7||L7L7||LJL7|L--JL--7L7F---JLJ||||.L-JLJ||L7LJL7L7L-7-L----JL7L-7LJ|LJ|L7|L7L-7FJLJL-J|||L7L7F7L-7F7|F-JF7LJ||JL-|LJ.|J
7FL|||L--7FJL7||LJF7|||FJFJLJF-7|F------7L7|L----7FJ||L7F--7FJL-JF7FJFJF7L-------7|F7|F-JF7|L|L7|F-JL7F----JLJFJL|||F7LJLJL-7||F7|L7J|L77FL7
|7JFJ|L||||F-J|F--JLJLJL-JF7FJ|LJL-----7L7|L7F---JL-J|FJL-7LJF7F7|LJFL7|L-7F-7F7FJ|||||F-J|L7L-J|L7F-J|F------JF7LJLJL-7F---J|||||FJ|F--|J7.
L7-|FJF|FJ||F-JL-7F7F7F7F-J||JF---7F---JFJ|FJL-7F7F-7||F--JF-JLJ|L-7F-JL-7||7|||L7||||||F-JFJJF7L-JL--JL-------JL7F7F-7LJF---JLJ|||F7|||LJF7
L|-LJFJ-L7|||F---J|LJ||||7FJL7L-7FJL-7F7L-JL7F7LJLJFJ||L7F-JF---JF7|L7F7FJ|L7||L-JLJLJLJ|F-JF-JL--7FF7F---7F-----J|||FJFFJF-----J|LJL7JFLFL-
.|.|||JJ|LJLJL7F-7|F7LJLJFJF7|F-JL--7LJL-7F7LJL--7FJL||LLJF-JF7F7|||FJ||L7|FJ|L----7F---J|F7|F7F-7L-J|L--7LJF-7F7FJ|||F7L7L---7F-JF7FJ.7.|L|
--J-LJF-F-7F7FLJFJ||L----JFJLJL7F--7L7F-7LJL---7||L-7LJF--JF7|LJ||LJL7|L7||L7L7F--7|L7F-7||LJ|LJLL--7L--7L--JFJ|||FJ||||-L7F--J|F7|LJJ7F7J-7
F7.|7.JLL7LJ|F7-L-JL-----7|F---J|F7L7LJ|L---7F7L7L--JF7L--7|||F-JL-7FJ|7LJ|FJFJL7FJ|F||FJ||F7L7F----JF-7L----JJ|LJL-J|||F-JL---J||L----J|.--
LLFF-F7F7L-7LJL7F7F------J|L----J||FJF7F---7||L7L----JL7|FJ|LJ|F-7FJL7L--7|L7|F7|L7|FJ||FJLJL-JL--7F7|FJF---7F7L7F---J||L7F----7||F7F-7FJ-LJ
L--|FJL|L--JF-7LJLJF7F-7F7|F----7|LJFJLJF7FJLJ7L7F--7F7L7|FJF7||L|L-7L7F7||FJ||LJJLJL-J||F7F-----7LJLJL7|F--J||FJL--7FJL-JL7FF7LJLJ|||||...|
||LL77LL-7F7L7|F-7FJ||J||LJL---7|L--JF--JLJF7FF7|L-7LJL-JLJFJLJL7L7FJFJ||||L7|||F---7F-J|||L----7L----7LJL---JLJF---JL7F--7L-JL---7||7LJF-7F
7J7LLF.L7||L-J||FLJ7LJFJ|F-7JF-J|F7F7L-----JL-J||F7L7F7JF7LL-7F7L7LJLL-JLJ|FJ|L7L-7FJL-7||L-----JF---7L--------7L-----JL-7|F------J|L-7----7
LL7JF|F.FJ|JF-JL7F-7JFJFJL7L-JF7LJLJL-----7F7F-J||L7LJL-JL7F-J|L7L7F7JF--7|L7|FJF-JL---J||F------J7F-JF-------7|F7F------J|L---7-|L|F-J7-F-L
.FL-FJJFL7||L7F-JL7L-JFJF7L---JL---------7||LJF7LJFL-7F-7FJL-7|FL7LJL-JF7||FJ||.L7F----7|LJLF7F----JF-JF7F7F7FJ|||L--7F--7|F7F-JF77|L7----.L
F-LJ.|.LFJ|-JLJF--JF7FJFJ|F7F77F-------7FJLJF-JL7F7F7|L7||F-7||F7|F7F--JLJLJLLJ-JLJF7F7LJJF-J||F--7FJF-JLJLJLJFJ|L7F7LJF7|LJLJF-JL7L7|7.|LJJ
-.|FL-7|L-J7|F7L-7FJLJ7L7LJLJL-JF7F7FF7|L--7|F--J|||||FJLJL7|LJ|LJ||L7F7F7J|.|LLFFFJ||L7F7L-7|LJF-JL7L----7F-7L-J7||L7FJLJLF7F|F--JFLJJ7.F|J
|-F-JF|-F7F--JL7FJ|F---7L-7F7F7FJLJL-J|L7F7LJL---JLJ||L--7FJL--JF7|L7LJLJL-77F77F7L7||FJ||7FJL7JL---JF7F--J|FJF7F7|L7||F7F7|L-JL---7J|7|FL-7
FFL7L7J7||L-7F-J|FJL7F7L--J|||||F-----J|||L--7F7F-7FJL7F-JL----7||L7L7F7F7FJFJL-JL7|LJL7||FJF7|F7F-7||LJF--JL7||||L7||LJLJLJF---7F7L-7-L-J||
--7LF-7F||F7||F7LJ7-LJL--7FJLJLJL7F----7|L--7||LJ7LJF7LJ.F7FF77||L7L7||LJLJJL----7|L-7FJ||L-J||||L7L7L--JF-7FJ|||L7LJL7F---7L--7|||F-JLL|--J
L7|.|L-FJLJ||||L7F7FF7F--J|F7L|F7LJF---JL-7FJLJF----JL7FFJL7|L7LJ-|FJLJ.LJ7L|F--7|L--JL7|L77FJ|||.|FJF7F7L7LJFJLJFJ.F7LJF-7|F7FJLJLJJJ7F--J|
|LJ|L7JL--7LJLJFJ||FJ|L--7||L7FJL--JF--7F7LJF7FJF7F--7L7|F7LJFJLF-JL---7-FF-7L-7|L----7|L7L7L7|||FJL-JLJL-JF-JF7FJF7||F7|FJLJ|L--7LJ..FLJLL7
LJ77-L7||FJF7F7|J||L7L---J|L7LJF----JF-J|L7J|||FJLJF7L-J||L--J7.L7F7F--JFLL7L--JL--7F7||LL7|FJ|||L---------JF-J|L-JLJLJLJL--7L---JL|7|7.7|F7
|.F77.-J7|FJLJ||FJL7L-----J-L--JF----JLFJFJFJLJL-7FJL7LFJL7.F-777||LJ7|-F||L---7F-7LJLJL--J|L7LJL---------7FJF7|F-7F7F-7F7F7L---7J.|J.L7||J|
L7|L7FJLF||-F7||L-7L7F----7F7F7LL------JFJ-L7F---JL7FJFJF7L-JFJF7|L-7-77L7FL-F-J|.L7F---7F7L-JF-7F-------7|L-J|LJ-LJLJ|||LJL----JLFJ-7.FJ||L
L7J-LLFF-J|FJ|||F-JFJL---7||LJL7F-------JF7FJL--7F7|L-JFJL---JL|||F7|-77LLJ|.L7FJF-J|F--J|L---JFJL-7F----JL---JF7JF7-F7|L-----7F77JL-J-L-FFL
-J-|JLL|F7|L7LJLJF7L-7F7FJLJF-7|L--7F-7F7|||F---J|LJF--JF7F----J|LJLJF7FF|J|FFJL7L7FJL---JF7F-7|F7FJL7F--------JL-JL-J||F--7F7LJ|7.|-FL|7J.|
L.F.F-JLJLJJL7F-7|L-7||||F--J7||F7FJL7||LJLJ|F7F-JF7|-F7|||F---7L-7JJ7|||.LJFL--JFJ|F7F7F7||L7|LJLJ.FJL7F-----7F----7FJ|L-7LJ|F7L777-L|JL77.
FFL-J-F7J|JJ|||.LJF-JLJ|LJF--7||||L7FJLJF-7FJ|LJF7|LJFJLJ|||F77L--J|JL7F-.|.|LLF7L-J|LJLJLJL-JL--7F-JF7|L-7F7FJL---7|L7|F7L-7LJL-J-JF-7-J.F7
J..FF-JL7F7F-J|F7FJF7F7L-7|F7LJLJL-J|F7FJFJL-JF-J|L-7|F--J|LJL7F---7--7J--F7L-FJL---JF7F7F7F---7FJL-7|LJ|FJ|||F7F--JL-JLJL--JJF-7L|.|||FLFF7
.FFFL--7LJ|L--J||L7|LJL7FJLJL7F-7F--J|LJ||F7F7L-7L--J|||F-JF--J|F--JFFJ7.|.F77|F-----JLJLJLJF--JL---J|F--JFJ|LJ||F--7F-7F7F7F7|FJ7J.-7-F7L7J
|-LJ|JL|F7L---7||-LJF--J|F7F-JL7LJ7F7L-7FJ||||F-JF--7|L-JF7L---JL-7F7|7LJJFLFFJ|F-----7F----JF7F7F-7FJL---JFJF7LJ|F-JL7LJLJLJLJ||.7FL||L|7.L
L7.F||.||L-7F7LJ|F-7L--7|||L---JF--JL--JL-J|||L--JF-J|F--JL-7F-7F-J||F7J|J|J|L7|L-7F-7|L7F7F-J|||L7LJF-----JFJL-7||F7L|F7F7F7F7|7F777|F.L|||
FL7FF7-LJF-J|L-7LJFJF--JLJL---77L---------7LJ|F7F7L--JL----7|L7LJ|7|LJL---7|F.||F7LJFJL-J||L7FLJ|FJF-JF--7F7L7F7LJLJL-J|LJLJLJLJFJ|7LL|F-FLJ
LLL|JFJ|JL--JF7|F7L-JF7F7F7F-7L-7F--------JF7||||L7F--7F--7|L7|JF--JF---7FJ7L-LJ||F-JF---JL-JF--JL7L--JF7LJL-J|L-7F7F--J.F7F7F-7|FJ.-.LL7|.|
.|.L-|-F--7F-JLJ|L-7FJ||LJLJLL-7|L-7F-7F---J||||L7|L-7|L-7||LLJLL--7|JF7LJ7|7|F-J|L--JF---7F7L----JLF7FJ|F----JF7LJ|L----JLJLJFJ||F7.7-L|--F
|-.L-|LL-7|L7F7FJF-JL7||F----7FJL--JL7LJF---JLJ|FJL7FJL-7|LJLF7.|JFJL7||F7F--7|F7|F-7FL7F7LJL----7F7||L7|L---7FJL7FJF-7F---7F7L-JL-77|J7L.|J
-.7|.|FF-JL-J||L7L7F-J|||F7F7LJF-----JF-JJF--7LLJF7|L--7||F-7||7F7L7FJ||||L7FJLJ|LJFJF7LJL------7LJLJ|-|L---7LJF-JL7|JLJF7J||L7F---JJ.|LJ-F7
.L|J--7L7F-7FJL-J7||F7LJLJLJL7FJF----7L7F7|F-JF77||L---J|||FJ||FJL-J|FJLJL7|L7F-JF7L-J|F--------JF--7L-JF--7L--JJF7LJF7FJL7LJFJ|F--7.FLLJFF7
F||LF.|7||F||F--7FJ||L7F---7FJ|7L---7L7|||||F7|L-JL----7LJ||FJ|L---7||F---J|FJL--JL7F7|L-7F-7F7F7|F-JF--JF7L-----JL-7|||F7L7.L-J|F-J-.|.L|.|
.LJ-J-77LJFJ||F7LJFJL7|L-7FJL-JF----J7|LJLJLJLJF---7F--JLFJLJFJF7F7|||L-7-FJL-7FF7L|||L7|LJFLJLJLJL--JF--JL--7F-----J|LJ|L-JF7F-J|L|J.-7-LFJ
L|J.JF|F|LL7|LJL--JF7||F-J|F7F7L-----7|F7F7F7F7||F7|L7.F7|F--JFJLJLJ|L7FJFJF--JFJ|FJ|L7L7F7F--7F-7F7F7L--7FF7LJJF----JF7L---JLJF-JF77|LL.F||
||F77LJ---L||-F----JLJLJF7LJLJ|F7F7F-JLJLJLJ||||FJ|L-JFJLJL--7|F---7|7|L7|FJF7||FJL7|7L7|||L-7|L7LJLJL7F7L-JL7F7|F-7F-J|F7F-7F7L--J|J.F|.-FL
|.L-|-LJL|7LJFJF7F7F-7F7|L7F-7LJLJLJJF7LF7F7LJLJL7L7F7|F--7F-JLJF--JL7|FJ||FJL-JL7FJ|F-JLJ|F-J|FJF7F-7LJL---7LJLJL7|L-7||||-LJL----J-7LJ7FJ|
FJ.||L|.LJFLFL7|LJLJFJ||L7||FJF7F----JL-JLJL--7F-JFJ|||L-7LJF7LFJF-7FJ||FJLJF----JL-J|F7F7LJF7LJFJLJJL-----7|F7F7FJL7FJLJLJF7F7F-7JL7.FL|JJ7
7J.-7.L-F|J.F7LJF---JFJ|FJLJL-JLJF7F----------JL7FJ7|||F-JF-J|FJFJFJL7||L7F-JFF7F7F77LJLJL7FJL7FJF7F------7|||||||F7LJF--7||LJ|L7|F-7-F.LJ-L
F77-|F|F|||F|L-7L---7|7LJF-----7FJLJ7F--7F7F7F7FJ|F-JLJL7|L-7|L-JLL-7LJL-JL--7||||||F7FF--J|F7LJ-||L-----7LJLJLJ|LJL-7L-7|FJF7L7|LJFJ.|7L7FJ
L||.77F|J-F-L-7L--7FJ|F7-L----7|L----JF-J||||||L7||F---7L7F-JL7F---7L--7F--7FJ|||||LJL7L--7|||FF-J|F7LF7FJF7F7F7L----JF7|||FJL7LJF7L7F--.|-J
..L-L|7JF7FJJLL--7|L-J|L7F----JL-7LF--JF7|||LJL-JLJ|F77L-JL-7FJL7F7L---JL7FLJFJ|||L--7L-7|LJ|L-JF-J|L-J|L-JLJLJL-7F--7|||||L-7L7FJL-J-JF7J-|
FJ7L|.F.|JL-F-7F-JL--7|FJL------7L-JF--JLJLJF7F---7LJL7F7F--J|F7LJL-----7L7F7L7|||F-7|F-JF7|L--7|F7L--7L------7F-J|F-J|||LJF-JFJL-77||FLF.FL
FLL-FLJF77FFL7LJF7F-7|||F7F7F7F-JF--JF-7F-7FJ||F-7L---J||L-7FJ||F7F-7F--JFJ|L7||||L7|||F7|L----JLJL7F7L------7LJF7|L--JLJF7L77L---JF7-77L-JJ
J7LF|---J|L--L7FJLJFJ||||||LJLJF7L7F-J-|L7LJ7LJL7L---7J|L7FJ|FJ|||L7|L--7L-JFJ|||L-JLJLJ|L----7F--7LJL-------JF-JLJF7F---JL7L------7LJJ7F||J
LJ-J7FJ||L..7.LJF7-L7LJ|||L--7FJL-JL--7L7L---7F-JF-7FJFJFJL7||FJ||FJ|F7-|F--JFJ|L----7F-JF7F--J|F-JF7F7F-7F7F7|F7F7||L----7L7F-7F-7|JJ.-JJ-7
F7|LL|||-7J-F---JL--JF7LJL---J|F7F----J7L--7FJL-7|7LJ.L7|F7||||-||L7|||FJL7F7L7L7F7F-JL7|||L--7|L7FJ||||-LJLJ|||LJLJ|F-7F-JFJL7LJLLJ.LL-|LLJ
---F7||J-|J-L------7FJL7F7F--7||LJF---7F7F7LJF--JL-7F7FJ||LJLJL7||FJ||||F-J|L7|FJ||L-7FJFJ|LF7||FJ|FJ|||F-7F-J|L---7|L7||F7L7FJF-7.|7|..|77F
L7LJ-L.LFFF--------J|F-J||L7FJ||F-JF7FJ|||L-7L---7FJ|||FJL7F-7FJ||L7||||L-7|FJ|L7||F7||||FJFJ|LJL-JL7|LJL7LJJFJF7F-JL-JLJ|L7|L-JFJ7F7.-7||7|
.F-F.|J-|LL-----7F7FJ|F7|L-JL-JLJF-JLJFJLJF-JF7FFJL7|||L7L|L7LJFJL-JLJLJF-J||FJFJ|||||L7||L|FJ.F---7|L--7L---JFJLJF------JFJL--7|.L|.-7L-J.|
-7JJF-.FLFLF----J|||FJ|LJF-7F7F-7L-7FFJF--JF-JL7|F7LJ|L7L7|FJF-JF7F-7F-7|F-J|L7L7|||||FJ|L-J|F7L--7|L---JF7F-7|F--JF7F7F7FJ||F7LJ--7F7F-JLF.
|.|FJ.7JL|FL7F7F7|LJ|FJF7L7|||L7|F7|FJFJF7JL--7LJ||F-JLL7LJ||L--JLJFJL7LJL-7L7L7||||LJL7L--7||L7F-J|LF---J|L7|LJF7FJLJLJ|L---J|-||JL--LJ.-J7
J.-L7--7JLFLLJLJLJF7LJFJL-JLJ|FJLJ|LJFJ|||F7F7L7FJLJF7F7|F7L77F7F7FJF7L7-F7L7|FJ|||L--7|F7FJLJFJL-7L7L-7F7L-JL7FJ|L----7|F---7L777||.LFJF|-J
|7-LL-FJ.FFL|FF---JL--JF----7LJF-7L--JJFJ|||||L|L-7|||||||L-JFJ||LJFJL7L7||.||L7|||JF-J||||F--JJF7|FJF7LJL---7LJLL7F-7FJ|L--7|FJJ-JJFF-7|-7|
FJJ|L|L-|7-LJFL7F7F7F7FJF---JF7|JL--7F7L7|||||FJF-JFJLJ|||F7FJFJL--JJJL7||L7||FJ||L7L-7||||L7F-7|||L-J|F7F--7|F-7LLJ-|L7L7F-JLJ|L---|J|||.||
J|L|-7|.L|-||-LLJ||LJLJFL7F7FJLJF---J||.||||||L7|F7L7F-J|||||FJ|LF7.FF7||L7||||FJ|FJF7||||L7|L7|||L7F-J||L77|LJFJF7F7L-J7|L-7|LL7L7L7JF-J-F7
F7-7-|77J|.FF7FF-J|F-----J|LJF--JF7F7|L7|||||L7|LJ|FJL7FJ|||||F7FJL7FJ||L7||||||FJ|FJ||||L-J|FJ|||FJL7FJ|FJFJF7|FJLJL7F77L--J--.F.J-L-7J|JLJ
|JJLJJL7.|7|||FL-7|L---7F7|F7L---J||||L||||||FJ|F-JL-7||FJ|||LJ||F7||FJ|FJ|||||LJFJ||||LJF7J|||||LJF7LJFJL7L-JLJL7F--J|L----7JJF77FFLJ|.7..L
J7|-|.LJJJ-F|7F7LLJFF--J||||L-----J|||FJ|||LJL7|L7F7FJ||L7|||F-JLJ||||FJL7|LJ||F7L7L7|L-7|L-JL7||F-JL--JF7L------JL-7L|F----JJ|||JLL..L-F-7.
L|F-7-FL7J.L|-||F7F-JF7FJ|||F7F7F7FJ||L7||L--7||FJ|||7LJFJ|||L-7F7||||L-7|L7FJ||L-JFJ|F7||F7F-JLJL7F--7FJ|F-----7F-7L-JL----7F-JL777F-J.L|LF
.J7LJ|FJ|J.F7.||||L--J|L7||LJLJ||LJFLJ-LJ|F--JLJL7||L--7L7|||F-J|LJ|||F7||FJL7||F-7L7||||LJ|L-7F--J|F-JL7|L-7F-7LJ-|F-------JL-7FJF7JL-J7|FF
LFJJLLJF7LFJL-JLJ|F---JFJ||F---JL-7F-----J|F7|F7FJ|L7F7L7||||L-7L-7|||||||L7J||LJFJFJ|||L7LL7FJL7F7||F-7|L-7LJFJF-7|L-----7F7F7|L-J|7-|.7-|F
FFJ..LFFJ|L7F---7LJF7F7L7||L7F7F7FJL-7F--7LJL7||L7|FJ|L7|LJLJF-JF7|LJ||||L7L7||F-JFJ.LJ|FJF7||F7LJLJ||-|L-7L7FJFJFJ|F7F7F7LJLJLJF--J7..FJJL|
F-77---J77.LJF--JF7|||L7|||7LJ||||JF7LJF-JF-7LJL7||L7L7|L-7JFJF7||L-7||||FJFJ||L-7L---7LJ||LJLJ|FF--JL7|F7|.|L-JFJ|LJLJ|||F---7FJFJ..LJ7F--J
J--7J.|J|L7LFJF--J|||L7LJLJF--J|||FJL--JF7L7L7F-J||7L7||F-JFJFJ|||F7|||||L-JFJ|F-JF7F7L7F-JF7F7L7L7F-7||||L7L7F-JJF7F-7|||L-7FJL-7F7-|.|-L7J
|.-F7---7JF-L7|F--J|L-JF7F7L-7FJLJL7F-7FJ|FJ||L-7||F-J||L-7L7|FJ|||||LJ|L--7L7|L-7||||FJL7FJLJL7L7LJ-||LJL-JFJ|LF7|LJFJ||L7FJL7F7L7.||7|.FJF
7-|J.|LLJF7FFLJL7F-JF--JLJ|F7LJF7F7||FJL7|L7FJF7|||L7FJL7FJFJ|L7||||L7JL7F7|.LJJFJ|||||LFJ|F--7L7L--7|L----7L7L-JLJF7L7|L7|L-7||L-J.F--7-J..
|.77.J-LJ7F-LJJFJ|-|L--7F7LJL--JLJLJ||F7||FJL7|LJLJFJL7FJ|JL7|FJ|||L7L-7LJ||F---JFJ|||L7L7LJF-JFJF--J|F7F--JJL7F---JL-JL-JL--J|L7|L---LJJJ77
-.L|--L-JF|.||-L7|-F7F-J||F7F---7F7FJ||||||LFJL--7.L7FJL7L-7|||FJ||J|F-JF-J|L--7FJFJ||FJF|F7L-7L7L-7|LJ|L---7-|L-----------7J7L7|77F|77J..FL
.-.|L7F|JJLJ-7JFLJ.|LJF-JLJLJF7FJ|||-|||||L7|F7F7L-7||F-JF-J||||FJ|FJL-7L7FJ7F7|L7L-J|||FJ||F7|FJF7L--7|F---JFJF7F7F7F-7F-7L7JFLJJF-J-|-F-||
-77L7L|J7.LJJJ|F|J-L7FJF----7|LJFJ||FJ|LJ|FJLJLJ|F-JLJ|F7L-7||||L7|L7F-J|||FFJLJFJF--JL7L7|LJLJL7||F-7||L--7FJFJ||LJLJFLJ|L-J-JJLFJJ--J-|-J7
|7F.F7L-FF.L|L|7J.F-JL7|F7F7LJF7|FJ|L7|F-JL7F-7FJL--7J||L7FJ||||FJ|FJL--7|L7L7F7|7L-7F7|FJL7F7F-J||L7LJ|F-7|L7L7|L-7F7F-----7J.F7L.F7J.FL7.|
L-|F7FLFJF7.-7L7-FL-7FJLJLJL7FJ||L7L7LJL-7FJL7LJF--7L7||FJ|.||LJ|FJL-7F7||FJ-LJ|L7F-J||||F7LJ|L7FJL7||FJ|FJ|7L7|L-7LJ||F----JLF-J7L-J|-FJ.LJ
|FFLJJFLJLL-|77|.F-7LJ.F7FF7||F|L7L7L7F7FJL-7L-7L-7|FJLJL7|-||F-J|F--J|||||F---JFJL-7||||||F7L7|L7FJL7L7||FJF-JL7FJF7LJL-7|LL-.||77LF-F-L-.|
LLF7|F7J.LJ.||L7FL7L---JL-JLJ|FJFJ.|FJ|LJF7FJJFJF-J|L-7F-J||LJ|F7|L-7FJLJ||L-7F7|F--J|||LJ|||FJ|FJL7FJL||LJFL-7FJL7|L7F7FJ-J||-LL77.J-||L|7J
7JLJ||LL7-|7LJLL-L|F7F--7F--7|L7L7FJL7|F7||L-7L7L7FJF7||F7L7-FJ|||F-J|F--JL-7|||||F7FJLJJFJ|||-|L7-|L-7|L7F---JL-7|L7||LJ.LFF7.JJLJ.L|FF--J.
L7.LL...|.|L-7|J7.LJ||F7LJF7||J|FJL7FJ||||L7FJFJFJL7|LJ|||FJJL7|LJL-7||F7F-7|||||LJ|L---7L7||L7L-JFJF-J|FJL-7F7F-J|FJLJ.|-F-J|77FLJ-JL-J..F|
L77.FL.7--JL|F7-F---JLJ|F-JLJ|FJL-7||FJ|||FJL7L-JFL|L77LJ||JJ-LJFF7FJ||||L7LJLJ||7FJF7F7|FJ|L7L--7L7L-7||F7J||LJ.FJ|F7F-7FJLF77-|.|F-J.|.FF7
FL7F-.LL|-|.LL--L--7F--JL----JL7F7|||L7|LJL-7L--7-||FJ-L7LJFFF---JLJFJ|||FJJ|LFJL7L7|||||L7|FJF--JLL7FJLJ||FJL7J-L7LJLJFJ7J||J|LLL7LJJ|7-J.J
7-L77FJ.7.J7.|.L|LLLJ.LLF------J|||||FJL--7FJF--JL-LJJ7.7JF7-L---7F7|FJ||L-77L|F7|J||||||FLJL7L----7|L7JFJLJF7L7-JL7F-7|LJ|FJF-7|.|7..|7..|J
LJF---..|7|LJ|7FJ.|FF|JJL----7F-J||LJL-7F-JL-JJJ|7L|JLF-|LF7--JLFJ|LJ|FJL-7|-FJ|LJFJ|||LJF---JF7F--JL7L7L7F7|L-JJF-JL7LJJ-FLFJF-L.L7..L-7FF7
.F||7..F|--7-|-7.FFF|J|JF----JL7-|L--7-|||-L7|7FL7-|L7.FJ-F.F|J-|FJJJ||7FLLJJL7|7L|FJ||7.L--7FJ|L-7J||FJLLJLJJJ-F|F7FJL|JF7|.L|JJL-JF-FJL7FJ
F|L-J|F-J|-|-|||7-FJL7|-L-7F-7FJFJF--J-LJ-7LF-7L77JL.7F|J-|-F|7-LJ7-FLJ-|JFJ.||L7-|||LJF----JL7L7FJJFJL777L|FJ7FLLJ||-FJ|LJF--J7F-FL|L|.F|LJ
L7-.7J|.L|||---||J||FFL.J7LJFJ|-L7L--7F7JLJ7L-J|.||FL---F-J|L7-7J.L-7J|FF.|-7-L7L7LJ7LLL--7F7FJ|LJF-JF7|.||LJLFJ-|-LJ-|.L7F|||LFJF7LJ.7-LJF7
L7-7|F-L7LJ||.L|JFFFJ7J-L77FL7L-7L7F7LJL7FJ-F|--7FL7J|..LJ|L7|F--FLLJ.F||FJJ|7LL-J-L|-F7|-|||L--7.L-7|LJ7LJ7|.|LJLF7FL--JF-.--J.|JJ.FFJF|.LF
FJJ|F7F-7J.JJ-F7JLLJ.J-7F777J|F-J-LJ|F7FJJFFFJ||F-|7-F7.LLF-F|-J-F7-JF-L7LL.LF-LJJJLJJL7J-LJL-7FJ7|L|L7.|JJFF-JJ-LL|F7.|-J|FJJJ.L-F77|F-JL7|
F7.--JJ-L-7LJJJL7L|J-LL7.F---J|F7F|-LJ||J.FF---F-||-7.77-.-FJ|7..JJJ.L7|-|.|-||L7JLLL-LJ||.J-LLJ7L7.L7|7J-7-FJ|..-7LLJF|J-F|.|LL77|.LJJ|-L-7
|77|.LJ.FLLJ|F7J|FF7F7LJ-L---7LJ|-JFLFJL7-JJ|.L|FFJ--7.|L|7.-J-L77L7|J-L7.F7-J7F77-7F7FF|-JJ.|FL7-J7LLJ7JF--J|L--F|...F|7JF---FJ|7-7||LF7J7|
F-J-7J|-||F77F|-|F|LJ|FJ.FLF-JF-J|-FLL--J-|-J7.FFJ|-J7F-7LJ-JJ|.||||.LFLF7JLL|||L--JJL|-7L|JFJ7.F7LJ7|J|||JF|FL7--LJJ7F|J||L|-J-|J.F-L-FL7FJ
L-J.J.||.L7J.7JF|-|-|FF|7FLL--J-L77|-|J|FL7.FL--|-F7FJLL7-77J.F7L|-|7FJ-|L7-7.|L-7.FJJL||.|FFJJ-|J|FJJ--7|-|-7JF7|F7---LJF7.|7.L.|F|JL--J|7.
|--7J-L7--FJ.LL-7LFJLL7--FFLL.L--FJ|--7---L-7L7-LFF|J-LL|.|7L7JL-L-FJLJ.J.L-FLJ7-J77JJ.L--L|--L-J--|-FJL|JJL7J--J-JJJ.L..L--LF|J.F|--F-JJL|-";
            }
            else
            {
                Map = @"7-F7-
.FJ|7
SJLL7
|F--J
LJ.LJ";
            }
            n = Map.Split(delimiter_line, StringSplitOptions.None).Count();
            int[,] Tile = new int[n, n];
            int[] startingPoint = new int[2];
            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    int[] newTile = new int[2];
                    newTile[0] = r;
                    newTile[1] = c;
                    Tile[r, c] = Map.Split(delimiter_line, StringSplitOptions.None)[r][c];
                    if (Tile[r, c] == 'S') { startingPoint[0] = r; startingPoint[1] = c; }
                }
            }

            bool FindingLoop = false;
            pipes = new Pipes(startingPoint);
            while (!FindingLoop)
            {
                foreach (PipePath path in pipes.PipePath)
                {
                    ExplorePositions(path);
                }
                int ppn = pipes.PipePath.Count();
                for (int i = 0; i < ppn; i++)
                {
                    foreach (int[] point in pipes.PipePath[i].NextYou)
                    {
                        if (ContainsPoint(pipes.PipePath[i].NextYou, startingPoint))
                        {
                            var a = "";//7030
                        }

                        if (!ContainsPoint(pipes.PipePath[i].VisitedPositions, point))
                        {
                            if (pipes.PipePath[i].startingPath == startingPoint)
                            {
                                PipePath pipePath = new PipePath();
                                pipePath.startingPath = point;
                                pipePath.VisitedPositions.Add(startingPoint);
                                pipePath.VisitedPositions.Add(point);
                                pipePath.YouAre.Add(point);
                                pipes.PipePath.Add(pipePath);
                            }
                            else
                            {
                                pipes.PipePath[i].VisitedPositions.Add(point);
                                pipes.PipePath[i].YouAre.Add(point);
                            }
                        }
                    }
                    pipes.PipePath[i].NextYou.RemoveAll(x => x != null);
                }
                if (pipes.PipePath[0].startingPath.Equals(startingPoint)) pipes.PipePath.RemoveAt(0);

            }
            //ShowPipe();
        }

        public void ShowPipe()
        {

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    string text = ".";

                }
            }
        }

        public bool ContainsPoint(List<int[]> list, int[] point)
        {
            bool ret = false;
            foreach (var item in list)
            {
                if (item[0] == point[0] && item[1] == point[1]) return true;
            }
            return ret;
        }
        

        public bool IsAdjacentStartingPoint(int[] point)
        {
            return false;
        }
        

        public void ExplorePositions(PipePath pipes)
        {
            foreach (int[] point in pipes.YouAre)
            {
                if (point[0] > 0) if (PipesMaker(point, new int[] { point[0] - 1, point[1] }, "UP")) pipes.NextYou.Add(new int[] { point[0] - 1, point[1] });
                if (point[1] < n - 1) if (PipesMaker(point, new int[] { point[0], point[1] + 1 }, "RIGHT")) pipes.NextYou.Add(new int[] { point[0], point[1] + 1 });
                if (point[0] < n - 1) if (PipesMaker(point, new int[] { point[0] + 1, point[1] }, "DOWN")) pipes.NextYou.Add(new int[] { point[0] + 1, point[1] });
                if (point[1] > 0) if (PipesMaker(point, new int[] { point[0], point[1] - 1 }, "LEFT")) pipes.NextYou.Add(new int[] { point[0], point[1] - 1 });
            }
            pipes
                .YouAre.RemoveAll(x => x.Any());

        }

        public bool PipesMaker(int[] oldpoint, int[] newpoint, string moveTo)
        {
            //   S|-.  JL
            //         7F
            char oldTile = Map.Split(delimiter_line, StringSplitOptions.None)[oldpoint[0]][oldpoint[1]];
            char newTile = Map.Split(delimiter_line, StringSplitOptions.None)[newpoint[0]][newpoint[1]];
            switch (oldTile)
            {
                case 'S': break;
                case '|': if (!PossibleMoves.VERTICAL.Contains(moveTo)) return false; break;
                case '-': if (!PossibleMoves.HORIZONTAL.Contains(moveTo)) return false; break;
                case 'J': if (!PossibleMoves.J.Contains(moveTo)) return false; break;
                case 'L': if (!PossibleMoves.L.Contains(moveTo)) return false; break;
                case '7': if (!PossibleMoves.SEVEN.Contains(moveTo)) return false; break;
                case 'F': if (!PossibleMoves.F.Contains(moveTo)) return false; break;
                default: return false;
            }

            switch (moveTo)
            {
                case "UP":
                    if (PossibleMoves.UP.Contains(newTile)) return true;
                    break;
                case "RIGHT":
                    if (PossibleMoves.RIGHT.Contains(newTile)) return true;
                    break;
                case "DOWN":
                    if (PossibleMoves.DOWN.Contains(newTile)) return true;
                    break;
                case "LEFT":
                    if (PossibleMoves.LEFT.Contains(newTile)) return true;
                    break;
            }
            if (newTile == 'S') return true;//if (!pipe.VisitedPositions.Contains(oldpoint)) return true; 
            return false;
        }
        public static class PossibleMoves
        {
            //POSSIBLE DESTINATIONS
            public static char[] UP = new char[] { '|', 'F', '7' };
            public static char[] DOWN = new char[] { '|', 'L', 'J' };
            public static char[] LEFT = new char[] { '-', 'L', 'F' };
            public static char[] RIGHT = new char[] { '-', 'J', '7' };

            //POSSIBLE STARTING
            public static string[] J = new string[] { "LEFT", "UP" };
            public static string[] F = new string[] { "DOWN", "RIGHT" };
            public static string[] L = new string[] { "UP", "RIGHT" };
            public static string[] SEVEN = new string[] { "DOWN", "LEFT" };

            public static string[] VERTICAL = new string[] { "UP", "DOWN" };
            public static string[] HORIZONTAL = new string[] { "LEFT", "RIGHT" };
        }
        public class Pipes
        {
            public int[] startingPoint = new int[2];
            public List<PipePath> PipePath = new List<PipePath>();
            public Pipes(int[] point)
            {
                this.startingPoint = point;
                this.PipePath = new List<PipePath>();
                PipePath pipePath = new PipePath();
                pipePath.YouAre.Add(startingPoint);
                pipePath.startingPath = startingPoint;
                this.PipePath.Add(pipePath);

            }
        }
        public class PipePath
        {
            public PipePath()
            {
            }
            public PipePath(List<int[]> VisitedPositionForNewPath)
            {
                this.VisitedPositions = VisitedPositionForNewPath;
            }
            public List<int[]> VisitedPositions = new List<int[]>();
            public int[] startingPath = null;
            public List<int[]> YouAre = new List<int[]>();
            public List<int[]> NextYou = new List<int[]>();

        }
        public void Part2(object input, bool test, ref object solution)
        {
            test = true;

            if (!test)
            {
                Map = @"F--FJ77F-L7---J-F777-F7|77F|-J-F|77|F-L-J-77-L7FL77-7|----.F7-|-|.7-.-FL-FF-FF77.|.F-F-.F|.7.F.7-F-77.J7|----7LL7F|7|-F-J-777-77.F-7-FL77FF|
F7FJ.LF-7JJ||.JF-J-7F-FLJJ|F7JFL|LJ.|.|7LFF-J|J-L7LJFJF||.F.FFJFF7L7.L-JF7.FLL-FJ-J7F|L-7|F7.7-|.J7|F-7LJ|FFL7J-77||LF-JJ|L7JLLJLL--F7|.LFFJ
LL7|7FJFJ7L--LJ|.LF7--|JJL-.|7FF-7.LJ-LLJFLJL|.|.LJ||.7JF-7F|L-|J|L7F||LFJL|.7J|LJ.--77|.LLFF77||7F|JLF-77-|7F7LFJLJ-FJJ-7FLL7J.|7|7.L.FLF77
||FJ--.F-7FL7JJL7.|JL-LF777FFF7|-FJJ.L||--JF7JLL|777LL-.|FLL|.|J.|L|-L7.LFL7-JF7F77..L7F7JF7|L77JLFJJ.L7|LLJJJLFJFJJ.JJJ.L7.||FLFFF7F|FJ-||F
||JJF|-J7L|.LJ7..-J.|JL||F7F7JFF-7J.7|F7J-L|J-|-J7-7-|.FFL7LJ77|FJ7|JLJL--.||L|FFJ--LLJJFF|LJFJ77.||..F7J7FF|L7JF|J777|-|JJF|J7.L|.|FLF7FFJ|
|J|-|7JF77L-|JF-7J-7J-||LJLJL--7.|FF77|||..LF7-7L77F--L77L|FF|L77LFF-7JJFLFF7|F7-J-LF|7F-7L-7|7F7.F77F-7FL|77F7LFJ-JFLJFJJ-LJJ|L.|FF-7FL7JFJ
|LJJ||LL|77F|JLF7|FL-F7L-7F7F7FJF-7||-||77.F|.FL7|7LF7|L7|LF7JLJ|F-||JL-7|F|L-J|JJJ.FF-JFJF-JL7|L7||7L7L-7.FJ|J|LJ7.|.J|..FJ|L7.-7JLL||L-.JJ
|F|LF7.||-FJ||.L7-|.F|L--J|LJLJ-|FJ||L||F7F7777L|7F7FF7J|F7||7.FF|F|JL7J-F.L7F7|F7F7JL-7|-L-7FJ|FJ||F7L7FJ7|.|LL|FL--F-L7FF-|FJ-L|.-.L-7-FL7
L-F---|7.FLJJF||L...|L---7L7F7.FJL7||FJ||LJ|F77FFF7F7|L7FJ||L--7F-7-7-J7-|7LLJ|LJ|||JFL||F7F|L7|L-J|||-||F7F|J7FLJ-7-7-LF7LF|7JF-JL77LLL7.|J
J7F7JL|LFL-JJF77|-F-----7|FJ||7L-7||||FJ|F-J|L7F7||||L7|L7||F--JL7|-F7L7.|7FF7L7FJ||F7FJ||L7L7LJF-7||L-J||L-77L-J|-F-LF7.77L7J.|F7L-7-|JL|L|
.|L|FJ..LLL.FF-77F|F---7||L7|L-7FJLJ||L7|L-7|FJ|||LJL7|L7|LJL7J|FJL-J|J||LFFJL-JL-J|||L7||FJ7L-7|JLJL7F-JL7FJ-|J7LF77|||-L.LL|F.LJLL|7-7LL-J
-.|LJ||-F7|FJ.FF7.LJJF7|LJFJ|F-JL-7FJL7LJF-J||FJ|L--7|L7|L-7FJ.FL7F--JF7J.FJF7F---7LJL7|LJL-7-FJL7-F7|L---J|JF7LF-JLFF7||L-F--JF7|.LL77|JL-|
LLJ.LLJFJJ-7-FF||.J|J||L-7L7|L--7FJL-7L-7|F7|LJFJ7F-JL7||F-JL-77F|L---J|-FJFJLJLF-JF--J|F7F7L7|F7L7|||F----J.||7FFL7F7FJ7JFL||||LJ..L|7J|FF7
.||-.L|-|7.J7F-J|7--FJL-7L7||F--JL7F-JF-JLJLJF7L-7L--7|||L--7FJF7|F----J.|FJJLF7|F7|LF7||LJL7|LJL7||LJL7F--77||-F7.F7-JF7.-7L777.FF7F.L7--||
-|7.|||JLF7.FJF7L77.L--7L-JLJL7F--JL-7L--7F7FJL--JF7FJ||L7F-JL7|LJL-7F-77LJ7F7||LJ|L7|LJL7F-J|F--J||F--JL7FJFJ|FJL-J|7JFJ.L777JF7-J--JFJ7|.7
F77FFF---L7-L-JL7|77LF-JF7F7F-JL----7|F-7||LJF7F7.||L7||FJL-7FJ|F7F7LJFJF7|FJ|||F-JFJ|F--JL--JL-7FJ|L-7F7|L7|FJL7F--J---7L7FJ|FLL7.F|LLJ7L7|
|J|7.F7|.F-.|FF-J|F7LL--J||LJ7F-----J|L7|||F7|LJL7|L-JLJL-7FJL7||LJL--JFJL7L7|||L-7L7|L------7LFJL7|F-J|||FJ|L-7|L-7||-J7L7.FFJL|LJ-L7.|77L7
JF||7|F|7.|7|FL-7|||F7F--JL--7L--7F-7|FJ||||||F--JL----7F-JL-7||L--7LF7L-7|FJ||L--JFJ|F------JFJF7||L7L||||-L7FJ|F-JF7.|F-J7L|FF7-JJ-7--.|J|
F|LJ-|.|LFLJ|77FJLJLJ|L--7F--JF7-||FJLJFJ|||LJL-7F7F7F-J|F7F7|||F--JFJ|-FJLJFJL---7|||L-7F---7|FJ||L7L-JLJ|F-J|FJL7FJ|7F7.F|||FF|LLJ.||.LJJF
7J.F7J7---J.LLFJF---7|F7FJL--7|L7LJL-7FJFJ||F---J||||L-7||LJLJ||L--7L7L7L--7L7-F7FJL-JF-JL7F-JLJFJ|-L7F---JL7FJL7FJ|FJFJ|.F77JFFF.FFF7F-JL7J
F777.LF-JJ7F7LL7|F--J|||L7F--JL7L7F--JL7L7|||F7.FJ|||F7||L---7||F--J-L7|F7J|FJFJ|L-7F-JF7FJL7F7FJFJF-JL---7FJL7FJL-JL7L7|FJL77FJL.F7.7JJ.|J7
|F-J--7|.FJ-F7L|||F-7LJ|-|L7F7-L7|L--7FJFJ||LJL7L7||||||L7F--J||L--7F-J||L-JL-JFJF7|L7FJ|L-7|||L7||L7F----JL7FJ|F7F-7L-JLJF-J-J-J.||FJ|.F7.|
LLJ7FF-77|7-L|L||||-L-7L-JFJ|L-7|L7F7||.L7LJF--JF||||||L7|L--7LJF--JL-7|L----7FJ-|LJFJ|FJF-S|||FJ|F7||F-7.F7|L7||||FJF----JFF7J|FFLJJ7L-FJFJ
.FLF-LJ--LL7FF7LJ||F--JF-7|FJF-JL7|||||F7|F7|F7F7||||||FJL7F7|F-JF7F7FJ|LF7F7||F7|F7L7|L7L-7|||L7||||||FJFJLJFJ||LJL-JF7F7LF||-.JL|-L-7||||J
LF7L|.|LLJJLFJL-7LJL--7|.|||FJF7FJLJLJ|||LJ|||LJLJ||LJ||F7LJ||L7FJ|||L7L7|LJLJLJ|LJ|FJ|FJF-JLJL7|LJ||LJ|FJF-7L-JL---7FJLJL-7||..|7..F-J7-||.
F|7F-F---J.|L-7FJ-|JF7LJFJLJL-J|L---7FJ||F7|LJF7F7|L7FJLJL7FJ|FJ|FJ|L7|FJL----7FJ-FJ|||||L-7F7FJL7FJL7FJL7|-L-7F----JL7F7F-J|L7.-7..JJ.J.77-
-J||.|7|.F-FF7|L-7F-J|F-JF--7F-J-F--J|FJ||||F-JLJLJFJL--7J||FJL-JL7|FJ|L7-F7F-J|-FJFJFJ|F-7||||F-JL7J||F7|L--7|L-7LF7.LJ|L-7|FJ7JLF.|-||FL-7
|-F--F77-77FJ|L7FJL-7LJF7|F-JL--7L--7||FJ|LJL7F7-F7L--7FJFJ|L-7F7FJ|L7L7|FJ||F7L7L7L7L7LJFJLJ|||F-7|FJ||||F--JL--JFJL-7FJF7||L--7.|-J7LL7-|7
L||-FJ||LF-JFJFJ|F--JF-JLJL-7F-7|F--JLJ|7L-7FJ||FJL7F7|L7L7L--J|||FJFJL||L7|LJL7L7|FJ-|F-JF7J||LJFJ|L7||LJL7F7F--7|F--JL7|LJ|F--JF|||||-LFF7
F|7.JL-F-JF7L7L7|L---JJF----JL7LJL----7|F7||L-J||F-J||L7L7|F---J||L7L7FJ|L|L7F-JFJ|L7FJL--J|FJ|F7L7|L||L-7FJ|||F-J|L7-F7|L--JL----7J77FF-J|L
-JL7JFLL7FJL7L-J|F----7L-----7|F77F7F-J|||FJF--J||F7||LL7LJL---7LJJL7|L7|FJFJL-7L7|FJL-7F--JL7||L7|L7||F7||7||||F7|FJFJLJF-7F-----JJL|JL77FJ
.J.|-J..LJ7LL7F-JL-7F7L------JLJ|FJ|L-7||||FJF7FJ|||||F7L-7F---JF--7||J|||FJF7|L7|||F7FJL7F-7|||FJL7||LJ||L-J||LJLJL-JF7FJFJL-7.L-|F-7.LJJ.|
|FFJ7.FF7F7F-JL7-F7LJL--7F7F---7|L7|F7|LJLJL-J|L7||||LJ|F-J|F7F7L-7LJL7|||L7||F-J||||||F-JL7||LJ|F7|||F-J|F--J|F---7F7|LJ7L-7FJ7||FFJ.F7FJ--
-|7|F7FJLJLJF-7L7|L7F---J|||F--JL-JLJ|L7F-----JFJ||||F-JL-7LJ||L7FJF--J|||FJ||L-7LJ|||||F7FJ|L-7|||||||F7|L7JFJL--7|||L-7-F7||-F-LJL|7|-|-L|
.|F-JLJF----J-|FJ|FJL--7FJLJL7F-----7|-||F7F7F7L7|||||F7F7L7FJL7|L7|F7L||||-||F7L-7|||||||L7|F7|||||||||||FJFJF7F-JLJL7FJ|||||J|7J|.LJJ|||F|
FLL-7F7|F7F---JL7|L7F7FJ|F7F-J|F7F--J|FJ|||||||FJ|||||||||FJL-7||FJ|||FJ||L7||||F-J||||||L7|||LJ||||||||LJL-JFJLJF7F7JLJ-FJ|LJ||7--JJ|-7F||J
||7.LJLJ|LJF---7LJFJ||L-J||L--J||L--7||FJ|LJ|||L7||||LJLJ|L-7FJ|LJJ||||FJL7|||||L7FJ|||LJFJ||L-7||LJ|||||F7F-JLF-JLJL-7F7L7|F-7JJ|.LF|7.-L|-
F7-F-7F7L--JJF-JF-J7||F--JL--7FJL---J||L7|.FJ||FJ||||F7F-JF-JL7L7F-J|||L7FJ||LJ|FJ|FJ|L7JL7||F-J||F-J|||FJLJ.F7L7F----J|L-JLJFJJ-FL7L-F7J.|.
F77L7LJ|-F7F7L-7L7F7|||F7F--7|L7F---7||.|L7|FJ||FJ||LJ|L-7L--7L-JL-7|LJ7|L7|L-7||FJL7L7L-7|||L77LJ|F7|||L----JL-JL----7L7F-7FJF7JFJL7|||FF|J
|L--JF7L-JLJL-7|FJ||||LJ||F-JL7LJF7FJ||FJFJLJFJ|L-J|F7L7FL7F-JF----JL--7|FJL7FJ||L7FJ.|F7|LJL7L-7JLJ||LJJF-7F--7F7F7F7L7LJFJL-JL-7.L7F-|-FJJ
|F--7||F7F7F-7|||FJLJ|F7LJL7F7L7FJLJJLJL7L-7-L7|F--J||FJF-JL7FJF7F7F-7FJ||-FJL7||FJ||FJ||L-7FJF7L-7FJ|F--JFJL-7||||||L7L7FJF-7F--J7FJ|LL-|J.
LJF-J|||LJLJFJLJ|L-7FJ|L7F7LJL7LJF--7F7FJF7|F7LJL7F7||L7L7F7|L-JLJ||FJ|7|L7|F-JLJL7L7L7||F-JL7||F-J|FJL--7L---JLJ||LJFJFJ|FJ7|L-7J|-L-7J|F77
FFJF7|LJF7F-JF-7L--JL7|FJ|L7F7|F-JF-J||L-J|LJL--7|||||FJFJ|||F7F-7||L7L7L-J|L---7FJFJ7||||F7FJ||L7JLJF7F7L7F7F7LFJL-7L-JF|L-7L-7|F7J|LF77J-J
FL-J||F-JLJF7L7L7F--7||L7L7||LJL7|L--JL--7L7F---JLJ||||.L-JLJ||L7LJL7L7L-7-L----JL7L-7LJ|LJ|L7|L7L-7FJLJL-J|||L7L7F7L-7F7|F-JF7LJ||JL-|LJ.|J
7FL|||L--7FJL7||LJF7|||FJFJLJF-7|F------7L7|L----7FJ||L7F--7FJL-JF7FJFJF7L-------7|F7|F-JF7|L|L7|F-JL7F----JLJFJL|||F7LJLJL-7||F7|L7J|L77FL7
|7JFJ|L||||F-J|F--JLJLJL-JF7FJ|LJL-----7L7|L7F---JL-J|FJL-7LJF7F7|LJFL7|L-7F-7F7FJ|||||F-J|L7L-J|L7F-J|F------JF7LJLJL-7F---J|||||FJ|F--|J7.
L7-|FJF|FJ||F-JL-7F7F7F7F-J||JF---7F---JFJ|FJL-7F7F-7||F--JF-JLJ|L-7F-JL-7||7|||L7||||||F-JFJJF7L-JL--JL-------JL7F7F-7LJF---JLJ|||F7|||LJF7
L|-LJFJ-L7|||F---J|LJ||||7FJL7L-7FJL-7F7L-JL7F7LJLJFJ||L7F-JF---JF7|L7F7FJ|L7||L-JLJLJLJ|F-JF-JL--7FF7F---7F-----J|||FJFFJF-----J|LJL7JFLFL-
.|.|||JJ|LJLJL7F-7|F7LJLJFJF7|F-JL--7LJL-7F7LJL--7FJL||LLJF-JF7F7|||FJ||L7|FJ|L----7F---J|F7|F7F-7L-J|L--7LJF-7F7FJ|||F7L7L---7F-JF7FJ.7.|L|
--J-LJF-F-7F7FLJFJ||L----JFJLJL7F--7L7F-7LJL---7||L-7LJF--JF7|LJ||LJL7|L7||L7L7F--7|L7F-7||LJ|LJLL--7L--7L--JFJ|||FJ||||-L7F--J|F7|LJJ7F7J-7
F7.|7.JLL7LJ|F7-L-JL-----7|F---J|F7L7LJ|L---7F7L7L--JF7L--7|||F-JL-7FJ|7LJ|FJFJL7FJ|F||FJ||F7L7F----JF-7L----JJ|LJL-J|||F-JL---J||L----J|.--
LLFF-F7F7L-7LJL7F7F------J|L----J||FJF7F---7||L7L----JL7|FJ|LJ|F-7FJL7L--7|L7|F7|L7|FJ||FJLJL-JL--7F7|FJF---7F7L7F---J||L7F----7||F7F-7FJ-LJ
L--|FJL|L--JF-7LJLJF7F-7F7|F----7|LJFJLJF7FJLJ7L7F--7F7L7|FJF7||L|L-7L7F7||FJ||LJJLJL-J||F7F-----7LJLJL7|F--J||FJL--7FJL-JL7FF7LJLJ|||||...|
||LL77LL-7F7L7|F-7FJ||J||LJL---7|L--JF--JLJF7FF7|L-7LJL-JLJFJLJL7L7FJFJ||||L7|||F---7F-J|||L----7L----7LJL---JLJF---JL7F--7L-JL---7||7LJF-7F
7J7LLF.L7||L-J||FLJ7LJFJ|F-7JF-J|F7F7L-----JL-J||F7L7F7JF7LL-7F7L7LJLL-JLJ|FJ|L7L-7FJL-7||L-----JF---7L--------7L-----JL-7|F------J|L-7----7
LL7JF|F.FJ|JF-JL7F-7JFJFJL7L-JF7LJLJL-----7F7F-J||L7LJL-JL7F-J|L7L7F7JF--7|L7|FJF-JL---J||F------J7F-JF-------7|F7F------J|L---7-|L|F-J7-F-L
.FL-FJJFL7||L7F-JL7L-JFJF7L---JL---------7||LJF7LJFL-7F-7FJL-7|FL7LJL-JF7||FJ||.L7F----7|LJLF7F----JF-JF7F7F7FJ|||L--7F--7|F7F-JF77|L7----.L
F-LJ.|.LFJ|-JLJF--JF7FJFJ|F7F77F-------7FJLJF-JL7F7F7|L7||F-7||F7|F7F--JLJLJLLJ-JLJF7F7LJJF-J||F--7FJF-JLJLJLJFJ|L7F7LJF7|LJLJF-JL7L7|7.|LJJ
-.|FL-7|L-J7|F7L-7FJLJ7L7LJLJL-JF7F7FF7|L--7|F--J|||||FJLJL7|LJ|LJ||L7F7F7J|.|LLFFFJ||L7F7L-7|LJF-JL7L----7F-7L-J7||L7FJLJLF7F|F--JFLJJ7.F|J
|-F-JF|-F7F--JL7FJ|F---7L-7F7F7FJLJL-J|L7F7LJL---JLJ||L--7FJL--JF7|L7LJLJL-77F77F7L7||FJ||7FJL7JL---JF7F--J|FJF7F7|L7||F7F7|L-JL---7J|7|FL-7
FFL7L7J7||L-7F-J|FJL7F7L--J|||||F-----J|||L--7F7F-7FJL7F-JL----7||L7L7F7F7FJFJL-JL7|LJL7||FJF7|F7F-7||LJF--JL7||||L7||LJLJLJF---7F7L-7-L-J||
--7LF-7F||F7||F7LJ7-LJL--7FJLJLJL7F----7|L--7||LJ7LJF7LJ.F7FF77||L7L7||LJLJJL----7|L-7FJ||L-J||||L7L7L--JF-7FJ|||L7LJL7F---7L--7|||F-JLL|--J
L7|.|L-FJLJ||||L7F7FF7F--J|F7L|F7LJF---JL-7FJLJF----JL7FFJL7|L7LJ-|FJLJ.LJ7L|F--7|L--JL7|L77FJ|||.|FJF7F7L7LJFJLJFJ.F7LJF-7|F7FJLJLJJJ7F--J|
|LJ|L7JL--7LJLJFJ||FJ|L--7||L7FJL--JF--7F7LJF7FJF7F--7L7|F7LJFJLF-JL---7-FF-7L-7|L----7|L7L7L7|||FJL-JLJL-JF-JF7FJF7||F7|FJLJ|L--7LJ..FLJLL7
LJ77-L7||FJF7F7|J||L7L---J|L7LJF----JF-J|L7J|||FJLJF7L-J||L--J7.L7F7F--JFLL7L--JL--7F7||LL7|FJ|||L---------JF-J|L-JLJLJLJL--7L---JL|7|7.7|F7
|.F77.-J7|FJLJ||FJL7L-----J-L--JF----JLFJFJFJLJL-7FJL7LFJL7.F-777||LJ7|-F||L---7F-7LJLJL--J|L7LJL---------7FJF7|F-7F7F-7F7F7L---7J.|J.L7||J|
L7|L7FJLF||-F7||L-7L7F----7F7F7LL------JFJ-L7F---JL7FJFJF7L-JFJF7|L-7-77L7FL-F-J|.L7F---7F7L-JF-7F-------7|L-J|LJ-LJLJ|||LJL----JLFJ-7.FJ||L
L7J-LLFF-J|FJ|||F-JFJL---7||LJL7F-------JF7FJL--7F7|L-JFJL---JL|||F7|-77LLJ|.L7FJF-J|F--J|L---JFJL-7F----JL---JF7JF7-F7|L-----7F77JL-J-L-FFL
-J-|JLL|F7|L7LJLJF7L-7F7FJLJF-7|L--7F-7F7|||F---J|LJF--JF7F----J|LJLJF7FF|J|FFJL7L7FJL---JF7F-7|F7FJL7F--------JL-JL-J||F--7F7LJ|7.|-FL|7J.|
L.F.F-JLJLJJL7F-7|L-7||||F--J7||F7FJL7||LJLJ|F7F-JF7|-F7|||F---7L-7JJ7|||.LJFL--JFJ|F7F7F7||L7|LJLJ.FJL7F-----7F----7FJ|L-7LJ|F7L777-L|JL77.
FFL-J-F7J|JJ|||.LJF-JLJ|LJF--7||||L7FJLJF-7FJ|LJF7|LJFJLJ|||F77L--J|JL7F-.|.|LLF7L-J|LJLJLJL-JL--7F-JF7|L-7F7FJL---7|L7|F7L-7LJL-J-JF-7-J.F7
J..FF-JL7F7F-J|F7FJF7F7L-7|F7LJLJL-J|F7FJFJL-JF-J|L-7|F--J|LJL7F---7--7J--F7L-FJL---JF7F7F7F---7FJL-7|LJ|FJ|||F7F--JL-JLJL--JJF-7L|.|||FLFF7
.FFFL--7LJ|L--J||L7|LJL7FJLJL7F-7F--J|LJ||F7F7L-7L--J|||F-JF--J|F--JFFJ7.|.F77|F-----JLJLJLJF--JL---J|F--JFJ|LJ||F--7F-7F7F7F7|FJ7J.-7-F7L7J
|-LJ|JL|F7L---7||-LJF--J|F7F-JL7LJ7F7L-7FJ||||F-JF--7|L-JF7L---JL-7F7|7LJJFLFFJ|F-----7F----JF7F7F-7FJL---JFJF7LJ|F-JL7LJLJLJLJ||.7FL||L|7.L
L7.F||.||L-7F7LJ|F-7L--7|||L---JF--JL--JL-J|||L--JF-J|F--JL-7F-7F-J||F7J|J|J|L7|L-7F-7|L7F7F-J|||L7LJF-----JFJL-7||F7L|F7F7F7F7|7F777|F.L|||
FL7FF7-LJF-J|L-7LJFJF--JLJL---77L---------7LJ|F7F7L--JL----7|L7LJ|7|LJL---7|F.||F7LJFJL-J||L7FLJ|FJF-JF--7F7L7F7LJLJL-J|LJLJLJLJFJ|7LL|F-FLJ
LLL|JFJ|JL--JF7|F7L-JF7F7F7F-7L-7F--------JF7||||L7F--7F--7|L7|JF--JF---7FJ7L-LJ||F-JF---JL-JF--JL7L--JF7LJL-J|L-7F7F--J.F7F7F-7|FJ.-.LL7|.|
.|.L-|-F--7F-JLJ|L-7FJ||LJLJLL-7|L-7F-7F---J||||L7|L-7|L-7||LLJLL--7|JF7LJ7|7|F-J|L--JF---7F7L----JLF7FJ|F----JF7LJ|L----JLJLJFJ||F7.7-L|--F
|-.L-|LL-7|L7F7FJF-JL7||F----7FJL--JL7LJF---JLJ|FJL7FJL-7|LJLF7.|JFJL7||F7F--7|F7|F-7FL7F7LJL----7F7||L7|L---7FJL7FJF-7F---7F7L-JL-77|J7L.|J
-.7|.|FF-JL-J||L7L7F-J|||F7F7LJF-----JF-JJF--7LLJF7|L--7||F-7||7F7L7FJ||||L7FJLJ|LJFJF7LJL------7LJLJ|-|L---7LJF-JL7|JLJF7J||L7F---JJ.|LJ-F7
.L|J--7L7F-7FJL-J7||F7LJLJLJL7FJF----7L7F7|F-JF77||L---J|||FJ||FJL-J|FJLJL7|L7F-JF7L-J|F--------JF--7L-JF--7L--JJF7LJF7FJL7LJFJ|F--7.FLLJFF7
F||LF.|7||F||F--7FJ||L7F---7FJ|7L---7L7|||||F7|L-JL----7LJ||FJ|L---7||F---J|FJL--JL7F7|L-7F-7F7F7|F-JF--JF7L-----JL-7|||F7L7.L-J|F-J-.|.L|.|
.LJ-J-77LJFJ||F7LJFJL7|L-7FJL-JF----J7|LJLJLJLJF---7F--JLFJLJFJF7F7|||L-7-FJL-7FF7L|||L7|LJFLJLJLJL--JF--JL--7F-----J|LJ|L-JF7F-J|L|J.-7-LFJ
L|J.JF|F|LL7|LJL--JF7||F-J|F7F7L-----7|F7F7F7F7||F7|L7.F7|F--JFJLJLJ|L7FJFJF--JFJ|FJ|L7L7F7F--7F-7F7F7L--7FF7LJJF----JF7L---JLJF-JF77|LL.F||
||F77LJ---L||-F----JLJLJF7LJLJ|F7F7F-JLJLJLJ||||FJ|L-JFJLJL--7|F---7|7|L7|FJF7||FJL7|7L7|||L-7|L7LJLJL7F7L-JL7F7|F-7F-J|F7F-7F7L--J|J.F|.-FL
|.L-|-LJL|7LJFJF7F7F-7F7|L7F-7LJLJLJJF7LF7F7LJLJL7L7F7|F--7F-JLJF--JL7|FJ||FJL-JL7FJ|F-JLJ|F-J|FJF7F-7LJL---7LJLJL7|L-7||||-LJL----J-7LJ7FJ|
FJ.||L|.LJFLFL7|LJLJFJ||L7||FJF7F----JL-JLJL--7F-JFJ|||L-7LJF7LFJF-7FJ||FJLJF----JL-J|F7F7LJF7LJFJLJJL-----7|F7F7FJL7FJLJLJF7F7F-7JL7.FL|JJ7
7J.-7.L-F|J.F7LJF---JFJ|FJLJL-JLJF7F----------JL7FJ7|||F-JF-J|FJFJFJL7||L7F-JFF7F7F77LJLJL7FJL7FJF7F------7|||||||F7LJF--7||LJ|L7|F-7-F.LJ-L
F77-|F|F|||F|L-7L---7|7LJF-----7FJLJ7F--7F7F7F7FJ|F-JLJL7|L-7|L-JLL-7LJL-JL--7||||||F7FF--J|F7LJ-||L-----7LJLJLJ|LJL-7L-7|FJF7L7|LJFJ.|7L7FJ
L||.77F|J-F-L-7L--7FJ|F7-L----7|L----JF-J||||||L7||F---7L7F-JL7F---7L--7F--7FJ|||||LJL7L--7|||FF-J|F7LF7FJF7F7F7L----JF7|||FJL7LJF7L7F--.|-J
..L-L|7JF7FJJLL--7|L-J|L7F----JL-7LF--JF7|||LJL-JLJ|F77L-JL-7FJL7F7L---JL7FLJFJ|||L--7L-7|LJ|L-JF-J|L-J|L-JLJLJL-7F--7|||||L-7L7FJL-J-JF7J-|
FJ7L|.F.|JL-F-7F-JL--7|FJL------7L-JF--JLJLJF7F---7LJL7F7F--J|F7LJL-----7L7F7L7|||F-7|F-JF7|L--7|F7L--7L------7F-J|F-J|||LJF-JFJL-77||FLF.FL
FLL-FLJF77FFL7LJF7F-7|||F7F7F7F-JF--JF-7F-7FJ||F-7L---J||L-7FJ||F7F-7F--JFJ|L7||||L7|||F7|L----JLJL7F7L------7LJF7|L--JLJF7L77L---JF7-77L-JJ
J7LF|---J|L--L7FJLJFJ||||||LJLJF7L7F-J-|L7LJ7LJL7L---7J|L7FJ|FJ|||L7|L--7L-JFJ|||L-JLJLJ|L----7F--7LJL-------JF-JLJF7F---JL7L------7LJJ7F||J
LJ-J7FJ||L..7.LJF7-L7LJ|||L--7FJL-JL--7L7L---7F-JF-7FJFJFJL7||FJ||FJ|F7-|F--JFJ|L----7F-JF7F--J|F-JF7F7F-7F7F7|F7F7||L----7L7F-7F-7|JJ.-JJ-7
F7|LL|||-7J-F---JL--JF7LJL---J|F7F----J7L--7FJL-7|7LJ.L7|F7||||-||L7|||FJL7F7L7L7F7F-JL7|||L--7|L7FJ||||-LJLJ|||LJLJ|F-7F-JFJL7LJLLJ.LL-|LLJ
---F7||J-|J-L------7FJL7F7F--7||LJF---7F7F7LJF--JL-7F7FJ||LJLJL7||FJ||||F-J|L7|FJ||L-7FJFJ|LF7||FJ|FJ|||F-7F-J|L---7|L7||F7L7FJF-7.|7|..|77F
L7LJ-L.LFFF--------J|F-J||L7FJ||F-JF7FJ|||L-7L---7FJ|||FJL7F-7FJ||L7||||L-7|FJ|L7||F7||||FJFJ|LJL-JL7|LJL7LJJFJF7F-JL-JLJ|L7|L-JFJ7F7.-7||7|
.F-F.|J-|LL-----7F7FJ|F7|L-JL-JLJF-JLJFJLJF-JF7FFJL7|||L7L|L7LJFJL-JLJLJF-J||FJFJ|||||L7||L|FJ.F---7|L--7L---JFJLJF------JFJL--7|.L|.-7L-J.|
-7JJF-.FLFLF----J|||FJ|LJF-7F7F-7L-7FFJF--JF-JL7|F7LJ|L7L7|FJF-JF7F-7F-7|F-J|L7L7|||||FJ|L-J|F7L--7|L---JF7F-7|F--JF7F7F7FJ||F7LJ--7F7F-JLF.
|.|FJ.7JL|FL7F7F7|LJ|FJF7L7|||L7|F7|FJFJF7JL--7LJ||F-JLL7LJ||L--JLJFJL7LJL-7L7L7||||LJL7L--7||L7F-J|LF---J|L7|LJF7FJLJLJ|L---J|-||JL--LJ.-J7
J.-L7--7JLFLLJLJLJF7LJFJL-JLJ|FJLJ|LJFJ|||F7F7L7FJLJF7F7|F7L77F7F7FJF7L7-F7L7|FJ|||L--7|F7FJLJFJL-7L7L-7F7L-JL7FJ|L----7|F---7L777||.LFJF|-J
|7-LL-FJ.FFL|FF---JL--JF----7LJF-7L--JJFJ|||||L|L-7|||||||L-JFJ||LJFJL7L7||.||L7|||JF-J||||F--JJF7|FJF7LJL---7LJLL7F-7FJ|L--7|FJJ-JJFF-7|-7|
FJJ|L|L-|7-LJFL7F7F7F7FJF---JF7|JL--7F7L7|||||FJF-JFJLJ|||F7FJFJL--JJJL7||L7||FJ||L7L-7||||L7F-7|||L-J|F7F--7|F-7LLJ-|L7L7F-JLJ|L---|J|||.||
J|L|-7|.L|-||-LLJ||LJLJFL7F7FJLJF---J||.||||||L7|F7L7F-J|||||FJ|LF7.FF7||L7||||FJ|FJF7||||L7|L7|||L7F-J||L77|LJFJF7F7L-J7|L-7|LL7L7L7JF-J-F7
F7-7-|77J|.FF7FF-J|F-----J|LJF--JF7F7|L7|||||L7|LJ|FJL7FJ|||||F7FJL7FJ||L7||||||FJ|FJ||||L-J|FJ|||FJL7FJ|FJFJF7|FJLJL7F77L--J--.F.J-L-7J|JLJ
|JJLJJL7.|7|||FL-7|L---7F7|F7L---J||||L||||||FJ|F-JL-7||FJ|||LJ||F7||FJ|FJ|||||LJFJ||||LJF7J|||||LJF7LJFJL7L-JLJL7F--J|L----7JJF77FFLJ|.7..L
J7|-|.LJJJ-F|7F7LLJFF--J||||L-----J|||FJ|||LJL7|L7F7FJ||L7|||F-JLJ||||FJL7|LJ||F7L7L7|L-7|L-JL7||F-JL--JF7L------JL-7L|F----JJ|||JLL..L-F-7.
L|F-7-FL7J.L|-||F7F-JF7FJ|||F7F7F7FJ||L7||L--7||FJ|||7LJFJ|||L-7F7||||L-7|L7FJ||L-JFJ|F7||F7F-JLJL7F--7FJ|F-----7F-7L-JL----7F-JL777F-J.L|LF
.J7LJ|FJ|J.F7.||||L--J|L7||LJLJ||LJFLJ-LJ|F--JLJL7||L--7L7|||F-J|LJ|||F7||FJL7||F-7L7||||LJ|L-7F--J|F-JL7|L-7F-7LJ-|F-------JL-7FJF7JL-J7|FF
LFJJLLJF7LFJL-JLJ|F---JFJ||F---JL-7F-----J|F7|F7FJ|L7F7L7||||L-7L-7|||||||L7J||LJFJFJ|||L7LL7FJL7F7||F-7|L-7LJFJF-7|L-----7F7F7|L-J|7-|.7-|F
FFJ..LFFJ|L7F---7LJF7F7L7||L7F7F7FJL-7F--7LJL7||L7|FJ|L7|LJLJF-JF7|LJ||||L7L7||F-JFJ.LJ|FJF7||F7LJLJ||-|L-7L7FJFJFJ|F7F7F7LJLJLJF--J7..FJJL|
F-77---J77.LJF--JF7|||L7|||7LJ||||JF7LJF-JF-7LJL7||L7L7|L-7JFJF7||L-7||||FJFJ||L-7L---7LJ||LJLJ|FF--JL7|F7|.|L-JFJ|LJLJ|||F---7FJFJ..LJ7F--J
J--7J.|J|L7LFJF--J|||L7LJLJF--J|||FJL--JF7L7L7F-J||7L7||F-JFJFJ|||F7|||||L-JFJ|F-JF7F7L7F-JF7F7L7L7F-7||||L7L7F-JJF7F-7|||L-7FJL-7F7-|.|-L7J
|.-F7---7JF-L7|F--J|L-JF7F7L-7FJLJL7F-7FJ|FJ||L-7||F-J||L-7L7|FJ|||||LJ|L--7L7|L-7||||FJL7FJLJL7L7LJ-||LJL-JFJ|LF7|LJFJ||L7FJL7F7L7.||7|.FJF
7-|J.|LLJF7FFLJL7F-JF--JLJ|F7LJF7F7||FJL7|L7FJF7|||L7FJL7FJFJ|L7||||L7JL7F7|.LJJFJ|||||LFJ|F--7L7L--7|L----7L7L-JLJF7L7|L7|L-7||L-J.F--7-J..
|.77.J-LJ7F-LJJFJ|-|L--7F7LJL--JLJLJ||F7||FJL7|LJLJFJL7FJ|JL7|FJ|||L7L-7LJ||F---JFJ|||L7L7LJF-JFJF--J|F7F--JJL7F---JL-JL-JL--J|L7|L---LJJJ77
-.L|--L-JF|.||-L7|-F7F-J||F7F---7F7FJ||||||LFJL--7.L7FJL7L-7|||FJ||J|F-JF-J|L--7FJFJ||FJF|F7L-7L7L-7|LJ|L---7-|L-----------7J7L7|77F|77J..FL
.-.|L7F|JJLJ-7JFLJ.|LJF-JLJLJF7FJ|||-|||||L7|F7F7L-7||F-JF-J||||FJ|FJL-7L7FJ7F7|L7L-J|||FJ||F7|FJF7L--7|F---JFJF7F7F7F-7F-7L7JFLJJF-J-|-F-||
-77L7L|J7.LJJJ|F|J-L7FJF----7|LJFJ||FJ|LJ|FJLJLJ|F-JLJ|F7L-7||||L7|L7F-J|||FFJLJFJF--JL7L7|LJLJL7||F-7||L--7FJFJ||LJLJFLJ|L-J-JJLFJJ--J-|-J7
|7F.F7L-FF.L|L|7J.F-JL7|F7F7LJF7|FJ|L7|F-JL7F-7FJL--7J||L7FJ||||FJ|FJL--7|L7L7F7|7L-7F7|FJL7F7F-J||L7LJ|F-7|L7L7|L-7F7F-----7J.F7L.F7J.FL7.|
L-|F7FLFJF7.-7L7-FL-7FJLJLJL7FJ||L7L7LJL-7FJL7LJF--7L7||FJ|.||LJ|FJL-7F7||FJ-LJ|L7F-J||||F7LJ|L7FJL7||FJ|FJ|7L7|L-7LJ||F----JLF-J7L-J|-FJ.LJ
|FFLJJFLJLL-|77|.F-7LJ.F7FF7||F|L7L7L7F7FJL-7L-7L-7|FJLJL7|-||F-J|F--J|||||F---JFJL-7||||||F7L7|L7FJL7L7||FJF-JL7FJF7LJL-7|LL-.||77LF-F-L-.|
LLF7|F7J.LJ.||L7FL7L---JL-JLJ|FJFJ.|FJ|LJF7FJJFJF-J|L-7F-J||LJ|F7|L-7FJLJ||L-7F7|F--J|||LJ|||FJ|FJL7FJL||LJFL-7FJL7|L7F7FJ-J||-LL77.J-||L|7J
7JLJ||LL7-|7LJLL-L|F7F--7F--7|L7L7FJL7|F7||L-7L7L7FJF7||F7L7-FJ|||F-J|F--JL-7|||||F7FJLJJFJ|||-|L7-|L-7|L7F---JL-7|L7||LJ.LFF7.JJLJ.L|FF--J.
L7.LL...|.|L-7|J7.LJ||F7LJF7||J|FJL7FJ||||L7FJFJFJL7|LJ|||FJJL7|LJL-7||F7F-7|||||LJ|L---7L7||L7L-JFJF-J|FJL-7F7F-J|FJLJ.|-F-J|77FLJ-JL-J..F|
L77.FL.7--JL|F7-F---JLJ|F-JLJ|FJL-7||FJ|||FJL7L-JFL|L77LJ||JJ-LJFF7FJ||||L7LJLJ||7FJF7F7|FJ|L7L--7L7L-7||F7J||LJ.FJ|F7F-7FJLF77-|.|F-J.|.FF7
FL7F-.LL|-|.LL--L--7F--JL----JL7F7|||L7|LJL-7L--7-||FJ-L7LJFFF---JLJFJ|||FJJ|LFJL7L7|||||L7|FJF--JLL7FJLJ||FJL7J-L7LJLJFJ7J||J|LLL7LJJ|7-J.J
7-L77FJ.7.J7.|.L|LLLJ.LLF------J|||||FJL--7FJF--JL-LJJ7.7JF7-L---7F7|FJ||L-77L|F7|J||||||FLJL7L----7|L7JFJLJF7L7-JL7F-7|LJ|FJF-7|.|7..|7..|J
LJF---..|7|LJ|7FJ.|FF|JJL----7F-J||LJL-7F-JL-JJJ|7L|JLF-|LF7--JLFJ|LJ|FJL-7|-FJ|LJFJ|||LJF---JF7F--JL7L7L7F7|L-JJF-JL7LJJ-FLFJF-L.L7..L-7FF7
.F||7..F|--7-|-7.FFF|J|JF----JL7-|L--7-|||-L7|7FL7-|L7.FJ-F.F|J-|FJJJ||7FLLJJL7|7L|FJ||7.L--7FJ|L-7J||FJLLJLJJJ-F|F7FJL|JF7|.L|JJL-JF-FJL7FJ
F|L-J|F-J|-|-|||7-FJL7|-L-7F-7FJFJF--J-LJ-7LF-7L77JL.7F|J-|-F|7-LJ7-FLJ-|JFJ.||L7-|||LJF----JL7L7FJJFJL777L|FJ7FLLJ||-FJ|LJF--J7F-FL|L|.F|LJ
L7-.7J|.L|||---||J||FFL.J7LJFJ|-L7L--7F7JLJ7L-J|.||FL---F-J|L7-7J.L-7J|FF.|-7-L7L7LJ7LLL--7F7FJ|LJF-JF7|.||LJLFJ-|-LJ-|.L7F|||LFJF7LJ.7-LJF7
L7-7|F-L7LJ||.L|JFFFJ7J-L77FL7L-7L7F7LJL7FJ-F|--7FL7J|..LJ|L7|F--FLLJ.F||FJJ|7LL-J-L|-F7|-|||L--7.L-7|LJ7LJ7|.|LJLF7FL--JF-.--J.|JJ.FFJF|.LF
FJJ|F7F-7J.JJ-F7JLLJ.J-7F777J|F-J-LJ|F7FJJFFFJ||F-|7-F7.LLF-F|-J-F7-JF-L7LL.LF-LJJJLJJL7J-LJL-7FJ7|L|L7.|JJFF-JJ-LL|F7.|-J|FJJJ.L-F77|F-JL7|
F7.--JJ-L-7LJJJL7L|J-LL7.F---J|F7F|-LJ||J.FF---F-||-7.77-.-FJ|7..JJJ.L7|-|.|-||L7JLLL-LJ||.J-LLJ7L7.L7|7J-7-FJ|..-7LLJF|J-F|.|LL77|.LJJ|-L-7
|77|.LJ.FLLJ|F7J|FF7F7LJ-L---7LJ|-JFLFJL7-JJ|.L|FFJ--7.|L|7.-J-L77L7|J-L7.F7-J7F77-7F7FF|-JJ.|FL7-J7LLJ7JF--J|L--F|...F|7JF---FJ|7-7||LF7J7|
F-J-7J|-||F77F|-|F|LJ|FJ.FLF-JF-J|-FLL--J-|-J7.FFJ|-J7F-7LJ-JJ|.||||.LFLF7JLL|||L--JJL|-7L|JFJ7.F7LJ7|J|||JF|FL7--LJJ7F|J||L|-J-|J.F-L-FL7FJ
L-J.J.||.L7J.7JF|-|-|FF|7FLL--J-L77|-|J|FL7.FL--|-F7FJLL7-77J.F7L|-|7FJ-|L7-7.|L-7.FJJL||.|FFJJ-|J|FJJ--7|-|-7JF7|F7---LJF7.|7.L.|F|JL--J|7.
|--7J-L7--FJ.LL-7LFJLL7--FFLL.L--FJ|--7---L-7L7-LFF|J-LL|.|7L7JL-L-FJLJ.J.L-FLJ7-J77JJ.L--L|--L-J--|-FJL|JJL7J--J-JJJ.L..L--LF|J.F|--F-JJL|-";
            }
            else
            {
                Map = @"...........
.S-------7.
.|F-----7|.
.||.....||.
.||.....||.
.|L-7.F-J|.
.|..|.|..|.
.L--J.L--J.
...........";
                /*                Map = @"FF7FSF7F7F7F7F7F---7
                L|LJ||||||||||||F--J
                FL-7LJLJ||||||LJL-77
                F--JF--7||LJLJ7F7FJ-
                L---JF-JLJ.||-FJLJJ7
                |F|F-JF---7F7-L7L|7|
                |FFJF7L7F-JF7|JL---7
                7-L-JL7||F7|L7F-7F7|
                L.L7LFJ|||||FJL7||LJ
                L7JLJL-JLJLJL--JLJ.L";*/
            }
            nr = Map.Split(delimiter_line, StringSplitOptions.None).Count();
            nc = 11;//nc = 20;
            int[,] Tile = new int[nr, nc];
            Inner = new bool[nr, nc];
            Perimeter = new bool[nr, nc];
            int[] startingPoint = new int[2];
            for (int r = 0; r < nr; r++)
            {
                for (int c = 0; c < nc; c++)
                {
                    int[] newTile = new int[2];
                    newTile[0] = r;
                    newTile[1] = c;
                    Tile[r, c] = Map.Split(delimiter_line, StringSplitOptions.None)[r][c];
                    if (Tile[r, c] == 'S') { startingPoint[0] = r; startingPoint[1] = c; }
                }
            }

            bool FindingLoop = false;
            pipes = new Pipes(startingPoint);
            try
            {

                while (!FindingLoop)
                {
                    Console.WriteLine(pipes.PipePath[0].VisitedPositions.Count());
                    foreach (PipePath path in pipes.PipePath)
                    {
                        ExplorePositions(path);
                    }
                    int ppn = pipes.PipePath.Count();
                    for (int i = 0; i < ppn; i++)
                    {
                        foreach (int[] point in pipes.PipePath[i].NextYou)
                        {
                            if (ContainsPoint(pipes.PipePath[i].NextYou, startingPoint))
                            {
                                var a = "";//7030
                                //FindingLoop = true;
                            }

                            if (!ContainsPoint(pipes.PipePath[i].VisitedPositions, point))
                            {
                                if (pipes.PipePath[i].startingPath == startingPoint)
                                {
                                    PipePath pipePath = new PipePath();
                                    pipePath.startingPath = point;
                                    pipePath.VisitedPositions.Add(startingPoint);
                                    pipePath.VisitedPositions.Add(point);
                                    pipePath.YouAre.Add(point);
                                    pipes.PipePath.Add(pipePath);
                                }
                                else
                                {
                                    pipes.PipePath[i].VisitedPositions.Add(point);
                                    pipes.PipePath[i].YouAre.Add(point);
                                }
                            }
                        }
                        pipes.PipePath[i].NextYou.RemoveAll(x => x != null);
                    }
                    if (pipes.PipePath[0].startingPath.Equals(startingPoint)) pipes.PipePath.RemoveAt(0);

                }

                MakePerimeter(pipes.PipePath[0]);
            }
            catch (Exception ex)
            {

            }
            //ShowPipe();
        }
        public void MakePerimeter(PipePath pipePath)
        {
            foreach (var point in pipePath.VisitedPositions)
            {
                char elem = Map.Split(delimiter_line, StringSplitOptions.None)[point[0]][point[1]];
                if (elem == 'S') { }
                if (elem == '-')
                {
                    for (int i = point[0]; i < nr; i++)
                    {
                        Inner[i, point[1]] = !Inner[i, point[1]];
                    }

                }
                if (elem == '|')
                {
                    for (int i = point[1]; i < nc; i++)
                    {
                        Inner[point[0], i] = !Inner[point[0], i];
                    }

                }
            }
            for (int i = 0; i < nr; i++)
            {
                for (int j = 0; j < nc; j++)
                {
                    string text = (Inner[i, j]) ? "S" : "N";
                    Console.Write(text);
                }
                Console.WriteLine();

            }
        }

    }
}
