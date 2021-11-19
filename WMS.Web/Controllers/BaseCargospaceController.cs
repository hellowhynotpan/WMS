using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS.WebApi.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WMS.IService;
using WMS.Model;
using WMS.Model.DTO;
using WMS.WebApi.Common;

namespace WMS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BaseCargospaceController : ControllerBase
    {
        private readonly IBaseCargospaceService _iBaseCargospaceService;

        private readonly IBaseWareHouseService _iBaseWareHouseService;

        public BaseCargospaceController(IBaseCargospaceService iBaseCargospaceService,IBaseWareHouseService iBaseWareHouseService)
        {
            _iBaseCargospaceService = iBaseCargospaceService;
            _iBaseWareHouseService = iBaseWareHouseService;

        }

        [HttpGet("GetAll")]
        public async Task<ApiResult> GetUser()
        {
            var data = await _iBaseCargospaceService.QueryAsync();
            return ApiResultHelper.Success(data);
        }

        [HttpGet("Find")]
        public async Task<ApiResult> QueryUserById([FromServices] IMapper iMapper,[FromQuery] string id)
        {
            var data = await _iBaseCargospaceService.FindAsync(id);
            if (data == null) return ApiResultHelper.Error("不存在该储位");
            var wh = await _iBaseWareHouseService.FindAsync(data.WhId);
            if (wh == null) return ApiResultHelper.Error("对应仓库不存在");
            if (wh.Status == 1) return ApiResultHelper.Error("仓库已废弃");
            var csDto=iMapper.Map<BaseCargospaceDTO>(data);
            csDto.WhName = wh.WhName;
            return ApiResultHelper.Success(csDto);
        }

        [HttpPost("Edit")]
        public async Task<ApiResult> Edit([FromBody] BaseCargospaceDTO baseCargospaceDTO)
        {
            
            var data = await _iBaseCargospaceService.FindAsync(baseCargospaceDTO.Id);
            if (data == null) return ApiResultHelper.Error("不存在该储位");
            data.CsName = baseCargospaceDTO.CsName;
            data.CsNo = baseCargospaceDTO.CsNo;
            data.LastUpdOwner = baseCargospaceDTO.LastUpdOwner;
            data.LastUpdTime = DateTime.Now;
            data.Memo = baseCargospaceDTO.Memo;
            data.Status = baseCargospaceDTO.Status;
            if (!string.IsNullOrEmpty(baseCargospaceDTO.WhId))
            {
                var wh = await _iBaseWareHouseService.FindAsync(x => x.Id == baseCargospaceDTO.WhId);
                if (wh == null) return ApiResultHelper.Error("对应仓库不存在");
                if (wh.Status == 1) return ApiResultHelper.Error("仓库已废弃");
                data.WhId = baseCargospaceDTO.WhId;
            }
            var cs = await _iBaseCargospaceService.EditAsync(data);
            if (cs != 1) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success("修改成功");
        }

        [HttpPost("Create")]
        public async Task<ApiResult> Create( [FromBody] BaseCargospace baseCargospace)
        {
            var wh= await _iBaseWareHouseService.FindAsync(x => x.Id == baseCargospace.WhId);
            if (wh == null) return ApiResultHelper.Error("不存在该仓库");
            if (wh.Status == 1) return ApiResultHelper.Error("该仓库已作废");
            var cs = await _iBaseCargospaceService.FindAsync(x => x.CsNo == baseCargospace.CsNo && x.CreateOwner == baseCargospace.CreateOwner);
            if (cs != null) return ApiResultHelper.Error("储位编号重复");
            cs = await _iBaseCargospaceService.FindAsync(x => x.CsName == baseCargospace.CsName&&x.CreateOwner== baseCargospace.CreateOwner);
            if (cs != null) return ApiResultHelper.Error("储位已存在");
            baseCargospace.Id = Guid.NewGuid().ToString("N");
            if (string.IsNullOrEmpty(baseCargospace.CsNo))
            {
                baseCargospace.CsNo = "CS" + DateTime.Now.ToString("yyyyMMddHHmmss");
            }
            baseCargospace.Status = 0;
            baseCargospace.CreateTime = DateTime.Now;
            var data = await _iBaseCargospaceService.CreateAsync(baseCargospace);
            if (data == null) return ApiResultHelper.Error("新增失败");
            return ApiResultHelper.Success("新增成功");
        }

        [HttpGet("QueryPage")]
        public async Task<ApiResult> QueryPage([FromQuery] string Func, [FromQuery] int num, [FromQuery] string createOwner, [FromQuery] string whId, [FromQuery] int status)
        {
            Expression<Func<BaseCargospace, bool>> func = u => true;
            if (!string.IsNullOrEmpty(Func))
            {
                func = ExpressionFuncExtender.And<BaseCargospace>(func, x => x.CsNo.ToLower().Contains(Func.Trim().ToLower())
                || x.CsNo.ToLower().Contains(Func.Trim().ToLower()));
            }
            if (!string.IsNullOrEmpty(whId))
            {
                func = ExpressionFuncExtender.And<BaseCargospace>(func, x => x.WhId== whId);
            }
            func = ExpressionFuncExtender.And<BaseCargospace>(func, x => x.CreateOwner == createOwner && x.Status ==status);
            var data = await _iBaseCargospaceService.QueryAsync(func, num, x => x.CreateTime);
            return ApiResultHelper.Success(data);
        }

        [HttpGet("Invalid")]
        public async Task<ApiResult> Invalid([FromQuery] string id, [FromQuery] string invalidOwner)
        {
            var data = await _iBaseCargospaceService.FindAsync(x => x.Id == id.Trim());
            if (data == null) return ApiResultHelper.Error("储位不存在");
            data.InvalidOwner = invalidOwner;
            data.InvalidTime = DateTime.Now;
            data.Status = 1;
            int num = await _iBaseCargospaceService.EditAsync(data);
            if (num != 1) return ApiResultHelper.Error("作废失败");
            return ApiResultHelper.Success(data);
        }

        [HttpGet("QueryCsName")]
        public async Task<ApiResult> QueryCsName([FromServices] IMapper iMapper, [FromQuery] string createOwner)
        {
            List<BaseCargospace> baseCargospace = await _iBaseCargospaceService.QueryAsync(x => x.CreateOwner == createOwner && x.Status == 0);
            var baseCargospaceDTO = iMapper.Map<List<BaseCargospaceDTO>>(baseCargospace);
            return ApiResultHelper.Success(baseCargospaceDTO);
        }

        [HttpGet("Frozen")]
        public async Task<ApiResult> Frozen([FromQuery] string id, [FromQuery] string freezeOwner)
        {
            var data = await _iBaseCargospaceService.FindAsync(x => x.Id == id.Trim());
            if (data == null) return ApiResultHelper.Error("储位不存在");
            if (data.Status == 1) return ApiResultHelper.Error("储位已作废");
            if (data.Status == 2) return ApiResultHelper.Error("储位已冻结");
            data.FreezeOwner = freezeOwner;
            data.FreezeTime = DateTime.Now;
            data.Status = 2;
            int num = await _iBaseCargospaceService.EditAsync(data);
            if (num != 1) return ApiResultHelper.Error("作废失败");
            return ApiResultHelper.Success(data);
        }
    }
}
