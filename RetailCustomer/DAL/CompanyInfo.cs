using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RetailCustomer;
namespace RetailCustomer.DAL
{
    public class CompanyInfo
    {
        tobaccocastleEntities db = new tobaccocastleEntities ();

        public companyinfo GetCompanyInfo(int companyId)
        {
            return db.companyinfoes.Find(companyId);       
        }
        public string getCompanyName()
        {            
            return db.companyinfoes.Find(1).compName;
        }
        public string getCompanyWebsite()
        {
            return db.companyinfoes.Find(1).CompWebsite;
        }
        public string getCompanyPrintAddress()
        {
            return db.companyinfoes.Find(1).CompPrintAddress;
        }
        public string getCompanyAddress()
        {
            return db.companyinfoes.Find(1).CompLongAddress;
        }
        public string getCompanyContactNo()
        {
            return db.companyinfoes.Find(1).CompContactNo;
        }
        public string getCompanyEmail()
        {
            return db.companyinfoes.Find(1).CompEmailId;
        }
        public string getFootNoteOne()
        {
            return db.companyinfoes.Find(1).CompFootNote1;
        }
        public string getFootNoteTwo()
        {
            return db.companyinfoes.Find(1).CompFootNote2;
        }




        public string getCompanyName(int id)
        {
            return db.companyinfoes.Find(id).compName;
        }
        public string getCompanyWebsite(int id)
        {
            return db.companyinfoes.Find(id).CompWebsite;
        }
        public string getCompanyPrintAddress(int id)
        {
            return db.companyinfoes.Find(id).CompPrintAddress;
        }
        public string getCompanyAddress(int id)
        {
            return db.companyinfoes.Find(id).CompLongAddress;
        }
        public string getCompanyContactNo(int id)
        {
            return db.companyinfoes.Find(id).CompContactNo;
        }
        public string getCompanyEmail(int id)
        {
            return db.companyinfoes.Find(id).CompEmailId;
        }
        public string getFootNoteOne(int id)
        {
            return db.companyinfoes.Find(id).CompFootNote1;
        }
        public string getFootNoteTwo(int id)
        {
            return db.companyinfoes.Find(id).CompFootNote2;
        }
    }
}