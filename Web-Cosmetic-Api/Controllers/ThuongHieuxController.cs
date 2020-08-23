using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Web_Cosmetic_Api.Models;

namespace Web_Cosmetic_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThuongHieuxController : ControllerBase
    {
        private readonly usb40857_webusbeautyContext _context;

        public ThuongHieuxController(usb40857_webusbeautyContext context)
        {
            _context = context;
        }

        // GET: api/ThuongHieux
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThuongHieu>>> GetThuongHieux()
        {
            var thuongHieux = _context.ThuongHieux.ToList();
            string str = "";
            foreach (var th in thuongHieux)
            {
                var data = new { id = th.Id, moTa = th.MoTa, tenThuongHieu = th.TenThuongHieu};
                str += JsonConvert.SerializeObject(data) + ",";
            }
            if (str == "")
            {
                return Ok("[]");
            }
            str = str.Remove(str.Length - 1);
            str = "[" + str + "]";
            return Ok(str);

        }

        // GET: api/ThuongHieux/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ThuongHieu>> GetThuongHieu(int id)
        {
            //var thuongHieu = _context.ThuongHieux
            //                                        .Include(th => th.LoaiSps)
            //                                            .ThenInclude(lSp => lSp.SanPhams)
            //                                                .ThenInclude(sp => sp.KichCoSps)
            //                                        .Include(th => th.LoaiSps)
            //                                            .ThenInclude(lSp => lSp.SanPhams)
            //                                                .ThenInclude(sp => sp.KichCoSps)
            //                                        .Where(th => th.Id == id)
            //                                        .FirstOrDefault();

            var thuongHieu = await _context.ThuongHieux.SingleAsync(th => th.Id == id);
            _context.Entry(thuongHieu)
                .Collection(lsp => lsp.LoaiSps)
                .Query()
                .Include(sp => sp.SanPhams)
                .ThenInclude(sp => sp.HinhAnhs)
                .Load();
            _context.Entry(thuongHieu)
                .Collection(lsp => lsp.LoaiSps)
                .Query()
                .Include(sp => sp.SanPhams)
                .ThenInclude(sp => sp.KichCoSps)
                .Load();
            _context.Entry(thuongHieu)
                .Collection(lsp => lsp.HinhAnhs)
                .Load();
            if (thuongHieu == null)
            {
                return NotFound();
            }

            return thuongHieu;
        }
        // GET: api/ThuongHieux/5
        [HttpGet("getDetailSanPham/{idSP}")]
        public async Task<ActionResult<ThuongHieu>> GetDetailSanPham(int idSP)
        {
            var sanPham = _context.SanPhams.First(x => x.Id == idSP);
            var loaiSanPham = _context.LoaiSps.First(x => x.Id == sanPham.IdLoaiSp);
            var thuongHieu = _context.ThuongHieux.First(x => x.Id == loaiSanPham.IdThuongHieu);
            //var sanphaminfo = _context.ThuongHieux.Where(th => th.Id == thuongHieu.Id)
            //                                        //loc Loai sp
            //                                        .Select(b => new
            //                                        {
            //                                            b,
            //                                            LoaiSp = b.LoaiSps.Where(p => p.Id == loaiSanPham.Id)
            //                                                //Loc Sp
            //                                                .Select(sp => new
            //                                                {
            //                                                    sp,
            //                                                    SanPham = sp.SanPhams.Where(y => y.Id == idSP)
            //                                                        //loc Hinh anh
            //                                                        .Select(c => new
            //                                                        {
            //                                                            c,
            //                                                            HinhAnh = c.HinhAnhs
            //                                                        })

            //                                                })

            //                                        }) 
            //                                        .AsEnumerable().Select(x => x.b)
            //                                        .FirstOrDefault();
            var sanphaminfo = await _context.ThuongHieux.SingleAsync(th => th.Id == thuongHieu.Id);
            _context.Entry(thuongHieu)
                .Collection(lsp => lsp.LoaiSps)
                .Query().Where(lsp => lsp.Id == loaiSanPham.Id)
                .Include(sp => sp.SanPhams)
                .ThenInclude(sp => sp.HinhAnhs)
                .Load();
            _context.Entry(thuongHieu)
                .Collection(lsp => lsp.LoaiSps)
                .Query().Where(lsp => lsp.Id == loaiSanPham.Id)
                .Include(sp => sp.SanPhams)
                .ThenInclude(sp => sp.KichCoSps)
                .Load();
            _context.Entry(thuongHieu)
                .Collection(lsp => lsp.HinhAnhs)
                .Load();
            _context.Entry(thuongHieu)
                .Collection(lsp => lsp.LoaiSps)
                .Query().Where(lsp => lsp.Id == loaiSanPham.Id)
                .Include(sp => sp.SanPhams).Where(sp =>sp.Id == sanPham.Id)
                .Load();
            if (sanphaminfo == null)
            {
                return NotFound();
            }

            return sanphaminfo;
        }
        [HttpGet("GetDsLoaiSanPham/{idTH}")]
        public async Task<ActionResult<LoaiSp>> GetDsLoaiSanPham(int idTH)
        {
            var loaiSp = _context.LoaiSps.Where(lsp=> lsp.IdThuongHieu == idTH).ToList();
            string str = "";
            foreach (var th in loaiSp)
            {
                var data = new { id = th.Id, moTa = th.MoTa, tenThuongHieu = th.TenLoaiSp };
                str += JsonConvert.SerializeObject(data) + ",";
            }
            if (str == "")
            {
                return Ok("[]");
            }
            str = str.Remove(str.Length - 1);
            str = "[" + str + "]";
            return Ok(str);

        }
        [HttpGet("GetDsSanPham/{idTH}")]
        public async Task<ActionResult<ThuongHieu>> GetDsSanPham(int idTH)
        {
            var thuongHieu = _context.ThuongHieux.First(x => x.Id == idTH);
            var dssanpham = await _context.ThuongHieux.SingleAsync(th => th.Id == idTH);
            _context.Entry(thuongHieu)
                .Collection(lsp => lsp.LoaiSps)
                .Query()
                .Include(sp => sp.SanPhams)
                .ThenInclude(sp => sp.HinhAnhs)
                .Load();
            if (dssanpham == null)
            {
                return NotFound();
            }

            return dssanpham;

        }
        // PUT: api/ThuongHieux/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutThuongHieu(int id,ThuongHieu thuongHieu)
        {
            var thuongHieux = new ThuongHieu();
            
            var result = _context.ThuongHieux.Where(th => th.Id == id).FirstOrDefault<ThuongHieu>();
            if( result != null && thuongHieu != null)
            {
                    result.TenThuongHieu = thuongHieu.TenThuongHieu;
                    result.MoTa = thuongHieu.MoTa;
                _context.SaveChanges();
            }
            return Ok();
        }

        // POST: api/ThuongHieux
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ThuongHieu>> PostThuongHieu(ThuongHieu thuongHieu)
        {
            _context.ThuongHieux.Add(thuongHieu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetThuongHieu", new { id = thuongHieu.Id }, thuongHieu);
        }

        // DELETE: api/ThuongHieux/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ThuongHieu>> DeleteThuongHieu(int id)
        {
            var thuongHieu = await _context.ThuongHieux.FindAsync(id);
            if (thuongHieu == null)
            {
                return NotFound();
            }
          
            _context.ThuongHieux.Remove(thuongHieu);
            await _context.SaveChangesAsync();

            return thuongHieu;
        }
        
        private bool ThuongHieuExists(int id)
        {
            return _context.ThuongHieux.Any(e => e.Id == id);
        }
    }
}
