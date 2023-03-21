using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Evaluation;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using PetSite.Models;
using PetSite.Models.DTOs;
using PetSite.Models.EFModels;
using PetSite.Models.Repositories;
using PetSite.Models.ViewModels;
using System.Linq;
using System.Xml.Linq;


namespace PetSite.Controllers
{
	public class PurchasesController : Controller
	{
		private readonly PetSiteContext _context;

		public PurchasesController(PetSiteContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			var data = new PurchasesRepository(_context).GetAll();
			return View(data);
		}

		public IActionResult Test()
		{
			//var data = _context.Products.Select(x => x.Name).Distinct().ToList();
			//var dataAll = _context.Products.ToList();
			//新增按鈕用的
			ViewData["ProductDB"] = _context.Products.Select(x => new Product { Name = x.Name }).Distinct().ToList();
			ViewData["SizeDB"] = _context.Sizes.Select(x => new Size { SizeId = x.SizeId, Name = x.Name }).ToList();
			ViewData["ColorDB"] = _context.Colors.Select(x => new Color { ColorId = x.ColorId, Name = x.Name }).ToList();
			ViewData["FlavorDB"] = _context.Flavors.Select(x => new Flavor { FlavorId = x.FlavorId, Name = x.Name }).ToList();

			//現有按鈕用的
			ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierName");
			ViewData["Product"] = new SelectList(_context.Products.Select(x => new Product { Name = x.Name }).Distinct().ToList(), "Name", "Name");
			ViewData["Size"] = new SelectList(_context.Sizes, "SizeId", "Name");
			ViewData["Color"] = new SelectList(_context.Colors, "ColorId", "Name");
			ViewData["Flavor"] = new SelectList(_context.Flavors, "FlavorId", "Name");
			//ViewData["Color"] = new 
			return View();
		}
		[HttpPost]
		public IActionResult Test([FromForm] PurchaseItemVM model)
		{
			//var dataa = model.PurchaseItemCreateList[0];

			var d = _context.Products.Where(x => x.Name == model.ProductName[0] && x.ColorId == Convert.ToInt32(model.Color[0])
														  && x.SizeId == Convert.ToInt32(model.Size[0])
														  && x.FlavorId == Convert.ToInt32(model.Flavor[0]))
															 .Select(x => x.ProductId).FirstOrDefault(); ;

			//新增一筆訂單
			var data = new PurchaseCreateDto
			{
				SupplierId = Convert.ToInt32(model.SupplierName),
				Status = false,
				Total = 0,
				CreateDate = DateTime.Now,
			};
			var dataDB = new Purchase
			{
				SupplierId = data.SupplierId,
				Status = data.Status,
				Total = data.Total,
				CreateDate = data.CreateDate,
			};
			_context.Purchases.Add(dataDB);
			_context.SaveChanges();
			//新增訂單明細

			//抓最新的訂單Id(剛剛成立的訂單)
			var purchasesId = _context.Purchases.Max(x => x.PurchaseId);

			//新增每筆訂單明細
			for (int i = 0; i < model.Color.Count; i++)
			{
				//找出訂購了哪個產品
				var productsId = _context.Products.Where(x => x.Name == model.ProductName[i] && x.ColorId == Convert.ToInt32(model.Color[i])
														  && x.SizeId == Convert.ToInt32(model.Size[i])
														  && x.FlavorId == Convert.ToInt32(model.Flavor[i]))
															 .Select(x => x.ProductId).FirstOrDefault();
				//新增每筆訂單明細
				var purchaseItem = new PurchaseItem
				{
					PurchaseId = purchasesId,
					ProductId = productsId,
					Price = model.Price[i],
					Qty = model.Qty[i],
					Total = model.Price[i] * model.Qty[i],
				};
				_context.PurchaseItems.Add(purchaseItem);
				_context.SaveChanges();
			}
			//foreach (var item in model)
			//{
			//	//找出訂購了哪個產品
			//	var productsId = _context.Products.Where(x => x.ColorId == Convert.ToInt32(item.Color)
			//											  && x.SizeId == Convert.ToInt32(item.Size)
			//											  && x.FlavorId == Convert.ToInt32(item.Flavor))
			//												 .Select(x => x.ProductId).FirstOrDefault();

			//	//新增每筆訂單明細
			//	var purchaseItem = new PurchaseItem
			//	{
			//		PurchaseId = purchasesId,
			//		ProductId = productsId,
			//		Price = item.Price,
			//		Qty = item.Qty,
			//		Total = item.Price * item.Qty,
			//	};
			//	_context.PurchaseItems.Add(purchaseItem);
			//	_context.SaveChanges();
			//}

			//把每筆訂單明細的Total加起來
			var purchasesTotal = _context.PurchaseItems.Where(x => x.PurchaseId == purchasesId).Sum(x => x.Total);
			//修改採購單的Total
			var updatePurchasesTotal = _context.Purchases.Find(purchasesId);
			updatePurchasesTotal.Total = purchasesTotal;
			_context.SaveChanges();

			return RedirectToAction(nameof(Index));
		}
		[Route("/Purchases/Test/Json")]
		public IActionResult IndexJson()
		{

			var productsAll = _context.Products.Select(x => new Product { Name = x.Name }).Distinct().ToList();
			var colorsAll = _context.Colors.Select(x => new Color { ColorId = x.ColorId, Name = x.Name }).ToList();
			var sizesAll = _context.Sizes.Select(x => new Size { SizeId = x.SizeId, Name = x.Name }).ToList();
			var flavorsAll = _context.Flavors.Select(x => new Flavor { FlavorId = x.FlavorId, Name = x.Name }).ToList();
			var data = new SelectVM
			{
				ProductItem = productsAll,
				ColorItem = colorsAll,
				SizeItem = sizesAll,
				FlavorItem = flavorsAll,
			};
			return Json(data);
		}
		public IActionResult Edit(int id)
		{
			//如果採購單已入庫return
			var purchaseStatus = _context.Purchases.Where(x => x.PurchaseId == id).Select(x => x.Status).FirstOrDefault();
			if (purchaseStatus == true)
			{
				return RedirectToAction("Index");
			}

			var selectlistProductName = _context.Products.Select(x => new Product { Name = x.Name }).Distinct().ToList();
			//取得採購單Id為id(帶入的參數)的明細內的所有產品名字,大小id 和名字,顏色id 和名字,口味id 和名字,價格,數量
			var dataSelectList = _context.PurchaseItems
				.Where(x => x.PurchaseId == id)
				.Include(x => x.Purchase)
				.Include(x => x.Product)
				.ThenInclude(x => x.Color)
				.Include(x => x.Product)
				.ThenInclude(x => x.Size)
				.Include(x => x.Product)
				.ThenInclude(x => x.Flavor)
				.Select(x => new PurchaseItemEditSelectVM
				{
					ProductName = x.Product.Name,
					SizeId = x.Product.SizeId,
					SizeName = x.Product.Size.Name,
					ColorId = x.Product.ColorId,
					ColorName = x.Product.Color.Name,
					FlavorId = x.Product.FlavorId,
					FlavorName = x.Product.Flavor.Name,
					Price = x.Price,
					Qty = x.Qty
				})
				.ToList();
			var selectProductBag = new List<SelectList>();
			var selectSizeBag = new List<SelectList>();
			var selectColorBag = new List<SelectList>();
			var selectFlavorBag = new List<SelectList>();
			var inputPriceBag = new List<decimal>();
			var inputQtyBag = new List<int>();
			var deleteButtonId = new List<string>();
			var purchasesItemId = new List<int>();//訂單明細Id
			int deleteId = 1;
			int supplierId = _context.Purchases.Where(x => x.PurchaseId == id).Select(x => x.Supplier.SupplierId).FirstOrDefault();
			purchasesItemId = _context.PurchaseItems.Where(x => x.PurchaseId == id).Select(x => x.PurchasesItemId).ToList();

			//把所有下拉式選單和預設值配置好
			foreach (var i in dataSelectList)
			{
				new InputCheckbox();
				selectProductBag.Add(new SelectList(selectlistProductName, "Name", "Name", i.ProductName));
				selectSizeBag.Add(new SelectList(_context.Sizes, "SizeId", "Name", i.SizeId));
				selectColorBag.Add(new SelectList(_context.Colors, "ColorId", "Name", i.ColorId));
				selectFlavorBag.Add(new SelectList(_context.Flavors, "FlavorId", "Name", i.FlavorId));
				inputPriceBag.Add(i.Price);
				inputQtyBag.Add(i.Qty);
				deleteButtonId.Add($"Delete{deleteId}");
				deleteId++;
			}
			int index = dataSelectList.Count();
			ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierName", supplierId);
			ViewData["ProductBag"] = selectProductBag;
			ViewData["SizeBag"] = selectSizeBag;
			ViewData["ColorBag"] = selectColorBag;
			ViewData["FlavorBag"] = selectFlavorBag;
			ViewData["PriceBag"] = inputPriceBag;
			ViewData["QtyBag"] = inputQtyBag;
			ViewData["index"] = index;//總共有幾個ProductItem
			ViewData["deleteButtonId"] = deleteButtonId;
			ViewData["purchasesItemId"] = purchasesItemId;
			ViewData["purchasesId"] = id;
			return View();
		}
		[HttpPost]
		public IActionResult Edit([FromForm] PurchaseItemEditVM model)
		{

			var dataBaseId = _context.PurchaseItems.Where(x => x.PurchaseId == model.PurchaseId).Select(x => x.PurchasesItemId).ToList();
			var orgPurchaseItem = _context.PurchaseItems.Where(x => x.PurchaseId == model.PurchaseId).Select(x => x.PurchasesItemId).ToList();
			//如果原始資料不存在輸入的 model 則刪除
			for (int i = 0; i < orgPurchaseItem.Count(); i++)
			{
				if (model.Id.Any(x => x == orgPurchaseItem[i]) == false)
				{
					var purchaseItem = _context.PurchaseItems.Find(orgPurchaseItem[i]);
					if (purchaseItem == null)
					{
						continue;
					}
					_context.PurchaseItems.Remove(purchaseItem);
					_context.SaveChanges();
				}
			}
			//新增或修改
			for (int i = 0; i < model.Id.Count; i++)
			{
				var productsId = _context.Products.Where(x => x.Name == model.ProductName[i] && x.ColorId == Convert.ToInt32(model.Color[i])
														  && x.SizeId == Convert.ToInt32(model.Size[i])
														  && x.FlavorId == Convert.ToInt32(model.Flavor[i]))
															 .Select(x => x.ProductId).FirstOrDefault();


				//model.id不是0的要修改(如果 Pk 值存在於原始資料則更新資料)
				if (model.Id[i] != 0)
				{
					//找到那筆訂單明細id修改
					var purchaseItem = _context.PurchaseItems.Find(model.Id[i]);
					if (purchaseItem == null)
					{
						continue;
					}
					purchaseItem.ProductId = productsId;
					purchaseItem.Price = model.Price[i];
					purchaseItem.Qty = model.Qty[i];
					purchaseItem.Total = model.Price[i] * model.Qty[i];
					_context.SaveChanges();
					continue;
				}
				//判斷id是0的就新增(如果 Pk 欄位為空值則新增)
				var purchasesItem = new PurchaseItem
				{
					PurchaseId = model.PurchaseId,
					ProductId = productsId,
					Price = model.Price[i],
					Qty = model.Qty[i],
					Total = model.Price[i] * model.Qty[i],
				};
				_context.PurchaseItems.Add(purchasesItem);
				_context.SaveChanges();
			}

			//總total的價格
			var purchaseTotal = _context.PurchaseItems.Where(x => x.PurchaseId == model.PurchaseId).Sum(x => x.Total);
			//修改採購單的Total
			var updatePurchasesTotal = _context.Purchases.Find(model.PurchaseId);
			updatePurchasesTotal.Total = purchaseTotal;
			_context.SaveChanges();

			return RedirectToAction(nameof(Index));
		}
		public IActionResult Delete(int id)
		{
			var data = _context.Purchases.Where(x => x.PurchaseId == id).Include(x => x.Supplier).SingleOrDefault();

			return View(data);
		}

		[HttpPost]
		public IActionResult Delete(Purchase model)
		{
			var data = _context.Purchases.SingleOrDefault(x => x.PurchaseId == model.PurchaseId);
			_context.Purchases.Remove(data);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult Storage(int id)
		{
			var status = _context.Purchases.Where(x => x.PurchaseId == id).Select(x => x.Status).FirstOrDefault();
			if (status == true)
			{
				return RedirectToAction(nameof(Index));
			}
			//找到要入庫的明細
			var data = _context.PurchaseItems
				.Where(x => x.PurchaseId == id).ToList();
			//修改產品庫存
			for (int i = 0; i < data.Count(); i++)
			{
				var product = _context.Products.Where(x => x.ProductId == data[i].ProductId).FirstOrDefault();
				var productQty = product.Quantity + data[i].Qty;
				product.Quantity = productQty;
				_context.SaveChanges();
			}
			//把採購單狀態改為true
			var purchase = _context.Purchases.Where(x => x.PurchaseId == id).FirstOrDefault();
			purchase.Status = true;
			_context.SaveChanges();
			return RedirectToAction("Index", "inventory");
		}
		[HttpPost, ActionName("Storage")]
		public IActionResult StorageHttp(int id)
		{
			var data = _context.PurchaseItems
				.Where(x => x.PurchaseId == id).ToList();
			var a = 0;
			return View(data);
		}
	}
}
