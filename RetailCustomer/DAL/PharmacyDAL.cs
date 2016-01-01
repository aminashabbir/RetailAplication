using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

using RetailCustomer.DAL;
using System.Web.Helpers;
using System.Web.Mvc;

namespace RetailCustomer.DAL
{
    public class PharmacyDAL
    {
        private tobaccocastleEntities db = new tobaccocastleEntities();
     

        public PharmacyDAL()
        {        }
   
        #region Data Entry Part 
              
        #region Patients
        public string AddPatient(patient patient)
        {
            try
            {
                db.patients.Add(patient);
                db.SaveChanges();
                return "customer Added";
            }
            catch {

                return "Can not Add, Please check your values,try again later.";
            }
          
        }
        public List<patient> GetPatients()
        {
            return db.patients.ToList();        
        }
        public patient GetPatient(int? id)
        {
            return db.patients.Find(id);
        }
        public string EditPatient(patient patient)
        {
            try {
                patient p = db.patients.Find(patient.patientsId);
                p.patientFatherName = patient.patientFatherName;
                p.patientName = patient.patientName;
                p.contactNumber = patient.contactNumber;
                p.address = patient.address;
                p.dateOfBirth = patient.dateOfBirth;
                p.emailId = patient.emailId;
                db.SaveChanges();
               return "Edited";
            }
            catch{
                return "Error, Can not Edit.";
            }
           
        }
        #endregion

        #region Products
        public string GetCategoryUnit(int categoryId)
        {
            return db.categories.Find(categoryId).categoryUnit;
        }
        public SelectList GetCategory()
        {
            var categories = db.categories;
            return new SelectList(categories, "categoryId", "categoryName");
        }
        public List<category> Categories()
        {
            return db.categories.ToList();
        }
        public SelectList GetCategory(int CatId)
        {
            var categories = db.categories;
            return new SelectList(categories, "categoryId", "categoryName",CatId);
        }
        public bool AddCategory(string CategoryName, String Unit)
        {           
            try
            {
                category checkold;
                checkold = db.categories.Where(z => z.categoryName == CategoryName && z.categoryUnit == Unit).FirstOrDefault();
                if (checkold == null)
                {
                    category cat = new category();
                    cat.categoryName = CategoryName;
                    cat.categoryUnit = Unit;
                    db.categories.Add(cat);
                    db.SaveChanges();
                    return true;
                }
                else {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public SelectList GetProduct(int CategoryId)
        {
            var Products = db.products.Where(p => p.categoryId == CategoryId);
            return new SelectList(Products, "productId", "productName");
        }
        public bool AddProduct(string productName, int CategoryId)
        {
            try
            {
                product checkold;
                checkold = db.products.Where(x => x.productName == productName && x.categoryId == CategoryId).FirstOrDefault();
                if (checkold == null)
                {
                    product pro = new product();
                    pro.productName = productName;
                    pro.categoryId = CategoryId;
                    db.products.Add(pro);
                    db.SaveChanges();
                    return true;
                }
                else {
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }
        public SelectList GetSuppliers()
        {
            var suppliers = db.suppliers.Where(s => s.type != "Branch");
            return new SelectList(suppliers, "supplierId", "supplierName");
        }
        public SelectList GetSuppliers(int ProductId)
        {
            var suppliers = db.productsupplieds.Where(s => s.productdetail.product.productId == ProductId).Select(g => new { id=g.supplierId, name = g.supplier.supplierName });
            
            return new SelectList(suppliers, "id", "name");
        }
        public bool Addsupplier(string address, string munf, string contNo, string email, string name, string type)
        {
            try {
                supplier supplier = new supplier();
                supplier.address = address;
                supplier.manufecture = munf;
                supplier.supplierContactNumber = contNo;
                supplier.supplierEmail = email;
                supplier.supplierName = name;
                supplier.type = type;
                db.suppliers.Add(supplier);
                db.SaveChanges();
                return true;
            }
            catch {
                return false;
            }
            
        }

        public bool AddNewProductDetails(int ProductId, double size, int supplierID, int thresholdLevel, int expiryDays, string barcode)
        {
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    productdetail checkold;
                    checkold = db.productdetails.Where(o => o.productId == ProductId && o.productSize == size).FirstOrDefault();
                    if (checkold == null)
                    {
                        productdetail pd = new productdetail();
                        pd.alertBFExpiryDays = expiryDays;
                        pd.productId = ProductId;
                        pd.productSize = size;
                        pd.thresholdLevel = thresholdLevel;
                        if (barcode != "")
                        {
                            pd.barcode = barcode;
                        }
                        var proDetil = db.productdetails.Add(pd);
                        db.SaveChanges();
                        productsupplied PS = new productsupplied();
                        PS.supplierId = supplierID;
                        PS.productDetailId = proDetil.productDetailId;
                        db.productsupplieds.Add(PS);
                        db.SaveChanges();
                        dbTransaction.Commit();
                        return true;
                    }
                    else { return false; }
                }
                catch (Exception)
                {
                    dbTransaction.Rollback();
                    return false;
                }
            }
        }
        public List<supplier> GetAllSuppliers()
        {
            return db.suppliers.Where(s => s.type != "Branch").OrderBy(c => c.supplierName).ToList();
        }
        public supplier GetASupplier(int? SupplierId)
        {
            return db.suppliers.Find(SupplierId);
        }
        public string EditSupplier(supplier  Supplier)
        {
            try
            {
                supplier p = db.suppliers.Find(Supplier.supplierId);
                p.address = Supplier.address;
                p.manufecture = Supplier.manufecture;
                p.supplierContactNumber = Supplier.supplierContactNumber;
                p.supplierEmail = Supplier.supplierEmail;
                p.supplierName = Supplier.supplierName;
                db.SaveChanges();
                return "Edited";
            }
            catch
            {
                return "Error, Can not Edit.";
            }

        }
        public List<productdetail> GetProducts()
        {
           // return db.productdetails.Include(p => p.product).Include(p => p.product.category).ToList();
            return db.productdetails.OrderBy(p=>p.product.productName).ToList();
        }
        public productdetail GetAProduct(int? productDetailId)
        {
            return db.productdetails.Find(productDetailId);
        }
        public string EditProductDetailed(productdetail ProdDet, int[] suppliers)
        {
            if (ProdDet.product.productName == null || ProdDet.productSize == null)
            {
                return "Product  name or size missing";
            }
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    productdetail prod = db.productdetails.Find(ProdDet.productDetailId);
                    if (prod.product.categoryId == ProdDet.product.categoryId)
                    {
                        prod.productSize = ProdDet.productSize;
                        prod.product.productName = ProdDet.product.productName;                       
                        prod.thresholdLevel = ProdDet.thresholdLevel;
                        prod.alertBFExpiryDays = ProdDet.alertBFExpiryDays;
                        if ( ProdDet.barcode!=null)
                        {
                            prod.barcode = ProdDet.barcode.Trim();
                        }
                        db.SaveChanges();
                        var prosupliz = db.productsupplieds.Where(i => i.productDetailId == ProdDet.productDetailId);
                        #region Supplier part in case category not changed
                        if (suppliers != null) {
                            foreach (int sid in suppliers)
                            {
                                if (prosupliz != null)
                                {
                                    if (prosupliz.Where(w => w.supplierId == sid).FirstOrDefault() == null)
                                    {
                                        productsupplied newps = new productsupplied();
                                        newps.supplierId = sid;
                                        newps.productDetailId = ProdDet.productDetailId;
                                        db.productsupplieds.Add(newps);
                                        db.SaveChanges();
                                    }
                                }
                                else
                                {
                                    productsupplied newps = new productsupplied();
                                    newps.supplierId = sid;
                                    newps.productDetailId = ProdDet.productDetailId;
                                    db.productsupplieds.Add(newps);
                                    db.SaveChanges();                                
                                }                                
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        /////////check for already exist??????????????????????
                        product  ProExist = db.products.Where(k => k.productName == ProdDet.product.productName && k.categoryId == ProdDet.product.categoryId).FirstOrDefault();
                        if (ProExist != null)
                        {
                            prod.productSize = ProdDet.productSize;
                            prod.thresholdLevel = ProdDet.thresholdLevel;
                            prod.alertBFExpiryDays = ProdDet.alertBFExpiryDays;
                            if (ProdDet.barcode != null)
                            {
                                prod.barcode = ProdDet.barcode.Trim();
                            }
                            prod.productId = ProExist.productId;
                            db.SaveChanges();
                        }
                        else {
                            product p = new product();
                            p.categoryId = ProdDet.product.categoryId;
                            p.productName = ProdDet.product.productName;
                            var newpro = db.products.Add(p);
                            db.SaveChanges();
                            prod.productSize = ProdDet.productSize;
                            prod.thresholdLevel = ProdDet.thresholdLevel;
                            prod.alertBFExpiryDays = ProdDet.alertBFExpiryDays;
                            if (ProdDet.barcode != null)
                            {
                                prod.barcode = ProdDet.barcode.Trim();
                            }
                            prod.productId = newpro.productId;
                            db.SaveChanges();
                            var prosupliz = db.productsupplieds.Where(i => i.productDetailId == ProdDet.productDetailId);
                            #region Supplier part in case category not changed
                            if (suppliers != null)
                            {
                                foreach (int sid in suppliers)
                                {
                                    if (prosupliz != null)
                                    {
                                        if (prosupliz.Where(w => w.supplierId == sid).FirstOrDefault() == null)
                                        {
                                            productsupplied newps = new productsupplied();
                                            newps.supplierId = sid;
                                            newps.productDetailId = ProdDet.productDetailId;
                                            db.productsupplieds.Add(newps);
                                            db.SaveChanges();
                                        }
                                    }
                                    else
                                    {
                                        productsupplied newps = new productsupplied();
                                        newps.supplierId = sid;
                                        newps.productDetailId = ProdDet.productDetailId;
                                        db.productsupplieds.Add(newps);
                                        db.SaveChanges();
                                    }
                                }
                            }
                            #endregion                 
                        }                       
                    }
                    dbTransaction.Commit();
                    return "ok";
                }
                catch (Exception)
                {
                    dbTransaction.Rollback();
                    return "Error! Barcode must be unique";
                }
            }
        }
        public string AddsupplierProductRelation(int productId, List<int> supplierIds)
        {
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    if (supplierIds != null)
                    {
                        foreach (int i in supplierIds)
                        {
                            if (db.productsupplieds.Where(p => p.productDetailId == productId && p.supplierId == i).FirstOrDefault() == null)
                            {
                                productsupplied ps = new productsupplied();
                                ps.productDetailId = productId;
                                ps.supplierId = i;
                                db.productsupplieds.Add(ps);
                                db.SaveChanges();
                            }                            
                        }
                    }                   
                    dbTransaction.Commit();
                    return "ok";
                }
                catch (Exception)
                {
                    dbTransaction.Rollback();
                    return "Error";
                }
            }
        }
        public bool EditCategory(int categoryId, string categoryUnit)
        {
            try
            {
                category cat = db.categories.Find(categoryId);
                cat.categoryUnit = categoryUnit;
                db.SaveChanges();
                return true;
            }

            catch {
                return false;
            }
        }
        #endregion

        #region Employee
        public SelectList GetAuthorties()
        {
            return new SelectList(db.authorities, "authorityId", "authority1");
        }
        public string AddNewUser(string UserName, string Password, short AuthortyID)
        {
            try
            {
                user user = new user();
                user.password = ConvertPassword(Password);
                user.userName = UserName;
                user.authorityId = AuthortyID;
                db.users.Add(user);
                db.SaveChanges();
                return "New user added";
            }
            catch {
                return "Can not add new user,double check User Name, it must be unique.";
            }            
        }
        public string ConvertPassword(string password) // this function returns string-hash of a string
        {
            var hashed = Crypto.Hash(password, "MD5");
            return hashed;
        }
        public SelectList GetUserNames()
        {
            return new SelectList(db.users.Where(u => u.enabled == false), "userName", "userName");
        }
        public SelectList GetManagers()
        {
           return new SelectList(db.employees.Where(u => u.user.enabled == true), "empId", "User.userName");
        }
        public SelectList GetDesignations()
        {           
            return new SelectList(db.designations, "designationId", "designation1");
        }
        public SelectList GetDepartments()
        {
            return new SelectList(db.departments.Where(d=>d.deleted==false), "departmentId", "name");
        }
        public string AddEmployee(string username, int DesigId, int DeptId, int managId, bool status, string gender, string remarks,
                                  DateTime joining, string address, string mobile, DateTime DOB, string lName, string fName,bool deleted)
        {
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    employee emp = new employee();
                    emp.address = address;
                    emp.birthDate = DOB;
                    emp.deleted = deleted;
                    emp.departmentId = DeptId;
                    emp.designationId = DesigId;
                    emp.firstName = fName;
                    emp.gender = gender;
                    emp.joiningDate = joining;
                    emp.lastName = lName;
                    emp.managerId = managId;
                    emp.mobile = mobile;
                    emp.remarks = remarks;
                    emp.userName = username;
                    db.employees.Add(emp);
                    db.SaveChanges();

                    user user = db.users.Find(username);
                    user.enabled = status;
                    db.SaveChanges();

                    dbTransaction.Commit();
                    return "ok";
                }
                catch
                {
                    dbTransaction.Rollback();
                    return "There are some errors, please try again.";
                }
            }
        }
        
        #endregion

        #endregion

        #region Order-Part
        /// ////////////////////////Editing  After First Meeting ////////////////////////////
        #region Edited  Order  
        public bool AddNewOrder(DateTime date, string ordernumber,int EmpId,int supplierId)
        {
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    order neworder = new order();
                    neworder.orderStatusId = 1;
                    neworder.orderNumber = ordernumber;
                    neworder.orderDate = date;
                    neworder.empId = EmpId;
                    neworder.supplierId = supplierId;
                    db.orders.Add(neworder);
                    db.SaveChanges();
                    dbTransaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    dbTransaction.Rollback();
                    return false;
                }
            }

        }
        public List<order> GetAllOrders()
        {
            List<order> list = db.orders.Where(o => o.orderStatusId < 4 && o.supplier.type != "Branch").OrderBy(d => d.orderStatusId).OrderByDescending(d => d.orderDate).ToList();
            return list;
        }
        public SelectList GetSupplier()
        {
            return new SelectList(db.suppliers.Where(s => s.type != "Branch"), "supplierId", "supplierName");
        }
        public string GetLastOrderNumber()
        {
            var neworder = db.orders.OrderByDescending(o=>o.orderId).FirstOrDefault();
            if (neworder != null)
            {
                return neworder.orderNumber;
            }
            else {
                return "";
            }
        }
        public List<OrderPlaceTable> GetOrderDetails(int? orderID)
        {
            List<OrderPlaceTable> list = new List<OrderPlaceTable>();
            OrderPlaceTable item = new OrderPlaceTable();
            var orderPlacedItems = db.orderplaceditems.Where(o => o.orderId == orderID);
            foreach (var OP in orderPlacedItems)
            {
                item = new OrderPlaceTable();
                item.IndexNumber = list.Count + 1;
                item.OrderPlaceId = OP.orderPlacedItemsId;
                item.ProductName = GetProductNameForSoldItem(OP.productsupplied.productDetailId);
                item.QuantityOrderd = OP.quantityOrderd;
                item.PackSize = OP.packSize;
                item.RatePrUnit = OP.ratePrUnit;
                list.Add(item);            
            }
            return list;
        }
        public SelectList SearchProductForOrderPlacement(int supplierId, string searchKey)
        {
            var ProdSupp = from l in db.productsupplieds.Where(p => p.supplierId == supplierId && p.productdetail.product.productName.Contains("" + searchKey))
                           select new { l.productSuppliedId, l.productdetail.product.productName, l.productdetail.productSize, l.productdetail.product.category.categoryName, l.productdetail.product.category.categoryUnit };
            var products = ProdSupp.AsEnumerable().Select(g => new { Id = g.productSuppliedId, DetailProductName = g.productName + " , " + g.productSize.ToString() + " " + g.categoryUnit + " , " + g.categoryName }).ToList();
            return new SelectList(products, "Id", "DetailProductName");
        }
        public order getOrderByOrderId(int? orderID)
        {
            order order = db.orders.Find(orderID);
            return order;
        }
        public int GetProductDetailIdAgainstProductSupliedId(int ProductSuppliedId)
        {
            productsupplied prodSupplied = db.productsupplieds.Find(ProductSuppliedId);
            return prodSupplied.productDetailId;           
        }
        public double GetLastPricePrItemOfASpecificProduct(int ProductDetailId)
        {
            var stockRow = db.stocks.Where(s => s.productsupplied.productDetailId == ProductDetailId).OrderByDescending(s => s.expiryDate).FirstOrDefault();
            if (stockRow != null)
            {
                return  (double)stockRow.PricePrItem;
            }
            return 0.0;
        }
        public string AddOrderDetails(int ProductSuppliedId, int Quantity, int orderID, int packSize, double prUnitRate)
        {
            try
            {
                orderplaceditem OPI = db.orderplaceditems.Where(o => o.orderId == orderID && o.productSuppliedId == ProductSuppliedId).FirstOrDefault();
                if (OPI == null)
                {
                    orderplaceditem orderItem = new orderplaceditem();
                    orderItem.productSuppliedId = ProductSuppliedId;
                    orderItem.orderId = orderID;
                    orderItem.packSize = packSize;
                    orderItem.quantityOrderd = Quantity;
                    orderItem.ratePrUnit = prUnitRate;
                    db.orderplaceditems.Add(orderItem);
                    db.SaveChanges();
                    return "ok";
                }
                else {
                    OPI.quantityOrderd += Quantity;
                    db.Entry(OPI).State = EntityState.Modified;
                    db.SaveChanges();
                    return "ok";             
                }
            }
            catch {
                return "not ok";
            }            
        }
        public void DeletItemFromOrder(int OrderPlacedId)
        {
            orderplaceditem Odr = db.orderplaceditems.Find(OrderPlacedId);
            if (Odr.order.orderStatusId == 1)
            {
                orderplaceditem OD = db.orderplaceditems.Find(OrderPlacedId);
                db.orderplaceditems.Remove(OD);
                db.SaveChanges();
            }     
          
        }
        public bool ChangeOrderStatus(int Oid, string disc,int statusid)
        {
            try
            {
                order ordertoUpdate = db.orders.Find(Oid);
                ordertoUpdate.statusChangedDiscription =  disc;
                ordertoUpdate.orderStatusId = statusid;
                db.SaveChanges();
                return true;
            }
            catch {
                return false;
            }
        }
        public void ChangeOrderStatus(int Oid, string whattodo)
        {
            order ordertoUpdate = db.orders.Find(Oid);
            if (whattodo == "place")
            {
                int newStatusId = db.orderstatus.Where(o => o.statusName == "Placed").FirstOrDefault().orderStatusId;
                ordertoUpdate.orderStatusId = newStatusId;
                db.SaveChanges();
            }
            else if (whattodo == "drop")
            {
                int newStatusId = db.orderstatus.Where(o => o.statusName == "Droped").FirstOrDefault().orderStatusId;
                ordertoUpdate.orderStatusId = newStatusId;
                db.SaveChanges();
            }
            else if (whattodo=="Partial")
            {
                int newStatusId = db.orderstatus.Where(o => o.statusName == "Partial Recived").FirstOrDefault().orderStatusId;
                ordertoUpdate.orderStatusId = newStatusId;
                db.SaveChanges();
            
            }
        }
     // not yet used/////////////
        public int?  AddOrderPartialAndUpdateOrderStatus(int Oid, int newStatusId, string discription, int empID,DateTime recivingDate)
        {
             using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    order ordertoUpdate = db.orders.Find(Oid);
                    ordertoUpdate.orderStatusId = newStatusId;                    
                    db.SaveChanges();
                    orderpartial orderpartialtoUpdate = new orderpartial();
                    orderpartialtoUpdate.discription = discription;
                    orderpartialtoUpdate.empId=empID;
                    orderpartialtoUpdate.orderId = Oid;
                    orderpartialtoUpdate.recevingDate = recivingDate;
                   var returnvalue= db.orderpartials.Add(orderpartialtoUpdate);
                    db.SaveChanges();
                    dbTransaction.Commit();
                    return returnvalue.orderPartialsId;
                }
                catch (Exception)
                {
                    dbTransaction.Rollback();
                    return null;                   
                }
            }                        
        }
        #endregion

        #region Editing Order Receving 
        public List<OrderReciveStructure> OrderReciveDisplaySingle(int? partialOrderID)
        {           
            List<OrderReciveStructure> list = new List<OrderReciveStructure>();
            OrderReciveStructure ordertable;
            var PartailOrderItems = db.stocks.Where(s => s.orderPartialsId == partialOrderID);
            foreach (var item in PartailOrderItems)
            {
                ordertable = new OrderReciveStructure();
                ordertable.BatchNO = item.batchNO;
                ordertable.ProductName1 = item.productsupplied.productdetail.product.productName+" , "+item.productsupplied.productdetail.productSize+" "+item.productsupplied.productdetail.product.category.categoryUnit;
                ordertable.QuantityRecived = item.quantityReceived;
                ordertable.QuantityAcepted = item.quantity;
                ordertable.PricePrItem = item.PricePrItem;
                ordertable.DiscountPercentage = item.discountPercentage;
                ordertable.StockId = item.stockId;
                list.Add(ordertable);         
            }           
            return list;
        }
        public int? creatNewPartialOrder(int? OrderID,int? empId,DateTime date)
        {
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    orderpartial partial = new orderpartial();
                    partial.orderId = (int)OrderID;
                    partial.empId = (int)empId;
                    partial.recevingDate = date;
                    var returnvalue = db.orderpartials.Add(partial);
                    db.SaveChanges();
                    ChangeOrderStatus((int)OrderID, "Partial");
                    dbTransaction.Commit();
                    return returnvalue.orderPartialsId;
                }
                catch
                {
                    dbTransaction.Rollback();
                    return null;
                }
            }
        }
        public DateTime? GetDateOfPartial(int partialID)
        {
           return db.orderpartials.Find(partialID).recevingDate;     
        }
        public string addStockItem(int prodductSuppliedId, int quantityRecive, int quantityAccepted, double PricePrItem, double PriceSelling,
                                           string batchNo, int packSize, DateTime ExpiryDate, double Discount, int orderPartialId)
        {
            try
            {
                stock stok = new stock();
                stok.itemSold = 0;
                stok.batchNO = batchNo;
                stok.discountPercentage = Discount;
                stok.expiryDate = ExpiryDate;
                stok.orderPartialsId = orderPartialId;
                stok.packSize = packSize;
                stok.PricePrItem = PricePrItem;
                stok.productSuppliedId = prodductSuppliedId;
                stok.quantity = quantityAccepted;
                stok.quantityReceived = quantityRecive;
                stok.sellingPricePrItem = PriceSelling;
                db.stocks.Add(stok);
                db.SaveChanges();
                return "ok";
            }
            catch
            {

                return "Error";
            }
        }
      public string  GetBarCodeOfProdSuppID(int proSupID)
        {
            int id = db.productsupplieds.Find(proSupID).productDetailId;
            return db.productdetails.Find(id).barcode;
        }
        public SelectList GetOrderdItems(int orderId)
        {
            List<ProductListWithSizeAndCategory> newlist = new List<ProductListWithSizeAndCategory>();
            ProductListWithSizeAndCategory product = new ProductListWithSizeAndCategory();
            var items = db.orderplaceditems.Where(o => o.orderId == orderId);
            foreach (var item in items)
            {
                product = new ProductListWithSizeAndCategory();
                product.DetailProductName = GetProductNameForSoldItem(item.productsupplied.productDetailId);
                product.Id = item.productSuppliedId;
                newlist.Add(product);
            }
            SelectList list = new SelectList(newlist, "Id", "DetailProductName");
            return list;
        }
        public string updateOrderPartialDiscriptionOrDate(string discription, DateTime date, int orderPartialId)
        {
            try
            {
                orderpartial orderPartial = db.orderpartials.Find(orderPartialId);
                orderPartial.recevingDate = date;
                if (discription.Trim().Length > 0)
                {
                    orderPartial.discription = discription;
                }
                db.SaveChanges();
                return "ok";
            }
            catch {
                return "not ok";
            }          
        }

        public List<List<OrderReciveStructure>> OrderReciveDisplay(int? orderID)
        {
            List<List<OrderReciveStructure>> Mainlist = new List<List<OrderReciveStructure>>();
            List<OrderReciveStructure> innerlist = new List<OrderReciveStructure>();
            OrderReciveStructure ordertable;//= new OrderReciveStructure();
            int partialId = 0;
            var stocks = db.stocks.Where(o => o.orderpartial.orderId == orderID);
            foreach (var stock in stocks)
            {
                if (partialId == 0)
                {
                    partialId = stock.orderPartialsId;
                }
                if (partialId != stock.orderPartialsId) //// condition check for if next partial
                {
                    partialId = stock.orderPartialsId;
                    Mainlist.Add(innerlist);
                    innerlist = new List<OrderReciveStructure>();
                }
                ordertable = new OrderReciveStructure();
                ordertable.ProductName1 = stock.productsupplied.productdetail.product.productName + stock.productsupplied.productdetail.productSize.ToString() + stock.productsupplied.productdetail.product.category.categoryUnit;
                ordertable.QuantityRecived = stock.quantityReceived;
                ordertable.OrderRecivingDateD = stock.orderpartial.recevingDate;
                ordertable.BatchNO = stock.batchNO;
                ordertable.StockId = stock.stockId;
                ordertable.PricePrItem = stock.PricePrItem;
                innerlist.Add(ordertable);
            }
            Mainlist.Add(innerlist); //add last list
            return Mainlist;
        }
      
        #endregion 
        
        #region Order Reporting
        public order GetOrderIncudedSupplierForPrint(int orderID)
        {
            order ordr =db.orders.Include(s=>s.supplier).Where(o=>o.orderId==orderID).FirstOrDefault();           
            return ordr;
        }

        public List<order> GetOrderOfADay(DateTime date)
        {
            return db.orders.Where(o => o.orderDate == date && o.orderstatu.statusName != "Draft").ToList();        
        }
        public List<order> GetOrderOfTwoDates(DateTime? FromDate,DateTime? ToDate)
        {
            return db.orders.Where(o => o.orderDate >= FromDate && o.orderDate <= ToDate && o.orderstatu.statusName != "Draft").ToList();
        }
        public string GetSupplierName(int? SupplierId)
        {
            return db.suppliers.Where(s => s.supplierId == SupplierId).FirstOrDefault().supplierName;
        }
        public List<order> GetOrderOfADay(DateTime date,int? supplierId)
        {
            return db.orders.Where(o => o.orderDate == date && o.orderstatu.statusName != "Draft"&& o.supplierId==supplierId).Include(j=>j.orderplaceditems).ToList();
        }
        public List<order> GetOrderOfTwoDates(DateTime? FromDate, DateTime? ToDate, int supplierId)
        {
            return db.orders.Where(o => o.orderDate >= FromDate && o.orderDate <= ToDate && o.orderstatu.statusName != "Draft"&& o.supplierId==supplierId).ToList();
        }
       
        #endregion
        /// /////////////////////////Eployee part//////////
        public int GetDesignationIdOfAnEmployee(int employeeID) {
            var empDetails = db.employees.Where(e => e.empId == employeeID).FirstOrDefault();
            return empDetails.designationId;
        }
        
        #endregion
              
        #region Department
        public department FindDepartment(int? id)
        {
            return db.departments.Find(id);
        }

        public void AddNewDeprtment(department dep)
        {
            db.departments.Add(dep);
            db.SaveChanges();
        }

        public void EditDepartment(department dep)
        {
            //db.Entry(dep).State = EntityState.Modified;
            //db.SaveChanges();
        }

        #endregion

        #region Designation

        #endregion

        #region Login
        public string GetPriceingMethod()
        {
            return db.configdetails.Where(c => c.admincongifsetting.configName == "PricingStrategy").FirstOrDefault().configValue;
        }
       public int  GetExpiryDays()
        {

           //return from db
            return 2;
        }
        public string Login(string username, string Password)
        {
            string s = "invalid";
            
            var hashedpassword = Crypto.Hash(Password, "MD5");
            user User = db.users.Where(u => u.userName == username && u.password == hashedpassword).FirstOrDefault();
            if (User != null && User.enabled == true)
            {
                var GuestId = db.configdetails.Where(c => c.admincongifsetting.configName == "GuestId").FirstOrDefault().configValue;
                string user_name = User.userName;
                var user_info = User.employees.Where(us => us.userName == User.userName).FirstOrDefault();
                int Emp_ID = user_info.empId;
                string First_name = user_info.firstName;
                string role = User.authority.authority1;
                string authortyId = User.authorityId.ToString();
                s = user_name + "," + First_name + "," + Emp_ID.ToString() + "," + role + "," + GuestId + "," + authortyId;    
            }
            

            return s;
        }
        public void SignOut()
        {

        }
        public List<Roles> GetRoles(int authortyId)
        {
            List<Roles> list = new List<Roles>();
            var ForRoles = db.roleandauthorities.Where(r => r.authorityId == authortyId).Include(r => r.role);
            string parent = "";
            Roles role= new Roles();
            RoleReturnType returner=new RoleReturnType();
            foreach (var r in ForRoles)
            {
                returner = new RoleReturnType();
                if (parent == "")
                {
                    parent = r.role.role1.displayText;
                    role.Role = parent;
                    role.RoleCount = list.Count + 1;
                }
                if (parent != r.role.role1.displayText)
                {////new role is here                
                    list.Add(role);
                    role = new Roles();
                    parent = r.role.role1.displayText;
                    role.Role = parent;
                    role.RoleCount = list.Count + 1;
                }
                returner.ActionName=r.role.actionName;
                returner.ControlerName=r.role.controlerName;
                returner.DisplayText=r.role.displayText;                
                role.RoleList.Add(returner);             
            }
            list.Add(role);
            return list;        
        }
        public employee  GetEmployeeDetails(int EmpId)
       {
           employee employee = db.employees.Where(e => e.empId == EmpId).Include(e => e.designation).FirstOrDefault();
           return employee;
       }
        public int? ReturnDiscountPr()
        {
            var discount = db.configdetails.Where(c => c.admincongifsetting.configName == "OverAllDiscount").FirstOrDefault().configValue;
            return int.Parse(discount);
        }
        public string UpdatePassword(string oldpass, string newpass,string username)
        {
            try
            {
                var hashedpassword = Crypto.Hash(oldpass, "MD5");
                user u = db.users.Find(username);
                if (u != null)
                {
                    if (u.password == hashedpassword)
                    {
                        var newhashedpassword = Crypto.Hash(newpass, "MD5");
                        u.password = newhashedpassword;
                        db.SaveChanges();
                        return "Successfully changed!";
                    }
                    else
                    {
                        return "Old Password Not Matched";
                    }
                }
                return "Error, user not found";
            }
            catch {
                return "Error, try again!";
            }
        }
        public string GetCutOffTime()
        {
            return db.configdetails.Where(d => d.admincongifsetting.configName == "cutoffTime").FirstOrDefault().configValue;
        }
        #endregion

        #region Alerts and messages
        public List<IdAndSum> ShortageAlerts()
        {
            List<IdAndSum> shortageItems = db.Database.SqlQuery<IdAndSum>("select  tmp.Sum, tmp.Name,tmp.Size,tmp.Unit , tmp.Category  "+
                                                                         " FROM  productdetails, (SELECT SUM(S.quantity - S.itemSold) AS Sum, Pde.productSize AS Size, Pro.productName AS Name, Cat.categoryName AS Category, Cat.categoryUnit AS Unit, Pde.productDetailId as ProDetId " +
		                                                                 " FROM  stock S INNER JOIN "+
                                                                         " productsupplied Psup ON S.productSuppliedId = Psup.productSuppliedId INNER JOIN "+
                                                                         " productdetails Pde ON Psup.productDetailId = Pde.productDetailId INNER JOIN "+
                                                                         " product Pro ON Pde.productId = Pro.productId INNER JOIN category Cat ON Pro.categoryId = Cat.categoryId "+
                                                                         " WHERE S.quantity >= S.itemsold and DATEDIFF(S.expirydate,CURDATE())>= Pde.alertBFExpiryDays group by Pde.productDetailId order by Pro.productName ) as tmp " +
                                                                         " where tmp.Sum <= productdetails.thresholdLevel AND productDetailId=tmp.ProDetId  ;").ToList();

            return shortageItems;
        }
        public List<ForExpiryAlerts> ExpiryAlertsFiveDaysM()
        {
            List<ForExpiryAlerts> ExpiryAlerts = db.Database.SqlQuery<ForExpiryAlerts>("SELECT sum(S.quantity-S.itemsold) as Sum  ,  Pde.productSize as Size , Pro.productName as Name," +
                                                                        "Cat.categoryName as Category  ,  Cat.categoryUnit as Unit , S.expiryDate as Expiry " +
                                                                        "FROM  stock S  JOIN productsupplied Psup ON S.productSuppliedId = Psup.productSuppliedId " +
                                                                        "JOIN productdetails Pde ON Psup.productDetailId = Pde.productDetailId " +
                                                                        "JOIN product Pro ON Pde.productId = Pro.productId  JOIN category Cat  ON Pro.categoryId = Cat.categoryId " +
                                                                        " WHERE S.quantity > S.itemsold and DATEDIFF(S.expirydate,CURDATE()) < Pde.alertBFExpiryDays and DATEDIFF(S.expirydate,CURDATE()) > (Pde.alertBFExpiryDays  - 5) group by Pde.productDetailId  order by Pro.productName;").ToList();
           
            return ExpiryAlerts;
        }
        public List<ForExpiryAlerts> ExpiryAlerts()
        {
            List<ForExpiryAlerts> ExpiryAlerts = db.Database.SqlQuery<ForExpiryAlerts>("SELECT sum(S.quantity-S.itemsold) as Sum  ,  Pde.productSize as Size , Pro.productName as Name," +
                                                                       "Cat.categoryName as Category  ,  Cat.categoryUnit as Unit , S.expiryDate as Expiry " +
                                                                        "FROM  stock S  JOIN productsupplied Psup ON S.productSuppliedId = Psup.productSuppliedId " +
                                                                        "JOIN productdetails Pde ON Psup.productDetailId = Pde.productDetailId " +
                                                                        "JOIN product Pro ON Pde.productId = Pro.productId  JOIN category Cat  ON Pro.categoryId = Cat.categoryId " +
                                                                        " WHERE S.quantity > S.itemsold and DATEDIFF(S.expirydate,CURDATE()) = Pde.alertBFExpiryDays group by Pde.productDetailId  order by Pro.productName;").ToList();
            return ExpiryAlerts;
        }
        public List<ForExpiryAlerts> ExpiryAlertsFiveDaysP()
        {
            List<ForExpiryAlerts> ExpiryAlerts = db.Database.SqlQuery<ForExpiryAlerts>("SELECT sum(S.quantity-S.itemsold) as Sum  ,  Pde.productSize as Size , Pro.productName as Name," +
                                                                        "Cat.categoryName as Category  ,  Cat.categoryUnit as Unit , S.expiryDate as Expiry " +
                                                                        "FROM  stock S  JOIN productsupplied Psup ON S.productSuppliedId = Psup.productSuppliedId " +
                                                                        "JOIN productdetails Pde ON Psup.productDetailId = Pde.productDetailId " +
                                                                        "JOIN product Pro ON Pde.productId = Pro.productId  JOIN category Cat  ON Pro.categoryId = Cat.categoryId " +
                                                                        " WHERE S.quantity > S.itemsold and DATEDIFF(S.expirydate,CURDATE()) > Pde.alertBFExpiryDays  and DATEDIFF(S.expirydate,CURDATE()) < (Pde.alertBFExpiryDays + 5) group by Pde.productDetailId  order by Pro.productName;").ToList();
            
            return ExpiryAlerts;
        }


        public DateTime thisdate( int? days)
        {
            DateTime date = DateTime.Now;
            date.AddDays((double)days);
            return date;        
        }
   
        #endregion

      #region Sales Part
        // PricingStrategy   same  , highest
        #region highest price sale
        public string SaveASoldItemHighestPrice(int Quantity, int requiredproDetailID, int salesID)
        {
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var chkpro =db.solditems.Where(s=>s.salesId==salesID && s.productDetailId==requiredproDetailID).FirstOrDefault();
                    if (chkpro == null)
                    {
                        solditem sold = new solditem();
                        // asuming quantity is one and no discount on product level
                        DateTime date = DateTime.Today;
                        int? pd = db.productdetails.Find(requiredproDetailID).alertBFExpiryDays;
                        DateTime dt2 = date.AddDays((int)pd);
                        var getStock = db.stocks.Where(s => s.productsupplied.productDetailId == requiredproDetailID && s.quantity > s.itemSold && s.expiryDate > dt2).Max(p => p.sellingPricePrItem);
                        sold.amount = getStock; // saving single item amount 
                        sold.quantity = Quantity;
                        sold.productDetailId = requiredproDetailID;
                        sold.salesId = salesID;
                        var newsold = db.solditems.Add(sold);
                        db.SaveChanges();
                    }
                    else {                        
                        double newsinglePrice = (double)(chkpro.amount / chkpro.quantity);
                        chkpro.quantity += 1;
                        chkpro.amount = newsinglePrice *chkpro.quantity;
                        db.SaveChanges();
                    }
                    double total = (double)db.solditems.Where(x => x.salesId == salesID).Sum(sa => sa.amount );
                    sale getsale = db.sales.Find(salesID);
                    getsale.saleAmount = total;
                    if (getsale.discounPercentage != null && getsale.discounPercentage > 0)
                    {
                        getsale.netAmount = (double)(total - ((total * getsale.discounPercentage) / 100));
                    }
                    else
                    {
                        getsale.netAmount = total;
                    }
                    db.SaveChanges();
                    dbTransaction.Commit();
                    return "ok";
                }
                catch (Exception)
                {
                    dbTransaction.Rollback();
                    return "Not-ok";
                }
            }
        }
        public string updateQuantityOfASoldItemHighestPrice(int Quantity, int soldItemId)
        {
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    solditem thisSoldItem = db.solditems.Find(soldItemId);
                    double newsinglePrice = (double)(thisSoldItem.amount / thisSoldItem.quantity);
                    thisSoldItem.quantity = Quantity;
                    thisSoldItem.amount = newsinglePrice * thisSoldItem.quantity;
                    db.SaveChanges();
                    //////////change sale amount accourding to quantity 
                    double total = (double)db.solditems.Where(x => x.salesId == thisSoldItem.salesId).Sum(sa => sa.amount );
                    sale sale = db.sales.Find(thisSoldItem.salesId);
                    sale.saleAmount = total;
                    if (sale.discounPercentage != null && sale.discounPercentage > 0)
                    {
                        sale.netAmount = (double)(total - ((total * sale.discounPercentage) / 100));
                    }
                    else
                    {
                        sale.netAmount = total;
                    }

                    db.SaveChanges();
                    dbTransaction.Commit();
                    return "ok";
                }
                catch (Exception)
                {
                    dbTransaction.Rollback();
                    return "Not-ok";
                }
            }
        }
        public string BillSalesHighestPrice(int salesID, int paymentTypeId)
        {
            string MessageReturn = "";
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    //stock decreacig and soldstock rows entres
                    var solditems = db.solditems.Where(d => d.salesId == salesID);
                    if (solditems != null)
                    {
                        foreach (var sold in solditems)
                        {
                            int remainingQty = sold.quantity;
                            DateTime now = DateTime.Today;
                            DateTime nowPlusDays = now.AddDays((int)sold.productdetail.alertBFExpiryDays);
                            var chkStok = db.stocks.Where(s => s.productsupplied.productDetailId == sold.productDetailId && s.quantity > s.itemSold && s.expiryDate > nowPlusDays).OrderBy(s => s.expiryDate);
                            foreach (var cs in chkStok)
                            {
                                if ((cs.quantity - cs.itemSold) >= remainingQty)
                                {
                                    //items are avalible in this row 
                                    cs.itemSold += remainingQty;
                                    db.SaveChanges();
                                    soldstock sstk = new soldstock();
                                    sstk.soldItemId = sold.soldItemId;
                                    sstk.stockId = cs.stockId;
                                    double newsinglePrice = (double)(sold.amount / sold.quantity);
                                    sstk.singleItemPrice = newsinglePrice;
                                    sstk.rowQuantity = remainingQty;
                                    db.soldstocks.Add(sstk);
                                    db.SaveChanges();
                                    break;
                                }
                                else
                                {
                                    remainingQty -= (int)(cs.quantity - cs.itemSold);                                   
                                    soldstock sstk = new soldstock();
                                    sstk.soldItemId = sold.soldItemId;
                                    sstk.stockId = cs.stockId;
                                    double newsinglePrice = (double)(sold.amount / sold.quantity);
                                    sstk.singleItemPrice = newsinglePrice;
                                    sstk.rowQuantity = (int)(cs.quantity - cs.itemSold);
                                    db.soldstocks.Add(sstk);
                                    db.SaveChanges();
                                    cs.itemSold = cs.quantity;
                                    db.SaveChanges();
                                }
                            }
                        }

                        sale sales = db.sales.Find(salesID);
                        sales.salesStatus = "Done";
                        sales.paymentTypeId = (short)paymentTypeId;
                        sales.salesNumber = GetSaleNumber(sales.salesmansaleday.salesDayId); ///sales.salesId; ;
                        db.SaveChanges();
                        dbTransaction.Commit();
                        MessageReturn = "Done";
                    }
                    else {
                        MessageReturn = "no item found";
                    }
                   
                }
                catch (Exception)
                {
                    dbTransaction.Rollback();
                    MessageReturn = MessageReturn + "can not perform this Bill , Please check Your network Connection and try again!!!!!!!!!";
                    return MessageReturn;
                }
            }
            return MessageReturn;
        }
        public List<SaleWithInStockItem> salesItemsWithInStockItem(int salesID)
        {
            List<SaleWithInStockItem> avialbleItem = new List<SaleWithInStockItem>();
            try
            {
                var xyz = db.Database.SqlQuery<SaleWithInStockItem>("SELECT  SI.soldItemId  as SoldItemId, SI.productDetailId as ProductDetailID," +
                    " SI.amount as priceUnit, SI.quantity as Quantity, P.productName as Pname, " +
                    " C.categoryUnit as Cunit, C.categoryName as Cname, Pde.productSize as Psize, " +
                    " (select sum(S.quantity-S.itemsold) from  stock S  Join productsupplied PS on S.productSuppliedId = PS.productSuppliedId " +
                    " where   S.quantity > S.itemsold and DATEDIFF(S.expirydate,CURDATE()) > Pde.alertBFExpiryDays " +
                    " and PS.productDetailId= Pde.productDetailId ) as InStockItem " +
                    " FROM  solditems  SI  JOIN productdetails Pde On  Pde.productDetailId =SI.productDetailId " +
                    " JOIN product P ON Pde.productId = P.productId Join category C ON  P.categoryId=C.categoryId " +
                    " where   SI.salesId = " + salesID.ToString() + " ;");
                avialbleItem = xyz.ToList();
                return avialbleItem;
            }
            catch (Exception e)
            {
                //SaleWithInStockItem s = new SaleWithInStockItem();
                //s.Pname = e.Message;
                //avialbleItem.Add(s);
                return avialbleItem;
            }
        }
        public List<IdAndSum> GetGridValues(int id)
        {
            List<IdAndSum> list = new List<IdAndSum>();
            List<solditem> xyz = db.solditems.Where(d => d.salesId == id).ToList();
            foreach(var v in xyz)
            {
            IdAndSum i= new IdAndSum();
                i.Unit=v.productdetail.product.category.categoryUnit;
                i.Size=(int)v.productdetail.productSize;
                i.Name=v.productdetail.product.productName;
                i.Sum=v.quantity;
                i.PurchaseSum=(double)v.amount;
                list.Add(i);
            }           
            return list;        
        }
        public List<IdAndSum> GetProductGridValues(int prodetid,DateTime date)
        {
            List<IdAndSum> list = new List<IdAndSum>();
            DateTime d2=date.AddDays(1);
            List<solditem> xyz = db.solditems.Where(d => d.sale.date >= date && d.sale.date<d2&& d.productDetailId==prodetid).ToList();
            foreach (var v in xyz)
            {
                IdAndSum i = new IdAndSum();
                i.Unit = v.productdetail.product.category.categoryUnit;
                i.Size = (int)v.productdetail.productSize;
                i.Name = v.productdetail.product.productName;
                i.Sum = v.quantity;
                i.PurchaseSum = (double)v.amount;
                list.Add(i);
            }
            return list;
        }
        public bool DeleteSoldItem(int? solditemID)
        {
            ///////delete soldstok then sold item of respective  solditem ID        
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var soldStockItems = db.soldstocks.Where(s => s.soldItemId == solditemID);
                    db.soldstocks.RemoveRange(soldStockItems);
                    db.SaveChanges();
                    solditem SI = db.solditems.Find(solditemID);
                    sale saleitem = db.sales.Find(SI.salesId);
                    saleitem.saleAmount -= (SI.amount * SI.quantity);
                    saleitem.netAmount = (double)(saleitem.saleAmount - ((saleitem.saleAmount * saleitem.discounPercentage) / 100));
                    db.SaveChanges();
                    db.solditems.Remove(SI);
                    db.SaveChanges();
                    dbTransaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    dbTransaction.Rollback();
                    return false;
                }
            }
        }
     
        #endregion
        #region same price sale
        public string SaveASoldItem(int Quantity, int requiredproDetailID, int salesID)
        {
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    solditem sold = new solditem();
                    soldstock newsale = new soldstock();
                    // asuming quantity is one and no discount on product level
                    DateTime date = DateTime.Today;
                    int? pd = db.productdetails.Find(requiredproDetailID).alertBFExpiryDays;
                    DateTime dt2 = date.AddDays((int)pd);
                    var getStock = db.stocks.Where(s => s.productsupplied.productDetailId == requiredproDetailID && s.quantity > s.itemSold && s.expiryDate > dt2).OrderBy(x => x.expiryDate).FirstOrDefault();
                    sold.amount = getStock.sellingPricePrItem; // saving single item amount 
                    sold.quantity = Quantity;
                    sold.productDetailId = requiredproDetailID;
                    sold.salesId = salesID;
                    var newsold = db.solditems.Add(sold);
                    db.SaveChanges();
                    newsale.soldItemId = newsold.soldItemId;
                    newsale.stockId = getStock.stockId;
                    newsale.rowQuantity=Quantity;
                    newsale.singleItemPrice=getStock.sellingPricePrItem;
                    db.soldstocks.Add(newsale);
                    db.SaveChanges();
                    double total = (double)db.solditems.Where(x => x.salesId == salesID).Sum(sa => sa.amount );
                    sale getsale = db.sales.Find(salesID);
                    getsale.saleAmount = total;
                    if (getsale.discounPercentage != null && getsale.discounPercentage > 0)
                    {
                        getsale.netAmount = (double)(total - ((total * getsale.discounPercentage) / 100));
                    }
                    else
                    {
                        getsale.netAmount = total;
                    }
                    db.SaveChanges();
                    dbTransaction.Commit();
                    return "ok";
                }
                catch (Exception)
                {
                    dbTransaction.Rollback();
                    return "Not-ok";
                }
            }
        }
        public string updateQuantityOfASoldItem(int Quantity, int soldItemId)
        {
            using (var dbTransaction = db.Database.BeginTransaction())
            {// solditemid is soldstockid
                try
                {///need to change this function thouraly
                    soldstock thisSoldItem = db.soldstocks.Find(soldItemId);
                    if (thisSoldItem.rowQuantity < Quantity)
                    {
                        var vallz = db.stocks.Where(u => u.stockId == thisSoldItem.stockId).Select(a => new { price = a.sellingPricePrItem, sID = a.stockId, availbleitems = (a.quantity - a.itemSold) }).FirstOrDefault();
                        if (vallz.availbleitems > Quantity)
                        {
                            thisSoldItem.rowQuantity = Quantity;
                            db.SaveChanges();
                            solditem slditm = db.solditems.Where(jk => jk.soldItemId == thisSoldItem.soldItemId).FirstOrDefault();
                            slditm.amount = db.soldstocks.Where(g => g.soldItemId == slditm.soldItemId).Sum(l=>l.singleItemPrice*l.rowQuantity);
                            slditm.quantity = (int)db.soldstocks.Where(g => g.soldItemId == slditm.soldItemId).Sum(j=>j.rowQuantity);
                            db.SaveChanges();
                        }
                        else
                        {
                            List<int> sideez = db.soldstocks.Where(f => f.solditem.salesId == thisSoldItem.solditem.salesId && f.solditem.productDetailId == thisSoldItem.solditem.productDetailId).Select(x => x.stockId).ToList();
                            thisSoldItem.solditem.quantity = (int)vallz.availbleitems;
                            db.SaveChanges();
                            int remainquantity = (int)(Quantity - vallz.availbleitems);
                            DateTime date = DateTime.Today;
                            int? pd = db.productdetails.Find(thisSoldItem.solditem.productDetailId).alertBFExpiryDays;
                            DateTime dt2 = date.AddDays((int)pd);
                            var getStock2 = db.stocks.Where(s => s.productsupplied.productDetailId == thisSoldItem.solditem.productDetailId && s.quantity > s.itemSold && s.expiryDate > dt2).OrderBy(x => x.expiryDate).Select(q => new { sId = q.stockId, avalibleItems = (q.quantity - q.itemSold), price = q.sellingPricePrItem });
                            foreach (var gs in getStock2)
                            {
                                if (!sideez.Contains(gs.sId))
                                {
                                    sideez.Add(gs.sId);
                                    if (gs.avalibleItems >= remainquantity)
                                    { //add new row in sold stock  
                                        soldstock newsstock = new soldstock();
                                        newsstock.soldItemId = thisSoldItem.soldItemId;
                                        newsstock.stockId = gs.sId;
                                        newsstock.rowQuantity=remainquantity;
                                        newsstock.singleItemPrice=gs.price;
                                        db.soldstocks.Add(newsstock);
                                        db.SaveChanges();
                                        break;
                                    }
                                    else
                                    {
                                        //add new row in sold stock  and decrse remaingquantity
                                        soldstock newsstock = new soldstock();
                                        newsstock.soldItemId = thisSoldItem.soldItemId;
                                        newsstock.rowQuantity = gs.avalibleItems;
                                        newsstock.singleItemPrice = gs.price;
                                        newsstock.stockId = gs.sId;
                                        db.soldstocks.Add(newsstock);
                                        db.SaveChanges();
                                        remainquantity =(int)(remainquantity - gs.avalibleItems);
                                    }
                                }
                            }
                            solditem slditm = db.solditems.Where(jk => jk.soldItemId == thisSoldItem.soldItemId).FirstOrDefault();
                            slditm.amount = db.soldstocks.Where(g => g.soldItemId == slditm.soldItemId).Sum(l => l.singleItemPrice * l.rowQuantity);
                            slditm.quantity = (int)db.soldstocks.Where(g => g.soldItemId == slditm.soldItemId).Sum(j => j.rowQuantity);
                            db.SaveChanges();

                        }
                    }
                    else
                    {
                        thisSoldItem.rowQuantity = Quantity;
                        db.SaveChanges();
                        solditem slditm = db.solditems.Where(jk => jk.soldItemId == thisSoldItem.soldItemId).FirstOrDefault();
                        slditm.amount = db.soldstocks.Where(g => g.soldItemId == slditm.soldItemId).Sum(l => l.singleItemPrice * l.rowQuantity);
                        slditm.quantity = (int)db.soldstocks.Where(g => g.soldItemId == slditm.soldItemId).Sum(j => j.rowQuantity);
                        db.SaveChanges();
                    }
                  
                    //////////change sale amount accourding to quantity 
                    double total = (double)db.solditems.Where(x => x.salesId == thisSoldItem.solditem.salesId).Sum(sa => sa.amount );
                    sale sale = db.sales.Find(thisSoldItem.solditem.salesId);
                    sale.saleAmount = total;
                    if (sale.discounPercentage != null && sale.discounPercentage > 0)
                    {
                        sale.netAmount = (double)(total - ((total * sale.discounPercentage) / 100));
                    }
                    else
                    {
                        sale.netAmount = total;
                    }

                    db.SaveChanges();
                    dbTransaction.Commit();
                    return "ok";
                }
                catch (Exception)
                {
                    dbTransaction.Rollback();
                    return "Not-ok";
                }
            }
        }
        public string BillSales(int salesID, int paymentTypeId)
        {
            bool anychanges = false;
            string MessageReturn = "";
            bool AllOK = true;
            #region Check if all item are in stock
            List<SaleWithInStockItem> newlist = new List<SaleWithInStockItem>();
            var getsalethis = db.Database.SqlQuery<SaleWithInStockItem>("SELECT  SI.productDetailId as ProductDetailID, "+
                " SI.quantity as Quantity, P.productName as Pname, "+
                "  C.categoryUnit as Cunit, Pde.productSize as Psize, "+
                " (select sum(S.quantity-S.itemsold) from  stock S  Join productsupplied PS on S.productSuppliedId = PS.productSuppliedId "+
                " where   S.quantity > S.itemsold and DATEDIFF(S.expirydate,CURDATE()) > Pde.alertBFExpiryDays "+
                " and PS.productDetailId= Pde.productDetailId ) as InStockItem "+
                " FROM  solditems  SI "+
                " JOIN productdetails Pde On  Pde.productDetailId =SI.productDetailId "+
                " JOIN product P ON Pde.productId = P.productId Join category C ON  P.categoryId=C.categoryId "+
                " where   SI.salesId = "+ salesID +" ; ");
            if (getsalethis != null)
            {
                newlist = getsalethis.ToList();
            }
            if (newlist.Count > 0)
            {
                foreach (var gets in newlist)
                {
                    if (gets.Quantity > gets.InStockItem)
                    {
                        AllOK = false;
                        MessageReturn += gets.Pname + ", " + gets.Psize + gets.Cunit + "Required Items are( " + gets.Quantity + " )" + " Available Items are (" + gets.InStockItem + " ).";
                    }
                }
            }
            else {
                return "nothing to sell";
            }
            #endregion 

           if (AllOK == true)
            {                ////do selling
                using (var dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        List<TwoIntegers> Remainings = new List<TwoIntegers>();
                        TwoIntegers item = new TwoIntegers();
                        var ToSellItems = db.soldstocks.Where(s => s.solditem.salesId == salesID);
                        foreach (var sel in ToSellItems)
                        {
                            if ((sel.stock.quantity - sel.stock.itemSold) >= sel.rowQuantity)
                            {
                                sel.stock.itemSold += sel.rowQuantity;
                                db.SaveChanges();
                            }
                            else
                            {
                                item = new TwoIntegers();
                                item.Value = (int)(sel.rowQuantity - (sel.stock.quantity - sel.stock.itemSold));
                                item.Id = sel.solditem.productDetailId;
                                item.OldId = sel.soldItemId;
                                Remainings.Add(item);
                                sel.rowQuantity -= item.Value;
                                sel.stock.itemSold = sel.stock.quantity;
                                db.SaveChanges();
                            }
                        }
                        if (Remainings.Count > 0)
                        {
                            anychanges = true;
                            foreach (TwoIntegers newItem in Remainings)
                            {
                                List<int> list = GetStockIdsForProduct(newItem.Id, newItem.Value);
                                int itemRemaining = newItem.Value;
                                soldstock newsold = new soldstock();
                                foreach (int id in list)
                                {
                                    stock st = db.stocks.Find(id);
                                    if ((st.quantity - st.itemSold) >= itemRemaining)
                                    {
                                        st.itemSold += itemRemaining;
                                        db.SaveChanges();
                                        newsold = new soldstock();
                                        newsold.stockId = id;
                                        newsold.soldItemId = newItem.OldId;
                                        newsold.rowQuantity = itemRemaining;
                                        newsold.singleItemPrice = st.sellingPricePrItem;
                                        db.soldstocks.Add(newsold);
                                        db.SaveChanges();
                                        itemRemaining = 0;
                                    }
                                    else
                                    {
                                        itemRemaining = itemRemaining - (int)(st.quantity - st.itemSold);                                     
                                        newsold = new soldstock();
                                        newsold.stockId = id;
                                        newsold.soldItemId = newItem.OldId;
                                        newsold.rowQuantity = (int)(st.quantity - st.itemSold);
                                        newsold.singleItemPrice = st.sellingPricePrItem;
                                        db.soldstocks.Add(newsold);
                                        db.SaveChanges();
                                        st.itemSold = st.quantity;
                                        db.SaveChanges();
                                    }
                                }
                            }
                        }
                        if (anychanges)
                        {
                            var soldiez = db.solditems.Where(i => i.salesId == salesID);
                            foreach(var s in soldiez){
                                s.quantity =(int) db.soldstocks.Where(n => n.soldItemId == s.soldItemId).Sum(d => d.rowQuantity);
                                s.amount = db.soldstocks.Where(n => n.soldItemId == s.soldItemId).Sum(d => d.rowQuantity*d.singleItemPrice);
                                db.SaveChanges();
                            }
                            double total = (double)db.solditems.Where(x => x.salesId == salesID).Sum(sa => sa.amount);
                            sale getsale = db.sales.Find(salesID);
                            getsale.saleAmount = total;
                            if (getsale.discounPercentage != null && getsale.discounPercentage > 0)
                            {
                                getsale.netAmount = (double)(total - ((total * getsale.discounPercentage) / 100));
                            }
                            else
                            {
                                getsale.netAmount = total;
                            }
                            db.SaveChanges();                        
                        }
                        sale sales = db.sales.Find(salesID);
                        sales.salesStatus = "Done";
                        sales.paymentTypeId = (short)paymentTypeId;
                        sales.salesNumber = GetSaleNumber(sales.salesmansaleday.salesDayId); ///sales.salesId; ;
                        db.SaveChanges();
                        dbTransaction.Commit();
                        MessageReturn = "Done";
                        return MessageReturn;
                    }
                    catch (Exception)
                    {
                        dbTransaction.Rollback();
                        MessageReturn = MessageReturn + "can not perform this Bill , Please check Your network Connection and try again!!!!!!!!!";
                        return MessageReturn;
                    }
                }
                
            }
            else
            { ////// no selling return with erro message out of stock case
                return MessageReturn;

            }
        }
        public List<SaleWithInStockItem> salesItemsWithInStockItemSame(int salesID)
        {
            List<SaleWithInStockItem> avialbleItem = new List<SaleWithInStockItem>();
            try
            {//// solditemid is soldstockid
                var xyz = db.Database.SqlQuery<SaleWithInStockItem>("SELECT  SS.soldStokId as SoldItemId, SI.productDetailId as ProductDetailID," +
  " SS.singleItemPrice * SS.rowQuantity as priceUnit, SS.rowQuantity as Quantity, P.productName as Pname, " +
  " C.categoryUnit as Cunit, C.categoryName as Cname, Pde.productSize as Psize, " +
  " (select sum(S.quantity-S.itemsold) from  stock S  Join productsupplied PS on S.productSuppliedId = PS.productSuppliedId " +
  " where   S.quantity > S.itemsold and DATEDIFF(S.expirydate,CURDATE()) > Pde.alertBFExpiryDays " +
  " and PS.productDetailId= Pde.productDetailId ) as InStockItem " +
  " FROM soldstock SS " +
  " join solditems  SI on SS.soldItemId = SI.soldItemId " +
  " JOIN productdetails Pde On  Pde.productDetailId =SI.productDetailId " +
  " JOIN product P ON Pde.productId = P.productId Join category C ON  P.categoryId=C.categoryId " +
  " where   SI.salesId = " + salesID.ToString() + " ;");
                avialbleItem = xyz.ToList();
                return avialbleItem;
            }
            catch (Exception e)
            {
                //SaleWithInStockItem s = new SaleWithInStockItem();
                //s.Pname = e.Message;
                //avialbleItem.Add(s);
                return avialbleItem;
            }
        }
        public bool DeleteSoldItemSame(int? soldStockID)
        {
            ///////delete soldstok then sold item of respective  solditem ID        
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    soldstock soldStockItems = db.soldstocks.Find(soldStockID);
                    solditem SI = db.solditems.Find(soldStockItems.soldItemId);
                    sale sale= db.sales.Find(SI.salesId);
                    if (db.soldstocks.Where(x => x.soldItemId == SI.soldItemId).Count() > 1)
                    {
                        db.soldstocks.Remove(soldStockItems);
                        db.SaveChanges();
                        SI.quantity =(int) db.soldstocks.Where(t => t.soldItemId == SI.soldItemId).Sum(q => q.rowQuantity);
                        SI.amount = db.soldstocks.Where(t => t.soldItemId == SI.soldItemId).Sum(q => q.rowQuantity*q.singleItemPrice);
                        db.SaveChanges();
                        double total = (double)db.solditems.Where(x => x.salesId == sale.salesId).Sum(sa => sa.amount);
                        sale.saleAmount = total;
                        if (sale.discounPercentage != null && sale.discounPercentage > 0)
                        {
                            sale.netAmount = (double)(total - ((total * sale.discounPercentage) / 100));
                        }
                        else
                        {
                            sale.netAmount = total;
                        }
                        db.SaveChanges();
                    }
                    else { 
                    // single item
                        db.soldstocks.Remove(soldStockItems);
                        db.SaveChanges();
                        db.solditems.Remove(SI);
                        db.SaveChanges();
                        double total = (double)db.solditems.Where(x => x.salesId == sale.salesId).Sum(sa => sa.amount);                       
                        sale.saleAmount = total;
                        if (sale.discounPercentage != null && sale.discounPercentage > 0)
                        {
                            sale.netAmount = (double)(total - ((total * sale.discounPercentage) / 100));
                        }
                        else
                        {
                            sale.netAmount = total;
                        }
                        db.SaveChanges();
                    }
                   
                    dbTransaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    dbTransaction.Rollback();
                    return false;
                }
            }
        }
     
        #endregion

        #region Sales After Editing
         public int GetSaleNumber(int SaleDayId)
        {
            int salenumber = 0;
            using (tobaccocastleEntities db2 = new tobaccocastleEntities())
            {
                salesday s = db2.salesdays.Find(SaleDayId);
                salenumber = (int)s.saleNumber;
                s.saleNumber = salenumber + 1;
                db2.SaveChanges();
            }
            return salenumber;
        }
        public SelectList GetPaymentTypes()
        {
            var types = db.paymenttypes;
            return new SelectList(types, "paymentTypeId", "paymentTypeName");
        }
        public int CheckCustomer(int GuestID,int SaleId) {
            int p = (int)db.sales.Find(SaleId).patientsId;
            if (p == GuestID)
            { 
            return 0;
            }
          return 1;
        }
       public string  AttachCustomer(int CustomerId,int SaleID)
     {
         sale s = db.sales.Find(SaleID);
         s.patientsId = CustomerId;
         db.SaveChanges();
         return db.patients.Find(CustomerId).patientName;
    }
       public SelectList SearchPatient(string SearchKey)
       {
           List<ProductListWithSizeAndCategory> patientsList = new List<ProductListWithSizeAndCategory>();

           var patients = db.patients.Where(p => p.patientName.Contains("" + SearchKey) || p.contactNumber.Contains("" + SearchKey));
           ProductListWithSizeAndCategory patient = new ProductListWithSizeAndCategory();
           foreach (var p in patients)
           {
               patient = new ProductListWithSizeAndCategory();
               patient.DetailProductName = p.patientName + " , " + p.patientFatherName + " , " + p.contactNumber;
               patient.Id = p.patientsId;
               patientsList.Add(patient);
           }
           SelectList list = new SelectList(patientsList, "Id", "DetailProductName");
           return list;
       }
       public int StartANewSale(int EmployId, DateTime Date, int patientId, int? dicPrcentage, int? saleNumber, int? returnId , int salemanDayId)
       {
           sale newSale = new sale();
           newSale.empId = EmployId; 
           newSale.date = Date;
           newSale.patientsId = patientId;
           newSale.returnOfSalesId = returnId;
           newSale.salesStatus = "Draft";
           newSale.discounPercentage = dicPrcentage;
           newSale.salesNumber = 0;
           newSale.salesmanSaleDayId = salemanDayId;
           newSale.saleAmount = 0;
           newSale.netAmount = 0;
           var forId = db.sales.Add(newSale);
           db.SaveChanges();          
           return forId.salesId;
       }
       public sale GetSalesDetails(int Salesid)
       {
           return db.sales.Find(Salesid);
       }
       public int NumberOfItemOfSale(int Salesid)
       {
        int? val=db.solditems.Where(s => s.salesId == Salesid).Sum(m=>m.quantity);
           if(val==null)
           {
               return 0;
           }
           return (int)val;
       }
       public double? SubTotalOfSale(int Salesid)
       {
      var  val=db.sales.Find(Salesid).saleAmount;
         if (val == null)
         {
             return 0;
         }
         return val;
       }
       public double? TotalOfSale(int Salesid)
       {
           var val = db.sales.Find(Salesid).netAmount;
           if (val == null)
           {
               return 0;
           }
           return val;
       }
       public string  UpdateDiscountOfSale(int salesID,double discountPercentage)
        {
            try
            {
                sale s = db.sales.Find(salesID);
                s.discounPercentage = discountPercentage;
                if (discountPercentage != 0)
                {
                    s.netAmount =s.saleAmount- (s.saleAmount * discountPercentage) / 100;
                }
                else {
                    s.netAmount = s.saleAmount;
                }
                db.SaveChanges();
                return "ok";
            }
            catch {
                return "Error";
            }
           
        }
       public SelectList SearchProduct(string SearchKey)
       {
           var query = from l in db.productdetails.Where(p => p.barcode.Contains("" +SearchKey)|| p.product.productName.Contains("" + SearchKey))
                       select new { l.productDetailId, l.product.productName, l.productSize, l.product.category.categoryName, l.product.category.categoryUnit, l.barcode };
           var products = query.AsEnumerable().Select(g => new { id = g.productDetailId, name = g.productName + " , "+g.productSize+" "+g.categoryUnit+", " + g.barcode}).ToList();
           return new SelectList(products, "id", "name");
       }
       public SelectList SearchProductNS(string SearchKey,int suppID)
       {
           var query = from l in db.productsupplieds.Where(p =>p.supplierId==suppID && (p.productdetail.product.productName.Contains("" + SearchKey) || p.productdetail.barcode.Contains("" +SearchKey)))
                       select new { l.productSuppliedId, l.productdetail.product.productName, l.productdetail.productSize, l.productdetail.product.category.categoryName, l.productdetail.product.category.categoryUnit,l.productdetail.barcode };
           var products = query.AsEnumerable().Select(g => new { id = g.productSuppliedId, name = g.productName + " , " + g.productSize.ToString() + ", "  + g.barcode }).ToList();
           return new SelectList(products, "id", "name");
       }
       public ReturnValuesForStock GetNumberOfAvalibleProducts(int productDetailId)
       {
           ReturnValuesForStock values = new ReturnValuesForStock();
           double price = 0.0;
           int count = 0;
           DateTime now = DateTime.Now;
           DateTime nowPlusDays = now.AddDays((int)GetEDayOfAProDuctDetail(productDetailId));
           var chkStok = db.stocks.Where(s => s.productsupplied.productDetailId == productDetailId && s.quantity != s.itemSold && s.expiryDate > nowPlusDays).OrderBy(e=>e.expiryDate);
           foreach (var stok in chkStok)
           {
               count += (stok.quantity - (int)stok.itemSold);
               if (stok.sellingPricePrItem > price)
               {
                   price = stok.sellingPricePrItem;
               }
           }
           values.Count = count;
           values.Price = price;
           return values;
       }
       public string GetProductNameForSoldItem(int productdetailid)
       {
           var products = db.productdetails.Where(p => p.productDetailId == productdetailid).FirstOrDefault();
           string ProductNametoReturn = ProductNametoReturn = products.product.productName + " , " + products.productSize +" "+ products.product.category.categoryUnit + " , " + products.product.category.categoryName;
           return ProductNametoReturn;
       }
       public string GetCustomerName(int? salesId)
       {
           sale sale = db.sales.Find(salesId);
           return sale.patient.patientName;
       }
       public List<sale> GetDraftSalesOfToday(int EmpId,int salemanSaleDayId)
       {
           var Draftsales = db.sales.Where(s =>s.empId==EmpId&&s.salesmanSaleDayId==salemanSaleDayId &&s.salesStatus == "Draft" &&s.solditems.Count>0);
           List<sale> list = Draftsales.ToList();
           return list;
       }
       public List<sale> GetDraftSalesOfToday()
       {
           var Draftsales = db.sales.Where(s => s.salesStatus == "Draft");
           List<sale> list = Draftsales.ToList();
           return list;
       }
       public bool RemoveAllDraftSalesOfToday()
       {
          List<int> idz = new List<int>();
           var Draftsales = db.sales.Where(s => s.salesStatus == "Draft");
          foreach(sale s in Draftsales)
          {
              idz.Add(s.salesId);
          }
          foreach (int id in idz)
          {
              CancelSale(id);          
          }
           return true;
       }
       public bool RemoveAllDraftSalesOfTodayOfAnEmploye(int empId)
       {
           List<int> idz = new List<int>();
           var Draftsales = db.sales.Where(s =>s.empId==empId &&s.salesStatus == "Draft");
           using (var dbTransaction = db.Database.BeginTransaction())
           {
               try
               {
                   db.sales.RemoveRange(Draftsales);
                   db.SaveChanges();
                   dbTransaction.Commit();
                   return true;
               }
               catch (Exception)
               {
                   dbTransaction.Rollback();
                   return false;
               }
           }

           //foreach (sale s in Draftsales)
           //{
           //    idz.Add(s.salesId);
           //}
           //foreach (int id in idz)
           //{
           //    CancelSale(id);
           //}
          
       }
      
       public bool CancelSale(int salesID)
       {
           string chksale = db.sales.Find(salesID).salesStatus;
           if (chksale == "Draft")
           {             
               using (var dbTransaction = db.Database.BeginTransaction())
               {
                   try
                   {   sale sales = db.sales.Find(salesID);
                       db.sales.Remove(sales);
                       db.SaveChanges();
                       dbTransaction.Commit();
                       return true;
                   }
                   catch (Exception)
                   {
                       dbTransaction.Rollback();
                       return false;
                   }
               }
           }
           return false;
       }
        
       public List<SalesTableStructure> salesItems(int? salesID)
       {
           List<SalesTableStructure> list = new List<SalesTableStructure>();
           SalesTableStructure Item = new SalesTableStructure();
           var sale = db.solditems.Where(s => s.salesId == salesID).Include(s => s.soldstocks);
           foreach (var s in sale)
           {
               Item = new SalesTableStructure();
               //var sold = s.soldstocks.Where(t => t.soldItemId == s.soldItemId);
               Item.Product = s.productdetail.product.productName + " , " + s.productdetail.productSize + " " + s.productdetail.product.category.categoryUnit;
               Item.Quantity = s.quantity;
               Item.UnitPrice = s.amount / s.quantity;
               Item.Amount = s.amount;
               Item.SoldItemId = s.soldItemId;
               list.Add(Item);
           }
           return list;
       }
       public int? GetEDayOfAProDuctDetail(int productDetailId)
       {
           var pro =db.productdetails.Where(pr => pr.productDetailId == productDetailId).FirstOrDefault();
           return pro.alertBFExpiryDays;
       }
       public List<int> GetStockIdsForProduct(int productDetailId, int quantityReqired)
       {
           List<int> stockIdz = new List<int>();
           int remainquantity = quantityReqired;
           DateTime now = DateTime.Now;
           DateTime nowPlusDays = now.AddDays((int)GetEDayOfAProDuctDetail(productDetailId));
           var chkStok = db.stocks.Where(s => s.productsupplied.productDetailId == productDetailId && s.quantity != s.itemSold && s.expiryDate > nowPlusDays).OrderBy(s => s.expiryDate);

           foreach (var stok in chkStok)
           {
               if ((stok.quantity - stok.itemSold) >= remainquantity)
               {
                   remainquantity = 0;
                   stockIdz.Add(stok.stockId);
                   
                   //return stock id
                   break;
               }
               else
               {
                   if (remainquantity != 0)
                   {
                       remainquantity = remainquantity - stok.quantity;
                       stockIdz.Add(stok.stockId);

                       //return stock id
                   }
               }
           }

           return stockIdz;
       }
       public int GetAvailableStock(int productdetailId)
       {
           var avialbleItem =db.Database.SqlQuery<int>("SELECT sum(S.quantity-S.itemsold) as inStockItem  " +
               "FROM  stock S  JOIN productsupplied Psup ON S.productSuppliedId = Psup.productSuppliedId  "+
               "JOIN productdetails Pde ON Psup.productDetailId = Pde.productDetailId  "+
               "WHERE S.quantity > S.itemsold and DATEDIFF(S.expirydate,CURDATE()) = Pde.alertBFExpiryDays and Pde.productDetailId =  " + productdetailId.ToString()+ "; ");
           if (avialbleItem == null)
           {
               return 0;
           }
           else {
               return avialbleItem.Single();
           }
       }
      
        public List<ReturnValuesForStock> GetStockIdsWithPriceForProduct(int productDetailId, int quantityReqired)
       {
           List<ReturnValuesForStock> stockIdz = new List<ReturnValuesForStock>();
           int remainquantity = quantityReqired;
           ReturnValuesForStock val = new ReturnValuesForStock();
           DateTime now = DateTime.Now;
           DateTime nowPlusDays = now.AddDays((int)GetEDayOfAProDuctDetail(productDetailId));
           var chkStok = db.stocks.Where(s => s.productsupplied.productDetailId == productDetailId && s.quantity != s.itemSold && s.expiryDate > nowPlusDays).OrderBy(s => s.expiryDate);

           foreach (var stok in chkStok)
           {
               val = new ReturnValuesForStock();
               if ((stok.quantity - stok.itemSold) >= remainquantity)
               {
                   val.Id = stok.stockId;
                   val.Count = remainquantity;
                   val.Price = stok.sellingPricePrItem;
                   stockIdz.Add(val);
                   remainquantity = 0;
                   //return stock id
                   break;
               }
               else
               {
                   if (remainquantity != 0)
                   {
                       val.Id = stok.stockId;
                       val.Count = (int)(stok.quantity - stok.itemSold);
                       val.Price = stok.sellingPricePrItem;
                       stockIdz.Add(val);
                       remainquantity = remainquantity - stok.quantity;
                       //return stock id
                   }
               }
           }

           return stockIdz;
       }
   
        //old and not in used functions
        public int? SearchProductByBarcode(string SearchKey)
        {
            try{
                var xyz = db.productdetails.Where(d => d.barcode == SearchKey).FirstOrDefault().productDetailId;
                return xyz;
            }
           catch{
               return 0;
           }
            
           
           
        }
        public string BillSales(int salesID)
        {
            string MessageReturn = "";
            bool AllOK = true;
            var q = from g in
                        (from c in db.solditems.Where(s => s.salesId == salesID) group c by c.productDetailId)
                    select new { g.Key, sum = g.Sum(c => c.quantity) };

            if (q.Count() < 1)
            {
                return "Nothing to Sell :)";
            }
            if (q != null)
            {
                foreach (var s in q)
                {
                    ReturnValuesForStock avalibleItems = GetNumberOfAvalibleProducts(s.Key);
                    if (avalibleItems.Count < s.sum)
                    {
                        AllOK = false;
                        MessageReturn = MessageReturn + " product Name(" + GetProductNameForSoldItem(s.Key) + ") Require Items are (" + s.sum + ")  avalible items are (" + avalibleItems.Count + ")." + Environment.NewLine;
                    }
                }
            }
            else
            {
                AllOK = false;
            }

            if (AllOK == true)
            {                ////do selling
                using (var dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        List<TwoIntegers> Remainings = new List<TwoIntegers>();
                        TwoIntegers item = new TwoIntegers();
                        var ToSellItems = db.soldstocks.Where(s => s.solditem.salesId == salesID);
                        foreach (var sel in ToSellItems)
                        {
                            if ((sel.stock.quantity - sel.stock.itemSold) >= sel.solditem.quantity)
                            {
                                sel.stock.itemSold += sel.solditem.quantity;
                                db.SaveChanges();
                            }
                            else
                            {
                                item = new TwoIntegers();
                                item.Value = (int)(sel.solditem.quantity - (sel.stock.quantity - sel.stock.itemSold));
                                item.Id = sel.solditem.productDetailId;
                                item.OldId = sel.soldItemId;
                                Remainings.Add(item);
                                sel.stock.itemSold = sel.stock.quantity;
                                db.SaveChanges();
                            }
                        }
                        if (Remainings.Count > 0)
                        {
                            foreach (TwoIntegers newItem in Remainings)
                            {
                                List<int> list = GetStockIdsForProduct(newItem.Id, newItem.Value);
                                int itemRemaining = newItem.Value;
                                soldstock newsold = new soldstock();
                                foreach (int id in list)
                                {
                                    stock st = db.stocks.Find(id);
                                    if ((st.quantity - st.itemSold) >= itemRemaining)
                                    {
                                        st.itemSold += itemRemaining;
                                        db.SaveChanges();
                                        newsold = new soldstock();
                                        newsold.stockId = id;
                                        newsold.soldItemId = newItem.OldId;
                                        db.soldstocks.Add(newsold);
                                        db.SaveChanges();
                                        itemRemaining = 0;
                                    }
                                    else
                                    {
                                        itemRemaining = itemRemaining - (int)(st.quantity - st.itemSold);
                                        st.itemSold = st.quantity;
                                        db.SaveChanges();
                                        newsold = new soldstock();
                                        newsold.stockId = id;
                                        newsold.soldItemId = newItem.OldId;
                                        db.soldstocks.Add(newsold);
                                        db.SaveChanges();
                                    }
                                }
                            }
                        }
                        sale sales = db.sales.Find(salesID);
                        sales.salesStatus = "Done";
                        db.SaveChanges();
                        dbTransaction.Commit();
                        MessageReturn = "Done";
                    }
                    catch (Exception)
                    {
                        dbTransaction.Rollback();
                        MessageReturn = MessageReturn + "can not perform this Bill , Please check Your network Connection and try again!!!!!!!!!";
                        return MessageReturn;
                    }
                }
                return MessageReturn;
            }
            else
            { ////// no selling return with erro message
                return MessageReturn;

            }
        }
        #endregion
      #region Return Of Sales Part
        public List<ReturnOfSalesStructure> salesItemsForReturn(int? salesID)
       {
           List<ReturnOfSalesStructure> list = new List<ReturnOfSalesStructure>();
           ReturnOfSalesStructure Item = new ReturnOfSalesStructure();
           var sale = db.soldstocks.Where(s => s.solditem.salesId == salesID);
           foreach (var s in sale)
           {
               Item = new ReturnOfSalesStructure();
               Item.QuantitySold = s.solditem.quantity;
               Item.AmountSold = (double)s.solditem.amount;
               Item.BatchNo=s.stock.batchNO;
               Item.ExpiryDate=s.stock.expiryDate;
               Item.SoldItemID = s.soldItemId;
               Item.ProductName = s.solditem.productdetail.product.productName + " " + s.solditem.productdetail.productSize.ToString() + " " + s.solditem.productdetail.product.category.categoryUnit;
               list.Add(Item);
           }
           return list;
       }
       public int? GetsaleIdOfSaleNumber(int salesNumber)
       {           
           var sale = db.sales.Where(s => s.salesNumber == salesNumber).FirstOrDefault().salesId;
           return sale;
       }
       public int? GetsaleIdOfSaleNumber(int salesNumber, DateTime saleDate)
       {
           DateTime saledatePlus1 = saleDate.AddDays(1);
           int? saledayId = db.salesdays.Where(g => g.startDateTime >= saleDate && g.startDateTime < saledatePlus1).FirstOrDefault().salesDayId;
           if (saledayId == null)
           {
               return null;
           }
           var sale = db.sales.Where(s => s.salesNumber == salesNumber && s.salesmansaleday.salesDayId == saledayId).FirstOrDefault().salesId;
           return sale;
       }
       public int GetANewReturnId(int salesId, int salemanDayId)
       {           
           returnofsale r = new returnofsale();
           r.amountToReturn = 0;
           r.returnType = "Draft";
           r.salesId = salesId;
           r.date = DateTime.Now;
           r.salesmanSaleDayId=salemanDayId;
           var newReturn = db.returnofsales.Add(r);
           db.SaveChanges();
           return newReturn.returnOfSalesId;
       }
       public bool AddItemsDetailsOfReturn(int[] ids,int ReturnId)
       {
           using (var dbTransaction = db.Database.BeginTransaction())
           {
               try
               {
                   returnitem rItem = new returnitem();
                   foreach (int id in ids)
                   {
                       returnitem OldRItem = db.returnitems.Where(r => r.soldItemId == id).FirstOrDefault();
                       if (OldRItem == null)
                       {
                           rItem = new returnitem();
                           rItem.returnOfSalesId = ReturnId;
                           rItem.soldItemId = id;
                           rItem.returnAmount = 0;
                           rItem.quantityReturned = 0;
                           db.returnitems.Add(rItem);
                           db.SaveChanges();
                       }
                   }
                   dbTransaction.Commit();
                   return true;
               }
               catch
               {
                   dbTransaction.Rollback();
                   return false;
               }
           }       
       }
       public List<ReturnOfSalesStructure> GetItemsDetailsOfReturn(int RetunOfSaleid)
       {
           List<ReturnOfSalesStructure> list = new List<ReturnOfSalesStructure>();
           ReturnOfSalesStructure item=new ReturnOfSalesStructure ();
           var returnz = db.returnitems.Where(i => i.returnOfSalesId == RetunOfSaleid).Include(s => s.solditem);
           foreach (var ret in returnz)
           {
               item = new ReturnOfSalesStructure();
               item.ProductName = ret.solditem.productdetail.product.productName + " " + ret.solditem.productdetail.productSize.ToString() + " " + ret.solditem.productdetail.product.category.categoryUnit;
               item.QuantitySold = ret.solditem.quantity;
               item.AmountSold = (double)ret.solditem.amount;
               item.QuantityReturn = (int)ret.quantityReturned;
               item.AmountReturn =(int)ret.returnAmount;
               item.SoldItemID = ret.returnItemsId;
               list.Add(item);
           
           }
           return list;
       }
     public double  GetDiscountOfASale(int SaleId)
     {
         try
         {
             return (double)db.sales.Find(SaleId).discounPercentage;
         }
         catch {
             return 0;
         }
        
     }
       public bool SaveReturn(int ReturnItemId,int QuantityReturend,int DiscountPrcent)
       {
           using (var dbTransaction = db.Database.BeginTransaction())
           {
               try
               {
                   returnitem ret = db.returnitems.Find(ReturnItemId);
                   double oldAmount=(double)ret.returnAmount;                 
                   ret.quantityReturned = QuantityReturend;
                   double disct = (double)ret.solditem.sale.discounPercentage;
                   ret.returnAmount = ((ret.solditem.amount / ret.solditem.quantity) * QuantityReturend) - ((((ret.solditem.amount / ret.solditem.quantity) * QuantityReturend) * disct) / 100);
                   db.SaveChanges();
                   returnofsale rOfsale = db.returnofsales.Find(ret.returnOfSalesId);
                   var Totalreturn = db.returnitems.Where(s => s.returnOfSalesId == ret.returnOfSalesId).Select(s => s.returnAmount).Sum();
                   rOfsale.amountToReturn = Totalreturn;
                   db.SaveChanges();                  
                   dbTransaction.Commit();
                   return true;
               }
               catch
               {
                   dbTransaction.Rollback();
                   return false;
               }
           }
        }
       public bool CancelThisReturn(int returnId)
       {
           using (var dbTransaction = db.Database.BeginTransaction())
           {
               try
               {
                   var returnz = db.returnitems.Where(r => r.returnOfSalesId == returnId);
                   foreach (returnitem r in returnz)
                   {
                       db.returnitems.Remove(r);
                       db.SaveChanges();
                   }

                   var ret = db.returnofsales.Find(returnId);
                   db.returnofsales.Remove(ret);
                   db.SaveChanges();
                   dbTransaction.Commit();
                   return true;
               }
               catch
               {
                   dbTransaction.Rollback();
                   return false;
               }
           }
       }
       public bool FinalReturn(int returnID, string Status)
       {
           using (var dbTransaction = db.Database.BeginTransaction())
           {
               try
               {
                   List<int> iDz = new List<int>();
                   var retrunz = db.returnitems.Where(r => r.returnOfSalesId == returnID);
                   foreach (returnitem r in retrunz)
                   {
                       iDz.Add(r.returnItemsId);
                   }
                   bool check = MakeReturn(iDz);
                   if (check == true)
                   {
                       var ItemsCheck = db.returnitems.Where(r => r.returnOfSalesId == returnID).Count();
                       if (ItemsCheck > 0)
                       {
                           returnofsale ret = db.returnofsales.Find(returnID);
                           ret.returnType = Status;
                           db.SaveChanges();
                       }
                       else {
                           returnofsale ret = db.returnofsales.Find(returnID);
                           db.returnofsales.Remove(ret);
                           db.SaveChanges();
                       }
                   }
                   else {
                       dbTransaction.Rollback();
                       return false;
                   }

                   dbTransaction.Commit();
                   return true;
               }
               catch
               {
                   dbTransaction.Rollback();
                   return false;
               }
           }
       }
       public bool MakeReturn(List<int> idz)
       {
           try
           {
               foreach (int id in idz)
               {
                   returnitem ret = db.returnitems.Find(id);
                   if (ret.quantityReturned == 0)
                   {
                       db.returnitems.Remove(ret);
                       db.SaveChanges();
                   }
                   else {
                       var  sold = db.soldstocks.Where(s => s.soldItemId == ret.soldItemId).OrderByDescending(e=>e.soldStokId);
                      int QToReturn=(int)ret.quantityReturned;
                       foreach (var s in sold)
                       {
                           if (s.rowQuantity >= QToReturn)
                           {
                               s.stock.itemSold -= QToReturn;
                               db.SaveChanges();
                               break;
                           }
                           else {
                               QToReturn -= (int)s.rowQuantity;
                               s.stock.itemSold -= s.rowQuantity;
                               db.SaveChanges();                           
                           }                           
                       }                      
                   }                  
               }
               return true;
           }
           catch
           {
               return false;
           }
       }
      public double  GetBalanceOrReturn(int returnzId)
      {
          double balance = 0.0;
          returnofsale ret = db.returnofsales.Find(returnzId);
          if (ret != null)
          {                         
              balance =(double) ret.amountToReturn;
          }
          return balance;
       }
       #endregion
      #region Sales Reports
      public double TotalSaleOfToday(int Empid, int salesDayID)
      {
          try
          {
              salesmansaleday sID = db.salesmansaledays.Where(s => s.empId==Empid && s.salesDayId==salesDayID).FirstOrDefault();
              double total =(double) db.sales.Where(s => s.salesmanSaleDayId == sID.salesmanSaleDayId && s.salesStatus != "Draft").Sum(x => x.netAmount);
              return total;
          }
          catch
          {
              return 0.0;
          }
      }
      public double? TotalReturnOfToday(int Empid, int salesDayID)
      {
          try
          {
              salesmansaleday sID = db.salesmansaledays.Where(s => s.empId == Empid && s.salesDayId == salesDayID).FirstOrDefault();
              double? total = db.returnofsales.Where(s => s.salesmanSaleDayId == sID.salesmanSaleDayId && s.returnType != "Draft").Sum(x => x.amountToReturn);
              if (total == null)
              { return 0; }
              else return total;
          }
          catch
          {
              return 0;
          }
      }
      public List<sale> GetSalesofAnEmployeeSaleDay(int salemanDayId,int empID)
      {
          
          return db.sales.Where(s =>s.empId==empID && s.salesmanSaleDayId==salemanDayId  && s.salesStatus != "Draft").ToList();
      }
      
      public List<sale> GetSalesofADate(DateTime date)
      {
          DateTime plusday = date.AddDays(1);
          return db.sales.Where(s => s.date >= date && s.date < plusday  && s.salesStatus != "Draft").ToList();
      }
      public List<sale> GetSalesBTWTwoDate(DateTime FromDate,DateTime ToDate)
      {
          DateTime plusday = ToDate.AddDays(1);
          return db.sales.Where(s => s.date >= FromDate && s.date < plusday && s.salesStatus != "Draft").ToList();
      }
      public List<ProductListWithSizeAndCategory> GetSoldItemsofADate(DateTime date)
      {
          DateTime plusday = date.AddDays(1);
          var xyz = db.solditems.Where(s => s.sale.date >= date && s.sale.date < plusday && s.sale.salesStatus != "Draft").GroupBy(p => p.productDetailId)
              .Select(group => new
              {
                  id = group.Key,
                  total = group.Sum(f => f.quantity),
                  name = group.FirstOrDefault().productdetail.product.productName,
                  size = group.FirstOrDefault().productdetail.productSize,
                  unit = group.FirstOrDefault().productdetail.product.category.categoryUnit
              }).OrderBy(o => o.name).ToList();
          List<ProductListWithSizeAndCategory> list = new List<ProductListWithSizeAndCategory>();
          foreach (var x in xyz)
          {
              ProductListWithSizeAndCategory pro = new ProductListWithSizeAndCategory();
              pro.Id = x.total;
              int si = (int)x.size;
              pro.DetailProductName = x.name + " " + si.ToString() + x.unit;
              list.Add(pro);
          }
          return list;
      }
      public List<ProductListWithSizeAndCategory> GetSoldItemsofTwoDates(DateTime Fromdate,DateTime ToDate )
      {
          DateTime plusday = ToDate.AddDays(1);
          var xyz = db.solditems.Where(s => s.sale.date >= Fromdate && s.sale.date < plusday && s.sale.salesStatus != "Draft").GroupBy(p => p.productDetailId)
               .Select(group => new
               {
                   id = group.Key,
                   total = group.Sum(f => f.quantity),
                   name = group.FirstOrDefault().productdetail.product.productName,
                   size = group.FirstOrDefault().productdetail.productSize,
                   unit = group.FirstOrDefault().productdetail.product.category.categoryUnit
               }).OrderBy(o => o.name).ToList();
          List<ProductListWithSizeAndCategory> list = new List<ProductListWithSizeAndCategory>();
          foreach (var x in xyz)
          {
              ProductListWithSizeAndCategory pro = new ProductListWithSizeAndCategory();
              pro.Id = x.total;
              int si = (int)x.size;
              pro.DetailProductName = x.name + " " + si.ToString() + x.unit;
              list.Add(pro);
          }
          return list;
      }
      public List<returnofsale> GetSalesReturnofADate(DateTime date)
      {
          DateTime plusday = date.AddDays(1);
          return db.returnofsales.Where(s => s.date >= date && s.date < plusday && s.returnType != "Draft").ToList();
      }
      public List<returnofsale> GetSalesReturnBTWTwoDate(DateTime FromDate, DateTime ToDate)
      {
          DateTime plusday = ToDate.AddDays(1);
          return db.returnofsales.Where(s => s.date >= FromDate && s.date < plusday && s.returnType != "Draft").ToList();
      }
      public List<solditem> SalemanSaleData(DateTime date)
      { 
           DateTime dp1 = date.AddDays(1);
           return db.solditems.Where(x => x.sale.date >= date && x.sale.date < dp1 && x.sale.salesStatus != "Draft").ToList();
      }
      public List<solditem> SalemanSaleDataTwo(DateTime fromdate,DateTime date)
      {
          DateTime dp1 = date.AddDays(1);
          return db.solditems.Where(x => x.sale.date >= fromdate && x.sale.date < dp1 && x.sale.salesStatus != "Draft").ToList();
      }
      public List<CostReprtDataType> CostReportOneDate(DateTime date)
      {
          List<CostReprtDataType> newlist = new  List<CostReprtDataType>();
          CostReprtDataType c;
          DateTime dp1 = date.AddDays(1);
          var data = db.soldstocks.Where(i => i.solditem.sale.date >= date && i.solditem.sale.date < dp1 && i.solditem.sale.salesStatus != "Draft").Select(g => new { id = g.solditem.productDetailId, name = g.solditem.productdetail.product.productName, size = g.solditem.productdetail.productSize, unit = g.solditem.productdetail.product.category.categoryUnit, ppricesingle = g.stock.PricePrItem, spricesingle = g.singleItemPrice, quantity = g.rowQuantity, disc = g.solditem.sale.discounPercentage });
          var newdata = data.Select(h => new { Spri = (h.spricesingle * h.quantity) - (h.spricesingle * h.quantity) * (h.disc / 100), Ppri = (h.ppricesingle * h.quantity), qty = h.quantity, names = h.name, siz = h.size, uni = h.unit, i = h.id }).GroupBy(w => w.i).Select(u => new { TotalPPrice = u.Sum(j => j.Ppri), TotalSalePrice = u.Sum(j => j.Spri), Quantity = u.Sum(j => j.qty), name = u.FirstOrDefault().names, size = u.FirstOrDefault().siz, unit = u.FirstOrDefault().uni }).OrderBy(z=>z.name);
          foreach (var d in newdata)
          {
              c = new CostReprtDataType();
              c.name = d.name;
              c.pprice = (double)d.TotalPPrice;
              c.quantity = (int)d.Quantity;
              c.size = d.size;
              c.sprice = (double)d.TotalSalePrice;
              c.unit = d.unit;
              newlist.Add(c);
          }
          return newlist;
      }
      public List<CostReprtDataType> CostReportTwoDate(DateTime date, DateTime toDate)
      {
          List<CostReprtDataType> newlist = new List<CostReprtDataType>();
          CostReprtDataType c;
          DateTime dp1 = toDate.AddDays(1);
          var data = db.soldstocks.Where(i => i.solditem.sale.date >= date && i.solditem.sale.date < dp1 && i.solditem.sale.salesStatus != "Draft").Select(g => new {id=g.solditem.productDetailId ,name = g.solditem.productdetail.product.productName, size = g.solditem.productdetail.productSize, unit = g.solditem.productdetail.product.category.categoryUnit, ppricesingle = g.stock.PricePrItem, spricesingle = g.singleItemPrice, quantity = g.rowQuantity , disc=g.solditem.sale.discounPercentage});
          var newdata = data.Select(h => new { Spri = (h.spricesingle * h.quantity) - (h.spricesingle * h.quantity) * (h.disc / 100), Ppri = (h.ppricesingle * h.quantity), qty = h.quantity, names = h.name, siz = h.size, uni = h.unit, i = h.id }).GroupBy(w => w.i).Select(u => new { TotalPPrice = u.Sum(j => j.Ppri), TotalSalePrice = u.Sum(j => j.Spri), Quantity = u.Sum(j => j.qty), name = u.FirstOrDefault().names, size = u.FirstOrDefault().siz, unit = u.FirstOrDefault().uni }).OrderBy(z=>z.name);
          foreach (var d in newdata)
          {
              c = new CostReprtDataType();
              c.name = d.name;
              c.pprice = (double)d.TotalPPrice;
              c.quantity = (int)d.Quantity;
              c.size = d.size;
              c.sprice = (double)d.TotalSalePrice;
              c.unit = d.unit;
              newlist.Add(c);
          }
          return newlist;
      }

      #endregion
  
      #region partial day close secen 
      public bool CheckNewDayOpen(int cuttoffTime)
      {
          DateTime todayis = DateTime.Today;
          TimeSpan ts = new TimeSpan(cuttoffTime , 0, 0);
          todayis = todayis + ts;
          DateTime todayplus1 = todayis.AddDays(1);
          salesday s = db.salesdays.Where(x => x.startDateTime >= todayis && x.startDateTime < todayplus1).FirstOrDefault();
          if (s != null)
          {
              return true;
          }
          else
          {
              return false;
          }
      }
      public int? GetTodayDayId(int cuttoffTime)
      {
          DateTime todayis = DateTime.Today;
          TimeSpan ts = new TimeSpan(cuttoffTime, 0, 0);
          todayis = todayis + ts;
          DateTime todayplus1 = todayis.AddDays(1) ;        
          int? sdayId = db.salesdays.Where(x => x.startDateTime >= todayis && x.startDateTime < todayplus1).FirstOrDefault().salesDayId;
          if (sdayId != null)
          {
              return sdayId;
          }
          else
          {
              return 0;
          }
      }
      public int StartNewDay(int cuttoffTime)
      {
          salesday s = new salesday();
          TimeSpan ts = new TimeSpan(cuttoffTime, 0, 0);
          s.startDateTime = DateTime.Today+ts;
          s.saleNumber = 1;
          var newSalesDay = db.salesdays.Add(s);
          db.SaveChanges();
          return newSalesDay.salesDayId;
      }
      public int StartSalesmanDay(int EmpId,int SaleDayId,double StartingAmount)
      {
          salesmansaleday ssd = new salesmansaleday();
          ssd.dayStartTime = DateTime.Now;
          ssd.empId = EmpId;
          ssd.salesDayId = SaleDayId;
          ssd.startingAmount = StartingAmount;
          ssd.statusName = "start";
          var sd = db.salesmansaledays.Add(ssd);
          db.SaveChanges();
          return sd.salesmanSaleDayId;      
      }
      public bool EndSalesmanDay(int SalmanSaleDayID, double saleAmount)
      {
          try
          {
              salesmansaleday ssd = db.salesmansaledays.Find(SalmanSaleDayID);
              ssd.dayEndTime = DateTime.Now;
              ssd.endingAmount = saleAmount;
              ssd.statusName = "end";
              db.SaveChanges();
              return true;
          }
          catch {
              return false;
          }
      }
      public bool CheckSalesmanDayOpen(int EmpId,int SaleDayId)
      {          
          salesmansaleday s = db.salesmansaledays.Where(x => x.salesDayId==SaleDayId && x.empId==EmpId && x.statusName=="start").FirstOrDefault();
          if (s != null)
          {
              return true;
          }
          else
          {
              return false;
          }
      }
      public int GetSalesmanDayOpenID(int EmpId, int SaleDayId)
      {
          try
          {
              int s = db.salesmansaledays.Where(x => x.salesDayId == SaleDayId && x.empId == EmpId && x.statusName == "start").FirstOrDefault().salesmanSaleDayId;
              return s;
          }
          catch
          {
              return 0;
          }
      }
      public double TodaysTotalSale(int salemanDayId,int EmpId)
      {
          double TodaysReturnz = TodaysTotalreturn(salemanDayId);
          try
          {             
              double total = (double)db.sales.Where(s => s.salesmanSaleDayId == salemanDayId && s.empId==EmpId && s.salesStatus != "Draft").Sum(x => x.netAmount);
              return total-TodaysReturnz;
          }
          catch
          {
              return 0 -TodaysReturnz;
          }
      }
      public double TodaysTotalreturn(int salemanDayId)
      {
          try
          {
              double total = (double)db.returnofsales.Where(s => s.salesmanSaleDayId == salemanDayId &&  s.returnType != "Draft").Sum(x => x.amountToReturn);
              return total;
          }
          catch
          {
              return 0.0;
          }
      }


      public salesmansaleday GetSalemanDayOfASalemanDayId(int salemanDayId)
      {
          return db.salesmansaledays.Find(salemanDayId);
      }
      public int? CheckPreviousDayOpen(int ThisDaySaleDayId, int EmpId)
      {
          salesmansaleday sd = db.salesmansaledays.Where(d => d.empId == EmpId && d.salesDayId < ThisDaySaleDayId && d.dayEndTime == null).FirstOrDefault();
          if (sd == null)
          {
              return null;
          }
          return sd.salesmanSaleDayId;
      }
      #endregion

           
      #endregion

      #region Borrow /Lend Part

      #region borow Lend Part

      public SelectList GetBranches(int branchid)
        {
            var branches = db.suppliers.Where(s => s.type == "Branch");
            return new SelectList(branches, "supplierId", "supplierName", branchid);
        }
               
        public bool AddLendingRecord(int BranchId ,string borrowerName, string LendingNumber ,DateTime date, int EmpId , int StatusId)
        {
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    order orderagainstborrow = new order();
                    orderagainstborrow.empId = EmpId;
                    orderagainstborrow.orderDate = date;
                    orderagainstborrow.orderNumber = LendingNumber;
                    orderagainstborrow.orderStatusId = 1;
                    orderagainstborrow.supplierId = BranchId;
                   var ord= db.orders.Add(orderagainstborrow);
                    db.SaveChanges();
                    borrowlend BL = new borrowlend();
                    BL.borrowerName = borrowerName;
                    BL.lendingNumber = LendingNumber;
                    BL.borrowLendStatusId = StatusId;
                    BL.branchAsSupplierId = BranchId;
                    BL.empId = EmpId;
                    BL.lendingDate = date;
                    BL.netAmount = 0;
                    BL.orderId = ord.orderId;
                    db.borrowlends.Add(BL);
                    db.SaveChanges(); 
                 
                    dbTransaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    dbTransaction.Rollback();
                    return false;
                }
            }
           
        }

        public List<borrowlend> GetBorrowLendAgainstAnEmpID()
        {

            var GetBorrows = db.borrowlends.Where(b => b.borrowLendStatusId < 4);
            List<borrowlend> list =GetBorrows.ToList();
            return list;
        }

        public borrowlend GetSingleBorrowLend(int? id)
        {
            borrowlend BL = db.borrowlends.Find( id);
            return BL;
        }

        public bool EditLendDetails(borrowlend Bl)
        {
            borrowlend BorrowLend = db.borrowlends.Find(Bl.borrowLendId);
            if (Bl.borrowLendStatusId == 1)
            {
                BorrowLend.borrowerName = Bl.borrowerName;
                BorrowLend.borrowLendStatusId = Bl.borrowLendStatusId;
                BorrowLend.branchAsSupplierId = Bl.branchAsSupplierId;
                BorrowLend.lendingDate = Bl.lendingDate;
                BorrowLend.lendingNumber = Bl.lendingNumber;
                db.SaveChanges();
                return true;
            }
            else
            {
                BorrowLend.borrowLendStatusId = Bl.borrowLendStatusId;
                db.SaveChanges();
                return true;
            }
        }
      
        public SelectList GetBorrowlendStatuses(int statusId)
        { 
        var lendingStatus=db.borrowlendstatuses.Where(b=>b.borrowLendStatusId>1);
        SelectList list =new SelectList(lendingStatus,"borrowLendStatusId","statusName",statusId);
        return list;
        }

        public List<SalesTableStructure> LendingItems(int? id)
        {
            List<SalesTableStructure> list=new List<SalesTableStructure>();
             SalesTableStructure Item = new SalesTableStructure();
            var sale = db.borrowlendstocks.Where(s => s.borrowLendId == id).Include(s => s.borrowstocks);
            foreach (var s in sale)
            {
                Item = new SalesTableStructure();
                var sold = s.borrowstocks.Where(t => t.borrowLendStockId == s.borrowLendStockId);
                Item.Product = s.productdetail.product.productName + " " + s.productdetail.productSize.ToString() + " " + s.productdetail.product.category.categoryUnit;
                Item.Quantity = s.quantityBorrowed;
                Item.UnitPrice = s.amount / s.quantityBorrowed;
                Item.Amount = s.amount;
                Item.SoldItemId = s.borrowLendStockId;                            
                list.Add(Item);
            }
            return list;
        }
        public string   SaveALendingItem(int _quantity,  int  _Lendid,int  _proDetID)
        {
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    borrowlendstock sold = new borrowlendstock();
                    borrowstock newsale = new borrowstock();
                    List<ReturnValuesForStock> idz = GetStockIdsWithPriceForProduct(_proDetID, _quantity);
                    foreach (ReturnValuesForStock id in idz)
                    {
                        sold = new borrowlendstock();
                        sold.amount = (id.Price * _quantity);
                        sold.quantityBorrowed = _quantity;
                        sold.borrowLendId = _Lendid;
                        sold.productDetailId = _proDetID;
                        var forsoldItem = db.borrowlendstocks.Add(sold);
                        db.SaveChanges();
                        newsale = new borrowstock();
                        newsale.borrowLendStockId = forsoldItem.borrowLendStockId;
                        newsale.stockId = id.Id;
                        db.borrowstocks.Add(newsale);
                        db.SaveChanges();
                    }
                    borrowlend LendItem = db.borrowlends.Find(_Lendid);
                    if (LendItem != null)
                    {
                        var TotalLend = db.borrowlendstocks.Where(s => s.borrowLendId == _Lendid).Select(s => s.amount).Sum();
                        LendItem.netAmount = TotalLend;
                    }
                    db.SaveChanges();
                    dbTransaction.Commit();
                    return "ok";
                }
                catch
                {
                    dbTransaction.Rollback();
                    return "not ok";
                }
            }
           
        }

        public bool  DeleteLendItem(int BorrowLendStockid)
        {
            ///////delete soldstok then sold item of respective  solditem ID        
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                  borrowlendstock SI = db.borrowlendstocks.Find(BorrowLendStockid);
                  string chk=GetBorrowedStatus(SI.borrowLendId);
                  if (chk == "Draft")
                  {
                      var LendStockItems = db.borrowstocks.Where(s => s.borrowLendStockId == BorrowLendStockid);
                      foreach (var s in LendStockItems)
                      {
                          borrowstock SS = db.borrowstocks.Find(s.borrowStockId);
                          db.borrowstocks.Remove(SS);
                          db.SaveChanges();
                      }
                      db.borrowlendstocks.Remove(SI);
                      db.SaveChanges();
                      borrowlend boLe = db.borrowlends.Find(SI.borrowLendId);
                      boLe.netAmount = db.borrowlendstocks.Where(b => b.borrowLendId == SI.borrowLendId).Sum(c => c.amount);
                      db.SaveChanges();                     
                      dbTransaction.Commit();
                      return true;
                  }
                  else
                  {
                      return false;
                  }
                }
                catch (Exception)
                {
                    dbTransaction.Rollback();
                    return false;
                }
            }        
        }

       public bool CancelBorrow(int id)
       {
           using (var dbTransaction = db.Database.BeginTransaction())
           {
               try
               {
                   List<int> LendItemIdz = new List<int>();
                   var LendItemsToDel = db.borrowlendstocks.Where(d => d.borrowLendId == id);
                   foreach (borrowlendstock s in LendItemsToDel)
                   {
                      LendItemIdz.Add(s.borrowLendStockId);
                   }
                   foreach (int i in LendItemIdz)
                   { ///find all the respective rows of  borrowedstock
                       var Lendstok = db.borrowstocks.Where(s => s.borrowLendStockId == i);
                       foreach (var Lend in Lendstok)
                       {//////deleting each borrowed row one by one
                           borrowstock SS = db.borrowstocks.Find(Lend.borrowStockId);
                           db.borrowstocks.Remove(SS);
                           db.SaveChanges();
                       }
                       ////delete Lend Item against id
                       borrowlendstock SI = db.borrowlendstocks.Find(i);
                       db.borrowlendstocks.Remove(SI);
                       db.SaveChanges();
                   }
                   ////////finaly delete  BorrowLend item
                   borrowlend borrow = db.borrowlends.Find(id);
                   db.borrowlends.Remove(borrow);
                   db.SaveChanges();

                   dbTransaction.Commit();
               }
               catch (Exception)
               {
                   dbTransaction.Rollback();
                   return false;
               }
           }
       return true;
       }

       public string  Borrow(int BorrowId)
       {
           string MessageReturn = "";
           bool AllOK = true;
           var LendItemsToBorrow = db.borrowlendstocks.Where(d => d.borrowLendId == BorrowId);
           foreach (borrowlendstock s in LendItemsToBorrow)
           {
               ReturnValuesForStock avalibleItems = GetNumberOfAvalibleProducts(s.productDetailId);
               if (avalibleItems.Count < s.quantityBorrowed)
               {
                   AllOK = false;
                   MessageReturn = MessageReturn + " product Name:" + s.productdetail.product.productName+" "+ s.productdetail.productSize.ToString()+" "+s.productdetail.product.category.categoryUnit + " Require Items are " + s.quantityBorrowed + "  avalible items are" + avalibleItems.Count + "." + Environment.NewLine;
               }
           }
           if (AllOK == true)
           {                ////do borrowing
               using (var dbTransaction = db.Database.BeginTransaction())
               {
                   try
                   {
                       List<TwoIntegers> LendItemIdz = new List<TwoIntegers>();////borrowlendstockidz
                      /// var salesItemsTosel = db.solditems.Where(d => d.salesId == salesID);
                       foreach (borrowlendstock s in LendItemsToBorrow)
                       {/////////////get all borrowitemz idz + quentity reqired
                           TwoIntegers t = new TwoIntegers();
                           t.Id = s.borrowLendStockId;
                           t.Value = (int)s.quantityBorrowed;
                           LendItemIdz.Add(t);
                       }
                       foreach (TwoIntegers values in LendItemIdz)
                       { ///find all the respective rows of  borrowstock
                           int itemRemaining = values.Value;
                           var borrowStok = db.borrowstocks.Where(s => s.borrowLendStockId == values.Id);
                           #region Single stock row
                           if (borrowStok.Count() == 1)
                           {
                               var sold1 = borrowStok.FirstOrDefault();
                               int stockid = sold1.stockId;
                               stock s = db.stocks.Find(stockid);
                               int productDetailID = s.productsupplied.productDetailId;
                               if ((s.quantity - s.itemSold) >= itemRemaining)
                               {
                                   s.itemSold += itemRemaining;
                                   db.SaveChanges();
                                   itemRemaining = 0;
                               }
                               else
                               {
                                   itemRemaining = itemRemaining - (int)(s.quantity - s.itemSold);
                                   s.itemSold = s.quantity;
                                   db.SaveChanges();
                                   borrowstock newsold = new borrowstock();
                                   /////// find  more stock rows
                                   List<int> list = GetStockIdsForProduct(productDetailID, itemRemaining);
                                   foreach (int id in list)
                                   {
                                       stock st = db.stocks.Find(id);
                                       if ((st.quantity - st.itemSold) >= itemRemaining)
                                       {
                                           st.itemSold += itemRemaining;
                                           db.SaveChanges();
                                           newsold = new borrowstock ();
                                           newsold.stockId = id;
                                           newsold.borrowLendStockId = values.Id;
                                           db.borrowstocks.Add(newsold);
                                           db.SaveChanges();
                                           itemRemaining = 0;
                                       }
                                       else
                                       {
                                           itemRemaining = itemRemaining - (int)(st.quantity - st.itemSold);
                                           st.itemSold = st.quantity;
                                           db.SaveChanges();
                                           newsold = new borrowstock();
                                           newsold.stockId = id;
                                           newsold.borrowLendStockId = values.Id;
                                           db.borrowstocks.Add(newsold);
                                           db.SaveChanges();
                                       }
                                   }
                               }
                           }
                           #endregion
                           else
                           {
                               int productDetailID = 0;
                               foreach (var sold in borrowStok)
                               {////// update each stock row one by one- for itemsold
                                   stock s = db.stocks.Find(sold.stockId);
                                   productDetailID = s.productsupplied.productDetailId;
                                   if ((s.quantity - s.itemSold) >= itemRemaining)
                                   {
                                       s.itemSold += itemRemaining;
                                       db.SaveChanges();
                                       itemRemaining = 0;
                                   }
                                   else
                                   {
                                       itemRemaining = itemRemaining - (int)(s.quantity - s.itemSold);
                                       s.itemSold = s.quantity;
                                       db.SaveChanges();
                                   }
                               }

                               #region rare case

                               if (itemRemaining != 0)
                               {
                                   borrowstock newsold = new borrowstock();
                                   /////// find  more stock rows
                                   List<int> list = GetStockIdsForProduct(productDetailID, itemRemaining);
                                   foreach (int id in list)
                                   {
                                       stock st = db.stocks.Find(id);
                                       if ((st.quantity - st.itemSold) >= itemRemaining && itemRemaining != 0)
                                       {
                                           st.itemSold += itemRemaining;
                                           db.SaveChanges();
                                           newsold = new borrowstock();
                                           newsold.stockId = id;
                                           newsold.borrowLendStockId = values.Id;
                                           db.borrowstocks.Add(newsold);
                                           db.SaveChanges();
                                           itemRemaining = 0;
                                       }
                                       else if (itemRemaining != 0)
                                       {
                                           itemRemaining = itemRemaining - (int)(st.quantity - st.itemSold);
                                           st.itemSold = st.quantity;
                                           db.SaveChanges();
                                           newsold = new borrowstock();
                                           newsold.stockId = id;
                                           newsold.borrowLendStockId = values.Id;
                                           db.borrowstocks.Add(newsold);
                                           db.SaveChanges();
                                       }
                                   }
                               }
                               #endregion
                           }
                       }
                       ////////finaly update  borrow item
                       borrowlend borrow = db.borrowlends.Find(BorrowId);
                       borrow.borrowLendStatusId = 2;
                       db.SaveChanges();
                       dbTransaction.Commit();
                       MessageReturn = "Done";
                   }
                   catch (Exception)
                   {
                       dbTransaction.Rollback();
                       MessageReturn = MessageReturn + "can not perform this Bill , Please check Your network Connection and try again!!!!!!!!!";
                       return MessageReturn;
                   }
               }
               return MessageReturn;
           }
           else
           { ////// no selling return with erro message
               return MessageReturn;

           }
       }
       
        public string GetBorrowedStatus(int? BorrowLendId)
       {
           try
           {
               borrowlend bl = db.borrowlends.Find(BorrowLendId);
               int Statusid = bl.borrowLendStatusId;
               borrowlendstatus BLS = db.borrowlendstatuses.Find(Statusid);
               string statusname = BLS.statusName;
               return statusname;
           }
           catch {
               return "Error";
           }
          


       }
      #endregion
       
        #region Reciving Side
        public double? GetNetRecivedAmountOfAnOrder(int? lendId)
        {
            int orderid = db.borrowlends.Find(lendId).orderId;
            var sum = db.stocks.Where(s => s.orderpartial.order.orderId == orderid).Select(s => s.PricePrItem * s.quantity).Sum();
            if (sum == null)
            {
                return 0;
            }
            return sum;
        }

       public int?  GetOrderPIdAgainstBorrowNumber(string BorrowLendnumber,int empId,DateTime date)
        {
            using (var dbTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var orderid = db.orders.Where(o => o.orderNumber == BorrowLendnumber).FirstOrDefault();
                    orderid.orderStatusId = 3;
                    db.SaveChanges();
                    orderpartial newPartial = new orderpartial();
                    newPartial.orderId = orderid.orderId;
                    newPartial.recevingDate = date;
                    newPartial.discription = "This is a return of the products which were lended to another branch";
                    newPartial.empId = empId;
                    var returnValues = db.orderpartials.Add(newPartial);
                    db.SaveChanges();
                    dbTransaction.Commit();
                    return returnValues.orderPartialsId;
                }
                catch
                {
                    dbTransaction.Rollback();
                    return null;
                }
            }
        }
      
       public string  SaveOrderItemOfBorrowReturned(OrderReciveStructure OrderRecive,int orderPId)
        {
            using (var dbTransaction = db.Database.BeginTransaction())
             {
                 try
                 {   stock stock = new stock();
                     stock.productSuppliedId = (int)OrderRecive.ProductSuppliedId;
                     stock.itemSold = 0;
                     stock.batchNO = OrderRecive.BatchNO;
                     stock.expiryDate = OrderRecive.ExpiryDateD;
                     stock.quantity = (int)OrderRecive.QuantityAcepted;
                     stock.sellingPricePrItem = OrderRecive.SellingPricePrItem;
                     stock.discountPercentage = OrderRecive.DiscountPercentage;
                     stock.orderPartialsId = orderPId;
                     stock.packSize = OrderRecive.PackSize;
                     stock.PricePrItem = OrderRecive.PricePrItem;
                     stock.quantityReceived = OrderRecive.QuantityRecived;
                     
                     db.stocks.Add(stock);
                     db.SaveChanges();
                     dbTransaction.Commit();
                     return "ok";
                 }
                 catch (Exception)
                 {
                     dbTransaction.Rollback();
                     return "Error";
                 }
             }       
        }
   
      public int  GetProductSuppliedIdAgaintSupplierAndProductDetail(int ProductDetailID, int  SupplierID)
    {
        var supplied = db.productsupplieds.Where(p => p.productDetailId == ProductDetailID && p.supplierId == SupplierID).FirstOrDefault();
        if (supplied != null)
        {
            return supplied.productSuppliedId;
        }
        else
        {
            productsupplied PS = new productsupplied();
            PS.productDetailId = ProductDetailID;
            PS.supplierId = SupplierID;
            var ProSup = db.productsupplieds.Add(PS);
            db.SaveChanges();
            return ProSup.productSuppliedId;
        }         
    }
   
        #endregion
    
        #region Return Part
      public bool CancelThisReturnBorrow(int returnId)
      {
          using (var dbTransaction = db.Database.BeginTransaction())
          {
              try
              {
                  var returnz = db.borrowreturnitems.Where(r => r.returnOfBorrowId == returnId);
                  foreach (borrowreturnitem r in returnz)
                  {
                      db.borrowreturnitems.Remove(r);
                      db.SaveChanges();
                  }
                  var BR = db.borrowreturns.Where(br => br.returnOfBorrowId == returnId);
                  foreach(borrowreturn r in BR)
                  {
                      db.borrowreturns.Remove(r);
                      db.SaveChanges();
                  }
                  var ret = db.returnofborrows.Find(returnId);
                  db.returnofborrows.Remove(ret);
                  db.SaveChanges();
                  dbTransaction.Commit();
                  return true;
              }
              catch
              {
                  dbTransaction.Rollback();
                  return false;
              }
          }
      }
      public int? GetBorrowIdOfBorrowNumber(string  Borrownumber)
      {
          var borrowID = db.borrowlends.Where(s => s.lendingNumber == Borrownumber).FirstOrDefault().borrowLendId;
          return borrowID;
      }
      public List<ReturnOfSalesStructure> BorrowItemsForReturn(int? borrowId)
      {
          List<ReturnOfSalesStructure> list = new List<ReturnOfSalesStructure>();
          ReturnOfSalesStructure Item = new ReturnOfSalesStructure();
          var borrows = db.borrowstocks.Where(s => s.borrowlendstock.borrowLendId == borrowId);
          foreach (var b in borrows)
          {
              Item = new ReturnOfSalesStructure();
              Item.QuantitySold = (int)b.borrowlendstock.quantityBorrowed;
              Item.AmountSold = (double)b.borrowlendstock.amount;
              Item.BatchNo = b.stock.batchNO;
              Item.ExpiryDate = b.stock.expiryDate;
              Item.SoldItemID = b.borrowLendStockId;
              Item.ProductName = GetProductNameForSoldItem(b.borrowlendstock.productDetailId);
              list.Add(Item);
          }
          return list;
      }
      public borrowlend GetBorrowDetails(int id)
      {
          return db.borrowlends.Find(id);
      }
      public int? GetANewBorrowReturnId(int borowID, int empId)
      {
          using (var dbTransaction = db.Database.BeginTransaction())
          {
              try
              {
                  returnofborrow r = new returnofborrow();
                  r.netAmountReturn = 0;
                  r.status = "Draft";
                  r.empId = empId;
                  r.date = DateTime.Now;
                  var newReturn = db.returnofborrows.Add(r);
                  db.SaveChanges();
                  borrowreturn br = new borrowreturn();
                  br.borrowLendId = borowID;
                  br.returnOfBorrowId = newReturn.returnOfBorrowId;
                  db.borrowreturns.Add(br);
                  db.SaveChanges();
                  dbTransaction.Commit();
                  return newReturn.returnOfBorrowId;
              }
              catch
              {
                  dbTransaction.Rollback();
                  return null;
              }
          }
      }
      public bool AddItemsDetailsOfReturnBorrow(int[] ids, int ReturnId)
      {
          using (var dbTransaction = db.Database.BeginTransaction())
          {
              try
              {
                  borrowreturnitem rItem = new borrowreturnitem();
                  foreach (int id in ids)
                  {
                      rItem = new borrowreturnitem();
                          rItem.returnOfBorrowId = ReturnId;
                          rItem.borrowLendStockId = id;
                          rItem.amount = 0;
                          rItem.quantityReturn = 0;
                          db.borrowreturnitems.Add(rItem);
                          db.SaveChanges();                      
                  }
                  dbTransaction.Commit();
                  return true;
              }
              catch
              {
                  dbTransaction.Rollback();
                  return false;
              }
          }
      }
      public List<ReturnOfSalesStructure> GetItemsDetailsOfReturnBorrow(int RetunOfBorrowId)
      {
          List<ReturnOfSalesStructure> list = new List<ReturnOfSalesStructure>();
          ReturnOfSalesStructure item = new ReturnOfSalesStructure();
          var returnz = db.borrowreturnitems.Where(i => i.returnOfBorrowId == RetunOfBorrowId).Include(s => s.borrowlendstock);
          foreach (var ret in returnz)
          {
              int OldQuantity = 0;
              item = new ReturnOfSalesStructure();
              item.ProductName = GetProductNameForSoldItem(ret.borrowlendstock.productDetailId);             
              item.AmountSold = (double)ret.borrowlendstock.amount;
              item.QuantityReturn = (int)ret.quantityReturn;
              item.AmountReturn = (int)ret.amount;
              item.SoldItemID = ret.borrowReturnItemsId;
              using(tobaccocastleEntities fdb=new tobaccocastleEntities()){
              var checkBRI =fdb.borrowreturnitems.Where(b=>b.borrowLendStockId==ret.borrowLendStockId);
              OldQuantity = 0;
              foreach (var chk in checkBRI)
              {
                  OldQuantity +=(int) chk.quantityReturn;
              }
              }              
              item.QuantitySold = (int)(ret.borrowlendstock.quantityBorrowed-OldQuantity);
              list.Add(item);
          }
          return list;
      }
      public bool SaveReturnBorrow(int BorrowReturnItemId, int QuantityReturend)
      {
          using (var dbTransaction = db.Database.BeginTransaction())
          {
              try
              {
                  borrowreturnitem ret = db.borrowreturnitems.Find(BorrowReturnItemId);
                  double oldAmount = (double)ret.amount;
                  ret.quantityReturn = QuantityReturend;
                  ret.amount = ((ret.borrowlendstock.amount / ret.borrowlendstock.quantityBorrowed) * QuantityReturend);
                  db.SaveChanges();
                  returnofborrow rOfborow = db.returnofborrows.Find(ret.returnOfBorrowId);
                  var Totalreturn = db.borrowreturnitems.Where(s => s.returnOfBorrowId == ret.returnOfBorrowId).Select(s => s.amount).Sum();
                  rOfborow.netAmountReturn = Totalreturn;
                  db.SaveChanges();
                  dbTransaction.Commit();
                  return true;
              }
              catch
              {
                  dbTransaction.Rollback();
                  return false;
              }
          }
      }
      public bool MakeReturnborrow(List<int> idz)
      {
          try
          {
              foreach (int id in idz)
              {                 
                  borrowreturnitem ret = db.borrowreturnitems.Find(id);
                  
                  if (ret.quantityReturn == 0)
                  {
                      db.borrowreturnitems.Remove(ret);
                      db.SaveChanges();
                  }
                  else
                  {
                      var sold = db.borrowstocks.Where(s => s.borrowLendStockId == ret.borrowLendStockId).OrderBy(b=>b.stock.expiryDate);
                       int? QR = ret.quantityReturn;
                      foreach(var s in sold)
                      {
                          if ((s.stock.quantity - s.stock.itemSold) > QR)
                          {
                              s.stock.itemSold -= QR;
                              db.SaveChanges();
                              QR = 0;
                              break;
                          }
                          else {
                              s.stock.itemSold = 0;
                              QR -= (s.stock.quantity - s.stock.itemSold);
                              db.SaveChanges();    
                          }                                             
                       }                   
                  }
              }
              return true;
          }
          catch
          {
              return false;
          }
      }
        public bool MakeBorrowReturn(int returnId)
      {
          using (var dbTransaction = db.Database.BeginTransaction())
          {
              try
              {
                  var retrunidz = db.borrowreturnitems.Where(r => r.returnOfBorrowId == returnId).Select(b=>b.borrowReturnItemsId).ToList();
                  bool check = MakeReturnborrow(retrunidz);
                  if (check == true)
                  {
                      var ItemsCheck = db.borrowreturnitems.Where(r => r.returnOfBorrowId == returnId).Count();
                      if (ItemsCheck > 0)
                      {
                          returnofborrow ret = db.returnofborrows.Find(returnId);
                          ret.status= "Done";
                          db.SaveChanges();
                      }
                      else
                      {
                          returnofborrow ret = db.returnofborrows.Find(returnId);
                          db.returnofborrows.Remove(ret);
                          db.SaveChanges();
                      }
                  }
                  else
                  {
                      dbTransaction.Rollback();
                      return false;
                  }

                  dbTransaction.Commit();
                  return true;
              }
              catch
              {
                  dbTransaction.Rollback();
                  return false;
              }
          }
      }
        #endregion 

        #region reporting
        public List<borrowlend> GetBorrowsofADate(DateTime date)
        {
            DateTime plusday = date.AddDays(1);
            return db.borrowlends.Where(s => s.lendingDate >= date && s.lendingDate < plusday && s.borrowlendstatus.statusName != "Draft").ToList();
        }
        public List<borrowlend> GetBorrowsofTwoDates(DateTime FromDate,DateTime ToDate)
        {
            return db.borrowlends.Where(s => s.lendingDate >= FromDate && s.lendingDate < ToDate && s.borrowlendstatus.statusName != "Draft").ToList();
        }
        public List<returnofborrow> GetReturnBorrowsofADate(DateTime date)
        {
            DateTime plusday = date.AddDays(1);
            //var xyz= db.borrowreturnitems.Where(s => s.returnofborrow.date >= date && s.returnofborrow.date < plusday && s.returnofborrow.status != "Draft").ToList();
            //foreach (var x in xyz)
            //{
            //   string s= x.borrowlendstock.productdetail.product.productName;
            //   int? q = x.quantityReturn;
            //   string bn = x.borrowlendstock.borrowlend.lendingNumber;
            //   DateTime rd = x.returnofborrow.date;
            ////
            //   // DateTime xd=x.borrowlendstock.borrowstocks.
            //}
            return db.returnofborrows.Where(s => s.date >= date && s.date < plusday && s.status != "Draft").ToList();
        }
        #endregion 
      #endregion

      #region Stock Reporting
        public string updatePriceInStock(int proDetailId,Double NewPriceSelling)
        {
            try
            {
                int noOfRowUpdated = db.Database.ExecuteSqlCommand("UPDATE stock S " +
            " JOIN  productsupplied PS ON S.productSuppliedId = PS.productSuppliedId " +
            " JOIN productdetails Pde ON PS.productDetailId = Pde.productDetailId " +
            " SET  S.sellingPricePrItem = " + NewPriceSelling +
            " where  S.quantity > S.itemsold " +
            " and DATEDIFF(S.expirydate,CURDATE()) > Pde.alertBFExpiryDays  and Pde.productDetailId = " + proDetailId + " ;");
                return "Price Updated";
            }
            catch {
                return "Please try again!!!";
            }
        }
     public ReturnValuesForStock GetStockItemInfo(int ProductDetailId)
        {
            ReturnValuesForStock r = new ReturnValuesForStock();
            try {
                var o = db.Database.SqlQuery<ReturnValuesForStock>(" SELECT   sum(S.quantity-S.itemsold) as Count, " +
                                      "max(S.sellingPricePrItem) as Price " +
                                      " from  stock S  " +
                                      " Join productsupplied PS on S.productSuppliedId = PS.productSuppliedId " +
                                      " join productdetails Pde on PS.productDetailId=Pde.productDetailId " +
                                      "  where   S.quantity > S.itemsold " +
                                      " and DATEDIFF(S.expirydate,CURDATE()) > Pde.alertBFExpiryDays  " +
                                      "  and PS.productDetailId =" + ProductDetailId + " ; ").ToList();               
                 
                foreach (var i in o)
                {
                    r.Count = i.Count;
                    r.Price = i.Price;
                    break;
                }
                return r;
            }
            catch {
                r.Count = 0;
                r.Price = 0.0;
                return r;                
            }
        }
     public string  DeletItemFromStock(int? id)
        {
            stock stk = db.stocks.Find(id);
            if (stk.itemSold == 0)
            {
                db.stocks.Remove(stk);
                db.SaveChanges();
                return "Done";
            }
            return "Can not delete this item.one or more items are sold.";
        }

     public List<IdAndSum> GetCurrentStock()
      {
          List<IdAndSum> stockInventry = db.Database.SqlQuery<IdAndSum>("SELECT sum(S.quantity-S.itemsold) as Sum , sum( (S.quantity-S.itemsold) * pricePrItem ) as PurchaseSum ,  Pde.productSize as Size , Pro.productName as Name," +
                                                                        "Cat.categoryName as Category  ,  Cat.categoryUnit as Unit "+
                                                                        "FROM  stock S  JOIN productsupplied Psup ON S.productSuppliedId = Psup.productSuppliedId "+
                                                                        "JOIN productdetails Pde ON Psup.productDetailId = Pde.productDetailId "+
                                                                        "JOIN product Pro ON Pde.productId = Pro.productId  JOIN category Cat  ON Pro.categoryId = Cat.categoryId "+
                                                                        " WHERE S.quantity >= S.itemsold and DATEDIFF(S.expirydate,CURDATE()) >= Pde.alertBFExpiryDays group by Pde.productDetailId order by Pro.productName;").ToList();
          
          return stockInventry;
      }

     public List<StockType> GetAvalibleStockRows()
     {
         List<StockType> list = new List<StockType>();
         var xyz= db.Database.SqlQuery<StockType>(" SELECT Pro.productName as Name, "+
                                                   " Pde.productSize as Size , "+
                                                   " Cat.categoryUnit as Unit, "+
                                                   "  Cat.categoryName as Category  ,"+
                                                   " S.quantity as Quantity, "+
                                                   " S.itemsold as ItemSold  , "+
                                                   " S.stockId as StockId, "+
                                                   " S.expiryDate as Expiry, "+
                                                   " S.packSize as PackSize, "+
                                                   " S.discountPercentage as Disc, "+
                                                   " S.PricePrItem as PriceRecived, "+
                                                   " S.sellingPricePrItem as SellingPrice "+
                                                   " FROM  stock S  JOIN productsupplied Psup ON S.productSuppliedId = Psup.productSuppliedId "+
                                                   " JOIN productdetails Pde ON Psup.productDetailId = Pde.productDetailId  "+
                                                   " JOIN product Pro ON Pde.productId = Pro.productId   "+
                                                   " JOIN category Cat  ON Pro.categoryId = Cat.categoryId "+
                                                   " WHERE S.quantity > S.itemsold and DATEDIFF(S.expirydate,CURDATE()) > Pde.alertBFExpiryDays ;");
         list = xyz.OrderBy(s=>s.Name).ToList();
         return list;
     }

     public stock GetStockItem(int StockId)
     {
         return db.stocks.Find(StockId);
     }
     public bool EditStockItem(stock stock, string username)
     {
         try
         {
             stock st = db.stocks.Find(stock.stockId);
             string discription = "";
             discription = "Old Data:Expiry Date= " + st.expiryDate + ", Quantity= " + st.quantity + ", pricePrItem= " + st.PricePrItem + ", sellingPricePrItem= " + st.sellingPricePrItem + "; new Data:Expiry Date= " + stock.expiryDate + ", Quantity= " + stock.quantity + ", pricePrItem= " + stock.PricePrItem + ", sellingPricePrItem= " + stock.sellingPricePrItem + "; ";
             st.quantity = stock.quantity;
             st.expiryDate = stock.expiryDate;
             st.PricePrItem = stock.PricePrItem;
             st.sellingPricePrItem = stock.sellingPricePrItem;
             db.SaveChanges();
            
             pharmacylog pl = new pharmacylog();
             pl.actionPerformed = "Edit";
             pl.date = DateTime.Now;
             pl.tableName = "stock";
             pl.userName = username;
             pl.Discription = discription;
             db.pharmacylogs.Add(pl);
             db.SaveChanges();
             return true;
         }
         
         catch
         {
           return  false;
         }
     }
      #endregion

      #region Old Functions
      public SelectList SearchProduct2(string SearchKey)
      {
          List<ProductListWithSizeAndCategory> Productlist = new List<ProductListWithSizeAndCategory>();
          var products = db.productdetails.Where(p => p.product.productName.Contains("" + SearchKey));
          ProductListWithSizeAndCategory product = new ProductListWithSizeAndCategory();
          foreach (var p in products)
          {
              product = new ProductListWithSizeAndCategory();
              product.DetailProductName = GetProductNameForSoldItem(p.productDetailId);
              product.Id = p.productDetailId;
              Productlist.Add(product);
          }
          SelectList list = new SelectList(Productlist, "Id", "DetailProductName");
          return list;
      }
      
      #endregion
      
      
    }
}


/*
  if (ProExist != null)
                        {
                           var prdExist= db.productdetails.Where(l => l.productId == ProExist.productId).FirstOrDefault();
                           if (prdExist != null)
                           {
                               var prosupliz = db.productsupplieds.Where(i => i.productDetailId == prdExist.productDetailId);
                               #region Supplier part in case category not changed
                               if (suppliers != null)
                               {
                                   foreach (int sid in suppliers)
                                   {
                                       if (prosupliz != null)
                                       {
                                           if (prosupliz.Where(w => w.supplierId == sid).FirstOrDefault() == null)
                                           {
                                               productsupplied newps = new productsupplied();
                                               newps.supplierId = sid;
                                               newps.productDetailId = ProdDet.productDetailId;
                                               db.productsupplieds.Add(newps);
                                               db.SaveChanges();
                                           }
                                       }
                                       else
                                       {
                                           productsupplied newps = new productsupplied();
                                           newps.supplierId = sid;
                                           newps.productDetailId = ProdDet.productDetailId;
                                           db.productsupplieds.Add(newps);
                                           db.SaveChanges();
                                       }
                                   }
                               }
                               #endregion
                           }
                           else
                           {   productdetail npdn = new productdetail();
                               npdn.productSize = ProdDet.productSize;
                               npdn.product.productName = ProdDet.product.productName;
                               npdn.product.categoryId = ProdDet.product.categoryId;
                               npdn.thresholdLevel = ProdDet.thresholdLevel;
                               npdn.alertBFExpiryDays = ProdDet.alertBFExpiryDays;
                               if ( ProdDet.barcode != null)
                               {//ProdDet.barcode.Trim() != ""
                                   npdn.barcode = ProdDet.barcode.Trim();
                               }
                               var createNewProDel = db.productdetails.Add(npdn);
                               db.SaveChanges();
                               if (suppliers != null) {
                                   foreach (int sid in suppliers)
                                   {
                                       productsupplied newps = new productsupplied();
                                       newps.supplierId = sid;
                                       newps.productDetailId = createNewProDel.productDetailId;
                                       db.productsupplieds.Add(newps);
                                       db.SaveChanges();
                                   }
                               }                               
                           }
                        }
                        else {
                            product p = new product();
                            p.categoryId = ProdDet.product.categoryId;
                            p.productName = ProdDet.product.productName;
                            var newpro = db.products.Add(p);
                            db.SaveChanges();
                            productdetail pd = new productdetail();
                            pd.productId = newpro.productId;
                            pd.productSize = ProdDet.productSize;
                            pd.alertBFExpiryDays = ProdDet.alertBFExpiryDays;
                            pd.thresholdLevel = ProdDet.thresholdLevel;
                            if (ProdDet.barcode != null)
                            {
                                pd.barcode = ProdDet.barcode.Trim();
                            }
                            var newpd = db.productdetails.Add(pd);
                            db.SaveChanges();
                            var pros = db.productsupplieds.Where(x => x.productDetailId == ProdDet.productDetailId);
                            if (pros != null)
                            {
                                foreach (productsupplied s in pros)
                                {
                                    db.productsupplieds.Remove(s);
                                    db.SaveChanges();
                                }
                            }
                            if (suppliers != null)
                            {
                                foreach (int sid in suppliers)
                                {
                                    productsupplied nps = new productsupplied();
                                    nps.supplierId = sid;
                                    nps.productDetailId = newpd.productDetailId;
                                    db.productsupplieds.Add(nps);
                                    db.SaveChanges();
                                }
                            }
                        }                       
                        db.productdetails.Remove(prod);
                        db.SaveChanges();                  
 */