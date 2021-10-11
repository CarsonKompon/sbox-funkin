using System.Collections.Generic;
using System;
using Sandbox;

public class ChartBase : LibraryAttribute
{
    public virtual string name {get;set;} = "Unnamed Song";
    
    public virtual string songInst {get;set;} = "roses_inst";
    public virtual string songVoices {get;set;} = "roses_voices";

    public virtual string jsonFile {get;set;} = null;
    public virtual ChartFile Chart {get;set;}
}