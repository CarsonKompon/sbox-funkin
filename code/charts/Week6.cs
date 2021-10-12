using System;
using System.Collections.Generic;
using Sandbox;

[ChartBase]
public class ChartRoses : ChartBase
{
    public ChartRoses(){
        name = "Roses";

        songInst = "roses_inst";
        songVoices = "roses_voices";

        jsonFile = "/charts/week6/roses/roses-hard.json";
    }
}

[ChartBase]
public class ChartTest : ChartBase
{
    public ChartTest(){
        name = "Funny Chart";

        songInst = "roses_inst";
        songVoices = "roses_voices";

        jsonFile = "/charts/week6/roses/roses-hard.json";
    }
}