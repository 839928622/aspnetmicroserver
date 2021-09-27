using Mapster;
using Ordering.Domain.Entities;

namespace Ordering.Application.Helpers
{
    public class MapperRegister : ICodeGenerationRegister
    {
        /// <inheritdoc />
        public void Register(CodeGenerationConfig config)
        {
            config.AdaptTo("[name]Dto")
                .ForType<Order>(cfg =>
                {
                    cfg.Ignore(s =>s.CreatedBy);
                    cfg.Ignore(s => s.CreatedDate);
                    cfg.Ignore(s => s.LastModifiedBy);
                    cfg.Ignore(s => s.LastModifiedDate);
                });

            config.GenerateMapper("[name]Mapper")
                .ForType<Order>();
        }
    }
}