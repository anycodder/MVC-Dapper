using Dapper.Project.Core.Entity;
namespace Dapper.Project.Data;

/*Interface sınıfında sadece method tanımları bulunur. İçlerine kod parçacığı yazılmaz.
 İçerisinde tanımlanan method tanımları bu interface’i implemente edecek diğer sınıflar tarafından implement edilmesi zorunludur*/


public interface IRepository<TEntity>    //entitylerinden birini alabilir ve bu değişkendir bu yüzden generic bir tip yazıyopruz
{
    IEnumerable<TEntity> GetAll();  //listenin tümünü getirme
    //TEntity GetById(int id);        //id bulma
    Task<TEntity> GetById(int id); //asekron id bulma
    Task<TEntity> Insert(TEntity obj);       //ekleme
    Task<TEntity> Update(TEntity obj);       //güncelleme
    Task<TEntity> Delete(int id);            //silme
}



