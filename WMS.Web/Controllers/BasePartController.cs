using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS.WebApi.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WMS.IService;
using WMS.Model.Entity;
using WMS.Model.DTO;
using WMS.WebApi.Common;

namespace WMS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BasePartController : ControllerBase
    {
        private readonly IBasePartService _iBasePartService;

        public BasePartController(IBasePartService iBasePartService)
        {
            _iBasePartService = iBasePartService;

        }

        [HttpGet("GetAll")]
        public async Task<ApiResult> GetUser()
        {
            var data = await _iBasePartService.QueryAsync();
            return ApiResultHelper.Success(data);
        }

        [HttpGet("Find")]
        public async Task<ApiResult> Find([FromQuery] string id)
        {
            var data = await _iBasePartService.FindAsync(id);
            if (data == null) return ApiResultHelper.Error("查询失败");
            return ApiResultHelper.Success(data);
        }

        [HttpPost("Create")]
        public async Task<ApiResult> Create([FromBody] BasePart basePart)
        {
            basePart.Id = Guid.NewGuid().ToString("N");
            if (string.IsNullOrEmpty(basePart.PartNo))
            {
                basePart.PartNo = "PART" + DateTime.Now.ToString("yyyyMMddHHmmss");
            }
            if (string.IsNullOrEmpty(basePart.PartName))
            {
                return ApiResultHelper.Error("物料名称不允许为空");
            }
            var cs = await _iBasePartService.FindAsync(x => x.PartNo == basePart.PartNo && x.CreateOwner == basePart.CreateOwner&&x.Status==0);
            if (cs != null) return ApiResultHelper.Error("物料编号已存在");
            cs = await _iBasePartService.FindAsync(x => x.PartName == basePart.PartName && x.PartSpec == basePart.PartSpec && x.CreateOwner == basePart.CreateOwner && x.Status == 0);
            if (cs != null) return ApiResultHelper.Error("已存在同名物料名称+规格");
            basePart.CreateTime = DateTime.Now;
            basePart.Status = 0;
            var b = await _iBasePartService.CreateAsync(basePart);
            if (b == null) return ApiResultHelper.Error("新增失败");
            return ApiResultHelper.Success("新增成功");
        }

        [HttpPost("Edit")]
        public async Task<ApiResult> EditUser([FromBody] BasePart basePart)
        {
            var data = await _iBasePartService.EditAsync(basePart);
            if (data != 1) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success("修改成功");
        }

        [HttpGet("QueryPartName")]
        public async Task<ApiResult> QueryPartName([FromServices] IMapper iMapper, [FromQuery] string createOwner)
        {
            List<BasePart> basePart = await _iBasePartService.QueryAsync(x => x.CreateOwner == createOwner && x.Status == 0);
            var baseParteDTO = iMapper.Map<List<BasePartDTO>>(basePart);
            return ApiResultHelper.Success(baseParteDTO);
        }


        [HttpGet("QueryPage")]
        public async Task<ApiResult> QueryPage([FromQuery] string Func, [FromQuery] int num, [FromQuery] string createOwner)
        {
            Expression<Func<BasePart, bool>> func = u => true;
            if (!string.IsNullOrEmpty(Func))
            {
                func = ExpressionFuncExtender.And<BasePart>(func, x => x.PartName.ToLower().Contains(Func.Trim().ToLower())
                || x.PartNo.ToLower().Contains(Func.Trim().ToLower()));
            }
            func = ExpressionFuncExtender.And<BasePart>(func, x => x.CreateOwner == createOwner);
            var data = await _iBasePartService.QueryAsync(func, num, x => x.CreateTime );
            return ApiResultHelper.Success(data);
        }


        [HttpPost("CreateByImg")]
        [RequestSizeLimit(Int64.MaxValue)]
        public async Task<ApiResult> CreateByImg([FromForm] IFormCollection collection)
        {
            BasePart basePart = new BasePart();
            basePart.Id = Guid.NewGuid().ToString("N");
            basePart.PartNo = collection["partNo"];
            if (string.IsNullOrEmpty(basePart.PartNo))
            {
                basePart.PartNo = "PART" + DateTime.Now.ToString("yyyyMMddHHmmss");
            }
            if (string.IsNullOrEmpty(collection["partName"]))
            {
                return ApiResultHelper.Error("物料名称不允许为空");
            }
            if (string.IsNullOrEmpty(collection["partName"]))
            {
                return ApiResultHelper.Error("物料名称不允许为空");
            }
            basePart.PartName = collection["partName"];
            basePart.PartSpec = collection["partSpec"];
            basePart.PartType = Convert.ToInt32(collection["partType"]);
            basePart.Memo = collection["Memo"];
            basePart.CreateOwner = collection["createOwner"];
            basePart.CreateTime = DateTime.Now;
            basePart.Status = 0;
            var cs = await _iBasePartService.FindAsync(x => x.PartNo == basePart.PartNo && x.CreateOwner == basePart.CreateOwner);
            if (cs != null) return ApiResultHelper.Error("物料编号已存在");
            cs = await _iBasePartService.FindAsync(x => x.PartName == basePart.PartName && x.PartSpec == basePart.PartSpec && x.CreateOwner == basePart.CreateOwner);
            if (cs != null) return ApiResultHelper.Error("已存在同名物料名称+规格");
            FormFileCollection filelist = (FormFileCollection)collection.Files;
            foreach (IFormFile file in filelist)
            {
                string temp = Guid.NewGuid().ToString("N");
                string Tpath = "./static/images/part/";
                string name = filelist[0].FileName;
                string type = System.IO.Path.GetExtension(name);
                DirectoryInfo di = new DirectoryInfo(Tpath);
                basePart.PartImage = "/img/part/" + temp + type;
                if (!di.Exists)
                {
                    di.Create();
                }
                using (FileStream fs = System.IO.File.Create(Tpath + temp + type))
                {
                    filelist[0].CopyTo(fs);
                    fs.Flush();
                }
                break;
            }
            var b = await _iBasePartService.CreateAsync(basePart);
            if (b == null) return ApiResultHelper.Error("新增失败");
            return ApiResultHelper.Success("新增成功");
        }

        [HttpGet("Invalid")]
        public async Task<ApiResult> Invalid([FromQuery] string id, [FromQuery] string invalidOwner)
        {
            var data = await _iBasePartService.FindAsync(x => x.Id == id.Trim());
            if (data == null) return ApiResultHelper.Error("物料不存在");
            data.InvalidOwner = invalidOwner;
            data.InvalidTime = DateTime.Now;
            data.Status = 1;
            int num = await _iBasePartService.EditAsync(data);
            if (num != 1) return ApiResultHelper.Error("作废失败");
            return ApiResultHelper.Success(data);
        }
    }
}
