using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class Rate2
    {
public int ClientCompanyId {get;set;}
public string Link            {get;set;}
public string Code            {get;set;}
public string BatchNo         {get;set;}
public decimal SL_Rate         {get;set;}
public string Remarks         {get;set;}
public decimal IT_Disc         {get;set;}
public decimal IT_TAX          {get;set;}
public Guid RefId           {get;set;}
public string OperationFlag { get; set; }

    }
}
