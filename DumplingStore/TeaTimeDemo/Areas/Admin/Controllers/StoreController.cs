using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System.Data;
using DumplingStore.DataAccess;
using DumplingStore.DataAccess.Data;
using DumplingStore.DataAccess.Repository;
using DumplingStore.DataAccess.Repository.IRepository;
using DumplingStore.Models;
using DumplingStore.Models.ViewModels;
using DumplingStore.Utility;
using static System.Net.Mime.MediaTypeNames;

namespace DumplingStore.Areas.Admin.Controllers//DumplingStore.DataAccess.DataAccess.Controllers
{
    [Area("Admin")]
   // [Authorize(Roles = SD.Role_Admin)]
    public class StoreController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public StoreController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Store> objStoreList = _unitOfWork.Store.GetAll().ToList();
            return View(objStoreList);
        }

        public IActionResult Upsert(int? id)
        {
           
         
            if(id == null || id == 0)
            {
                return View(new Store());
            }
            else
            {
                // 執行編輯
                Store storeObj = _unitOfWork.Store.Get(u => u.Id == id);
                return View();
            }
            
        }
        [HttpPost]
        public IActionResult Upsert(Store storeObj)
        {
           

            if (ModelState.IsValid)
            {
                if (storeObj.Id == 0)
                {
                    _unitOfWork.Store.Add(storeObj);

                    
                }
                else
                {
                    _unitOfWork.Store.Update(storeObj);
                }
                _unitOfWork.Save();
                TempData["success"] = "店鋪新增成功!";
                return RedirectToAction("Index");
                
            }
            else
            {
                return View(storeObj);
            }
         
        }

       /* public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? ProductFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            if (ProductFromDb == null)
            {
                return NotFound();
            }
            return View(ProductFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Product obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "類別新增成功!";
                return RedirectToAction("Index");
            }
            return View();
        }*/

        /*public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product categoryFromDb = _unitOfWork.Product.Get(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product? obj = _unitOfWork.Product.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "類別刪除成功!";
            return RedirectToAction("Index");
        }*/
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Store> objStoreList = _unitOfWork.Store.GetAll().ToList();
            return Json(new{data = objStoreList });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var StoreToBeDeleted = _unitOfWork.Store.Get(u => u.Id == id);
            if (StoreToBeDeleted == null)
            {
                return Json(new{success = false, message = "刪除失敗"});

            }
           
            _unitOfWork.Store.Remove(StoreToBeDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "刪除成功" });
        }
    #endregion

}
}
