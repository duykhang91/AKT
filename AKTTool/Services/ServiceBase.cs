using AKTTool.Database;
using AKTTool.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace AKTTool.Services
{
  public abstract class ServiceBase
  {
    protected readonly IUnitOfWork UnitOfWork;
    //protected readonly IMapper Mapper;
    protected readonly AppDbContext AppContext;

    protected ServiceBase(
      IUnitOfWork unitOfWork,
      //IMapper mapper, 
      AppDbContext appContext)
    {
      UnitOfWork = unitOfWork;
      //Mapper = mapper;
      AppContext = appContext;
    }
  }
}
