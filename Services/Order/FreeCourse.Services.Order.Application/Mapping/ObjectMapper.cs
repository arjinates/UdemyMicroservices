using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Application.Mapping
{
    //Class Library'lerde DI Container olmadıgı icin startup icinde configureServices'e AddAutoMapper() ekleyemiyorum, buradan static ekliyorum
    public class ObjectMapper
    {
        //Sadece Mapper cagrildiginda initialize etsin diye Lazy sinifini kullanıyorum
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
       {
           var config = new MapperConfiguration(cfg =>
           {
               cfg.AddProfile<CustomMapping>();
           });

           return config.CreateMapper();
       });

        public static IMapper Mapper => lazy.Value; //Ben ObjectMapper.Mapper'i cagirmayana kadar yukarıdaki kodlar calismayacak
    }
}
