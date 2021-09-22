﻿using System.Collections.Generic;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System.Linq; //aşağıdaki kodları yazmadan önce bunu mutlaka ekle yoksa yazarken sıkıntı çıkıyor

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, NorthwindContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = from oc in context.OperationClaims
                             join uoc in context.UserOperationClaims
                             on oc.Id equals uoc.OperationClaimId
                             where uoc.UserId == user.Id
                             select new OperationClaim()
                             {
                                 Id = oc.Id,
                                 Name = oc.Name
                             };

                return result.ToList();
            }
        }
    }
}
