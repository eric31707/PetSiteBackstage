using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using PetSite.Models;
using PetSite.Models.DTOs;
using PetSite.Models.EFModels;
using PetSite.Models.ViewModels;
using X.PagedList;

namespace PetSite.Controllers.Hotel
{
    public class RoomsController : Controller
    {
        private readonly PetSiteContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RoomsController(PetSiteContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }


        //[Route("Pages/Razor")]
        //public async Task<IActionResult> Index(int? page,int roomId)
        //{
        //    var rooms = await _context.Rooms.ToListAsync();
        //    var roomvm = rooms.Select(x => x.ToEntity()).ToList();
        //    foreach (var n in roomvm)
        //    {
        //        var roompics = _context.RoomImages.Where(x => x.RoomId == n.RoomId).ToList();
        //        n.fileName = roompics.Select(x => x.Image).ToArray();
        //    }

        //    var pageSize = 5;
        //    var pageNumber = page ?? 1;
        //    var list=rooms.OrderBy(x=>x.RoomId);

        //    IPagedList<Room> pagedList = list.ToPagedList(pageNumber, pageSize);                  
        //    return View(pagedList);

        //}


        //GET: Rooms
        const int pageSize = 3;  //每頁顯示幾筆資料
        public async Task<IActionResult> Index(string searchString,int? page = 1)
        {
            var rooms =  _context.Rooms.ToList();
            var roomvm = rooms.Select(x => x.ToEntity()).ToList();
            foreach (var n in roomvm)
            {
                var roompics = _context.RoomImages.Where(x => x.RoomId == n.RoomId).ToList();
                n.fileName = roompics.Select(x => x.Image).ToArray();
            }     
            
            if (roomvm == null)
            {
                return Problem("查無此房間");
            }
        
            if (!String.IsNullOrEmpty(searchString))
            {
                roomvm = roomvm.Where(s => s.Address.Contains(searchString)|| s.Name.Contains(searchString)|| s.Price.ToString().Contains(searchString)|| s.Type.Contains(searchString)).ToList();
            }

            ViewBag.RoomVM = GetPagedRooms(page, pageSize, roomvm);

            return View(await roomvm.Skip<RoomVM>(pageSize * ((page ?? 1) - 1)).Take(pageSize).ToListAsync());

        }

        public async Task<IActionResult> IndexPartial(string searchString, int? page)
        {
            var rooms = _context.Rooms.ToList();
            var roomvm = rooms.Select(x => x.ToEntity()).ToList();
            foreach (var n in roomvm)
            {
                var roompics = _context.RoomImages.Where(x => x.RoomId == n.RoomId).ToList();
                n.fileName = roompics.Select(x => x.Image).ToArray();
            }

            if (roomvm == null)
            {
                return Problem("查無此房間");
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                roomvm = roomvm.Where(s => s.Address.Contains(searchString) || s.Name.Contains(searchString) || s.Price.ToString().Contains(searchString) || s.Type.Contains(searchString)).ToList();
            }

            ViewBag.RoomVM = GetPagedRooms(page, pageSize, roomvm);

            return PartialView(await roomvm.Skip<RoomVM>(pageSize * ((page ?? 1) - 1)).Take(pageSize).ToListAsync());

        }



        //public async Task<IActionResult> Index(string RoomId, int page = 1, int pageSize = 2)
        //{

        //    var rooms = await _context.Rooms.ToListAsync();
        //    var roomvm = rooms.Select(x => x.ToEntity()).ToList();
        //    foreach (var n in roomvm)
        //    {
        //        var roompics = _context.RoomImages.Where(x => x.RoomId == n.RoomId).ToList();
        //        n.fileName = roompics.Select(x => x.Image).ToArray();
        //    }
        //    ViewBag.RoomId = rooms.Select(x => x.RoomId).ToList();
        //    return View(rooms.ToPagedList(page, pageSize));

        //}


        protected IPagedList<RoomVM> GetPagedRooms(int? page, int pageSize, List<RoomVM> roomvm)
        {
            // 過濾從client傳送過來有問題頁數
            if (page.HasValue && page < 1)
                return null;
            // 從資料庫取得資料
            var listUnpaged =GetStuffFromDatabase(roomvm);
            IPagedList<RoomVM> pagelist = listUnpaged.ToPagedList(page ?? 1, pageSize);
            // 過濾從client傳送過來有問題頁數，包含判斷有問題的頁數邏輯
            if (pagelist.PageNumber != 1 && page.HasValue && page > pagelist.PageCount)
                return null;
            return pagelist;
        }
        protected IQueryable<RoomVM> GetStuffFromDatabase(List<RoomVM> roomvm)
        {
            var Rooms = _context.Rooms;
            var RoomImages = _context.RoomImages;

            var query =
                      from R in roomvm
                      select new RoomVM
                      {
                          Address = R.Address,
                          Type = R.Type,
                          Name = R.Name,
                          Price = R.Price,
                      };
            return query.AsQueryable();
        }


        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rooms == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .FirstOrDefaultAsync(m => m.RoomId == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Rooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomVM addRoomRequest)
        {
            var room = new Room()
            {
                RoomId = addRoomRequest.RoomId,
                Type = addRoomRequest.Type,
                Name = addRoomRequest.Name,
                Price = addRoomRequest.Price,
                Address = addRoomRequest.Address,
            };
            int num = 0;
            if (addRoomRequest.Photo == null)
            {
                await _context.Rooms.AddAsync(room);
                await _context.SaveChangesAsync();
            }
            else
            {
                await _context.Rooms.AddAsync(room);
                await _context.SaveChangesAsync();
                foreach (var items in addRoomRequest.Photo)
                {
                    var newRoom = _context.Rooms.OrderByDescending(p => p.RoomId).FirstOrDefault(a => a.Name == room.Name);
                    string photoName = Guid.NewGuid().ToString() + ".png";
                    var filePath = Path.GetTempFileName();
                    //string extension = Path.GetExtension(photoName).ToLowerInvariant();
                    //// 判斷是否為允許上傳的檔案附檔名
                    //List<string> allowedExtextsion = new List<string> { ".jpg", ".bmp",".png" };

                    string path = _webHostEnvironment.WebRootPath + "/RoomImages/" + photoName;
                    using (var stream = System.IO.File.Create(path))
                    {
                        addRoomRequest.Photo[num].CopyToAsync(stream);
                        num++;
                    }
                    var roompicture = new RoomImage()
                    {
                        RoomId = newRoom.RoomId,
                        Image = photoName
                    };
                    await _context.RoomImages.AddAsync(roompicture);
                }
                await _context.SaveChangesAsync();
            }


            return RedirectToAction("Index");

        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rooms == null)
            {
                return NotFound();
            }
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            var roomvm = room.ToEntity();
            var roompics = _context.RoomImages.Where(x => x.RoomId == roomvm.RoomId).ToList();
            roomvm.fileName = roompics.Select(x => x.Image).ToArray();
            return View(roomvm);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoomId,Type,Name,Price,Address,Photo")] RoomVM room)
        {
            if (id != room.RoomId)
            {
                return NotFound();
            }
            var roomvm = new Room()
            {
                RoomId = room.RoomId,
                Type = room.Type,
                Name = room.Name,
                Price = room.Price,
                Address = room.Address,
            };
            if (room.Photo == null)
            {

                _context.Update(roomvm);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.Update(roomvm);
                await _context.SaveChangesAsync();
                var newRoom = _context.Rooms.FirstOrDefault(p => p.RoomId==roomvm.RoomId);
                int num = 0;
                foreach (var items in room.Photo)
                {
                    string photoName = Guid.NewGuid().ToString() + ".png";
                    string path = _webHostEnvironment.WebRootPath + "/RoomImages/" + photoName;
                    using (var stream = System.IO.File.Create(path))
                    {
                        room.Photo[num].CopyToAsync(stream);
                        num++;
                    }
                    var roompicture = new RoomImage()
                    {
                        RoomId = newRoom.RoomId,
                        Image = photoName
                    };
                    await _context.RoomImages.AddAsync(roompicture);
                    _context.Update(roomvm);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Rooms/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || _context.Rooms == null)
            {
                return NotFound();
            }

            var room =  _context.Rooms
                .FirstOrDefault(m => m.RoomId == id);
            var roomvm = room.ToEntity();
            var roompics = _context.RoomImages.Where(x => x.RoomId == roomvm.RoomId).ToList();
            roomvm.fileName = roompics.Select(x => x.Image).ToArray();
            if (room == null)
            {
                return NotFound();
            }

            return View(roomvm);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public  IActionResult DeleteConfirmed(int id)
        {
            if (_context.Rooms == null)
            {
                return Problem("Entity set 'PetSiteContext.Rooms'  is null.");
            }
            var room =  _context.Rooms.Find(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
            }
            var roomvm = room.ToEntity();
            var roompics = _context.RoomImages.Where(x => x.RoomId == roomvm.RoomId).ToList();
            roomvm.fileName = roompics.Select(x => x.Image).ToArray();
            foreach (var n in roomvm.fileName)
            {
                string sourceDir = _webHostEnvironment.WebRootPath + "/RoomImages/" + n;
                System.IO.File.Delete(sourceDir);
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public string ImgDelete(string img)
        {

            string sourceDir = _webHostEnvironment.WebRootPath + "/RoomImages/" + img;
            System.IO.File.Delete(sourceDir);
            var deleteimg = _context.RoomImages.Where(x => x.Image == img).FirstOrDefault();
            _context.RoomImages.Remove(deleteimg);
            _context.SaveChanges();
            return ("已成功刪除");

        }
    }
}