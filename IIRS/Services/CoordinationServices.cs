using IIRS.IRepository.Base;
using IIRS.IServices;
using IIRS.Models.ViewModel;
using IIRS.Services.Base;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Services
{
    public class CoordinationServices : BaseServices, ICoordinationServices
    {
        private readonly ILogger<CoordinationServices> _logger;
        private readonly IDBTransManagement _dbTransManagement;
        public CoordinationServices(IDBTransManagement dbTransManagement, ILogger<CoordinationServices> logger) : base(dbTransManagement)
        {
            _logger = logger;
            _dbTransManagement = dbTransManagement;
        }
        /// <summary>
        /// 社区教育
        /// </summary>
        /// <param name="qlrmc"></param>
        /// <param name="zjhm"></param>
        /// <returns></returns>
        public Task<List<CoordinationVModel>> GetCommunityList(string qlrmc, string zjhm)
        {

            throw new NotImplementedException();
        }
        /// <summary>
        /// 开发企业
        /// </summary>
        /// <param name="qlrmc"></param>
        /// <param name="zjhm"></param>
        /// <returns></returns>
        public Task<List<CoordinationVModel>> GetEnterpriseList(string qlrmc, string zjhm)
        {
            throw new NotImplementedException();
        }
        public Task<List<CoordinationVModel>> GetCommunityPeopleList(string zl)
        {
            throw new NotImplementedException();
        }
        public Task<List<CoordinationVModel>> GetIntermediaryList(string qlrmc, string zjhm)
        {
            throw new NotImplementedException();
        }

        public Task<List<CoordinationVModel>> GetPaymentTransferList(string qlrmc, string zjhm)
        {
            throw new NotImplementedException();
        }
    }
}
