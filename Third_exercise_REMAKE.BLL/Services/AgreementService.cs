using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Third_exercise_REMAKE.BLL.Dtos.Agreement;
using Third_exercise_REMAKE.BLL.Helper;
using Third_exercise_REMAKE.BLL.IServices;
using Third_exercise_REMAKE.Core.Models;
using Third_exercise_REMAKE.DAL.IRepository;

namespace Third_exercise_REMAKE.BLL.Services
{
    public class AgreementService : IAgreementService
    {
        IAgreementRepository _agreementRepository;
        public AgreementService(IAgreementRepository AgreementRepository)
        {
            _agreementRepository = AgreementRepository;
            SeedData();
        }
        public int Create(AgreementModel agreement)
        {
            agreement.DaysUntilExpiration = agreement.ExpirationDate.Day - agreement.CreatedDate.Day;

            _agreementRepository.Add(agreement);
            return _agreementRepository.SaveChanges();

        }
        public FilterResultDto Paging(AgreementPagingDto dto)
        {
            int start = dto.start ?? 0;
            int end = dto.end ?? 50;

            List<AgreementModel> queryResult = _agreementRepository.FilterSortPaging(null, null, from: start, total: end - start + 1);
            int queryResultSize = queryResult.Count();
            int lastIndex = -1;
            int blockSize = end - start;

            if (queryResultSize == 0)
            {
                lastIndex = 0;
            }
            else if (queryResultSize <= blockSize)
            {
                lastIndex = queryResultSize + start;
            }
            else
            {
                queryResult.RemoveAt(queryResultSize - 1);
            }

            var Dto = new FilterResultDto()
            {
                agreementList = queryResult,
                lastIndex = lastIndex
            };

            return Dto;
        }
        public FilterResultDto FilterSortPaging(AgreementFilterSortPagingDto dto)
        {
            int start = dto.pagingDto.start ?? 0;
            int end = dto.pagingDto.end ?? 50;

            var filterDtoList = dto.filterDtoList;
            var sortDto = dto.sortDto;
            int blockSize = end - start;
            int queryResultSize = 0;
            int lastIndex = -1;

            Expression<Func<AgreementModel, bool>> filterQuery = x => true;
            List<Expression<Func<AgreementModel, bool>>> filterQueries = new List<Expression<Func<AgreementModel, bool>>>();
            filterDtoList.ForEach(filterModel =>
            {
                switch (filterModel.columnName)
                {
                    case "status":
                        filterQueries.Add(f => f.Status.Contains(filterModel.filterValue));
                        break;
                    case "quoteNumber":
                        filterQueries.Add(f => f.QuoteNumber.Contains(filterModel.filterValue));
                        break;
                    case "agreementName":
                        filterQueries.Add(f => f.AgreementName.Contains(filterModel.filterValue));
                        break;
                    case "agreementType":
                        filterQueries.Add(f => f.AgreementType.Contains(filterModel.filterValue));
                        break;
                    case "distributorName":
                        filterQueries.Add(f => f.DistributorName.Contains(filterModel.filterValue));
                        break;
                    case "effectiveDate":
                        filterQueries.Add(f => f.EffectiveDate.Date.Equals(DateTime.Parse(filterModel.filterValue).Date));
                        break;
                    case "expirationDate":
                        filterQueries.Add(f => f.ExpirationDate.Date.Equals(DateTime.Parse(filterModel.filterValue).Date));
                        break;
                    case "createdDate":
                        filterQueries.Add(f => f.CreatedDate.Date.Equals(DateTime.Parse(filterModel.filterValue).Date));
                        break;
                    case "daysUntilExpiration":
                        filterQueries.Add(f => f.DaysUntilExpiration.Equals(Int32.Parse(filterModel.filterValue)));
                        break;
                }
            });

            var type = sortDto.sortType;
            Func<IQueryable<AgreementModel>, IOrderedQueryable<AgreementModel>> sortQuery = x => x.OrderBy(x => x.Id);

            if (sortDto != null)
            {
                switch (sortDto.sortColumn)
                {
                    case "id":
                        if (type == "asc")
                            sortQuery = x => x.OrderBy(x => x.Id);
                        else
                            sortQuery = x => x.OrderByDescending(x => x.Id);
                        break;
                    case "status":
                        if (type == "asc")
                            sortQuery = x => x.OrderBy(x => x.Status);
                        else
                            sortQuery = x => x.OrderByDescending(x => x.Status);
                        break;
                    case "quoteNumber":
                        if (type == "asc")
                            sortQuery = x => x.OrderBy(x => x.QuoteNumber);
                        else
                            sortQuery = x => x.OrderByDescending(x => x.QuoteNumber);
                        break;
                    case "agreementName":
                        if (type == "asc")
                            sortQuery = x => x.OrderBy(x => x.AgreementName);
                        else
                            sortQuery = x => x.OrderByDescending(x => x.AgreementName);
                        break;
                    case "agreementType":
                        if (type == "asc")
                            sortQuery = x => x.OrderBy(x => x.AgreementType);
                        else
                            sortQuery = x => x.OrderByDescending(x => x.AgreementType);
                        break;
                    case "distributorName":
                        if (type == "asc")
                            sortQuery = x => x.OrderBy(x => x.DistributorName);
                        else
                            sortQuery = x => x.OrderByDescending(x => x.DistributorName);
                        break;
                    case "effectiveDate":
                        if (type == "asc")
                            sortQuery = x => x.OrderBy(x => x.EffectiveDate);
                        else
                            sortQuery = x => x.OrderByDescending(x => x.EffectiveDate);
                        break;
                    case "expirationDate":
                        if (type == "asc")
                            sortQuery = x => x.OrderBy(x => x.ExpirationDate);
                        else
                            sortQuery = x => x.OrderByDescending(x => x.ExpirationDate);
                        break;
                    case "createdDate":
                        if (type == "asc")
                            sortQuery = x => x.OrderBy(x => x.CreatedDate);
                        else
                            sortQuery = x => x.OrderByDescending(x => x.CreatedDate);
                        break;
                    case "daysUntilExpiration":
                        if (type == "asc")
                            sortQuery = x => x.OrderBy(x => x.DaysUntilExpiration);
                        else
                            sortQuery = x => x.OrderByDescending(x => x.DaysUntilExpiration);
                        break;
                    default:
                        sortQuery = x => x.OrderBy(x => x.Id);
                        break;
                }
            }

            List<AgreementModel> queryResult = _agreementRepository.FilterSortPaging(filterQueries, sortQuery, start, end - start + 1);

            queryResultSize = queryResult.Count();
            if (queryResultSize == 0)
            {
                lastIndex = 0;
            }
            else if (queryResultSize <= blockSize)
            {
                lastIndex = queryResultSize + start;
            }
            else
            {
                queryResult.RemoveAt(queryResultSize - 1);
            }

            var result = new FilterResultDto()
            {
                agreementList = queryResult,
                lastIndex = lastIndex
            };

            return result;
        }

        public AgreementModel Update(AgreementDto dto)
        {
            if (IsExist(dto.Id + ""))
            {
                AgreementModel model = new AgreementModel
                {
                    Id = dto.Id,
                    AgreementName = dto.AgreementName,
                    AgreementType = dto.AgreementType,
                    CreatedDate = dto.CreatedDate,
                    DistributorName = dto.DistributorName,
                    EffectiveDate = dto.EffectiveDate,
                    ExpirationDate = dto.ExpirationDate,
                    QuoteNumber = dto.QuoteNumber,
                    Status = dto.Status
                };

                _agreementRepository.Update(model);
                return GetById(model.Id + "");
            }
            else throw new Exception("Id not found");
        }

        public bool Delete(string id)
        {
            var model = GetById(id);
            if (model == null) return false;
            else
            {
                _agreementRepository.Delete(model);
                _agreementRepository.SaveChanges();
                return true;
            }
        }
        public AgreementModel GetById(string id)
        {
            var Agreements = _agreementRepository.Filter(x => x.Id == Int32.Parse(id));
            if (Agreements.Any()) return Agreements.First();
            else return null;
        }
        private void SeedData()
        {
            if (_agreementRepository.Any())
            {
                return;
            }
            var records = SeedHelper.SeedData<AgreementModel>("./SeedingData.json");
            records.ForEach(record => record.DaysUntilExpiration = new TimeSpan((record.ExpirationDate - record.CreatedDate).Ticks).Days);
            _agreementRepository.AddRange(records);
            _agreementRepository.SaveChanges();
        }
        public bool IsExist(string id)
        {
            return _agreementRepository.IsExist(Int32.Parse(id));
        }

    }
}
